// This File Needs to be reviewed Still. Don't Remove this comment.

using CamcoTasks.Infrastructure;
using CamcoTasks.Infrastructure.Entities;
using CamcoTasks.Infrastructure.Entities.Task;
using CamcoTasks.Infrastructure.Entities.TaskInfo;


namespace CamcoTasks.Infrastructure.IRepositories;

public interface IRecurringTaskRepository : IRepository<RecurringTask>
{
    /// <summary>
    /// Caution: This Method is being used in Task Project
    /// </summary>
    Task<int> GetMaxRecurringId();

    /// <summary>
    /// Caution: This Method is being used in Task, 5S Project
    /// </summary>
    Task<List<RecurringTask>> GetSubTasks(int taskId);

    /// <summary>
    /// Caution: This Method is being used in AutomatedSystemService Project
    /// </summary>
    Task<List<RecurringTask>> GetOverdueRecurringTasksAsync();

    /// <summary>
    /// Caution: This Method is being used in Task, AutomatedSystemService Project
    /// </summary>
    Task<List<RecurringTask>> GetRecurringTasks();

    /// <summary>
    /// Caution: This Method is being used in Task Project
    /// </summary>
    Task<List<RecurringTask>> GetRecurringTasksAsync(DateTime fromDateCompleted, DateTime toDateCompleted,
        bool isDelete, bool isDeactivated);

    /// <summary>
    /// Caution: This Method is being used in Task Project
    /// </summary>
    Task<int> GetRecurringTasksCountAsync();

    /// <summary>
    /// Caution: This Method is being used in Task Project
    /// </summary>
    Task<int> GetRecurringTasksCountAsync(bool isDeleted, bool isDeActivate);

	/// <summary>
	/// Caution: This Method is being used in Task, AutomatedSystemService Project
	/// </summary>
	Task<List<RecurringTask>> GetRecurringTasks(Func<RecurringTask, bool> p);

    /// <summary>
    /// Caution: This Method is being used in Task, AutomatedSystemService Project
    /// </summary>
    List<RecurringTask> GetRecurringTasks(int skip, int take, Func<RecurringTask, bool> p);

    /// <summary>
    /// Caution: This Method is being used in Task Project
    /// </summary>
    List<RecurringTask> GetRecurringTasksSync(Func<RecurringTask, bool> p);

    /// <summary>
    /// Caution: This Method is being used in Task Project
    /// </summary>
    Task<int> GetRecurringTaskCount(Func<RecurringTask, bool> p);

    /// <summary>
    /// Caution: This Method is being used in Task Project
    /// </summary>
    Task<RecurringTask> GetRecurringTaskById(int id);

    /// <summary>
    /// Caution: This Method is being used in Task Project
    /// </summary>
    RecurringTask GetRecurringTaskByIdSync(int id);

    /// <summary>
    /// Caution: This Method is being used in Task Project
    /// </summary>
    Task<List<RecurringTask>> GetRecurringTasksByIsDeletedAndDeactivatedAndParentTaskIdAsync(bool isDeleted,
        bool isDeactivated, int? parentTaskId);

    /// <summary>
    /// Caution: This Method is being used in Task Project
    /// </summary>
    Task<List<RecurringTask>> GetRecurringTasksByIsDeletedAndDeactivatedAndParentTaskIdOrderByIdAsync(
        bool isDeleted, bool isDeactivated, int? parentTaskId);

    /// <summary>
    /// Caution: This Method is being used in Task Project
    /// </summary>
    Task<List<RecurringTask>> GetRecurringTasksByIsDeletedAndDeactivatedAndSearchValueAsync(bool isDeleted,
        bool isDeactivated, string searchPattern);

    /// <summary>
    /// Caution: This Method is being used in Task Project
    /// </summary>
    Task<List<RecurringTask>> GetRecurringTasksByIsDeletedAndDeactivatedAndParentTaskIdAndSkipAndTakeAsync(
        bool isDeleted, bool isDeactivated, int? parentTaskId, int skip, int take);

    /// <summary>
    /// Caution: This Method is being used in Task Project
    /// </summary>
    Task<int> CountRecurringTasks(bool isDeleted, bool isDeactivated, int? parentTaskId);

    /// <summary>
    /// Caution: This Method is being used in Task Project
    /// </summary>
    Task<List<RecurringTask>> GetRecurringTasksByIsDeletedAndDeactivatedAndParentTaskIdAndPersonResponsibleAsync(
        bool isDeleted, bool isDeactivated, int? parentTaskId, string responsiblePerson);

    /// <summary>
    /// Caution: This Method is being used in Task Project
    /// </summary>
    Task<List<RecurringTask>> GetRecurringTasksByIsDeletedAndDeactivatedAndParentTaskIdAndTaskAreaAsync(
        bool isDeleted, bool isDeactivated, bool isApproved, int? parentTaskId,
        string taskArea);

    /// <summary>
    /// Caution: This Method is being used in Task Project
    /// </summary>
    List<RecurringTask> GetRecurringTasksSync();

    /// <summary>
    /// Caution: This Method is being used in Task Project
    /// </summary>
    Task<bool> ClearTaskType(string taskType);

    /// <summary>
    /// Caution: This Method is being used in Task Project
    /// </summary>
    Task<List<UpdateReportViewModel>> GetUpdateReport(string auditPerson, DateTime reportingDate);

    /// <summary>
    /// Caution: This Method is being used in Task Project
    /// </summary>
    Task<List<UpdateReportViewModel>> GetUpdateReport(int recurringTaskId);

    /// <summary>
    /// Caution: This Method is being used in Task Project
    /// </summary>
    Task<List<UpdateReportViewModel>> GetUpdateReport(string auditPerson, DateTime reportingFromDate,
        DateTime reportingToDate);
}