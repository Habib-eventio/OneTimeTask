using CamcoTasks.Infrastructure.Entities.TaskInfo;
using System.Collections.Generic;

namespace CamcoTasks.ViewModels.TasksTasksTaskTypeDTO
{
    public class TasksTasksTaskTypeDTONew
    {
        public static TaskTaskType Map(TasksTasksTaskTypeViewModel viewModel)
        {
            if (viewModel == null) { return null; }

            return new TaskTaskType
            {
                TaskType = viewModel.TaskType,
                Id = (int)viewModel.Id,
                Email = viewModel.Email,
                Email2 = viewModel.Email2,
                CreatedAt=viewModel.CreatedAt
            };
        }

        public static TasksTasksTaskTypeViewModel Map(TaskTaskType dataEntity)
        {
            if (dataEntity == null) { return null; }

            return new TasksTasksTaskTypeViewModel
            {
                TaskType = dataEntity.TaskType,
                Id = dataEntity.Id,
                Email = dataEntity.Email,
                Email2 = dataEntity.Email2,
                CreatedAt=dataEntity.CreatedAt
            };
        }

        public static IEnumerable<TasksTasksTaskTypeViewModel> Map(IEnumerable<TaskTaskType> dataEntityList)
        {
            if (dataEntityList == null) { yield break; }
            foreach (var item in dataEntityList)
            {
                yield return Map(item);
            }
        }
    }
}
