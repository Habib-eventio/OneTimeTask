namespace CamcoTasks.Accessory.AutomationJobTask.IService
{
    public interface IDeactiveRecurringTasksService
    {
        Task DoWork(CancellationToken stoppingToken);
    }
}
