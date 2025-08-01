using CamcoTasks.Data.Services;
using CamcoTasks.Infrastructure;
using CamcoTasks.Service.IService;
using CamcoTasks.Service.Service;
using CamcoTasks.Service.Service.Ollama;
using Microsoft.Extensions.Options;

namespace CamcoTasks.AppSettings
{
    public static class AppServiceSetting
    {
        public static IServiceCollection AppService(this IServiceCollection services)
        {
            services.AddTransient<FileManagerService>();
            services.AddTransient<IEmployeeService, EmployeeService>();
            services.AddTransient<IJobDescriptionsService, JobDescriptionsService>();
            services.AddTransient<IDepartmentService, DepartmentService>();
            services.AddTransient<ITasksService, TasksService>();
            services.AddTransient<ILoggingChangeLogService, LoggingChangeLogService>();
            services.AddTransient<ILoggingChangeLogService, LoggingChangeLogService>();
            services.AddTransient<IUpdateNotesService, UpdateNotesService>();
            services.AddTransient<ITasksService, TasksService>();
            services.AddTransient<ITimeClockEmployeeSettingService, TimeClockEmployeeSettingService>();
            services.AddTransient<IFileManagerService, FileManagerService>();
            services.AddTransient<ILoggingChangeLogService, LoggingChangeLogService>();
            services.AddTransient<ILogingService, NullLogingService>();
            services.AddTransient<IPageLoadTimeService, NullPageLoadTimeService>();

            services.AddHttpClient<IBotService, OllamaBotService>((sp, client) =>
            {
                var opt = sp.GetRequiredService<IOptions<OllamaOptions>>().Value;
                client.BaseAddress = new Uri(opt.BaseUrl.TrimEnd('/'));
                client.Timeout = TimeSpan.FromMinutes(5);
            });

            services.AddTransient<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
