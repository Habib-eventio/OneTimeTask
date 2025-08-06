using CamcoTasks.Data.ModelsViewModel;
using CamcoTasks.Service.IService;
using CamcoTasks.ViewModels.EmailQueue;
using CamcoTasks.ViewModels.EmployeeDTO;
using CamcoTasks.ViewModels.LoggingChangeLogDTO;
using CamcoTasks.ViewModels.TasksFrequencyListDTO;
using CamcoTasks.ViewModels.TasksImagesDTO;
using CamcoTasks.ViewModels.TasksRecTasksDTO;
using CamcoTasks.ViewModels.TasksTasksDTO;
using CamcoTasks.ViewModels.TasksTasksTaskTypeDTO;
using CamcoTasks.ViewModels.TasksTaskUpdatesDTO;
using CamcoTasks.Infrastructure.Entities.TaskInfo;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;
using System.Text;
using IUnitOfWork = CamcoTasks.Infrastructure.IUnitOfWork;
using CamcoTasks.Infrastructure.Common.Email;
using CamcoTasks.Infrastructure.Entities.Task;
using CamcoTasks.Infrastructure.Defaults;

namespace CamcoTasks.Service.Service
{
	public class TasksService : ITasksService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly ILogger<TasksService> _logger;
		private readonly IFileManagerService _fileManagerService;
		private readonly IEmployeeService _employeeService;
		private readonly IEmailService _emailService;
		private readonly ILoggingChangeLogService _loggingChangeLogService;
		private readonly IJobDescriptionsService _jobDescriptionsService;


		public TasksService(IUnitOfWork unitOfWork, ILogger<TasksService> logger,
			ILoggingChangeLogService loggingChangeLogService, IEmailService emailService,
			IFileManagerService fileManagerService, IEmployeeService employeeService, IJobDescriptionsService jobDescriptionService)
		{
			_unitOfWork = unitOfWork;
			_logger = logger;
			_loggingChangeLogService = loggingChangeLogService;
			_fileManagerService = fileManagerService;
			_employeeService = employeeService;
			_emailService = emailService;
			_jobDescriptionsService = jobDescriptionService;
		}

		public async Task<TasksRecTasksViewModel> AddRecurringTask(TasksRecTasksViewModel _rectask)
		{
			var result = TasksRecTasksDTONew.Map(_rectask);
			await _unitOfWork.RecurringTasks.AddAsync(result);
			await _unitOfWork.CompleteAsync();
			return TasksRecTasksDTONew.Map(await _unitOfWork.RecurringTasks.GetAsync(result.Id));
		}

		public async Task<int> AddTask(TasksTasksViewModel _task)
		{
			return await _unitOfWork.TaskTasks.AddTask(TasksTasksDTONew.Map(_task));
		}

		public async Task<int> AddTaskType(TasksTasksTaskTypeViewModel _type)
		{
			var result = TasksTasksTaskTypeDTONew.Map(_type);
			await _unitOfWork.TaskTaskTypes.AddAsync(result);
			await _unitOfWork.CompleteAsync();
			return result.Id;
		}

		public async Task<int> AddTaskUpdate(TasksTaskUpdatesViewModel _update)
		{
			var taskUpdate = new TaskUpdate()
			{
				TaskId = _update.TaskID,
				IsDeleted = _update.IsDeleted,
				PictureLink = _update.PictureLink,
				RecurringId = _update.RecurringID,
				Update = _update.Update,
				UpdateDate = _update.UpdateDate,
				FileLink = _update.FileLink,
				IsPicture = _update.IsPicture,
				IsAudit = _update.IsAudit,
				IsPass = _update.IsPass,
				GraphNumber = _update.GraphNumber,
				DueDate = _update.DueDate,
				TaskCompleted = _update.TaskCompleted,
				QuestionAnswer = _update.QuestionAnswer,
				FailReason = _update.FailReason,
				FailedAuditList = _update.FailedAuditList,
				EmailId = _update.EmailId,
				CreatedDate = _update.CreatedDate,
				PostponeReason = _update.PostponeReason,
				PostponeDays = _update.PostponeDays,
				UpdateBy = _update.UpdateBy,
				UpdatedDocumentLink = _update.UpdatedDocumentLink,
			};

			if (string.IsNullOrEmpty(taskUpdate.Update))
			{ taskUpdate.Update = ""; }

			await _unitOfWork.TaskUpdates.AddAsync(taskUpdate);
			await _unitOfWork.CompleteAsync();
			return await _unitOfWork.TaskUpdates.AddTaskUpdate(TasksTaskUpdateDTONew.Map(_update));

			return taskUpdate.UpdateId;
		}

		public async Task<int> AddTaskUpdateSync(TasksTaskUpdatesViewModel _update)
		{
			var result = new TaskUpdate()
			{
				TaskId = _update.TaskID,
				IsDeleted = _update.IsDeleted,
				PictureLink = _update.PictureLink,
				RecurringId = _update.RecurringID,
				Update = _update.Update,
				UpdateDate = _update.UpdateDate,
				FileLink = _update.FileLink,
				IsPicture = _update.IsPicture,
				IsAudit = _update.IsAudit,
				IsPass = _update.IsPass,
				GraphNumber = _update.GraphNumber,
				DueDate = _update.DueDate,
				TaskCompleted = _update.TaskCompleted,
				QuestionAnswer = _update.QuestionAnswer,
				FailReason = _update.FailReason,
				FailedAuditList = _update.FailedAuditList,
				EmailId = _update.EmailId,
				CreatedDate = _update.CreatedDate,
				PostponeReason = _update.PostponeReason,
				PostponeDays = _update.PostponeDays,
				UpdateBy = _update.UpdateBy,
				UpdatedDocumentLink = _update.UpdatedDocumentLink,
			};

			if (string.IsNullOrEmpty(result.Update))
			{
				result.Update = "";
			}
			await _unitOfWork.TaskUpdates.AddAsync(result);
			await _unitOfWork.CompleteAsync();
			return result.UpdateId;
		}

		public async Task<IEnumerable<TasksTaskUpdatesViewModel>> GetUpdatesByTask(int TaskId, DateTime UpdateDate)
		{
			return TasksTaskUpdateDTONew.Map(await _unitOfWork.TaskUpdates.FindAllAsync(x => x.RecurringId == TaskId &&
		x.UpdateDate == UpdateDate));
		}

		public async Task<IEnumerable<TasksTaskUpdatesViewModel>> GetLatestUpdates()
		{
			return TasksTaskUpdateDTONew.Map(await _unitOfWork.TaskUpdates.GetLatestUpdates());
		}

		public async Task<IEnumerable<UpdateReportViewModel>> GetUpdateReport(string auditPerson, DateTime reapotingDate)
		{
			var items = await _unitOfWork.RecurringTasks.GetUpdateReport(auditPerson, reapotingDate);

			if (items.Any())
			{
				items[0].StartTime = items[0].EndTime;
				items[0].TimeSpent = 0;

				for (int index = 1; index < items.Count; index++)
				{
					items[index].StartTime = items[index - 1].EndTime;
					items[index].TimeSpent = (int)(items[index].CreatedDate.Value - items[index - 1].CreatedDate.Value).TotalMinutes;

					if ((items[index].TimeSpent / 60) > 3)
					{
						items.Remove(items[index]);
					}
				}
			}

			return items;
		}

		public async Task<IEnumerable<UpdateReportViewModel>> GetUpdateReport(int recTasksId)
		{
			var items = await _unitOfWork.RecurringTasks.GetUpdateReport(recTasksId);

			if (items.Any())
			{
				items[0].StartTime = items[0].EndTime;
				items[0].TimeSpent = 0;

				for (int index = 1; index < items.Count; index++)
				{
					items[index].StartTime = items[index - 1].EndTime;
					items[index].TimeSpent = (int)(items[index].CreatedDate.Value - items[index - 1].CreatedDate.Value).TotalMinutes;

					if ((items[index].TimeSpent / 60) > 3)
					{
						items.Remove(items[index]);
					}
				}
			}

			return items;
		}

		public async Task<IEnumerable<UpdateReportViewModel>> GetUpdateReport(string auditPerson,
			DateTime reapotingFromDate, DateTime reapotingToDate)
		{
			var items = await _unitOfWork.RecurringTasks.GetUpdateReport(auditPerson, reapotingFromDate, reapotingToDate);

			if (items.Any())
			{
				items[0].StartTime = items[0].EndTime;
				items[0].TimeSpent = 0;

				for (int index = 1; index < items.Count; index++)
				{
					items[index].StartTime = items[index - 1].EndTime;
					items[index].TimeSpent = (int)(items[index].CreatedDate.Value - items[index - 1].CreatedDate.Value).TotalMinutes;

					if ((items[index].TimeSpent / 60) > 3)
					{
						items.Remove(items[index]);
					}
				}
			}

			return items;
		}

		public async Task<IEnumerable<TasksTasksViewModel>> GetAllTasks()
		{
			return TasksTasksDTONew.Map(await _unitOfWork.TaskTasks.FindAllAsNoTrackingAsync(x => (x.IsDeleted == null || x.IsDeleted == false) &&
					  x.ParentTaskId == null));
		}

		public async Task<IEnumerable<TasksTasksViewModel>> GetAllTasks1()
		{
			return TasksTasksDTONew.Map(await _unitOfWork.TaskTasks.FindAllAsync(x => x.IsDeleted == false));
		}

		public async Task<IEnumerable<TasksTasksViewModel>> GetAllTasks2()
		{
			return TasksTasksDTONew.Map(await _unitOfWork.TaskTasks.FindAllAsync(x => (x.IsDeleted == false) &&
				x.ParentTaskId == null));
		}

                public async Task<IEnumerable<TasksTasksViewModel>> GetAllTasks3(string TaskId)
                {
                        return TasksTasksDTONew.Map(await _unitOfWork.TaskTasks.FindAllAsync(x => (x.IsDeleted == false) &&
                                x.ParentTaskId == Convert.ToInt32(TaskId)));
                }

                public async Task<IEnumerable<TasksTasksViewModel>> GetTasksByPerson(string personFullName)
                {
                        if (string.IsNullOrWhiteSpace(personFullName))
                        {
                                return Enumerable.Empty<TasksTasksViewModel>();
                        }

                        var normalized = personFullName.Trim().ToLower();

                        return TasksTasksDTONew.Map(await _unitOfWork.TaskTasks.FindAllAsync(x => (x.IsDeleted == null || x.IsDeleted == false)
                                                                                                     && x.ParentTaskId == null
                                                                                                     && x.PersonResponsible != null
                                                                                                     && x.PersonResponsible.ToLower() == normalized));
                }

                public async Task<IEnumerable<TasksTasksViewModel>> GetAllTasks(string OldTypeValue)
                {
                        return TasksTasksDTONew.Map(await _unitOfWork.TaskTasks.FindAllAsync(x => x.TaskType == OldTypeValue));
                }

		public async Task<IEnumerable<TasksTasksViewModel>> GetAllTasksSync()
		{
			return TasksTasksDTONew.Map(await _unitOfWork.TaskTasks.FindAllAsync(x => (x.IsDeleted == null || x.IsDeleted == false) &&
			   x.ParentTaskId == null));
		}

		public async Task<IEnumerable<TasksTasksViewModel>> GetAllTasksSync1()
		{
			return TasksTasksDTONew.Map(await _unitOfWork.TaskTasks.FindAllAsync(x => (x.IsDeleted == false) &&
					  x.ParentTaskId == null));
		}

		public async Task<TasksFrequencyListViewModel> GetFrequency(string frequency)
		{
			return TasksFrequencyListDTONew.Map(await _unitOfWork.FrequencyLists.FindAsync(a => a.Frequency == frequency));
		}

		public async Task<TasksFrequencyListViewModel> GetFrequencySync(string frequency)
		{
			return TasksFrequencyListDTONew.Map(await _unitOfWork.FrequencyLists.FindAsync(a => a.Frequency == frequency));
		}

		public async Task<TasksFrequencyListViewModel> GetFrequencySync(int frequencyId)
		{
			return TasksFrequencyListDTONew.Map(await _unitOfWork.FrequencyLists.FindAsync(a => a.Id == frequencyId));
		}

		public async Task<TasksRecTasksViewModel> GetRecurringTaskById(int Id)
		{
			return TasksRecTasksDTONew.Map(await _unitOfWork.RecurringTasks.GetRecurringTaskById(Id));
		}

		public TasksRecTasksViewModel GetRecurringTaskByIdSync(int Id)
		{
			return TasksRecTasksDTONew.Map(_unitOfWork.RecurringTasks.GetRecurringTaskByIdSync(Id));
		}

		public async Task<IEnumerable<TasksRecTasksViewModel>> GetRecurringTasks(Func<RecurringTask, bool> p)
		{
			return TasksRecTasksDTONew.Map(await _unitOfWork.RecurringTasks.GetRecurringTasks(p));
		}

		public IEnumerable<TasksRecTasksViewModel> GetRecurringTasksSync(Func<RecurringTask, bool> p)
		{
			return TasksRecTasksDTONew.Map(_unitOfWork.RecurringTasks.GetRecurringTasksSync(p));
		}

		public async Task<IEnumerable<TasksRecTasksViewModel>> GetRecurringTasks(bool isDeleted, bool isDeactivated, int? parentTaskId)
		{
			return TasksRecTasksDTONew.Map(await _unitOfWork.RecurringTasks.GetRecurringTasksByIsDeletedAndDeactivatedAndParentTaskIdAsync(isDeleted, isDeactivated, parentTaskId));
		}

		public async Task<IEnumerable<TasksRecTasksViewModel>> GetRecurringTasksForPercentageAsync(bool isDeleted, bool isDeactivated,
			int? parentTaskId)
		{
			return TasksRecTasksDTONew.Map(await _unitOfWork.RecurringTasks.GetRecurringTasksByIsDeletedAndDeactivatedAndParentTaskIdOrderByIdAsync(isDeleted, isDeactivated, parentTaskId));
		}

		public async Task<IEnumerable<TasksRecTasksViewModel>> GetRecurringTasksBySearchAsync(
			bool isDeleted, bool isDeactivated, string searchParttern)
		{
			return TasksRecTasksDTONew.Map(await _unitOfWork.RecurringTasks.GetRecurringTasksByIsDeletedAndDeactivatedAndSearchValueAsync(isDeleted,
				isDeactivated, searchParttern));
		}

		public async Task<IEnumerable<TasksRecTasksViewModel>> GetRecurringTasks(bool isDeleted, bool isDeactivated, int? parentTaskId, int skip, int take)
		{
			return TasksRecTasksDTONew.Map(await _unitOfWork.RecurringTasks.GetRecurringTasksByIsDeletedAndDeactivatedAndParentTaskIdAndSkipAndTakeAsync(isDeleted, isDeactivated, parentTaskId, skip, take));
		}

		public async Task<int> CountRecurringTasks(bool isDeleted, bool isDeactivated, int? parentTaskId)
		{
			return await _unitOfWork.RecurringTasks.CountAsync(x => x.IsDeleted == isDeleted && x.IsDeactivated == isDeactivated && x.IsApproved && x.ParentTaskId.Equals(parentTaskId));
		}

		public async Task<int> CountRecurringTasks(bool isDeleted, bool isDeactivated, DateTime toDate, string auditPerson)
		{
			return await _unitOfWork.RecurringTasks.CountAsync(x => x.AuditPerson.Contains(auditPerson)
															  && x.IsDeleted == isDeleted
															  && x.IsDeactivated == isDeactivated
															  && x.IsApproved
															  && x.UpcomingDate.HasValue &&
															  x.UpcomingDate.Value.Date < toDate.Date);
		}

		public async Task<int> CountPendingRecurringTasks(bool isDeleted, bool isDeactivated)
		{
			return await _unitOfWork.RecurringTasks.CountAsync(x => x.IsDeleted == isDeleted
															  && x.IsDeactivated == isDeactivated
															  && !x.IsApproved);
		}

		public async Task<int> CountApprovedLastMonthRecurringTasks()
		{
			DateTime today = DateTime.Now.AddDays(-30);
			return await _unitOfWork.RecurringTasks.CountAsync(x => !x.IsDeleted
															  && !x.IsDeactivated
															  && x.IsApproved
															  && x.DateApproved.HasValue
															  && x.DateApproved.Value.Date >= today.Date);
		}

		public async Task<IEnumerable<TasksRecTasksViewModel>> GetRecurringTasksSync(bool isDeleted, bool isDeactivated, int? parentTaskId)
		{
			return TasksRecTasksDTONew.Map(await _unitOfWork.RecurringTasks.GetRecurringTasksByIsDeletedAndDeactivatedAndParentTaskIdAsync(isDeleted, isDeactivated, parentTaskId));
		}

		public async Task<IEnumerable<TasksRecTasksViewModel>> GetRecurringTasks(bool isDeleted, bool isDeactivated,
			int? parentTaskId, string personResp)
		{
			return TasksRecTasksDTONew.Map(await _unitOfWork.RecurringTasks.GetRecurringTasksByIsDeletedAndDeactivatedAndParentTaskIdAndPersonResponsibleAsync(isDeleted, isDeactivated, parentTaskId, personResp));
		}

		public async Task<IEnumerable<TasksRecTasksViewModel>> GetRecurringTasksByTaskAreaAsync(bool isDeleted, bool isDeactivated, bool isApproved,
			int? parentTaskId, string taskArea)
		{
			return TasksRecTasksDTONew.Map(await _unitOfWork.RecurringTasks.GetRecurringTasksByIsDeletedAndDeactivatedAndParentTaskIdAndTaskAreaAsync(isDeleted, isDeactivated, isApproved, parentTaskId,
				taskArea));
		}

		public async Task<int> GetRecurringTasksCountAsync()
		{
			return await _unitOfWork.RecurringTasks.GetRecurringTasksCountAsync();
		}

		public async Task<int> GetRecurringTasksCountAsync(bool isDeleted, bool isDactivate)
		{
			return await _unitOfWork.RecurringTasks.GetRecurringTasksCountAsync(isDeleted, isDactivate);
		}

		public IEnumerable<TasksRecTasksViewModel> GetRecurringTasksSync()
		{
			return TasksRecTasksDTONew.Map(_unitOfWork.RecurringTasks.GetRecurringTasksSync());
		}

		public async Task<TasksTasksViewModel> GetTaskById(int Id)
		{
			return TasksTasksDTONew.Map(await _unitOfWork.TaskTasks.GetAsync(Id));
		}

		public async Task<IEnumerable<TasksFrequencyListViewModel>> GetTaskFreqs()
		{
			return TasksFrequencyListDTONew.Map(await _unitOfWork.FrequencyLists.GetListAsync());
		}

		public async Task<IEnumerable<TasksFrequencyListViewModel>> GetTaskFreqsSync()
		{
			return TasksFrequencyListDTONew.Map(await _unitOfWork.FrequencyLists.GetListAsync());
		}

		//public async Task<IEnumerable<TasksTasksViewModel>> GetTasks(Func<TaskTask, bool> p)
		//{
		//    return TasksTasksDTONew.Map(await _unitOfWork.TaskTasks.GetTasks(p));
		//}

		public async Task<IEnumerable<TasksTasksViewModel>> GetTasks()
		{
			return TasksTasksDTONew.Map(await _unitOfWork.TaskTasks.FindAllAsync(x => x.IsDeleted == false));
		}

		public async Task<int> GetOneTimeTasksCountAsync()
		{
			return await _unitOfWork.TaskTasks.CountAsync(x => x.IsDeleted == false && x.ParentTaskId == null);
			//return await _unitOfWork.RecurringTasks.GetOneTimeTasksCountAsync();
		}

		public async Task<IEnumerable<TasksTasksViewModel>> GetTasksSync()
		{
			return TasksTasksDTONew.Map(await _unitOfWork.TaskTasks.FindAllAsync(x => x.IsDeleted == false));
		}

		public async Task<TasksTasksTaskTypeViewModel> GetTaskType(string TaskType)
		{
			return TasksTasksTaskTypeDTONew.Map(await _unitOfWork.TaskTaskTypes.FindAsync(x => x.TaskType == TaskType));
		}

		public async Task<IEnumerable<TasksTasksTaskTypeViewModel>> GetTaskTypes()
		{
			return TasksTasksTaskTypeDTONew.Map(await _unitOfWork.TaskTaskTypes.GetListAsync());
		}

		public async Task<IEnumerable<TasksTasksTaskTypeViewModel>> GetTaskTypesSync()
		{
			return TasksTasksTaskTypeDTONew.Map(await _unitOfWork.TaskTaskTypes.GetListAsync());
		}

		public async Task<IEnumerable<TasksTasksViewModel>> GetEmptyTaskTypes()
		{
			var result =
				await _unitOfWork.TaskTasks.FindAllAsync(x => x.IsDeleted != true && string.IsNullOrEmpty(x.TaskType));
			return TasksTasksDTONew.Map(result);
		}
		public async Task<bool> ClearTaskType(string TaskType)
		{
			return await _unitOfWork.RecurringTasks.ClearTaskType(TaskType);
		}

		public async Task<IEnumerable<TasksTaskUpdatesViewModel>> GetTaskUpdates(int taskId)
		{
			return TasksTaskUpdateDTONew.Map(await _unitOfWork.TaskUpdates.FindAllAsync(x => x.TaskId == taskId && (x.IsDeleted == null || x.IsDeleted == false)));
		}

		public async Task<IEnumerable<TasksTaskUpdatesViewModel>> GetTaskUpdates1(int taskId)
		{
			return TasksTaskUpdateDTONew.Map(await _unitOfWork.TaskUpdates.FindAllAsync(x => x.TaskId == taskId));
		}

		public async Task<TasksTaskUpdatesViewModel> GetRecurringTaskLatestUpdateAsync(int recurringTaskId)
		{
			return TasksTaskUpdateDTONew.Map(await _unitOfWork.TaskUpdates.GetRecurringTaskLatestUpdateAsync(recurringTaskId));
		}

		public async Task<IEnumerable<TasksTaskUpdatesViewModel>> GetTaskUpdates(int recurringId, string ignoreNudgudUpdate, string ignoreEmailUpdate, bool isDeleted)
		{
			return TasksTaskUpdateDTONew.Map(await _unitOfWork.TaskUpdates.FindAllAsync(x => x.RecurringId == recurringId && !x.Update.Contains(ignoreNudgudUpdate)
				&& !x.Update.Contains(ignoreEmailUpdate)
				&& x.IsDeleted == false));
		}

		public async Task<IEnumerable<TasksTaskUpdatesViewModel>> GetTaskUpdates(int recurringId, DateTime updateDate)
		{
			return TasksTaskUpdateDTONew.Map(await _unitOfWork.TaskUpdates.FindAllAsync(x => x.RecurringId.HasValue && x.RecurringId == recurringId && x.UpdateDate <= updateDate && x.TaskCompleted.HasValue));
		}

		public async Task<IEnumerable<TasksTaskUpdatesViewModel>> GetTaskUpdates(int recurringId, bool isDeleted)
		{
			return TasksTaskUpdateDTONew.Map(await _unitOfWork.TaskUpdates.GetTaskUpdatesAsync(recurringId, isDeleted));
		}

		public async Task<int> GetTaskUpdatesCountAsync(int recurringId, bool isDeleted)
		{
			return await _unitOfWork.TaskUpdates.CountAsync(x => x.RecurringId == recurringId && x.IsDeleted == isDeleted);
			//return await _unitOfWork.TaskUpdates.GetTaskUpdatesCountAsync(recurringId, isDeleted);
		}

		public async Task<IEnumerable<TasksTaskUpdatesViewModel>> GetTaskUpdatesSync()
		{
			return TasksTaskUpdateDTONew.Map(await _unitOfWork.TaskUpdates.FindAllAsync(x => !x.Update.ToLower().Contains("system generated update") && x.RecurringId.HasValue && x.DueDate.HasValue && x.DueDate >= DateTime.Today.AddMonths(-12)));
		}

		public async Task<TasksTaskUpdatesViewModel> GetTaskUpdatesByIdAsync(int Id)
		{
			return TasksTaskUpdateDTONew.Map(await _unitOfWork.TaskUpdates.FindAsync(t => t.UpdateId == Id));
		}

		public async Task<TasksTaskUpdatesViewModel> GetTaskUpdatesById(int Id)
		{
			return TasksTaskUpdateDTONew.Map(await _unitOfWork.TaskUpdates.FindAsync(t => t.UpdateId == Id));
		}

		public async Task<IEnumerable<TasksImagesViewModel>> GetOneTimeTaksUpdateFilesAsync(int taskId)
		{
			return TasksImagesDTONew.Map(await _unitOfWork.TaskImages.FindAllAsync(x => x.OneTimeId == taskId
			&& x.IsDeleted == false));
		}

		public async Task<IEnumerable<TasksImagesViewModel>> GetOneTimeTaksUpdateFilesAsync1(int taskId)
		{
			return TasksImagesDTONew.Map(await _unitOfWork.TaskImages.FindAllAsync(x => x.UpdateId == taskId && (x.IsDeleted == null || x.IsDeleted == false)));
		}

		public async Task<IEnumerable<TasksTaskUpdatesViewModel>> GetTaskUpdatesWithAllFieldSync(int recurringId, bool isDeleted)
		{
			return TasksTaskUpdateDTONew.Map(await _unitOfWork.TaskUpdates.FindAllAsync(x => x.RecurringId == recurringId && x.IsDeleted == isDeleted));
		}

		public async Task<IEnumerable<TasksTaskUpdatesViewModel>> GetTaskUpdatesWithAllFieldSync(int taskId)
		{
			return TasksTaskUpdateDTONew.Map((await _unitOfWork.TaskUpdates.FindAllAsync(x => x.RecurringId == taskId && x.IsDeleted == false)).OrderByDescending(x => x.UpdateDate));
		}

		public async Task<IEnumerable<TasksTaskUpdatesViewModel>> GetTaskUpdates(List<int> Ids)
		{
			return TasksTaskUpdateDTONew.Map(await _unitOfWork.TaskUpdates.FindAllAsync(x => Ids.Contains((int)x.TaskId)));
		}

		public async Task<IEnumerable<TasksTaskUpdatesViewModel>> GetTaskUpdates()
		{
			return TasksTaskUpdateDTONew.Map(await _unitOfWork.TaskUpdates.GetListAsync());
		}
		public async Task<bool> RemoveOneTask(TasksTasksViewModel _task)
		{
			return await _unitOfWork.TaskTasks.RemoveOneTask(TasksTasksDTONew.Map(_task));
		}

		public async Task<bool> RemoveRecurringTask(TasksRecTasksViewModel _rectask)
		{
			await _unitOfWork.RecurringTasks.RemoveAsync(TasksRecTasksDTONew.Map(_rectask));
			return await _unitOfWork.CompleteAsync();
		}

		public async Task<bool> RemoveTask(TasksTasksViewModel _task)
		{
			return await _unitOfWork.TaskTasks.RemoveTask(TasksTasksDTONew.Map(_task));
		}

		public async Task<bool> RemoveTaskType(TasksTasksTaskTypeViewModel _type)
		{
			await _unitOfWork.TaskTaskTypes.RemoveAsync(TasksTasksTaskTypeDTONew.Map(_type));
			return await _unitOfWork.CompleteAsync();
		}

		public async Task<bool> RemoveTaskUpdate(TasksTaskUpdatesViewModel _update)
		{
			return await _unitOfWork.TaskUpdates.RemoveTaskUpdate(TasksTaskUpdateDTONew.Map(_update));
		}

		public async Task<bool> UpdateOneTask(TasksTasksViewModel _task)
		{
			return await _unitOfWork.TaskTasks.UpdateOneTask(TasksTasksDTONew.Map(_task));
		}

		public async Task<TasksRecTasksViewModel> UpdateRecurringTask(TasksRecTasksViewModel _recurringTask)
		{
			var recurringTask = await _unitOfWork.RecurringTasks.GetAsync(_recurringTask.Id);

			recurringTask.TaskId = _recurringTask.TaskId;
			recurringTask.PersonResponsible = _recurringTask.PersonResponsible;
			recurringTask.Description = _recurringTask.Description;
			recurringTask.DateCreated = _recurringTask.DateCreated;
			recurringTask.DateCompleted = _recurringTask.DateCompleted;
			recurringTask.UpcomingDate = _recurringTask.UpcomingDate;
			recurringTask.Updates = _recurringTask.Updates;
			recurringTask.Initiator = _recurringTask.Initiator;
			recurringTask.PictureLink = _recurringTask.PictureLink;
			recurringTask.IsDeleted = _recurringTask.IsDeleted;
			recurringTask.IsDeactivated = _recurringTask.IsDeactivated;
			recurringTask.InstructionFileLink = _recurringTask.InstructionFileLink;
			recurringTask.NudgeCount = _recurringTask.NudgeCount;
			recurringTask.EmailsList = _recurringTask.EmailsList;
			recurringTask.CompletedOnTime = _recurringTask.CompletedOnTime;
			recurringTask.IsGraphRequired = _recurringTask.IsGraphRequired;
			recurringTask.AuditPerson = _recurringTask.AuditPerson;
			recurringTask.GraphTitle = _recurringTask.GraphTitle;
			recurringTask.VerticalAxisTitle = _recurringTask.VerticalAxisTitle;
			recurringTask.IsTrendLine = _recurringTask.IsTrendLine;
			recurringTask.ExternalAuditor = _recurringTask.ExternalAuditor;
			recurringTask.IsPassOrFail = _recurringTask.IsPassOrFail;
			recurringTask.Question = _recurringTask.Question;
			recurringTask.IsQuestionRequired = _recurringTask.IsQuestionRequired;
			recurringTask.IsPicRequired = _recurringTask.IsPicRequired;
			recurringTask.UpdateImageType = _recurringTask.UpdateImageType;
			recurringTask.UpdateImageLocation = _recurringTask.UpdateImageLoction;
			recurringTask.UpdateImageDescription = _recurringTask.UpdateImageDescription;
			recurringTask.FailedEmailsList = _recurringTask.FailedEmailsList;
			recurringTask.IsAuditRequired = _recurringTask.IsAuditRequired;
			recurringTask.LatestGraphValue = _recurringTask.LatestGraphValue;
			recurringTask.ParentTaskId = _recurringTask.ParentTaskId;
			recurringTask.IsDescriptionMandatory = _recurringTask.IsDescriptionMandatory;
			recurringTask.StartDate = _recurringTask.StartDate;
			recurringTask.IsMaxValueRequired = _recurringTask.IsMaxValueRequired;
			recurringTask.MaxYAxisValue = _recurringTask.MaxYAxisValue;
			recurringTask.IsHandDeliveredRequired = _recurringTask.IsHandDeliveredRequired;
			recurringTask.IsProtected = _recurringTask.IsProtected;
			recurringTask.DueDateReminder = _recurringTask.DueDateReminder;
			recurringTask.EmailCount = _recurringTask.EmailCount;
			recurringTask.IsPicture = _recurringTask.IsPicture;
			recurringTask.TaskArea = _recurringTask.TaskArea;
			recurringTask.AuthorizationList = _recurringTask.AuthorizationList;
			recurringTask.FrequencyId = _recurringTask.TasksFreq.Id;
			recurringTask.TaskDescriptionSubject = _recurringTask.TaskDescriptionSubject;
			recurringTask.IsPositionSpecific = _recurringTask.IsPositionSpecific;
			recurringTask.JobTitle = _recurringTask.JobTitle;
			recurringTask.StartDueDateDay = _recurringTask.StartDueDateDay;
			recurringTask.EndDueDateDay = _recurringTask.EndDueDateDay;
			recurringTask.ExpectMinutes = _recurringTask.ExpectMinutes;
			recurringTask.Location = _recurringTask.Location;
			recurringTask.EmailsListJobId = _recurringTask.EmailsListJobId;
			recurringTask.IsXAxisInterval = _recurringTask.IsXAxisInterval;
			recurringTask.XAxisIntervalTypeId = _recurringTask.XAxisIntervalTypeId;
			recurringTask.XAxisIntervalRange = _recurringTask.XAxisIntervalRange;
			recurringTask.TaskStartDueDate = _recurringTask.TaskStartDueDate;
			recurringTask.TaskEndDueDate = _recurringTask.TaskEndDueDate;
			recurringTask.IsTaskDuePeriod = _recurringTask.IsTaskDuePeriod;
			recurringTask.IsTaskDelayed = _recurringTask.IsTaskDelayed;
			recurringTask.DelayReason = _recurringTask.DelayReason;
			recurringTask.TaskDelayedDate = _recurringTask.TaskDelayedDate;
			recurringTask.IsTaskRandomize = _recurringTask.IsTaskRandomize;
			recurringTask.IsDocumentRequired = _recurringTask.IsDocumentRequired;
			recurringTask.UpdatedDocumentLink = _recurringTask.UpdatedDocumentLink;
			recurringTask.UpdatedDocumentDescription = _recurringTask.UpdatedDocumentDescription;
			recurringTask.HandDocumentDeliverTo = _recurringTask.HandDocumentDeliverTo;
			recurringTask.TaskDeactivatedDate = _recurringTask.TaskDeactivatedDate;

			await _unitOfWork.CompleteAsync();

			return TasksRecTasksDTONew.Map(recurringTask);
		}

		public async Task<TasksRecTasksViewModel> UpdateRecurringTaskSync(TasksRecTasksViewModel _rectask)
		{

			var recTask = await _unitOfWork.RecurringTasks.GetAsync(_rectask.Id);

			recTask.DateCompleted = _rectask.DateCompleted;
			recTask.Description = _rectask.Description;
			recTask.FrequencyId = _rectask.TasksFreq.Id;
			recTask.Id = _rectask.Id;
			recTask.Initiator = _rectask.Initiator;
			recTask.IsDeactivated = _rectask.IsDeactivated;
			recTask.IsDeleted = _rectask.IsDeleted;
			recTask.IsApproved = _rectask.IsApproved;
			recTask.ApprovedByEmployeeId = _rectask.ApprovedByEmployeeId;
			recTask.DateApproved = _rectask.DateApproved;
			recTask.PersonResponsible = _rectask.PersonResponsible;
			recTask.PictureLink = _rectask.PictureLink;
			recTask.DateCreated = _rectask.DateCreated;
			recTask.TaskId = _rectask.TaskId;
			recTask.UpcomingDate = _rectask.UpcomingDate;
			recTask.Updates = _rectask.Updates;
			recTask.InstructionFileLink = _rectask.InstructionFileLink;
			recTask.NudgeCount = _rectask.NudgeCount;
			recTask.EmailCount = _rectask.EmailCount;
			recTask.EmailsList = _rectask.EmailsList;
			recTask.CompletedOnTime = _rectask.CompletedOnTime;
			recTask.TasksTaskUpdates = TasksTaskUpdateDTONew.Map(_rectask.TasksTaskUpdates).ToList();
			recTask.IsGraphRequired = _rectask.IsGraphRequired;
			recTask.AuditPerson = _rectask.AuditPerson;
			recTask.GraphTitle = _rectask.GraphTitle;
			recTask.IsTrendLine = _rectask.IsTrendLine;
			recTask.VerticalAxisTitle = _rectask.VerticalAxisTitle;
			recTask.ExternalAuditor = _rectask.ExternalAuditor;
			recTask.IsPassOrFail = _rectask.IsPassOrFail;
			recTask.Question = _rectask.Question;
			recTask.IsQuestionRequired = _rectask.IsQuestionRequired;
			recTask.IsPicRequired = _rectask.IsPicRequired;
			recTask.UpdateImageDescription = _rectask.UpdateImageDescription;
			recTask.UpdateImageType = _rectask.UpdateImageType;
			recTask.UpdateImageLocation = _rectask.UpdateImageLoction;
			recTask.FailedEmailsList = _rectask.FailedEmailsList;
			recTask.IsAuditRequired = _rectask.IsAuditRequired;
			recTask.LatestGraphValue = _rectask.LatestGraphValue;
			recTask.ParentTaskId = _rectask.ParentTaskId;
			recTask.IsDescriptionMandatory = _rectask.IsDescriptionMandatory;
			recTask.StartDate = _rectask.StartDate;
			recTask.IsMaxValueRequired = _rectask.IsMaxValueRequired;
			recTask.MaxYAxisValue = _rectask.MaxYAxisValue;
			recTask.IsHandDeliveredRequired = _rectask.IsHandDeliveredRequired;
			recTask.IsProtected = _rectask.IsProtected;
			recTask.DueDateReminder = _rectask.DueDateReminder;
			recTask.IsPicture = _rectask.IsPicture;
			recTask.TaskDescriptionSubject = _rectask.TaskDescriptionSubject;
			recTask.TaskArea = _rectask.TaskArea;
			recTask.AuthorizationList = _rectask.AuthorizationList;
			recTask.IsPositionSpecific = _rectask.IsPositionSpecific;
			recTask.JobTitle = _rectask.JobTitle;
			recTask.StartDueDateDay = _rectask.StartDueDateDay;
			recTask.EndDueDateDay = _rectask.EndDueDateDay;
			recTask.ExpectMinutes = _rectask.ExpectMinutes;
			recTask.Location = _rectask.Location;
			recTask.EmailsListJobId = _rectask.EmailsListJobId;
			recTask.IsXAxisInterval = _rectask.IsXAxisInterval;
			recTask.XAxisIntervalTypeId = _rectask.XAxisIntervalTypeId;
			recTask.XAxisIntervalRange = _rectask.XAxisIntervalRange;
			recTask.TaskStartDueDate = _rectask.TaskStartDueDate;
			recTask.TaskEndDueDate = _rectask.TaskEndDueDate;
			recTask.IsTaskDuePeriod = _rectask.IsTaskDuePeriod;
			recTask.IsTaskRandomize = _rectask.IsTaskRandomize;
			recTask.IsDocumentRequired = _rectask.IsDocumentRequired;
			recTask.UpdatedDocumentLink = _rectask.UpdatedDocumentLink;
			recTask.UpdatedDocumentDescription = _rectask.UpdatedDocumentDescription;
			recTask.HandDocumentDeliverTo = _rectask.HandDocumentDeliverTo;
			recTask.TaskDeactivatedDate = _rectask.TaskDeactivatedDate;

			await _unitOfWork.CompleteAsync();

			return TasksRecTasksDTONew.Map(TasksRecTasksDTONew.Map(_rectask));
		}

		//public async Task BulkUpdateRecurringTask(TasksRecTasksViewModel _rectask)
		//{
		//    await _unitOfWork.RecurringTasks.UpdateAsync(TasksRecTasksDTONew.Map(_rectask));
		//}

		public async Task<bool> UpdateTask(TasksTasksViewModel _task, int? PreviousPriority)
		{
			return await _unitOfWork.TaskTasks.UpdateTask(TasksTasksDTONew.Map(_task), PreviousPriority);
		}

		public async Task<bool> UpdateTaskType(TasksTasksTaskTypeViewModel _type)
		{
			var taskTask = await _unitOfWork.TaskTaskTypes.GetAsync(_type.Id ?? 0);

			taskTask.TaskType = _type.TaskType;
			taskTask.Id = (int)_type.Id;
			taskTask.Email = _type.Email;
			taskTask.Email2 = _type.Email2;

			await _unitOfWork.CompleteAsync();
			//await _unitOfWork.TaskTaskTypes.UpdateAsync(TasksTasksTaskTypeDTONew.Map(_type));
			return true;
		}

		public async Task<bool> UpdateTaskUpdate(TasksTaskUpdatesViewModel _update)
		{
			var taskUpdate = await _unitOfWork.TaskUpdates.GetAsync(_update.UpdateId);

			taskUpdate.TaskId = _update.TaskID;
			taskUpdate.IsDeleted = _update.IsDeleted;
			taskUpdate.PictureLink = _update.PictureLink;
			taskUpdate.RecurringId = _update.RecurringID;
			taskUpdate.Update = _update.Update;
			taskUpdate.UpdateDate = _update.UpdateDate;
			taskUpdate.UpdateId = _update.UpdateId;
			taskUpdate.FileLink = _update.FileLink;
			taskUpdate.IsPicture = _update.IsPicture;
			taskUpdate.IsAudit = _update.IsAudit;
			taskUpdate.IsPass = _update.IsPass;
			taskUpdate.GraphNumber = _update.GraphNumber;
			taskUpdate.DueDate = _update.DueDate;
			taskUpdate.TaskCompleted = _update.TaskCompleted;
			taskUpdate.QuestionAnswer = _update.QuestionAnswer;
			taskUpdate.FailReason = _update.FailReason;
			taskUpdate.FailedAuditList = _update.FailedAuditList;
			taskUpdate.EmailId = _update.EmailId;
			taskUpdate.CreatedDate = _update.CreatedDate;
			taskUpdate.PostponeReason = _update.PostponeReason;
			taskUpdate.PostponeDays = _update.PostponeDays;
			taskUpdate.UpdateBy = _update.UpdateBy;
			taskUpdate.UpdatedDocumentLink = _update.UpdatedDocumentLink;

			await _unitOfWork.CompleteAsync();

			return true;
		}

		public async Task<bool> UpdateTaskUpdateSync(TasksTaskUpdatesViewModel _update)
		{
			return await _unitOfWork.TaskUpdates.UpdateTaskUpdateSync(TasksTaskUpdateDTONew.Map(_update));
		}


		public async Task<IEnumerable<TasksImagesViewModel>> GetRecurringTaskImagesAsync(int TaskId)
		{
			return TasksImagesDTONew.Map(await _unitOfWork.TaskImages.FindAllAsync(x => x.RecurringId.Value == TaskId && !x.IsDeleted.Value));
		}

		public async Task<IEnumerable<TasksImagesViewModel>> GetRecurringTaskImages(int TaskId)
		{
			return TasksImagesDTONew.Map(await _unitOfWork.TaskImages.FindAllAsync(x => x.RecurringId.Value == TaskId && !x.IsDeleted.Value));
		}

		public async Task<IEnumerable<TasksImagesViewModel>> GetOneTimeTaskImagesAsync(int TaskId)
		{
			return TasksImagesDTONew.Map(await _unitOfWork.TaskImages.FindAllAsync(x => x.OneTimeId.Value == TaskId && !x.IsDeleted.Value));
		}

		public async Task<int> InsertTaskImageAsync(TasksImagesViewModel TaksImage)
		{
			var result = TasksImagesDTONew.Map(TaksImage);
			await _unitOfWork.TaskImages.AddAsync(result);
			await _unitOfWork.CompleteAsync();
			return result.Id;
		}

		public async Task<TasksImagesViewModel> UpdateTaskImageAsync(TasksImagesViewModel TaksImage)
		{
			var taskImage = await _unitOfWork.TaskImages.GetAsync(TaksImage.Id);

			taskImage.Id = TaksImage.Id;
			taskImage.RecurringId = TaksImage.RecurringId;
			taskImage.OneTimeId = TaksImage.OneTimeId;
			taskImage.PictureLink = TaksImage.PictureLink;
			taskImage.IsDeleted = TaksImage.IsDeleted;
			taskImage.ImageNote = TaksImage.ImageNote;
			taskImage.UpdateId = TaksImage.UpdateId;
			taskImage.FileName = TaksImage.FileName;
			await _unitOfWork.CompleteAsync();

			//await _unitOfWork.TaskImages.UpdateAsync(TasksImagesDTONew.Map(TaksImage));
			return TaksImage;
		}

		public async Task<TasksImagesViewModel> GetUpdateTaskImageAsync(int TaksImageId)
		{
			return TasksImagesDTONew.Map(await _unitOfWork.TaskImages.GetAsync(TaksImageId));
		}

		public async Task<TasksImagesViewModel> DeleteTaskImageAsync(TasksImagesViewModel TaksImage)
		{
			var result = TasksImagesDTONew.Map(TaksImage);
			await _unitOfWork.TaskImages.RemoveAsync(result);
			await _unitOfWork.CompleteAsync();
			return TaksImage;
		}

		public async Task<IEnumerable<TasksImagesViewModel>> GetRecurringTaskImagesCountAsync(int TaskId)
		{
			return TasksImagesDTONew.Map(await _unitOfWork.TaskImages.FindAllAsync(x => x.RecurringId.Value == TaskId && !x.IsDeleted.Value));
		}
		public async Task<IEnumerable<TasksImagesViewModel>> GetOneTimeTaskImagesCountAsync(int TaskId)
		{
			return TasksImagesDTONew.Map(await _unitOfWork.TaskImages.FindAllAsync(x => x.OneTimeId.Value == TaskId && !x.IsDeleted.Value));
		}

		public async Task<int> GetMaxRecurringId()
		{
			return await _unitOfWork.RecurringTasks.GetMaxRecurringId();
		}

		public async Task<IEnumerable<TasksImagesViewModel>> GetUpdatesImagesCountAsync(int UpdateId)
		{
			return TasksImagesDTONew.Map(await _unitOfWork.TaskImages.FindAllAsync(x => x.UpdateId == UpdateId && !x.IsDeleted.Value));
		}

		public async Task<TasksImagesViewModel> GetUpdatesImages(int UpdateId)
		{
			return TasksImagesDTONew.Map(await _unitOfWork.TaskImages.FindAsync(x => x.UpdateId == UpdateId && !x.IsDeleted.Value));
		}

		public async Task<IEnumerable<TasksImagesViewModel>> GetUpdatesImagesAsync(int UpdateId)
		{
			return TasksImagesDTONew.Map(await _unitOfWork.TaskImages.FindAllAsync(x => x.UpdateId == UpdateId && !x.IsDeleted.Value));
		}

		public async Task<IEnumerable<TasksRecTasksViewModel>> GetSubTasks(int TaskId)
		{
			return TasksRecTasksDTONew.Map(await _unitOfWork.RecurringTasks.GetSubTasks(TaskId));
		}

		public Task<List<IGrouping<string, TaskTask>>> GetActiveTasks()
		{
			return _unitOfWork.TaskTasks.GetActiveTasks();
		}

		public async Task<int> GetPastDueTasks()
		{
			return await _unitOfWork.TaskTasks.CountAsync(x => x.DateCompleted < DateTime.Today);
			//return _unitOfWork.RecurringTasks.GetPastDueTasks();
		}

		public async Task<int> GetRecurringTaskCount(Func<RecurringTask, bool> p)
		{
			return await _unitOfWork.RecurringTasks.GetRecurringTaskCount(p);
		}

		public async Task<int> GetRecurringTaskCountSync(Func<RecurringTask, bool> p)
		{
			return await _unitOfWork.RecurringTasks.GetRecurringTaskCount(p);
		}

		public IEnumerable<TasksRecTasksViewModel> GetRecurringTasks(int skip,
			int take, Func<RecurringTask, bool> p)
		{
			return TasksRecTasksDTONew.Map(_unitOfWork.RecurringTasks.GetRecurringTasks(skip, take, p));
		}

		public async Task<IEnumerable<TasksRecTasksViewModel>> GetRecurringTasks()
		{
			return TasksRecTasksDTONew.Map(await _unitOfWork.RecurringTasks.GetRecurringTasks());
		}

		public async Task<IEnumerable<TasksRecTasksViewModel>> GetRecurringTasksAsync(DateTime fromDateCompleted,
			DateTime toDateCompleted, bool isDelete, bool isDeactivated)
		{
			return TasksRecTasksDTONew.Map(await _unitOfWork.RecurringTasks.GetRecurringTasksAsync(fromDateCompleted,
				toDateCompleted, isDelete, isDeactivated));
		}

		public async Task<IEnumerable<TasksTaskUpdatesViewModel>> GetTaskUpdatesAsync(
			string ignoreSystemGeneret, string ignoreNudgudUpdate, string ignoreEmailUpdate,
			bool hasRecurringTaskValue, bool hasDueDateValue, int DueDateDuration, bool isDelete)
		{
			return TasksTaskUpdateDTONew.Map(await _unitOfWork.TaskUpdates.GetTaskUpdatesAsync(ignoreSystemGeneret,
			ignoreNudgudUpdate, ignoreEmailUpdate, hasRecurringTaskValue, hasDueDateValue, DueDateDuration, isDelete));
		}

		public async Task<IEnumerable<TasksTaskUpdatesViewModel>> GetTaskUpdatesForPercentageAsync(
		   string ignoreSystemGeneret, string ignoreNudgudUpdate, string ignoreEmailUpdate,
		   bool hasRecurringTaskValue, bool hasDueDateValue, int DueDateDuration, bool isDelete)
		{
			return TasksTaskUpdateDTONew.Map(await _unitOfWork.TaskUpdates.GetTaskUpdatesForPercentageAsync(ignoreSystemGeneret,
			ignoreNudgudUpdate, ignoreEmailUpdate, hasRecurringTaskValue, hasDueDateValue, DueDateDuration, isDelete));
		}

		public async Task<IEnumerable<TasksTaskUpdatesViewModel>> GetTaskUpdatesAsync(int taskId,
			string ignoreSystemGenerated, string ignoreNudgedUpdate, string ignoreEmailUpdate,
			DateTime fromUpdateDate, DateTime toUpdateDate, bool isDelete)
		{
			var result = await _unitOfWork.TaskUpdates.FindAllWithOrderByAscendingAsync(x => x.IsDeleted == isDelete
				&& !x.Update.Contains(ignoreNudgedUpdate)
				&& !x.Update.Contains(ignoreSystemGenerated)
				&& !x.Update.Contains(ignoreEmailUpdate)
				&& x.RecurringId == taskId
				&& (x.UpdateDate <= fromUpdateDate && x.UpdateDate >= toUpdateDate),
				x => x.UpdateId);
			return TasksTaskUpdateDTONew.Map(result);
		}

		public async Task<IEnumerable<TasksTaskUpdatesViewModel>> GetTaskUpdatesAsync(int recurringId,
			string ignoreNudgudUpdate, string ignoreEmailUpdate, bool isDeleted, bool hasDueDateValue,
			int DueDateDuration, bool isTaskCompleted)
		{
			return TasksTaskUpdateDTONew.Map(await _unitOfWork.TaskUpdates.GetTaskUpdatesAsync(recurringId, ignoreNudgudUpdate,
			ignoreEmailUpdate, isDeleted, hasDueDateValue, DueDateDuration, isTaskCompleted));
		}

		private SingleRecTaskPercentage SinglePercentageCalculation(IEnumerable<TasksTaskUpdatesViewModel> totalTaskUpdates,
			TasksFrequencyListViewModel frequency)
		{
			var calculationResult = new SingleRecTaskPercentage();

			if (!totalTaskUpdates.Any() || frequency == null)
				return calculationResult;

			try
			{
				foreach (var update in totalTaskUpdates)
				{
					calculationResult.dueDate += update.DueDate.ToString() + Environment.NewLine;
					calculationResult.updateDate += update.UpdateDate.ToString() + Environment.NewLine;

					var numberofMisses = 0;

					if (update.DueDate.HasValue && update.UpdateDate >= update.DueDate.Value)
					{
						var updateDateDifference = update.UpdateDate.Subtract(update.DueDate.Value);

						if (updateDateDifference.Days >= frequency.Days)
						{
							int result = (updateDateDifference.Days / frequency.Days);
							numberofMisses += result;
						}

						int resultOfTotalNumber = numberofMisses > 0 ? numberofMisses : 1;
						calculationResult.totalNumber += resultOfTotalNumber;
					}
					else
					{
						calculationResult.totalNumber++;
					}

					calculationResult.totalnumberofMisses += numberofMisses;
					calculationResult.numberOfMisses += Convert.ToString(numberofMisses) + Environment.NewLine;
					calculationResult.totalNumbers += numberofMisses > 0 ? Convert.ToString(numberofMisses) + Environment.NewLine : "1" + Environment.NewLine;
				}
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Rect task error:", ex);
			}

			return calculationResult;
		}

		private System.Data.DataTable CreateTableForRecTaskPercentage()
		{
			System.Data.DataTable table = new System.Data.DataTable("Percentage");

			table.Columns.Add("Task Id", System.Type.GetType("System.Int32"));
			table.Columns.Add("Frequency", System.Type.GetType("System.String"));
			table.Columns.Add("Task Completion Dates", System.Type.GetType("System.String"));
			table.Columns.Add("Task Due Dates", System.Type.GetType("System.String"));
			table.Columns.Add("Number of Misses", System.Type.GetType("System.String"));
			table.Columns.Add("Total Number", System.Type.GetType("System.String"));
			table.Columns.Add("On Time Percentage", System.Type.GetType("System.String"));
			table.Columns.Add("Overall On Time Percentage", System.Type.GetType("System.String"));

			return table;
		}

		private DataRow RecTaskPercentageCalculationForRow(System.Data.DataTable table, TasksRecTasksViewModel task,
			TasksFrequencyListViewModel frequency, IEnumerable<TasksTaskUpdatesViewModel> totalTaskUpdates)
		{
			DataRow row = table.NewRow();

			if (table == null || task == null || frequency == null || !totalTaskUpdates.Any())
				return row;

			try
			{
				row["Task Id"] = task.Id;
				row["Frequency"] = task.Frequency;

				int totalNumber = 0;
				int totalnumberofMisses = 0;

				string dueDate = "";
				string updateDate = "";
				string numberOfMisses = "";
				string totalNumbers = "";

				SingleRecTaskPercentage calculationResult = SinglePercentageCalculation(totalTaskUpdates, frequency);

				dueDate = calculationResult.dueDate;
				updateDate = calculationResult.updateDate;
				numberOfMisses = calculationResult.numberOfMisses;
				totalNumbers = calculationResult.totalNumbers;
				totalNumber += calculationResult.totalNumber;
				totalnumberofMisses += calculationResult.totalnumberofMisses;

				if (!string.IsNullOrEmpty(dueDate))
					dueDate = dueDate.Remove(dueDate.Length - 2);

				if (!string.IsNullOrEmpty(updateDate))
					updateDate = updateDate.Remove(updateDate.Length - 2);

				if (!string.IsNullOrEmpty(numberOfMisses))
					numberOfMisses = numberOfMisses.Remove(numberOfMisses.Length - 2);

				if (!string.IsNullOrEmpty(totalNumbers))
					totalNumbers = totalNumbers.Remove(totalNumbers.Length - 2);

				row["Task Due Dates"] = dueDate;
				row["Task Completion Dates"] = updateDate;
				row["Number of Misses"] = numberOfMisses;
				row["Total Number"] = totalNumbers;

				if (totalNumber > 0)
				{
					double resultOfTaskPercentage = 1.00 - (Convert.ToDouble(totalnumberofMisses) / Convert.ToDouble(totalNumber));
					task.TaskPercentage = resultOfTaskPercentage;

					row["On Time Percentage"] = Convert.ToString(resultOfTaskPercentage);
				}
				else
				{
					task.TaskPercentage = 0;

					row["On Time Percentage"] = Convert.ToString(0);
				}

				task.NumberOfMisses = totalnumberofMisses;
				task.TotalNumber = totalNumber;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Rect task error:", ex);
			}

			return row;
		}

		public System.Data.DataTable PercentageCalculation(List<TasksRecTasksViewModel> tasksList,
			List<TasksTaskUpdatesViewModel> updateTaskList, List<TasksFrequencyListViewModel> frequenceList,
			int dateDifference)
		{
			System.Data.DataTable table = CreateTableForRecTaskPercentage();

			if (!tasksList.Any() || !updateTaskList.Any() || !frequenceList.Any())
				return table;

			try
			{
				List<TasksRecTasksViewModel> totalTasks = new();

				if (dateDifference == 0)
				{
					totalTasks = new(tasksList);
				}
				else
				{
					totalTasks = tasksList.Where(x => x.DateCompleted > DateTime.Today.AddMonths(dateDifference)).ToList();
				}

				var totaltasksIds = totalTasks.Select(x => x.Id).ToList();
				var totalUpdates = updateTaskList.Where(x => totaltasksIds.Contains(x.RecurringID.Value));

				foreach (var task in totalTasks)
				{
					var frequency = frequenceList.FirstOrDefault(f => f.Frequency == task.Frequency);
					var totalTaskUpdates = totalUpdates.Where(x => x.RecurringID == task.Id).OrderBy(a => a.UpdateDate);
					table.Rows.Add(RecTaskPercentageCalculationForRow(table, task, frequency, totalTaskUpdates));
				}

				double totalPercentage = 1 - (totalTasks.Sum(x => x.NumberOfMisses) / totalTasks.Sum(x => x.TotalNumber));
				totalPercentage = double.IsNaN(totalPercentage) ? 0 : (totalPercentage * 100);

				totalPercentage = Convert.ToInt32(totalPercentage);

				DataRow finalRow = table.NewRow();
				finalRow["Overall On Time Percentage"] = totalPercentage;
				table.Rows.Add(finalRow);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Rect task error:", ex);
			}

			return table;
		}

		public System.Data.DataTable RecTaskUpdatePercentageCalculation(TasksRecTasksViewModel task,
			IEnumerable<TasksTaskUpdatesViewModel> updateTaskList,
			TasksFrequencyListViewModel frequency)
		{
			System.Data.DataTable table = CreateTableForRecTaskPercentage();

			if (task == null || !updateTaskList.Any() || frequency == null)
				return table;

			try
			{
				table.Rows.Add(RecTaskPercentageCalculationForRow(table, task, frequency, updateTaskList));

				double totalPercentage = 1 - (task.NumberOfMisses / task.TotalNumber);
				totalPercentage = double.IsNaN(totalPercentage) ? 0 : (totalPercentage * 100);

				totalPercentage = Convert.ToInt32(totalPercentage);

				DataRow finalRow = table.NewRow();
				finalRow["Overall On Time Percentage"] = totalPercentage;
				table.Rows.Add(finalRow);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Rect task error:", ex);
			}

			return table;
		}

		public int RecurringUpcommingDate(TasksFrequencyListViewModel taskFrequency)
		{
			int frequencyDay = 0;

			if (taskFrequency != null)
			{
				if (taskFrequency.Frequency.ToUpper() == "MON-WED-FRI")
				{
					string dayName = DateTime.Today.DayOfWeek.ToString();
					switch (dayName)
					{
						case "Saturday":
							frequencyDay = 2;
							break;
						case "Sunday":
							frequencyDay = 1;
							break;
						case "Monday":
							frequencyDay = 2;
							break;
						case "Tuesday":
							frequencyDay = 1;
							break;
						case "Wednesday":
							frequencyDay = 2;
							break;
						case "Thursday":
							frequencyDay = 1;
							break;
						case "Friday":
							frequencyDay = 3;
							break;
					}
				}
				else
				{
					frequencyDay = (int)taskFrequency.Days;
				}
			}

			return frequencyDay;
		}

		public DateTime RecurringUpcommingDate(DateTime date)
		{
			DateTime updateDate = date;
			string dayName = date.DayOfWeek.ToString();

			switch (dayName)
			{
				case "Saturday":
					updateDate = date.AddDays(2);
					break;
				case "Sunday":
					updateDate = date.AddDays(1);
					break;
			}

			return updateDate;
		}

		private int Day(DayOfWeek dayOfWeek)
		{
			int days = 0;

			switch (dayOfWeek)
			{
				case DayOfWeek.Sunday:
					days = (int)dayOfWeek;
					break;
				case DayOfWeek.Monday:
					days = (int)dayOfWeek;
					break;
				case DayOfWeek.Tuesday:
					days = (int)dayOfWeek;
					break;
				case DayOfWeek.Wednesday:
					days = (int)dayOfWeek;
					break;
				case DayOfWeek.Thursday:
					days = (int)dayOfWeek;
					break;
				case DayOfWeek.Friday:
					days = (int)dayOfWeek;
					break;
				case DayOfWeek.Saturday:
					days = (int)dayOfWeek;
					break;
				default:
					days = 0;
					break;
			}

			return days;
		}

		private DateTime RecurringUpcommingDate(int dayOfWeek, DateTime dueDate)
		{
			DateTime date = dueDate;
			int incDayOfWeek = 0;

			while (dayOfWeek != incDayOfWeek)
			{
				date = date.AddDays(1);
				incDayOfWeek = Day(date.DayOfWeek);
			}

			return date;
		}

		private TasksRecTasksViewModel RecurringUpcommingDate(TasksRecTasksViewModel task)
		{
			if (string.IsNullOrEmpty(task.StartDueDateDay) && string.IsNullOrEmpty(task.EndDueDateDay))
				return task;

			DateTime date = new DateTime();

			try
			{
				TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;

				if (task.DateCompleted == null) return task;

				int upcomminDays = Day(task.DateCompleted.Value.Date.DayOfWeek);

				int startDays = Day((DayOfWeek)Enum.Parse(typeof(DayOfWeek),
						textInfo.ToTitleCase(task.StartDueDateDay.ToLower())));

				int endDays = Day((DayOfWeek)Enum.Parse(typeof(DayOfWeek),
						textInfo.ToTitleCase(task.EndDueDateDay.ToLower())));

				if (upcomminDays < startDays)
				{
					date = RecurringUpcommingDate(startDays, task.DateCompleted.Value);
				}
				else if (upcomminDays >= endDays)
				{
					date = RecurringUpcommingDate(startDays, task.DateCompleted.Value);
				}
				else
				{
					date = RecurringUpcommingDate(endDays, task.DateCompleted.Value);
				}

				task.UpcomingDate = date;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "rec task upcomming error:", ex);
			}

			return task;
		}

		public TasksRecTasksViewModel RecurringUpcommingDate(TasksRecTasksViewModel recurringTask,
			TasksFrequencyListViewModel frequency, TasksTaskUpdatesViewModel rectUpdate)
		{
			if (frequency != null && frequency.Frequency == "TWICE/WEEK"
				&& !string.IsNullOrEmpty(recurringTask.StartDueDateDay) && !string.IsNullOrEmpty(recurringTask.EndDueDateDay))
			{
				recurringTask = RecurringUpcommingDate(recurringTask);
			}
			else
			{
				int days = RecurringUpcommingDate(frequency);

				if (frequency.Frequency == "Twice/Day")
				{
					var numberOfUpdate = recurringTask.TasksTaskUpdates.Where(x => x.UpdateDate.Date == rectUpdate.UpdateDate.Date).Count();
					if (numberOfUpdate == 1)
					{
						days = 1;
					}
				}
				else if (frequency.Frequency == "Three/Day")
				{
					var numberOfUpdate = recurringTask.TasksTaskUpdates.Where(x => x.UpdateDate.Date == rectUpdate.UpdateDate.Date).Count();
					if (numberOfUpdate == 2)
					{
						days = 1;
					}
				}

				if (days > 0)
				{
					recurringTask.UpcomingDate = recurringTask.DateCompleted?.Date.AddDays(days);
					recurringTask.UpcomingDate = RecurringUpcommingDate(recurringTask.UpcomingDate.Value);
				}
			}

			return recurringTask;
		}

		public async Task<List<TasksTaskUpdatesViewModel>> RecTaskGraphTrendLineCalculation(int monthDuration,
			List<TasksTaskUpdatesViewModel> graphUpdates)
		{
			List<TasksTaskUpdatesViewModel> GraphTrendLineUpdates = new List<TasksTaskUpdatesViewModel>();

			if (!graphUpdates.Any())
			{
				return GraphTrendLineUpdates;
			}
			DateTime fristTrendLineUpdateDate = graphUpdates.First().UpdateDate;
			bool isTrue = true;
			int eightUpdatesForTrendline = 8;
			if (graphUpdates.Count < eightUpdatesForTrendline)
			{
				GraphTrendLineUpdates.Add(new TasksTaskUpdatesViewModel
				{
					GraphNumber = 0,
					UpdateDate = fristTrendLineUpdateDate,
				});
				return GraphTrendLineUpdates;
			}

			decimal averageGaphValue = 0;
			for (int i = 0; i <= graphUpdates.Count - eightUpdatesForTrendline; i++)
			{
				var data = graphUpdates.Skip(i).Take(eightUpdatesForTrendline).ToList();
				if (data.Any() && data.Count >= 8)
				{
					decimal sum = data.Sum(x => x.GraphNumber);
					int dataLength = data.Count;

					if (dataLength > 0)
					{
						averageGaphValue = sum / dataLength;
					}
					else
					{
						averageGaphValue = 0;
					}

					GraphTrendLineUpdates.Add(new TasksTaskUpdatesViewModel
					{
						GraphNumber = averageGaphValue,
						UpdateDate = data.Last().UpdateDate,
					});
				}
				else
				{
					await Task.Run(() => isTrue = false);
				}

			}
			return GraphTrendLineUpdates;
		}

		public async Task<TasksTaskUpdatesViewModel> SaveCaptureImageAsync(Dictionary<Guid, string> capturedImgaesList,
			int recurringTaskId, TasksTaskUpdatesViewModel recurringTaskUpdate)
		{
			if (capturedImgaesList != null && capturedImgaesList.Any())
			{
				foreach (var Img in capturedImgaesList)
				{
					var Base64Image = Img.Value.Replace("data:image/jpeg;base64,", "");
					byte[] ImgBytes = Convert.FromBase64String(Base64Image);

					var Imgstream = new MemoryStream(ImgBytes);

					var ImageFileName = $"{Guid.NewGuid()}.jpeg";
					var ResultPath = _fileManagerService.CreateRecurringTaskDirectory(recurringTaskId.ToString()) + ImageFileName;
					_fileManagerService.WriteToFile(Imgstream, ResultPath);
					recurringTaskUpdate.PictureLink = ResultPath;
					recurringTaskUpdate.IsPicture = true;

					TasksImagesViewModel TM = new TasksImagesViewModel()
					{
						UpdateId = recurringTaskUpdate.UpdateId,
						PictureLink = ResultPath,
						IsDeleted = false,
						OneTimeId = 0,
						RecurringId = 0,
					};
					await InsertTaskImageAsync(TM);
				}
			}

			return recurringTaskUpdate;
		}
                public async Task<EmailQueueViewModel> GetEmailByEmailIdAsync(int EmailId)
                {
                        await Task.CompletedTask;
                        return new EmailQueueViewModel();
                }

		public async Task<bool> SendEmail(TasksRecTasksViewModel newTask, bool IsEditing,
		 string url, string emailSubject)
		{
			StringBuilder customBody = new();
			customBody.Append($"<br><label style=\"font-weight:bold\"> TASK ID: </label><strong>" + newTask.Id + "</strong>");
			//check email for edit task or new task.
			if (IsEditing)
			{
				Dictionary<string, string> taskProperty = new Dictionary<string, string>()
		{
			{"Description", "Description"},
			{"PersonResponsible", "Person Responsible"},
			{"Initiator", "Initiator"},
			{"StartDate", "StartDate"},
			{"Frequency", "Frequency"},
			{"IsGraphRequired", "Is Graph Required"},
			{"IsPassOrFail", "Is Pass Or Fail Required"},
			{"IsPicRequired", "Is Pic Required"},
			{"IsQuestionRequired", "Does A Question Need To Be Answered"},
			{"IsDescriptionMandatory", "Is Description Mandatory"},
			{"IsAuditRequired", "Are Names That Faild The Audit Required"},
			{"IsHandDeliveredRequired", " Hand Delivered Document Needed To Distribution List"},
			{"IsProtected", "Password Protected"},
			{"IsPositionSpecific", "Position Specific"},
			{"JobTitle", "Job Title"},
			{"TaskDescriptionSubject", "Task Description Subject"},
			{"TaskArea", "Area"},
			{"AuditPerson", "Auditor"},
		};

				TasksRecTasksViewModel oldTask = GetRecurringTaskByIdSync(newTask.Id);

				//check old task and new task change value.
				foreach (var item in taskProperty)
				{
					var newValue = oldTask.GetType().GetProperty(item.Key).GetValue(oldTask);
					var oldValue = newTask.GetType().GetProperty(item.Key).GetValue(newTask);

					if (newValue != null && oldValue != null)
					{
						string oldValueString = newValue.ToString();
						string newValueString = oldValue.ToString();

						if (newValueString != null && oldValueString != null)
						{
							if (newValueString.ToUpper() != oldValueString.ToUpper())
							{
								customBody.Append($"<br><label style=\"font-weight:bold\"> FIELD CHANGED: </label>" + item.Value);
								customBody.Append($"<br><label style=\"font-weight:bold\"> OLD VALUE: </label>" + oldTask.GetType().GetProperty(item.Key).GetValue(oldTask));
								customBody.Append($"<br><label style=\"font-weight:bold\"> NEW VALUE: </label>" + newTask.GetType().GetProperty(item.Key).GetValue(newTask));
								customBody.Append($"<br><label style=\"font-weight:bold\"> CHANGED BY: </label>" + newTask.EditBy);

								await _loggingChangeLogService.InsertAsync(new LoggingChangeLogViewModel
								{
									RecordType = "CAMCO TASKS",
									RecordId = oldTask.Id,
									RecordField = item.Value,
									OldValue = oldValueString.ToUpper(),
									NewValue = newValueString.ToUpper(),
									UpdateDate = DateTime.Now,
									UpdatedBy = newTask.EditBy
								});
							}
						}
					}
				}
				customBody.Append($"<br><label style=\"font-weight:bold\"> Link: </label> <a href=\"" + url + "viewrecurringtasks/OpenTask/" + newTask.Id.ToString() + "\" target=\"_blank\">" + url + "viewrecurringtasks/OpenTask/" + newTask.Id.ToString() + "</a>");

			}
			else
			{
				customBody.Append($"<br><label style=\"font-weight:bold\"> DESCRIPTION: </label>" + newTask.Description.ToUpper());
				customBody.Append($"<br><label style=\"font-weight:bold\"> UPCOMING DATE: </label>" + newTask.UpcomingDate);
				customBody.Append($"<br><label style=\"font-weight:bold\"> FREQUENCY: </label>" + newTask.Frequency);
				customBody.Append($"<br><label style=\"font-weight:bold\"> INITIATOR: </label>" + newTask.Initiator);
				customBody.Append($"<br><label style=\"font-weight:bold\"> PERSON RESPONSIBLE: </label>" + newTask.PersonResponsible);
				customBody.Append($"<br><label style=\"font-weight:bold\"> Link: </label> <a href=\"" + url + "viewrecurringtasks/OpenTask/" + newTask.Id.ToString() + "\" target=\"_blank\">" + url + "viewrecurringtasks/OpenTask/" + newTask.Id.ToString() + "</a>");
			}

			var taskImages = (await GetRecurringTaskImagesAsync(newTask.Id)).ToList();

			//add task attach file in email.
			string taskAttachements = "";
			if (!string.IsNullOrEmpty(newTask.InstructionFileLink))
			{
				taskAttachements += newTask.InstructionFileLink + ":";
			}

			if (taskImages.Any())
			{
				foreach (var item in taskImages)
				{
					string attach = _fileManagerService.FilePathForEmail(item);

					if (!string.IsNullOrEmpty(attach))
					{
						taskAttachements += attach + ":";
					}
					else
					{
						taskAttachements += item.PictureLink + ":";
					}
				}
			}

			if (!string.IsNullOrEmpty(taskAttachements))
				taskAttachements = taskAttachements.Remove(taskAttachements.Length - 1);

			//email subject.
			string emailSendTo = string.Empty;
			EmployeeViewModel singleEmployee = _employeeService.GetEmployeeSync(newTask.Initiator);

			if (singleEmployee != null)
			{
				emailSendTo = $"{singleEmployee.Email};";
			}

			singleEmployee = _employeeService.GetEmployeeSync(newTask.PersonResponsible);

			if (singleEmployee != null)
			{
				emailSendTo = $"{emailSendTo}{singleEmployee.Email};";
			}

			singleEmployee = _employeeService.GetEmployeeSync(newTask.EditBy);

			if (singleEmployee != null)
			{
				emailSendTo = $"{emailSendTo}{singleEmployee.Email};";
			}


			if (!string.IsNullOrEmpty(newTask.EmailsList))
			{
				emailSendTo = $"{emailSendTo}{newTask.EmailsList};";
			}

			if (!string.IsNullOrEmpty(newTask.EmailsListJobId))
			{
				List<long> jobIds = newTask.EmailsListJobId.Split(';').Select(j => long.Parse(j)).ToList();
				IEnumerable<EmployeeViewModel> employees = await _employeeService.GetListAsync(true, false);
				foreach (long jobId in jobIds)
				{
					List<long> employeeIds = await _jobDescriptionsService.GetJobDescriptionWithEmployeesAsync(jobId);
					List<EmployeeViewModel> filteredEmployees = employees.Where(employee => employee.JobId == jobId || employeeIds.Contains(employee.Id)).ToList();
					if (!filteredEmployees.Any(employee => !string.IsNullOrEmpty(employee.Email) && emailSendTo.Contains(employee.Email)))
					{
						foreach (var employee in filteredEmployees)
						{
							if (!string.IsNullOrEmpty(employee.Email) && !emailSendTo.Contains(employee.Email))
							{
								emailSendTo = $"{emailSendTo}{employee.Email};";
							}
						}
					}
				}
			}

			if (!string.IsNullOrEmpty(emailSendTo))
				emailSendTo = emailSendTo.Remove(emailSendTo.Length - 1);

			string body = EmailDefaults.GenerateEmailTemplate("Tasks", customBody.ToString());
			if (!IsEditing)
			{
				await _emailService.SendEmailAsync(IsEditing ? EmailTypes.ActionBasedRecurringTaskReadyForReview : EmailTypes.ActionBasedRecurringTaskCreated,
					Array.Empty<string>(), "A NEW TASKS NEEDS TO BE REVIEWED", body, taskAttachements, new string[] { "rarnold@camcomfginc.com", "trinity.purdy@camcomfginc.com" });
			}

			//send email.
			if (!string.IsNullOrEmpty(emailSendTo))
			{
				try
				{
					await _emailService.SendEmailAsync(IsEditing ? EmailTypes.ActionBasedRecurringTaskUpdated : EmailTypes.ActionBasedRecurringTaskCreated,
						Array.Empty<string>(), emailSubject, body, taskAttachements, emailSendTo.Split(';').ToArray());
					return true;
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex);
					return true;
				}
			}

			return false;
		}
		public async Task<bool> DeclineTaskEmail(TasksRecTasksViewModel recTask, string note, string emailSubject, bool isInquiry)
		{
			var deniedBy = await _employeeService.GetByEmployeeIdAsync(recTask.ApprovedByEmployeeId.Value);
			StringBuilder customBody = new();
			customBody.Append($"<br><label style=\"font-weight:bold\"> TASK ID: </label><strong>" + recTask.Id + "</strong>");
			customBody.Append($"<br><label style=\"font-weight:bold\"> DESCRIPTION: </label>" + recTask.Description.ToUpper());
			customBody.Append($"<br><label style=\"font-weight:bold\"> UPCOMING DATE: </label>" + recTask.UpcomingDate);
			customBody.Append($"<br><label style=\"font-weight:bold\"> FREQUENCY: </label>" + recTask.Frequency);
			customBody.Append($"<br><label style=\"font-weight:bold\"> INITIATOR: </label>" + recTask.Initiator);
			customBody.Append($"<br><label style=\"font-weight:bold\"> PERSON RESPONSIBLE: </label>" + recTask.PersonResponsible);
			if (note == null)
			{
				customBody.Append($"<br><label style=\"font-weight:bold\"> DENIED BY: </label>" + deniedBy.FullName);
			}
			else
			{
				if (isInquiry)
				{
					customBody.Append($"<br><label style=\"font-weight:bold\"> INQUISITION BY: </label>" + deniedBy.FullName);
					customBody.Append($"<br><label style=\"font-weight:bold\"> INQUIRY: </label><strong>" + note.ToUpper() + "</strong>");
				}
				else
				{
					customBody.Append($"<br><label style=\"font-weight:bold\"> DENIED BY: </label>" + deniedBy.FullName);
					customBody.Append($"<br><label style=\"font-weight:bold\"> NOTE: </label><strong>" + note.ToUpper() + "</strong>");
				}
			}

			string emailSendTo = string.Empty;
			EmployeeViewModel singleEmployee = _employeeService.GetEmployeeSync(recTask.Initiator);

			if (singleEmployee != null)
			{
				emailSendTo = $"{singleEmployee.Email};";
			}

			if (deniedBy != null)
			{
				emailSendTo = $"{emailSendTo}{deniedBy.Email};";
			}

			if (!string.IsNullOrEmpty(emailSendTo))
				emailSendTo = emailSendTo.Remove(emailSendTo.Length - 1);
			string body = EmailDefaults.GenerateEmailTemplate("Tasks", customBody.ToString());
			//send email.
			if (!string.IsNullOrEmpty(emailSendTo))
			{
				try
				{
					await _emailService.SendEmailAsync(EmailTypes.ActionBasedRecurringTaskDeclined, Array.Empty<string>(), emailSubject, body, null, emailSendTo.Split(';').ToArray());
					return true;
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex);
					return true;
				}
			}

			return false;
		}
		public async Task DeleteTaskAsync(IEnumerable<int> taskIds)
		{
			if (taskIds == null || !taskIds.Any())
				return;
			
			foreach (var taskId in taskIds)
			{
				var task = await _unitOfWork.TaskTasks.GetAsync(taskId);
				if (task == null)
					continue;
				task.IsDeleted = true;
			}
			await _unitOfWork.CommitAsync();
		}
		public async Task<TasksTasksViewModel?> GetTaskByIdAsync(int taskId)
		{
			var selectTaskId = await _unitOfWork.TaskTasks.GetAsync(taskId);        
			if (selectTaskId is null) return null;
			return TasksTasksDTONew.Map(selectTaskId);
		}

		public async Task<bool> UpdateTaskCostingCodeAsync(int taskId, int? costingCode)
		{
			try
			{
				var entity = await _unitOfWork.TaskTasks.GetAsync(taskId);
				if (entity == null)
					return false;
				entity.CostingCode = costingCode;
				await _unitOfWork.CommitAsync();

				return true;
			}
			catch (Exception ex)
			{
				_logger.LogError(
					ex,
					"Failed to update costing code for task {TaskId} to {CostingCode}",
					taskId, costingCode);
				return false;
			}
		}
    }
}
