using CamcoTasks.Accessory.AutomationJob.IService;
using CamcoTasks.Accessory.AutomationJobTask.Service;
using CamcoTasks.Service.IService;
using Microsoft.Extensions.Logging;

namespace CamcoTasks.Accessory.AutomationJob.Service
{
    public class DeactiveRecurringTaskReportService : IDeactiveRecurringTaskReportService
    {
        private IAutomatedAutomationsService _automatedAutomationsService;
        private IRecurringTaskReportService _recurringTaskReportService;
        private readonly ILogger _logger;
        int defaultDelay = 600000;

        public DeactiveRecurringTaskReportService(ILogger<RecurringTaskAuditReportService> logger, IAutomatedAutomationsService automatedAutomationsService, IRecurringTaskReportService recurringTaskReportService)
        {
            _automatedAutomationsService = automatedAutomationsService;
            _recurringTaskReportService = recurringTaskReportService;
            _logger = logger;
        }

        public async Task DoWork(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await DeactiveRecurringTasksReportAsync(stoppingToken);
            }
        }

        private async Task DeactiveRecurringTasksReportAsync(CancellationToken stoppingToken)
        {
            try
            {
                var automation = await _automatedAutomationsService
                    .GetAutomationAsync("Deactive Recurring Task Report");

                if (automation != null)
                {
                    if (automation.IsEnabled)
                    {
                        DateTime today = DateTime.Now;
                        if (automation.LastRun.Date != today.Date || automation.NeedsRestart)
                        {
                            string dayName = today.DayOfWeek.ToString();
                            if (automation.GetType().GetProperty(dayName).GetValue(automation).Equals(true)
                                || automation.NeedsRestart)
                            {
                                if (automation.EarliestTime < today.TimeOfDay && today.TimeOfDay < automation.LatestTime
                                    || automation.NeedsRestart)
                                {
                                    await _recurringTaskReportService
                                        .DeactiveRecurringTasksReportAsync();

                                    automation.LastRun = today;
                                    automation.IsRunningNow = true;
                                    await _automatedAutomationsService.UpdateAsync(automation);
                                }
                            }
                        }
                    }
                    else
                    {
                        automation.IsRunningNow = false;
                        await _automatedAutomationsService.UpdateAsync(automation);
                    }
                }

                await Task.Delay(automation.TimerTick, stoppingToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "AutomationJobTask Exception Information:", ex);
                await Task.Delay(defaultDelay, stoppingToken);
            }
        }
    }
}
