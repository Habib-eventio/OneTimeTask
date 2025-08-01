using Append.Blazor.Printing;
using BlazorDownloadFile;
using Blazored.Toast.Services;
using CamcoTasks.Data.Services;
using CamcoTasks.Library;
using CamcoTasks.Service.IService;
using CamcoTasks.Infrastructure.Common.Email;
using CamcoTasks.ViewModels.EmployeeDTO;
using CamcoTasks.ViewModels.ModelsViewModel;
using CamcoTasks.ViewModels.TasksImagesDTO;
using CamcoTasks.ViewModels.TasksRecTasksDTO;
using CamcoTasks.ViewModels.TasksTaskUpdatesDTO;
using CamcoTasks.ViewModels.UpdateNotesDTO;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using Syncfusion.Blazor.Charts;
using Syncfusion.Blazor.Data;
using Syncfusion.Blazor.DropDowns;
using Syncfusion.Blazor.Grids;
using Syncfusion.Blazor.Inputs;
using Syncfusion.Blazor.Navigations;
using System.Net;
using System.Text.RegularExpressions;
using ChangeEventArgs = Microsoft.AspNetCore.Components.ChangeEventArgs;
using FileInfo = Syncfusion.Blazor.Inputs.FileInfo;
using System.Net.Mail;
using System.Text;
using System.Security.Policy;
using CamcoTasks.Infrastructure.Defaults;

namespace CamcoTasks.Pages.RecurringTasks
{
    public class ViewSubTasksModel : ComponentBase
    {
        private const string IMAGEFORMATS = @".jpg|.png|.gif|.jpeg|.bmp|.svg|.jfif|.apng|.ico$";
        private const string FILEFORMATS = @".pdf|.xlsx|.xls|.csv|.xlsb|.pptx|.docx$";

        [Inject]
        protected ITasksService taskService { get; set; }

        [Inject]
        protected IEmployeeService employeeService { get; set; }

        [Inject]
        protected IUpdateNotesService notesService { get; set; }

        [Inject]
        protected IPrintingService PrintingService { get; set; }

        [Inject]
        private IJSRuntime jSRuntime { get; set; }

        [Inject]
        private NavigationManager navigationManager { get; set; }

        [Inject]
        private FileManagerService fileManagerService { get; set; }

        [Inject]
        private IWebHostEnvironment webHostEnvironment { get; set; }

        [Inject]
        private IToastService _toastService { get; set; }

        [Inject]
        private IEmailService emailService { get; set; }

        [Inject]
        private IBlazorDownloadFileService blazorDownloadFileService { get; set; }

        [Parameter]
        public string TaskId { get; set; }

        protected TasksRecTasksViewModel MainTaskViewModel { get; set; } = new TasksRecTasksViewModel();
        protected Dictionary<string, object> htmlAttribute = new Dictionary<string, object>() { { "rows", "4" } };
        protected Dictionary<string, object> htmlAttributeBig = new Dictionary<string, object>() { { "rows", "7" }, { "spellcheck", "true" } };
        protected Dictionary<string, string> PreviewImages { get; set; } = new();
        protected Dictionary<string, FileInfo> PreviewImagesList { get; set; } = new();
        protected Dictionary<string, string> PreviewFiles { get; set; } = new();
        protected Dictionary<string, FileInfo> PreviewFilesList { get; set; } = new();

        protected List<TasksImagesViewModel> SelectedTaskFiles { get; set; } = new List<TasksImagesViewModel>();
        protected List<TasksImagesViewModel> SelectedTaskImages { get; set; } = new List<TasksImagesViewModel>();
        protected List<CustomGraphModel> customGraphModels { get; set; } = new List<CustomGraphModel>();
        protected List<TasksRecTasksViewModel> DeactivatedTasks { get; set; } = new List<TasksRecTasksViewModel>();
        protected List<TasksRecTasksViewModel> ActivatedRecurringTasks { get; set; } = new List<TasksRecTasksViewModel>();
        protected List<EmployeeEmail> employeeEmails { get; set; } = new List<EmployeeEmail>();
        protected List<EmployeeEmail> FailedemployeeEmails { get; set; } = new List<EmployeeEmail>();
        protected List<EmployeeEmail> AuditemployeeEmails { get; set; } = new List<EmployeeEmail>();
        protected List<TasksRecTasksViewModel> Tasks { get; set; } = new List<TasksRecTasksViewModel>();
        protected List<TasksTaskUpdatesViewModel> GraphUpdates { get; set; } = new List<TasksTaskUpdatesViewModel>();
        protected List<TasksTaskUpdatesViewModel> GraphUpdatesTemp { get; set; } = new List<TasksTaskUpdatesViewModel>();
        protected List<string> TasksCompleted { get; set; } = new List<string>();
        protected List<string> Frequencies { get; set; }
        protected List<TaskCompletionViewModel> EmployeesButtons { get; set; } = new List<TaskCompletionViewModel>();
        protected List<string> Employees { get; set; }
        protected List<string> EmployeesFilter { get; set; }
        protected List<UploadFiles> TaskUploadFiles { get; set; } = new List<UploadFiles>();
        protected List<UploadFiles> UpdateUploadFiles { get; set; } = new List<UploadFiles>();
        protected List<UploadFiles> SampleuploadFiles { get; set; } = new List<UploadFiles>();

        protected IEnumerable<EmployeeViewModel> EmployeeList { get; set; }

        protected string TypeDropdownVal { get; set; } = "All";
        protected string SelectedEmpId { get; set; }
        protected string TaskImage { get; set; }
        protected string TaskFile { get; set; }
        protected string UpdateRespondBody { get; set; } = "";
        protected string SimpleRespondBody { get; set; } = "";
        protected string RespondBody { get; set; } = "";
        protected string UpdateTitle { get; set; } = "ADD";
        protected string AllEmployees { get; set; } = "ALL EMPLOYEES";
        protected string AllEmployeesColor { get; set; } = "ALL EMPLOYEES";

        protected DateTime MinUpdateDate { get; set; } = DateTime.Today;
        protected SfUploader UploadObj { get; set; }
        protected SfUploader SampleUploadObj { get; set; }
        protected SfUploader UPdateUploadObj { get; set; }
        protected SfUploader TaskUploadRef { get; set; }
        protected UploadFiles UploadFileUpdate { get; set; } = new UploadFiles();

        protected int InActiveTasksCount { get; set; }
        protected int InActiveEmployeeCount { get; set; }
        protected int ReAssignTaskseCount { get; set; }
        protected int RecTasksCount { get; set; }
        protected int CompletedOnTimeCount { get; set; }
        protected int tempTaskId { get; set; }
        protected int AllEmployeesPercentage { get; set; }
        protected int RecentTaskImagesCount { get; set; }
        protected int RecentTaskFilesCount { get; set; }

        protected bool IsSpinner { get; set; } = true;
        protected bool IsDoneAudit { get; set; } = true;
        protected bool IsDonePass { get; set; } = true;
        protected bool IsTaskDone { get; set; }
        protected bool IsTaskGraphRequired { get; set; } = false;
        public bool IsUsingCamera { get; set; } = false;
        protected bool OpenChartTemp = false;
        protected bool IsDoingTask { get; set; } = false;

        private bool reLoad = true;

        protected SfGrid<TasksRecTasksViewModel> RecurringTasksGrid { get; set; }
        protected SfGrid<EmployeeEmail> EMailGrid1 { get; set; }
        protected SfGrid<EmployeeEmail> EMailGrid2 { get; set; }
        protected SfGrid<TasksRecTasksViewModel> DeactivatedGrid { get; set; }
        protected SfGrid<TasksTaskUpdatesViewModel> RecUpdateGrid { get; set; }
        protected SfGrid<UpdateNotesViewModel> NotesGrid { get; set; }

        protected SfComboBox<string, string> InitiatorBox { get; set; }
        protected SfComboBox<string, string> PersonBox { get; set; }
        protected SfComboBox<string, string> AddPersonBox { get; set; }

        protected SfTextBox updateDescription { get; set; }
        protected SfTextBox EmailNoteRef { get; set; }
        protected SfTextBox SimpleEmailNoteRef { get; set; }
        protected SfChart ChartObj { get; set; }
        protected SfChart ChartTempObj { get; set; }

        protected TasksRecTasksViewModel SelectedTaskViewModel { get; set; } = new TasksRecTasksViewModel();
        protected TasksRecTasksViewModel SelectedUpdateTaskViewModel { get; set; } = new TasksRecTasksViewModel();

        protected UpdateNotesViewModel updateNotesViewModel = new();
        protected TasksTaskUpdatesViewModel SelectedUpdateViewModel { get; set; } = new TasksTaskUpdatesViewModel()
        { UpdateDate = DateTime.Today, IsAudit = false, IsPass = false };
        protected TasksTaskUpdatesViewModel DeleteUpdateViewModel { get; set; } = new TasksTaskUpdatesViewModel()
        { UpdateDate = DateTime.Today, IsAudit = false, IsPass = false };

        protected UpdateNotesViewModel NoteEditDelete { get; set; } = new UpdateNotesViewModel();
        protected TasksTaskUpdatesViewModel NoteUpdateEditDelete { get; set; } = new TasksTaskUpdatesViewModel();

        protected override async Task OnInitializedAsync()
        {
            await LoadData();

            IsSpinner = false;
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                System.Threading.Thread.Sleep(2000);
                await jSRuntime.InvokeAsync<object>("modalDraggable");
            }
        }

        protected async Task LoadData()
        {
            if (string.IsNullOrEmpty(TaskId) || !int.TryParse(TaskId, out int value))
                return;

            try
            {
                MainTaskViewModel = await taskService.GetRecurringTaskById(Convert.ToInt32(TaskId));

                var _freqs = await taskService.GetTaskFreqs();
                Frequencies = _freqs.Select(a => a.Frequency).ToList();

                Tasks = (await taskService.GetSubTasks(MainTaskViewModel.Id)).ToList();
                Tasks = Tasks.OrderBy(a => a.UpcomingDate).ToList();
                RecTasksCount = Tasks.Count;

                var CompletionList = string.Join(";", Tasks.Select(x => x.CompletedOnTime?.Substring(x.CompletedOnTime.Length - 1, 1)));
                var TotalCompletedTasks = Regex.Matches(CompletionList, "1").Count;
                var TotalFaileTasks = Regex.Matches(CompletionList, "0").Count;
                if (TotalCompletedTasks != 0 && TotalFaileTasks != 0)
                    AllEmployeesPercentage = (TotalCompletedTasks * 100) / (TotalCompletedTasks + TotalFaileTasks);
                else AllEmployeesPercentage = 0;
                AllEmployeesColor = AllEmployeesPercentage >= 50 ? "#179B09" : "#CA0000";

                SetNewDefaults();
            }
            catch (Exception ex)
            {
                _toastService.ShowError(ex.Message + "Error");
            }

            int CompletedOne = 0, CompletedZero = 0;
            foreach (var IvTask in Tasks)
            {
                if (!string.IsNullOrEmpty(IvTask.CompletedOnTime))
                {
                    CompletedOne += IvTask.CompletedOnTime.Count(x => x == '1');
                    CompletedZero += IvTask.CompletedOnTime.Count(x => x == '0');
                }
            }

            await GetDeactivatedTasks();
        }

        protected async Task LoadCreateTaskData()
        {
            EmployeeList = await employeeService.GetListAsync(true);
            Employees = EmployeeList.Where(x => x.IsActive).Select(a => a.FullName).OrderBy(a => a).ToList();

            employeeEmails = EmployeeList.Where(x => !string.IsNullOrEmpty(x.Email) && x.IsActive).Select(x => new EmployeeEmail
            {
                Id = x.Id,
                FullName = x.FullName,
                Email = x.Email,
                IsSelected = false
            }).OrderBy(x => x.FullName).ToList();

            FailedemployeeEmails = employeeEmails.ToList();
            AuditemployeeEmails = employeeEmails.ToList();
        }

        protected int PossibleId { get; set; } = 0;
        protected string TaskTitle { get; set; } = "ADD NEW";
        protected async Task StartRecTask(TasksRecTasksViewModel RecTask)
        {
            if(EmployeeList == null)
            {
                await jSRuntime.InvokeAsync<object>("ShowModal", "#RecurringTaskLoadingModal");
                await LoadCreateTaskData();
            }

            await ClearRedBoxes();
            SelectedTaskViewModel = new();
            await TaskUploadRef.ClearAllAsync();
            TaskUploadFiles = new();

            if (RecTask == null)
            {
                TaskTitle = "ADD NEW";
                await StartNewTask();
            }
            else
            {
                TaskTitle = "EDIT";
                PossibleId = RecTask.Id;
                await StartEditTask(RecTask);
            }

            await jSRuntime.InvokeAsync<object>("HideModal", "#RecurringTaskLoadingModal");
            await jSRuntime.InvokeAsync<object>("ShowModal", "#RecurringTaskModal");

            await Task.Delay(200);
            await AddPersonBox.FocusIn();

            if (SelectedTaskViewModel.ImagesCount == 0 && SelectedTaskViewModel.FilesCount == 0) await TaskFileCount(RecTask);

        }

        protected async Task TaskFileCount(TasksRecTasksViewModel RecTask)
        {
            if (RecTask != null)
            {
                var TempTaskImages = await taskService.GetRecurringTaskImagesCountAsync(RecTask.Id);
                if (TempTaskImages != null)
                {
                    SelectedTaskViewModel.ImagesCount = TempTaskImages.Where(x => IsImage(x.PictureLink)).Count();
                    SelectedTaskViewModel.FilesCount = TempTaskImages.Where(x => IsFile(x.PictureLink)).Count();
                }
            }

        }

        protected async Task StartNewTask()
        {
            employeeEmails.ForEach(x => x.IsSelected = false);
            FailedemployeeEmails.ForEach(x => x.IsSelected = false);

            PossibleId = await taskService.GetMaxRecurringId();
            PossibleId++;

            TaskUploadFiles = new();
            RespondBody = "";
            if (TaskUploadRef != null)
                await TaskUploadRef.ClearAll();

            SelectedTaskViewModel = new TasksRecTasksViewModel();

            SelectedTaskViewModel = new TasksRecTasksViewModel
            {
                Initiator = "ARNOLD, RICHARD",
                Frequency = "Yearly",
                DateCreated = DateTime.Now,
                StartDate = DateTime.Today,
                NudgeCount = 0,
                IsPassOrFail = false,
                IsQuestionRequired = false,
                IsGraphRequired = false,
                IsPicRequired = false,
                IsDeactivated = false,
                IsDeleted = false,
                IsTrendLine = false,
                ParentTaskId = MainTaskViewModel.Id, 
                IsPicture = false
            };
        }
        protected List<TasksImagesViewModel> EditTaskImages { get; set; } = new List<TasksImagesViewModel>();
        protected async Task StartEditTask(TasksRecTasksViewModel RecTask)
        {
            EditTaskImages = (await taskService.GetRecurringTaskImagesAsync(RecTask.Id)).ToList();

            employeeEmails.ForEach(x => x.IsSelected = false);
            FailedemployeeEmails.ForEach(x => x.IsSelected = false);

            SelectedTaskViewModel = RecTask;

            if (!string.IsNullOrEmpty(SelectedTaskViewModel.EmailsList))
            {
                string[] splitEmails = SelectedTaskViewModel.EmailsList.Split(';');
                if (splitEmails.Any())
                {
                    foreach (var EmpEmail in splitEmails)
                    {
                        var email = employeeEmails.FirstOrDefault(x => x.Email == EmpEmail);

                        if (EmpEmail != null)
                        {
                            try
                            {
                                employeeEmails[employeeEmails.FindIndex(x => x.Email == EmpEmail)].IsSelected = true;
                            }
                            catch { continue; }
                        }
                    }
                }
            }

            if (EMailGrid2 != null)
            {
                EMailGrid2.Refresh();
                await EMailGrid2.RefreshColumns();
            }

            if (UploadObj != null)
                await UploadObj.ClearAll();

            await jSRuntime.InvokeAsync<object>("ShowModal", "#RecurringTaskModal");
        }
        protected async Task TrySaveTask()
        {
            IsDoingTask = true;
            if (SelectedTaskViewModel.Id == 0)
            {
                await AddNewTask();
            }
            else
            {
                await EditTask();
            }
            IsDoingTask = false;
        }
        protected async Task AddNewTask()
        {
            bool IsValid = true;
            try
            {
                if (IsUploading)
                {
                    _toastService.ShowError("Please Waitt For Uploading Files");
                    return;
                }

                if (string.IsNullOrEmpty(SelectedTaskViewModel.PersonResponsible))
                {
                    await jSRuntime.InvokeVoidAsync("AddRedBox", "PersonResponsible");
                    IsValid = false;
                }

                else if (!Employees.Contains(SelectedTaskViewModel.PersonResponsible))
                {
                    await jSRuntime.InvokeVoidAsync("AddRedBox", "PersonResponsible");
                    IsValid = false;
                }

                if (string.IsNullOrEmpty(SelectedTaskViewModel.Initiator))
                {
                    await jSRuntime.InvokeVoidAsync("AddRedBox", "Intiator");
                    IsValid = false;
                }
                else if (!Employees.Contains(SelectedTaskViewModel.Initiator))
                {
                    await jSRuntime.InvokeVoidAsync("AddRedBox", "Intiator");
                    IsValid = false;

                }

                if (string.IsNullOrEmpty(SelectedTaskViewModel.Frequency))
                {
                    await jSRuntime.InvokeVoidAsync("AddRedBox", "Freqncy");
                    return;
                }

                else if (!Frequencies.Contains(SelectedTaskViewModel.Frequency))
                {
                    await jSRuntime.InvokeVoidAsync("AddRedBox", "Freqncy");
                    IsValid = false;
                }

                if (!SelectedTaskViewModel.StartDate.HasValue)
                {
                    await jSRuntime.InvokeVoidAsync("AddRedBox", "StDate");
                    IsValid = false;
                }

                if (string.IsNullOrEmpty(SelectedTaskViewModel.Description))
                {
                    await jSRuntime.InvokeVoidAsync("AddRedBox", "TaskDescription");
                    IsValid = false;
                }

                if (string.IsNullOrEmpty(SelectedTaskViewModel.Question) && SelectedTaskViewModel.IsQuestionRequired == true)
                {
                    await jSRuntime.InvokeVoidAsync("AddRedBox", "QuestionD");
                    IsValid = false;
                }

                if (!IsValid)
                {
                    _toastService.ShowError("Please Fill Missing Fields");
                    return;
                }

                if (SelectedTaskViewModel.IsQuestionRequired == false)
                {
                    SelectedTaskViewModel.Question = "";
                }
                var shortString = SelectedTaskViewModel.StartDate.Value.ToShortDateString();
                DateTime dateTime = Convert.ToDateTime(shortString);

                if (SelectedTaskViewModel.UpcomingDate == null)
                {
                    SelectedTaskViewModel.UpcomingDate = SelectedTaskViewModel.StartDate;
                    SelectedTaskViewModel.UpcomingDate.Value.AddYears(1);
                }
                else if (dateTime.DayOfWeek.ToString().ToLower() == "saturday")
                {
                    SelectedTaskViewModel.UpcomingDate.Value.AddDays(2);
                }
                else if (dateTime.DayOfWeek.ToString().ToLower() == "sunday")
                {
                    SelectedTaskViewModel.UpcomingDate.Value.AddDays(1);
                }
                if (SelectedTaskViewModel.Id == 0)
                {
                    SelectedTaskViewModel = await taskService.AddRecurringTask(SelectedTaskViewModel);
                }

                foreach (var file in TaskUploadFiles)
                {
                    if (file.FileInfo != null)
                    {

                        if (!IsValidSize(file.FileInfo.Size))
                        {
                            var size = Convert.ToDouble(file.FileInfo.Size / 1000000);
                            _toastService.ShowError(string.Format("Image size can not be more than 20 mb. Your uploaded image size is {0} mb.", size));
                            return;
                        }

                        var ImageFileName = $"{Guid.NewGuid()}.{file.FileInfo.Type}";
                        var ResultPath = fileManagerService.CreateRecurringTaskDirectory(SelectedTaskViewModel.Id.ToString()) + ImageFileName;
                        fileManagerService.WriteToFile(file.Stream, ResultPath);
                        SelectedTaskViewModel.IsPicture = true;


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

                if (SelectedTaskViewModel.IsPicRequired == true)
                {
                    var file = SampleuploadFiles.FirstOrDefault();

                    if (file.FileInfo != null)
                    {
                        if (IsImage(file.FileInfo.Name.ToLower()))
                        {
                            if (!IsValidSize(file.FileInfo.Size))
                            {
                                var size = Convert.ToDouble(file.FileInfo.Size / 1000000);
                                _toastService.ShowError(string.Format("Image size can not be more than 20 mb. Your uploaded image size is {0} mb.", size));
                                return;
                            }

                            var ImageFileName = $"{Guid.NewGuid()}.{file.FileInfo.Type}";
                            var ResultPath = fileManagerService.CreateRecurringTaskDirectory(SelectedTaskViewModel.Id.ToString()) + ImageFileName;
                            fileManagerService.WriteToFile(file.Stream, ResultPath);
                            SelectedTaskViewModel.UpdateImageLoction = ResultPath;
                        }
                    }
                }

                SelectedTaskViewModel.IsDeleted = false;
                SelectedTaskViewModel.IsDeactivated = false;
                SelectedTaskViewModel.NudgeCount = 0;

                var SelectedEmails = employeeEmails.Where(x => x.IsSelected).OrderBy(x => x.FullName).ToList();

                if (SelectedEmails.Any())
                {
                    string EmailConcat = "";
                    foreach (var email in SelectedEmails)
                    {
                        EmailConcat += email.Email + ";";
                    }

                    if (!string.IsNullOrEmpty(EmailConcat))
                        if (!string.IsNullOrEmpty(EmailConcat))
                            EmailConcat = EmailConcat.Remove(EmailConcat.Length - 1);

                    SelectedTaskViewModel.EmailsList = EmailConcat;
                }

                var SelectedFailedEmails = FailedemployeeEmails.Where(x => x.IsSelected).OrderBy(x => x.FullName).ToList();
                if (SelectedFailedEmails.Any())
                {
                    string EmailConcat = "";
                    foreach (var email in SelectedFailedEmails)
                    {
                        EmailConcat += email.Email + ";";
                    }

                    if (!string.IsNullOrEmpty(EmailConcat))
                        EmailConcat = EmailConcat.Remove(EmailConcat.Length - 1);

                    SelectedTaskViewModel.FailedEmailsList = EmailConcat;
                }

                await SendEmail(SelectedTaskViewModel);

                await taskService.UpdateRecurringTask(SelectedTaskViewModel);

                if (SelectedTaskViewModel.Id != 0)
                {
                    SetNewDefaults();
                    if (TypeDropdownVal == "All")
                    {
                        Tasks = (await taskService.GetSubTasks(MainTaskViewModel.Id)).ToList();
                        RecTasksCount = Tasks.Count;
                    }
                    else
                    {
                        var modelTasks = await taskService.GetRecurringTasks(a => a.PersonResponsible == TypeDropdownVal && !a.IsDeleted
                        && !a.IsDeactivated);
                        Tasks = modelTasks.ToList();
                        RecTasksCount = Tasks.Count;
                    }

                    Tasks = Tasks.OrderBy(a => a.UpcomingDate).ToList();
                    RecTasksCount = Tasks.Count;

                    int CompletedOne = 0, CompletedZero = 0;
                    foreach (var IvTask in Tasks)
                    {
                        if (!string.IsNullOrEmpty(IvTask.CompletedOnTime))
                        {
                            CompletedOne += IvTask.CompletedOnTime.Count(x => x == '1');
                            CompletedZero += IvTask.CompletedOnTime.Count(x => x == '0');
                        }
                    }
                    if (CompletedOne != 0 && CompletedZero != 0)
                        CompletedOnTimeCount = (CompletedOne * 100) / (CompletedOne + CompletedZero);
                    else CompletedOnTimeCount = 0;

                    await jSRuntime.InvokeAsync<object>("HideModal", "#RecurringTaskModal");
                    _toastService.ShowSuccess("Task Added Successfully");
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    _toastService.ShowError(ex.InnerException.Message);
                }
                else
                {
                    _toastService.ShowError(ex.Message);
                }
            }
            await ClearRedBoxes();
            CleanTaskFiles();
        }
        protected async Task EditTask()
        {
            try
            {
                if (IsUploading)
                {
                    _toastService.ShowError("Please Waitt For Uploading Files");
                    return;
                }

                if (string.IsNullOrEmpty(SelectedTaskViewModel.PersonResponsible))
                {
                    _toastService.ShowError("Person Responsible Can't Be Empty");
                    return;
                }

                else if (!Employees.Contains(SelectedTaskViewModel.PersonResponsible))
                {
                    _toastService.ShowError("Please Enter Correct Person Responsible");
                    return;
                }

                if (string.IsNullOrEmpty(SelectedTaskViewModel.Initiator))
                {
                    _toastService.ShowError("Initiator Can't Be Empty");
                    return;
                }

                else if (!Employees.Contains(SelectedTaskViewModel.Initiator))
                {
                    _toastService.ShowError("Please Enter Correct Initiator");
                    return;
                }

                if (string.IsNullOrEmpty(SelectedTaskViewModel.Frequency))
                {
                    _toastService.ShowError("Frequency Can't Be Empty");
                    return;
                }

                else if (!Frequencies.Contains(SelectedTaskViewModel.Frequency))
                {
                    _toastService.ShowError("Please Enter Correct Frequency");
                    return;
                }
                var shortString = SelectedTaskViewModel.StartDate.Value.ToShortDateString();
                DateTime dateTime = Convert.ToDateTime(shortString);
                if (SelectedTaskViewModel.UpcomingDate == null)
                {
                    SelectedTaskViewModel.UpcomingDate = SelectedTaskViewModel.StartDate;
                    SelectedTaskViewModel.UpcomingDate.Value.AddYears(1);
                }
                else if (dateTime.DayOfWeek.ToString().ToLower() == "saturday")
                {
                    SelectedTaskViewModel.UpcomingDate.Value.AddDays(2);
                }
                else if (dateTime.DayOfWeek.ToString().ToLower() == "sunday")
                {
                    SelectedTaskViewModel.UpcomingDate.Value.AddDays(1);
                }

                if (!SelectedTaskViewModel.StartDate.HasValue)
                {
                    _toastService.ShowError("Start Date Can't Be Empty");
                    return;
                }

                if (string.IsNullOrEmpty(SelectedTaskViewModel.Description))
                {
                    _toastService.ShowError("Description Can't Be Empty");
                    return;
                }

                if (string.IsNullOrEmpty(SelectedTaskViewModel.Question) && SelectedTaskViewModel.IsQuestionRequired == true)
                {
                    _toastService.ShowError("Please ask question..!");
                    return;
                }

                foreach (var file in TaskUploadFiles)
                {
                    if (file.FileInfo != null)
                    {

                        if (!IsValidSize(file.FileInfo.Size))
                        {
                            var size = Convert.ToDouble(file.FileInfo.Size / 1000000);
                            _toastService.ShowError(string.Format("Image size can not be more than 20 mb. Your uploaded image size is {0} mb.", size));
                            return;
                        }

                        var ImageFileName = $"{Guid.NewGuid()}.{file.FileInfo.Type}";
                        var ResultPath = fileManagerService.CreateRecurringTaskDirectory(SelectedTaskViewModel.Id.ToString()) + ImageFileName;
                        fileManagerService.WriteToFile(file.Stream, ResultPath);
                        SelectedTaskViewModel.IsPicture = true;

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

                if (SelectedTaskViewModel.IsPicRequired == true)
                {
                    if (SampleuploadFiles != null)
                    {
                        var file = SampleuploadFiles.FirstOrDefault();

                        if (file != null)
                        {
                            if (file.FileInfo != null)
                            {
                                if (IsImage(file.FileInfo.Name.ToLower()))
                                {
                                    if (!IsValidSize(file.FileInfo.Size))
                                    {
                                        var size = Convert.ToDouble(file.FileInfo.Size / 1000000);
                                        _toastService.ShowError(string.Format("Image size can not be more than 20 mb. Your uploaded image size is {0} mb.", size));
                                        return;
                                    }

                                    var ImageFileName = $"{Guid.NewGuid()}.{file.FileInfo.Type}";
                                    var ResultPath = fileManagerService.CreateRecurringTaskDirectory(SelectedTaskViewModel.Id.ToString()) + ImageFileName;
                                    fileManagerService.WriteToFile(file.Stream, ResultPath);
                                    SelectedTaskViewModel.UpdateImageLoction = ResultPath;
                                }
                            }
                        }
                    }
                }

                var SelectedEmails = employeeEmails.Where(x => x.IsSelected).OrderBy(x => x.FullName).ToList();
                if (SelectedEmails.Any())
                {
                    string EmailConcat = "";
                    foreach (var email in SelectedEmails)
                    {
                        EmailConcat += email.Email + ";";
                    }

                    if (!string.IsNullOrEmpty(EmailConcat))
                        EmailConcat = EmailConcat.Remove(EmailConcat.Length - 1);

                    SelectedTaskViewModel.EmailsList = EmailConcat;
                }

                var SelectedFailedEmails = FailedemployeeEmails.Where(x => x.IsSelected).OrderBy(x => x.FullName).ToList();
                if (SelectedFailedEmails.Any())
                {
                    string EmailConcat = "";
                    foreach (var email in SelectedFailedEmails)
                    {
                        EmailConcat += email.Email + ";";
                    }

                    if (!string.IsNullOrEmpty(EmailConcat))
                        EmailConcat = EmailConcat.Remove(EmailConcat.Length - 1);

                    SelectedTaskViewModel.FailedEmailsList = EmailConcat;
                }

                var UpdatedTask = await taskService.UpdateRecurringTask(SelectedTaskViewModel);

                Tasks[Tasks.FindIndex(x => x.Id == SelectedTaskViewModel.Id)] = UpdatedTask;
                Tasks = Tasks.OrderBy(a => a.UpcomingDate).ToList();
                RecTasksCount = Tasks.Count;

                int CompletedOne = 0, CompletedZero = 0;
                foreach (var IvTask in Tasks)
                {
                    if (!string.IsNullOrEmpty(IvTask.CompletedOnTime))
                    {
                        CompletedOne += IvTask.CompletedOnTime.Count(x => x == '1');
                        CompletedZero += IvTask.CompletedOnTime.Count(x => x == '0');
                    }
                }
                if (CompletedOne != 0 && CompletedZero != 0)
                    CompletedOnTimeCount = (CompletedOne * 100) / (CompletedOne + CompletedZero);
                else CompletedOnTimeCount = 0;
                
                SetNewDefaults();
                RecurringTasksGrid.Refresh();
                await RecurringTasksGrid.RefreshColumns();
                _toastService.ShowSuccess("Task Updated Successfully");
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    _toastService.ShowError(ex.InnerException.Message);
                }
                else
                {
                    _toastService.ShowError(ex.Message);
                }
            }
            await jSRuntime.InvokeAsync<object>("HideModal", "#RecurringTaskModal");
            await ClearRedBoxes();
            CleanTaskFiles();
        }
        protected async Task CheckDescription()
        {
            if (string.IsNullOrEmpty(SelectedTaskViewModel.Description))
            {
                await jSRuntime.InvokeVoidAsync("AddRedBox", "TaskDescription");
            }
            else
            {
                await jSRuntime.InvokeVoidAsync("RemoveRedBox", "TaskDescription");
            }
        }
        protected async Task CheckPersonResponsible()
        {
            if (string.IsNullOrEmpty(SelectedTaskViewModel.PersonResponsible))
            {
                await jSRuntime.InvokeVoidAsync("AddRedBox", "PersonResponsible");
            }
            else
            {
                await jSRuntime.InvokeVoidAsync("RemoveRedBox", "PersonResponsible");
            }
        }
        protected async Task CheckInitiator()
        {
            if (string.IsNullOrEmpty(SelectedTaskViewModel.Initiator))
            {
                await jSRuntime.InvokeVoidAsync("AddRedBox", "Intiator");
            }
            else
            {
                await jSRuntime.InvokeVoidAsync("RemoveRedBox", "Intiator");
            }
        }
        protected async Task StartDeleteTask(TasksRecTasksViewModel task)
        {
            SelectedTaskViewModel = task;
            await jSRuntime.InvokeAsync<object>("ShowModal", "#deleteRecurringTask");
        }
        protected async Task DeleteTask()
        {
            try
            {
                SelectedTaskViewModel.IsDeleted = true;
                await taskService.UpdateRecurringTask(SelectedTaskViewModel);

                int Index = Tasks.FindIndex(x => x.Id == SelectedTaskViewModel.Id);
                Tasks.RemoveAt(Index);
                Tasks = Tasks.OrderBy(a => a.UpcomingDate).ToList();
                RecTasksCount = Tasks.Count;

                int CompletedOne = 0, CompletedZero = 0;
                foreach (var IvTask in Tasks)
                {
                    if (!string.IsNullOrEmpty(IvTask.CompletedOnTime))
                    {
                        CompletedOne += IvTask.CompletedOnTime.Count(x => x == '1');
                        CompletedZero += IvTask.CompletedOnTime.Count(x => x == '0');
                    }
                }
                if (CompletedOne != 0 && CompletedZero != 0)
                    CompletedOnTimeCount = (CompletedOne * 100) / (CompletedOne + CompletedZero);
                else CompletedOnTimeCount = 0;

                SetNewDefaults();

                RecurringTasksGrid.Refresh();
                await RecurringTasksGrid.RefreshColumns();

                _toastService.ShowSuccess("Task Deleted Successfully");
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    _toastService.ShowError(ex.InnerException.Message);
                }
                else
                {
                    _toastService.ShowError(ex.Message);
                }
            }
        }
        protected void StartDeactivateTask(TasksRecTasksViewModel vieWModel)
        {
            SelectedTaskViewModel = vieWModel;
        }
        protected async Task DeactivateTask(TasksRecTasksViewModel vieWModel)
        {
            if (vieWModel != null)
                SelectedTaskViewModel = vieWModel;
            try
            {
                SelectedTaskViewModel.IsDeactivated = true;
                await taskService.UpdateRecurringTask(SelectedTaskViewModel);

                int Index = Tasks.FindIndex(x => x.Id == SelectedTaskViewModel.Id);
                Tasks.RemoveAt(Index);
                Tasks = Tasks.OrderBy(a => a.UpcomingDate).ToList();
                RecTasksCount = Tasks.Count;

                int CompletedOne = 0, CompletedZero = 0;
                foreach (var IvTask in Tasks)
                {
                    if (!string.IsNullOrEmpty(IvTask.CompletedOnTime))
                    {
                        CompletedOne += IvTask.CompletedOnTime.Count(x => x == '1');
                        CompletedZero += IvTask.CompletedOnTime.Count(x => x == '0');
                    }
                }
                if (CompletedOne != 0 && CompletedZero != 0)
                    CompletedOnTimeCount = (CompletedOne * 100) / (CompletedOne + CompletedZero);
                else CompletedOnTimeCount = 0;

                SetNewDefaults();

                RecurringTasksGrid.Refresh();
                await RecurringTasksGrid.RefreshColumns();

                ReAssignTaskseCount++;
                _toastService.ShowSuccess("Task Deactivated Successfully");
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    _toastService.ShowError(ex.InnerException.Message);
                }
                else
                {
                    _toastService.ShowError(ex.InnerException.Message);
                }
            }
        }
        protected async Task GetDeactivatedTasks()
        {
            DeactivatedTasks = (await taskService.GetRecurringTasks(x => !x.IsDeleted && x.IsDeactivated)).ToList();
            ReAssignTaskseCount = DeactivatedTasks.Count;
        }
        protected async Task GetActivatedTasks()
        {
            if (reLoad)
            {
                ActivatedRecurringTasks = (await taskService.GetRecurringTasks(x => x.IsDeleted == false && x.IsDeactivated == false)).ToList();
                reLoad = false;
            }
        }
        public async Task SearchValueChange(Syncfusion.Blazor.Inputs.ChangedEventArgs args)
        {
            await DeactivatedGrid.SearchAsync(args.Value);
        }
        protected async Task SendEmail(TasksRecTasksViewModel task, bool Nudeged, string RespondBody = null)
        {

            var employee = await employeeService.GetEmployee(task.PersonResponsible);
            if (employee != null)
            {
                if (string.IsNullOrEmpty(employee.Email))
                {
                    _toastService.ShowWarning("Email Sending Error, Employee Has No Email");
                    return;
                }
                string body = "";

                if (!string.IsNullOrEmpty(RespondBody))
                    body = "Note: " + RespondBody + "<br>";

                body += "<label style=\"font-weight:bold\"> Description: </label>" + task.Description.ToUpper() +
               "<br><label style=\"font-weight:bold\"> Upcoming Date: </label>" + task.UpcomingDate?.Date.ToString("MM/dd/yyyy") +
               "<br><label style=\"font-weight:bold\"> Frequency: </label>" + task.Frequency +
               "<br><label style=\"font-weight:bold\"> Link To Mark Completed: </label> http://metrics.db.camco.local/viewrecurringtasks/OpenTask/" + task.Id.ToString();

                string Subject = "FRIENDLY REMINDER TO GET THIS TASK DONE";

                body = EmailDefaults.GenerateEmailTemplate("Tasks", body);
                await emailService.SendEmailAsync(EmailTypes.ActionBasedFriendlyReminderRecurringTask,
                    Array.Empty<string>(), Subject, body, string.Empty, new string[] { employee.Email });

                if (Nudeged)
                {
                    task.NudgeCount++;
                    await taskService.UpdateRecurringTask(task);
                }

                _toastService.ShowInfo("An Email Has Been Added To Queue");
            }
        }
        protected async Task SendEmail(TasksRecTasksViewModel newTask)
        {
            var employee = await employeeService.GetEmployee(newTask.PersonResponsible);
            if (employee != null)
            {
                if (string.IsNullOrEmpty(employee.Email))
                {
                    _toastService.ShowWarning("Email Sending Error, Employee Has No Email");
                    return;
                }

                StringBuilder customBody = new();
                customBody.Append($"<p><b>Task Id: </b>{newTask.Id}</p>");
                customBody.Append($"<p><b>Description: </b>{newTask.Description.ToUpper()}</p>");
                customBody.Append($"<p><b>Upcoming Date: </b>{newTask.UpcomingDate}</p>");
                customBody.Append($"<p><b>Frequency: </b>{newTask.Frequency}</p>");
                customBody.Append($"<p><b>Created By: </b>{newTask.Initiator}</p>");
                customBody.Append($"<p><b>Assigned By: </b>{newTask.PersonResponsible}</p>");
                customBody.Append($"<p><b>Link: </b> http://metrics.db.camco.local/viewrecurringtasks/OpenTask/{newTask.Id.ToString()}</p>");

                string Subject = "A NEW TASK HAS BEEN CREATED";
                string body = EmailDefaults.GenerateEmailTemplate("Tasks", customBody.ToString());
                await emailService.SendEmailAsync(EmailTypes.ActionBasedRecurringTaskCreated,
                    Array.Empty<string>(), Subject, body, string.Empty, new string[] { employee.Email });

                _toastService.ShowInfo("An Email Has Been Added To Queue");
            }
        }

        protected async Task SendEmailForEditTaskUpdate(TasksTaskUpdatesViewModel EditTaskUpd)
        {
            StringBuilder customBody = new();
            customBody.Append($"<p><b>Task Id: </b>{EditTaskUpd.RecurringID}</p>");
            customBody.Append($"<p><b>Description: </b>{EditTaskUpd.Update}</p>");
            customBody.Append($"<p><b>Upcoming Date: </b>{EditTaskUpd.UpcomingDate}</p>");
            customBody.Append($"<p><b>Frequency: </b>{SelectedTaskViewModel.Frequency}</p>");
            customBody.Append($"<p><b>Person Responsible: </b>{SelectedTaskViewModel.PersonResponsible}</p>");

            string Subject = "UPDATING TASK UPDATE";

            string body = EmailDefaults.GenerateEmailTemplate("Tasks", customBody.ToString());
            await emailService.SendEmailAsync(EmailTypes.ActionBasedTaskUpdateUpdated,
                Array.Empty<string>(), Subject, body, string.Empty, new string[] { "rarnold@camcomfginc.com" });

            _toastService.ShowInfo("An Email Has Been Added To Queue");

        }
        protected async Task StartPrinting(ClickEventArgs args)
        {
            if (args.Item.Text == "Print Report")
            {
                _toastService.ShowSuccess("Generating Report Started, Please Wait.");
                var rows = await RecurringTasksGrid.GetFilteredRecords();
                var TasksList = JsonConvert.DeserializeObject<List<TasksRecTasksViewModel>>(JsonConvert.SerializeObject(rows));
                var pdf = fileManagerService.CreatePdfInMemory(TasksList);
                await jSRuntime.InvokeVoidAsync("jsSaveAsFile", "SubRecurringTasks.pdf", Convert.ToBase64String(pdf));
            }
            else if (args.Item.Text == "TASK DUE IN NEXT 7 DAYS")
            {
                await FilteGrid7DaysDueDate();
            }
            else if (args.Item.Text == "PAST DUE TASKS")
            {
                await FilteGridPastDueDate();
            }

            else if (args.Item.Text == "Clear Filters")
            {
                await RecurringTasksGrid.ClearFiltering();
                ClearFilter();
            }
        }

        protected List<object> Toolbaritems = new()
        {
            "Search",
            new ItemModel() { Text = "Print Report", TooltipText = "Print", PrefixIcon = "e-print", Id = "Print" },
            new ItemModel() { Text = "TASK DUE IN NEXT 7 DAYS", TooltipText = "Filter", PrefixIcon = "e-filter", Id = "Filter7" },
            new ItemModel() { Text = "PAST DUE TASKS", TooltipText = "Filter", PrefixIcon = "e-filter", Id = "Filter8" },
            new ItemModel() { Text = "Clear Filters", TooltipText = "Filter", PrefixIcon = "e-erase", Id = "FilterClear" }
        };

        public Query GridQuery { get; set; }
        public async Task FilteGrid7DaysDueDate()
        {
            GridQuery = new Query();
            await RecurringTasksGrid.ClearFiltering();
            List<WhereFilter> Predicate = new();
            Predicate.Add(new WhereFilter()
            {
                Field = nameof(TasksRecTasksViewModel.UpcomingDate),
                value = DateTime.Now,
                Operator = "greaterthanorequal",
                IgnoreCase = true
            });
            Predicate.Add(new WhereFilter()
            {
                Field = nameof(TasksRecTasksViewModel.UpcomingDate),
                value = DateTime.Now.AddDays(7),
                Operator = "lessthanorequal",
                IgnoreCase = true
            });
            var ColPre = WhereFilter.And(Predicate);
            GridQuery = new Query().Where(ColPre);
        }

        public async Task FilteGridPastDueDate()
        {
            ClearFilter();
            await RecurringTasksGrid.ClearFiltering();
            GridQuery = new Query();
            List<WhereFilter> Predicate = new List<WhereFilter>();
            Predicate.Add(new WhereFilter()
            {
                Field = nameof(TasksRecTasksViewModel.UpcomingDate),
                value = DateTime.Now,
                Operator = "lessthanorequal",
                IgnoreCase = true
            });
            WhereFilter ColPre = WhereFilter.And(Predicate);
            GridQuery = new Query().Where(ColPre);
        }
        public void ClearFilter()
        {
            GridQuery = new Query();
        }

        public async Task ChangeTypeData(Syncfusion.Blazor.DropDowns.ChangeEventArgs<string> args)
        {
            TypeDropdownVal = args.Value;

            if (TypeDropdownVal == "All")
            {
                Tasks = (await taskService.GetSubTasks(MainTaskViewModel.Id)).ToList();
            }
            else
            {
                var tasksModel = (await taskService.GetSubTasks(MainTaskViewModel.Id)).ToList();
                Tasks = tasksModel.Where(a => a.PersonResponsible == TypeDropdownVal && !a.IsDeleted && !a.IsDeactivated).ToList();
            }

            Tasks = Tasks.OrderBy(a => a.UpcomingDate).ToList();
            RecTasksCount = Tasks.Count;

            int CompletedOne = 0, CompletedZero = 0;
            foreach (var IvTask in Tasks)
            {
                if (!string.IsNullOrEmpty(IvTask.CompletedOnTime))
                {
                    CompletedOne += IvTask.CompletedOnTime.Count(x => x == '1');
                    CompletedZero += IvTask.CompletedOnTime.Count(x => x == '0');
                }
            }
            if (CompletedOne != 0 && CompletedZero != 0)
                CompletedOnTimeCount = (CompletedOne * 100) / (CompletedOne + CompletedZero);
            else CompletedOnTimeCount = 0;

            RecurringTasksGrid.Refresh();
            await RecurringTasksGrid.RefreshColumns();
        }

        private string ChartImage = string.Empty;
        protected async Task GetChartImage(ExportEventArgs args)
        {
            ChartImage = args.DataUrl;
            await MarkCompleted();
        }
        protected async Task ExportAndMailGraph()
        {
            ChartImage = string.Empty;
            await ChartTempObj.ExportAsync(Syncfusion.Blazor.Charts.ExportType.PNG, "Chart",
                 Syncfusion.PdfExport.PdfPageOrientation.Portrait, false);
        }
        protected async Task MarkCompleted()
        {
            SelectedTaskViewModel.DateCompleted = DateTime.Now;
            if (string.IsNullOrEmpty(SelectedTaskViewModel.CompletedOnTime))
            {
                if (SelectedTaskViewModel.UpcomingDate?.Date >= SelectedTaskViewModel.DateCompleted?.Date)
                    SelectedTaskViewModel.CompletedOnTime = "1";
                else
                    SelectedTaskViewModel.CompletedOnTime = "0";
            }
            else
            {
                var splittedValues = SelectedTaskViewModel.CompletedOnTime.Split(";").ToList();
                if (splittedValues.Count > 10)
                {
                    var difference = splittedValues.Count - 9;
                    splittedValues.RemoveRange(0, difference);

                    SelectedTaskViewModel.CompletedOnTime = splittedValues[0];

                    for (int i = 1; i < splittedValues.Count; i++)
                    {
                        SelectedTaskViewModel.CompletedOnTime += $";{splittedValues[i]}";
                    }
                }

                if (SelectedTaskViewModel.UpcomingDate?.Date >= SelectedTaskViewModel.DateCompleted?.Date)
                    SelectedTaskViewModel.CompletedOnTime += ";1";
                else
                    SelectedTaskViewModel.CompletedOnTime += ";0";
            }

            var UpdatedTask = await taskService.UpdateRecurringTask(SelectedTaskViewModel);

            Tasks[Tasks.FindIndex(x => x.Id == SelectedTaskViewModel.Id)] = UpdatedTask;
            Tasks = Tasks.OrderBy(a => a.UpcomingDate).ToList();
            RecTasksCount = Tasks.Count;

            if (SelectedTaskViewModel != null)
                SelectedTaskViewModel.UpcomingDate = UpdatedTask.UpcomingDate;

            if (!string.IsNullOrEmpty(SelectedTaskViewModel.EmailsList))
            {
                string[] splitEmails = SelectedTaskViewModel.EmailsList.Split(';');
                if (splitEmails.Any())
                {
                    foreach (var EmpEmail in splitEmails)
                    {
                        string body = "<label style=\"font-weight:bold\"> Description: </label>" + SelectedTaskViewModel.Description.ToUpper() +
                            "<br><label style=\"font-weight:bold\"> Upcoming Date: </label>" + SelectedTaskViewModel.UpcomingDate?.Date.ToString("MM/dd/yyyy") +
                            "<br><label style=\"font-weight:bold\"> Frequency: </label>" + SelectedTaskViewModel.Frequency +
                            "<br><label style=\"font-weight:bold\"> Person Responsible: </label>" + SelectedTaskViewModel.PersonResponsible +
                            "<br><label style=\"font-weight:bold\"> Due Date: </label>" + SelectedTaskViewModel.UpcomingDate +
                            $"<br><label style=\"font-weight:bold; font-size:14px; color:red;\"> Task ID: {SelectedTaskViewModel.Id} </label>" +
                            "<br><label style=\"font-weight:bold\"> Update: </label>" + SelectedUpdateViewModel.Update;

                        if (SelectedTaskViewModel.IsGraphRequired  == true)
                        {
                            body += $"<br><label style=\"font-weight:bold\"> {SelectedTaskViewModel.GraphTitle} Value: </label>" + SelectedUpdateViewModel.GraphNumber;
                        }

                        if (SelectedTaskViewModel.IsQuestionRequired == true)
                        {
                            body += $"<br><label style=\"font-weight:bold\"> Question: </label>" + SelectedTaskViewModel.Question;
                            body += $"<br><label style=\"font-weight:bold\"> Question Answer: </label>" + SelectedUpdateViewModel.QuestionAnswer;
                        }

                        string Subject = "TASK COMPLETED";

                        if (SelectedTaskViewModel.IsPassOrFail == true)
                        {
                            body += $"<br><label style=\"font-weight:bold\"> Status: </label>" + (SelectedUpdateViewModel.IsPass == true ? "Passed" : "Failed");

                            if (SelectedUpdateViewModel.IsPass == false)
                                Subject = "TASK FAILED";
                        }

                        string Attachment = "";
                        if (SelectedTaskViewModel.IsPicRequired == true)
                        {
                            body += $"<br><label style=\"font-weight:bold\"> Image Note: </label>" + SelectedTaskViewModel.UpdateImageDescription;
                            Attachment = SelectedUpdateViewModel.PictureLink + ":" + SelectedTaskViewModel.UpdateImageLoction;
                        }

                        if (SelectedTaskViewModel.IsGraphRequired == true)
                        {
                            /*
                             <img style="display:block;width:100%;max-width:100%;border:0px;border-radius:0px" width="640" 
                            src="" border="0" alt="picture">
                             * */
                            if (!string.IsNullOrEmpty(ChartImage))
                            {
                                body += $"<br><img style=\"width:60%; max-width:100%; border:0px; border-radius:0px;\" " +
                                    $"src=\"{ChartImage}\" alt=\"picture\">";
                            }
                        }
                        body = EmailDefaults.GenerateEmailTemplate("Tasks", body);
                        await emailService.SendEmailAsync(EmailTypes.ActionBasedTaskCompletionStatus,
                            Array.Empty<string>(), Subject, body, Attachment, new string[] { EmpEmail });
                    }
                }
            }

            RecurringTasksGrid.Refresh();
            await RecurringTasksGrid.RefreshColumns();

            await jSRuntime.InvokeAsync<object>("HideModal", "#RecurringTaskModal");

            int CompletedOne = 0, CompletedZero = 0;
            foreach (var IvTask in Tasks)
            {
                if (!string.IsNullOrEmpty(IvTask.CompletedOnTime))
                {
                    CompletedOne += IvTask.CompletedOnTime.Count(x => x == '1');
                    CompletedZero += IvTask.CompletedOnTime.Count(x => x == '0');
                }
            }
            if (CompletedOne != 0 && CompletedZero != 0)
                CompletedOnTimeCount = (CompletedOne * 100) / (CompletedOne + CompletedZero);
            else CompletedOnTimeCount = 0;

            _toastService.ShowSuccess("Task Updated Successfully");
        }

        protected async Task StartRespond(TasksTaskUpdatesViewModel task)
        {
            RespondBody = "";
            SelectedUpdateViewModel = task;
            await jSRuntime.InvokeAsync<object>("ShowModal", "#RespondModal");
            await Task.Delay(200);
            await EmailNoteRef.FocusIn();
        }

        protected async Task StartSampleImaageAdd()
        {
            await SampleUploadObj.ClearAllAsync();
            SampleuploadFiles = null;
        }

        protected async Task CheckUpdateMailBody()
        {
            if (string.IsNullOrEmpty(UpdateRespondBody))
            {
                await jSRuntime.InvokeVoidAsync("AddRedBox", "UpdateMailBody");
            }
            else
            {
                await jSRuntime.InvokeVoidAsync("RemoveRedBox", "UpdateMailBody");
            }
        }

        protected async Task SendUpdatedEmail()
        {
            if (string.IsNullOrEmpty(UpdateRespondBody))
            {
                await jSRuntime.InvokeVoidAsync("AddRedBox", "UpdateMailBody");
                _toastService.ShowError("Please Enter a Note");
                return;
            }
            else
            {
                await jSRuntime.InvokeVoidAsync("RemoveRedBox", "UpdateMailBody");
            }

            var Task = await taskService.GetRecurringTaskById((int)SelectedUpdateViewModel.RecurringID);

            var employee = await employeeService.GetEmployee(Task.PersonResponsible);
            if(employee != null)
            {
                if(employee.Email != null)
                {
                    string body = "Note: " + UpdateRespondBody + "<br>";
                    body += "<br><h2 style=\"text-decoration: underline; font-weight:bold; margin-bottom:5px;\">" +
                       "Recurring Task Description: </h2>"
                       + "<h4 style=\"margin-top: 9px; margin-left: 5px;\">" + Task.Description.ToUpper() + "</h4>" +
                       "<br><h2 style=\"text-decoration: underline; font-weight:bold; margin-bottom:5px;\">" +
                       "Rich Arnold Response: </h2>" + "<h4 style=\"margin-top: 9px; margin-left: 5px;\">" + RespondBody + "</h4>"

                       + "<br><br><label style=\"font-weight:bold; font-size: 20px;\">Link To Task:</label> " +
                          $"<a href=\"http://metrics.db.camco.local/viewrecurringtasks/" +
                          employee.FirstName + "/" + employee.LastName + "\" target=\"_blank\"> "
                          + $" {employee.FirstName} {employee.LastName} </a>";

                    //if (!string.IsNullOrEmpty(SelectedUpdateViewwModel.PictureLink))
                    //    body += "<br><h3 style=\"font-weight:bold\"Image: </h3>" + SelectedUpdateViewwModel.PictureLink;

                    if (!string.IsNullOrEmpty(SelectedUpdateViewModel.FileLink))
                        body += "<br><label style=\"font-weight:bold\"> File: </label>" + SelectedUpdateViewModel.FileLink;


                    string Subject = "RICH ARNOLD SENT A RESPONSE";

                    body = EmailDefaults.GenerateEmailTemplate("Tasks", body);
                    await emailService.SendEmailAsync(EmailTypes.ActionBasedResponseForRecurringTask,
                        Array.Empty<string>(), Subject, body, string.Empty, new string[] { employee.Email });

                    await taskService.UpdateRecurringTask(Task);

                    _toastService.ShowInfo("An Email Has Been Added To Queue");

                    await jSRuntime.InvokeAsync<object>("HideModal", "#RespondModal");

                    RespondBody = string.Empty;
                    SelectedUpdateViewModel = new TasksTaskUpdatesViewModel();
                }
                else _toastService.ShowError("Email is not found!");
            }
            else _toastService.ShowError("Employee is not found!");

        }

        TasksRecTasksViewModel EmailTask { get; set; } = new TasksRecTasksViewModel();
        protected async Task StartEmail(TasksRecTasksViewModel task)
        {
            RespondBody = "";
            EmailTask = task;
            await jSRuntime.InvokeAsync<object>("ShowModal", "#SimpleEmailModal");
            await Task.Delay(200);
            await SimpleEmailNoteRef.FocusIn();
        }

        protected async Task CheckEmailBodyNote()
        {
            if (string.IsNullOrEmpty(SimpleRespondBody))
            {
                await jSRuntime.InvokeVoidAsync("AddRedBox", "EmailBodyNote");
            }
            else
            {
                await jSRuntime.InvokeVoidAsync("RemoveRedBox", "EmailBodyNote");
            }
        }

        protected async Task SendTaskEmail()
        {
            if (string.IsNullOrEmpty(SimpleRespondBody))
            {
                await jSRuntime.InvokeVoidAsync("AddRedBox", "EmailBodyNote");
                _toastService.ShowError("Please Enter a Note");
                return;
            }
            else if (!string.IsNullOrEmpty(SimpleRespondBody))
            {
                await jSRuntime.InvokeVoidAsync("RemoveRedBox", "EmailBodyNote");
            }

            var employee = await employeeService.GetEmployee(EmailTask.PersonResponsible);

            if(employee != null)
            {
                if(employee.Email != null)
                {
                    string body = "<br><h2 style=\"text-decoration: underline; font-weight:bold; margin-bottom:5px;\">" +
                "Recurring Task Description: </h2>"
                + "<h4 style=\"margin-top: 9px; margin-left: 5px;\">" + EmailTask.Description.ToUpper() + "</h4>" +
                "<br><h2 style=\"text-decoration: underline; font-weight:bold; margin-bottom:5px;\">" +
                "Rich Arnold Says Regarding This Task: </h2>" + "<h4 style=\"margin-top: 9px; margin-left: 5px;\">"
                + SimpleRespondBody + "</h4>"

                + "<br><br><label style=\"font-weight:bold; font-size: 20px;\">Link To Task:</label> " +
                   $"<a href=\"http://metrics.db.camco.local/viewrecurringtasks/OpenTask/{EmailTask.Id}\" target=\"_blank\"> "
                   + $" Recurring Task #{EmailTask.Id}</a>";

                    string Subject = "RICH ARNOLD SENT A RESPONSE";

                    body = EmailDefaults.GenerateEmailTemplate("Tasks", body);
                    await emailService.SendEmailAsync(EmailTypes.ActionBasedResponseForRecurringTask,
                        Array.Empty<string>(), Subject, body, string.Empty, new string[] { employee.Email });

                    _toastService.ShowInfo("An Email Has Been Added To Queue");

                    await jSRuntime.InvokeAsync<object>("HideModal", "#SimpleEmailModal");

                    if (EmailTask.EmailCount == null) EmailTask.EmailCount = 0;
                    EmailTask.EmailCount++;
                    await taskService.UpdateRecurringTask(EmailTask);

                    SimpleRespondBody = string.Empty;
                    EmailTask = new TasksRecTasksViewModel();
                }
                else _toastService.ShowWarning("Email is not found!");
            }
            else _toastService.ShowWarning("Employee is not found!");
        }

        protected async Task PrintingImage()
        {
            await jSRuntime.InvokeAsync<object>("PrintImage", ConvertImagetoBase64(TaskImage));
        }

        protected async Task PrintingImage(string FilePath)
        {
            await jSRuntime.InvokeAsync<object>("PrintImage", FilePath);
        }

        protected TasksImagesViewModel SelectedTMV { get; set; } = new TasksImagesViewModel();
        protected void StartDeleteTaskImage(TasksImagesViewModel TMV)
        {
            SelectedTMV = TMV;
        }

        protected async Task DeleteTaskImage()
        {
            SelectedTaskImages.Remove(SelectedTMV);
            await taskService.DeleteTaskImageAsync(SelectedTMV);
            if (!SelectedTaskImages.Any())
            {
                if (ImageUpdateModel != null)
                {
                    ImageUpdateModel.PictureLink = "";
                    await taskService.UpdateTaskUpdate(ImageUpdateModel);
                }
            }
        }

        protected async Task DeleteTaskFile()
        {
            SelectedTaskFiles.Remove(SelectedTMV);
            await taskService.DeleteTaskImageAsync(SelectedTMV);
            if (!SelectedTaskImages.Any())
            {
                if (ImageUpdateModel != null)
                {
                    ImageUpdateModel.PictureLink = "";
                    await taskService.UpdateTaskUpdate(ImageUpdateModel);
                }
            }
        }

        protected async Task ActivateTask(TasksRecTasksViewModel tasksRecTasks)
        {
            try
            {
                tasksRecTasks.IsDeactivated = false;
                await taskService.UpdateRecurringTask(tasksRecTasks);

                Tasks.Add(tasksRecTasks);
                Tasks = Tasks.OrderBy(a => a.UpcomingDate).ToList();
                RecTasksCount = Tasks.Count;

                int CompletedOne = 0, CompletedZero = 0;
                foreach (var IvTask in Tasks)
                {
                    if (!string.IsNullOrEmpty(IvTask.CompletedOnTime))
                    {
                        CompletedOne += IvTask.CompletedOnTime.Count(x => x == '1');
                        CompletedZero += IvTask.CompletedOnTime.Count(x => x == '0');
                    }
                }
                if (CompletedOne != 0 && CompletedZero != 0)
                    CompletedOnTimeCount = (CompletedOne * 100) / (CompletedOne + CompletedZero);
                else CompletedOnTimeCount = 0;

                int Index = DeactivatedTasks.FindIndex(x => x.Id == tasksRecTasks.Id);
                DeactivatedTasks.RemoveAt(Index);

                SetNewDefaults();

                RecurringTasksGrid.Refresh();
                await RecurringTasksGrid.RefreshColumns();

                if (DeactivatedGrid != null)
                {
                    DeactivatedGrid.Refresh();
                    await DeactivatedGrid.RefreshColumns();
                }

                ReAssignTaskseCount--;

                if (ReAssignTaskseCount == 0)
                {
                    await jSRuntime.InvokeAsync<object>("HideModal", "#ActivateTaskModal");
                }

                _toastService.ShowSuccess("Task Activated Successfully");
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    _toastService.ShowError(ex.InnerException.Message);
                }
                else
                {
                    _toastService.ShowError(ex.InnerException.Message);
                }
            }
        }

        protected async Task MakeSubTask(TasksRecTasksViewModel activeRecTasks)
        {
            try
            {
                if (activeRecTasks != null)
                {
                    navigationManager.NavigateTo($"/ViewRecSubTasks/{activeRecTasks.Id}");
                    MainTaskViewModel = activeRecTasks;
                    await jSRuntime.InvokeAsync<object>("HideModal", "#ActiveTaskModal");
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    _toastService.ShowError(ex.InnerException.Message);
                }
                else
                {
                    _toastService.ShowError(ex.InnerException.Message);
                }
            }
        }

        protected async Task AuditCheckedChanged(ChangeEventArgs args, TasksTaskUpdatesViewModel selectedTask)
        {
            while (!IsDoneAudit)
                await Task.Delay(25);

            IsDoneAudit = false;

            selectedTask.IsAudit = (bool)args.Value;
            await taskService.UpdateTaskUpdate(selectedTask);
            StateHasChanged();

            IsDoneAudit = true;
        }

        protected async Task FilterFields(string PersonResponsible)
        {
            await RecurringTasksGrid.ClearFiltering();
            await RecurringTasksGrid.FilterByColumn(nameof(TasksRecTasksViewModel.PersonResponsible),
                       "equal", PersonResponsible, "or");
        }

        protected async Task PassCheckedChanged(ChangeEventArgs args, TasksTaskUpdatesViewModel selectedTask)
        {
            while (!IsDonePass)
                await Task.Delay(25);

            IsDonePass = false;

            selectedTask.IsPass = (bool)args.Value;
            await taskService.UpdateTaskUpdate(selectedTask);
            StateHasChanged();

            IsDonePass = true;
        }

        protected bool IsUploading = false;
        protected void BeforeUploadImage()
        {
            IsUploading = true;
        }

        protected void SelectSampleImage(UploadChangeEventArgs args)
        {
            SampleuploadFiles = args.Files;
            IsUploading = false;
        }
        protected async Task RemoveSamplePreImage(BeforeRemoveEventArgs args)
        {
            SampleuploadFiles = new();
            await SampleUploadObj.ClearAllAsync();
        }

        protected void SelectTaskImages(UploadChangeEventArgs args)
        {
            TaskUploadFiles = args.Files;
            IsUploading = false;

            RecentTaskImagesAndFiles();
        }

        protected void RemoveTaskPreImage(BeforeRemoveEventArgs args)
        {
            if (args.FilesData[0] == null)
                return;

            var RemoveFile = TaskUploadFiles.FirstOrDefault(x => x.FileInfo?.Id == args.FilesData[0].Id);
            if (RemoveFile != null)
            {
                RemoveFile.FileInfo = null;
            }
            RecentTaskImagesAndFiles();
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

        protected async Task UseDeviceCamera()
        {
            CapturedImgaesList = new();
            await jSRuntime.InvokeVoidAsync("ready", this);
        }

        protected Dictionary<Guid, string> CapturedImgaesList = new();
        protected async Task Capture()
        {
            CapturedImgaesList.Add(Guid.NewGuid(), await jSRuntime.InvokeAsync<string>("take_snapshot"));
        }

        protected void RemoveCamImage(KeyValuePair<Guid, string> Kvp)
        {
            CapturedImgaesList.Remove(Kvp.Key);
        }

        protected void BeforeUploadTaskImage()
        {
            IsUploading = true;
        }

        protected async Task TaskPreViewImage()
        {
            IsRenderingImages = true;
            PreviewImages = new();
            PreviewImagesList = new();
            if (TaskUploadFiles.Count > 0)
            {
                await Task.Delay(1);
                foreach (var file in TaskUploadFiles)
                {
                    if (file.FileInfo != null)
                    {
                        if (IsImage(file.FileInfo.Name.ToLower()))
                        {
                            if (!IsValidSize(file.FileInfo.Size))
                            {
                                var size = Convert.ToDouble(file.FileInfo.Size / 1000000);
                                _toastService.ShowError(string.Format("Image size can not be more than 20 mb. Your uploaded image size is {0} mb.", size));
                                return;
                            }
                            byte[] byteImage = file.Stream.ToArray();
                            PreviewImages.Add(file.FileInfo.Id, $"data:image/png;base64, " + Convert.ToBase64String(byteImage));
                            PreviewImagesList.Add(file.FileInfo.Id, file.FileInfo);
                        }
                    }
                }
            }
            IsRenderingImages = false;
        }

        protected async Task TaskPreViewFile()
        {
            IsRenderingImages = true;
            PreviewFiles = new();
            PreviewFilesList = new();
            if (TaskUploadFiles.Count > 0)
            {
                await Task.Delay(1);
                foreach (var file in TaskUploadFiles)
                {
                    if (file.FileInfo != null)
                    {
                        if (IsFile(file.FileInfo.Name.ToLower()))
                        {
                            if (!IsValidSize(file.FileInfo.Size))
                            {
                                var size = Convert.ToDouble(file.FileInfo.Size / 1000000);
                                _toastService.ShowError(string.Format("Image size can not be more than 20 mb. Your uploaded image size is {0} mb.", size));
                                return;
                            }
                            byte[] byteFile = file.Stream.ToArray();
                            PreviewFiles.Add(file.FileInfo.Id, $"data:image/png;base64, " + Convert.ToBase64String(byteFile));
                            PreviewFilesList.Add(file.FileInfo.Id, file.FileInfo);
                        }
                    }
                }
            }
            IsRenderingImages = false;
        }
        protected void SelectUpdateImage(UploadChangeEventArgs args)
        {
            UploadFileUpdate = args.Files.FirstOrDefault();
            UpdateUploadFiles = args.Files;
            IsUploading = false;
        }

        protected void PreViewUpdateImage(UploadFiles? args)
        {

            byte[] byteImage = args.Stream.ToArray();
            TaskImage = $"data:image/png;base64, " + Convert.ToBase64String(byteImage);
        }

        protected string SelectedPreTMV { get; set; }
        protected void StartDeleteTaskPreImage(string TMV)
        {
            SelectedPreTMV = TMV;
        }

        protected async Task TaskPreImageRemove(string index)
        {
            var File = TaskUploadFiles[TaskUploadFiles.FindIndex(x => x.FileInfo?.Id == index)].FileInfo;
            var RemoveList = new FileInfo[1] { File };
            await TaskUploadRef.RemoveAsync(RemoveList);
            PreviewImages.Remove(index);
            PreviewImagesList.Remove(index);
            File = null;
        }

        protected async Task TaskPreFileRemove(string index)
        {
            var File = TaskUploadFiles[TaskUploadFiles.FindIndex(x => x.FileInfo?.Id == index)].FileInfo;
            var RemoveList = new FileInfo[1] { File };
            await TaskUploadRef.RemoveAsync(RemoveList);
            PreviewFiles.Remove(index);
            PreviewFilesList.Remove(index);
            File = null;
            SelectedPreTMV = null;
        }

        protected async Task UpdatePreImageRemove(string index)
        {
            var file = UpdateUploadFiles[TaskUploadFiles.FindIndex(x => x.FileInfo?.Id == index)].FileInfo;
            var removeList = new FileInfo[1] { file };
            await UPdateUploadObj.RemoveAsync(removeList);
            PreviewImages.Remove(index);
            PreviewImagesList.Remove(index);
            file = null;
        }

        protected async Task UpdatePreViewImage()
        {
            IsRenderingImages = true;
            PreviewImages = new();
            PreviewImagesList = new();
            if (UpdateUploadFiles.Count > 0)
            {
                await Task.Delay(1);
                foreach (var file in UpdateUploadFiles)
                {
                    if (file.FileInfo != null)
                    {
                        if (IsImage(file.FileInfo.Name.ToLower()))
                        {
                            if (!IsValidSize(file.FileInfo.Size))
                            {
                                var size = Convert.ToDouble(file.FileInfo.Size / 1000000);
                                _toastService.ShowError(string.Format("Image size can not be more than 20 mb. Your uploaded image size is {0} mb.", size));
                                return;
                            }
                            byte[] byteImage = file.Stream.ToArray();
                            PreviewImages.Add(file.FileInfo.Id, $"data:image/png;base64, " + Convert.ToBase64String(byteImage));
                            PreviewImagesList.Add(file.FileInfo.Id, file.FileInfo);
                        }
                    }
                }
            }
            IsRenderingImages = false;
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

        protected async Task StartTaskUpdate(TasksRecTasksViewModel taskViewModel, TasksTaskUpdatesViewModel taskUpdate)
        {
            AuditemployeeEmails.ForEach(x => x.IsSelected = false);
            SelectedUpdateTaskViewModel = taskViewModel;
            if (taskUpdate == null)
            {
                MinUpdateDate = DateTime.Today;
                UpdateTitle = "ADD";
                await StartUpdateAdd();
            }
            else
            {
                SelectedUpdateViewModel = taskUpdate;
                MinUpdateDate = taskUpdate.UpdateDate;
                UpdateTitle = "EDIT";
                await StartUpdateEdit();
            }
        }
        protected async Task StartUpdateAdd()
        {
            await Task.Delay(200);
            selectedPassAnswer = "NO";
            if (UPdateUploadObj != null)
                await UPdateUploadObj.ClearAll();

            IsTaskGraphRequired = SelectedUpdateTaskViewModel.IsGraphRequired;
            tempTaskId = SelectedUpdateTaskViewModel.Id;
            SelectedUpdateViewModel = new TasksTaskUpdatesViewModel() { UpdateDate = DateTime.Today, IsAudit = false, IsPass = false };
            SelectedUpdateViewModel.UpcomingDate = SelectedUpdateTaskViewModel.UpcomingDate;

            await jSRuntime.InvokeVoidAsync("ShowModal", "#RecTaskUpdate");
            await Task.Delay(200);

            if (SelectedUpdateTaskViewModel.IsPassOrFail == true)
            {
                await jSRuntime.InvokeVoidAsync("SetUpSelected", "no");
            }

            if (updateDescription != null)
            {
                await updateDescription.FocusIn();
            }
        }
        protected async Task StartUpdateEdit()
        {
            if (SelectedUpdateTaskViewModel.IsPassOrFail == true)
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

            SelectedUpdateViewModel.UpcomingDate = SelectedUpdateTaskViewModel.UpcomingDate;
            await jSRuntime.InvokeVoidAsync("ShowModal", "#RecTaskUpdate");
        }
        protected async Task TrySaveUpdate()
        {
            if (SelectedUpdateViewModel.UpdateId == 0)
                await AddNewUpdate();
            else
                await EditUpdate();
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
            if (string.IsNullOrEmpty(SelectedUpdateViewModel.Update))
            {
                await jSRuntime.InvokeVoidAsync("AddRedBox", "RecUpdDesc");
            }
            else
            {
                await jSRuntime.InvokeVoidAsync("RemoveRedBox", "RecUpdDesc");
            }
        }

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
        protected async Task AddNewUpdate()
        {
            try
            {
                bool isValid = true;
                if (SelectedUpdateViewModel.UpdateDate < DateTime.Today)
                {
                    //_toastService.ShowError("Please Select Valid Date");
                    await jSRuntime.InvokeVoidAsync("AddRedBox", "RectaskUpdDate");
                    isValid = false;
                }

                else
                {
                    await jSRuntime.InvokeVoidAsync("RemoveRedBox", "RectaskUpdDate");
                }

                if (string.IsNullOrEmpty(SelectedUpdateViewModel.Update))
                {
                    await jSRuntime.InvokeVoidAsync("AddRedBox", "RecUpdDesc");
                    isValid = false;
                    //_toastService.ShowError("Please Enter Update Description");
                }
                else
                {
                    await jSRuntime.InvokeVoidAsync("RemoveRedBox", "RecUpdDesc");
                }

                if (SelectedUpdateTaskViewModel.IsGraphRequired == true)
                {
                    if (SelectedUpdateViewModel.GraphNumber < 0)
                    {
                        await jSRuntime.InvokeVoidAsync("AddRedBox", "RecUpdGraphNo");
                        isValid = false;
                    }
                    else
                    {
                        SelectedUpdateViewModel.GraphNumber = SelectedUpdateViewModel.GraphNumber;
                        await jSRuntime.InvokeVoidAsync("RemoveRedBox", "RecUpdGraphNo");
                    }
                    SelectedUpdateTaskViewModel.LatestGraphValue = SelectedUpdateViewModel.GraphNumber;
                }

                if (SelectedUpdateTaskViewModel.IsPassOrFail == true)
                {
                    if (selectedPassAnswer == "NA")
                    {
                        if (string.IsNullOrEmpty(selectedPassAnswer))
                        {
                            await jSRuntime.InvokeVoidAsync("AddRedBox", "RecUpdFail");
                            isValid = false;
                        }
                        else
                        {
                            await jSRuntime.InvokeVoidAsync("RemoveRedBox", "RecUpdFail");
                        }
                    }
                }

                if (SelectedUpdateTaskViewModel.IsQuestionRequired == false)
                    SelectedUpdateTaskViewModel.Question = "";

                if (SelectedUpdateTaskViewModel.IsQuestionRequired == true)
                {
                    if (string.IsNullOrEmpty(SelectedUpdateViewModel.QuestionAnswer))
                    {
                        await jSRuntime.InvokeVoidAsync("AddRedBox", "RecupdQuestion");
                        isValid = false;
                    }
                    else
                    {
                        await jSRuntime.InvokeVoidAsync("RemoveRedBox", "RecupdQuestion");
                    }
                }

                if (string.IsNullOrEmpty(SelectedUpdateViewModel.FailReason) && SelectedUpdateViewModel.IsPass == false
                    && SelectedUpdateTaskViewModel.IsPassOrFail == true)
                {
                    await jSRuntime.InvokeVoidAsync("AddRedBox", "RecUpdFail");
                    isValid = false;
                    return;
                }

                if (!isValid)
                {
                    _toastService.ShowError("Please Fill All Required Field..!");
                    return;
                }

                List<TasksImagesViewModel> LoadedFiles = new List<TasksImagesViewModel>();
                if (SelectedUpdateTaskViewModel.IsPicRequired == true)
                {
                    var file = UploadFileUpdate;

                    if (file.FileInfo != null)
                    {
                        if (IsImage(file.FileInfo.Name.ToLower()))
                        {
                            if (!IsValidSize(file.FileInfo.Size))
                            {
                                var size = Convert.ToDouble(file.FileInfo.Size / 1000000);
                                _toastService.ShowError(string.Format("Image size can not be more than 20 mb. Your uploaded image size is {0} mb.", size));
                                return;
                            }

                            var ImageFileName = $"{Guid.NewGuid()}.{file.FileInfo.Type}";
                            var ResultPath = fileManagerService.CreateRecurringTaskDirectory(tempTaskId.ToString()) + ImageFileName;
                            fileManagerService.WriteToFile(file.Stream, ResultPath);
                            SelectedUpdateViewModel.PictureLink = ResultPath;

                            TasksImagesViewModel TM = new TasksImagesViewModel()
                            {
                                UpdateId = SelectedUpdateViewModel.UpdateId,
                                PictureLink = ResultPath,
                                IsDeleted = false,
                                OneTimeId = 0,
                                RecurringId = 0,
                            };
                            LoadedFiles.Add(TM);
                            //await taskService.InsertTaskImageAsync(TM);
                        }
                        else if (IsFile(file.FileInfo.Name.ToLower()))
                        {
                            if (!IsValidSize(file.FileInfo.Size))
                            {
                                var size = Convert.ToDouble(file.FileInfo.Size / 1000000);
                                _toastService.ShowError(string.Format("Image size can not be more than 20 mb. Your uploaded File size is {0} mb.", size));
                                return;
                            }

                            var ImageFileName = $"{Guid.NewGuid()}.{file.FileInfo.Type}";
                            var ResultPath = fileManagerService.CreateRecurringTaskDirectory(tempTaskId.ToString()) + ImageFileName;
                            fileManagerService.WriteToFile(file.Stream, ResultPath);
                            SelectedUpdateViewModel.FileLink = ResultPath;

                            TasksImagesViewModel TM = new TasksImagesViewModel()
                            {
                                UpdateId = SelectedUpdateViewModel.UpdateId,
                                FileName = ResultPath,
                                IsDeleted = false,
                                OneTimeId = 0,
                                RecurringId = 0,
                            };
                            LoadedFiles.Add(TM);
                            //await taskService.InsertTaskImageAsync(TM);
                        }
                    }

                    if (CapturedImgaesList.Any())
                    {
                        foreach (var Img in CapturedImgaesList)
                        {
                            var Base64Image = Img.Value.Replace("data:image/jpeg;base64,", "");
                            byte[] ImgBytes = Convert.FromBase64String(Base64Image);

                            var Imgstream = new MemoryStream(ImgBytes);

                            var ImageFileName = $"{Guid.NewGuid()}.jpeg";
                            var ResultPath = fileManagerService.CreateRecurringTaskDirectory(tempTaskId.ToString()) + ImageFileName;
                            fileManagerService.WriteToFile(Imgstream, ResultPath);
                            SelectedUpdateViewModel.PictureLink = ResultPath;

                            TasksImagesViewModel TM = new TasksImagesViewModel()
                            {
                                UpdateId = SelectedUpdateViewModel.UpdateId,
                                PictureLink = ResultPath,
                                IsDeleted = false,
                                OneTimeId = 0,
                                RecurringId = 0,
                            };
                            LoadedFiles.Add(TM);
                        }
                    }
                    CapturedImgaesList = new();

                    await UPdateUploadObj.ClearAllAsync();
                    UploadFileUpdate = new UploadFiles(); ;
                }

                SelectedUpdateViewModel.RecurringID = tempTaskId;
                SelectedUpdateViewModel.IsDeleted = false;
                SelectedUpdateViewModel.TaskID = null;

                if (SelectedUpdateTaskViewModel.IsGraphRequired == true)
                {
                    var UpdateLookUp = SelectedUpdateTaskViewModel.TasksTaskUpdates.FirstOrDefault(x => x.UpdateDate.Date == SelectedUpdateViewModel.UpdateDate.Date);

                    if (UpdateLookUp != null)
                    {
                        _toastService.ShowError("You Can't add 2 Graph Points for same Date of Update, please Edit the update if needed");
                        return;
                    }
                }

                SelectedUpdateTaskViewModel.DateCompleted = DateTime.Now;

                if (SelectedUpdateTaskViewModel.UpcomingDate?.Date >= SelectedUpdateTaskViewModel.DateCompleted?.Date)
                    SelectedUpdateViewModel.TaskCompleted = true;
                else
                    SelectedUpdateViewModel.TaskCompleted = false;

                SelectedUpdateViewModel.IsPass = selectedPassAnswer == "YES" ? true : false;

                SelectedUpdateViewModel.FailedAuditList = "";
                if (SelectedUpdateTaskViewModel.IsAuditRequired == true)
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

                SelectedUpdateViewModel.UpdateId = await taskService.AddTaskUpdate(SelectedUpdateViewModel);

                foreach (var LoadedFile in LoadedFiles)
                {
                    LoadedFile.UpdateId = SelectedUpdateViewModel.UpdateId;
                    await taskService.InsertTaskImageAsync(LoadedFile);
                }

                if (SelectedUpdateTaskViewModel.IsGraphRequired == true)
                {
                    await ExportAndMailGraph();
                }
                else
                {
                    await MarkCompleted();
                }

                SelectedUpdateTaskViewModel.TasksTaskUpdates.Add(SelectedUpdateViewModel);

                SelectedUpdateTaskViewModel.TasksTaskUpdates = SelectedUpdateTaskViewModel.TasksTaskUpdates.OrderByDescending(
                    x => x.UpdateDate.Date).ToList();

                UploadFileUpdate = new();
                IsTaskDone = false;

                if (RecUpdateGrid != null)
                {
                    RecUpdateGrid.Refresh();
                    await RecUpdateGrid.RefreshColumns();
                }
                if (UploadObj != null)
                {
                    await UploadObj.ClearAll();
                }

                await jSRuntime.InvokeAsync<object>("HideModal", "#RecTaskUpdate");
                SelectedUpdateViewModel = new TasksTaskUpdatesViewModel() { UpdateDate = DateTime.Today, IsAudit = false, IsPass = false };

                _toastService.ShowSuccess("Update Has Been Added!");

                UploadFileUpdate = new UploadFiles();
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    _toastService.ShowError(ex.InnerException.Message);
                }
                else
                {
                    _toastService.ShowError(ex.Message);
                }
                await jSRuntime.InvokeAsync<object>("HideModal", "#RecTaskUpdate");
            }

            employeeEmails.ForEach(x => x.IsSelected = true);
            FailedemployeeEmails.ForEach(x => x.IsSelected = true);

            UploadFileUpdate = new();
            SelectedUpdateViewModel = new TasksTaskUpdatesViewModel() { UpdateDate = DateTime.Today, IsAudit = false, IsPass = false };
        }
        protected async Task EditUpdate()
        {
            if (SelectedUpdateTaskViewModel.UpcomingDate.HasValue && SelectedUpdateTaskViewModel.UpcomingDate < DateTime.Now)
            {
                _toastService.ShowError(string.Format("You Can not Update this task, Due date End"));
                return;
            }

            if (SelectedUpdateTaskViewModel.IsPicRequired == true)
            {
                var file = UploadFileUpdate;

                if (file.FileInfo != null)
                {
                    if (IsImage(file.FileInfo.Name.ToLower()))
                    {
                        if (!IsValidSize(file.FileInfo.Size))
                        {
                            var size = Convert.ToDouble(file.FileInfo.Size / 1000000);
                            _toastService.ShowError(string.Format("Image size can not be more than 20 mb. Your uploaded image size is {0} mb.", size));
                            return;
                        }

                        var ImageFileName = $"{Guid.NewGuid()}.{file.FileInfo.Type}";
                        var ResultPath = fileManagerService.CreateRecurringTaskDirectory(tempTaskId.ToString()) + ImageFileName;
                        fileManagerService.WriteToFile(file.Stream, ResultPath);
                        SelectedUpdateViewModel.PictureLink = ResultPath;

                        TasksImagesViewModel TM = new TasksImagesViewModel()
                        {
                            PictureLink = ResultPath,
                            RecurringId = tempTaskId,
                            IsDeleted = false,
                            OneTimeId = 0
                        };
                        await taskService.InsertTaskImageAsync(TM);
                    }
                    else if (IsFile(file.FileInfo.Name.ToLower()))
                    {
                        if (!IsValidSize(file.FileInfo.Size))
                        {
                            var size = Convert.ToDouble(file.FileInfo.Size / 1000000);
                            _toastService.ShowError(string.Format("Image size can not be more than 20 mb. Your uploaded image size is {0} mb.", size));
                            return;
                        }

                        var ImageFileName = $"{Guid.NewGuid()}.{file.FileInfo.Type}";
                        var ResultPath = fileManagerService.CreateRecurringTaskDirectory(tempTaskId.ToString()) + ImageFileName;
                        fileManagerService.WriteToFile(file.Stream, ResultPath);
                        SelectedUpdateViewModel.FileLink = ResultPath;

                        TasksImagesViewModel TM = new TasksImagesViewModel()
                        {
                            PictureLink = ResultPath,
                            RecurringId = tempTaskId,
                            IsDeleted = false,
                            OneTimeId = 0
                        };
                        await taskService.InsertTaskImageAsync(TM);
                    }
                }
                else
                {
                    _toastService.ShowError("Please Upload an Image.");
                    return;
                }
            }

            if (string.IsNullOrEmpty(SelectedUpdateViewModel.Update))
            {
                _toastService.ShowError("Please Enter Update Description");

                return;
            }

            if (SelectedUpdateTaskViewModel.IsQuestionRequired == true && string.IsNullOrEmpty(SelectedUpdateViewModel.QuestionAnswer))
            {
                _toastService.ShowError("Please Write Question's Answer..!");
                return;
            }

            if (SelectedUpdateTaskViewModel.IsPassOrFail == true)
            {
                if (string.IsNullOrEmpty(selectedPassAnswer) || selectedPassAnswer == "NA")
                {
                    _toastService.ShowError("Please Select whether it's Passed or Not");
                    return;
                }
            }

            if ((string.IsNullOrEmpty(SelectedUpdateViewModel.FailReason) && SelectedUpdateViewModel.IsPass == false) && SelectedUpdateTaskViewModel.IsPassOrFail == true)
            {
                _toastService.ShowError("Fail Reason Can't Be Empty..!");
                return;
            }

            if (SelectedUpdateViewModel.IsPass == true)
                SelectedUpdateViewModel.FailReason = "";

            if (SelectedUpdateTaskViewModel.IsGraphRequired == true)
            {
                if (SelectedUpdateViewModel.GraphNumber < 0)
                {
                    _toastService.ShowError("Please Enter Update Graph Number");
                    return;
                }
            }

            SelectedUpdateViewModel.IsPass = selectedPassAnswer == "NO" ? false : true;
            SelectedUpdateViewModel.FailedAuditList = "";
            if (SelectedUpdateTaskViewModel.IsAuditRequired == true)
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

            if (SelectedUpdateTaskViewModel.IsGraphRequired == true)
            {
                var LatestUpdates = (await taskService.GetTaskUpdates1(SelectedUpdateTaskViewModel.Id)).ToList();
                if (LatestUpdates.Any())
                {
                    LatestUpdates = LatestUpdates.OrderByDescending(x => x.UpdateDate).ToList();
                    SelectedUpdateTaskViewModel.LatestGraphValue = LatestUpdates[0].GraphNumber;
                    await taskService.UpdateRecurringTask(SelectedUpdateTaskViewModel);
                }
            }

            if (result)
            {
                await SendEmailForEditTaskUpdate(SelectedUpdateViewModel);
            }

            await UPdateUploadObj.ClearAllAsync();
            UploadFileUpdate = new();
            if (RecUpdateGrid != null)
            {
                RecUpdateGrid.Refresh();
                await RecUpdateGrid.RefreshColumns();
            }

            RecurringTasksGrid.Refresh();
            await RecurringTasksGrid.RefreshColumnsAsync();

            await jSRuntime.InvokeAsync<object>("HideModal", "#RecTaskUpdate");
            SelectedUpdateViewModel = new TasksTaskUpdatesViewModel() { UpdateDate = DateTime.Today, IsAudit = false, IsPass = false };
        }

        string selectedPassAnswer = "NO";
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

        protected void SetViewModelGraph(TasksRecTasksViewModel model, bool IsRequired)
        {
            model.IsGraphRequired = IsRequired;
            if (IsRequired)
                model.IsPassOrFail = false;
        }

        protected void SetQuestionViewModel(TasksRecTasksViewModel model, bool IsRequired)
        {
            model.IsQuestionRequired = IsRequired;
        }
        protected void SetImageViewModel(TasksRecTasksViewModel model, bool IsRequired)
        {
            model.IsPicRequired = IsRequired;
        }

        protected void SetUpdteImageType(int Type)
        {
            SelectedTaskViewModel.UpdateImageType = Type;
        }

        protected void SetViewModelPassOrFail(TasksRecTasksViewModel model, bool IsRequired)
        {
            model.IsPassOrFail = IsRequired;
            if (IsRequired)
            {
                SelectedTaskViewModel.IsGraphRequired = !IsRequired;
                FailedemployeeEmails = FailedemployeeEmails.Select(x => new EmployeeEmail
                {
                    FullName = x.FullName,
                    Email = x.Email,
                    IsSelected = false
                }).OrderBy(x => x.FullName).ToList();

                if (!string.IsNullOrEmpty(model.FailedEmailsList))
                {
                    string[] splitEmails = model.FailedEmailsList.Split(';');
                    if (splitEmails.Any())
                    {
                        foreach (var email in FailedemployeeEmails)
                        {
                            if (splitEmails.Contains(email.Email))
                                email.IsSelected = true;
                        }
                    }
                }
            }
        }

        protected async Task CheckNoteDate()
        {
            if (updateNotesViewModel.NoteDate < DateTime.Today)
            {
                await jSRuntime.InvokeVoidAsync("AddRedBox", "NoteDate");
            }
            else
            {
                await jSRuntime.InvokeVoidAsync("RemoveRedBox", "NoteDate");
            }
        }

        protected async Task ChecNotes()
        {
            if (string.IsNullOrEmpty(updateNotesViewModel.Notes))
            {
                await jSRuntime.InvokeVoidAsync("AddRedBox", "Notes");
            }
            else
            {
                await jSRuntime.InvokeVoidAsync("RemoveRedBox", "Notes");
            }
        }
        public async Task AddNewUpdateNote()
        {
            try
            {
                bool isValid = true;
                if (updateNotesViewModel.NoteDate < DateTime.Today)
                {
                    //_toastService.ShowError("Please Select Valid Date");
                    await jSRuntime.InvokeVoidAsync("AddRedBox", "NoteDate");
                    isValid = false;
                }

                if (string.IsNullOrEmpty(updateNotesViewModel.Notes))
                {
                    //_toastService.ShowError("Please Enter a Note");
                    await jSRuntime.InvokeVoidAsync("AddRedBox", "Notes");
                    isValid = false;
                }

                if (!isValid)
                {
                    _toastService.ShowError("Please Enter All Required Field");
                    return;
                }

                updateNotesViewModel.UpdateID = NoteUpdateEditDelete.UpdateId;
                updateNotesViewModel.ID = await notesService.Insert(updateNotesViewModel);

                UploadFileUpdate = new();
                IsTaskDone = false;

                if (NotesGrid != null)
                {
                    NoteUpdateEditDelete.Notes.Add(updateNotesViewModel);
                    NotesGrid.Refresh();
                    await NotesGrid.RefreshColumns();
                }

                await jSRuntime.InvokeAsync<object>("HideModal", "#AddUpdateNote");

                updateNotesViewModel = new UpdateNotesViewModel() { NoteDate = DateTime.Today };

                _toastService.ShowSuccess("Note Has Been Added!");
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    _toastService.ShowError(ex.InnerException.Message);
                }
                else
                {
                    _toastService.ShowError(ex.Message);
                }
                await jSRuntime.InvokeAsync<object>("HideModal", "#AddTaskUpdate");
            }
        }

        public async Task EditUpdateNote()
        {
            try
            {
                if (NoteEditDelete.NoteDate < DateTime.Today)
                {
                    _toastService.ShowError("Please Select Valid Date");
                    return;
                }

                if (string.IsNullOrEmpty(NoteEditDelete.Notes))
                {
                    _toastService.ShowError("Please Enter a Note");
                    return;
                }

                await notesService.Update(NoteEditDelete);

                UploadFileUpdate = new();
                IsTaskDone = false;

                if (NotesGrid != null)
                {
                    NotesGrid.Refresh();
                    await NotesGrid.RefreshColumns();
                }

                await jSRuntime.InvokeAsync<object>("HideModal", "#EditUpdateNote");

                NoteEditDelete = new UpdateNotesViewModel() { NoteDate = DateTime.Today };

                _toastService.ShowSuccess("Note Has Been Modified!");
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    _toastService.ShowError(ex.InnerException.Message);
                }
                else
                {
                    _toastService.ShowError(ex.Message);
                }
                await jSRuntime.InvokeAsync<object>("HideModal", "#EditUpdateNote");
            }
        }

        public async Task DeleteUpdateNote()
        {
            try
            {
                NoteEditDelete.IsDeleted = true;
                await notesService.Update(NoteEditDelete);

                if (NotesGrid != null)
                {
                    NotesGrid.Refresh();
                    await NotesGrid.RefreshColumns();
                }

                await jSRuntime.InvokeAsync<object>("HideModal", "#DeleteUpdateNote");

                NoteEditDelete = new UpdateNotesViewModel() { NoteDate = DateTime.Today };

                var updatesModel = await notesService.GetAll(NoteUpdateEditDelete.UpdateId);

                NoteUpdateEditDelete.Notes = updatesModel.ToList();

                if (NotesGrid != null)
                {
                    NotesGrid.Refresh();
                    await NotesGrid.RefreshColumns();
                }
                _toastService.ShowSuccess("Note Has Been Deleted!");
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    _toastService.ShowError(ex.InnerException.Message);
                }
                else
                {
                    _toastService.ShowError(ex.Message);
                }
                await jSRuntime.InvokeAsync<object>("HideModal", "#DeleteUpdateNote");
            }
            // SetNewDefaults();
        }

        public bool IsImage(string file) => Regex.IsMatch(file, IMAGEFORMATS);
        public bool IsFile(string file) => Regex.IsMatch(file, FILEFORMATS);
        public bool IsValidSize(double length) => length / 1000000 <= 20;

        protected async Task ChangeFrequency(ChangeEventArgs<string, string> args)
        {
            if (!args.IsInteracted)
                return;

            var frequency = await taskService.GetFrequency(args.Value);
            if (frequency != null)
            {
                if (SelectedTaskViewModel.DateCompleted.HasValue)
                {
                    var Date = SelectedTaskViewModel.DateCompleted.Value.AddDays((double)frequency.Days);
                    var DateName = Date.DayOfWeek.ToString();
                    Date = DateName.Contains("Sat") ? Date.AddDays(2) : (DateName.Contains("Sun") ? Date.AddDays(1) : Date);

                    SelectedTaskViewModel.UpcomingDate = Date;
                }
                else if (SelectedTaskViewModel.StartDate.HasValue)
                {
                    var Date = SelectedTaskViewModel.StartDate.Value.AddDays((double)frequency.Days);
                    var DateName = Date.DayOfWeek.ToString();
                    Date = DateName.Contains("Sat") ? Date.AddDays(2) : (DateName.Contains("Sun") ? Date.AddDays(1) : Date);

                    SelectedTaskViewModel.UpcomingDate = Date;
                }
                await jSRuntime.InvokeVoidAsync("RemoveRedBox", "Intiator");
            }
            else
            {
                await jSRuntime.InvokeVoidAsync("AddRedBox", "Intiator");
            }
        }

        protected async Task ChangeFrequencyDate(Syncfusion.Blazor.Calendars.ChangedEventArgs<DateTime?> args, bool IsEdit)
        {
            if (!args.IsInteracted)
                return;

            var frequency = await taskService.GetFrequency(SelectedTaskViewModel.Frequency);
            if (IsEdit)
            {
                if (SelectedTaskViewModel.StartDate.HasValue)
                {
                    if (frequency != null)
                        if (SelectedTaskViewModel.StartDate.HasValue)
                        {
                            var Date = SelectedTaskViewModel.StartDate.Value.AddDays((double)frequency.Days);
                            var DateName = Date.DayOfWeek.ToString();
                            Date = DateName.Contains("Sat") ? Date.AddDays(2) : (DateName.Contains("Sun") ? Date.AddDays(1) : Date);

                            SelectedTaskViewModel.UpcomingDate = Date;
                        }
                }
            }
            else
            {
                if (SelectedTaskViewModel.StartDate.HasValue)
                {
                    if (frequency != null)
                        if (SelectedTaskViewModel.StartDate.HasValue)
                        {
                            var Date = SelectedTaskViewModel.StartDate.Value.AddDays((double)frequency.Days);
                            var DateName = Date.DayOfWeek.ToString();
                            Date = DateName.Contains("Sat") ? Date.AddDays(2) : (DateName.Contains("Sun") ? Date.AddDays(1) : Date);

                            SelectedTaskViewModel.UpcomingDate = Date;
                        }
                }
            }
        }

        protected async Task ClearImage(TasksRecTasksViewModel recTask)
        {
            recTask.PictureLink = string.Empty;
            await taskService.UpdateRecurringTask(recTask);
            _toastService.ShowSuccess("Image Cleared");
        }

        protected void MarkCompleteCheck(ChangeEventArgs args, EmployeeEmail e) => e.IsSelected = (bool)args.Value;
        protected void MarkCompleteCheck(ChangeEventArgs args, TasksRecTasksViewModel e) => e.IsDeactivated = (bool)args.Value;
        protected void MarkFailedCheck(ChangeEventArgs args, EmployeeEmail e) => e.IsSelected = (bool)args.Value;

        protected bool IsRenderingImages = false;
        protected async Task LoadTaskImages(TasksRecTasksViewModel TaskModel)
        {
            ImageUpdateModel = null;
            IsRenderingImages = true;
            SelectedTaskViewModel = TaskModel;
            SelectedTaskImages = (await taskService.GetRecurringTaskImagesAsync(TaskModel.Id)).ToList();
            this.SelectedTaskImages = SelectedTaskImages.Where(i => IsImage(i.PictureLink)).ToList();
            IsRenderingImages = false;
        }

        TasksTaskUpdatesViewModel ImageUpdateModel { get; set; } = null;
        protected async Task LoadUpdateImages(TasksTaskUpdatesViewModel UpdateModel)
        {
            ImageUpdateModel = UpdateModel;
            SelectedTaskImages = new();
            IsRenderingImages = true;
            SelectedTaskImages = (await taskService.GetUpdatesImagesAsync(UpdateModel.UpdateId)).ToList();
            if (SelectedTaskImages.Count() == 0)
            {
                if (!string.IsNullOrEmpty(UpdateModel.PictureLink))
                {
                    TasksImagesViewModel TM = new()
                    {
                        PictureLink = UpdateModel.PictureLink,
                        OneTimeId = 0,
                        UpdateId = UpdateModel.UpdateId,
                        FileName = string.Empty,
                        ImageNote = string.Empty,
                        IsDeleted = false,
                        RecurringId = 0
                    };
                    TM.Id = await taskService.InsertTaskImageAsync(TM);
                    SelectedTaskImages.Add(TM);
                }
            }
            IsRenderingImages = false;
        }

        protected void LoadTaskImages(List<TasksImagesViewModel> ImageModels)
        {
            IsRenderingImages = true;

            SelectedTaskImages = ImageModels.ToList();

            IsRenderingImages = false;
        }

        protected SfTextBox ImageNoteBox { get; set; }
        protected TasksImagesViewModel SelectedImageNote { get; set; } = new TasksImagesViewModel();
        protected void StartModifyingImageNote(TasksImagesViewModel model)
        {
            SelectedImageNote = model;
        }

        protected async Task ModifyImageNote()
        {
            await taskService.UpdateTaskImageAsync(SelectedImageNote);
            _toastService.ShowSuccess("Successfully Saved");

            await jSRuntime.InvokeAsync<object>("HideModal", "#ImageNoteModal");
        }

        protected bool IsRenderingFiles = false;
        protected async Task LoadTaskFiles(TasksRecTasksViewModel TaskModel)
        {
            IsRenderingFiles = true;
            SelectedTaskViewModel = TaskModel;
            var SelectedTaskFiles = (await taskService.GetRecurringTaskImagesAsync(TaskModel.Id)).ToList();
            this.SelectedTaskFiles = SelectedTaskFiles.Where(x => IsFile(x.PictureLink)).ToList();
            foreach (var file in SelectedTaskFiles)
            {
                file.FileName = Path.GetFileName(file.PictureLink);
            }
            IsRenderingFiles = false;
        }

        protected async Task RefreshNoteModal(TasksTaskUpdatesViewModel Update)
        {
            NoteUpdateEditDelete = Update;
            updateNotesViewModel = new UpdateNotesViewModel() { NoteDate = DateTime.Today };
            await jSRuntime.InvokeAsync<object>("ShowModal", "#AddUpdateNote");
        }

        protected void ViewImage(TasksRecTasksViewModel Model)
        {
            if (Model != null)
                TaskImage = ConvertImagetoBase64(Model.PictureLink);
        }

        protected void ViewFile(TasksRecTasksViewModel model)
        {
            if (model != null) TaskFile = model.InstructionFileLink;
        }

        protected void ViewImage(string ImageLink, bool Converting)
        {
            TaskImage = Converting ? ConvertImagetoBase64(ImageLink) : ImageLink;
        }

        protected void ViewImage(TasksImagesViewModel Model)
        {
            if (Model != null)
                TaskImage = ConvertImagetoBase64(Model.PictureLink);
        }

        protected bool OpenChart = false;
        protected async Task ViewTaskGraph()
        {
            SelectedTaskViewModel.GraphTitle = string.IsNullOrEmpty(SelectedTaskViewModel.GraphTitle) ? "No Title" :
                SelectedTaskViewModel.GraphTitle.ToUpper();

            SelectedTaskViewModel.VerticalAxisTitle = string.IsNullOrEmpty(SelectedTaskViewModel.VerticalAxisTitle) ? "No Title" :
                SelectedTaskViewModel.VerticalAxisTitle.ToUpper();

            OpenChart = true;

            await Task.Delay(15);

            GraphUpdates = new();
            var tempUpdates = SelectedTaskViewModel.TasksTaskUpdates.Where(x => x.GraphNumber >= 0);
            if (tempUpdates != null)
                GraphUpdates = tempUpdates.OrderBy(x => x.UpdateDate.Date).ToList();

            foreach (var GU in GraphUpdates)
            {
                GU.GraphDate = GU.UpdateDate.ToString("MM/dd/yyyy");
            }

            await ChartObj.RefreshAsync();
            await jSRuntime.InvokeAsync<object>("ShowModal", "#GraphModel");

            StateHasChanged();
        }

        protected void HideGraph()
        {
            OpenChartTemp = false;
        }
        protected async Task ViewTempGraph()
        {
            SelectedTaskViewModel.GraphTitle = string.IsNullOrEmpty(SelectedTaskViewModel.GraphTitle) ? "No Title" :
                SelectedTaskViewModel.GraphTitle.ToUpper();

            SelectedTaskViewModel.VerticalAxisTitle = string.IsNullOrEmpty(SelectedTaskViewModel.VerticalAxisTitle) ? "No Title" :
                SelectedTaskViewModel.VerticalAxisTitle.ToUpper();

            OpenChartTemp = true;
            
            await Task.Delay(15);
            StateHasChanged();

            GraphUpdatesTemp = new();
            var tempUpdates = SelectedTaskViewModel.TasksTaskUpdates.Where(x => x.GraphNumber >= 0);
            if (tempUpdates != null)
                GraphUpdatesTemp = tempUpdates.OrderBy(x => x.UpdateDate.Date).ToList();

            foreach (var GU in GraphUpdatesTemp)
            {
                GU.GraphDate = GU.UpdateDate.ToString("MM/dd/yyyy");
            }

            await ChartTempObj.RefreshAsync();
        }

        protected void ViewImage(TasksTaskUpdatesViewModel Model)
        {
            if (Model != null)
                TaskImage = ConvertImagetoBase64(Model.PictureLink);
        }

        protected async Task PrintChart() => await ChartObj.PrintAsync();


        protected async Task DeleteUpdateConfirm(TasksRecTasksViewModel recTask, TasksTaskUpdatesViewModel tasksTaskUpdates)
        {
            SelectedTaskViewModel = recTask;
            DeleteUpdateViewModel = tasksTaskUpdates;
            await jSRuntime.InvokeAsync<object>("ShowModal", "#deleteupdateModal");
        }

        protected async Task StartEditNote(TasksRecTasksViewModel viewmodel, UpdateNotesViewModel UpdateNote)
        {
            NoteEditDelete = UpdateNote;
            await Task.Delay(1);
        }

        protected void StartDeleteNote(TasksTaskUpdatesViewModel update, UpdateNotesViewModel UpdateNote)
        {
            NoteUpdateEditDelete = update;
            NoteEditDelete = UpdateNote;
        }

        protected async Task DeleteUpdate()
        {
            try
            {
                SelectedTaskViewModel.TasksTaskUpdates.Remove(DeleteUpdateViewModel);

                DeleteUpdateViewModel.IsDeleted = true;
                var result = await taskService.UpdateTaskUpdate(DeleteUpdateViewModel);

                if (RecUpdateGrid != null)
                {
                    RecUpdateGrid.Refresh();
                    await RecUpdateGrid.RefreshColumns();
                }

                DeleteUpdateViewModel = new TasksTaskUpdatesViewModel()
                { UpdateDate = DateTime.Today, IsAudit = false, IsPass = false };

                _toastService.ShowSuccess("Update Has Been Deleted!");
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    _toastService.ShowError(ex.InnerException.Message);
                }
                else
                {
                    _toastService.ShowError(ex.Message);
                }
            }
        }

        protected string ConvertImagetoBase64(string path)
        {
            if (string.IsNullOrEmpty(path))
                return string.Empty;
            try
            {
                using (var webClient = new WebClient())
                {
                    byte[] imageBytes = webClient.DownloadData(path);
                    return $"data:image/{Path.GetExtension(path).Replace(".", "").ToLower()};base64, " +
                Convert.ToBase64String(imageBytes);
                }
            }
            catch
            {
                return string.Empty;
            }
        }

        protected void ReturnTo5STasks()
        {
            navigationManager.NavigateTo($"http://5s.db.camco.local/fetchEmployeeTasks/{SelectedEmpId}");
        }

        protected void SetNewDefaults()
        {
            try
            {
                employeeEmails.ForEach(x => x.IsSelected = true);
                FailedemployeeEmails.ForEach(x => x.IsSelected = true);

                UploadFileUpdate = new();
                SelectedUpdateViewModel = new TasksTaskUpdatesViewModel() { UpdateDate = DateTime.Today, IsAudit = false, IsPass = false };

                SelectedTaskViewModel = new TasksRecTasksViewModel
                {
                    Initiator = "ARNOLD, RICHARD",
                    Frequency = "Yearly",
                    DateCreated = DateTime.Now,
                    IsPassOrFail = false,
                    IsQuestionRequired = false,
                    IsGraphRequired = false,
                    IsPicRequired = false,
                    IsDeactivated = false,
                    IsDeleted = false,
                    IsTrendLine = false,
                };
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task ViewTaskUpdates(TasksRecTasksViewModel args)
        {
            IsTaskGraphRequired = args.IsGraphRequired;

            TasksCompleted = new List<string>();
            var updatesModel = await taskService.GetTaskUpdates(args.Id);

            SelectedTaskViewModel = args;
            SelectedTaskViewModel.TasksTaskUpdates = updatesModel.OrderByDescending(x => x.UpdateDate.Date).ToList();
            var TempTaskImages = await taskService.GetRecurringTaskImagesCountAsync(SelectedTaskViewModel.Id);
            if (TempTaskImages != null)
            {
                SelectedTaskViewModel.ImagesCount = TempTaskImages.Where(x => IsImage(x.PictureLink)).Count();
                SelectedTaskViewModel.FilesCount = TempTaskImages.Where(x => IsFile(x.PictureLink)).Count();
            }

            SelectedTaskViewModel.GraphTitle = string.IsNullOrEmpty(SelectedTaskViewModel.GraphTitle) ? "" : SelectedTaskViewModel.GraphTitle;
            SelectedTaskViewModel.VerticalAxisTitle = string.IsNullOrEmpty(SelectedTaskViewModel.VerticalAxisTitle) ? "" : SelectedTaskViewModel.VerticalAxisTitle;

            //if (!string.IsNullOrEmpty(args.CompletedOnTime))
            //{
            //    TasksCompleted = args.CompletedOnTime.Split(";").Take(10).ToList();
            //}

            if (RecUpdateGrid != null)
            {
                RecUpdateGrid.Refresh();
                await RecUpdateGrid.RefreshColumns();
            }
            if (IsTaskGraphRequired)
                await ViewTempGraph();
        }

        public async Task UpdateDetailDataBoundHandler(DetailDataBoundEventArgs<TasksTaskUpdatesViewModel> args)
        {
            var updatesModel = await notesService.GetAll(args.Data.UpdateId);

            args.Data.Notes = updatesModel.ToList();

            if (NotesGrid != null)
            {
                NotesGrid.Refresh();
                await NotesGrid.RefreshColumns();
            }
        }

        protected async Task StartFilteringGrid(ActionEventArgs<TasksRecTasksViewModel> args)
        {
            var rows = await RecurringTasksGrid.GetFilteredRecords();
            var TasksList = JsonConvert.DeserializeObject<List<TasksRecTasksViewModel>>(JsonConvert.SerializeObject(rows));
            RecTasksCount = TasksList.Count;

            int CompletedOne = 0, CompletedZero = 0;
            foreach (var IvTask in TasksList)
            {
                if (!string.IsNullOrEmpty(IvTask.CompletedOnTime))
                {
                    CompletedOne += IvTask.CompletedOnTime.Count(x => x == '1');
                    CompletedZero += IvTask.CompletedOnTime.Count(x => x == '0');
                }
            }
            if (CompletedOne != 0 && CompletedZero != 0)
                CompletedOnTimeCount = (CompletedOne * 100) / (CompletedOne + CompletedZero);
            else CompletedOnTimeCount = 0;

            //args = RecurringTasksGrid.GetSelectedRecords()
        }

        protected async Task ClearRedBoxes()
        {
            await jSRuntime.InvokeVoidAsync("RemoveRedBox", "TaskDescription");
            await jSRuntime.InvokeVoidAsync("RemoveRedBox", "Intiator");
            await jSRuntime.InvokeVoidAsync("RemoveRedBox", "PersonResponsible");
            await jSRuntime.InvokeVoidAsync("RemoveRedBox", "StDate");
            await jSRuntime.InvokeVoidAsync("RemoveRedBox", "EmailBodyNote");
            await jSRuntime.InvokeVoidAsync("RemoveRedBox", "EmailBodyNote");
            await jSRuntime.InvokeVoidAsync("RemoveRedBox", "UpdateMailBody");
        }

        protected void ReturnToTasks() => navigationManager.NavigateTo(AppInformation.fivesUrl);

        protected void RecentTaskImagesAndFiles()
        {
            if (TaskUploadFiles != null)
            {
                RecentTaskImagesCount = TaskUploadFiles.Where(x => x.FileInfo != null && IsImage(x.FileInfo.Name)).Count();

                RecentTaskFilesCount = TaskUploadFiles.Where(x => x.FileInfo != null && IsFile(x.FileInfo.Name)).Count();
            }

        }

        protected void CleanTaskFiles()
        {
            TaskUploadFiles.Clear();
            RecentTaskImagesCount = 0;
            RecentTaskFilesCount = 0;
            SelectedTaskViewModel.ImagesCount = 0;
            SelectedTaskViewModel.FilesCount = 0;
        }
    }
}
