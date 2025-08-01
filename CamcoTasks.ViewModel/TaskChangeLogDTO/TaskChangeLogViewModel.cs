using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CamcoTasks.ViewModels.TaskChangeLogDTO
{
	public class TaskChangeLogViewModel
	{
		public int Id { get; set; }
		public int TaskId { get; set; }
		public string Action { get; set; }
		public string ChangeDetails { get; set; }
		public DateTime DateModified { get; set; }
		public string IPAddress { get; set; }
	}
}
