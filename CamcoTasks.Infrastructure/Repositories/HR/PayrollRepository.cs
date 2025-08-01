//// This File Needs to be reviewed Still. Don't Remove this comment.

//using ERP.Data.Entities.HR;
//using ERP.Repository.IRepository.HR;
//using Microsoft.EntityFrameworkCore;
//using CamcoTasks.Infrastructure;

//namespace CamcoTasks.Infrastructure.Repository.HR;

//public class PayrollRepository : Repository<Payroll>,
//	IPayrollRepository
//{
//	public PayrollRepository(DatabaseContext context) : base(context)
//	{

//	}

//	private DatabaseContext DatabaseContext => (DatabaseContext)Context;

//	public async Task<List<Payroll>> GetPayrollsAsync()
//	{
//		return await (from payroll in DatabaseContext.Payrolls
//			orderby payroll.BatchNumber descending
//			select payroll).ToListAsync();
//	}

//	public async Task<DateTime> GetLatestPayrollDateByEmployeeIdAsync(long employeeId)
//	{
//		var payrollInformation = await (from payroll in DatabaseContext.Payrolls
//			where payroll.IsPayrollLocked == true
//			orderby payroll.PayrollStartDate descending
//			select payroll).FirstOrDefaultAsync();

//		return payrollInformation.PayrollStartDate;
//	}
//}