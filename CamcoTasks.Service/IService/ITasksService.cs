using CamcoTasks.ViewModels.EmailQueue;
using CamcoTasks.ViewModels.TasksFrequencyListDTO;
using CamcoTasks.ViewModels.TasksImagesDTO;
using CamcoTasks.ViewModels.TasksRecTasksDTO;
using CamcoTasks.ViewModels.TasksTasksDTO;
using CamcoTasks.ViewModels.TasksTasksTaskTypeDTO;
using CamcoTasks.ViewModels.TasksTaskUpdatesDTO;
using CamcoTasks.Infrastructure.Entities.TaskInfo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using CamcoTasks.Infrastructure.Entities.Task;

namespace CamcoTasks.Service.IService
{
    public interface ITasksService
    {
        Task<int> GetMaxRecurringId();
        Task<IEnumerable<TasksImagesViewModel>> GetRecurringTaskImagesCountAsync(int TaskId);
        Task<IEnumerable<TasksImagesViewModel>> GetOneTimeTaskImagesCountAsync(int TaskId);
        Task<IEnumerable<TasksImagesViewModel>> GetUpdatesImagesCountAsync(int UpdateId);
        Task<IEnumerable<TasksImagesViewModel>> GetRecurringTaskImagesAsync(int TaskId);
        Task<IEnumerable<TasksImagesViewModel>> GetRecurringTaskImages(int TaskId);
        Task<IEnumerable<TasksImagesViewModel>> GetOneTimeTaskImagesAsync(int TaskId);
        Task<TasksImagesViewModel> GetUpdatesImages(int UpdateId);
        Task<IEnumerable<TasksImagesViewModel>> GetUpdatesImagesAsync(int UpdateId);
        Task<int> InsertTaskImageAsync(TasksImagesViewModel TaksImage);
        Task<TasksImagesViewModel> UpdateTaskImageAsync(TasksImagesViewModel TaksImage);
        Task<TasksImagesViewModel> GetUpdateTaskImageAsync(int TaksImageId);
        Task<TasksImagesViewModel> DeleteTaskImageAsync(TasksImagesViewModel TaksImage);
        Task<IEnumerable<TasksTasksViewModel>> GetAllTasks();
        Task<IEnumerable<TasksTasksViewModel>> GetAllTasks1();
        Task<IEnumerable<TasksTasksViewModel>> GetAllTasks2();
        Task<IEnumerable<TasksTasksViewModel>> GetAllTasks3(string taskId);

        Task<IEnumerable<TasksTasksViewModel>> GetTasksByPerson(string personResponsible);

        Task<IEnumerable<TasksTasksViewModel>> GetAllTasks(string OldTypeValue);

        Task<IEnumerable<TasksTasksViewModel>> GetAllTasksSync();
        Task<IEnumerable<TasksTasksViewModel>> GetAllTasksSync1();

        Task<TasksTasksViewModel> GetTaskById(int Id);
        Task<IEnumerable<TasksTasksViewModel>> GetTasks();
        Task<int> GetOneTimeTasksCountAsync();
        Task<IEnumerable<TasksTasksViewModel>> GetTasksSync();
        Task<IEnumerable<TasksRecTasksViewModel>> GetSubTasks(int TaskId);
        Task<IEnumerable<TasksTaskUpdatesViewModel>> GetTaskUpdatesAsync(
            string ignoreSystemGeneret, string ignoreNudgudUpdate, string ignoreEmailUpdate,
            bool hasRecurringTaskValue, bool hasDueDateValue, int DueDateDuration, bool isDelete);
        Task<IEnumerable<TasksTaskUpdatesViewModel>> GetTaskUpdatesForPercentageAsync(
           string ignoreSystemGeneret, string ignoreNudgudUpdate, string ignoreEmailUpdate,
           bool hasRecurringTaskValue, bool hasDueDateValue, int DueDateDuration, bool isDelete);
        Task<IEnumerable<TasksTaskUpdatesViewModel>> GetTaskUpdatesAsync(int taskId,
            string ignoreSystemGeneret, string ignoreNudgudUpdate, string ignoreEmailUpdate,
            DateTime fromUpdateDate, DateTime toUpdateDate, bool isDelete);
        Task<IEnumerable<TasksTaskUpdatesViewModel>> GetTaskUpdatesAsync(int recurringId,
            string ignoreNudgudUpdate, string ignoreEmailUpdate, bool isDeleted, bool hasDueDateValue,
            int DueDateDuration, bool isTaskCompleted);
        Task<IEnumerable<TasksTaskUpdatesViewModel>> GetTaskUpdates(int taskId);
        Task<IEnumerable<TasksTaskUpdatesViewModel>> GetTaskUpdates1(int taskId);

        Task<TasksTaskUpdatesViewModel> GetRecurringTaskLatestUpdateAsync(int recurringTaskId);
        Task<IEnumerable<TasksTaskUpdatesViewModel>> GetTaskUpdates(int recurringId, string ignoreNudgudUpdate,
            string ignoreEmailUpdate, bool isDeleted);
        Task<IEnumerable<TasksTaskUpdatesViewModel>> GetTaskUpdates(int recurringId, DateTime updateDate);
        Task<IEnumerable<TasksTaskUpdatesViewModel>> GetTaskUpdates(int recurringId, bool isDeleted);
        Task<int> GetTaskUpdatesCountAsync(int recurringId, bool isDeleted);
        Task<IEnumerable<TasksTaskUpdatesViewModel>> GetTaskUpdatesSync();
        Task<TasksTaskUpdatesViewModel> GetTaskUpdatesByIdAsync(int Id);
        Task<TasksTaskUpdatesViewModel> GetTaskUpdatesById(int Id);
        Task<IEnumerable<TasksImagesViewModel>> GetOneTimeTaksUpdateFilesAsync(int taskId);
        Task<IEnumerable<TasksImagesViewModel>> GetOneTimeTaksUpdateFilesAsync1(int taskId);

        Task<IEnumerable<TasksTaskUpdatesViewModel>> GetTaskUpdatesWithAllFieldSync(int recurringId, bool isDeleted);
        Task<IEnumerable<TasksTaskUpdatesViewModel>> GetTaskUpdatesWithAllFieldSync(int taskId);
        Task<IEnumerable<TasksTaskUpdatesViewModel>> GetTaskUpdates(List<int> Ids);
        Task<IEnumerable<TasksTaskUpdatesViewModel>> GetTaskUpdates();
        Task<IEnumerable<TasksRecTasksViewModel>> GetRecurringTasks();
        Task<IEnumerable<TasksRecTasksViewModel>> GetRecurringTasksAsync(DateTime fromDateCompleted,
            DateTime toDateCompleted, bool isDelete, bool isDeactivated);
        Task<IEnumerable<TasksRecTasksViewModel>> GetRecurringTasks(Func<RecurringTask, bool> p);
        IEnumerable<TasksRecTasksViewModel> GetRecurringTasks(int skip, int take, Func<RecurringTask, bool> p);
        IEnumerable<TasksRecTasksViewModel> GetRecurringTasksSync(Func<RecurringTask, bool> p);
        Task<int> GetRecurringTaskCount(Func<RecurringTask, bool> p);
        Task<int> GetRecurringTaskCountSync(Func<RecurringTask, bool> p);
        Task<TasksRecTasksViewModel> GetRecurringTaskById(int Id);
        TasksRecTasksViewModel GetRecurringTaskByIdSync(int Id);
        Task<IEnumerable<TasksRecTasksViewModel>> GetRecurringTasks(bool isDeleted, bool isDeactivated, int? parentTaskId);
        Task<IEnumerable<TasksRecTasksViewModel>> GetRecurringTasksForPercentageAsync(bool isDeleted, bool isDeactivated,
            int? parentTaskId);
        Task<IEnumerable<TasksRecTasksViewModel>> GetRecurringTasksBySearchAsync(
            bool isDeleted, bool isDeactivated, string searchParttern);
        Task<IEnumerable<TasksRecTasksViewModel>> GetRecurringTasks(bool isDeleted, bool isDeactivated,
            int? parentTaskId, int skip, int take);
        Task<int> CountRecurringTasks(bool isDeleted, bool isDeactivated, int? parentTaskId);
        Task<int> CountRecurringTasks(bool isDeleted, bool isDeactivated, DateTime toDate, string auditPerson);
        Task<IEnumerable<TasksRecTasksViewModel>> GetRecurringTasksSync(bool isDeleted, bool isDeactivated, int? parentTaskId);
        Task<IEnumerable<TasksRecTasksViewModel>> GetRecurringTasks(bool isDeleted, bool isDeactivated, int? parentTaskId, string PersonResponsible);
        Task<IEnumerable<TasksRecTasksViewModel>> GetRecurringTasksByTaskAreaAsync(bool isDeleted, bool isDeactivated, bool isApproved,
            int? parentTaskId, string taskArea);
        Task<int> GetRecurringTasksCountAsync();
        Task<int> GetRecurringTasksCountAsync(bool isDeleted, bool isDactivate);
        IEnumerable<TasksRecTasksViewModel> GetRecurringTasksSync();
        Task<TasksTasksTaskTypeViewModel> GetTaskType(string TaskType);
        Task<IEnumerable<TasksTasksTaskTypeViewModel>> GetTaskTypes();
        Task<IEnumerable<TasksTasksTaskTypeViewModel>> GetTaskTypesSync();
        Task<IEnumerable<TasksTasksViewModel>> GetEmptyTaskTypes();
        Task<bool> ClearTaskType(string TaskType);
        Task<IEnumerable<TasksFrequencyListViewModel>> GetTaskFreqs();
        Task<IEnumerable<TasksFrequencyListViewModel>> GetTaskFreqsSync();
        Task<int> AddTask(TasksTasksViewModel _task);
        Task<TasksRecTasksViewModel> AddRecurringTask(TasksRecTasksViewModel _rectask);
        Task<int> AddTaskType(TasksTasksTaskTypeViewModel _type);
        Task<int> AddTaskUpdate(TasksTaskUpdatesViewModel _update);
        Task<int> AddTaskUpdateSync(TasksTaskUpdatesViewModel _update);
        Task<IEnumerable<TasksTaskUpdatesViewModel>> GetUpdatesByTask(int TaskId, DateTime UpdateDate);
        Task<IEnumerable<TasksTaskUpdatesViewModel>> GetLatestUpdates();
        Task<bool> UpdateTaskUpdate(TasksTaskUpdatesViewModel _update);
        Task<bool> UpdateTaskUpdateSync(TasksTaskUpdatesViewModel _update);
        Task<bool> RemoveTaskUpdate(TasksTaskUpdatesViewModel _update);
        Task<bool> UpdateTaskType(TasksTasksTaskTypeViewModel _type);
        Task<bool> UpdateTask(TasksTasksViewModel _task, int? PreviousPriority);
        Task<bool> UpdateOneTask(TasksTasksViewModel _task);
        Task<bool> RemoveOneTask(TasksTasksViewModel _task);
        Task<TasksFrequencyListViewModel> GetFrequency(string frequency);
        Task<TasksFrequencyListViewModel> GetFrequencySync(string frequency);
        Task<TasksFrequencyListViewModel> GetFrequencySync(int frequencyId);
        Task<TasksRecTasksViewModel> UpdateRecurringTask(TasksRecTasksViewModel _rectask);
        Task<TasksRecTasksViewModel> UpdateRecurringTaskSync(TasksRecTasksViewModel _rectask);
        //Task BulkUpdateRecurringTask(TasksRecTasksViewModel _rectask);
        Task<bool> RemoveTask(TasksTasksViewModel _task);
        Task<bool> RemoveRecurringTask(TasksRecTasksViewModel _rectask);
        Task<bool> RemoveTaskType(TasksTasksTaskTypeViewModel _type);
        Task<List<IGrouping<string, TaskTask>>> GetActiveTasks();
        Task<int> GetPastDueTasks();
        DataTable PercentageCalculation(List<TasksRecTasksViewModel> tasksList, List<TasksTaskUpdatesViewModel> updateTaskList, List<TasksFrequencyListViewModel> frequenceList, int dateDifference);
        DataTable RecTaskUpdatePercentageCalculation(TasksRecTasksViewModel task,
            IEnumerable<TasksTaskUpdatesViewModel> updateTaskList,
            TasksFrequencyListViewModel frequency);
        Task<IEnumerable<UpdateReportViewModel>> GetUpdateReport(string auditPerson, DateTime reapotingDate);
        Task<IEnumerable<UpdateReportViewModel>> GetUpdateReport(int recTasksId);
        Task<IEnumerable<UpdateReportViewModel>> GetUpdateReport(string auditPerson,
            DateTime reapotingFromDate, DateTime reapotingToDate);
        int RecurringUpcommingDate(TasksFrequencyListViewModel taskFrequency);
        DateTime RecurringUpcommingDate(DateTime date);
        TasksRecTasksViewModel RecurringUpcommingDate(TasksRecTasksViewModel recurringTask,
            TasksFrequencyListViewModel frequency, TasksTaskUpdatesViewModel rectUpdate);
        Task<List<TasksTaskUpdatesViewModel>> RecTaskGraphTrendLineCalculation(int monthDuration,
            List<TasksTaskUpdatesViewModel> graphUpdates);
        Task<TasksTaskUpdatesViewModel> SaveCaptureImageAsync(Dictionary<Guid, string> capturedImgaesList,
            int recurringTaskId, TasksTaskUpdatesViewModel recurringTaskUpdate);
        Task<int> CountPendingRecurringTasks(bool isDeleted, bool isDeactivated);
        Task<int> CountApprovedLastMonthRecurringTasks();
        Task<EmailQueueViewModel> GetEmailByEmailIdAsync(int EmailId);
        Task<bool> SendEmail(TasksRecTasksViewModel newTask, bool IsEditing, string url, string emailSubject);
        Task<bool> DeclineTaskEmail(TasksRecTasksViewModel recTask, string note, string emailSubject, bool isInquiry);
        Task DeleteTaskAsync(IEnumerable<int> taskIds);
		Task<bool> UpdateTaskCostingCodeAsync(int taskId, int? costingCode);
		Task<TasksTasksViewModel?> GetTaskByIdAsync(int taskId);

	}
}
