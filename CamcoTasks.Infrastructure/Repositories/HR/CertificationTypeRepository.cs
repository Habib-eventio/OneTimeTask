//// This File Needs to be reviewed Still. Don't Remove this comment.

//using ERP.Data.Entities.HR;
//using ERP.Repository.IRepository.HR;
//using ERP.Repository.UnitOfWork;
//using Microsoft.EntityFrameworkCore;

//namespace ERP.Repository.Repository.HR;

//public class CertificationTypeRepository : Repository<CertificationType>,
//	ICertificationTypeRepository
//{
//	public CertificationTypeRepository(DatabaseContext context) : base(context)
//	{

//	}

//	private DatabaseContext DatabaseContext => (DatabaseContext)Context;

//	public async Task<string> GetTypeFromCertificationTypeByCertificationTypeIdAsync(int certificationTypeId)
//	{
//		return await (from certificationType in DatabaseContext.CertificationTypes
//			where certificationType.IsDeleted == false && certificationType.Id == certificationTypeId
//			select certificationType.Type).FirstOrDefaultAsync();
//	}
//}