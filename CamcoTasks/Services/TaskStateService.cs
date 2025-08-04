using System;
using System.Threading.Tasks;

namespace CamcoTasks.Services
{
    public class TaskStateService
    {
        public event Func<Task>? OnChange;

        public Task NotifyStateChanged()
        {
            return OnChange?.Invoke() ?? Task.CompletedTask;
        public event Action? OnChange;

        public void NotifyStateChanged()
        {
            OnChange?.Invoke();
        }
    }
}
