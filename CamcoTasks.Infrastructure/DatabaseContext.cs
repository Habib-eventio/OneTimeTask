using CamcoTasks.Infrastructure.Entities;
using CamcoTasks.Infrastructure.Entities.CAMCO;
using CamcoTasks.Infrastructure.Entities.HR;
using CamcoTasks.Infrastructure.Entities.Login;
using CamcoTasks.Infrastructure.Entities.Maintenance;
using CamcoTasks.Infrastructure.Entities.Production;
using CamcoTasks.Infrastructure.Entities.TaskInfo;
using CamcoTasks.Infrastructure.Entities.TimeClock;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using static CamcoTasks.Infrastructure.Entities.TimeClock.EmployeeWeeklyHoursReportViewModel;

namespace CamcoTasks.Infrastructure
{
    public class DatabaseContext : IdentityDbContext<User, Role, long>
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }

        // Domain entities

        //public virtual DbSet<BusinessTime> BusinessTimes { get; set; }
        //public virtual DbSet<EmployeeExtraTimeRecord> EmployeeExtraTimeRecords { get; set; }
        public virtual DbSet<EmployeeApprovedRecord> EmployeeApprovedRecords { get; set; }
        public virtual DbSet<EmployeeCheckInAndOut> EmployeeCheckInAndOuts { get; set; }
        public virtual DbSet<VacationRequest> VacationRequests { get; set; }
        public virtual DbSet<TimeSheetDatum> TimeSheetsData { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<JobDescription> JobDescriptions { get; set; }
        public virtual DbSet<Item> Items { get; set; }
        public DbSet<TaskTask> TaskTasks { get; set; }
        public DbSet<TaskUpdate> TaskUpdates { get; set; }
        public DbSet<RecurringTask> RecurringTasks { get; set; }
        public DbSet<FrequencyList> FrequencyLists { get; set; }
        public DbSet<SubTask> SubTasks { get; set; }
        public DbSet<TaskEditor> TaskEditors { get; set; }
        public DbSet<TaskFolder> TaskFolders { get; set; }
        public DbSet<TaskImage> TaskImages { get; set; }
        public virtual DbSet<TestQuestion> TestQuestions { get; set; }
        public virtual DbSet<PermissionGroup> PermissionGroups { get; set; }
        public DbSet<TaskTaskType> TaskTaskTypes { get; set; }
        public DbSet<TaskChangeLog> TaskChangeLogs { get; set; }
        public DbSet<UpdateNote> UpdateNotes { get; set; }
        public DbSet<WorkOrderData> WorkOrdersData { get; set; }
        public virtual DbSet<UserLcaPlanPhaseState> UserLcaPlanPhaseStates { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<QualityLevelStatus> QualityLevelStatuses { get; set; }
        public virtual DbSet<WeeklyWage> WeeklyWages { get; set; }
        public virtual DbSet<LoginLog> LoginLogs { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<PerformanceReview> PerformanceReviews { get; set; }
        public virtual DbSet<Test> Tests { get; set; }
        public virtual DbSet<PayrollDetail> PayrollDetails { get; set; }
        public virtual DbSet<DepartmentAbbreviatedName> DepartmentAbbreviatedNames { get; set; }
        public virtual DbSet<Payroll> Payrolls { get; set; }
        public virtual DbSet<ApplicantStatus> ApplicantStatuses { get; set; }
        public virtual DbSet<DepartmentAndManager> DepartmentsAndManagers { get; set; }
        public virtual DbSet<EmployeeSetting> EmployeeSettings { get; set; }
        public virtual DbSet<EmailType> EmailTypes { get; set; }
        public virtual DbSet<State> States { get; set; }
        public virtual DbSet<EmailQueue> EmailQueues { get; set; }
        public virtual DbSet<EmployeeExtraTimeCategory> EmployeeExtraTimeCategories { get; set; }
        public virtual DbSet<EmploymentHistory> EmploymentHistories { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }
    }
}
