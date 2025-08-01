using CamcoTasks.Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CamcoTasks.Service.Service
{
	public class UserContextService: IUserContextService
	{
		public int CurrentEmployeeId { get; set; }
	}
}
