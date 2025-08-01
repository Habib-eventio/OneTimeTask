//// This File Needs to be reviewed Still. Don't Remove this comment.

//using CamcoTasks.Infrastructure;
//using CamcoTasks.Infrastructure.Entities.HR;
//using CamcoTasks.Infrastructure.IRepository.HR;
//using Microsoft.EntityFrameworkCore;

//namespace ERP.Repository.Repository.HR;

//public class ApplicationTrackingHistoryRepository : Repository<ApplicationTrackingHistory>,
//	IApplicationTrackingHistoryRepository
//{
//	public ApplicationTrackingHistoryRepository(DatabaseContext context) : base(context)
//	{

//	}

//	private DatabaseContext DatabaseContext => (DatabaseContext)Context;

//	public async Task<ApplicationTrackingHistory> GetApplicantFromApplicationTrackingHistoryByIdAsync(
//		long applicantId)
//	{
//		return await (from applicant in DatabaseContext.ApplicationTrackingHistories
//			where applicant.ApplicantId == applicantId
//			select applicant).OrderByDescending(e => e.DateCreated).FirstOrDefaultAsync();
//	}
//}