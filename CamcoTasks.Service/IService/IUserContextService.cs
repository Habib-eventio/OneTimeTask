using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CamcoTasks.Service.IService
{
	public interface IUserContextService
	{
		int CurrentEmployeeId { get; set; }
	}
}
