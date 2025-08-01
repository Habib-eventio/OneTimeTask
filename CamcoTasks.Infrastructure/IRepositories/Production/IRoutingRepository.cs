// This File Needs to be reviewed Still. Don't Remove this comment.

using CamcoTasks.Infrastructure;
using CamcoTasks.Infrastructure.Entities.Production;

namespace ERP.Repository.IRepository.Production;

public interface IRoutingRepository : IRepository<Routing>
{
    /// <summary>
    /// Caution: This Method is being used in AutomatedSystemService Project
    /// </summary>
    string GetRoutingDescriptionByOperationAndPartNumber(string operation, string partNumber);

	/// <summary>
	/// Caution: This Method is being used in AutomatedSystemService Project
	/// </summary>
	Routing GetRoutingByRoutingId(int routingId, DateTime dateUpdated);

	/// <summary>
	/// Caution: This Method is being used in Production Project
	/// </summary>
	Task<List<string>> GetAllOperationNumbersAsync();

	/// <summary>
	/// Caution: This Method is being used in Production Project
	/// </summary>
	Task<List<string>> GetOperationNumbersByPartNumberAsync(string partNo);

    /// <summary>
    /// This Method is being used in Quality, Setup Project
    /// </summary>
    /// <param name="partNumber"></param>
    /// <returns></returns>
    //Task<List<OperationNumberViewModel>> GetOperationNumberByPartNumber(string partNumber);

    /// <summary>
    /// This Method is being used in the Quality Project
    /// </summary>
    Task<List<Routing>> GetAllRoutingDataAsync();
}