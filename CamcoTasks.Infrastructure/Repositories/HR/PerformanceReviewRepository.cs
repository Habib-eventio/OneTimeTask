// This File Needs to be reviewed Still. Don't Remove this comment.

using CamcoTasks.Infrastructure;
using CamcoTasks.Infrastructure.Entities.HR;
using CamcoTasks.Infrastructure.IRepository.HR;
using Microsoft.EntityFrameworkCore;

namespace ERP.Repository.Repository.HR;

public class PerformanceReviewRepository : Repository<PerformanceReview>,
	IPerformanceReviewRepository
{
	public PerformanceReviewRepository(DatabaseContext context) : base(context)
	{

	}

	private DatabaseContext DatabaseContext => (DatabaseContext)Context;

	public async Task<PerformanceReview> GetPerformanceReviewByEmployeeIdAsync(long employeeId)
	{
		return await (from performanceReview in DatabaseContext.PerformanceReviews
			where performanceReview.EmployeeId == employeeId
			orderby performanceReview.Date descending
			select performanceReview).FirstOrDefaultAsync();
	}

	public async Task<PerformanceReview> GetFollowUpDateByIdAsync(long employeeId)
	{
		return await (from performanceReview in DatabaseContext.PerformanceReviews
			where performanceReview.EmployeeId == employeeId &&
			      performanceReview.IsDeleted == false
			orderby performanceReview.Date descending
			select performanceReview).FirstOrDefaultAsync();
	}
}