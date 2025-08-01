//// This File Needs to be reviewed Still. Don't Remove this comment.

//using ERP.Data.Entities.HR;
//using ERP.Repository.IRepository.HR;
//using ERP.Repository.UnitOfWork;
//using Microsoft.EntityFrameworkCore;

//namespace ERP.Repository.Repository.HR;

//public class ApplicationMethodRepository : Repository<ApplicationMethod>,
//	IApplicationMethodRepository
//{
//	public ApplicationMethodRepository(DatabaseContext context) : base(context)
//	{

//	}

//	private DatabaseContext DatabaseContext => (DatabaseContext)Context;

//	public async Task<string> GetApplicationMethodFromMethodsByMethodIdAsync(long id)
//	{
//		return await (from method in DatabaseContext.ApplicationMethods
//			where method.Id == id
//			select method.MethodName).FirstOrDefaultAsync();
//	}
//}