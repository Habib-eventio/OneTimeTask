//// This File Needs to be reviewed Still. Don't Remove this comment.

//using ERP.Data.Entities.HR;
//using ERP.Repository.IRepository.HR;
//using ERP.Repository.UnitOfWork;
//using Microsoft.EntityFrameworkCore;

//namespace ERP.Repository.Repository.HR;

//public class AdvertisementTypeRepository : Repository<AdvertisementType>,
//	IAdvertisementTypeRepository
//{
//	public AdvertisementTypeRepository(DatabaseContext context) : base(context)
//	{

//	}

//	private DatabaseContext DatabaseContext => (DatabaseContext)Context;

//	public async Task<bool> GetIsCustomFromAdvertisementTypeByAdvertisementTypeIdAsync(long advertisementTypeId)
//	{
//		return await (from advertisementType in DatabaseContext.AdvertisementTypes
//			where advertisementType.Id == advertisementTypeId
//			select advertisementType.IsCustom).FirstOrDefaultAsync();
//	}
//}