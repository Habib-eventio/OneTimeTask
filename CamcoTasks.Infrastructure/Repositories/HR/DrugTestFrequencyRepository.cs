//// This File Needs to be reviewed Still. Don't Remove this comment.

//using ERP.Data.Entities.HR;
//using ERP.Repository.IRepository.HR;
//using ERP.Repository.UnitOfWork;
//using Microsoft.EntityFrameworkCore;

//namespace ERP.Repository.Repository.HR;

//public class DrugTestFrequencyRepository : Repository<DrugTestFrequency>,
//	IDrugTestFrequencyRepository
//{
//	public DrugTestFrequencyRepository(DatabaseContext context) : base(context)
//	{

//	}

//	private DatabaseContext DatabaseContext => (DatabaseContext)Context;

//	public async Task<DrugTestFrequency> GetLatestDrugTestFrequencyByEmployeeIdAsync(long employeeId)
//	{
//		return await (from drugTestFrequency in DatabaseContext.DrugTestFrequencies
//			where drugTestFrequency.EmployeeId == employeeId
//			orderby drugTestFrequency.DateCreated descending
//			select drugTestFrequency).FirstOrDefaultAsync();
//	}
//}