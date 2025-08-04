using System;

namespace CamcoTasks.Services
{
    public class TaskStateService
    {
        public event Action? OnChange;

        public void NotifyStateChanged()
        {
            OnChange?.Invoke();
        }
    }
}
