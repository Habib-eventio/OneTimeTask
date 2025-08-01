using CamcoTasks.Infrastructure.Entities.TimeClock;
using CamcoTasks.Infrastructure.IRepositories;
using CamcoTasks.Infrastructure.IRepositories.HR;
using CamcoTasks.Infrastructure.IRepositories.Maintenance;
using CamcoTasks.Infrastructure.IRepositories.TaskInfo;
using CamcoTasks.Infrastructure.IRepository;
using CamcoTasks.Infrastructure.IRepository.Automated;
using CamcoTasks.Infrastructure.IRepository.CAMCO;
using CamcoTasks.Infrastructure.IRepository.HR;
using CamcoTasks.Infrastructure.IRepository.Logging;
using CamcoTasks.Infrastructure.IRepository.Page;
using CamcoTasks.Infrastructure.IRepository.Production;
using CamcoTasks.Infrastructure.IRepository.TaskInfo;
using CamcoTasks.Infrastructure.IRepository.TimeClock;
using CamcoTasks.Infrastructure.Repository.IRepository.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Threading.Tasks;

namespace CamcoTasks.Infrastructure
{
    public interface IUnitOfWork
    {
        //ICamcoJobRepository CamcoJobs { get; }
        IProjectRepository Projects { get; }
        ITimeSheetDatumRepository TimeSheetsData { get; }
        IJobDescriptionRepository JobDescriptions { get; }
        IDepartmentAndManagerRepository DepartmentsAndManagers { get; }
        IChangeLogRepository ChangeLogs { get; }
        IEmployeeApprovedRecordRepository EmployeeApprovedRecords { get; }
        ILoadTimeRepository LoadTimes { get; }
        IEmployeeSettingRepository EmployeeSettings { get; }
        IAutomationRepository Automations { get; }
        //IProjectRepository Projects { get; }
        IAboveAndBeyondRecognitionRepository AboveAndBeyondRecognitions { get; }
        IAccruedLostTimeAndVacationHistoryRepository AccruedLostTimeAndVacationHistories { get; }
        IApplicantStatusRepository ApplicantStatuses { get; }
        IChangeRequestRepository ChangeRequests { get; }
        ICustomReportRepository CustomReports { get; }
        IDepartmentAndManagersLogRepository DepartmentAndManagersLogs { get; }
        IDisciplineLogRepository DisciplineLogs { get; }
        //IEmailGroupRepository EmailGroups { get; }
        //IEmailTypeRepository EmailTypes { get; }
        IEmployeeRepository Employees { get; }
        IEmployeeAndJobDescriptionRepository EmployeeAndJobDescriptions { get; }
        IEmployeeChangeRequestRepository EmployeeChangeRequests { get; }
        IEmployeeChangeRequestStateRepository EmployeeChangeRequestStates { get; }
        IEmployeeChecklistItemRepository EmployeeChecklistItems { get; }
        IEmployeeChecklistStateRepository EmployeeChecklistStates { get; }
        IEmployeeShiftDifferentialRepository EmployeeShiftDifferentials { get; }
        IEmploymentHistoryRepository EmploymentHistories { get; }
        IGroupAndFeatureRepository GroupAndFeatures { get; }
        IInjuryAndIllnessIncidentReportingRepository InjuryAndIllnessIncidentsReporting { get; }
        IInterviewScoreCardRepository InterviewScoreCards { get; }
        IItemRepository Items { get; }
        IItemAllocationRepository ItemAllocations { get; }
        //IJobDescriptionRepository JobDescriptions { get; }
        //ILoginLogRepository LoginLogs { get; }
        IPayrollDetailRepository PayrollDetails { get; }
        IPerformanceImprovementPlanRepository PerformanceImprovementPlans { get; }
        IPerformanceImprovementPlanFollowupRepository PerformanceImprovementPlanFollowups { get; }
        IPermissionGroupAndAppRepository PermissionGroupAndApps { get; }
        IPermissionGroupAndUserRepository PermissionGroupAndUsers { get; }
        IPublicEmployeeRepository PublicEmployees { get; }
        IScissorLiftLicenseRepository ScissorLiftLicenses { get; }
        IStateRepository States { get; }
        ITempDisciplineRepository TemporaryDisciplines { get; }
        IFormAndInformationRepository FormsAndInformation { get; }
        ITemporaryPerformanceImprovementPlanAndFollowUpRepository TemporaryPerformanceImprovementPlanAndFollowUps { get; }
        ITestAssignmentRepository TestAssignments { get; }
        ITestQuestionRepository TestQuestions { get; }
        ITestResultRepository TestResults { get; }
        IWeeklyInsuranceLogRepository WeeklyInsuranceLogs { get; }
        IWeeklyWageRepository WeeklyWages { get; }
        IQualityLevelStatusRepository QualityLevelStatuses { get; }
        IPermissionActionRepository PermissionActions { get; }
        IPermissionRepository Permissions { get; }
        IFrequencyListRepository FrequencyLists { get; }
        IRecurringTaskRepository RecurringTasks { get; }
        ITaskImageRepository TaskImages { get; }
        ITaskTaskRepository TaskTasks { get; }
        ITaskTaskTypeRepository TaskTaskTypes { get; }
        ITaskUpdateRepository TaskUpdates { get; }
        IUpdateNoteRepository UpdateNotes { get; }
        ITaskEditorRepository TaskEditor { get; }
        ITaskFolderRepository TaskFolderRepository { get; }
        ISubTaskRepository SubTaskRepository { get; }
        ITaskChangeLogRepository TaskChangeLogs { get; }
        Task<bool> CompleteAsync();
        bool Complete();
        IDbContextTransaction GetCurrentTransaction();
        IDbContextTransaction BeginTransaction();
        void Refresh();
        Task CommitAsync();
        Task BeginTransactionAsync();
        Task RollbackAsync();
    }
}
