using CamcoTasks.Infrastructure.Entities.TaskInfo;
using System.Collections.Generic;

namespace CamcoTasks.ViewModels.TasksFrequencyListDTO
{
    public class TasksFrequencyListDTONew
    {
        public static FrequencyList Map(TasksFrequencyListViewModel viewModel)
        {
            if (viewModel == null) { return null; }

            return new FrequencyList
            {
                Days = viewModel.Days,
                Frequency = viewModel.Frequency,
                Id = viewModel.Id,
            };
        }

        public static TasksFrequencyListViewModel Map(FrequencyList dataEntity)
        {
            if (dataEntity == null) { return null; }

            return new TasksFrequencyListViewModel
            {
                Days = dataEntity.Days,
                Frequency = dataEntity.Frequency,
                Id = dataEntity.Id,
            };
        }

        public static IEnumerable<TasksFrequencyListViewModel> Map(IEnumerable<FrequencyList> dataEntityList)
        {
            if (dataEntityList == null) { yield break; }
            foreach (var item in dataEntityList)
            {
                yield return Map(item);
            }
        }
    }
}
