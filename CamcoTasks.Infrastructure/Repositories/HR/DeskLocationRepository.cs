//// This File Needs to be reviewed Still. Don't Remove this comment.

//using CamcoTasks.Infrastructure;
//using Microsoft.EntityFrameworkCore;

//namespace ERP.Repository.Repository.HR;

//public class DeskLocationRepository : Repository<DeskLocation>,
//{
//	public DeskLocationRepository(DatabaseContext context) : base(context)
//	{

//	}

//	private DatabaseContext DatabaseContext => (DatabaseContext)Context;

//	public async Task<bool> GetIsExistFromDeskLocationByDeskLocationIdAsync(long deskLocationId)
//	{
//		return await (from deskLocation in DatabaseContext.DeskLocations
//			where deskLocation.Id == deskLocationId
//			select deskLocation).AnyAsync();
//	}

//	public async Task<string> GetNameFromDeskLocationByDeskLocationIdAsync(long deskLocationId)
//	{
//		return await (from deskLocation in DatabaseContext.DeskLocations
//			where deskLocation.Id == deskLocationId
//			select deskLocation.DeskLocationCode).FirstOrDefaultAsync();
//	}
//}