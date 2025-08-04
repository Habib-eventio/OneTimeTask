using System;
using System.Threading.Tasks;

namespace CamcoTasks.Services
{
    public class TaskStateService
    {
        public event Func<Task>? StateChanged;

        public Task NotifyStateChangedAsync()
        {
            return StateChanged?.Invoke() ?? Task.CompletedTask;
        }
    }
}
