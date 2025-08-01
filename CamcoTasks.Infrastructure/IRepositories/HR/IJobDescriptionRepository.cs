// This File Needs to be reviewed Still. Don't Remove this comment.

using CamcoTasks.Infrastructure.Entities.HR;

namespace CamcoTasks.Infrastructure.IRepository.HR;

public interface IJobDescriptionRepository : IRepository<JobDescription>
{
	/// <summary>
	/// This Method is being used in HumanResource Project
	/// </summary>
	Task<bool> GetIsExistFromJobDescriptionByJobDescriptionIdAsync(long jobDescriptionId);

    /// <summary>
    /// This Method is being used in HumanResource, Task Project
    /// </summary>
    Task<string> GetNameFromJobDescriptionByJobDescriptionIdAsync(long jobId);

    /// <summary>
    /// This Method is being used in Task Project
    /// </summary>
    Task<IEnumerable<string>> GetNameListAsync(bool isDelete);
}