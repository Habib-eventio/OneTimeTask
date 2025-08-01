using CamcoTasks.Infrastructure.Entities.TaskInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CamcoTasks.ViewModels.TaskChangeLogDTO
{
	public class TaskChangeLogDTONew
	{
		public static TaskChangeLog Map(TaskChangeLogViewModel viewModel)
		{
			if (viewModel == null) { return null; }
			return new TaskChangeLog
			{
				TaskId = viewModel.TaskId,
				DateModified = viewModel.DateModified,
				Action = viewModel.Action,
				IPAddress = viewModel.IPAddress,
				Id = viewModel.Id
			};
		}
		public static TaskChangeLogViewModel Map(TaskChangeLog dataEntity)
		{
			if (dataEntity == null) { return null; }
			return new TaskChangeLogViewModel
			{
				TaskId = dataEntity.TaskId,
				DateModified = dataEntity.DateModified,
				Action = dataEntity.Action,
				IPAddress = dataEntity.IPAddress,
				Id = dataEntity.Id
			};
		}
		public static IEnumerable<TaskChangeLogViewModel> Map(IEnumerable<TaskChangeLog> dataEntityList)
		{
			if (dataEntityList == null) { yield break; }
			foreach (var item in dataEntityList)
			{
				yield return Map(item);
			}
		}
	}
}
