// This File Needs to be reviewed Still. Don't Remove this comment.

using CamcoTasks.Infrastructure.Entities;

namespace CamcoTasks.Infrastructure.IRepositories.HR;

public interface IEmailTypeRepository : IRepository<EmailType>
{
    /// <summary>
    /// This Method is being used in Metrics, Stockroom Project
    /// </summary>
    Task<int> GetEmailTypeIdByEmailNameAsync(string name);
}