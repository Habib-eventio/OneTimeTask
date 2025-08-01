//// This File Needs to be reviewed Still. Don't Remove this comment.

//using ERP.Data.Entities.HR;
//using ERP.Repository.IRepository.HR;
//using ERP.Repository.UnitOfWork;
//using Microsoft.EntityFrameworkCore;

//namespace ERP.Repository.Repository.HR;

//public class ShirtSizeRepository : Repository<ShirtSize>,
//	IShirtSizeRepository
//{
//	public ShirtSizeRepository(DatabaseContext context) : base(context)
//	{

//	}

//	private DatabaseContext DatabaseContext => (DatabaseContext)Context;

//	public async Task<bool> GetIsExistFromShirtSizeByShirtSizeIdAsync(long shirtSizeId)
//	{
//		return await DatabaseContext.ShirtSizes.AnyAsync(shirtSize => shirtSize.Id == shirtSizeId);
//	}

//	public async Task<string> GetNameFromShirtSizeByShirtSizeIdAsync(long shirtSizeId)
//	{
//		return (await DatabaseContext.ShirtSizes.FirstOrDefaultAsync(shirtSize => shirtSize.Id == shirtSizeId))
//			.ShirtSizeDetails;
//	}
//}