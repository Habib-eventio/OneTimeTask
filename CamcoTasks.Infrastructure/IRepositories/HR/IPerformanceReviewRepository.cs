// This File Needs to be reviewed Still. Don't Remove this comment.

using CamcoTasks.Infrastructure;
using CamcoTasks.Infrastructure.Entities.HR;

namespace CamcoTasks.Infrastructure.IRepository.HR;

public interface IPerformanceReviewRepository : IRepository<PerformanceReview>
{
	/// <summary>
	/// This Method is being used in HumanResource Project
	/// </summary>
	Task<PerformanceReview> GetPerformanceReviewByEmployeeIdAsync(long employeeId);

	/// <summary>
	/// This Method is being used in HumanResource Project
	/// </summary>
	Task<PerformanceReview> GetFollowUpDateByIdAsync(long employeeId);
}