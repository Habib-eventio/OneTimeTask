using BlazorDownloadFile;
using Blazored.Toast.Services;
using CamcoTasks.Data.Services;
using CamcoTasks.Infrastructure.Common.EnumHelper;
using CamcoTasks.Infrastructure.EnumHelper.Enums.Task;
using CamcoTasks.Infrastructure.ViewModel.Shared;
using CamcoTasks.Pages.RecurringTasks.ViewRecurringTasks;
using CamcoTasks.Service.IService;
using CamcoTasks.ViewModels.EmployeeDTO;
using CamcoTasks.ViewModels.TaskChangeLogDTO;
using CamcoTasks.ViewModels.TasksImagesDTO;
using CamcoTasks.ViewModels.TasksRecTasksDTO;
using CamcoTasks.ViewModels.TasksTasksDTO;
using CamcoTasks.ViewModels.TasksTasksTaskTypeDTO;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Syncfusion.Blazor.Grids;
using Syncfusion.Blazor.Inputs;
using System.ComponentModel.DataAnnotations;
using System.Net;
using static CamcoTasks.Pages.Tasks.ViewOneTimeSubTasks;
using FileInfo = Syncfusion.Blazor.Inputs.FileInfo;
using CamcoTasks.Services;

namespace CamcoTasks.Pages.Tasks.ViewTasks
{
    public partial class TasksCreateComponent
    {
        [Inject]
        private IJSRuntime jSRuntime { get; set; }
        [Inject]
        private IToastService _toastService { get; set; }
        [Inject]
        protected ITasksService taskService { get; set; }
        [Inject]
        protected IEmployeeService employeeService { get; set; }
        [Inject]
        private IDepartmentService _departmentService { get; set; }
        [Inject]
        private FileManagerService fileManagerService { get; set; }
        [Inject]
        private IBlazorDownloadFileService blazorDownloadFileService { get; set; }
        [Inject]
        protected ILogger<CreateRecurringTaskComponent> logger { get; set; }
        [Inject] ICommonDataService ICommonDataService { get; set; }
        [Inject] ICamcoProjectService CamcoProjectService { get; set; }
        [Inject] IUserContextService UserContextService { get; set; }
        [Inject] private TaskStateService TaskStateService { get; set; }
        [Parameter]
        public TasksTasksViewModel OneTimeTask { get; set; }
        [Parameter]
        public bool ForceRender { get; set; } = false;
        [Parameter]
        public EventCallback<Dictionary<string, string>> CallBackMessageTasksCreateComponent { get; set; }
        [Parameter]
        public EventCallback<bool> ReloadParentComponent { get; set; }
        protected string TaskTitle { get; set; } = "ADD NEW";

        protected bool IsLoadTask { get; set; } = true;
        protected bool IsDoingTask { get; set; } = false;
        public bool IsEditingTask { get; set; } = false;
        protected bool IsActiveTypeDialogComponent { get; set; } = false;
        protected bool IsUploading { get; set; } = false;
        protected bool IsActiveTasksDeleteFileComponent { get; set; } = false;
        protected bool IsActiveTasksImageNoteComponent { get; set; } = false;
        protected bool IsActiveTasksImageShowComponent { get; set; } = false;
        public List<ComboBoxModel<int>> StatusTypes { get; set; }
        public TasksTasksViewModel OldTask { get; set; } = new TasksTasksViewModel();
        protected TasksTasksViewModel SelectedTaskViewModel { get; set; } = new TasksTasksViewModel();

        public TasksTasksTaskTypeViewModel NewTaskType { get; set; } = new TasksTasksTaskTypeViewModel();

        protected Dictionary<string, object> htmlAttributeBig = new Dictionary<string, object>() { { "rows", "7" }, { "spellcheck", "true" } };

        protected SfUploader TaskUploadRef { get; set; }
        public string SelectedEmployeeName { get; set; }
        public List<string> TaskTypesAll { get; set; } = new List<string>();
        public List<string> Employees { get; set; }
        protected List<string> Departments { get; set; } = new List<string>();
        protected List<EmployeeEmail> EmployeeEmailsList { get; set; } = new List<EmployeeEmail>();
        public List<DDData> TaskTypes { get; set; } = new List<DDData>();
        protected List<UploadFiles> TaskUploadFiles { get; set; } = new List<UploadFiles>();
        protected List<UploadFiles> TaskUploadFile { get; set; } = new();
        protected List<TasksImagesViewModel> SelectedTaskFiles { get; set; } = new List<TasksImagesViewModel>();

        protected IEnumerable<EmployeeViewModel> EmployeeList { get; set; }
        protected List<string> StatusTask { get; set; } = new List<string>();
        protected int RecentTaskImagesCount { get; set; }
        protected int RecentTaskFilesCount { get; set; }
        public int? TempPriorityValue { get; set; }
        public int TaskTypeValues { get; set; } = 1;
        protected string StatusDropdownVal { get; set; } = "2";

        public TasksTasksViewModel SelectedUpdateTaskViewModel { get; set; } = new TasksTasksViewModel();

        protected string FilePath { get; set; }
        protected string SelectedPreTMV { get; set; }
        protected string TaskFile { get; set; } = null;
        protected string TaskImage { get; set; } = null;
        protected string OneTimeTaskErrorMessage { get; set; }
        protected TasksImagesViewModel SelectedTMV { get; set; } = new TasksImagesViewModel();
        protected TasksImagesViewModel SelectedImageNote { get; set; } = null;
        public List<int> CostingCodes { get; set; } = new List<int>();
        protected SfGrid<TasksTasksViewModel> TaskGrid { get; set; }
        protected IEnumerable<EmployeeViewModel> employeeList { get; set; }
        private int CurrentEmployeeId { get; set; }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await LoadData();
                await Task.Run(() => IsLoadTask = false);
                StateHasChanged();
            }
        }

        protected async Task LoadData()
        {
            try
            {
                await jSRuntime.InvokeAsync<object>("ShowModal", "#AddEditTaskModal");
                StatusTypes = ICommonDataService.PopulateStatusType();
                CostingCodes = await CamcoProjectService.GetCamcoProjectIdsAsync();
                employeeList = await employeeService.GetListAsync(true, false);
                await LoadEmployees();
                await StartOneTimeTask(OneTimeTask);
            }
            catch (Exception ex)
            {

                throw;
            }
          
        }

        protected async Task LoadEmployees()
        {
            EmployeeList = await employeeService.GetListAsync(true, false);
            Employees = EmployeeList.Select(a => a.FullName).OrderBy(a => a).ToList();

            EmployeeEmailsList = EmployeeList.Where(x => !string.IsNullOrEmpty(x.Email)).Select(x => new EmployeeEmail
            {
                Id = x.Id,
                FullName = x.FullName,
                Email = x.Email,
                IsSelected = false
            }).OrderBy(x => x.FullName).ToList();

            var taskTypes = await taskService.GetTaskTypes();
            TaskTypesAll = taskTypes.Select(a => a.TaskType).OrderBy(a => a).ToList();

            TaskTypeValues = 1;
            TaskTypes = TaskTypesAll.Select(a => new DDData { Text = a, Value = TaskTypeValues++.ToString() }).OrderBy(a => a.Text).ToList();
            TaskTypes.Insert(0, new DDData { Text = "ALL", Value = "0" });

            var DepartmentList = await _departmentService.GetListAsync();
            Departments = DepartmentList.Select(x => x.Name).OrderBy(x => x).ToList();
        }

        protected async Task StartOneTimeTask(TasksTasksViewModel task)
        {
            if (task == null)
            {
                await StartNewTask();
            }
            else
            {
                await StartEditTask(task);
            }

            if (task != null) await LoadFiles(task);
        }

        protected async Task StartNewTask()
        {
            await Task.Run(() => IsEditingTask = false);
            TaskTitle = "ADD NEW";
            SelectedTaskViewModel = new TasksTasksViewModel
            {
                Initiator = "",
                DateAdded = DateTime.Now,
                IsReviewed = false
            };
            NewTaskType = new TasksTasksTaskTypeViewModel();

            EmployeeEmailsList.ForEach(x => x.IsSelected = false);
        }

        protected async Task LoadFiles(TasksTasksViewModel TaskModel)
        {
            SelectedTaskFiles = (await taskService.GetOneTimeTaksUpdateFilesAsync(TaskModel.Id)).ToList();
        }

        protected async Task StartEditTask(TasksTasksViewModel task)
        {
            IsEditingTask = true;
            TaskTitle = "EDIT";
            try
            {
                SelectedTaskViewModel = await taskService.GetTaskById(task.Id);
                SelectedTaskViewModel.LastUpdate ??= DateTime.Now;
                OldTask = new TasksTasksViewModel()
                {
                    DateAdded = task.DateAdded,
                    DateCompleted = task.DateCompleted,
                    Description = task.Description,
                    Id = task.Id,
                    Initiator = task.Initiator,
                    PersonResponsible = task.PersonResponsible,
                    IsDeleted = task.IsDeleted,
                    CostingCode = task.CostingCode,
                    Progress = task.Progress,
                    TaskId = task.TaskId,
                    TasksTaskUpdates = task.TasksTaskUpdates,
                    TaskType = task.TaskType,
                    Update = task.Update,
                    StartDate = task.StartDate,
                    LastUpdate = task.LastUpdate,
                    DueDate = task.DueDate
                };
                TempPriorityValue = task.Priority;
                StateHasChanged();
            }
            catch (Exception Ex)
            {
                if (!string.IsNullOrEmpty(Ex.InnerException.Message))
                {
                    logger.LogError("Error Loading Task: ", Ex.Message);
                }
                else
                {
                    logger.LogError("Error Loading Task: ", Ex.InnerException.Message);
                }
            }
        }

        protected async Task TryHandlingTask()

        {
            if (IsEditingTask)
            {
                await HandleValidTaskEdit();
            }
            else
            {
                await HandleValidTaskAdd();
            }
        }

        protected async Task<bool> OneTimeTaskValidation(TasksTasksViewModel SelectedTask)
        {
            bool isValid = true;
            OneTimeTaskErrorMessage = null;

            if (string.IsNullOrEmpty(SelectedTask.Description))
            {
                await jSRuntime.InvokeVoidAsync("AddRedBox", "DescriptionIdAddTask");
                OneTimeTaskErrorMessage = "Please Fill Task Description Field";
                isValid = false;
                return isValid;
            }
            else
            {
                await jSRuntime.InvokeVoidAsync("RemoveRedBox", "DescriptionIdAddTask");
                OneTimeTaskErrorMessage = null;
            }
            if (string.IsNullOrEmpty(SelectedTask.TaskType))
            {
                await jSRuntime.InvokeVoidAsync("AddRedBox", "TaskTypeIdAddTask");
                OneTimeTaskErrorMessage = "Please Fill Task Type Field";
                isValid = false;
                return isValid;

            }
            else if (!TaskTypesAll.Contains(SelectedTask.TaskType))
            {
                await jSRuntime.InvokeVoidAsync("AddRedBox", "TaskTypeIdAddTask");
                OneTimeTaskErrorMessage = "Please Enter Correct Task Type Field";
                isValid = false;
                return isValid;
            }
            else
            {
                await jSRuntime.InvokeVoidAsync("RemoveRedBox", "TaskTypeIdAddTask");
                OneTimeTaskErrorMessage = null;
            }

            if (string.IsNullOrEmpty(SelectedTask.Initiator))
            {
                await jSRuntime.InvokeVoidAsync("AddRedBox", "InitiatorIdAddTask");
                OneTimeTaskErrorMessage = "Please Fill Task Initiator Field";
                isValid = false;
                return isValid;
            }
            else if (!Employees.Contains(SelectedTask.Initiator))
            {
                await jSRuntime.InvokeVoidAsync("AddRedBox", "InitiatorIdAddTask");
                OneTimeTaskErrorMessage = "Please Enter Correct Task Initiator Field";
                isValid = false;
                return isValid;
            }
            else
            {
                await jSRuntime.InvokeVoidAsync("RemoveRedBox", "InitiatorIdAddTask");
                OneTimeTaskErrorMessage = null;
            }

            if (string.IsNullOrEmpty(SelectedTask.PersonResponsible))
            {
                await jSRuntime.InvokeVoidAsync("AddRedBox", "PersonResponsibleIdAddTask");
                OneTimeTaskErrorMessage = "Please Fill Task Person Resp Field";
                isValid = false;
                return isValid;
            }
            else if (!Employees.Contains(SelectedTask.PersonResponsible))
            {
                await jSRuntime.InvokeVoidAsync("AddRedBox", "PersonResponsibleIdAddTask");
                OneTimeTaskErrorMessage = "Please Enter Correct Task Person Resp Field";
                isValid = false;
                return isValid;
            }
            else
            {
                await jSRuntime.InvokeVoidAsync("RemoveRedBox", "PersonResponsibleIdAddTask");
                OneTimeTaskErrorMessage = null;
            }

            if (SelectedTask.DateAdded == null)
            {
                await jSRuntime.InvokeVoidAsync("AddRedBox", "DateIdAddTask");
                OneTimeTaskErrorMessage = "Please Fill Task Date Added Field";
                isValid = false;
                return isValid;
            }
            else
            {
                await jSRuntime.InvokeVoidAsync("RemoveRedBox", "DateIdAddTask");
                OneTimeTaskErrorMessage = null;
            }

            return isValid;
        }

        protected async Task HandleValidTaskAdd()
        {
            if (!await OneTimeTaskValidation(SelectedTaskViewModel))
                return;

            var shortString = SelectedTaskViewModel.DateAdded.Value.ToShortDateString();
            DateTime dateTime = Convert.ToDateTime(shortString);

            if (SelectedTaskViewModel.UpcomingDate == null)
            {
                SelectedTaskViewModel.StartDate = SelectedTaskViewModel.DateAdded;
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

            string TaskType = SelectedTaskViewModel.TaskType;

            try
            {
                SelectedTaskViewModel.IsReviewed = false;
                SelectedTaskViewModel.IsDeleted = false;
                SelectedTaskViewModel.NudgeCount = 0;

                if (SelectedTaskViewModel.Id == 0)
                {
                    SelectedTaskViewModel.Id = await taskService.AddTask(SelectedTaskViewModel);
                }

                if (SelectedTaskViewModel.Id != 0)
                {

                    foreach (var file in TaskUploadFiles)
                    {
                        if (file.FileInfo != null)
                        {

                            if (!fileManagerService.IsValidSize(file.FileInfo.Size))
                            {
                                var size = Convert.ToDouble(file.FileInfo.Size / 1000000);
                                OneTimeTaskErrorMessage = (string.Format("Image size can not be more than 20 mb. Your uploaded image size is {0} mb.", size));
                                return;
                            }

                            var ImageFileName = $"{Guid.NewGuid()}.{file.FileInfo.Type}";
                            var ResultPath = fileManagerService.CreateOneTimeTaskDirectory(SelectedTaskViewModel.Id.ToString()) + ImageFileName;
                            fileManagerService.WriteToFile(file.Stream, ResultPath);


                            TasksImagesViewModel TM = new TasksImagesViewModel()
                            {
                                PictureLink = ResultPath,
                                OneTimeId = SelectedTaskViewModel.Id,
                                IsDeleted = false
                            };
                            await taskService.InsertTaskImageAsync(TM);
                        }
                    }
                    if (SelectedTaskViewModel.Priority != null)
                    {
                        int PrevPriority = Convert.ToInt32(SelectedTaskViewModel.Priority) + 1;
                        SelectedTaskViewModel.Priority = PrevPriority;
                        await taskService.UpdateOneTask(SelectedTaskViewModel);

                        await jSRuntime.InvokeAsync<object>("HideModal", "#AddEditTaskModal");

                        await CallBackMessageTasksCreateComponent.InvokeAsync(new Dictionary<string, string>() {
                        { "Task Addition Information", "Task Added Successfully" } });

                    }
                    await ReloadParentComponent.InvokeAsync(true);
                    await TaskStateService.NotifyStateChanged();
                    TaskStateService.NotifyStateChanged();

                }
            }

            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    logger.LogError("Task Add Error", ex.InnerException.Message);
                }
                else
                {
                    logger.LogError("Task Add Error", ex.Message);
                }
            }
        }

        protected async Task HandleValidTaskEdit()
        {
            if (!await OneTimeTaskValidation(SelectedTaskViewModel))
                return;

            var shortString = SelectedTaskViewModel.StartDate?.ToShortDateString();
            DateTime dateTime = Convert.ToDateTime(shortString);
            if (SelectedTaskViewModel.UpcomingDate == null)
            {
                SelectedTaskViewModel.StartDate = SelectedTaskViewModel.DateAdded;
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

            try
            {
                foreach (var file in TaskUploadFiles)
                {
                    if (file.FileInfo != null)
                    {
                        if (!fileManagerService.IsValidSize(file.FileInfo.Size))
                        {
                            var size = Convert.ToDouble(file.FileInfo.Size / 1000000);
                            OneTimeTaskErrorMessage = (string.Format("File size can not be more than 20 mb. Your uploaded image size is {0} mb.", size));
                            return;
                        }


                        var ImageFileName = $"{Guid.NewGuid()}.{file.FileInfo.Type}";
                        var ResultPath = fileManagerService.CreateOneTimeTaskDirectory(SelectedTaskViewModel.Id.ToString()) + ImageFileName;
                        fileManagerService.WriteToFile(file.Stream, ResultPath);


                        TasksImagesViewModel TM = new TasksImagesViewModel()
                        {
                            PictureLink = ResultPath,
                            OneTimeId = SelectedTaskViewModel.Id,
                            IsDeleted = false,
                            RecurringId = 0
                        };
                        await taskService.InsertTaskImageAsync(TM);

                        await TaskUploadRef.ClearAllAsync();
                        TaskUploadFile = new();
                    }
                }
                var isDone = await taskService.UpdateOneTask(SelectedTaskViewModel);
                if (!isDone)
                {
                    _toastService.ShowError("Update failed.");
                    return;
                }
                var userId = UserContextService.CurrentEmployeeId;
                if (userId <= 0)
                {
                    _toastService.ShowError("No employee logged in; cannot log action.");
                    return;
                }

                var logEntry = new TaskChangeLogViewModel
                {
                    TaskId = SelectedTaskViewModel.Id,

                    Action = "Task has updated",
                    ChangeDetails = $"Due: {SelectedTaskViewModel.UpcomingDate:yyyy-MM-dd}"
                };
                //Need to add service properly
                //await taskLogsService.SaveChangeLogAsync(logEntry);

                await ReloadParentComponent.InvokeAsync(true);
                await TaskStateService.NotifyStateChanged();
                TaskStateService.NotifyStateChanged();

            }

            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    logger.LogError("Task Update Error", ex.InnerException.Message);
                }
                else
                {
                    logger.LogError("Task Update Error", ex.Message);
                }
            }
        }
        private int GetUserIdByName(string employeeName)
        {
            var employee = employeeList.FirstOrDefault(e => e.FullName == employeeName);
            return (int)(employee?.Id ?? 0L);
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
                    TaskUploadFiles.Add(file);
            }

            IsUploading = false;
        }

        protected void RemoveTaskPreImage(BeforeRemoveEventArgs args)
        {
            if (args.FilesData[0] == null)
                return;

            var RemoveFile = TaskUploadFiles.FirstOrDefault(x => x.FileInfo?.Id == args.FilesData[0].Id);
            if (RemoveFile != null)
            {
                TaskUploadFiles = TaskUploadFiles.Where(x => x.FileInfo?.Id != args.FilesData[0].Id).ToList();
                RemoveFile.FileInfo = null;
            }
        }

        protected void MarkCompleteCheck(ChangeEventArgs args, EmployeeEmail e) => e.IsSelected = (bool)args.Value;
        protected void MarkCompleteCheck(ChangeEventArgs args, TasksRecTasksViewModel e) => e.IsDeactivated = (bool)args.Value;

        protected async Task CheckDescription()
        {
            if (string.IsNullOrEmpty(SelectedTaskViewModel.Description))
            {
                await jSRuntime.InvokeVoidAsync("AddRedBox", "DescriptionIdAddTask");
            }
            else
            {
                await jSRuntime.InvokeVoidAsync("RemoveRedBox", "DescriptionIdAddTask");
            }
        }
        protected async Task CheckSummary()
        {
            if (string.IsNullOrEmpty(SelectedTaskViewModel.Summary))
            {
                await jSRuntime.InvokeVoidAsync("AddRedBox", "SummaryIdAddTask");
            }
            else
            {
                await jSRuntime.InvokeVoidAsync("RemoveRedBox", "SummaryDescriptionIdAddTask");
            }
        }

        protected void AddTaskTypeInEdite()
        {
            IsActiveTypeDialogComponent = true;
        }

        protected async Task CallbackMessageFromTypeDialogComponent(Dictionary<string, string> message)
        {
            if (message != null && message.Any())
            {
                foreach (var item in message)
                {
                    _toastService.ShowSuccess(item.Value + " " + item.Key);
                }
            }

            await Task.Run(() => IsActiveTypeDialogComponent = false);
        }

        protected string ConvertImagetoBase64(UploadFiles file)
        {
            string path = "";

            if (file != null)
            {
                byte[] byteImage = file.Stream.ToArray();
                path = $"data:image/png;base64, " + Convert.ToBase64String(byteImage);
            }

            return path;
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
                    return $"data:image/{Path.GetExtension(path).Replace(".", "").ToLower()};base64, " + Convert.ToBase64String(imageBytes);
                }
            }
            catch
            {
                return string.Empty;
            }
        }

        protected void StartDeleteTaskFile(string TMV)
        {
            IsActiveTasksDeleteFileComponent = true;
            SelectedPreTMV = TMV;
            FilePath = SelectedPreTMV;
            SelectedTMV = null;
        }

        protected void StartDeleteTaskFile(TasksImagesViewModel TMV)
        {
            IsActiveTasksDeleteFileComponent = true;
            SelectedTMV = TMV;
            FilePath = null;
        }

        protected void StartModifyingImageNote(TasksImagesViewModel model)
        {
            IsActiveTasksImageNoteComponent = true;
            SelectedImageNote = model;
        }

        protected async Task PrintingImage(string FilePath)
        {
            await jSRuntime.InvokeAsync<object>("PrintImage", FilePath);
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

        protected void ViewFiles(TasksImagesViewModel Model)
        {
            if (Model != null)
            {
                if (fileManagerService.IsImage(Model.PictureLink))
                {
                    IsActiveTasksImageShowComponent = true;
                    TaskImage = ConvertImagetoBase64(Model.PictureLink);
                }
                else
                {
                    TaskFile = Model.PictureLink;
                }
            }
        }

        protected void ViewFiles(string path = null, string key = null)
        {
            if (fileManagerService.IsImage(key))
            {
                IsActiveTasksImageShowComponent = true;
                TaskImage = path;
            }
            else if (fileManagerService.IsImage(path))
            {
                IsActiveTasksImageShowComponent = true;
                TaskImage = path;
            }
            else
            {
                TaskFile = path;
            }
        }

        protected async Task DeleteSuccessFromDeleteFileComponent(TasksImagesViewModel model)
        {
            if (model != null)
            {
                SelectedTaskFiles.Remove(model);
                var ImageList = SelectedTaskFiles.Where(x => fileManagerService.IsImage(x.PictureLink)).ToList();
            }

            await Task.Run(() => IsActiveTasksDeleteFileComponent = false);
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
                    SelectedPreTMV = null;
                }
            }

            await Task.Run(() => IsActiveTasksDeleteFileComponent = false);
        }

        protected async Task CloseImageViewComponent(bool IsClose)
        {
            if (IsClose)
                await Task.Run(() => IsActiveTasksImageShowComponent = false);
        }

        protected async Task ImageNoteComponentCallback(bool isSave)
        {
            if (isSave)
            {
                _toastService.ShowSuccess("Successfully Saved");
            }
            await Task.Run(() => IsActiveTasksImageNoteComponent = false);
        }

        protected async Task CloseComponent()
        {
            await jSRuntime.InvokeAsync<object>("HideModal", "#AddEditTaskModal");
            await CallBackMessageTasksCreateComponent.InvokeAsync(new Dictionary<string, string>());
        }
        protected string GetEnumDisplayName(StatusType status)
        {
            var displayAttribute = status.GetType()
                .GetField(status.ToString())
                .GetCustomAttributes(typeof(DisplayAttribute), false)
                .FirstOrDefault() as DisplayAttribute;

            return displayAttribute != null ? displayAttribute.Name : status.ToString();
        }
        protected async Task RefreshComponent(bool isReload)
        {
            if (isReload)
            {
                await LoadData();

                if (TaskGrid != null)
                    await TaskGrid.Refresh();
            }
        }
    }
}
