// This File Needs to be reviewed Still. Don't Remove this comment.

using CamcoTasks.Infrastructure;
using CamcoTasks.Infrastructure.Entities.TaskInfo;


namespace CamcoTasks.Infrastructure.IRepositories.TaskInfo;

public interface ITaskUpdateRepository : IRepository<TaskUpdate>
{
    /// <summary>
    /// Caution: This Method is being used in Task Project
    /// </summary>
    Task<List<TaskUpdate>> GetLatestUpdates();

    /// <summary>
    /// This Method is not being used anywhere.
    /// </summary>
    [Obsolete("This will be deleted By 1st August 2025. As this is not being used anywhere. If you see it's use please report to ERP Manager")]
	Task<IEnumerable<TaskUpdate>> GetTaskUpdatesAsync(string ignoreSystemGenerated,
        string ignoreNudgedUpdate, string ignoreEmailUpdate, bool hasRecurringTaskValue, bool hasDueDateValue,
        int dueDateDuration, bool isDelete);

	/// <summary>
	/// This Method is not being used anywhere.
	/// </summary>
	[Obsolete("This will be deleted By 1st August 2025. As this is not being used anywhere. If you see it's use please report to ERP Manager")]
	Task<IEnumerable<TaskUpdate>> GetTaskUpdatesForPercentageAsync(string ignoreSystemGenerated,
        string ignoreNudgedUpdate, string ignoreEmailUpdate, bool hasRecurringTaskValue, bool hasDueDateValue,
        int dueDateDuration, bool isDelete);

	/// <summary>
	/// This Method is not being used anywhere.
	/// </summary>
	[Obsolete("This will be deleted By 1st August 2025. As this is not being used anywhere. If you see it's use please report to ERP Manager")]
	Task<List<TaskUpdate>> GetTaskUpdatesAsync(int recurringId, string ignoreNudgedUpdate,
        string ignoreEmailUpdate, bool isDeleted, bool hasDueDateValue, int dueDateDuration, bool isTaskCompleted);

	/// <summary>
	/// This Method is not being used anywhere.
	/// </summary>
	[Obsolete("This will be deleted By 1st August 2025. As this is not being used anywhere. If you see it's use please report to ERP Manager")]
	Task<List<TaskUpdate>> GetTaskUpdatesAsync(int recurringId, bool isDeleted);

    /// <summary>
    /// Caution: This Method is being used in Task, AutomatedSystemService Project
    /// </summary>
    Task<TaskUpdate> GetRecurringTaskLatestUpdateAsync(int recurringTaskId);

    /// <summary>
    /// Caution: This Method is being used in Task Project
    /// </summary>
    Task<int> AddTaskUpdate(TaskUpdate update);

    /// <summary>
    /// Caution: This Method is being used in Task Project
    /// </summary>
    int AddTaskUpdateSync(TaskUpdate update);

    /// <summary>
    /// Caution: This Method is being used in Task Project
    /// </summary>
    Task<bool> UpdateTaskUpdate(TaskUpdate update);

    /// <summary>
    /// Caution: This Method is being used in Task Project
    /// </summary>
    Task<bool> UpdateTaskUpdateSync(TaskUpdate update);

    /// <summary>
    /// Caution: This Method is being used in Task Project
    /// </summary>
    Task<bool> RemoveTaskUpdate(TaskUpdate update);
}