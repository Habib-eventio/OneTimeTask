//// This File Needs to be reviewed Still. Don't Remove this comment.

//using ERP.Data.Entities.Production;
//using ERP.Repository.UnitOfWork;

//namespace ERP.Repository.IRepository.Production;

//public interface IPartMasterRepository : IRepository<PartMaster>
//{
//    /// <summary>
//    /// Caution: This Method is being used in Production, Costing Project
//    /// </summary>
//    Task<List<PartMaster>> GetAllPartsAsync();

//    /// <summary>
//    /// Caution: This Method is being used in Production Project
//    /// </summary>
//    Task<string> GetPartNumberPartByPartId(long? partId);

//    /// <summary>
//    /// Caution: This Method is being used in Production Project
//    /// </summary>
//    Task<string> GetCustomerNameByPartId(string partId);

//    /// <summary>
//    /// Caution: This Method is being used in Production, ToolCrib Project
//    /// </summary>
//    Task<List<CX4211EShopOrderNumber>> GetAllShopOrdersAsync();

//    /// <summary>
//    /// Caution: This Method is being used in Production Project
//    /// </summary>
//    Task<string> GetShopOrderByOrderId(long? orderId);

//	/// <summary>
//	/// Caution: This Method is being used in Production, Setup Project
//	/// This method is used to retrieve the engineering drawing path on the server.
//	/// </summary>
//	Task<string> GetEngineeringDrawingPathByPartNumberAsync(string partNumber);
//}