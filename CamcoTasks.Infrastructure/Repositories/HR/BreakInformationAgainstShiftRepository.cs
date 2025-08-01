//// This File Needs to be reviewed Still. Don't Remove this comment.

//using CamcoTasks.Infrastructure;
//using ERP.Data.Entities.HR;
//using ERP.Repository.IRepository.HR;
//using ERP.Repository.UnitOfWork;
//using Microsoft.EntityFrameworkCore;

//namespace ERP.Repository.Repository.HR;

//public class BreakInformationAgainstShiftRepository : Repository<BreakInformationAgainstShift>,
//	IBreakInformationAgainstShiftRepository
//{
//	public BreakInformationAgainstShiftRepository(DatabaseContext context) : base(context)
//	{

//	}

//	private DatabaseContext DatabaseContext => (DatabaseContext)Context;

//	public async Task<bool> GetIsExistFromBreakInformationAgainstShiftByBreakInformationAgainstShiftIdAsync(
//		long breakInformationAgainstShiftId)
//	{
//		return await (from breakInformationAgainstShift in DatabaseContext.BreakInformationAgainstShifts
//			where breakInformationAgainstShift.Id == breakInformationAgainstShiftId
//			select breakInformationAgainstShift).AnyAsync();
//	}
//}