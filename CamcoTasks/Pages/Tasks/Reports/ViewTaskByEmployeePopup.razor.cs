using Blazored.Toast.Services;
using CamcoTasks.Data.Services;
using CamcoTasks.Service.IService;
using CamcoTasks.Infrastructure.Common.Email;
using CamcoTasks.ViewModels.EmployeeDTO;
using CamcoTasks.ViewModels.TasksImagesDTO;
using CamcoTasks.ViewModels.TasksRecTasksDTO;
using CamcoTasks.ViewModels.TasksTasksDTO;
using CamcoTasks.ViewModels.TasksTasksTaskTypeDTO;
using CamcoTasks.ViewModels.TasksTaskUpdatesDTO;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Syncfusion.Blazor.DropDowns;
using Syncfusion.Blazor.Grids;
using Syncfusion.Blazor.Inputs;
using System.Net;
using System.Text.RegularExpressions;
using static CamcoTasks.Pages.Tasks.ViewTasks.ViewTasksModel;
using DocumentFormat.OpenXml.Wordprocessing;
using CamcoTasks.Infrastructure.Defaults;

namespace CamcoTasks.Pages.Tasks.Reports
{
    public class ViewTaskByEmployeePopupModel : ComponentBase
    {
        private const string IMAGEFORMATS = @".jpg|.png|.gif|.jpeg|.bmp|.svg|.jfif|.apng|.ico$";
        private const string FILEFORMATS = @".pdf|.xlsb|.txt|.pptx|.zip|.rar|.pdf|.xlsx|.xls|.csv|.xlsb|.pptx|.docx$";

        [Inject]
        protected ITasksService taskService { get; set; }

        [Parameter]
        public string TaskType { get; set; }

        [Inject]
        private IJSRuntime jSRuntime { get; set; }

        [Inject]
        private NavigationManager navigationManager { get; set; }

        [Inject]
        private IToastService _toastService { get; set; }

        [Inject]
        private FileManagerService fileManagerService { get; set; }

        [Inject]
        private IEmailService emailService { get; set; }

        [Inject]
        protected IEmployeeService employeeService { get; set; }
        protected int RecentTaskImagesCount { get; set; }
        protected int RecentTaskFilesCount { get; set; }
        protected SfGrid<TasksTaskUpdatesViewModel> UpdateGrid { get; set; }
        protected string RespondBody { get; set; } = "";
        protected SfGrid<TasksTasksViewModel> TasksGrid { get; set; }
        public List<TasksTasksTaskTypeViewModel> TaskEnum { get; set; }
        public List<string> TaskTypesAll { get; set; } = new List<string>();
        public List<TasksTasksViewModel> MainTasksViewModel { get; set; }
        public List<TasksTasksViewModel> SelectedTaskTypeList { get; set; }
        protected bool IsRenderingFiles = false;
        public TasksTasksViewModel SelectedTask { get; set; } = new TasksTasksViewModel();
        protected SfComboBox<int?, TasksTasksTaskTypeViewModel> SfcomboBox { get; set; }
        protected TasksTaskUpdatesViewModel UpdateEditContext { get; set; } = new TasksTaskUpdatesViewModel();
        public TasksTasksViewModel UpdatesTask { get; set; } = new TasksTasksViewModel();
        public TasksTaskUpdatesViewModel DeleteViewModelUpdate { get; set; } = new TasksTaskUpdatesViewModel();
        protected TasksTaskUpdatesViewModel SelectedUpdateViewModel { get; set; } = new TasksTaskUpdatesViewModel();
        public TasksTaskUpdatesViewModel tasksTaskUpdate { get; set; } = new TasksTaskUpdatesViewModel();
        protected SfTextBox EmailNoteRef { get; set; }
        protected SfGrid<TasksTasksViewModel> TaskGrid { get; set; }
        protected Dictionary<string, Syncfusion.Blazor.Inputs.FileInfo> PreviewImagesList { get; set; } = new();
        protected Dictionary<string, string> PreviewImages { get; set; } = new();
        protected string TaskImage { get; set; }
        public List<string> Employees { get; set; }
        protected Dictionary<string, string> PreviewFiles { get; set; } = new();
        protected List<UploadFiles> TaskUploadFiles { get; set; } = new List<UploadFiles>();
        protected string TType { get; set; }
        public List<TasksTasksViewModel> Tasks { get; set; }
        protected int? SelectedTypeValue { get; set; } = 0;
        protected SfUploader TaskUploadRef { get; set; }
        protected Dictionary<string, Syncfusion.Blazor.Inputs.FileInfo> PreviewFilesList { get; set; } = new();
        public bool IsFile(string file) => Regex.IsMatch(file, FILEFORMATS);
        public bool IsImage(string file) => Regex.IsMatch(file, IMAGEFORMATS);
        protected bool IsInnerUpdate { get; set; } = false;
        protected string ErrorDateMessage { get; set; } = "";
        protected string ErrorUpdateMessage { get; set; } = "";
        protected List<EmployeeEmail> employeeEmails { get; set; } = new List<EmployeeEmail>();
        protected UploadFiles UploadFile { get; set; } = new UploadFiles();
        public bool IsValidSize(double length) => length / 1000000 <= 20;
        public bool IsSpinner { get; set; } = true;
        public bool IsTaskDone { get; set; } = false;
        protected SfUploader UploadObj { get; set; }
        public TasksTasksViewModel OldTask { get; set; } = new TasksTasksViewModel();
        public int? tempPriorityValue { get; set; }
        TasksTasksViewModel EmailTask { get; set; } = new TasksTasksViewModel();
        private static List<TasksTasksViewModel> mainTasksModel { get; set; } = new List<TasksTasksViewModel>();
        protected SfTextBox SimpleEmailNoteRef { get; set; }
        public int TaskTypeValues { get; set; } = 1;
        public TasksTasksViewModel NewTask { get; set; } = new TasksTasksViewModel() { DateAdded = DateTime.Now };
        public TasksTasksTaskTypeViewModel NewTaskType { get; set; } = new TasksTasksTaskTypeViewModel();
        protected string RespondBody2 { get; set; } = "";
        public List<DDData> TaskTypes { get; set; } = new List<DDData>();
        protected bool IsRenderingImages = false;
        public bool IsEditingTask { get; set; } = false;
        protected bool IsUploading { get; set; } = false;
        protected bool IsRunning { get; set; } = false;
        protected string TaskTitle { get; set; } = "ADD NEW";
        public int TaskCount { get; set; } = 0;
        protected bool IsDoingTask { get; set; } = false;
        protected int ProgressValue { get; set; } = 0;
        protected int ProgressMax { get; set; } = 100;
        protected SfComboBox<string, string> TaskeTypeCombo { get; set; }
        protected List<TasksImagesViewModel> SelectedTaskImages { get; set; } = new List<TasksImagesViewModel>();

        protected Dictionary<string, object> htmlAttribute = new Dictionary<string, object>() { { "rows", "4" } };
        protected List<string> DepartmentsViewModel { get; set; } = new List<string>();
        protected void MarkCompleteCheck(ChangeEventArgs args, EmployeeEmail e) => e.IsSelected = (bool)args.Value;
        protected void MarkCompleteCheck(ChangeEventArgs args, TasksRecTasksViewModel e) => e.IsDeactivated = (bool)args.Value;

        protected Dictionary<string, object> htmlAttributeBig = new Dictionary<string, object>() { { "rows", "7" }, { "spellcheck", "true" } };

        protected List<UploadFiles> TaskUploadFile { get; set; } = new();
        protected List<TasksImagesViewModel> SelectedTaskFiles { get; set; } = new List<TasksImagesViewModel>();
        protected string StatusDropdownVal { get; set; } = "2";
        protected List<IGrouping<string, TasksTasksViewModel>> ActivatedTasks { get; set; } = new List<IGrouping<string, TasksTasksViewModel>>();

        protected override async Task OnInitializedAsync()
        {

            TaskEnum = (await taskService.GetTaskTypes()).ToList();
            SelectedTypeValue = TaskEnum.FirstOrDefault()?.Id;
            StateHasChanged();


            mainTasksModel = (await taskService.GetAllTasksSync()).ToList();
            Tasks = mainTasksModel.Where(a => !a.DateCompleted.HasValue).ToList();

            ActivatedTasks = Tasks.GroupBy(x => x.TaskType).Where(a => a.Count() > 5).ToList();

            await LoadData();

            IsRunning = true;

        }
        protected async Task LoadData()
        {
            var _employees = await employeeService.GetListAsync(true);
            Employees = _employees.Select(a => a.FullName).OrderBy(a => a).ToList();
            employeeEmails = _employees.Where(x => !string.IsNullOrEmpty(x.Email)).Select(x => new EmployeeEmail
            {
                Id = x.Id,
                FullName = x.FullName,
                Email = x.Email,
                IsSelected = false
            }).OrderBy(x => x.FullName).ToList();

            MainTasksViewModel = (await taskService.GetAllTasksSync1()).ToList();

            if (!string.IsNullOrEmpty(TaskType))
                SelectedTypeValue = TaskEnum.FirstOrDefault(x => x.TaskType == TaskType)?.Id;

            if (SelectedTypeValue == 0)
            {
                if (TaskEnum.Any())
                {
                    SelectedTypeValue = TaskEnum.First().Id;
                    LoadEmployeeData(new SelectEventArgs<TasksTasksTaskTypeViewModel>()
                    {
                        ItemData = new TasksTasksTaskTypeViewModel { TaskType = TaskEnum.First().TaskType }
                    });
                }
            }
            else
            {
                LoadEmployeeData(new SelectEventArgs<TasksTasksTaskTypeViewModel>()
                {
                    ItemData = new TasksTasksTaskTypeViewModel { TaskType = TaskEnum.FirstOrDefault(x => x.Id == SelectedTypeValue).TaskType }
                });
            }

            await LoadPageDetails();
        }
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                System.Threading.Thread.Sleep(2500);
                await jSRuntime.InvokeAsync<object>("modalDraggable");
            }
        }
        private void SetProgressMax(int Value)
        {
            ProgressMax = Value;
            InvokeAsync(StateHasChanged);
        }
        private void SetProgressValue()
        {
            ProgressValue++;
            InvokeAsync(StateHasChanged);
        }
        protected async Task LoadPageDetails()
        {
            SelectedTask.TasksTaskUpdates = new List<TasksTaskUpdatesViewModel>();
            mainTasksModel = (await taskService.GetAllTasks()).ToList();

            var TaskUpdates = await taskService.GetTaskUpdates(mainTasksModel.Select(x => x.Id).ToList());

            SetProgressMax(mainTasksModel.Count + 100);
            mainTasksModel.ForEach(x =>
            {
                if (TaskUpdates.Any(f => f.TaskID == x.Id))
                {
                    x.LatestUpdate = TaskUpdates.Where(a => a.TaskID == x.Id)?.Max(g => g.UpdateDate.Date);
                }
                SetProgressValue();
                StateHasChanged();
            });

            //Here All The Active Tasks are Loaded, we can use this instead of sending another request to DB which is same thing.
            Tasks = mainTasksModel.Where(a => !a.DateCompleted.HasValue).ToList();
            //   TotalPasDueTasks = Tasks.Where(x => x.DateCompleted > DateTime.Today);
            var taskTypes = await taskService.GetTaskTypes();
            var _employees = (await employeeService.GetListAsync()).ToList();
            TaskCount = Tasks.Count;
            Tasks = Tasks.OrderBy(x => x.Priority).ToList();
            SetProgressValue(50);
            TaskTypesAll = taskTypes.Select(a => a.TaskType).OrderBy(a => a).ToList();
            StateHasChanged();

            TaskTypeValues = 1;
            TaskTypes = TaskTypesAll.Select(a => new DDData { Text = a, Value = TaskTypeValues++.ToString() }).OrderBy(a => a.Text).ToList();
            TaskTypes.Insert(0, new DDData { Text = "ALL", Value = "0" });
            SetProgressValue(50);
            StateHasChanged();

            Employees = _employees.Select(a => a.FullName).OrderBy(a => a).ToList();
            DepartmentsViewModel = _employees.Select(x => x.Department?.Name).OrderBy(x => x).Distinct().ToList();
            await SetNewDefaults();
            IsSpinner = false;
            StateHasChanged();
        }
        private void SetProgressValue(int Number)
        {
            ProgressValue += Number;
            InvokeAsync(StateHasChanged);
        }
        protected async Task CheckAddTaskTypeFirstEmail()
        {
            if (string.IsNullOrEmpty(NewTaskType.Email))
            {
                await jSRuntime.InvokeVoidAsync("AddRedBox", "FirstEmailId");
            }
            else
            {
                await jSRuntime.InvokeVoidAsync("RemoveRedBox", "FirstEmailId");
            }

            StateHasChanged();
        }
        protected async Task HandleValidAddType()
        {
            try
            {
                bool isValid = true;
                if (string.IsNullOrEmpty(NewTaskType.TaskType))
                {
                    isValid = false;
                    await jSRuntime.InvokeVoidAsync("AddRedBox", "AddTaskTypeId");
                    StateHasChanged();
                }
                else
                {
                    await jSRuntime.InvokeVoidAsync("RemoveRedBox", "AddTaskTypeId");
                    StateHasChanged();
                }

                if (string.IsNullOrEmpty(NewTaskType.Email))
                {
                    await jSRuntime.InvokeVoidAsync("AddRedBox", "FirstEmailId");
                    isValid = false;
                    StateHasChanged();
                }
                else
                {
                    await jSRuntime.InvokeVoidAsync("RemoveRedBox", "FirstEmailId");
                    StateHasChanged();
                }

                if (!isValid)
                {
                    _toastService.ShowError("Please Fill All Required Field..!");
                    return;
                }

                if (TaskTypesAll.Contains(NewTaskType.TaskType))
                {
                    _toastService.ShowError("Error: Task Type Already Exists!");
                    return;
                }

                NewTaskType.Id = 0;
                var _b = await taskService.AddTaskType(NewTaskType);
                NewTaskType.Id = _b;
                if (NewTaskType.Id != 0)
                {
                    TaskTypesAll.Add(NewTaskType.TaskType);
                    TaskTypesAll = TaskTypesAll.OrderBy(x => x).ToList();
                    TaskTypeValues = 1;
                    TaskTypes = TaskTypesAll.Select(a => new DDData { Text = a, Value = TaskTypeValues++.ToString() }).OrderBy(a => a.Text).ToList();
                    TaskTypes.Insert(0, new DDData { Text = "ALL", Value = "0" });

                    await SetTaskTypeDefaults();

                    await jSRuntime.InvokeAsync<object>("HideModal", "#AddTypeDialogOneTimeTask");

                    StateHasChanged();
                    _toastService.ShowSuccess("Task Type Added Successfully");
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    StateHasChanged();
                    _toastService.ShowError(ex.InnerException.Message);
                }
                else
                {
                    StateHasChanged();
                    _toastService.ShowError(ex.Message);
                }
            }

        }
        protected async Task SetTaskTypeDefaults()
        {
            NewTaskType = new TasksTasksTaskTypeViewModel();
            await InvokeAsync(StateHasChanged);
        }

        protected async Task CloseAddTaskTypeModal()
        {
            await jSRuntime.InvokeAsync<object>("HideModal", "#AddTypeDialogOneTimeTask");
        }
        protected async Task StartNewTask()
        {
            IsEditingTask = false;
            TaskTitle = "ADD NEW";
            SelectedTask = new TasksTasksViewModel
            {
                Initiator = "ARNOLD, RICHARD",
                Priority = 10,
                DateAdded = DateTime.Now,
                IsReviewed = false
            };
            NewTaskType = new TasksTasksTaskTypeViewModel();

            employeeEmails.ForEach(x => x.IsSelected = false);
            await Task.Delay(200);
            await TaskeTypeCombo.FocusAsync();
            StateHasChanged();
        }
        protected async Task CheckAddTaskType()
        {
            if (string.IsNullOrEmpty(NewTaskType.TaskType))
            {
                await jSRuntime.InvokeVoidAsync("AddRedBox", "AddTaskTypeId");
            }
            else
            {
                await jSRuntime.InvokeVoidAsync("RemoveRedBox", "AddTaskTypeId");
            }

            StateHasChanged();
        }
        protected async Task TaskPreViewFiles()
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
                                _toastService.ShowError(string.Format("File size can not be more than 20 mb. Your uploaded image size is {0} mb.", size));
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
            StateHasChanged();
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
            StateHasChanged();
        }
        protected void SelectTaskImages(UploadChangeEventArgs args)
        {
            foreach (var file in args.Files)
            {

                var fileLookUp = TaskUploadFiles.FirstOrDefault(x => x.FileInfo != null && file.FileInfo.Id == x.FileInfo.Id);
                if (fileLookUp == null)
                    TaskUploadFiles.Add(file);
            }

            RecentTaskImagesAndFiles();
            IsUploading = false;
        }
        protected async Task LoadTaskFiles(TasksTasksViewModel TaskModel)
        {
            IsRenderingFiles = true;
            var SelectedTaskFiles = (await taskService.GetOneTimeTaskImagesAsync(TaskModel.Id)).ToList();
            this.SelectedTaskFiles = SelectedTaskFiles.Where(x => IsFile(x.PictureLink)).ToList();
            foreach (var file in SelectedTaskFiles)
            {
                file.FileName = Path.GetFileName(file.PictureLink);
            }
            StateHasChanged();
            IsRenderingFiles = false;
        }
        protected async Task CheckDescription()
        {
            if (string.IsNullOrEmpty(SelectedTask.Description))
            {
                await jSRuntime.InvokeVoidAsync("AddRedBox", "DescriptionId");
            }
            else
            {
                await jSRuntime.InvokeVoidAsync("RemoveRedBox", "DescriptionId");
            }

            StateHasChanged();
        }

        protected async Task CheckInitiator()
        {
            if (string.IsNullOrEmpty(SelectedTask.Initiator))
            {
                await jSRuntime.InvokeVoidAsync("AddRedBox", "InitiatorId");
            }
            else
            {
                await jSRuntime.InvokeVoidAsync("RemoveRedBox", "InitiatorId");
            }

            StateHasChanged();
        }
        protected void BeforeUploadImage()
        {
            IsUploading = true;
        }
        protected async Task CheckDateAdded()
        {
            if (SelectedTask.DateAdded == null)
            {
                await jSRuntime.InvokeVoidAsync("AddRedBox", "DateId");
            }
            else
            {
                await jSRuntime.InvokeVoidAsync("RemoveRedBox", "DateId");
            }

            StateHasChanged();
        }
        protected async Task CheckTaskType()
        {
            if (string.IsNullOrEmpty(SelectedTask.TaskType))
            {
                await jSRuntime.InvokeVoidAsync("AddRedBox", "TaskTypeId");
            }
            else
            {
                await jSRuntime.InvokeVoidAsync("RemoveRedBox", "TaskTypeId");
            }

            StateHasChanged();
        }

        protected async Task HandleValidAdd()
        {
            bool isValid = true;
            if (string.IsNullOrEmpty(SelectedTask.TaskType))
            {
                await jSRuntime.InvokeVoidAsync("AddRedBox", "TaskTypeId");
                isValid = false;
                StateHasChanged();

            }
            else if (!TaskTypesAll.Contains(SelectedTask.TaskType))
            {
                await jSRuntime.InvokeVoidAsync("AddRedBox", "TaskTypeId");
                isValid = false;
                StateHasChanged();
            }
            else
            {
                await jSRuntime.InvokeVoidAsync("RemoveRedBox", "TaskTypeId");
            }

            if (string.IsNullOrEmpty(SelectedTask.Initiator))
            {
                await jSRuntime.InvokeVoidAsync("AddRedBox", "InitiatorId");
                isValid = false;
                StateHasChanged();
            }
            else if (!Employees.Contains(SelectedTask.Initiator))
            {
                _toastService.ShowError("Please Enter Correct Initiator");
                await jSRuntime.InvokeVoidAsync("AddRedBox", "InitiatorId");
                isValid = false;
                StateHasChanged();
            }
            else
            {
                await jSRuntime.InvokeVoidAsync("RemoveRedBox", "InitiatorId");
                StateHasChanged();
            }
            if (SelectedTask.DateAdded == null)
            {
                await jSRuntime.InvokeVoidAsync("AddRedBox", "DateId");
                isValid = false;
                StateHasChanged();
            }
            else
            {
                await jSRuntime.InvokeVoidAsync("RemoveRedBox", "DateId");
                StateHasChanged();
            }
            if (string.IsNullOrEmpty(SelectedTask.Description))
            {
                await jSRuntime.InvokeVoidAsync("AddRedBox", "DescriptionId");
                isValid = false;
                StateHasChanged();
            }
            else
            {
                await jSRuntime.InvokeVoidAsync("RemoveRedBox", "DescriptionId");
                StateHasChanged();
            }

            if (!isValid)
            {
                _toastService.ShowError(string.Format("Please Fill Required Field"));
                return;
            }
            string TaskType = SelectedTask.TaskType;
            try
            {
                SelectedTask.IsReviewed = false;
                SelectedTask.IsDeleted = false;
                SelectedTask.NudgeCount = 0;

                if (SelectedTask.Id == 0)
                {
                    SelectedTask.Id = await taskService.AddTask(SelectedTask);
                }

                if (SelectedTask.Id != 0)
                {

                    foreach (var file in TaskUploadFiles)
                    {
                        if (file.FileInfo != null)
                        {

                            if (!IsValidSize(file.FileInfo.Size))
                            {
                                var size = Convert.ToDouble(file.FileInfo.Size / 1000000);
                                _toastService.ShowError(string.Format("Image size can not be more than 20 mb. Your uploaded image size is {0} mb.", size));
                                isValid = false;
                                return;
                            }

                            var ImageFileName = $"{Guid.NewGuid()}.{file.FileInfo.Type}";
                            var ResultPath = fileManagerService.CreateOneTimeTaskDirectory(SelectedTask.Id.ToString()) + ImageFileName;
                            fileManagerService.WriteToFile(file.Stream, ResultPath);
                            SelectedTask.PictureLink = ResultPath;

                            TasksImagesViewModel TM = new TasksImagesViewModel()
                            {
                                PictureLink = ResultPath,
                                OneTimeId = SelectedTask.Id,
                                IsDeleted = false
                            };
                            await taskService.InsertTaskImageAsync(TM);


                        }
                    }
                    if (SelectedTask.Priority != null)
                    {
                        var taskLook = Tasks.FirstOrDefault(x => x.Id != SelectedTask.Id &&
                    x.TaskType == SelectedTask.TaskType &&
                    x.Priority == Convert.ToInt32(SelectedTask.Priority));

                        int PrevPriority = Convert.ToInt32(SelectedTask.Priority) + 1;
                        while (taskLook != null)
                        {
                            taskLook.Priority = PrevPriority;
                            await taskService.UpdateOneTask(taskLook);

                            Tasks[Tasks.FindIndex(x => x.Id == taskLook.Id)] = taskLook;
                            mainTasksModel[mainTasksModel.FindIndex(x => x.Id == taskLook.Id)] = taskLook;

                            taskLook = Tasks.FirstOrDefault(x => x.Id != taskLook.Id &&
                    x.TaskType == taskLook.TaskType &&
                    x.Priority == PrevPriority);

                            PrevPriority++;
                        }
                    }

                    if (StatusDropdownVal == "2" && !@SelectedTask.DateCompleted.HasValue)
                    {
                        Tasks.Add(SelectedTask);
                    }
                    else if (StatusDropdownVal == "3" && @SelectedTask.DateCompleted.HasValue)
                    {
                        Tasks.Add(SelectedTask);
                    }

                    mainTasksModel.Add(SelectedTask);

                    await TaskUploadRef.ClearAllAsync();
                    await jSRuntime.InvokeAsync<object>("HideModal", "#AddEditOneTimeTaskModal");

                    Tasks = Tasks.OrderBy(x => x.Priority).ToList();

                    await TaskGrid.Refresh();
                    await TaskGrid.RefreshColumnsAsync();

                    await SetNewDefaults();
                    StateHasChanged();

                    _toastService.ShowSuccess("Task Added Successfully");

                }
            }

            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    StateHasChanged();
                    _toastService.ShowError(ex.InnerException.Message);
                }
                else
                {
                    StateHasChanged();
                    _toastService.ShowError(ex.Message);
                }
            }
            await CleaneTaskFiles();

        }
        protected async Task HandleValidEdit()
        {
            bool isValid = true;
            if (string.IsNullOrEmpty(SelectedTask.TaskType))
            {
                await jSRuntime.InvokeVoidAsync("AddRedBox", "TaskTypeId");
                isValid = false;
            }
            else if (!TaskTypesAll.Contains(SelectedTask.TaskType))
            {
                await jSRuntime.InvokeVoidAsync("AddRedBox", "TaskTypeId");
                isValid = false;
            }
            else
            {
                await jSRuntime.InvokeVoidAsync("RemoveRedBox", "TaskTypeId");
            }

            if (string.IsNullOrEmpty(SelectedTask.Initiator))
            {
                await jSRuntime.InvokeVoidAsync("AddRedBox", "InitiatorId");
                isValid = false;
            }
            else if (!Employees.Contains(SelectedTask.Initiator))
            {
                await jSRuntime.InvokeVoidAsync("AddRedBox", "InitiatorId");
                isValid = false;
            }
            else
            {
                await jSRuntime.InvokeVoidAsync("RemoveRedBox", "InitiatorId");
            }

            if (SelectedTask.DateAdded == null)
            {
                await jSRuntime.InvokeVoidAsync("AddRedBox", "DateId");
                isValid = false;
            }
            else
            {
                await jSRuntime.InvokeVoidAsync("RemoveRedBox", "DateId");
            }

            if (string.IsNullOrEmpty(SelectedTask.Description))
            {
                await jSRuntime.InvokeVoidAsync("AddRedBox", "DescriptionId");
                isValid = false;
            }
            else
            {
                await jSRuntime.InvokeVoidAsync("RemoveRedBox", "DescriptionId");
            }

            if (!isValid)
            {
                _toastService.ShowError("Please Fill All Required Field..!");
                return;
            }

            try
            {
                foreach (var file in TaskUploadFiles)
                {
                    if (file.FileInfo != null)
                    {
                        if (!IsValidSize(file.FileInfo.Size))
                        {
                            var size = Convert.ToDouble(file.FileInfo.Size / 1000000);
                            _toastService.ShowError(string.Format("File size can not be more than 20 mb. Your uploaded image size is {0} mb.", size));
                            return;
                        }


                        var ImageFileName = $"{Guid.NewGuid()}.{file.FileInfo.Type}";
                        var ResultPath = fileManagerService.CreateOneTimeTaskDirectory(SelectedTask.Id.ToString()) + ImageFileName;
                        fileManagerService.WriteToFile(file.Stream, ResultPath);
                        SelectedTask.PictureLink = ResultPath;

                        TasksImagesViewModel TM = new TasksImagesViewModel()
                        {
                            PictureLink = ResultPath,
                            OneTimeId = SelectedTask.Id,
                            IsDeleted = false,
                            RecurringId = 0
                        };
                        await taskService.InsertTaskImageAsync(TM);

                        await TaskUploadRef.ClearAllAsync();
                        TaskUploadFile = new();
                    }
                }

                var _b = await taskService.UpdateTask(SelectedTask, tempPriorityValue);

                if (_b)
                {
                    int Ind = Tasks.FindIndex(x => x.Id == SelectedTask.Id);

                    if (SelectedTask.DateCompleted.HasValue)
                    {
                        Tasks.RemoveAt(Ind);
                    }
                    else
                    {
                        Tasks[Ind] = SelectedTask;
                    }

                    mainTasksModel[mainTasksModel.FindIndex(x => x.Id == SelectedTask.Id)] = SelectedTask;

                    if (SelectedTask.Priority != null)
                    {
                        var taskLook = Tasks.FirstOrDefault(x => x.Id != SelectedTask.Id &&
                    x.TaskType == SelectedTask.TaskType &&
                    x.Priority == Convert.ToInt32(SelectedTask.Priority));


                        int PrevPriority = Convert.ToInt32(SelectedTask.Priority) + 1;
                        while (taskLook != null)
                        {
                            taskLook.Priority = PrevPriority;
                            await taskService.UpdateOneTask(taskLook);

                            Tasks[Tasks.FindIndex(x => x.Id == taskLook.Id)] = taskLook;
                            mainTasksModel[mainTasksModel.FindIndex(x => x.Id == taskLook.Id)] = taskLook;

                            taskLook = Tasks.FirstOrDefault(x => x.Id != taskLook.Id &&
                    x.TaskType == taskLook.TaskType &&
                    x.Priority == PrevPriority);

                            PrevPriority++;
                        }
                    }

                    await jSRuntime.InvokeAsync<object>("HideModal", "#AddEditTaskModal");

                    await SetNewDefaults();

                    Tasks = Tasks.OrderBy(x => x.Priority).ToList();

                    await TaskGrid.Refresh();
                    await TaskGrid.RefreshColumnsAsync();
                    StateHasChanged();

                    _toastService.ShowSuccess("Task Updated Successfully");

                    Tasks.Add(SelectedTask);
                    mainTasksModel.Add(SelectedTask);
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    StateHasChanged();
                    _toastService.ShowError(ex.InnerException.Message);
                }
                else
                {
                    StateHasChanged();
                    _toastService.ShowError(ex.Message);
                }
            }
            await CleaneTaskFiles();
        }
        protected async Task TryHandlingTask()
        {
            await StartTask();
            if (IsEditingTask)
            {
                await HandleValidEdit();
            }
            else
            {
                await HandleValidAdd();
            }
            await StopTask();
        }
        protected async Task StartTask()
        {
            IsDoingTask = true;
            await InvokeAsync(StateHasChanged);
        }
        protected async Task StopTask()
        {
            IsDoingTask = false;
            await InvokeAsync(StateHasChanged);
        }
        protected async Task DeleteUpdate(TasksTaskUpdatesViewModel tasksTaskUpdates)
        {
            try
            {
                UpdatesTask.TasksTaskUpdates.Remove(tasksTaskUpdates);
                mainTasksModel[mainTasksModel.FindIndex(x => x.Id == tasksTaskUpdates.TaskID)].TasksTaskUpdates.Remove(tasksTaskUpdates);

                tasksTaskUpdates.IsDeleted = true;
                var result = await taskService.UpdateTaskUpdate(tasksTaskUpdates);

                if (UpdateGrid != null)
                {
                    await UpdateGrid.Refresh();
                    await UpdateGrid.RefreshColumnsAsync();
                }

                _toastService.ShowSuccess("Update Has Been Deleted!");
                StateHasChanged();
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    StateHasChanged();
                    _toastService.ShowError(ex.InnerException.Message);
                }
                else
                {
                    StateHasChanged();
                    _toastService.ShowError(ex.Message);
                }
            }
        }

        protected async Task CloseOTTUpdateModal()
        {
            await jSRuntime.InvokeAsync<object>("HideModal", "#TaskUpdatesPopup");
        }
        protected async Task CloseOTTAddTaskUpdate()
        {
            await jSRuntime.InvokeAsync<object>("HideModal", "#AddTaskUpdateModal");
        }
        protected async Task CloseImageModal()
        {
            await jSRuntime.InvokeAsync<object>("HideModal", "#ImagePopup");
        }
        protected async Task RefreshUpdateModal(bool IsInner)
        {
            IsInnerUpdate = IsInner;
            tasksTaskUpdate = new TasksTaskUpdatesViewModel() { UpdateDate = DateTime.Today };
            await Task.Delay(1);
            StateHasChanged();
        }

        protected async Task HandleUpdateModel()
        {
            bool isValid = true;
            if (UpdateEditContext.UpdateDate < DateTime.Today)
            {
                await jSRuntime.InvokeVoidAsync("AddRedBox", "OneTEditDateId");
                isValid = false;
            }
            else
            {
                await jSRuntime.InvokeVoidAsync("RemoveRedBox", "OneTEditDateId");
            }
            if (string.IsNullOrEmpty(UpdateEditContext.Update))
            {
                await jSRuntime.InvokeVoidAsync("AddRedBox", "OneTEditDescId");
                isValid = false;
            }
            else
            {
                await jSRuntime.InvokeVoidAsync("RemoveRedBox", "OneTEditDescId");
            }

            if (!isValid)
            {
                _toastService.ShowError("Please Fill All Required Field..!");
                return;
            }
            var result = await taskService.UpdateTaskUpdate(UpdateEditContext);

            var TaskIndex = mainTasksModel[mainTasksModel.FindIndex(x => x.Id == UpdateEditContext.TaskID)];
            TaskIndex.TasksTaskUpdates[TaskIndex.TasksTaskUpdates.FindIndex(x => x.UpdateId == UpdateEditContext.UpdateId)] = UpdateEditContext;

            if (UpdateGrid != null)
            {
                await UpdateGrid.Refresh();
                await UpdateGrid.RefreshColumnsAsync();
            }

            UpdateEditContext = new TasksTaskUpdatesViewModel();
            await SetNewDefaults();

            await jSRuntime.InvokeAsync<object>("HideModal", "#EditUpdateDialog");
            StateHasChanged();
        }
        protected void RecentTaskImagesAndFiles()
        {
            if (TaskUploadFiles != null)
            {
                RecentTaskImagesCount = TaskUploadFiles.Where(x => x.FileInfo != null && IsImage(x.FileInfo.Name)).Count();
                RecentTaskFilesCount = TaskUploadFiles.Where(x => x.FileInfo != null && IsFile(x.FileInfo.Name)).Count();
            }

        }
        protected async Task StartEditTask(TasksTasksViewModel task)
        {
            RecentTaskImagesAndFiles();


            IsEditingTask = true;
            TaskTitle = "EDIT";
            try
            {
                SelectedTask = await taskService.GetTaskById(task.Id);
                OldTask = new TasksTasksViewModel()
                {
                    DateAdded = task.DateAdded,
                    DateCompleted = task.DateCompleted,
                    Description = task.Description,
                    Id = task.Id,
                    Initiator = task.Initiator,
                    IsDeleted = task.IsDeleted,
                    Priority = task.Priority,
                    Progress = task.Progress,
                    TaskId = task.TaskId,
                    TasksTaskUpdates = task.TasksTaskUpdates,
                    TaskType = task.TaskType,
                    Update = task.Update
                };
                tempPriorityValue = task.Priority;
                StateHasChanged();
            }
            catch (Exception Ex)
            {
                if (!string.IsNullOrEmpty(Ex.InnerException.Message))
                {
                    _toastService.ShowError("Error Loading Task");
                }
                else
                {
                    _toastService.ShowError("Error Loading Task");
                }
            }

            if (UpdatesTask.ImagesCount == 0 && UpdatesTask.ImagesCount == 0) await TaskFileCount(task);
        }

        protected async Task TaskFileCount(TasksTasksViewModel task)
        {
            if (task != null)
            {
                UpdatesTask = task;
                UpdatesTask.TasksTaskUpdates = (await taskService.GetTaskUpdates1(task.Id)).OrderByDescending(x => x.UpdateDate.Date).ToList();
                var TempTaskImages = await taskService.GetOneTimeTaskImagesCountAsync(task.Id);
                if (TempTaskImages != null)
                {
                    UpdatesTask.ImagesCount = TempTaskImages.Where(x => IsImage(x.PictureLink)).Count();
                    UpdatesTask.FilesCount = TempTaskImages.Where(x => IsFile(x.PictureLink)).Count();
                }
            }
        }
        protected async Task CleaneTaskFiles()
        {
            TaskUploadFiles.Clear();
            RecentTaskImagesCount = 0;
            RecentTaskFilesCount = 0;
            UpdatesTask.ImagesCount = 0;
            UpdatesTask.FilesCount = 0;
            await jSRuntime.InvokeAsync<object>("HideModal", "#AddEditOneTimeTaskModal");
        }
        protected async Task CheckOneTEditDate()
        {
            if (UpdateEditContext.UpdateDate < DateTime.Now)
            {
                await jSRuntime.InvokeVoidAsync("AddRedBox", "OneTEditDateId");
            }
            else
            {
                await jSRuntime.InvokeVoidAsync("RemoveRedBox", "OneTEditDateId");
            }

            StateHasChanged();
        }

        protected async Task CheckOneTEditDesc()
        {
            if (string.IsNullOrEmpty(UpdateEditContext.Update))
            {
                await jSRuntime.InvokeVoidAsync("AddRedBox", "OneTEditDescId");
            }
            else
            {
                await jSRuntime.InvokeVoidAsync("RemoveRedBox", "OneTEditDescId");
            }

            StateHasChanged();
        }

        protected async Task SetNewDefaults()
        {
            SelectedTask = new TasksTasksViewModel
            {
                TasksTaskUpdates = new List<TasksTaskUpdatesViewModel>()
            };

            NewTask = new TasksTasksViewModel
            {
                Initiator = "ARNOLD, RICHARD",
                Priority = 10,
                DateAdded = DateTime.Now,
                IsReviewed = false
            };

            NewTaskType = new TasksTasksTaskTypeViewModel();

            await InvokeAsync(StateHasChanged);
        }

        protected async Task ConfirmDelete()
        {
            try
            {
                Tasks.Remove(SelectedTask);
                Tasks = Tasks.OrderBy(x => x.Priority).ToList();

                mainTasksModel.Remove(SelectedTask);

                SelectedTask.IsDeleted = true;

                await taskService.RemoveOneTask(SelectedTask);

                await TaskGrid.RefreshColumnsAsync();
                await TaskGrid.Refresh();

                await SetNewDefaults();
                StateHasChanged();

                _toastService.ShowSuccess("Task Removed!");
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
                StateHasChanged();
            }
        }
        protected void ViewUpdateImage(TasksTaskUpdatesViewModel model)
        {
            TaskImage = ConvertImagetoBase64(model.PictureLink);
        }

        protected async Task PrintingImage(string FilePath)
        {
            await jSRuntime.InvokeAsync<object>("PrintImage", TaskImage);
        }

        protected async Task LoadTaskImages(TasksTasksViewModel TaskModel)
        {
            IsRenderingImages = true;
            SelectedTaskImages = (await taskService.GetOneTimeTaskImagesAsync(TaskModel.Id)).ToList();
            StateHasChanged();
            IsRenderingImages = false;
        }

        protected string ConvertImagetoBase64(string path)
        {
            if (string.IsNullOrEmpty(path))
                return string.Empty;
            try
            {
                using var webClient = new WebClient();
                byte[] imageBytes = webClient.DownloadData(path);
                return $"data:image/{Path.GetExtension(path).Replace(".", "").ToLower()};base64, " +
                Convert.ToBase64String(imageBytes);
            }
            catch
            {
                return string.Empty;
            }
        }

        public async Task AddNewUpdate(bool CloseModal)
        {
            try
            {
                bool isValid = true;
                if (tasksTaskUpdate.UpdateDate < DateTime.Today)
                {
                    await jSRuntime.InvokeVoidAsync("AddRedBox", "OneTimeUpdDate");
                    ErrorDateMessage = "Please Select Valid Date";
                    isValid = false;
                }
                else
                {
                    await jSRuntime.InvokeVoidAsync("RemoveRedBox", "OneTimeUpdDate");
                    ErrorDateMessage = string.Empty;
                }


                if (string.IsNullOrEmpty(tasksTaskUpdate.Update))
                {
                    ErrorUpdateMessage = "Please Enter Update Description";
                    await jSRuntime.InvokeVoidAsync("AddRedBox", "OneTUpdNoteId");
                    isValid = false;
                }
                else
                {
                    await jSRuntime.InvokeVoidAsync("RemoveRedBox", "OneTUpdNoteId");
                    ErrorUpdateMessage = string.Empty;
                }

                if (!isValid)
                {
                    _toastService.ShowError("Please Fill All Required Field..!");
                    return;

                }

                var file = UploadFile;
                if (file.FileInfo != null)
                {
                    if (!IsValidSize(file.FileInfo.Size))
                    {
                        var size = Convert.ToDouble(file.FileInfo.Size / 1000000);
                        _toastService.ShowError(string.Format("Image size can not be more than 20 mb. Your uploaded image size is {0} mb.", size));
                        return;
                    }

                    var ImageFileName = $"{Guid.NewGuid()}.{file.FileInfo.Type}";
                    var ResultPath = fileManagerService.CreateOneTimeTaskDirectory(UpdatesTask.Id.ToString()) + ImageFileName;
                    fileManagerService.WriteToFile(file.Stream, ResultPath);
                    tasksTaskUpdate.PictureLink = ResultPath;
                }

                tasksTaskUpdate.TaskID = UpdatesTask.Id;
                tasksTaskUpdate.IsDeleted = false;
                tasksTaskUpdate.UpdateId = await taskService.AddTaskUpdate(tasksTaskUpdate);

                await SendUpdateEmail(UpdatesTask.Id);

                if (IsTaskDone)
                {
                    IsTaskDone = false;
                    if (UpdatesTask.DateCompleted == null)
                        UpdatesTask.DateCompleted = DateTime.Now;

                    var result2 = await taskService.UpdateTask(UpdatesTask, UpdatesTask.Priority);

                    Tasks.RemoveAt(Tasks.FindIndex(x => x.Id == UpdatesTask.Id));
                    mainTasksModel.RemoveAt(mainTasksModel.FindIndex(x => x.Id == UpdatesTask.Id));
                }

                UploadFile = new UploadFiles();
                IsTaskDone = false;

                Tasks = Tasks.OrderBy(x => x.Priority).ToList();

                if (UpdateGrid != null)
                {
                    UpdatesTask.TasksTaskUpdates.Add(tasksTaskUpdate);
                    UpdatesTask.TasksTaskUpdates = UpdatesTask.TasksTaskUpdates.OrderByDescending(x => x.UpdateDate.Date).ToList();
                    await UpdateGrid.Refresh();
                    await UpdateGrid.RefreshColumnsAsync();
                }

                await UploadObj.ClearAllAsync();

                await jSRuntime.InvokeAsync<object>("HideModal", "#AddTaskUpdate");

                if (CloseModal)
                {
                    await jSRuntime.InvokeAsync<object>("HideModal", "#TaskUpdatesModal");
                }

                tasksTaskUpdate = new TasksTaskUpdatesViewModel() { UpdateDate = DateTime.Today };

                _toastService.ShowSuccess("Update Has Been Added!");
                StateHasChanged();
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    StateHasChanged();
                    _toastService.ShowError(ex.InnerException.Message);
                }
                else
                {
                    StateHasChanged();
                    _toastService.ShowError(ex.Message);
                }
                await jSRuntime.InvokeAsync<object>("HideModal", "#AddTaskUpdate");
            }
        }

        protected async Task CheckOneTimeUpdDate()
        {
            if (tasksTaskUpdate.UpdateDate < DateTime.Today)
            {
                await jSRuntime.InvokeVoidAsync("AddRedBox", "OneTimeUpdDate");
            }
            else
            {
                await jSRuntime.InvokeVoidAsync("RemoveRedBox", "OneTimeUpdDate");
            }

            StateHasChanged();
        }

        protected async Task CheckOneTimeNote()
        {
            if (string.IsNullOrEmpty(tasksTaskUpdate.Update))
            {
                await jSRuntime.InvokeVoidAsync("AddRedBox", "OneTUpdNoteId");
            }
            else
            {
                await jSRuntime.InvokeVoidAsync("RemoveRedBox", "OneTUpdNoteId");
            }

            StateHasChanged();
        }

        protected void SelectImage(UploadChangeEventArgs args)
        {
            UploadFile = args.Files.FirstOrDefault();
        }

        protected async Task SendUpdateEmail(int taskId)
        {
            var task = await taskService.GetTaskById(taskId);

            string body = "<label style=\"font-weight:bold\"> Task ID: </label>" + task.Id +
                           "<br><label style=\"font-weight:bold\"> Description: </label>" + task.Description +
                           "<br><label style=\"font-weight:bold\"> Task Type: </label>" + task.TaskType +
                           "<br><label style=\"font-weight:bold\"> Priority: </label>" + task.Priority +
                           "<br><label style=\"font-weight:bold\"> Update: </label>" + tasksTaskUpdate.Update;

            string Subject = "ONE-TIME TASK HAS BEEN UPDATED";

            body = EmailDefaults.GenerateEmailTemplate("Tasks", body);
            await emailService.SendEmailAsync(EmailTypes.ActionBasedOneTimeTaskUpdated,
                Array.Empty<string>(), Subject, body, string.Empty, new string[] { "rarnold@camcomfginc.com;" });
        }

        public async Task ViewTaskUpdates(TasksTasksViewModel args)
        {
            var updatesModel = await taskService.GetTaskUpdates(args.Id);

            UpdatesTask = args;
            UpdatesTask.TasksTaskUpdates = (await taskService.GetTaskUpdates1(args.Id)).OrderByDescending(x => x.UpdateDate.Date).ToList();
            var TempTaskImages = await taskService.GetOneTimeTaskImagesCountAsync(UpdatesTask.Id);
            if (TempTaskImages != null)
            {
                UpdatesTask.ImagesCount = TempTaskImages.Where(x => IsImage(x.PictureLink)).Count();
                UpdatesTask.FilesCount = TempTaskImages.Where(x => IsFile(x.PictureLink)).Count();
            }
            if (UpdateGrid != null)
            {
                await UpdateGrid.Refresh();
                await UpdateGrid.RefreshColumnsAsync();
            }
            StateHasChanged();
        }

        protected void LoadEmployeeData(SelectEventArgs<TasksTasksTaskTypeViewModel> args)
        {
            if (args.ItemData != null)
            {
                SelectedTaskTypeList = MainTasksViewModel.Where(x => x.TaskType == args.ItemData.TaskType).ToList();
            }
        }
    }
}