using CamcoTasks.Infrastructure.EnumHelper.Enums.Task;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace CamcoTasks.Helpers
{
	public class StatusDisplay
	{
		public  int GetStatusTypeId(string taskStatus)
		{
			if (string.IsNullOrEmpty(taskStatus))
			{
				return (int)StatusType.Default;
			}
			taskStatus = taskStatus.Trim();

			foreach (var field in typeof(StatusType).GetFields())
			{
				var attribute = field.GetCustomAttribute<DisplayAttribute>();
				if (attribute != null && attribute.Name.Equals(taskStatus, StringComparison.OrdinalIgnoreCase))
				{
					return (int)Enum.Parse(typeof(StatusType), field.Name);
				}
			}

			return (int)StatusType.Default;
		}

		public string GetStatusName(int? statusTypeId)
		{
			if (!statusTypeId.HasValue)
			{
				return "Default";
			}

			if (Enum.IsDefined(typeof(StatusType), statusTypeId.Value))
			{
				var statusType = (StatusType)statusTypeId.Value;
				var attribute = statusType.GetType()
					.GetField(statusType.ToString())
					.GetCustomAttribute<DisplayAttribute>();

				return attribute?.Name ?? statusType.ToString();
			}

			return "Unknown";
		}
	}
}
