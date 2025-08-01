using CamcoTasks.Infrastructure.IRepositories;
using CamcoTasks.Infrastructure.IRepositories.HR;
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
using CamcoTasks.Infrastructure.Repositories.TaskInfo;
using CamcoTasks.Infrastructure.Repository.Automated;
using CamcoTasks.Infrastructure.Repository.CAMCO;
using CamcoTasks.Infrastructure.Repository.HR;
using CamcoTasks.Infrastructure.Repository.IRepository.HR;
using CamcoTasks.Infrastructure.Repository.Logging;
using CamcoTasks.Infrastructure.Repository.Page;
using CamcoTasks.Infrastructure.Repository.TaskInfo;
using CamcoTasks.Infrastructure.Repository.TimeClock;
using Microsoft.EntityFrameworkCore.Storage;
using System;


namespace CamcoTasks.Infrastructure;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly DatabaseContext _context;
    private IDbContextTransaction _transaction;

    public UnitOfWork(DatabaseContext context)
    {
        _context = context;
    }

    //private IChangeAuditRepository _changeAudits;

    #region HR Entities

    private IAboveAndBeyondRecognitionRepository _aboveAndBeyondRecognitions;
    private IAboveAndBeyondRecognitionLogRepository _aboveAndBeyondRecognitionLogs;
    private IAccruedLostTimeAndVacationHistoryRepository _accruedLostTimeAndVacationHistories;
    private IAdvertisementTypeRepository _advertisementTypes;
    private IApplicantStatusRepository _applicantStatuses;
    private IChangeRequestRepository _changeRequests;
    private ICityRepository _cities;
    private ICustomReportRepository _customReports;
    private IDepartmentAndManagersLogRepository _departmentAndManagersLogs;
    private IDepartmentAndManagerRepository _departmentsAndManagers;
    private IDisciplineRepository _disciplines;
    private IDisciplineCurrentRepository _disciplineCurrents;
    private IDisciplineLogRepository _disciplineLogs;
    //private IEmailTypeRepository _emailTypes;
    private IEmployeeRepository _employees;
    private IEmployeeAndJobDescriptionRepository _employeeAndJobDescriptions;
    private IEmployeeChangeRequestRepository _employeeChangeRequests;
    private IEmployeeChangeRequestStateRepository _employeeChangeRequestStates;
    private IEmployeeChecklistItemRepository _employeeChecklistItems;
    private IEmployeeChecklistStateRepository _employeeChecklistStates;
    private IEmployeeShiftDifferentialRepository _employeeShiftDifferentials;
    private IEmploymentHistoryRepository _employmentHistories;
    private IEvaluationStateAgainstInterviewRepository _evaluationStateAgainstInterviews;
    private IGroupAndFeatureRepository _groupAndFeatures;
    private IInjuryAndIllnessIncidentReportingRepository _injuryAndIllnessIncidentsReporting;
    private IInterviewScoreCardRepository _interviewScoreCards;
    private IItemRepository _items;
    private IItemAllocationRepository _itemAllocations;
    private ILoanBalanceHistoryRepository _loanBalanceHistories;
    private IPayStubMetricsDisplayRepository _payStubMetricsDisplay;
    private IPerformanceImprovementPlanRepository _performanceImprovementPlans;
    private IPerformanceImprovementPlanFollowupRepository _performanceImprovementPlanFollowups;
    private IPerformanceReviewRepository _performanceReviews;
    private IPermissionGroupAndAppRepository _permissionGroupAndApps;
    private IPermissionGroupAndUserRepository _permissionGroupAndUsers;
    private IPublicEmployeeRepository _publicEmployees;
    private IScissorLiftLicenseRepository _scissorLiftLicenses;
    private IStateRepository _states;
    private ITempDisciplineRepository _temporaryDisciplines;
    private ITemporaryPerformanceImprovementPlanAndFollowUpRepository
        _temporaryPerformanceImprovementPlanAndFollowUps;
    private ITestAssignmentRepository _testAssignments;
    private ITestQuestionRepository _testQuestions;
    private ITestResultRepository _testResults;
    private IUnemploymentClaimRepository _unemploymentClaims;
    private IUnemploymentClaimDocumentRepository _unemploymentClaimDocuments;
    private IUserLcaPlanPhaseStateRepository _userLcaPlanPhaseStates;
    private IWeeklyInsuranceLogRepository _weeklyInsuranceLogs;
    private IWeeklyWageRepository _weeklyWages;
    private IPayrollRepository _payrolls;
    private IPayrollDetailRepository _payrollDetails;
    private IQualityLevelStatusRepository _qualityLevelStatusRepository;
    private IPermissionActionRepository _permissionActionRepository;
    private IPermissionRepository _permissionRepository;
    private IFormAndInformationRepository _formsAndInformation;


    private IChangeLogRepository _changeLogs;

    public IChangeLogRepository ChangeLogs
    {
        get { return _changeLogs ??= new ChangeLogRepository(_context); }
    }
    private IAutomationRepository _automations;

    public IAutomationRepository Automations
    {
        get { return _automations ??= new AutomationRepository(_context); }
    }

    public IEmployeeRepository Employees
    {
        get { return _employees ??= new EmployeeRepository(_context); }
    }

    //private IEmployeeAndJobDescriptionRepository _employeeAndJobDescriptions;
    public IEmployeeAndJobDescriptionRepository EmployeeAndJobDescriptions
    {
        get { return _employeeAndJobDescriptions ??= new EmployeeAndJobDescriptionRepository(_context); }
    }

    public IFormAndInformationRepository FormsAndInformation
    {
        get { return _formsAndInformation ??= new FormAndInformationRepository(_context); }
    }

    public IPermissionRepository Permissions
    {
        get { return _permissionRepository ??= new PermissionRepository(_context); }
    }
    public IPermissionActionRepository PermissionActions
    {
        get { return _permissionActionRepository ??= new PermissionActionRepository(_context); }
    }

    public IQualityLevelStatusRepository QualityLevelStatuses
    {
        get { return _qualityLevelStatusRepository ??= new QualityLevelStatusRepository(_context); }
    }

    public IAboveAndBeyondRecognitionRepository AboveAndBeyondRecognitions
    {
        get { return _aboveAndBeyondRecognitions ??= new AboveAndBeyondRecognitionRepository(_context); }
    }

    //public IAboveAndBeyondRecognitionLogRepository AboveAndBeyondRecognitionLogs
    //{
    //    get { return _aboveAndBeyondRecognitionLogs ??= new AboveAndBeyondRecognitionLogRepository(_context); }
    //}

    public IAccruedLostTimeAndVacationHistoryRepository AccruedLostTimeAndVacationHistories
    {
        get
        {
            return _accruedLostTimeAndVacationHistories ??=
                new AccruedLostTimeAndVacationHistoryRepository(_context);
        }
    }

    //public IAdvertisementTypeRepository AdvertisementTypes
    //{
    //    get { return _advertisementTypes ??= new AdvertisementTypeRepository(_context); }
    //}

    public IApplicantStatusRepository ApplicantStatuses
    {
        get { return _applicantStatuses ??= new ApplicantStatusRepository(_context); }
    }


    //public IApplicationRepository Applications
    //{
    //    get { return _applications ??= new ApplicationRepository(_context); }
    //}

    //public IApplicationMethodRepository ApplicationMethods
    //{
    //    get { return _applicationMethods ??= new ApplicationMethodRepository(_context); }
    //}

    //public IApplicationTrackingReasonRepository ApplicationTrackingReasons
    //{
    //    get { return _applicationTrackingReasons ??= new ApplicationTrackingReasonRepository(_context); }
    //}

    //public IBreakInformationAgainstShiftRepository BreakInformationAgainstShifts
    //{
    //    get { return _breakInformationAgainstShifts ??= new BreakInformationAgainstShiftRepository(_context); }
    //}

    //public ICertificationRepository Certifications
    //{
    //    get { return _certifications ??= new CertificationRepository(_context); }
    //}

    //public ICertificationTypeRepository CertificationTypes
    //{
    //    get { return _certificationTypes ??= new CertificationTypeRepository(_context); }
    //}

    public IChangeRequestRepository ChangeRequests
    {
        get { return _changeRequests ??= new ChangeRequestRepository(_context); }
    }

    //public ICityRepository Cities
    //{
    //    get { return _cities ??= new CityRepository(_context); }
    //}

    public ICustomReportRepository CustomReports
    {
        get { return _customReports ??= new CustomReportRepository(_context); }
    }


    public IDepartmentAndManagersLogRepository DepartmentAndManagersLogs
    {
        get { return _departmentAndManagersLogs ??= new DepartmentAndManagersLogRepository(_context); }
    }


    //public IDisciplineRepository Disciplines
    //{
    //    get { return _disciplines ??= new DisciplineRepository(_context); }
    //}


    //public IDisciplineCurrentRepository DisciplineCurrents
    //{
    //    get { return _disciplineCurrents ??= new DisciplineCurrentRepository(_context); }
    //}

    public IDisciplineLogRepository DisciplineLogs
    {
        get { return _disciplineLogs ??= new DisciplineLogRepository(_context); }
    }


    //public IEmployeeAndJobDescriptionRepository EmployeeAndJobDescriptions
    //{
    //    get { return _employeeAndJobDescriptions ??= new EmployeeAndJobDescriptionRepository(_context); }
    //}

    public IEmployeeChangeRequestRepository EmployeeChangeRequests
    {
        get { return _employeeChangeRequests ??= new EmployeeChangeRequestRepository(_context); }
    }

    public IEmployeeChangeRequestStateRepository EmployeeChangeRequestStates
    {
        get { return _employeeChangeRequestStates ??= new EmployeeChangeRequestStateRepository(_context); }
    }

    public IEmployeeChecklistItemRepository EmployeeChecklistItems
    {
        get { return _employeeChecklistItems ??= new EmployeeChecklistItemRepository(_context); }
    }

    public IEmployeeChecklistStateRepository EmployeeChecklistStates
    {
        get { return _employeeChecklistStates ??= new EmployeeChecklistStateRepository(_context); }
    }

    public IEmployeeShiftDifferentialRepository EmployeeShiftDifferentials
    {
        get { return _employeeShiftDifferentials ??= new EmployeeShiftDifferentialRepository(_context); }
    }


    public IEmploymentHistoryRepository EmploymentHistories
    {
        get { return _employmentHistories ??= new EmploymentHistoryRepository(_context); }
    }

    public IEvaluationStateAgainstInterviewRepository EvaluationStateAgainstInterviews
    {
        get { return _evaluationStateAgainstInterviews ??= new EvaluationStateAgainstInterviewRepository(_context); }
    }

    public IGroupAndFeatureRepository GroupAndFeatures
    {
        get { return _groupAndFeatures ??= new GroupAndFeatureRepository(_context); }
    }

    public IInjuryAndIllnessIncidentReportingRepository InjuryAndIllnessIncidentsReporting
    {
        get
        {
            return _injuryAndIllnessIncidentsReporting ??=
                new InjuryAndIllnessIncidentReportingRepository(_context);
        }
    }

    public IInterviewScoreCardRepository InterviewScoreCards
    {
        get { return _interviewScoreCards ??= new InterviewScoreCardRepository(_context); }
    }

    public IItemRepository Items
    {
        get { return _items ??= new ItemRepository(_context); }
    }

    public IItemAllocationRepository ItemAllocations
    {
        get { return _itemAllocations ??= new ItemAllocationRepository(_context); }
    }
    /*
        public ILoanBalanceHistoryRepository LoanBalanceHistories
        {
            get { return _loanBalanceHistories ??= new LoanBalanceHistoryRepository(_context); }
        }
    */

    //public IPayStubMetricsDisplayRepository PayStubMetricsDisplay
    //{
    //    get { return _payStubMetricsDisplay ??= new PayStubMetricsDisplayRepository(_context); }
    //}


    public IPayrollDetailRepository PayrollDetails
    {
        get { return _payrollDetails ??= new PayrollDetailRepository(_context); }
    }

    public IPerformanceImprovementPlanRepository PerformanceImprovementPlans
    {
        get { return _performanceImprovementPlans ??= new PerformanceImprovementPlanRepository(_context); }
    }

    public IPerformanceImprovementPlanFollowupRepository PerformanceImprovementPlanFollowups
    {
        get
        {
            return _performanceImprovementPlanFollowups ??=
                new PerformanceImprovementPlanFollowupRepository(_context);
        }
    }

    //public IPerformanceReviewRepository PerformanceReviews
    //{
    //    get { return _performanceReviews ??= new PerformanceReviewRepository(_context); }
    //}




    public IPermissionGroupAndAppRepository PermissionGroupAndApps
    {
        get { return _permissionGroupAndApps ??= new PermissionGroupAndAppRepository(_context); }
    }

    public IPermissionGroupAndUserRepository PermissionGroupAndUsers
    {
        get { return _permissionGroupAndUsers ??= new PermissionGroupAndUserRepository(_context); }
    }

    public IPublicEmployeeRepository PublicEmployees
    {
        get { return _publicEmployees ??= new PublicEmployeeRepository(_context); }
    }

    public IScissorLiftLicenseRepository ScissorLiftLicenses
    {
        get { return _scissorLiftLicenses ??= new ScissorLiftLicenseRepository(_context); }
    }

    public IStateRepository States
    {
        get { return _states ??= new StateRepository(_context); }
    }

    private IEmployeeApprovedRecordRepository _employeeApprovedRecords;

    public IEmployeeApprovedRecordRepository EmployeeApprovedRecords
    {
        get { return _employeeApprovedRecords ??= new EmployeeApprovedRecordRepository(_context); }
    }

    public ITempDisciplineRepository TemporaryDisciplines
    {
        get { return _temporaryDisciplines ??= new TempDisciplineRepository(_context); }
    }

    public ITemporaryPerformanceImprovementPlanAndFollowUpRepository TemporaryPerformanceImprovementPlanAndFollowUps
    {
        get
        {
            return _temporaryPerformanceImprovementPlanAndFollowUps ??=
                new TemporaryPerformanceImprovementPlanAndFollowUpRepository(_context);
        }
    }

    public ITestAssignmentRepository TestAssignments
    {
        get { return _testAssignments ??= new TestAssignmentRepository(_context); }
    }

    public ITestQuestionRepository TestQuestions
    {
        get { return _testQuestions ??= new TestQuestionRepository(_context); }
    }

    public ITestResultRepository TestResults
    {
        get { return _testResults ??= new TestResultRepository(_context); }
    }

    public IWeeklyInsuranceLogRepository WeeklyInsuranceLogs
    {
        get { return _weeklyInsuranceLogs ??= new WeeklyInsuranceLogRepository(_context); }
    }

    public IWeeklyWageRepository WeeklyWages
    {
        get { return _weeklyWages ??= new WeeklyWageRepository(_context); }
    }

    #endregion


    #region Task Entities

    private IFrequencyListRepository _frequencyLists;
    private IRecurringTaskRepository _recurringTasks;
    private ITaskImageRepository _taskImages;
    private ITaskTaskRepository _taskTasks;
    private ITaskTaskTypeRepository _taskTaskTypes;
    private ITaskUpdateRepository _taskUpdates;
    private IUpdateNoteRepository _updateNotes;
    private ITaskEditorRepository _taskEditor;
    private ITaskFolderRepository _taskFolder;
    private ISubTaskRepository _subTask;
    private ITaskChangeLogRepository _taskChangeLog;
    private ITimeSheetDatumRepository _timeSheetsData;
    private IJobDescriptionRepository _jobDescriptions;
    private IProjectRepository _projects;

    public IFrequencyListRepository FrequencyLists
    {
        get { return _frequencyLists ??= new FrequencyListRepository(_context); }
    }

    public IRecurringTaskRepository RecurringTasks
    {
        get { return _recurringTasks ??= new RecurringTaskRepository(_context); }
    }

    public ITaskImageRepository TaskImages
    {
        get { return _taskImages ??= new TaskImageRepository(_context); }
    }
    private IEmployeeSettingRepository _employeeSettings;

    public IEmployeeSettingRepository EmployeeSettings
    {
        get { return _employeeSettings ??= new EmployeeSettingRepository(_context); }
    }


    public ITaskTaskRepository TaskTasks
    {
        get { return _taskTasks ??= new TaskTaskRepository(_context); }
    }

    public ITaskTaskTypeRepository TaskTaskTypes
    {
        get { return _taskTaskTypes ??= new TaskTaskTypeRepository(_context); }
    }

    public ITaskUpdateRepository TaskUpdates
    {
        get { return _taskUpdates ??= new TaskUpdateRepository(_context); }
    }

    public IUpdateNoteRepository UpdateNotes
    {
        get { return _updateNotes ??= new UpdateNoteRepository(_context); }
    }

    public ITaskEditorRepository TaskEditor
    {
        get { return _taskEditor ??= new TaskEditorRepository(_context); }
    }

    public ITaskFolderRepository TaskFolderRepository
    {
        get { return _taskFolder ??= new TaskFolderRepository(_context); }
    }

    public ISubTaskRepository SubTaskRepository
    {
        get { return _subTask ??= new SubTaskRepository(_context); }
    }

    public ITaskChangeLogRepository TaskChangeLogs
    {
        get { return _taskChangeLog ??= new TaskChangeLogRepository(_context); }
    }

    public ITimeSheetDatumRepository TimeSheetsData
    {
        get { return _timeSheetsData ?? throw new NotImplementedException(); }
    }

    public IJobDescriptionRepository JobDescriptions
    {
        get { return _jobDescriptions ?? throw new NotImplementedException(); }
    }

    public IDepartmentAndManagerRepository DepartmentsAndManagers
    {
        get { return _departmentsAndManagers ?? new DepartmentAndManagerRepository(_context); }
    }

    public IProjectRepository Projects
    {
        get { return _projects ??= new ProjectRepository(_context); }
    }

    #endregion

   
    public async Task<bool> CompleteAsync()
    {
        await using var dbContextTransaction = await _context.Database.BeginTransactionAsync();
        await _context.SaveChangesAsync();
        await dbContextTransaction.CommitAsync();

        return true;
    }

    public bool Complete()
    {
        using var dbContextTransaction = _context.Database.BeginTransaction();
        _context.SaveChanges();
        dbContextTransaction.Commit();
        return true;
    }

    public IDbContextTransaction BeginTransaction()
    {
        return _context.Database.BeginTransaction();
    }

    public IDbContextTransaction GetCurrentTransaction()
    {
        return _context.Database.CurrentTransaction;
    }

    private ILoadTimeRepository _loadTimes;

    public ILoadTimeRepository LoadTimes
    {
        get { return _loadTimes ??= new LoadTimeRepository(_context); }
    }


    public void Refresh()
    {
        _context.ChangeTracker.Entries().ToList().ForEach(e => e.Reload());
    }

    public void Dispose()
    {
        _context.Dispose();
    }

    public async Task BeginTransactionAsync()
    {
        _transaction = await _context.Database.BeginTransactionAsync();
    }

    public async Task CommitAsync()
    {
        try
        {
            await _context.SaveChangesAsync();
            if (_transaction != null)
            {
                await _transaction.CommitAsync();
            }
        }
        finally
        {
            if (_transaction != null)
            {
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }
    }

    public async Task RollbackAsync()
    {
        if (_transaction != null)
        {
            await _transaction.RollbackAsync();
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }

} 