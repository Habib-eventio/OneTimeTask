//// This File Needs to be reviewed Still. Don't Remove this comment.

//using CamcoTasks.Infrastructure;
//using ERP.Data.Entities.HR;
//using ERP.Repository.IRepository.HR;
//using Microsoft.EntityFrameworkCore;

//namespace ERP.Repository.Repository.HR;

//public class UserCertificationRepository : Repository<UserCertification>,
//	IUserCertificationRepository
//{
//	public UserCertificationRepository(DatabaseContext context) : base(context)
//	{

//	}

//	private DatabaseContext DatabaseContext => (DatabaseContext)Context;

//	public async Task<List<UserCertification>> GetCertificationByEmployeeIsActiveStateAsync(bool isActive)
//	{
//		return await (from userCertification in DatabaseContext.UserCertifications
//			join employee in DatabaseContext.Employees on userCertification.EmployeeId equals employee.Id
//			where (employee.IsActive == true || employee.IsActive == isActive) &&
//			      userCertification.IsDeleted == false && userCertification.IsMainCertification
//			select userCertification).OrderByDescending(a => a.DateOfCertification).ToListAsync();
//	}
//}