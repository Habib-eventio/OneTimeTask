using CamcoTasks.Infrastructure.CustomModels.HR;
using CamcoTasks.Infrastructure.Entities.HR;
using CamcoTasks.Infrastructure.IRepository.HR;
using Microsoft.EntityFrameworkCore;
using CamcoTasks.Infrastructure;

namespace CamcoTasks.Infrastructure.Repository.HR;

public class PayrollDetailRepository : Repository<PayrollDetail>, IPayrollDetailRepository
{
    public PayrollDetailRepository(DatabaseContext context) : base(context)
    {
    }

    private DatabaseContext DatabaseContext => (DatabaseContext)Context;

    public async Task<List<PayrollSummaryViewModel>> Last4WeeksPayrollSummaryAsync()
    {
        var lastSunday = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek);

        var lastFourPayrolls = await DatabaseContext.Payrolls
            .Where(p => p.DateSubmitted <= lastSunday)
            .OrderByDescending(p => p.DateSubmitted)
            .Take(4)
            .ToListAsync();

        var payrollsWithWeekStart = lastFourPayrolls
            .Select(p => new
            {
                p.Id,
                WeekStartDate = p.DateSubmitted.HasValue
                    ? p.DateSubmitted.Value.AddDays(-(int)p.DateSubmitted.Value.DayOfWeek + 1)
                    : DateTime.MinValue
            })
            .OrderByDescending(p => p.WeekStartDate)
            .ToList();

        var payrollIds = payrollsWithWeekStart.Select(p => p.Id).ToList();

        var payrollDetails = await DatabaseContext.PayrollDetails
            .Where(d => payrollIds.Contains(d.PayrollId))
            .ToListAsync();

        var weeklyPayrollSummary = payrollsWithWeekStart
            .Join(
                payrollDetails,
                payroll => payroll.Id,
                detail => detail.PayrollId,
                (payroll, detail) => new { payroll.WeekStartDate, detail.WeeksGrossPayment }
            )
            .GroupBy(x => x.WeekStartDate)
            .Select(g => new PayrollSummaryViewModel
            {
                WeekStartDate = g.Key,
                TotalWeeklyGrossPayment = g.Sum(x => x.WeeksGrossPayment ?? 0)
            })
            .OrderByDescending(summary => summary.WeekStartDate)
            .ToList();

        return weeklyPayrollSummary;
    }

    public async Task<List<PayrollDetail>> GetLockedPayrollDetailsByEmployeeIdAsync(long employeeId)
    {
        return await (from payroll in DatabaseContext.Payrolls
                       join payrollDetail in DatabaseContext.PayrollDetails on payroll.Id equals payrollDetail.PayrollId
                       where payroll.IsPayrollLocked == true && payrollDetail.EmployeeId == employeeId
                       select payrollDetail).ToListAsync();
    }

    public async Task<bool> GetIsExistPayrollFromPayrollByEmployeeIdAsync(long employeeId)
    {
        var result = await (from payroll in DatabaseContext.Payrolls
                             join payrollDetail in DatabaseContext.PayrollDetails on payroll.Id equals payrollDetail.PayrollId
                             where payroll.IsPayrollLocked == true && payrollDetail.EmployeeId == employeeId
                             select payroll).FirstOrDefaultAsync();
        return result != null;
    }

    public async Task<List<PayrollDetail>> GetLockedPayrollDetailsByDepartmentIdAsync(long departmentId)
    {
        return await (from payroll in DatabaseContext.Payrolls
                       join payrollDetail in DatabaseContext.PayrollDetails on payroll.Id equals payrollDetail.PayrollId
                       where payroll.IsPayrollLocked == true && payrollDetail.DepartmentId == departmentId
                       select payrollDetail).ToListAsync();
    }
}
