//// This File Needs to be reviewed Still. Don't Remove this comment.

//using ERP.Data.Entities.HR;
//using ERP.Repository.IRepository.HR;
//using ERP.Repository.UnitOfWork;
//using Microsoft.EntityFrameworkCore;

//namespace ERP.Repository.Repository.HR;

//public class DrugTestFrequencyDateRepository : Repository<DrugTestFrequencyDate>,
//	IDrugTestFrequencyDateRepository
//{
//	public DrugTestFrequencyDateRepository(DatabaseContext context) : base(context)
//	{

//	}

//	private DatabaseContext DatabaseContext => (DatabaseContext)Context;

//	public async Task<DateTime?> GetLatestDrugTestFrequencyDateByEmployeeIdAsync(long employeeId)
//	{
//		var drugTestDate = await (from drugTestFrequencyDate in DatabaseContext.DrugTestFrequencyDates
//			where drugTestFrequencyDate.EmployeeId == employeeId && drugTestFrequencyDate.TestDone == false
//			select drugTestFrequencyDate).FirstOrDefaultAsync();
//		return drugTestDate?.NextTestDate;
//	}
//}