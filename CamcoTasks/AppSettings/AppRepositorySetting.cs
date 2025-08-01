
using CamcoTasks.Infrastructure.Common.EnumHelper;
using CamcoTasks.Infrastructure.EnumHelper;
using CamcoTasks.Infrastructure.IRepositories;
using CamcoTasks.Infrastructure.IRepository.CAMCO;
using CamcoTasks.Infrastructure.IRepository.TimeClock;
using CamcoTasks.Infrastructure.Repositories.TaskInfo;
using CamcoTasks.Infrastructure.Repository.CAMCO;
using CamcoTasks.Infrastructure.Repository.TaskInfo;
using CamcoTasks.Infrastructure.Repository.TimeClock;
using CamcoTasks.Service.IService;
using CamcoTasks.Service.Service;

namespace CamcoTasks.AppSettings
{
	public static class AppRepositorySetting
    {
        public static IServiceCollection AppRepository(this IServiceCollection services)
        {
            //services.AddTransient<IEmailQueueRepository, EmailQueueRepository>();
            //services.AddTransient<IEmployeeRepository, EmployeeRepository>();
            //services.AddTransient<IMetricTypeRepository, MetricTypeRepository>();
            //services.AddTransient<ITimeSheetDatumRepository, TimeSheetDatumRepository>();
            services.AddScoped<IRecurringTaskRepository, RecurringTaskRepository>();
            //services.AddTransient<ITicketRepository, TicketRepository>();
            services.AddScoped<IUpdateNoteRepository, UpdateNoteRepository>();
            services.AddTransient<ICamcoProjectService, CamcoProjectService>();
            services.AddTransient<ICommonDataService, CommonDataService>();
            //services.AddTransient<IDepartmentAndManagerRepository, DepartmentAndManagerRepository>();
            //services.AddTransient<IQualityLineInspectionRepository, QualityLineInspectionRepository>();
            //services.AddTransient<IAutomationRepository, AutomationRepository>();
            //services.AddTransient<IUserRepository, UserRepository>();
            //services.AddTransient<IJobDescriptionRepository, JobDescriptionRepository>();
            //services.AddTransient<IEmployeeAndJobDescriptionRepository  , EmployeeAndJobDescriptionRepository>();
            //services.AddTransient<IChangeLogRepository, ChangeLogRepository>();
            services.AddTransient<IProjectRepository, ProjectRepository>();
            //services.AddTransient<ERP.Repository.IRepository.ToolCrib.IEmailDistributionListRepository, ERP.Repository.Repository.ToolCrib.EmailDistributionListRepository>();
            //services.AddTransient<IProgrammerClosedITTicketRepository, ProgrammerClosedITTicketRepository>();
            services.AddTransient<IEmployeeSettingRepository, EmployeeSettingRepository>();
            //services.AddTransient<IMiscellaneousRepository, MiscellaneousRepository>();
            //services.AddTransient<ITransactionTimeLogRepository, TransactionTimeLogRepository>();
            //services.AddTransient<ERP.Repository.IRepository.TimeTracking.IOptionRepository, ERP.Repository.Repository.TimeTracking.OptionRepository>();
            //services.AddTransient<ILoginLogRepository, LoginLogRepository>();
            //services.AddTransient<ICommonDataService, CommonDataService>();

            return services;
        }
    }
}
