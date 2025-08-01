namespace CamcoTasks.Accessory.AutomationJobTask.IService
{
    public interface IRecurringTaskAuditReportService
    {
        Task DoWork(CancellationToken stoppingToken);
    }
}
