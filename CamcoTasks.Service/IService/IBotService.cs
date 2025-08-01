using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CamcoTasks.Service.IService
{
	public interface IBotService
	{
		Task<string> SendMessageAsync(string userMessage, CancellationToken ct = default);
	}
}
