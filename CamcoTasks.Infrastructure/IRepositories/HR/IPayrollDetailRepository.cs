// This File Needs to be reviewed Still. Don't Remove this comment.

using CamcoTasks.Infrastructure.CustomModels.HR;
using CamcoTasks.Infrastructure.Entities.HR;

namespace CamcoTasks.Infrastructure.IRepository.HR;

public interface IPayrollDetailRepository : IRepository<PayrollDetail>
{
    /// <summary>
    /// This Method is being used in HumanResource Project
    /// </summary>
    Task<List<PayrollDetail>> GetLockedPayrollDetailsByEmployeeIdAsync(long employeeId);

    /// <summary>
    /// This Method is being used in AutomatedSystemService Project
    /// </summary>
    Task<List<PayrollSummaryViewModel>> Last4WeeksPayrollSummaryAsync();

    /// <summary>
    /// This Method is not being used anywhere.
    /// </summary>
    [Obsolete("This will be deleted By 1st August 2025. As this is not being used anywhere. If you see it's use please report to ERP Manager")]
	Task<bool> GetIsExistPayrollFromPayrollByEmployeeIdAsync(long employeeId);

	/// <summary>
	/// This Method is not being used anywhere.
	/// </summary>
	[Obsolete("This will be deleted By 1st August 2025. As this is not being used anywhere. If you see it's use please report to ERP Manager")]
	Task<List<PayrollDetail>> GetLockedPayrollDetailsByDepartmentIdAsync(long departmentId);
}