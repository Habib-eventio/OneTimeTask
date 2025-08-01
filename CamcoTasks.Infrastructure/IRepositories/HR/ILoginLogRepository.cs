// This File Needs to be reviewed Still. Don't Remove this comment.

using CamcoTasks.Infrastructure.Entities.HR;

namespace CamcoTasks.Infrastructure.IRepository.HR;

public interface ILoginLogRepository : IRepository<LoginLog>
{
    /// <summary>
    /// This Method is being used in Metrics, Login, Stockroom, HumanResource, Costing Project
    /// </summary>
    Task<LoginLog> GetLatestLoginLogByEmployeeIdAsync(long employeeId, short applicationId);
}