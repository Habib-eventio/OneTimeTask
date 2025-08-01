using Blazored.Toast.Services;
using CamcoTasks.Data.Services;
using CamcoTasks.Library;
using CamcoTasks.Pages.RecurringTasks.ViewRecurringTasks.Graph;
using CamcoTasks.Service.IService;
using CamcoTasks.Infrastructure.Common.Email;
using CamcoTasks.ViewModels.EmployeeDTO;
using CamcoTasks.ViewModels.PageLoadTimeDTO;
using CamcoTasks.ViewModels.TasksImagesDTO;
using CamcoTasks.ViewModels.TasksRecTasksDTO;
using CamcoTasks.ViewModels.TasksTaskUpdatesDTO;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using Syncfusion.Blazor.Inputs;
using FileInfo = Syncfusion.Blazor.Inputs.FileInfo;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Linq;
using System.Text;
using CamcoTasks.ViewModels.JobDescriptions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using BlazorDownloadFile;
using CamcoTasks.Infrastructure.Defaults;

namespace CamcoTasks.Pages.RecurringTasks.ViewRecurringTasks
{
    public partial class CreateRecUpdateComponent
    {
        protected int CountUpdate;

        protected bool IsExportGraph = false;

        protected TasksRecTasksViewModel RecTask = new TasksRecTasksViewModel();
        protected List<JobDescriptionEmployeeEmail> AllJobDescriptionAndEmployeeEmails { get; set; } =
            new List<JobDescriptionEmployeeEmail>();

        protected List<JobDescriptionEmployeeEmail> ActiveJobDescriptionWithEmployeeEmails =
           new List<JobDescriptionEmployeeEmail>();
        protected GraphComponent GraphComponentRef { get; set; }

        protected TasksTaskUpdatesViewModel SelectedUpdateViewModel { get; set; }
        protected PageLoadTimeViewModel PageLoadTime { get; set; } = new PageLoadTimeViewModel()
        {
            PageName = "RecurringTask",
            SectionName = "BeforeUpdateRectTask",
            DateCreated = DateTime.Now
        };
        protected bool IsDoingTask { get; set; } = false;
        protected bool IsUploading = false;
        private bool SaveAndClose = false;
        protected bool IsTaskGraphRequired { get; set; }
        protected bool IsQuestionRequired { get; set; }
        protected bool OpenChartTemp = false;
        protected bool IsLoading { get; set; } = true;
        protected bool IsActiveCameraComponent { get; set; } = false;

        protected int tempTaskId { get; set; }

        protected List<EmployeeEmail> AuditemployeeEmails { get; set; } = new List<EmployeeEmail>();
        protected IEnumerable<string> Auditemployee { get; set; }
        protected List<TasksImagesViewModel> UpdateUploadFiles { get; set; } = new List<TasksImagesViewModel>();
        protected List<TasksTaskUpdatesViewModel> GraphUpdatesTemp { get; set; } = new List<TasksTaskUpdatesViewModel>();

        protected TasksImagesViewModel UpdatedDocumentUploadFile = new TasksImagesViewModel();

        protected Dictionary<Guid, string> CapturedImgaesList { get; set; }
        protected Dictionary<string, object> htmlAttributeBig = new Dictionary<string, object>() { { "rows", "7" }, { "spellcheck", "true" } };
        protected Dictionary<string, object> htmlAttribute = new Dictionary<string, object>() { { "rows", "4" } };

        protected string TaskUpdateErrorMessage { get; set; } = string.Empty;
        protected string selectedPassAnswer = "NO";
        protected string UpdateTitle { get; set; } = "ADD";
        private string ChartImage = string.Empty;

        protected SfUploader UpdateUploadObj { get; set; }
        protected SfUploader DocumentUploadObj { get; set; }

        protected SfTextBox updateDescription { get; set; }

        [Inject]
        protected IFileManagerService FileManagerService { get; set; }
        [Inject]
        protected IJobDescriptionsService JobDescriptionsService { get; set; }

        [Inject]
        private IJSRuntime jSRuntime { get; set; }
        [Inject]
        protected ITasksService taskService { get; set; }
        [Inject]
        protected IEmployeeService employeeService { get; set; }
        [Inject]
        private FileManagerService fileManagerService { get; set; }
        [Inject]
        private IEmailService emailService { get; set; }
        [Inject]
        private ILogger<RecTaskUpdateComponent> _logger { get; set; }
        [Inject]
        private IToastService _toastService { get; set; }
        [Inject]
        protected IPageLoadTimeService pageLoadTimeService { get; set; }

        [Parameter]
        public TasksTaskUpdatesViewModel TaskUpdateViewModel { get; set; }
        [Parameter]
        public TasksRecTasksViewModel SelectedUpdateTaskViewModel { get; set; }
        [Parameter]
        public EventCallback<string> SuccessMessageCreateRecUpdateComponent { get; set; }
        [Parameter]
        public EventCallback CloseCreateRecUpdateComponent { get; set; }
        [Parameter]
        public EventCallback<int> RefreshToParentCreateRecUpdateComponent { get; set; }
        [CascadingParameter]
        public ViewRecurringTasks ViewRecurringTasksComponentRef { get; set; }

        [Parameter]
        public bool WithoutModel { get; set; }


        protected override async Task OnInitializedAsync()
        {
            await Task.Run(() => PageLoadTime.StartTime = DateTime.Now);
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await LoadCreateRecUpdateDate();
                await Task.Run(() => IsLoading = false);

                StateHasChanged();

                await PageLoadTimeCalculation();
            }
        }

        protected async Task PageLoadTimeCalculation()
        {
            PageLoadTime.EndTime = DateTime.Now;
            await pageLoadTimeService.InsertAsync(PageLoadTime);
        }

        protected async Task LoadCreateRecUpdateDate()
        {
            await jSRuntime.InvokeAsync<object>("ShowModal", "#RecTaskUpdate");
            await LoadData();
            await StartTaskUpdate();
        }

        protected async Task LoadData()
        {
            if (SelectedUpdateTaskViewModel != null)
            {
                RecTask = await taskService.GetRecurringTaskById(SelectedUpdateTaskViewModel.Id) ?? new TasksRecTasksViewModel();
            }

            if (RecTask != null)
            {
                if (RecTask.IsGraphRequired)
                    OpenChartTemp = true;

                RecTask.TasksTaskUpdates = (await taskService
                        .GetTaskUpdates(
                            RecTask.Id, "Rich Arnold ''NUDGED'' you to get the recurring Task", "Rich Arnold ''Email'' you to get the recurring Task", false))
                    .ToList();

                if (!RecTask.TasksTaskUpdates.Any())
                    RecTask.TasksTaskUpdates = new List<TasksTaskUpdatesViewModel>();

                RecTask.TasksTaskUpdates = RecTask.TasksTaskUpdates
                    .OrderByDescending(x => x.UpdateDate.Date).ToList();

                var EmployeesList = await employeeService.GetListAsync(true);

                AuditemployeeEmails = EmployeesList.Where(x => x.IsActive).Select(x => new EmployeeEmail
                {
                    Id = x.Id,
                    FullName = x.FullName,
                    Email = x.Email,
                    IsSelected = false
                }).OrderBy(x => x.FullName).ToList();

                Auditemployee = EmployeesList.Where(e => e.IsActive)
                    .Select(e => e.FullName)
                    .OrderBy(e => e);

                CountUpdate = await taskService.GetTaskUpdatesCountAsync(RecTask.Id, false);

                var jobDescription = await JobDescriptionsService.GetListAsync(false);

                AllJobDescriptionAndEmployeeEmails = await JobDescriptionsService
                .GetJobDescriptionWithEmployeeListAsync(
                EmployeesList.ToList(), jobDescription.ToList());

            }
        }

        protected async Task StartTaskUpdate()
        {
            IsDoingTask = false;

            AuditemployeeEmails.ForEach(x => x.IsSelected = false);

            if (TaskUpdateViewModel == null)
            {
                UpdateTitle = "ADD";
                StartUpdateAdd();
            }
            else
            {
                SelectedUpdateViewModel = await taskService.GetTaskUpdatesById(TaskUpdateViewModel.UpdateId);
                UpdateTitle = "EDIT";
                await StartUpdateEdit();
            }
        }

        protected void StartUpdateAdd()
        {
            selectedPassAnswer = "NO";

            IsTaskGraphRequired = RecTask.IsGraphRequired;
            IsQuestionRequired = RecTask
                .IsQuestionRequired.HasValue ? (bool)RecTask.IsQuestionRequired : false;
            tempTaskId = RecTask.Id;
            SelectedUpdateViewModel = new TasksTaskUpdatesViewModel()
            { Update = string.Empty, UpdateDate = DateTime.Today, IsAudit = false, IsPass = false,UpdateBy = RecTask.PersonResponsible };
            SelectedUpdateViewModel.UpcomingDate = RecTask.UpcomingDate;
        }

        protected async Task StartUpdateEdit()
        {
            IsTaskGraphRequired = RecTask.IsGraphRequired;
            tempTaskId = RecTask.Id;
            if (RecTask.IsPassOrFail == true)
            {
                if (SelectedUpdateViewModel.IsPass.HasValue)
                {
                    selectedPassAnswer = SelectedUpdateViewModel.IsPass.Value ? "YES" : "NO";
                }
                else
                {
                    SelectedUpdateViewModel.IsPass = false;
                    selectedPassAnswer = "NO";
                }
            }

            if (!(SelectedUpdateViewModel.FailedAuditList != null || SelectedUpdateViewModel.FailedAuditList != ""))
            {
                var FailedEmpList = SelectedUpdateViewModel.FailedAuditList.Split(";");
                foreach (var EmpId in FailedEmpList)
                {
                    AuditemployeeEmails[AuditemployeeEmails.FindIndex(x => x.Id == Convert.ToInt32(EmpId))].IsSelected = true;
                }
            }
            if (!string.IsNullOrEmpty(SelectedUpdateViewModel.PictureLink))
            {
                UpdateUploadFiles = (await taskService.GetUpdatesImagesAsync(SelectedUpdateViewModel.UpdateId)).ToList();
            }

            await Task.Run(() => SelectedUpdateViewModel.UpcomingDate = RecTask.UpcomingDate);
        }

        protected async Task TrySaveUpdate(bool SaveAndClose)
        {
            PageLoadTime.StartTime = DateTime.Now;
            PageLoadTime.SectionName = "AfterSaveUpdateRecTask";
            IsDoingTask = true;

            if (IsUploading)
            {
                TaskUpdateErrorMessage = "Files are Being Uploaded, Please Wait..";
                return;
            }

            this.SaveAndClose = SaveAndClose;

            if (SelectedUpdateViewModel.UpdateId == 0)
            {
                await AddNewUpdate();

            }
            else
            {
                await EditUpdate();

            }

            await PageLoadTimeCalculation();
        }

        protected async Task<bool> RecurringTaskUpdateValidation(TasksTaskUpdatesViewModel SelectedUpdateViewModel, TasksRecTasksViewModel RecTask, bool edit = false)
        {
            bool isValid = true;
            TaskUpdateErrorMessage = null;

            if (string.IsNullOrEmpty(SelectedUpdateViewModel.UpdateBy))
            {
                TaskUpdateErrorMessage = "Please fill completed by!";
                await jSRuntime.InvokeVoidAsync("AddRedBox", "Audit");
                isValid = false;
                IsDoingTask = false;
                return isValid;
            }

            if (RecTask.IsDescriptionMandatory == true)
            {
                if (string.IsNullOrEmpty(SelectedUpdateViewModel.Update))
                {
                    TaskUpdateErrorMessage = "Please fill update description!";
                    await jSRuntime.InvokeVoidAsync("AddRedBox", "RecUpdDesc");
                    isValid = false;
                    IsDoingTask = false;
                    return isValid;
                }
                else
                {
                    await jSRuntime.InvokeVoidAsync("RemoveRedBox", "RecUpdDesc");
                }
            }

            if (RecTask.IsGraphRequired)
            {
                if (SelectedUpdateViewModel.GraphNumber < 0)
                {
                    TaskUpdateErrorMessage = "Please fill graph value!";
                    await jSRuntime.InvokeVoidAsync("AddRedBox", "RecUpdGraphNo");
                    isValid = false;
                    IsDoingTask = false;
                    return isValid;
                }

                RecTask.LatestGraphValue = SelectedUpdateViewModel.GraphNumber;
            }

            if (RecTask.IsPassOrFail == true)
            {
                if (selectedPassAnswer == "NA")
                {
                    if (string.IsNullOrEmpty(selectedPassAnswer))
                    {
                        TaskUpdateErrorMessage = "Please fill pass or fail answare!";
                        await jSRuntime.InvokeVoidAsync("AddRedBox", "RecUpdFail");
                        isValid = false;
                        IsDoingTask = false;
                        return isValid;
                    }
                    else
                    {
                        await jSRuntime.InvokeVoidAsync("RemoveRedBox", "RecUpdFail");
                    }
                }
            }

            if (RecTask.IsQuestionRequired == false)
                RecTask.Question = "";

            if (RecTask.IsQuestionRequired == true)
            {
                if (string.IsNullOrEmpty(SelectedUpdateViewModel.QuestionAnswer))
                {
                    TaskUpdateErrorMessage = "Please fill question answer!";
                    await jSRuntime.InvokeVoidAsync("AddRedBox", "RecupdQuestion");
                    isValid = false;
                    IsDoingTask = false;
                    return isValid;
                }
                else
                {
                    await jSRuntime.InvokeVoidAsync("RemoveRedBox", "RecupdQuestion");
                }
            }

            if (string.IsNullOrEmpty(SelectedUpdateViewModel.FailReason)
                && SelectedUpdateViewModel.IsPass == false
                && RecTask.IsPassOrFail == true)
            {
                TaskUpdateErrorMessage = "PLEASE ADD FAIL REASON..!";
                await jSRuntime.InvokeVoidAsync("AddRedBox", "RecUpdFail");
                isValid = false;
                IsDoingTask = false;
                return isValid;
            }

            if (RecTask.IsGraphRequired && edit != true)
            {
                var taskFrequency = await taskService.GetFrequency(RecTask.Frequency);
                int days = taskService.RecurringUpcommingDate(taskFrequency);
                if (days > 0)
                {
                    TasksTaskUpdatesViewModel UpdateLookUp = null;

                    if (RecTask.TasksTaskUpdates.Any())
                    {
                        UpdateLookUp = RecTask
                        .TasksTaskUpdates
                        .FirstOrDefault(
                        x => x.UpdateDate.Date == SelectedUpdateViewModel.UpdateDate.Date);
                    }


                    if (UpdateLookUp != null)
                    {
                        TaskUpdateErrorMessage = "You Can't add 2 Graph Points for same Date of Update, please Edit the update if needed";
                        isValid = false;
                        IsDoingTask = false;
                        return isValid;
                    }
                }
                else
                {
                    if (taskFrequency.Frequency == "Twice/Day")
                    {
                        var numberOfUpdate = RecTask.TasksTaskUpdates.Where(x => x.UpdateDate.Date == SelectedUpdateViewModel.UpdateDate.Date).Count();
                        if (numberOfUpdate >= 2)
                        {
                            TaskUpdateErrorMessage = "You Can't add 3 Graph Points for same Date of Update, please Edit the update if needed";
                            isValid = false;
                            IsDoingTask = false;
                            return isValid;
                        }
                    }
                    else if (taskFrequency.Frequency == "Three/Day")
                    {
                        var numberOfUpdate = RecTask.TasksTaskUpdates.Where(x => x.UpdateDate.Date == SelectedUpdateViewModel.UpdateDate.Date).Count();
                        if (numberOfUpdate >= 3)
                        {
                            TaskUpdateErrorMessage = "You Can't add 4 Graph Points for same Date of Update, please Edit the update if needed";
                            isValid = false;
                            IsDoingTask = false;
                            return isValid;
                        }
                    }
                }
            }

            if (RecTask.IsPicRequired == true && !UpdateUploadFiles.Any()
                && CapturedImgaesList == null && UpdateTitle == "ADD")
            {
                TaskUpdateErrorMessage = "PLEASE UPLOAD IMAGE!";
                isValid = false;
                IsDoingTask = false;
                return isValid;
            }

            if (RecTask.IsDocumentRequired == true && UpdatedDocumentUploadFile.FileInfo ==  null 
                && UpdatedDocumentUploadFile.FileName == null && UpdatedDocumentUploadFile.Stream == null && UpdateTitle == "ADD")
            {
                TaskUpdateErrorMessage = "PLEASE UPLOAD CX FORM/PDF";
                _toastService.ShowError("PLEASE UPLOAD CX FORM/PDF");
                isValid = false;
                IsDoingTask = false;
                return isValid;
            }

            return isValid;
        }
        private bool IsFileValid(FileInfo fileInfo)
        {
            if (!fileManagerService.IsValidSize(fileInfo.Size) ||
                !(fileManagerService.IsFile(fileInfo.Name) ||
                 fileManagerService.IsImage(fileInfo.Name)))
            {
                var size = Convert.ToDouble(fileInfo.Size) / 1000000;
                TaskUpdateErrorMessage = $"Image size can not be more than 20 mb. Your uploaded image size is {size} mb or you are uploading the wrong file.";
                return false;
            }
            return true;
        }

        private async Task StartDownloadDocument()
        {
            if (!string.IsNullOrWhiteSpace(RecTask.UpdatedDocumentLink))
            {
                await FileManagerService.StartDownloadFile(RecTask.UpdatedDocumentLink);
            }
        }

        private async Task CheckUpdateFiles()
        {
            if (RecTask.IsPicRequired == true)
            {
                if (UpdateUploadFiles.Any())
                {
                    foreach (var file in UpdateUploadFiles)
                    {
                        if (file.FileInfo != null)
                        {

                            if (!IsFileValid(file.FileInfo))
                            {
                                return;
                            };

                            var ImageFileName = $"{Guid.NewGuid()}.{file.FileInfo.Type}";
                            var ResultPath = fileManagerService.CreateRecurringTaskDirectory(
                                tempTaskId.ToString()) + ImageFileName;
                            fileManagerService.WriteToFile(file.Stream, ResultPath);
                            SelectedUpdateViewModel.PictureLink = ResultPath;
                            SelectedUpdateViewModel.IsPicture = true;

                            string fileName = string.Empty;
                            if (!string.IsNullOrEmpty(file.FileInfo.Name))
                            {
                                fileName = file.FileInfo.Name;
                                int dotIndx = fileName.IndexOf('.');

                                if (dotIndx >= 0)
                                {
                                    fileName = fileName.Substring(0, dotIndx);
                                }
                            }

                            TasksImagesViewModel TM = new TasksImagesViewModel()
                            {
                                UpdateId = SelectedUpdateViewModel.UpdateId,
                                PictureLink = ResultPath,
                                IsDeleted = false,
                                OneTimeId = 0,
                                RecurringId = tempTaskId,
                                FileName = fileName,
                                ImageNote = file.ImageNote
                            };
                            await taskService.InsertTaskImageAsync(TM);
                        }
                    }
                }

                SelectedUpdateViewModel = await taskService.SaveCaptureImageAsync(CapturedImgaesList,
                    tempTaskId, SelectedUpdateViewModel);

                await UpdateUploadObj.ClearAllAsync();
            }

            if(RecTask.IsDocumentRequired == true)
            {
                if (UpdatedDocumentUploadFile.FileInfo != null && UpdatedDocumentUploadFile.Stream != null)
                {
                    if (!IsFileValid(UpdatedDocumentUploadFile.FileInfo))
                    {
                        return;
                    };

                    var FileName = $"{Guid.NewGuid()}.{UpdatedDocumentUploadFile.FileInfo.Type}";
                    var ResultPath = fileManagerService.CreateRecurringTaskDirectory(tempTaskId.ToString()) + FileName;
                    fileManagerService.WriteToFile(UpdatedDocumentUploadFile.Stream, ResultPath);
                    SelectedUpdateViewModel.UpdatedDocumentLink = ResultPath;
                }
            }
        }

        protected async Task AddNewUpdate()
        {
            try
            {
                if (!await RecurringTaskUpdateValidation(SelectedUpdateViewModel, RecTask))
                    return;

                SelectedUpdateViewModel.RecurringID = tempTaskId;
                SelectedUpdateViewModel.IsDeleted = false;
                SelectedUpdateViewModel.TaskID = null;

                SelectedUpdateViewModel.UpdateId = await taskService.AddTaskUpdateSync(SelectedUpdateViewModel);

                await CheckUpdateFiles();

                if (CountUpdate == 0)
                    SelectedUpdateViewModel.TaskCompleted = true;
                else if (RecTask.UpcomingDate?.Date >= SelectedUpdateViewModel.UpdateDate)
                    SelectedUpdateViewModel.TaskCompleted = true;
                else
                    SelectedUpdateViewModel.TaskCompleted = false;

                var taskFrequency = await taskService.GetFrequency(RecTask.Frequency);

                RecTask.DateCompleted = SelectedUpdateViewModel.UpdateDate;

                MarkCompleted();

                RecTask = taskService.RecurringUpcommingDate(RecTask,
                    taskFrequency, SelectedUpdateViewModel);
                if (RecTask.IsTaskRandomize == true)
                {
                    RandomizeUpcomingDate();
                }
                SelectedUpdateViewModel.DueDate = RecTask.UpcomingDate;
                if (RecTask.TaskStartDueDate != null && RecTask.TaskEndDueDate != null)
                {
                    bool isTaskDuePeriod = UpdateUpcomingDateInYearPeriod();
                    RecTask.IsTaskDuePeriod = isTaskDuePeriod;
                }
                if (!SelectedUpdateViewModel.TaskCompleted.Value)
                {
                    DateTime FromDate = RecTask.UpcomingDate.Value.AddDays(taskFrequency.Days);
                    DateTime ToDate = RecTask.UpcomingDate.Value.AddDays(taskFrequency.Days);

                    if (FromDate < DateTime.Today && taskFrequency.Days > 0)
                    {
                        while (ToDate.AddDays(taskFrequency.Days) < DateTime.Today)
                        {
                            ToDate = ToDate.AddDays(taskFrequency.Days);
                        }

                        var SystemUpdate = new TasksTaskUpdatesViewModel
                        {
                            DueDate = ToDate,
                            UpcomingDate = SelectedUpdateViewModel.UpcomingDate,
                            FailReason = SelectedUpdateViewModel.FailReason,
                            FailedAuditList = SelectedUpdateViewModel.FailedAuditList,
                            FileLink = SelectedUpdateViewModel.FileLink,
                            Color = SelectedUpdateViewModel.Color,
                            GraphDate = SelectedUpdateViewModel.GraphDate,
                            GraphNumber = SelectedUpdateViewModel.GraphNumber,
                            IsAudit = SelectedUpdateViewModel.IsAudit,
                            IsDeleted = SelectedUpdateViewModel.IsDeleted,
                            IsPass = SelectedUpdateViewModel.IsPass,
                            Notes = SelectedUpdateViewModel.Notes,
                            PictureLink = SelectedUpdateViewModel.PictureLink,
                            QuestionAnswer = SelectedUpdateViewModel.QuestionAnswer,
                            RecurringID = SelectedUpdateViewModel.RecurringID,
                            TaskCompleted = SelectedUpdateViewModel.TaskCompleted,
                            TaskID = SelectedUpdateViewModel.TaskID,
                            Update = $"-----System Generated Update Auto Completion from:{FromDate}, To:{ToDate}-----",
                            UpdateDate = SelectedUpdateViewModel.UpdateDate,
                            UpdateId = 0,
                        };

                        SystemUpdate.UpdateId = await taskService.AddTaskUpdate(SystemUpdate);

                        RecTask.TasksTaskUpdates.Add(SystemUpdate);
                    }
                }

                SelectedUpdateViewModel.IsPass = selectedPassAnswer == "YES" ? true : false;

                SelectedUpdateViewModel.FailedAuditList = "";
                if (RecTask.IsAuditRequired == true)
                {
                    var SelectedListEmployees = AuditemployeeEmails.Where(x => x.IsSelected);
                    foreach (var Emp in SelectedListEmployees)
                    {
                        if (string.IsNullOrEmpty(SelectedUpdateViewModel.FailedAuditList))
                        {
                            SelectedUpdateViewModel.FailedAuditList += Emp.Id;
                        }
                        else
                        {
                            SelectedUpdateViewModel.FailedAuditList += ";" + Emp.Id;
                        }
                    }
                }

                SelectedUpdateViewModel.CreatedDate = DateTime.Now;
                RecTask.IsTaskDelayed = false;

                SetActiveEmployeeEmailsWithJobs();
                await taskService.UpdateRecurringTask(RecTask);
                await taskService.UpdateTaskUpdate(SelectedUpdateViewModel);

                if (RecTask.IsGraphRequired)
                {
                    ChartImage = await GraphComponentRef.ExportGraphAsync();
                }

                await SendUpdatedEmail(RecTask, SelectedUpdateViewModel, false);

                

                IsDoingTask = false;

                if (SaveAndClose)
                {
                    await jSRuntime.InvokeAsync<object>("HideAllModalWithModalBackdrop");
                    SaveAndClose = false;

                    await SuccessMessageCreateRecUpdateComponent.InvokeAsync("Update Has Been Added!");
                    await ViewRecurringTasksComponentRef.RefreshViewRecurringTaskComponent(RecTask.Id);
                    await ViewRecurringTasksComponentRef.PercentageComponentRef.ReloadComponentAsync(RecTask.Id, SelectedUpdateViewModel.UpdateId);
                    await ViewRecurringTasksComponentRef.DeactivateRecTaskUpdateComponent();
                    ViewRecurringTasksComponentRef.DeactiveViewSingleRecurringTaskComponent();
                }
                else
                {
                    await jSRuntime.InvokeAsync<object>("HideModal", "#RecTaskUpdate");

                    await SuccessMessageCreateRecUpdateComponent.InvokeAsync("Update Has Been Added!");
                    await RefreshToParentCreateRecUpdateComponent.InvokeAsync(SelectedUpdateViewModel.UpdateId);
                    await CloseCreateRecUpdateComponent.InvokeAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Recurring task Create update error", ex);
            }
        }

        protected bool UpdateUpcomingDateInYearPeriod()
        {
            var upcomingDate = RecTask.UpcomingDate.Value;
            var fromDate = RecTask.TaskStartDueDate.Value;
            var toDate = RecTask.TaskEndDueDate.Value;

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
                    RecTask.UpcomingDate = new DateTime(upcomingDate.Year, fromDate.Month, fromDate.Day);
                    return false;
                }
                else
                {
                    RecTask.UpcomingDate = new DateTime(upcomingDate.Year + 1, fromDate.Month, fromDate.Day);
                    return false;
                }
            }
            else
            {
                if (string.Compare(upcomingMonthDay, fromMonthDay) >= 0 || string.Compare(upcomingMonthDay, toMonthDay) <= 0)
                {
                    return true;
                }
                else if (string.Compare(upcomingMonthDay, fromMonthDay) < 0)
                {
                    RecTask.UpcomingDate = new DateTime(upcomingDate.Year, fromDate.Month, fromDate.Day);
                    return false;
                }
                else
                {
                    return false;
                }
            }
        }

        protected async Task EditUpdate()
        {
            try
            {
                if (!await RecurringTaskUpdateValidation(SelectedUpdateViewModel, RecTask, true))
                    return;

                await CheckUpdateFiles();

                if (SelectedUpdateViewModel.IsPass == true)
                    SelectedUpdateViewModel.FailReason = "";

                SelectedUpdateViewModel.IsPass = selectedPassAnswer == "NO" ? false : true;
                SelectedUpdateViewModel.FailedAuditList = "";
                if (RecTask.IsAuditRequired == true)
                {
                    var SelectedListEmployees = AuditemployeeEmails.Where(x => x.IsSelected);
                    foreach (var Emp in SelectedListEmployees)
                    {
                        if (string.IsNullOrEmpty(SelectedUpdateViewModel.FailedAuditList))
                        {
                            SelectedUpdateViewModel.FailedAuditList += Emp.Id;
                        }
                        else
                        {
                            SelectedUpdateViewModel.FailedAuditList += ";" + Emp.Id;
                        }
                    }
                }

                var result = await taskService.UpdateTaskUpdate(SelectedUpdateViewModel);

                if (RecTask.IsGraphRequired)
                {
                    var LatestUpdates = (await taskService.GetTaskUpdates1(RecTask.Id)).ToList();
                    if (LatestUpdates.Any())
                    {
                        LatestUpdates = LatestUpdates.OrderByDescending(x => x.UpdateDate).ToList();
                        RecTask.LatestGraphValue = LatestUpdates[0].GraphNumber;
                        await taskService.UpdateRecurringTask(RecTask);
                    }
                }

                if (result)
                {
                    await SendUpdatedEmail(RecTask, SelectedUpdateViewModel, true);
                }

                await taskService.UpdateRecurringTask(RecTask);

                IsDoingTask = false;

                if (SaveAndClose)
                {
                    await jSRuntime.InvokeAsync<object>("HideAllModalWithModalBackdrop");
                    SaveAndClose = false;

                    await SuccessMessageCreateRecUpdateComponent.InvokeAsync("Update Successfull!");
                    await ViewRecurringTasksComponentRef.RefreshViewRecurringTaskComponent(RecTask.Id);
                    await ViewRecurringTasksComponentRef.PercentageComponentRef.ReloadComponentAsync(RecTask.Id, SelectedUpdateViewModel.UpdateId);
                    await ViewRecurringTasksComponentRef.DeactivateRecTaskUpdateComponent();
                    ViewRecurringTasksComponentRef.DeactiveViewSingleRecurringTaskComponent();
                }
                else
                {
                    await jSRuntime.InvokeAsync<object>("HideModal", "#RecTaskUpdate");

                    await SuccessMessageCreateRecUpdateComponent.InvokeAsync("Update Successfull!");
                    await RefreshToParentCreateRecUpdateComponent.InvokeAsync(SelectedUpdateViewModel.UpdateId);
                    await CloseCreateRecUpdateComponent.InvokeAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Task update edit error", ex);
            }
        }

        protected async Task SendUpdatedEmail(TasksRecTasksViewModel task, TasksTaskUpdatesViewModel taskUpdate, bool IsEdit)
        {
            if (string.IsNullOrEmpty(task.PersonResponsible) && string.IsNullOrEmpty(task.EmailsList))
            {
                await SuccessMessageCreateRecUpdateComponent.InvokeAsync("Email Not Found");
                return;
            }

            string Subject;
            string emailSendTo = string.Empty;
            if (IsEdit)
                Subject = "RECURRING TASK EDIT AN UPDATE";
            else
                Subject = "TASK UPDATE: " + task.TaskDescriptionSubject?.ToUpper();
            string greenCircle = "<span style=\"width: 15px; height: 15px; border-radius: 50%; background-color: #20D30C; display: inline-block; margin:3px 3px 0px 3px;\"></span>";
            string redCircle = "<span style=\"width: 15px; height: 15px; border-radius: 50%; background-color: #E0230D; display: inline-block; margin:3px 3px 0px 3px;\"></span>";

            string taskCompletionHistory = null;
            foreach (var update in task.TasksTaskUpdates.OrderByDescending(x => x.UpdateDate.Date).Take(14).Reverse())
            {
                if (update.TaskCompleted.HasValue && update.TaskCompleted.Value)
                {
                    taskCompletionHistory += greenCircle;
                }
                else
                {
                    taskCompletionHistory += redCircle;
                }
            }

            taskCompletionHistory = taskCompletionHistory += taskUpdate.TaskCompleted.HasValue && taskUpdate.TaskCompleted.Value ? greenCircle : redCircle;
            StringBuilder customBody = new StringBuilder();
            customBody.Append($"<p><b>TASK ID: </b>{task.Id}</p>");
            customBody.Append($"<p><b>TASK SUBJECT: </b>{task.TaskDescriptionSubject?.ToUpper()}</p>");
            customBody.Append($"<p><b>UPCOMING DATE: </b>{task.UpcomingDate?.Date.ToString("M/d/yyyy")}</p>");
            customBody.Append($"<p><b>FREQUENCY: </b>{task.Frequency}</p>");
            customBody.Append($"<p><b>TASK COMPLETION HISTORY: </b>{taskCompletionHistory}</p>");
            customBody.Append($"<p><b>PERSON RESPONSIBLE: </b>{task.PersonResponsible}</p>");
            customBody.Append($"<p><b>INITIATOR: </b>{task.Initiator}</p>");
            customBody.Append($"<p><b>DUE DATE: </b>{task.UpcomingDate?.Date.ToString("M/d/yyyy")}</p>");
            if (!string.IsNullOrEmpty(taskUpdate.Update)) {
                customBody.Append($"<p><b>UPDATE: </b>{taskUpdate.Update}</p>");
            }
            customBody.Append($"<br><p style=\"color: red;\"><b>REQUIRED FIELDS:</b></p>");

            if (task.IsDescriptionMandatory.HasValue && task.IsDescriptionMandatory.Value)
            {
                customBody.Append($"<p><b>UPDATE DESCRIPTION: </b>{taskUpdate.Update}</p>");
            }

            if (task.IsGraphRequired)
            {
                customBody.Append($"<p><b>{task.GraphTitle.ToUpper()} VALUE: </b>{taskUpdate.GraphNumber}</p>");
            }

            if (task.IsQuestionRequired == true)
            {
                customBody.Append($"<p><b>QUESTION: </b>{task.Question}</p>");
                customBody.Append($"<p><b>QUESTION ANSWER: </b>{taskUpdate.QuestionAnswer}</p>");
            }


            if (task.IsPassOrFail == true)
            {
                customBody.Append($"<p><b>STATUS: </b>{(taskUpdate.IsPass == true ? "PASSED" : "<b style='color: red;'>FAILED</b>")}</p>");

                if (taskUpdate.IsPass == false)
                {
                    Subject = "TASK FAILED";
                    customBody.Append($"<p><b>FAIL REASON: </b>{taskUpdate.FailReason.ToUpper()}</p>");
                    if (task.FailedEmailsList != null)
                    {
                        emailSendTo = task.FailedEmailsList + ";";
                    }
                }
            }

            string Attachment = "";
            if (task.IsPicRequired == true)
            {
                customBody.Append($"<p><b>IMAGE NOTE: </b>{task.UpdateImageDescription}</p>");

                if (!string.IsNullOrEmpty(taskUpdate.PictureLink))
                    Attachment = taskUpdate.PictureLink + ":";

                if (!string.IsNullOrEmpty(taskUpdate.FileLink))
                    Attachment = taskUpdate.FileLink + ":";
            }

            List<TasksImagesViewModel> images = new List<TasksImagesViewModel>();
            images = (await taskService.GetUpdatesImagesAsync(taskUpdate.UpdateId)).ToList();
            if (images.Any())
            {
                foreach (var item in images)
                {
                    string attachPath = fileManagerService.FilePathForEmail(item);

                    if (!string.IsNullOrEmpty(attachPath))
                    {
                        Attachment += attachPath + ":";
                    }
                    else
                    {
                        Attachment += item.PictureLink + ":";
                    }
                }
            }

            if (task.IsGraphRequired)
            {
                if (!string.IsNullOrEmpty(ChartImage))
                {
                    Attachment += ChartImage + ":";
                }
            }

            if (task.IsDocumentRequired)
            {
                Attachment += taskUpdate.UpdatedDocumentLink  + ":";
            }

            if (!string.IsNullOrEmpty(Attachment))
                Attachment = Attachment.Remove(Attachment.Length - 1);

            string[] splitPersonRes = task.PersonResponsible.Split(";");

            if (!splitPersonRes.Any())
            {
                await SuccessMessageCreateRecUpdateComponent.InvokeAsync("Email Sending Error, No Employee Found");
                return;
            }

            foreach (string emp in splitPersonRes)
            {
                EmployeeViewModel singleEmployee = employeeService.GetEmployeeSync(emp);

                if (singleEmployee != null && !string.IsNullOrEmpty(singleEmployee.Email))
                {
                    if (singleEmployee.IsActive)
                    {
                        emailSendTo += singleEmployee.Email + ";";
                    }
                    else
                    {
                        await SuccessMessageCreateRecUpdateComponent.InvokeAsync("Email Sending Error, Employee Is Not Active");
                    }
                }
                else
                {
                    await SuccessMessageCreateRecUpdateComponent.InvokeAsync("Email Sending Error, Employee Has No Email");
                }
            }
            if (!string.IsNullOrEmpty(taskUpdate.UpdateBy))
            {
                EmployeeViewModel updateByEmployee = employeeService.GetEmployeeSync(taskUpdate.UpdateBy);
                if (updateByEmployee != null && !string.IsNullOrEmpty(updateByEmployee.Email))
                {
                    emailSendTo += updateByEmployee.Email + ";";
                }

            }

            emailSendTo += task.EmailsList;

            if (!string.IsNullOrEmpty(emailSendTo) && emailSendTo.EndsWith(";"))
                emailSendTo = emailSendTo.Remove(emailSendTo.Length - 1);

            if (!string.IsNullOrEmpty(emailSendTo))
            {
                string body = EmailDefaults.GenerateEmailTemplate("Tasks", customBody.ToString());

                int emailId = await emailService.SendEmailAsync(EmailTypes.ActionBasedRecurringTaskUpdated,
                    Array.Empty<string>(), Subject, body, Attachment, emailSendTo.Split(';').ToArray());

                if (taskUpdate.EmailId == null)
                {
                    taskUpdate.EmailId = emailId;

                    await taskService.UpdateTaskUpdate(taskUpdate);
                }
            }

            await SuccessMessageCreateRecUpdateComponent.InvokeAsync("Email send Successful");
        }

        protected void MarkCompleted()
        {
            try
            {
                if (string.IsNullOrEmpty(RecTask.CompletedOnTime))
                {
                    if (RecTask.UpcomingDate?.Date >= RecTask.DateCompleted?.Date)
                        RecTask.CompletedOnTime = "1";
                    else
                        RecTask.CompletedOnTime = "0";
                }
                else
                {
                    if (RecTask.UpcomingDate?.Date >= RecTask.DateCompleted?.Date)
                        RecTask.CompletedOnTime += ";1";
                    else
                        RecTask.CompletedOnTime += ";0";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Mark complete error:", ex);
            }
        }

        protected void SetActiveEmployeeEmailsWithJobs()
        {
            try
            {
                if (RecTask.EmailsListJobId != null)
                {
                    var splitJobId = RecTask.EmailsListJobId.Split(';').Select(x => long.Parse(x)).ToList();

                    if (splitJobId.Count > 0)
                    {
                        for (int i = 0; i < splitJobId.Count; i++)
                        {
                            var jobDescriptionWithEmployee = AllJobDescriptionAndEmployeeEmails.Where(x => x.Id == splitJobId[i]).ToList();
                            ActiveJobDescriptionWithEmployeeEmails.AddRange(jobDescriptionWithEmployee);
                        }
                        if (ActiveJobDescriptionWithEmployeeEmails.Any())
                        {
                            RecTask.EmailsList = "";
                            foreach (var jobWithEmployee in ActiveJobDescriptionWithEmployeeEmails)
                            {
                                if (jobWithEmployee.EmployeeEmails.Any())
                                {
                                    RecTask.EmailsList += $"{jobWithEmployee.EmployeeEmails.FirstOrDefault().Email};";
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Setting Correct Employee Emails With Job Titles Error", ex);
            }
        }

        protected void RandomizeUpcomingDate()
        {
            try
            {

                Random random = new Random();
                switch (RecTask.Frequency.ToUpper())
                {
                        case "WEEKLY (RANDOM)":
                        RecTask.UpcomingDate = RecTask.UpcomingDate.Value.AddDays(random.Next(0, 4));
                        RecTask.UpcomingDate = taskService.RecurringUpcommingDate(RecTask.UpcomingDate.Value);
                        return;
                        case "MONTHLY (RANDOM)":
                        RecTask.UpcomingDate = RecTask.UpcomingDate.Value.AddDays(random.Next(0, 29));
                        RecTask.UpcomingDate = taskService.RecurringUpcommingDate(RecTask.UpcomingDate.Value);
                        return;
                        case "TWICE/MONTHLY (RANDOM)":
                        RecTask.UpcomingDate = RecTask.UpcomingDate.Value.AddDays(random.Next(0, 14));
                        RecTask.UpcomingDate = taskService.RecurringUpcommingDate(RecTask.UpcomingDate.Value);
                        return;
                        case "TWICE/WEEK (RANDOM)":
                        RecTask.UpcomingDate = RecTask.UpcomingDate.Value.AddDays(random.Next(0, 2));
                        RecTask.UpcomingDate = taskService.RecurringUpcommingDate(RecTask.UpcomingDate.Value);
                        return;
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "While Randomizing Next Upcoming Date Error", ex);
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

            IsUploading = false;
        }

        protected void RemoveRequiredDocument()
        {
            SelectedUpdateViewModel.UpdatedDocumentLink = null;
            UpdatedDocumentUploadFile = new();
        }

        public async Task KeyPressHandler(KeyboardEventArgs args)
        {
            if (args.Code == "Enter" && args.CtrlKey)
            {
                _toastService.ShowInfo("Saving Update..");
                await TrySaveUpdate(true);
            }
        }

        protected async Task CheckRecUpdGraphNo()
        {
            if (SelectedUpdateViewModel.GraphNumber < 0)
            {
                await jSRuntime.InvokeVoidAsync("AddRedBox", "RecUpdGraphNo");
            }
            else
            {
                await jSRuntime.InvokeVoidAsync("RemoveRedBox", "RecUpdGraphNo");
            }
        }

        protected async Task CheckRecUpdDesc()
        {
            if (RecTask.IsDescriptionMandatory == true)
            {
                if (string.IsNullOrEmpty(SelectedUpdateViewModel.Update))
                {
                    await jSRuntime.InvokeVoidAsync("AddRedBox", "RecUpdDesc");
                }
                else
                {
                    await jSRuntime.InvokeVoidAsync("RemoveRedBox", "RecUpdDesc");
                }
            }
        }

        protected void SelectUpdateImage(UploadChangeEventArgs args)
        {
            foreach (var file in args.Files)
            {
                var fileLookUp = UpdateUploadFiles.FirstOrDefault(x => x.FileInfo != null && file.FileInfo.Id == x.FileInfo.Id);
                if (fileLookUp == null)
                    UpdateUploadFiles.Add(new TasksImagesViewModel()
                    {
                        FileInfo = file.FileInfo,
                        ImageNote = string.Empty,
                        Stream = file.Stream,
                    });
            }

            IsUploading = false;
        }

        protected async Task DeleteSuccessFromDeleteFileComponent(string path)
        {
            if (!string.IsNullOrEmpty(path))
            {
                if (UpdateUploadFiles.Any())
                {
                    var File = UpdateUploadFiles[UpdateUploadFiles.FindIndex(x => x.FileInfo?.Id == path)].FileInfo;
                    var RemoveList = new FileInfo[1] { File };
                    await UpdateUploadObj.RemoveAsync(RemoveList);
                }
            }
        }

        protected void UseDeviceCamera()
        {
            IsActiveCameraComponent = true;
        }

        protected void RemoveUpdatePreImage(BeforeRemoveEventArgs args)
        {
            if (args.FilesData[0] == null)
                return;

            var RemoveFile = UpdateUploadFiles.FirstOrDefault(x => x.FileInfo?.Id == args.FilesData[0].Id);
            if (RemoveFile != null)
            {
                UpdateUploadFiles = UpdateUploadFiles.Where(x => x.FileInfo?.Id != args.FilesData[0].Id).ToList();
                RemoveFile.FileInfo = null;
            }

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

        protected void BeforeUploadImage()
        {
            IsUploading = true;
        }

        protected void SelectionAddChanged(ChangeEventArgs args)
        {
            selectedPassAnswer = args.Value.ToString();
            if (args.Value.ToString().ToLower() == "yes")
            {
                SelectedUpdateViewModel.IsPass = true;
            }
            else
            {
                SelectedUpdateViewModel.IsPass = false;
            }
        }

        protected async Task CheckPRecUpdFail()
        {
            if (string.IsNullOrEmpty(SelectedUpdateViewModel.FailReason))
            {
                await jSRuntime.InvokeVoidAsync("AddRedBox", "RecUpdFail");
            }
            else
            {
                await jSRuntime.InvokeVoidAsync("RemoveRedBox", "RecUpdFail");
            }
        }

        protected void MarkFailedCheck(ChangeEventArgs args, EmployeeEmail e) => e.IsSelected = (bool)args.Value;

        protected async Task CheckRecupdQuestion()
        {
            if (string.IsNullOrEmpty(SelectedUpdateViewModel.QuestionAnswer))
            {
                await jSRuntime.InvokeVoidAsync("AddRedBox", "RecupdQuestion");
            }
            else
            {
                await jSRuntime.InvokeVoidAsync("RemoveRedBox", "RecupdQuestion");
            }
        }

        protected async Task CloseComponent()
        {
            await jSRuntime.InvokeAsync<object>("HideModal", "#RecTaskUpdate");
            await CloseCreateRecUpdateComponent.InvokeAsync();
        }

        protected async Task SuccessMessageFromCameraComponent(Dictionary<Guid, string> capturedImgaesList)
        {
            if (capturedImgaesList != null && capturedImgaesList.Count > 0)
                CapturedImgaesList = capturedImgaesList;
            else
                CapturedImgaesList = null;

            await Task.Run(() => IsActiveCameraComponent = false);
        }

        public async Task MessageFromRecurringTaskPostponeComponent(TasksTaskUpdatesViewModel updateTask)
        {
            if (updateTask != null)
            {
                await Task.Run(() => SelectedUpdateViewModel.PostponeReason = updateTask.PostponeReason);
                await Task.Run(() => SelectedUpdateViewModel.PostponeDays = updateTask.PostponeDays);

                _toastService.ShowSuccess("Task Postpone save Successfully...");
            }
        }
    }
}
