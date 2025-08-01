namespace CamcoTasks.Accessory.AutomationJob.IService
{
    public interface IDeactiveRecurringTaskReportService
    {
        Task DoWork(CancellationToken stoppingToken);
    }
}
