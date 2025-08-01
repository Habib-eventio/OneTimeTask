//// This File Needs to be reviewed Still. Don't Remove this comment.

//using ERP.Data.Entities.HR;
//using ERP.Repository.IRepository.HR;
//using ERP.Repository.UnitOfWork;
//using Microsoft.EntityFrameworkCore;

//namespace ERP.Repository.Repository.HR;

//public class NatureOfViolationRepository : Repository<NatureOfViolation>,
//	INatureOfViolationRepository
//{
//	public NatureOfViolationRepository(DatabaseContext context) : base(context)
//	{

//	}

//	private DatabaseContext DatabaseContext => (DatabaseContext)Context;

//	public async Task<string> GetNameFromNatureOfViolationByNatureOfViolationIdAsync(long natureOfViolationId)
//	{
//		if (natureOfViolationId <= 0)
//		{
//			return "Not Found";
//		}

//		return await (from natureOfViolation in DatabaseContext.NatureOfViolations
//			where natureOfViolation.Id == natureOfViolationId
//			select natureOfViolation.Name).FirstOrDefaultAsync();
//	}

//	public async Task<int> GetIdFromNatureOfViolationByNameAndTypeIdAsync(string name, short enumTypeId)
//	{
//		return await (from natureOfViolation in DatabaseContext.NatureOfViolations
//			where natureOfViolation.Name.ToUpper() == name.ToUpper() && natureOfViolation.EnumTypeId == enumTypeId
//			select natureOfViolation.Id).FirstOrDefaultAsync();
//	}
//}