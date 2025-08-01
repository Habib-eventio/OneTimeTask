//// This File Needs to be reviewed Still. Don't Remove this comment.

//using ERP.Data.Entities.HR;
//using ERP.Repository.IRepository.HR;
//using ERP.Repository.UnitOfWork;

//namespace ERP.Repository.Repository.HR;

//public class EmploymentStatusRepository : Repository<EmploymentStatus>,
//	IEmploymentStatusRepository
//{
//	public EmploymentStatusRepository(DatabaseContext context) : base(context)
//	{

//	}

//	private DatabaseContext DatabaseContext => (DatabaseContext)Context;

//	public string GetStatusFromEmploymentStatusByEmploymentStatusId(short employmentStatusId)
//	{
//		if (employmentStatusId <= 0)
//		{
//			return "NOT FOUND";
//		}

//		var result = (from employmentStatus in DatabaseContext.EmploymentStatuses
//			where employmentStatus.Id == employmentStatusId
//			select employmentStatus).FirstOrDefault();

//		if (result != null)
//		{
//			return result.Status.ToUpper();
//		}

//		return "NOT FOUND";
//	}
//}