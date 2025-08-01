using CamcoTasks.Infrastructure.Entities.TaskInfo;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CamcoTasks.ViewModels.TasksTasksDTO
{
    public class TasksTasksDTONew
    {
        public static TaskTask Map(TasksTasksViewModel viewModel)
        {
            if (viewModel == null) { return null; }

            return new TaskTask
            {
                IsDeleted = viewModel.IsDeleted,
                TaskType = viewModel.TaskType,
                Update = viewModel.Update,
                TaskId = viewModel.TaskId,
                PictureLink = viewModel.PictureLink,
                DateAdded = viewModel.DateAdded,
                DateCompleted = viewModel.DateCompleted,
                Description = viewModel.Description,
                Id = viewModel.Id,
                PersonResponsible = viewModel.PersonResponsible,
                Initiator = viewModel.Initiator,
                IsReviewed = viewModel.IsReviewed,
                Priority = viewModel.Priority,
                Progress = viewModel.Progress,
                FileLink = viewModel.FileLink,
                NudgeCount = viewModel.NudgeCount,
                EmailCount = viewModel.EmailCount,
                Department = viewModel.Department,
                ParentTaskId = viewModel.ParentTaskId,
                UpcomingDate = viewModel.UpcomingDate,
                StartDate = viewModel.StartDate,
                StatusTypeId = viewModel.TaskStatusId,
				CostingCode=viewModel.CostingCode,
                Summary=viewModel.Summary,
                LastUpdate=viewModel.LastUpdate,
                DueDate=viewModel.DueDate
			};
        }
        
        public static TasksTasksViewModel Map(TaskTask dataEntity)
        {
            if (dataEntity == null) { return null; }

            return new TasksTasksViewModel
            {
                IsDeleted = dataEntity.IsDeleted,
                TaskType = dataEntity.TaskType,
                Update = dataEntity.Update,
                TaskId = dataEntity.TaskId,
                PictureLink = dataEntity.PictureLink,
                DateAdded = dataEntity.DateAdded,
                DateCompleted = dataEntity.DateCompleted,
                Description = dataEntity.Description,
                Id = dataEntity.Id,
                PersonResponsible = dataEntity.PersonResponsible,
                Initiator = dataEntity.Initiator,
                IsReviewed = dataEntity.IsReviewed,
                Priority = dataEntity.Priority,
                Progress = dataEntity.Progress,
                FileLink = dataEntity.FileLink,
                NudgeCount = dataEntity.NudgeCount,
                EmailCount = dataEntity.EmailCount,
                Department = dataEntity.Department,
                ParentTaskId = dataEntity.ParentTaskId,
                UpcomingDate = dataEntity.UpcomingDate,
                StartDate = dataEntity.StartDate,
				TaskStatusId=dataEntity.StatusTypeId,
				CostingCode=dataEntity.CostingCode,
				Summary=dataEntity.Summary,
                LastUpdate=dataEntity.LastUpdate,
                DueDate=dataEntity.DueDate,
			};
        }

        public static IEnumerable<TasksTasksViewModel> Map(IEnumerable<TaskTask> dataEntityList)
        {
            if (dataEntityList == null) { yield break; }

            foreach (var item in dataEntityList)
            {
                yield return Map(item);
            }
        }
    }
}
