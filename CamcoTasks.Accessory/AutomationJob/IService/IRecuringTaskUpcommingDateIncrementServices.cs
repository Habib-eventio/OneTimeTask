namespace CamcoTasks.Accessory.AutomationJob.IService
{
    public interface IRecuringTaskUpcommingDateIncrementServices
    {
        Task DoWork(CancellationToken stoppingToken);
    }
}
