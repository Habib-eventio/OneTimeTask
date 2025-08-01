//// This File Needs to be reviewed Still. Don't Remove this comment.

//using ERP.Data.Entities.HR;
//using ERP.Repository.IRepository.HR;
//using ERP.Repository.UnitOfWork;
//using Microsoft.EntityFrameworkCore;

//namespace ERP.Repository.Repository.HR;

//public class JobDescriptionRepository : Repository<JobDescription>,
//	IJobDescriptionRepository
//{
//	public JobDescriptionRepository(DatabaseContext context) : base(context)
//	{

//	}

//	private DatabaseContext DatabaseContext => (DatabaseContext)Context;

//	public async Task<bool> GetIsExistFromJobDescriptionByJobDescriptionIdAsync(long jobDescriptionId)
//	{
//		var result = await (from jobDescription in DatabaseContext.JobDescriptions
//			where jobDescription.Id == jobDescriptionId
//			select jobDescription).FirstOrDefaultAsync();

//		if (result != null)
//			return true;

//		return false;
//	}

//	public async Task<string> GetNameFromJobDescriptionByJobDescriptionIdAsync(long jobDescriptionId)
//	{
//		if (jobDescriptionId <= 0)
//		{
//			return "NOT FOUND";
//		}

//		var jobDescriptionName = await (from jobDescription in DatabaseContext.JobDescriptions
//			where jobDescription.Id == jobDescriptionId
//			select jobDescription.Name).FirstOrDefaultAsync();
//		return jobDescriptionName ?? "NOT FOUND";
//	}

//	public async Task<IEnumerable<string>> GetNameListAsync(bool isDelete)
//	{
//		return await DatabaseContext.JobDescriptions
//			.Where(j => j.IsDeleted == isDelete)
//			.OrderBy(n => n.Name)
//			.Select(x => x.Name)
//			.ToListAsync();
//	}
//}