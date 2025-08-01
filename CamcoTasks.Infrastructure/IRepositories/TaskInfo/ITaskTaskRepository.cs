// This File Needs to be reviewed Still. Don't Remove this comment.

using CamcoTasks.Infrastructure;
using CamcoTasks.Infrastructure.Entities.TaskInfo;


namespace CamcoTasks.Infrastructure.IRepositories;

public interface ITaskTaskRepository : IRepository<TaskTask>
{
    /// <summary>
    /// Caution: This Method is being used in Task Project
    /// </summary>
    Task<List<IGrouping<string, TaskTask>>> GetActiveTasks();

    /// <summary>
    /// Caution: This Method is being used in Task Project
    /// </summary>
    Task<int> AddTask(TaskTask task);

    /// <summary>
    /// Caution: This Method is being used in Task Project
    /// </summary>
    Task<bool> UpdateTask(TaskTask task, int? previousPriority);

    /// <summary>
    /// Caution: This Method is being used in Task Project
    /// </summary>
    Task<bool> UpdateOneTask(TaskTask task);

    /// <summary>
    /// Caution: This Method is being used in Task Project
    /// </summary>
    Task<bool> RemoveOneTask(TaskTask task);

    /// <summary>
    /// Caution: This Method is being used in Task Project
    /// </summary>
    Task<bool> RemoveTask(TaskTask task);
}