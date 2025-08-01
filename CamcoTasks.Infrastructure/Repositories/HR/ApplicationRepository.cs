//// This File Needs to be reviewed Still. Don't Remove this comment.

//using ERP.Data.Entities.HR;
//using ERP.Repository.IRepository.HR;
//using Microsoft.EntityFrameworkCore;

//namespace ERP.Repository.Repository.HR;

//public class ApplicationRepository : Repository<Application>,
//	IApplicationRepository
//{
//	public ApplicationRepository(DatabaseContext context) : base(context)
//	{

//	}

//	private DatabaseContext DatabaseContext => (DatabaseContext)Context;

//	public async Task<short> GetApplicationByApplicationNameAsync(string applicationName)
//	{
//		return await (from application in DatabaseContext.Applications
//			where application.ApplicationName == applicationName
//			select application.Id).FirstOrDefaultAsync();
//	}
//}