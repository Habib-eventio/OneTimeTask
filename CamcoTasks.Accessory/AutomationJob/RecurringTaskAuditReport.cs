using CamcoTasks.Accessory.AutomationJobTask.IService;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CamcoTasks.Accessory.AutomationJobTask
{
    public class RecurringTaskAuditReport : BackgroundService
    {
        private IServiceProvider _serviceProvider;
        private ILogger<DueRecurringTaskReport> _logger;

        public RecurringTaskAuditReport(IServiceProvider serviceProvider, ILogger<DueRecurringTaskReport> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await DoWork(stoppingToken);
        }

        private async Task DoWork(CancellationToken stoppingToken)
        {
            _logger.LogInformation(
                "Consume Scoped Service Hosted Service is working.");

            using (var scope = _serviceProvider.CreateScope())
            {
                var scopedProcessingService =
                    scope.ServiceProvider
                        .GetRequiredService<IRecurringTaskAuditReportService>();

                await scopedProcessingService.DoWork(stoppingToken);
            }
        }

        public override async Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation(
                "Consume Scoped Service Hosted Service is stopping.");

            await base.StopAsync(stoppingToken);
        }
    }
}
