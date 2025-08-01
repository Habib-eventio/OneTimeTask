// This File Needs to be reviewed Still. Don't Remove this comment.

using CamcoTasks.Infrastructure;
using CamcoTasks.Infrastructure.Entities;
using CamcoTasks.Infrastructure.Entities.CAMCO;

namespace CamcoTasks.Infrastructure.IRepository.CAMCO;

public interface IProjectRepository : IRepository<Project>
{
    /// <summary>
    /// This Method is being used in Task Project
    /// </summary>
    Task<Project> GetLastProjectEntityAsync();

	/// <summary>
	/// This Method is being used in Task Project
	/// </summary>
	Task<List<ProjectCosting>> GetProjectCostsListAsync();
    IQueryable<Project> GetAllAsQueryable();

}