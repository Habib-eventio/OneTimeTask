//// This File Needs to be reviewed Still. Don't Remove this comment.

//using CamcoTasks.Infrastructure;
//using ERP.Data.Entities.HR;
//using ERP.Repository.IRepository.HR;
//using Microsoft.EntityFrameworkCore;

//namespace ERP.Repository.Repository.HR;

//public class TestRepository : Repository<Test>,
//	ITestRepository
//{
//	public TestRepository(DatabaseContext context) : base(context)
//	{

//	}

//	private DatabaseContext DatabaseContext => (DatabaseContext)Context;

//	public async Task<bool> GetIsExistFromTestByTestIdAsync(long testId)
//	{
//		return await (from test in DatabaseContext.Tests
//			where test.Id == testId
//			select test).AnyAsync();
//	}
//}