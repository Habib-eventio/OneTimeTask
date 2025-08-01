//// This File Needs to be reviewed Still. Don't Remove this comment.

//using ERP.Data.Entities.HR;
//using ERP.Repository.IRepository.HR;
//using ERP.Repository.UnitOfWork;
//using Microsoft.EntityFrameworkCore;

//namespace ERP.Repository.Repository.HR;

//public class LogTypeRepository : Repository<LogType>,
//	ILogTypeRepository
//{
//	public LogTypeRepository(DatabaseContext context) : base(context)
//	{

//	}

//	private DatabaseContext DatabaseContext => (DatabaseContext)Context;

//	public async Task<int> GetIdFromLogTypeByTypeAndTypeIdAsync(string type, int enumTypeId)
//	{
//		return await (from logType in DatabaseContext.LogTypes
//			where logType.Type == type && logType.EnumTypeId == enumTypeId
//			select logType.Id).FirstOrDefaultAsync();
//	}
//}