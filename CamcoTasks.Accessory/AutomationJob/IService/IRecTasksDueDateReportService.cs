namespace CamcoTasks.Accessory.AutomationJob.IService
{
    public interface IRecTasksDueDateReportService
    {
        Task DoWork(CancellationToken stoppingToken);
    }
}
