namespace CamcoTasks.Accessory.AutomationJobTask.IService
{
    public interface IDueRecurringTaskService
    {
        Task DoWork(CancellationToken stoppingToken);
    }
}
