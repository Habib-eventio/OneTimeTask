//// This File Needs to be reviewed Still. Don't Remove this comment.

//using CamcoTasks.Infrastructure;
//using Microsoft.EntityFrameworkCore;

//namespace ERP.Repository.Repository.HR;

//public class TierLevelRepository : Repository<TierLevel>,
//	ITierLevelRepository
//{
//	public TierLevelRepository(DatabaseContext context) : base(context)
//	{

//	}

//	private DatabaseContext DatabaseContext => (DatabaseContext)Context;

//	public async Task<string> GetNameFromTierLevelByIdAsync(long tierLevelId)
//	{
//		if (tierLevelId <= 0)
//		{
//			return "NOT FOUND";
//		}

//		var tierLevel = await (from hrTierLevel in DatabaseContext.TierLevels
//			where hrTierLevel.IsDeleted == false &&
//			      hrTierLevel.Id == tierLevelId
//			select hrTierLevel.Name).FirstOrDefaultAsync();
//		return tierLevel ?? "NOT FOUND";
//	}
//}