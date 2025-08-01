//// This File Needs to be reviewed Still. Don't Remove this comment.

//using CamcoTasks.Infrastructure;
//using ERP.Data.Entities.HR;
//using ERP.Repository.IRepository.HR;
//using Microsoft.EntityFrameworkCore;

//namespace ERP.Repository.Repository.HR;

//public class CertificationRepository : Repository<Certification>,
//	ICertificationRepository
//{
//	public CertificationRepository(DatabaseContext context) : base(context)
//	{

//	}

//	private DatabaseContext DatabaseContext => (DatabaseContext)Context;

//	public async Task<string> GetNameFromCertificationByCertificationIdAsync(long certificationId)
//	{
//		if (certificationId <= 0)
//		{
//			return "Not Found";
//		}

//		var result = await (from certification in DatabaseContext.Certifications
//			where certification.Id == certificationId
//			select certification.Name).FirstOrDefaultAsync();
//		return result ?? "Not Found";
//	}
//}