using BlazorDownloadFile;
using Blazored.Toast.Services;
using ERP.Data.Entities.HR;
using CamcoTasks.Data.Services;
using CamcoTasks.Library;
using CamcoTasks.Service.IService;
using CamcoTasks.ViewModels.EmployeeDTO;
using CamcoTasks.ViewModels.JobDescriptions;
using CamcoTasks.ViewModels.PageLoadTimeDTO;
using CamcoTasks.ViewModels.TasksFrequencyListDTO;
using CamcoTasks.ViewModels.TasksImagesDTO;
using CamcoTasks.ViewModels.TasksRecTasksDTO;
using CamcoTasks.ViewModels.TasksTaskUpdatesDTO;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using Org.BouncyCastle.Asn1.X509;
using Syncfusion.Blazor.Data;
using Syncfusion.Blazor.DropDowns;
using Syncfusion.Blazor.Grids;
using Syncfusion.Blazor.Inputs;
using FileInfo = Syncfusion.Blazor.Inputs.FileInfo;
using Syncfusion.Blazor.PdfViewerServer;
using static Microsoft.AspNetCore.Razor.Language.TagHelperMetadata;
using CamcoTasks.Infrastructure.IRepository.HR;
using CamcoTasks.Infrastructure.Entities.HR;

namespace CamcoTasks.Pages.RecurringTasks.ViewRecurringTasks
{
    public partial class CreateRecurringTaskComponent
    {
        protected SfUploader HowToUploadRef;

        protected TasksImagesViewModel HowToFileUpload;

        protected string UploadFileId;

        protected DateTime MinDate = DateTime.Now;

        protected bool IsEditHistoryComponent = false;
        protected bool IsActiveJobDescriptionFilter = false;

        protected TasksRecTasksViewModel RecTaskForEditHistory = new TasksRecTasksViewModel();

        protected List<string> Days = new List<string>() { "MONDAY", "TUESDAY", "WEDNESDAY", "THURSDAY",
            "FRIDAY", "SATURDAY", "SUNDAY" };
        protected List<JobDescriptionEmployeeEmail> JobDescriptionWithEmployeeEmails =
            new List<JobDescriptionEmployeeEmail>();
        protected List<JobDescriptionEmployeeEmail> ActiveJobDescriptionWithEmployeeEmails =
            new List<JobDescriptionEmployeeEmail>();
        protected List<JobDescriptionEmployeeEmail> ActiveFilterJobDescriptionWithEmployeeEmails =
            new List<JobDescriptionEmployeeEmail>();

        protected bool IsLoadTask { get; set; } = true;
        protected bool IsUploading = false;
        protected bool IsDoingTask { get; set; } = false;
        protected bool IsFilter { get; set; } = false;
        protected bool IsActiveGraphComponent { get; set; } = false;
        protected bool IsActiveQuestionComponent { get; set; } = false;
        protected bool IsActiveSampleImageComponent { get; set; } = false;
        protected bool IsActiveFailedEmailComponent { get; set; } = false;

        protected List<string> Frequencies { get; set; }
        protected List<string> Employees { get; set; }
        protected List<string> EmployeesByJobId { get; set; }
        protected List<string> SearchByEmployeesList { get; set; }
        protected List<string> JobList { get; set; } = new List<string>();
        protected List<string> SearchByJobList { get; set; } = new List<string>();
        protected List<JobDescriptionsViewModal> SearchedJobListByEmployee { get; set; } = new List<JobDescriptionsViewModal>();
        protected List<JobDescriptionEmployeeEmail> FilterJobDescriptionEmployeeEmails { get; set; } =
            new List<JobDescriptionEmployeeEmail>();
        protected List<EmployeeEmail> DocumentDeliverToName { get; set; } = new List<EmployeeEmail>();
        protected List<EmployeeEmail> AuditemployeeEmails { get; set; } = new List<EmployeeEmail>();
        protected List<TasksImagesViewModel> TaskUploadFiles { get; set; } = new List<TasksImagesViewModel>();

        protected TasksImagesViewModel UpdatedDocumentUploadFile = new TasksImagesViewModel();
        protected List<TasksImagesViewModel> SelectedTaskFiles { get; set; } = new List<TasksImagesViewModel>();
        protected List<TasksRecTasksViewModel> Tasks { get; set; } = new List<TasksRecTasksViewModel>();
        protected List<UploadFiles> SampleuploadFiles { get; set; } = new List<UploadFiles>();
        protected List<UploadFiles> UpdateUploadFiles { get; set; } = new List<UploadFiles>();
        protected IEnumerable<EmployeeAndJobDescription> EmployeeAndJobList { get; set; }
        protected List<EmployeeEmail> HandDeliverRequiredToNames { get; set; }
        protected bool IsDocumentHandDeliveryEnabled { get; set; }
        protected List<string> TaskArea { get; set; } = new List<string>();
        protected List<EmployeeEmail> AuthorizationList { get; set; } = new List<EmployeeEmail>();

        protected IEnumerable<EmployeeViewModel> EmployeesList { get; set; }
        protected string SelectedJobTilteFromSearchedEmployee { get; set; }
        protected string DeclinedNote { get; set; }

        protected TasksRecTasksViewModel SelectedTaskViewModel { get; set; } = new TasksRecTasksViewModel() { ParentTaskId = null };
        protected TasksRecTasksViewModel SelectedTaskForGraph { get; set; }
        protected TasksRecTasksViewModel SelectedTaskForQuestion { get; set; }
        protected TasksRecTasksViewModel SelectedTaskForSampleImage { get; set; }

        protected Dictionary<string, object> htmlAttribute = new Dictionary<string, object>() { { "rows", "4" } };
        protected bool IsInquiryNote { get; set; } = false;
        protected string ErrorMessage { get; set; }
        protected PageLoadTimeViewModel PageLoadTime { get; set; } = new PageLoadTimeViewModel()
        {
            PageName = "RecurringTask",
            SectionName = "BeforeCreateRecTask",
            DateCreated = DateTime.Now
        };

        protected Dictionary<Guid, string> CapturedImgaesList = new();
        protected Dictionary<string, object> htmlAttributeBig = new Dictionary<string, object>() { { "rows", "7" }, { "spellcheck", "true" } };

        protected Dictionary<string, object> htmlAttributeForDocumentRequired = new Dictionary<string, object>() { { "rows", "5" }, { "spellcheck", "true" } };

        protected string TaskTitle { get; set; } = "ADD NEW";
        protected string RespondBody { get; set; } = "";
        protected string SampleImagePreView { get; set; }
        protected string TaskImage { get; set; } = null;
        protected string TaskFile { get; set; } = null;
        protected string RecTaskErrorMessage { get; set; } = string.Empty;

        protected int PossibleId { get; set; } = 0;
        protected int RecentTaskImagesAndFileCount { get; set; }
        protected int RecentTaskFilesCount { get; set; }

        protected SfUploader TaskUploadRef { get; set; }
        protected SfUploader UploadObj { get; set; }
        protected SfGrid<EmployeeEmail> EMailGrid2 { get; set; }

        protected TasksRecTasksViewModel ImageUpdateModel { get; set; } = null;

        protected TasksTaskUpdatesViewModel SelectedUpdateViewModel { get; set; } = new TasksTaskUpdatesViewModel()
        { Update = string.Empty, UpdateDate = DateTime.Today, IsAudit = false, IsPass = false };

        [Inject]
        private IJSRuntime jSRuntime { get; set; }
        [Inject]
        protected ITasksService taskService { get; set; }
        [Inject]
        protected IEmployeeService employeeService { get; set; }
        [Inject]
        protected IEmployeeAndJobDescriptionRepository employeeSearchJob { get; set; }
        [Inject]
        private FileManagerService fileManagerService { get; set; }
        [Inject]
        private NavigationManager navigationManager { get; set; }
        [Inject]
        private IBlazorDownloadFileService blazorDownloadFileService { get; set; }
        [Inject]
        protected ILogger<CreateRecurringTaskComponent> logger { get; set; }
        [Inject]
        private IToastService _toastService { get; set; }
        [Inject]
        protected IDepartmentService departmentService { get; set; }
        [Inject]
        protected IPageLoadTimeService pageLoadTimeService { get; set; }
        [Inject]
        protected IJobDescriptionsService JobDescriptionsService { get; set; }
        [Inject]
        protected ILoggingChangeLogService LoggingChangeLogService { get; set; }
        [Inject]
        protected ITimeClockEmployeeSettingService TimeClockEmployeeSettingService { get; set; }

        [Parameter]
        public TasksRecTasksViewModel RecurringTask { get; set; }
        [Parameter]
        public long? ApproverId { get; set; }
        [Parameter]
        public string EditByEmployeeName { get; set; }
        [Parameter]
        public EventCallback<int> RefreshParentComponent { get; set; }
        [Parameter]
        public EventCallback CloseCreateRecurringTaskComponent { get; set; }
        [Parameter]
        public EventCallback<string> SendMessageToParent { get; set; }


        protected override async Task OnInitializedAsync()
        {
            await Task.Run(() => PageLoadTime.StartTime = DateTime.Now);
        }

        protected override async void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                await LoadData();

                StateHasChanged();

                await PageLoadTimeCalculation();
            }
        }

        protected async Task PageLoadTimeCalculation()
        {
            PageLoadTime.EndTime = DateTime.Now;
            await pageLoadTimeService.InsertAsync(PageLoadTime);
        }

        protected async Task LoadData()
        {
            await jSRuntime.InvokeAsync<object>("ShowModal", "#RecurringTaskModal");
            EmployeeAndJobList = await employeeSearchJob.GetListAsync();
            await LoadEmployeeAsync();
            await StartRecTask(RecurringTask);

            IsLoadTask = false;
        }

        protected async Task LoadEmployeeAsync()
        {
            var frequency = (await taskService.GetTaskFreqs()).ToList();
            Frequencies = frequency.Select(a => a.Frequency).Take(20).ToList();

            EmployeesList = await employeeService.GetListAsync(true, false);
            Employees = EmployeesList.Where(x => x.IsActive).Select(a => a.FullName).OrderBy(a => a).Distinct().ToList();
            EmployeesByJobId = Employees;
            SearchByEmployeesList = EmployeesByJobId;

            var jobDescription = await JobDescriptionsService.GetListAsync(false);
            JobList = jobDescription.Select(j => j.Name).ToList();
            SearchByJobList = JobList;
            JobDescriptionWithEmployeeEmails = await JobDescriptionsService
                .GetJobDescriptionWithEmployeeListAsync(
                EmployeesList.ToList(), jobDescription.ToList());
            if (JobDescriptionWithEmployeeEmails.Any(j => j.JobName == "PRESIDENT"))
            {
                int indexjob = JobDescriptionWithEmployeeEmails.FindIndex(j => j.JobName == "PRESIDENT");
                if (indexjob >= 0)
                {
                    var job = JobDescriptionWithEmployeeEmails[indexjob];
                    int employeeIndex = job.EmployeeEmails.FindIndex(e => e.FullName == "ARNOLD, RICHARD");

                    if (employeeIndex >= 0)
                    {
                        job.EmployeeEmails[employeeIndex].IsSelected = true;
                        JobDescriptionWithEmployeeEmails[indexjob].EmployeeEmails[employeeIndex] =
                            job.EmployeeEmails[employeeIndex];
                    }
                }
            }
            FilterJobDescriptionEmployeeEmails = JobDescriptionWithEmployeeEmails;
            ActiveJobDescriptionWithEmployeeEmails = JobDescriptionWithEmployeeEmails
                .Where(j => j.EmployeeEmails.Any(e => e.IsSelected)).ToList();
            ActiveFilterJobDescriptionWithEmployeeEmails = ActiveJobDescriptionWithEmployeeEmails;

            AuthorizationList = EmployeesList
                .Where(x => !string.IsNullOrEmpty(x.Email))
                .Select(x => new EmployeeEmail
                {
                    Id = x.Id,
                    FullName = x.FullName,
                    Email = x.Email,
                    IsSelected = false
                }).OrderBy(x => x.FullName).ToList();

            DocumentDeliverToName = EmployeesList
                .Where(x => !string.IsNullOrEmpty(x.Email))
                .Select(x => new EmployeeEmail
                {
                    Id = x.Id,
                    FullName = x.FullName,
                    Email = x.Email,
                    IsSelected = false
                }).OrderBy(x => x.FullName).ToList();

            AuditemployeeEmails = EmployeesList
                .Select(x => new EmployeeEmail
                {
                    Id = x.Id,
                    FullName = x.FullName,
                    Email = x.Email,
                    IsSelected = false
                }).OrderBy(x => x.FullName).ToList();

            var timeClockEmployeesId = (await TimeClockEmployeeSettingService.GetEmpIdListAsync(true)).ToList();
            var departmentId = (await employeeService.GetListAsync(true, false, timeClockEmployeesId)).ToList();
            TaskArea = (await departmentService.GetListAsync(false, departmentId))
                .ToList();
            if (ApproverId != null)
            {
                var employee = await employeeService.GetByIdAsync(ApproverId ?? 0);
                EditByEmployeeName = employee.FullName;
            }
        }

        public async Task StartRecTask(TasksRecTasksViewModel RecTask)
        {
            SelectedTaskViewModel = new() { ParentTaskId = null };

            TaskUploadFiles = new();

            if (RecTask == null)
            {
                TaskTitle = "ADD NEW";
                await StartNewTask();
            }
            else
            {
                TaskTitle = "EDIT";
                RecTask = taskService.GetRecurringTaskByIdSync(RecTask.Id);
                PossibleId = RecTask.Id;
                StartEditTask(RecTask);
            }

            if (RecTask != null) await LoadFiles(RecTask);
        }

        protected async Task LoadFiles(TasksRecTasksViewModel TaskModel)
        {
            ImageUpdateModel = TaskModel;
            SelectedTaskViewModel = TaskModel;
            SelectedTaskFiles = (await taskService.GetRecurringTaskImagesAsync(TaskModel.Id)).ToList();
        }

        protected async Task StartNewTask()
        {
            PossibleId = await taskService.GetMaxRecurringId();
            PossibleId++;

            TaskUploadFiles = new();
            RespondBody = "";

            TasksFrequencyListViewModel frequency = await taskService.GetFrequencySync(1);

            if (frequency != null)
            {
                SelectedTaskViewModel = new TasksRecTasksViewModel
                {
                    Frequency = frequency.Frequency,
                    DateCreated = DateTime.Now,
                    StartDate = DateTime.Now,
                    NudgeCount = 0,
                    EmailCount = 0,
                    IsPassOrFail = false,
                    IsQuestionRequired = false,
                    IsGraphRequired = false,
                    IsPicRequired = false,
                    IsDeactivated = false,
                    IsDeleted = false,
                    IsTrendLine = false,
                    ParentTaskId = null,
                    IsProtected = false,
                    IsPicture = false
                };
            }
        }

        protected void StartEditTask(TasksRecTasksViewModel RecTask)
        {
            SelectedTaskViewModel = RecTask;

            string[] splitEmails;
            string[] splitEmailsJobId;
            if (SelectedTaskViewModel.EmailsList != null)
            {
                splitEmails = SelectedTaskViewModel.EmailsList.Split(';');
            }
            else
            {
                splitEmails = new string[0];
            }
            if (SelectedTaskViewModel.EmailsListJobId != null)
            {
                splitEmailsJobId = SelectedTaskViewModel.EmailsListJobId.Split(";");
            }
            else
            {
                splitEmailsJobId = new string[0];
            }
            JobDescriptionWithEmployeeEmails = JobDescriptionsService
                .GetSelectedJobDescriptionWithEmployeeLIst(
                splitEmailsJobId, splitEmails, JobDescriptionWithEmployeeEmails);
            FilterJobDescriptionEmployeeEmails = JobDescriptionWithEmployeeEmails.Where(j => !j.IsSelected).ToList();
            ActiveJobDescriptionWithEmployeeEmails = JobDescriptionWithEmployeeEmails
                .Where(j => j.IsSelected || j.EmployeeEmails.Any(e => e.IsSelected)).ToList();
            ActiveFilterJobDescriptionWithEmployeeEmails = ActiveJobDescriptionWithEmployeeEmails;

            EditAuthorization(SelectedTaskViewModel);
        }

        protected async Task StartTask()
        {
            await Task.Run(() => IsDoingTask = true);
        }
        protected async Task StopTask()
        {
            await Task.Run(() => IsDoingTask = false);
        }

        protected async Task TrySaveTask()
        {
            PageLoadTime.StartTime = DateTime.Now;
            PageLoadTime.SectionName = "SaveRecTask";

            await StartTask();

            if (SelectedTaskViewModel.Id == 0)
            {
                await AddNewTask();
            }
            else
            {
                await EditTask();
            }

            await StopTask();

            await PageLoadTimeCalculation();
        }

        protected async Task<bool> RecurringTaskValidation(TasksRecTasksViewModel task)
        {
            bool IsValid = true;
            RecTaskErrorMessage = null;

            if (string.IsNullOrEmpty(SelectedTaskViewModel.TaskDescriptionSubject))
            {
                await jSRuntime.InvokeVoidAsync("AddRedBox", "TaskDescriptionSubject");
                RecTaskErrorMessage = "Please Fill Task Description Subject Field";
                IsValid = false;
                return IsValid;
            }

            if (string.IsNullOrEmpty(SelectedTaskViewModel.Description))
            {
                await jSRuntime.InvokeVoidAsync("AddRedBox", "TaskDescription");
                RecTaskErrorMessage = "Please Fill Description Field";
                IsValid = false;
                return IsValid;
            }

            if (!JobList.Contains(SelectedTaskViewModel.JobTitle))
            {
                await jSRuntime.InvokeVoidAsync("AddRedBox", "JobId");
                RecTaskErrorMessage = "Please Fill correct Job Title Field";
                IsValid = false;
                return IsValid;
            }

            //Person responsible for recurring task is no longer mandatory field decided by Trinity

            //if (!Employees.Contains(SelectedTaskViewModel.PersonResponsible))
            //{
            //    await jSRuntime.InvokeVoidAsync("AddRedBox", "PersonResponsible");
            //    RecTaskErrorMessage = "Please Fill Person Responsible To Do The Recurring Task Field";
            //    IsValid = false;
            //    return IsValid;
            //}

            if (!Employees.Contains(SelectedTaskViewModel.Initiator))
            {
                await jSRuntime.InvokeVoidAsync("AddRedBox", "Auditor");
                RecTaskErrorMessage = "Please Fill correct Auditor Field";
                IsValid = false;
                return IsValid;
            }

            if (string.IsNullOrEmpty(SelectedTaskViewModel.Initiator))
            {
                await jSRuntime.InvokeVoidAsync("AddRedBox", "Intiator");
                RecTaskErrorMessage = "Please Fill Initiator Field";
                IsValid = false;
                return IsValid;
            }
            else if (!Employees.Contains(SelectedTaskViewModel.Initiator))
            {
                await jSRuntime.InvokeVoidAsync("AddRedBox", "Intiator");
                RecTaskErrorMessage = "Please Fill correct Initiator Field";
                IsValid = false;
                return IsValid;
            }

            if (string.IsNullOrEmpty(SelectedTaskViewModel.Frequency))
            {
                await jSRuntime.InvokeVoidAsync("AddRedBox", "Freqncy");
                RecTaskErrorMessage = "Please Fill Frequency Field";
                IsValid = false;
                return IsValid;
            }
            else if (!Frequencies.Contains(SelectedTaskViewModel.Frequency))
            {
                await jSRuntime.InvokeVoidAsync("AddRedBox", "Freqncy");
                RecTaskErrorMessage = "Please Fill correct Frequency Field";
                IsValid = false;
                return IsValid;
            }
            else if (SelectedTaskViewModel.TasksFreq.Frequency == "TWICE/WEEK")
            {
                if (string.IsNullOrEmpty(SelectedTaskViewModel.StartDueDateDay))
                {
                    RecTaskErrorMessage = "Please fill Start Day!";
                    await jSRuntime.InvokeVoidAsync("AddRedBox", "StatDueDayId");
                    IsValid = false;
                    return IsValid;
                }
                else if (string.IsNullOrEmpty(SelectedTaskViewModel.EndDueDateDay))
                {
                    RecTaskErrorMessage = "Please fill End Day!";
                    await jSRuntime.InvokeVoidAsync("AddRedBox", "EndDueDayId");
                    IsValid = false;
                    return IsValid;
                }
            }

            if (string.IsNullOrEmpty(SelectedTaskViewModel.TaskArea))
            {
                await jSRuntime.InvokeVoidAsync("AddRedBox", "TaskArea");
                RecTaskErrorMessage = "Please Fill Area Field";
                IsValid = false;
                return IsValid;
            }
            else if (!TaskArea.Contains(SelectedTaskViewModel.TaskArea))
            {
                await jSRuntime.InvokeVoidAsync("AddRedBox", "TaskArea");
                RecTaskErrorMessage = "Please Fill correct Area Field";
                IsValid = false;
                return IsValid;
            }

            if (!SelectedTaskViewModel.UpcomingDate.HasValue
                && SelectedTaskViewModel.UpcomingDate < SelectedTaskViewModel.StartDate)
            {
                await jSRuntime.InvokeVoidAsync("AddRedBox", "UpcommingDate");
                RecTaskErrorMessage = "Please Fill Due Date Field";
                IsValid = false;
                return IsValid;
            }

            if (string.IsNullOrEmpty(SelectedTaskViewModel.GraphTitle) && SelectedTaskViewModel.IsGraphRequired == true)
            {
                IsActiveGraphComponent = true;
                RecTaskErrorMessage = "Please set graph title and graph  horizontal axis title..!";
                SelectedTaskForGraph = SelectedTaskViewModel;
                IsValid = false;
                return IsValid;
            }
            else if (string.IsNullOrEmpty(SelectedTaskViewModel.VerticalAxisTitle) && SelectedTaskViewModel.IsGraphRequired == true)
            {
                IsActiveGraphComponent = true;
                RecTaskErrorMessage = "Please set graph title and graph vertical axis title..!";
                SelectedTaskForGraph = SelectedTaskViewModel;
                IsValid = false;
                return IsValid;
            }
            else if ((SelectedTaskViewModel.IsGraphRequired == true
                && SelectedTaskViewModel.IsMaxValueRequired == true)
                && (SelectedTaskViewModel.MaxYAxisValue == null
                || SelectedTaskViewModel.MaxYAxisValue <= 0))
            {
                IsActiveGraphComponent = true;
                RecTaskErrorMessage = "Please set graph Y axis value..!";
                SelectedTaskForGraph = SelectedTaskViewModel;
                IsValid = false;
                return IsValid;
            }

            if (!DocumentDeliverToName.Any(e => e.IsSelected == true) && SelectedTaskViewModel.IsHandDeliveredRequired == true)
            {
                IsActiveFailedEmailComponent = IsDocumentHandDeliveryEnabled = true;
                CheckFailedEmail(SelectedTaskViewModel.Id);
                RecTaskErrorMessage = "PLEASE SELECT EMPLOYEE FOR HAND DELIVERED";
                HandDeliverRequiredToNames = DocumentDeliverToName;
                IsValid = false;
                return IsValid;
            }

            if (string.IsNullOrEmpty(SelectedTaskViewModel.Question) && SelectedTaskViewModel.IsQuestionRequired == true)
            {
                IsActiveQuestionComponent = true;
                RecTaskErrorMessage = "Please ask question..!";
                SelectedTaskForQuestion = SelectedTaskViewModel;
                IsValid = false;
                return IsValid;
            }

            if (string.IsNullOrEmpty(SelectedTaskViewModel.UpdateImageDescription) && SelectedTaskViewModel.IsPicRequired == true)
            {
                IsActiveSampleImageComponent = true;
                RecTaskErrorMessage = "Please set image description..!";
                SelectedTaskForSampleImage = SelectedTaskViewModel;
                IsValid = false;
                return IsValid;
            }

            if (!AuthorizationList.Any(e => e.IsSelected == true) && SelectedTaskViewModel.IsProtected == true)
            {
                RecTaskErrorMessage = "Please select at last one employee..!";
                IsValid = false;
                return IsValid;
            }

            return IsValid;

        }

        private bool IsFileValid(FileInfo fileInfo)
        {
            if (!fileManagerService.IsValidSize(fileInfo.Size) ||
                !(fileManagerService.IsFile(fileInfo.Name) ||
                 fileManagerService.IsImage(fileInfo.Name)))
            {
                var size = Convert.ToDouble(fileInfo.Size) / 1000000;
                RecTaskErrorMessage = $"Image size can not be more than 20 mb. Your uploaded image size is {size} mb or you are uploading the wrong file.";
                return false;
            }
            return true;
        }

        private async Task CheckUploadFiles()
        {
            if (TaskUploadFiles.Any())
            {
                foreach (var file in TaskUploadFiles)
                {
                    if (file.FileInfo != null)
                    {
                        if (!IsFileValid(file.FileInfo))
                        {
                            return;
                        };

                        var ImageFileName = $"{Guid.NewGuid()}.{file.FileInfo.Type}";
                        var ResultPath = fileManagerService.CreateRecurringTaskDirectory(SelectedTaskViewModel.Id.ToString()) + ImageFileName;
                        fileManagerService.WriteToFile(file.Stream, ResultPath);
                        SelectedTaskViewModel.IsPicture = true;

                        string fileName = string.Empty;
                        if (!string.IsNullOrEmpty(file.FileInfo.Name))
                        {
                            fileName = file.FileInfo.Name;
                            int dotIndx = fileName.IndexOf($".{file.FileInfo.Type}");

                            if (dotIndx >= 0)
                            {
                                fileName = fileName.Substring(0, dotIndx);
                            }
                        }

                        TasksImagesViewModel TM = new TasksImagesViewModel()
                        {
                            PictureLink = ResultPath,
                            RecurringId = SelectedTaskViewModel.Id,
                            IsDeleted = false,
                            OneTimeId = 0,
                            FileName = fileName,
                            ImageNote = file.ImageNote
                        };
                        await taskService.InsertTaskImageAsync(TM);

                    }
                }
            }

            if (HowToFileUpload != null)
            {
                if (!IsFileValid(HowToFileUpload.FileInfo))
                {
                    return;
                };

                var FileName = $"{Guid.NewGuid()}.{HowToFileUpload.FileInfo.Type}";
                var ResultPath = fileManagerService.CreateRecurringTaskDirectory(SelectedTaskViewModel.Id.ToString()) + FileName;
                fileManagerService.WriteToFile(HowToFileUpload.Stream, ResultPath);

                SelectedTaskViewModel.InstructionFileLink = ResultPath;
            }

            if (SelectedTaskViewModel.IsPicRequired == true)
            {
                var file = SampleuploadFiles?.FirstOrDefault();

                if (file?.FileInfo != null)
                {
                    if (!IsFileValid(file.FileInfo))
                    {
                        return;
                    };

                        var ImageFileName = $"{Guid.NewGuid()}.{file.FileInfo.Type}";
                        var ResultPath = fileManagerService.CreateRecurringTaskDirectory(SelectedTaskViewModel.Id.ToString()) + ImageFileName;
                        fileManagerService.WriteToFile(file.Stream, ResultPath);
                        SelectedTaskViewModel.UpdateImageLoction = ResultPath;
                }
            }

            if(UpdatedDocumentUploadFile.FileInfo != null && UpdatedDocumentUploadFile.Stream != null && SelectedTaskViewModel.IsDocumentRequired == true)
            {
                if (!IsFileValid(UpdatedDocumentUploadFile.FileInfo))
                {
                    return;
                };

                    var FileName = $"{Guid.NewGuid()}.{UpdatedDocumentUploadFile.FileInfo.Type}";
                    var ResultPath = FileManagerService.CreateRecurringTaskDirectory(SelectedTaskViewModel.Id.ToString()) + FileName;
                    FileManagerService.WriteToFile(UpdatedDocumentUploadFile.Stream, ResultPath);
                    SelectedTaskViewModel.UpdatedDocumentLink = ResultPath;
            }

        }

        private void CheckData()
        {
            if (SelectedTaskViewModel.IsProtected.HasValue
                    && SelectedTaskViewModel.IsProtected.Value
                    && AuthorizationList.Any())
            {
                string employeeConcat = "";
                foreach (var emp in AuthorizationList)
                {
                    if (emp.IsSelected)
                        employeeConcat += Convert.ToString(emp.Email) + ";";
                }

                if (!string.IsNullOrEmpty(employeeConcat))
                    employeeConcat = employeeConcat.Remove(employeeConcat.Length - 1);

                SelectedTaskViewModel.AuthorizationList = employeeConcat;
            }

            var SelectedJobEmails = FilterJobDescriptionEmployeeEmails
                .Where(x => x.IsSelected || x.EmployeeEmails.Any(e => e.IsSelected))
                .OrderBy(x => x.JobName)
                .Concat(ActiveFilterJobDescriptionWithEmployeeEmails)
                .DistinctBy(x => x.Id)
                .ToList();
            if (SelectedJobEmails.Any())
            {
                SelectedTaskViewModel.EmailsListJobId = string
                    .Join(";", SelectedJobEmails.Select(j => j.Id.ToString()));
                SelectedTaskViewModel.EmailsList = string
                    .Join(";", SelectedJobEmails
                    .SelectMany(j => j.EmployeeEmails)
                    .Where(e => e.IsSelected)
                    .Select(e => e.Email));
            }

            if (SelectedTaskViewModel.IsHandDeliveredRequired == true)
            {
                var SelectedHandDeliverDocumentTo = DocumentDeliverToName.Where(x => x.IsSelected).OrderBy(x => x.FullName).ToList();
                if (SelectedHandDeliverDocumentTo.Any())
                {
                    string selectedHandDeliverToConcat = "";
                    foreach (var name in SelectedHandDeliverDocumentTo)
                    {
                        selectedHandDeliverToConcat += name.FullName + ";";
                    }

                    if (!string.IsNullOrEmpty(selectedHandDeliverToConcat))
                        selectedHandDeliverToConcat = selectedHandDeliverToConcat.Remove(selectedHandDeliverToConcat.Length - 1);

                    SelectedTaskViewModel.HandDocumentDeliverTo = selectedHandDeliverToConcat;
                }
            }
        }

        protected async Task AddNewTask()
        {
            RecTaskErrorMessage = null;

            try
            {
                if (IsUploading)
                {
                    RecTaskErrorMessage = "Please Waitt For Uploading Files";
                    return;
                }

                if (!await RecurringTaskValidation(SelectedTaskViewModel))
                    return;

                if (SelectedTaskViewModel.IsQuestionRequired == false)
                    SelectedTaskViewModel.Question = "";

                TasksFrequencyListViewModel frequency = await taskService.GetFrequencySync(SelectedTaskViewModel.Frequency);
                SelectedTaskViewModel.TasksFreq = frequency;
                
                if(SelectedTaskViewModel.TaskStartDueDate != null && SelectedTaskViewModel.TaskEndDueDate != null)
                {
                    bool isTaskDuePeriod = IsYearDuePeriod();
                    SelectedTaskViewModel.IsTaskDuePeriod = isTaskDuePeriod;
                }

                if (SelectedTaskViewModel.Id == 0)
                {
                    SelectedTaskViewModel = await taskService.AddRecurringTask(SelectedTaskViewModel);
                }

                await CheckUploadFiles();

                CheckData();

                SelectedTaskViewModel.IsDeleted = false;
                SelectedTaskViewModel.IsDeactivated = false;
                SelectedTaskViewModel.IsApproved = false;
                SelectedTaskViewModel.NudgeCount = 0;

                string resultPath = string.Empty;

                var recurringScreenshotBase64 =
                        await jSRuntime.InvokeAsync<string>("TakeScreenshotAndCopy", "RecurringTaskModal");

                if (!string.IsNullOrEmpty(recurringScreenshotBase64))
                {
                    string[] data = recurringScreenshotBase64.Split(",");

                    string time = DateTime.Now.TimeOfDay.ToString();
                    time = time.Replace(".", "_").Replace(":", "_");

                    string imageFileName = Convert.ToString(SelectedTaskViewModel.Id) + "_"
                        + time + ".PNG";
                    resultPath = FileManagerService.CreateRecurringTaskDirectory(SelectedTaskViewModel.Id.ToString()) + imageFileName;

                    byte[] bytes = Convert.FromBase64String(data[1]);
                    Stream stream = new MemoryStream(bytes);

                    FileManagerService.WriteToFile((MemoryStream)stream, resultPath);
                }
                if (SelectedTaskViewModel.InstructionFileLink != null)
                {
                    SelectedTaskViewModel.InstructionFileLink = SelectedTaskViewModel.InstructionFileLink + ":" + resultPath;
                }
                else
                {
                    SelectedTaskViewModel.InstructionFileLink = resultPath;
                }
                await SendEmail(SelectedTaskViewModel, false);

                await taskService.UpdateRecurringTask(SelectedTaskViewModel);

                if (SelectedTaskViewModel.Id != 0)
                {
                    await jSRuntime.InvokeAsync<object>("HideModal", "#RecurringTaskModal");

                    await RefreshParentComponent.InvokeAsync(SelectedTaskViewModel.Id);
                    await SendMessageToParent.InvokeAsync("Task Added Successfully");
                    await CloseCreateRecurringTaskComponent.InvokeAsync();
                }
            }
            catch (Exception ex)
            {
                if (ex != null)
                {
                    logger.LogError(ex, "Add new recurring task errror:", ex);
                }
                else
                {
                    logger.LogError("Add new recurring task errror.");
                }
            }
        }

        protected bool IsYearDuePeriod()
        {
            var upcomingDate = SelectedTaskViewModel.UpcomingDate.Value;
            var fromDate = SelectedTaskViewModel.TaskStartDueDate.Value;
            var toDate = SelectedTaskViewModel.TaskEndDueDate.Value;

            // Extract month and day for comparison
            var fromMonthDay = fromDate.ToString("MM-dd");
            var toMonthDay = toDate.ToString("MM-dd");
            var upcomingMonthDay = upcomingDate.ToString("MM-dd");

            if (string.Compare(fromMonthDay, toMonthDay) <= 0)
            {
                if (string.Compare(upcomingMonthDay, fromMonthDay) >= 0 && string.Compare(upcomingMonthDay, toMonthDay) <= 0)
                {
                    return true;
                }
                else if (String.Compare(upcomingMonthDay, fromMonthDay) < 0)
                {
                    SelectedTaskViewModel.UpcomingDate = new DateTime(upcomingDate.Year, fromDate.Month, fromDate.Day);
                    return false;
                }
                else
                {
                    SelectedTaskViewModel.UpcomingDate = new DateTime(upcomingDate.Year + 1, fromDate.Month, fromDate.Day);
                    return false;
                }
            }
            else
            {
                if (string.Compare(upcomingMonthDay, fromMonthDay) >= 0 || string.Compare(upcomingMonthDay, toMonthDay) <= 0)
                {
                    return true;
                }
                else if(string.Compare(upcomingMonthDay, fromMonthDay) < 0)
                {
                     SelectedTaskViewModel.UpcomingDate = new DateTime(upcomingDate.Year, fromDate.Month, fromDate.Day);
                     return false;
                }
                else
                {
                    return false;
                }
            }
        }
        protected async Task EditTask()
        {
            RecTaskErrorMessage = null;

            try
            {
                if (IsUploading)
                {
                    RecTaskErrorMessage = "Please Wait For Uploading Files";
                    return;
                }

                if (!await RecurringTaskValidation(SelectedTaskViewModel))
                    return;

                TasksFrequencyListViewModel frequency = await taskService.GetFrequencySync(SelectedTaskViewModel.Frequency);
                SelectedTaskViewModel.TasksFreq = frequency;

                if (SelectedTaskViewModel.IsQuestionRequired == null || SelectedTaskViewModel.IsQuestionRequired == false)
                    SelectedTaskViewModel.Question = String.Empty;

                await CheckUploadFiles();
               
                if (!SelectedTaskViewModel.IsApproved && ApproverId != null)
                {
                    SelectedTaskViewModel.IsApproved = true;
                    SelectedTaskViewModel.ApprovedByEmployeeId = ApproverId;
                    SelectedTaskViewModel.DateApproved = DateTime.Now.Date;
                }
                if (!string.IsNullOrEmpty(EditByEmployeeName))
                {
                    SelectedTaskViewModel.EditBy = EditByEmployeeName;
                }
                if(SelectedTaskViewModel.TaskStartDueDate == null || SelectedTaskViewModel.TaskEndDueDate == null)
                {
                    SelectedTaskViewModel.IsTaskDuePeriod = false;
                }
                if (CapturedImgaesList.Any())
                {
                    foreach (var Img in CapturedImgaesList)
                    {
                        var Base64Image = Img.Value.Replace("data:image/jpeg;base64,", "");
                        byte[] ImgBytes = Convert.FromBase64String(Base64Image);

                        var Imgstream = new MemoryStream(ImgBytes);

                        var ImageFileName = $"{Guid.NewGuid()}.jpeg";
                        var ResultPath = fileManagerService.CreateRecurringTaskDirectory(SelectedTaskViewModel.Id.ToString()) + ImageFileName;
                        fileManagerService.WriteToFile(Imgstream, ResultPath);
                        SelectedTaskViewModel.PictureLink = ResultPath;

                        TasksImagesViewModel TM = new TasksImagesViewModel()
                        {
                            PictureLink = ResultPath,
                            RecurringId = SelectedTaskViewModel.Id,
                            IsDeleted = false,
                            OneTimeId = 0
                        };
                        await taskService.InsertTaskImageAsync(TM);
                    }
                }
                CapturedImgaesList = new();

                CheckData();

                await SendEmail(SelectedTaskViewModel, true);

                var UpdatedTask = taskService.UpdateRecurringTaskSync(SelectedTaskViewModel);

                await jSRuntime.InvokeAsync<object>("HideModal", "#RecurringTaskModal");

                await RefreshParentComponent.InvokeAsync(SelectedTaskViewModel.Id);
                await SendMessageToParent.InvokeAsync("Task Updated Successfully");
                await CloseCreateRecurringTaskComponent.InvokeAsync();
            }
            catch (Exception ex)
            {
                if (ex != null)
                {
                    logger.LogWarning(ex, "Task Update Error:", ex);
                }
                else
                {
                    logger.LogWarning("Task Update Error", ex.Message);
                }
            };
        }

        protected async Task CloseComponent(KeyboardEventArgs eventArgs)
        {
            if (eventArgs == null || eventArgs.Code == "Escape")
            {
                await jSRuntime.InvokeAsync<object>("HideModal", "#RecurringTaskModal");

                await CloseCreateRecurringTaskComponent.InvokeAsync();
            }
        }

        protected async Task SendEmail(TasksRecTasksViewModel Newtask, bool IsEditing)
        {
            string emailSubject;
            bool isSend = false;

            if (IsEditing)
            {
                emailSubject = "A TASK HAS BEEN MODIFIED";
                isSend = await taskService.SendEmail(Newtask, IsEditing, navigationManager.BaseUri, emailSubject);
            }
            else
            {
                emailSubject = "A NEW TASK HAS BEEN CREATED";
                isSend = await taskService.SendEmail(Newtask, IsEditing, navigationManager.BaseUri, emailSubject);
            }

            if (isSend)
            {
                await SendMessageToParent.InvokeAsync("An Email Has Been Added To Queue");
            }
            else
            {
                await SendMessageToParent.InvokeAsync("Email Sending Error, Employee Has No Email");
            }
        }

        protected async Task RemoveTaskPreImage(BeforeRemoveEventArgs args)
        {
            if (args.FilesData[0] == null)
                return;

            var RemoveFile = TaskUploadFiles.FirstOrDefault(x => x.FileInfo?.Id == args.FilesData[0].Id);
            if (RemoveFile != null)
            {
                TaskUploadFiles = TaskUploadFiles.Where(x => x.FileInfo?.Id != args.FilesData[0].Id).ToList();
                await Task.Run(() => RemoveFile.FileInfo = null);
            }
        }

        public async Task DeleteSuccessFromDeleteFileComponent(TasksImagesViewModel model)
        {
            if (model != null)
            {
                SelectedTaskFiles.Remove(model);

                if (!SelectedTaskFiles.Any())
                {
                    if (ImageUpdateModel != null)
                    {
                        ImageUpdateModel.IsPicture = false;
                        await taskService.UpdateRecurringTask(ImageUpdateModel);

                        await RefreshParentComponent.InvokeAsync();
                    }
                }
            }
        }

        protected async Task DeleteSuccessFromDeleteFileComponent(string path)
        {
            if (!string.IsNullOrEmpty(path))
            {
                if (TaskUploadFiles.Any())
                {
                    var File = TaskUploadFiles[TaskUploadFiles.FindIndex(x => x.FileInfo?.Id == path)].FileInfo;
                    var RemoveList = new FileInfo[1] { File };
                    await TaskUploadRef.RemoveAsync(RemoveList);
                }
            }
        }

        protected async Task DeleteSuccessFromDeleteFileComponentForHowToFile(string path)
        {
            if (!string.IsNullOrEmpty(path))
            {
                await HowToUploadRef.RemoveAsync(new FileInfo[1] { HowToFileUpload.FileInfo });
                HowToFileUpload = null;
            }
        }

        protected void BeforeUploadTaskImage()
        {
            IsUploading = true;
        }

        protected void RemoveHowToFile(BeforeRemoveEventArgs args)
        {
            if (args.FilesData[0] == null)
                return;

            if (HowToFileUpload != null && HowToFileUpload.FileInfo.Id == args.FilesData[0].Id)
            {
                HowToFileUpload = null;
            }

        }

        protected async Task PrintingImage()
        {
            await jSRuntime.InvokeAsync<object>("PrintImage", fileManagerService.ConvertImagetoBase64(TaskImage));
        }

        protected async Task PrintingImage(string FilePath)
        {
            await jSRuntime.InvokeAsync<object>("PrintImage", FilePath);
        }

        protected void RemoveUpdatePreImage(BeforeRemoveEventArgs args)
        {
            if (args.FilesData[0] == null)
                return;

            var RemoveFile = UpdateUploadFiles.FirstOrDefault(x => x.FileInfo?.Id == args.FilesData[0].Id);
            if (RemoveFile != null)
            {
                RemoveFile.FileInfo = null;
            }

        }

        protected void BeforeUploadImage()
        {
            IsUploading = true;
        }

        protected void SelectTaskImages(UploadChangeEventArgs args)
        {
            foreach (var file in args.Files)
            {
                var fileLookUp = TaskUploadFiles.FirstOrDefault(x => x.FileInfo != null && file.FileInfo.Id == x.FileInfo.Id);
                if (fileLookUp == null)
                {
                    TaskUploadFiles.Add(new TasksImagesViewModel()
                    {
                        FileInfo = file.FileInfo,
                        ImageNote = string.Empty,
                        Stream = file.Stream,
                    });
                }

            }

            IsUploading = false;
        }

        protected async Task ActivePastPicture(string inputName)
        {
            var _filePasteModule = await jSRuntime
                .InvokeAsync<IJSObjectReference>("import", "./js/PastePicture.js");

            await _filePasteModule.InvokeVoidAsync(
                "clearCookie", "PastPicture");

            await _filePasteModule.InvokeVoidAsync(
                "SetCookie", "PastPicture", inputName);

            KeboardKeyManager.SimulateKeyStroke('v', ctrl: true);

            await _filePasteModule.InvokeVoidAsync(
                "ActivePicturePastButton");
        }

        protected void UploadFileNoteComponentSucessMessage(string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                var item = TaskUploadFiles.FirstOrDefault(x => x.FileInfo.Id == UploadFileId);

                if (item != null)
                {
                    item.ImageNote = message;
                    int indexId = TaskUploadFiles.IndexOf(item);

                    if (indexId >= 0)
                    {
                        TaskUploadFiles[indexId] = item;
                    }
                }
            }
        }

        protected void SelectHowToFile(UploadChangeEventArgs args)
        {
            foreach (var file in args.Files)
            {
                var fileLookUp = TaskUploadFiles.FirstOrDefault(x => x.FileInfo != null && file.FileInfo.Id == x.FileInfo.Id);
                if (fileLookUp == null)
                    HowToFileUpload = new TasksImagesViewModel()
                    {
                        FileInfo = file.FileInfo,
                        ImageNote = string.Empty,
                        Stream = file.Stream
                    };
            }

            IsUploading = false;
        }

        public void PreventESC(KeyboardEventArgs args)
        {
            if (args.Code == "Escape")
            {
                _toastService.ShowError("ERR");
            }
        }

        protected async Task StartModifyingImageNote(TasksImagesViewModel model)
        {
            if (model != null)
            {
                await taskService.UpdateTaskImageAsync(model);
                _toastService.ShowSuccess("Successfully Saved");
            }
        }

        protected async Task StartDownloadFile(string filePath)
        {
            var OriginalPath = filePath;
            if (!File.Exists(OriginalPath))
            {
                _toastService.ShowError("Fie Doesn't Exist, Deleted or have been moved");
                return;
            }
            await blazorDownloadFileService.DownloadFile(Path.GetFileName(OriginalPath), File.ReadAllBytes(OriginalPath), "application/octet-stream");
        }

        protected void CheckFailedEmail(int id)
        {
            TasksRecTasksViewModel task = taskService.GetRecurringTaskByIdSync(id);

            DocumentDeliverToName = DocumentDeliverToName.Select(x => new EmployeeEmail
            {
                FullName = x.FullName,
                Email = x.Email,
                IsSelected = false
            }).OrderBy(x => x.FullName).ToList();

            if (task != null && !string.IsNullOrEmpty(task.HandDocumentDeliverTo) && task.IsHandDeliveredRequired == true)
            {
                string[] splitEmails = task.HandDocumentDeliverTo.Split(';');
                if (splitEmails.Any())
                {
                    foreach (var name in DocumentDeliverToName)
                    {
                        if (splitEmails.Contains(name.FullName))
                            name.IsSelected = true;
                    }
                }
            }
        }
        protected void SetViewModelHandDeliverDocument(TasksRecTasksViewModel model, bool IsRequired)
        {
            if (IsRequired)
            {
                model.IsHandDeliveredRequired = IsRequired;
                IsDocumentHandDeliveryEnabled = true;
                CheckFailedEmail(model.Id);

                IsActiveFailedEmailComponent = true;
                HandDeliverRequiredToNames = DocumentDeliverToName;
            }
            else
            {
                SelectedTaskViewModel.IsHandDeliveredRequired = IsRequired;
                SelectedTaskViewModel.HandDocumentDeliverTo = null;
            }
        }
        protected async Task SetDocumentRequiredToUpdated(TasksRecTasksViewModel model, bool IsRequired)
        {
            if (IsRequired)
            {
                model.IsDocumentRequired = IsRequired;
                await jSRuntime.InvokeAsync<object>("ShowModal", "#documentRequiredUpdate");
            }
            else
            {
                SelectedTaskViewModel.IsDocumentRequired = IsRequired;
            }
        }

        protected void SelectRequiredDocument(UploadChangeEventArgs args)
        {
            foreach (var file in args.Files)
            {
                if (UpdatedDocumentUploadFile.FileInfo == null && UpdatedDocumentUploadFile.Stream == null)
                    UpdatedDocumentUploadFile = new TasksImagesViewModel()
                    {
                        FileInfo = file.FileInfo,
                        ImageNote = string.Empty,
                        Stream = file.Stream
                    };
            }
            SelectedTaskViewModel.UpdatedDocumentLink = "";
            IsUploading = false;
        }
        protected void RemoveRequiredDocument()
        {
            SelectedTaskViewModel.UpdatedDocumentLink = null;
        }
        protected async Task CheckMandatoryFields()
        {
            if (SelectedTaskViewModel.UpdatedDocumentLink != null && SelectedTaskViewModel.UpdatedDocumentDescription != null)
            {
                await jSRuntime.InvokeAsync<object>("HideModal", "#documentRequiredUpdate");
            }
            else if(SelectedTaskViewModel.UpdatedDocumentLink == null)
            {
                ErrorMessage = "PLEASE UPLOAD CX FORM/PDF";
            }
            else if (SelectedTaskViewModel.UpdatedDocumentDescription == null)
            {
                ErrorMessage = "PLEASE ADD DESCRIPTION FOR CX FORM/PDF";
            }
        }

        protected async void CloseDocumentRequired()
        {
            SelectedTaskViewModel.IsDocumentRequired = false;
            await jSRuntime.InvokeAsync<object>("HideModal", "#documentRequiredUpdate");
        }
        protected void SuccessMessageFromFaildEmailComponent(List<EmployeeEmail> faildEmployeeEmail)
        {
            if (faildEmployeeEmail != null)
            {
                    DocumentDeliverToName = faildEmployeeEmail;
            }

            IsActiveFailedEmailComponent = false;
        }

        protected void SetViewModelGraph(TasksRecTasksViewModel model, bool IsRequired)
        {
            if (IsRequired)
            {
                IsActiveGraphComponent = true;
                model.IsGraphRequired = IsRequired;
                SelectedTaskForGraph = model;
            }
            else
            {
                SelectedTaskViewModel.IsGraphRequired = IsRequired;
                SelectedTaskViewModel.GraphTitle = null;
                SelectedTaskViewModel.VerticalAxisTitle = null;
            }
        }

        protected void EditGraph(TasksRecTasksViewModel model)
        {
            IsActiveGraphComponent = true;
            SelectedTaskForGraph = model;
        }

        protected void SuccessCallbackFromGraphComponent(TasksRecTasksViewModel model)
        {
            if (model != null)
            {
                SelectedTaskViewModel.IsGraphRequired = model.IsGraphRequired;
                SelectedTaskViewModel.GraphTitle = model.GraphTitle;
                SelectedTaskViewModel.VerticalAxisTitle = model.VerticalAxisTitle;
                SelectedTaskViewModel.IsMaxValueRequired = model.IsMaxValueRequired;
                SelectedTaskViewModel.MaxYAxisValue = model.MaxYAxisValue;
            }

            IsActiveGraphComponent = false;
        }

        protected void SetQuestionViewModel(TasksRecTasksViewModel model, bool IsRequired)
        {
            if (IsRequired)
            {
                IsActiveQuestionComponent = true;
                SelectedTaskForQuestion = model;
            }
            else
            {
                SelectedTaskViewModel.IsQuestionRequired = false;
                SelectedTaskViewModel.Question = null;
            }
        }

        protected void successMessageFromQuestionComponent(TasksRecTasksViewModel model)
        {
            if (model != null)
            {
                SelectedTaskViewModel.IsQuestionRequired = model.IsQuestionRequired;
                SelectedTaskViewModel.Question = model.Question;
            }

            IsActiveQuestionComponent = false;
        }

        protected void SetImageViewModel(TasksRecTasksViewModel model, bool IsRequired)
        {
            if (IsRequired)
            {
                IsActiveSampleImageComponent = true;
                SelectedTaskForSampleImage = model;
            }
            else
            {
                SelectedTaskViewModel.IsPicRequired = IsRequired;
                SelectedTaskViewModel.UpdateImageDescription = null;
            }
        }

        protected void SuccessMessageFromSampleImageComponent(TasksRecTasksViewModel model)
        {
            if (model != null)
            {
                SelectedTaskViewModel.IsPicRequired = model.IsPicRequired;
                SelectedTaskViewModel.UpdateImageDescription = model.UpdateImageDescription;
            }

            IsActiveSampleImageComponent = false;
        }

        protected void MarkCompleteCheck(ChangeEventArgs args, EmployeeEmail e) => e.IsSelected = (bool)args.Value;

        protected void MarkCompleteCheck(ChangeEventArgs args, TasksRecTasksViewModel e) => e.IsDeactivated = (bool)args.Value;

        protected void EditAuthorization(TasksRecTasksViewModel task)
        {
            if (task != null)
            {
                if (!string.IsNullOrEmpty(task.AuthorizationList))
                {
                    string[] splitAuthorizationList = task.AuthorizationList.Split(";");

                    if (splitAuthorizationList.Any())
                    {
                        foreach (var item in AuthorizationList)
                        {
                            if (splitAuthorizationList.Contains(item.Email))
                                item.IsSelected = true;
                        }
                    }
                }
            }
        }

        protected void SelectJobTitleEmailDistibution(ChangeEventArgs args, JobDescriptionEmployeeEmail e)
        {
            if (args == null || e == null) return;

            bool isResult = (bool)args.Value;
            int indexJob = JobDescriptionWithEmployeeEmails.FindIndex(x => x.Id == e.Id);
            if (indexJob >= 0)
            {
                var job = JobDescriptionWithEmployeeEmails[indexJob];

                if (job != null)
                {
                    JobDescriptionWithEmployeeEmails[indexJob].IsSelected = isResult;
                    var employee = job.EmployeeEmails;

                    if (!employee.Any())
                    {
                        if (e.IsSelected)
                            _toastService.ShowWarning("No Employee Under The Job...");
                    }

                    if (employee.Any())
                    {
                        JobDescriptionWithEmployeeEmails[indexJob]
                            .EmployeeEmails[0].IsSelected = isResult;
                    }
                    int indexActiveFilterJob = ActiveFilterJobDescriptionWithEmployeeEmails.FindIndex(x => x.Id == e.Id);
                    if (indexActiveFilterJob >= 0)
                    {   
                        ActiveFilterJobDescriptionWithEmployeeEmails[indexActiveFilterJob].IsSelected = isResult;
                    }
                    else if (isResult)
                    {
                        ActiveFilterJobDescriptionWithEmployeeEmails.Add(job);
                    }

                    int indexFilterJob = FilterJobDescriptionEmployeeEmails
                        .FindIndex(x => x.Id == e.Id);
                    if (indexFilterJob >= 0)
                    {
                        FilterJobDescriptionEmployeeEmails[indexFilterJob].IsSelected = isResult;
                    }

                }
            }
        }

        protected void SelectActiveJobTitleEmailDistibution(ChangeEventArgs args, JobDescriptionEmployeeEmail Je)
        {
            if (args == null || Je == null) return;

            bool isResult = (bool)args.Value;
            int indexJob = ActiveJobDescriptionWithEmployeeEmails.FindIndex(x => x.Id == Je.Id);
            if (indexJob >= 0)
            {
                FilterJobDescriptionEmployeeEmails = FilterJobDescriptionEmployeeEmails
                    .Select(x => new JobDescriptionEmployeeEmail
                    {
                        Id = x.Id,
                        JobName = x.JobName,
                        IsSelected = x.IsSelected,
                        EmployeeEmails = x.EmployeeEmails.ToList(),
                    })
                     .ToList();

                ActiveJobDescriptionWithEmployeeEmails[indexJob].IsSelected = isResult;

                int indexFilterjob = ActiveFilterJobDescriptionWithEmployeeEmails
                        .FindIndex(x => x.Id == Je.Id);
                if (indexFilterjob >= 0)
                {
                    ActiveFilterJobDescriptionWithEmployeeEmails[indexFilterjob].IsSelected = isResult;
                }
            }
        }

        void RemoveItem(JobDescriptionEmployeeEmail item)
        {
            if (item == null) return;
            ActiveFilterJobDescriptionWithEmployeeEmails.Remove(item);
            int jobIndex = JobDescriptionWithEmployeeEmails.FindIndex(j => j.Id == item.Id);
            if (jobIndex >= 0)
            {
                JobDescriptionWithEmployeeEmails[jobIndex].IsSelected = false;
                JobDescriptionWithEmployeeEmails[jobIndex].EmployeeEmails = JobDescriptionWithEmployeeEmails[jobIndex].EmployeeEmails.Select(employeeEmail =>
                {
                    employeeEmail.IsSelected = false;
                    return employeeEmail;
                }).ToList();
                FilterJobDescriptionEmployeeEmails = JobDescriptionWithEmployeeEmails.Where(j => !j.IsSelected).ToList();
            }
            item.IsSelected = false;
            item.EmployeeEmails = item.EmployeeEmails.Select(employeeEmail =>
            {
                employeeEmail.IsSelected = false;
                return employeeEmail;
            }).ToList();
        }

        protected async Task EmailDistibutionFilter(ChangeEventArgs args)
        {
            await Task.Run(() => IsFilter = true);
            string key = Convert.ToString(args.Value).ToUpper();

            JobDescriptionWithEmployeeEmails = JobDescriptionWithEmployeeEmails
                .Select(x => new JobDescriptionEmployeeEmail
                {
                    Id = x.Id,
                    JobName = x.JobName,
                    IsSelected = false,
                    EmployeeEmails = x.EmployeeEmails.ToList(),
                }).ToList();

            if (string.IsNullOrEmpty(key))
            {
                FilterJobDescriptionEmployeeEmails = JobDescriptionWithEmployeeEmails;
            }
            else
            {
                FilterJobDescriptionEmployeeEmails = JobDescriptionWithEmployeeEmails
                    .Where(j => j.JobName.Contains(key) || j.EmployeeEmails.Any(e => e.FullName.Contains(key))).ToList();
            }

            await Task.Run(() => IsFilter = false);
        }

        protected async Task ActiveEmailDistibutionFilter(ChangeEventArgs args)
        {
            await Task.Run(() => IsActiveJobDescriptionFilter = true);
            string key = Convert.ToString(args.Value).ToUpper();

            ActiveJobDescriptionWithEmployeeEmails = ActiveJobDescriptionWithEmployeeEmails
                .Select(x => new JobDescriptionEmployeeEmail
                {
                    Id = x.Id,
                    JobName = x.JobName,
                    IsSelected = false,
                    EmployeeEmails = x.EmployeeEmails.ToList(),
                }).ToList();

            if (string.IsNullOrEmpty(key))
            {
                ActiveFilterJobDescriptionWithEmployeeEmails = ActiveJobDescriptionWithEmployeeEmails;
            }
            else
            {
                ActiveFilterJobDescriptionWithEmployeeEmails = ActiveJobDescriptionWithEmployeeEmails
                    .Where(x => x.JobName.Contains(key)
                    || x.EmployeeEmails.Any(e => e.FullName.Contains(key))).ToList();
            }

            await Task.Run(() => IsActiveJobDescriptionFilter = false);
        }

        protected void SelectEmailDistibution(ChangeEventArgs args, long jobId, EmployeeEmail e)
        {
            if (args == null || e == null) return;

            bool isResult = (bool)args.Value;
            int indexJob = JobDescriptionWithEmployeeEmails.FindIndex(j => j.Id == jobId);
            if (indexJob >= 0)
            {
                int indexEmployee = JobDescriptionWithEmployeeEmails[indexJob]
                    .EmployeeEmails.FindIndex(x => x.Id == e.Id);
                if (indexJob >= 0)
                {
                    JobDescriptionWithEmployeeEmails[indexJob]
                         .EmployeeEmails[indexEmployee].IsSelected = isResult;

                    int indexFilterjob = FilterJobDescriptionEmployeeEmails
                        .FindIndex(x => x.Id == jobId);
                    if (indexFilterjob >= 0)
                    {
                        int indexFilterEmployee = FilterJobDescriptionEmployeeEmails[indexFilterjob]
                            .EmployeeEmails.FindIndex(x => x.Id == e.Id);

                        if (indexFilterEmployee >= 0)
                        {
                            FilterJobDescriptionEmployeeEmails[indexFilterjob]
                            .EmployeeEmails[indexFilterEmployee].IsSelected = isResult;
                        }
                    }
                }
            }
        }

        protected void ChangeIsPositionSpecific(bool isSwitch)
        {
            if (isSwitch)
                SelectedTaskViewModel.PersonResponsible = "";
        }

        protected void ChangeFrequencyAsync(ChangeEventArgs<string, string> args)
        {
            if (args == null) return;
            if (SelectedTaskViewModel.Frequency == "TWICE/WEEK (RANDOM)" || SelectedTaskViewModel.Frequency == "WEEKLY (RANDOM)"
                || SelectedTaskViewModel.Frequency == "TWICE/MONTHLY (RANDOM)" || SelectedTaskViewModel.Frequency == "MONTHLY (RANDOM)")
            {
                SelectedTaskViewModel.IsTaskRandomize = true;
            }
            else
            {
                SelectedTaskViewModel.IsTaskRandomize = false;
            }

        }

        protected async Task ChangeJobTitleAsync(ChangeEventArgs<string, string> args)
        {
            if (args == null) return;

            var job = await JobDescriptionsService.GetByNameAsyncs(args.Value);

            if (job == null) return;


            EmployeesByJobId = JobDescriptionWithEmployeeEmails.FirstOrDefault(x => x.Id == job.Id).EmployeeEmails.Select(x => x.FullName).ToList();

            if (EmployeesByJobId.Count == 0)
            {
                EmployeesByJobId = EmployeesList.Where(employee => employee.JobId == job.Id).Select(employee => employee.FullName).OrderBy(employee => employee).Distinct().ToList();
                SelectedTaskViewModel.PersonResponsible = EmployeesByJobId.FirstOrDefault();
                return;
            }

            SelectedTaskViewModel.PersonResponsible = EmployeesByJobId.FirstOrDefault();
        }
        protected async Task SearchEmployeeJobTitle(ChangeEventArgs<string, string> args)
        {
            if (args == null) return;

            var employee = await employeeService.GetEmployee(args.Value);

            if (employee == null) return;

            var jobTitle = EmployeeAndJobList.Where(x => employee.Id == x.EmployeeId).Select(x => x.JobId).ToList();

            if (employee.JobId == 0 && jobTitle.Count == 0)
            {
                SearchByJobList = null;
                _toastService.ShowWarning("No Job Title Responsible Found for the task...");
                return;
            }
            SearchedJobListByEmployee = new();
            SearchedJobListByEmployee.Add(await JobDescriptionsService.GetByIdAsync(employee.JobId));
            if (jobTitle.Count > 0)
            {
                foreach (var job in jobTitle)
                {
                    SearchedJobListByEmployee.Add(await JobDescriptionsService.GetByIdAsync(job));
                }
            }
            SearchByJobList = SearchedJobListByEmployee.Where(x => x.IsDeleted == false).Select(x => x.Name).Distinct().ToList();
            SelectedJobTilteFromSearchedEmployee = SearchByJobList.FirstOrDefault();
        }

        protected void ActiveEditHistoryComponent()
        {
            IsEditHistoryComponent = true;

            if (RecurringTask == null)
                RecTaskForEditHistory = new TasksRecTasksViewModel();
            else
                RecTaskForEditHistory = RecurringTask;
        }

        protected void DeActiveEditHistoryComponent()
        {
            IsEditHistoryComponent = false;
        }
        protected async Task DeclineTask()
        {
            SelectedTaskViewModel.IsDeleted = true;
            SelectedTaskViewModel.ApprovedByEmployeeId = ApproverId;
            var UpdatedTask = taskService.UpdateRecurringTaskSync(SelectedTaskViewModel);
            bool send = await taskService.DeclineTaskEmail(SelectedTaskViewModel, null, "NEW TASK DENIED", true);
            await jSRuntime.InvokeAsync<object>("HideModal", "#RecurringTaskModal");
            await RefreshParentComponent.InvokeAsync(SelectedTaskViewModel.Id);
            await CloseCreateRecurringTaskComponent.InvokeAsync();
        }

        protected async Task OpenNoteOrInquiry(bool isInquiry)
        {
            if (isInquiry)
            {
                IsInquiryNote = true;
            }
            await jSRuntime.InvokeAsync<object>("ShowModal", "#DeclineOrInquiryNote");
        }
        protected async Task SendEmailInquiryOrNote()
        {
            if (string.IsNullOrEmpty(DeclinedNote))
            {
                ErrorMessage = "PLEASE ENTER A NOTE";
                await jSRuntime.InvokeVoidAsync("AddRedBox", "Notes");
                return;
            }
            SelectedTaskViewModel.ApprovedByEmployeeId = ApproverId;
            var UpdatedTask = taskService.UpdateRecurringTaskSync(SelectedTaskViewModel);
            if (IsInquiryNote)
            {
                bool send = await taskService.DeclineTaskEmail(SelectedTaskViewModel, DeclinedNote, "INQUIRY ABOUT NEW TASK", true);
            }
            else
            {
                bool send = await taskService.DeclineTaskEmail(SelectedTaskViewModel, DeclinedNote, "NEW TASK DENIED", false);
            }
            await CloseComponent();
        }
        protected async Task CloseComponent()
        {
            if (ErrorMessage != null || DeclinedNote!=null)
            {
                DeclinedNote = null;
                ErrorMessage = null;
                await jSRuntime.InvokeVoidAsync("RemoveRedBox", "Notes");
            }
            await jSRuntime.InvokeAsync<object>("HideModal", "#DeclineOrInquiryNote");
        }
    }
}
