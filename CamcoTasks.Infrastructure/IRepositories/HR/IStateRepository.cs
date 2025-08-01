// This File Needs to be reviewed Still. Don't Remove this comment.

using CamcoTasks.Infrastructure;
using CamcoTasks.Infrastructure.Entities;

namespace CamcoTasks.Infrastructure.IRepositories;

public interface IStateRepository : IRepository<State>
{
	/// <summary>
	/// This Method is not being used anywhere.
	/// </summary>
	[Obsolete("This will be deleted By 1st August 2025. As this is not being used anywhere. If you see it's use please report to ERP Manager")]
	Task<string> GetNameFromStateByIdAsync(long stateId);
}