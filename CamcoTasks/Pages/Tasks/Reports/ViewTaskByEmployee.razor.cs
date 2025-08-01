using Blazored.Toast.Services;
using CamcoTasks.Data.Services;
using CamcoTasks.Service.IService;
using CamcoTasks.Infrastructure.Common.Email;
using CamcoTasks.ViewModels.TasksImagesDTO;
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
using DocumentFormat.OpenXml.Wordprocessing;
using CamcoTasks.Infrastructure.Defaults;

namespace CamcoTasks.Pages.Tasks
{
    public class ViewTaskByEmployeeModel : ComponentBase
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

        protected SfGrid<TasksTaskUpdatesViewModel> UpdateGrid { get; set; }
        protected string RespondBody { get; set; } = "";
        protected SfGrid<TasksTasksViewModel> TasksGrid { get; set; }
        public List<TasksTasksTaskTypeViewModel> TaskEnum { get; set; }
        public List<TasksTasksViewModel> MainTasksViewModel { get; set; }
        public List<TasksTasksViewModel> SelectedTaskTypeList { get; set; }
        public TasksTasksViewModel SelectedTask { get; set; } = new TasksTasksViewModel();
        protected SfComboBox<int?, TasksTasksTaskTypeViewModel> SfcomboBox { get; set; }
        protected TasksTaskUpdatesViewModel UpdateEditContext { get; set; } = new TasksTaskUpdatesViewModel();
        public TasksTasksViewModel UpdatesTask { get; set; } = new TasksTasksViewModel();
        public TasksTaskUpdatesViewModel DeleteViewModelUpdate { get; set; } = new TasksTaskUpdatesViewModel();
        protected TasksTaskUpdatesViewModel SelectedUpdateViewModel { get; set; } = new TasksTaskUpdatesViewModel();
        public TasksTaskUpdatesViewModel tasksTaskUpdate { get; set; } = new TasksTaskUpdatesViewModel();
        protected SfTextBox EmailNoteRef { get; set; }
        protected SfGrid<TasksTasksViewModel> TaskGrid { get; set; }
        protected string TaskImage { get; set; }
        public List<TasksTasksViewModel> Tasks { get; set; }
        protected int? SelectedTypeValue { get; set; } = 0;
        public bool IsFile(string file) => Regex.IsMatch(file, FILEFORMATS);
        public bool IsImage(string file) => Regex.IsMatch(file, IMAGEFORMATS);
        protected bool IsInnerUpdate { get; set; } = false;
        protected string ErrorDateMessage { get; set; } = "";
        protected string ErrorUpdateMessage { get; set; } = "";
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
        public TasksTasksViewModel NewTask { get; set; } = new TasksTasksViewModel() { DateAdded = DateTime.Now };
        public TasksTasksTaskTypeViewModel NewTaskType { get; set; } = new TasksTasksTaskTypeViewModel();
        protected string RespondBody2 { get; set; } = "";

        protected bool IsRenderingImages = false;
        protected List<TasksImagesViewModel> SelectedTaskImages { get; set; } = new List<TasksImagesViewModel>();

        protected Dictionary<string, object> htmlAttribute = new Dictionary<string, object>() { { "rows", "4" } };

        protected Dictionary<string, object> htmlAttributeBig = new Dictionary<string, object>() { { "rows", "7" }, { "spellcheck", "true" } };

        protected override async Task OnInitializedAsync()
        {
            TaskEnum = (await taskService.GetTaskTypes()).ToList();
            SelectedTypeValue = TaskEnum.FirstOrDefault()?.Id;
            StateHasChanged();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                MainTasksViewModel = (await taskService.GetAllTasks()).ToList();

                if (!string.IsNullOrEmpty(TaskType))
                    SelectedTypeValue = TaskEnum.FirstOrDefault(x => x.TaskType == TaskType)?.Id;

                if (SelectedTypeValue == 0)
                {
                    if (TaskEnum.Any())
                    {
                        SelectedTypeValue = TaskEnum.First().Id;
                        await LoadEmployeeData(new SelectEventArgs<TasksTasksTaskTypeViewModel>()
                        {
                            ItemData = new TasksTasksTaskTypeViewModel { TaskType = TaskEnum.First().TaskType }
                        });
                    }
                }
                else
                {
                    await LoadEmployeeData(new SelectEventArgs<TasksTasksTaskTypeViewModel>()
                    {
                        ItemData = new TasksTasksTaskTypeViewModel { TaskType = TaskEnum.FirstOrDefault(x => x.Id == SelectedTypeValue).TaskType }
                    });
                }
            }

            StateHasChanged();
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

        protected async Task CloseOTTUpdateModal()
        {
            await jSRuntime.InvokeAsync<object>("HideModal", "#TaskUpdatesModal");
        }
        protected async Task CloseOTTAddTaskUpdate()
        {
            await jSRuntime.InvokeAsync<object>("HideModal", "#AddTaskUpdate");
        }
        protected async Task CloseImageModal()
        {
            await jSRuntime.InvokeAsync<object>("HideModal", "#ImageModal");
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
                await jSRuntime.InvokeAsync<object>("HideModal", "#AddTaskUpdate");
                await SendUpdateEmail(UpdatesTask.Id);

                if (IsTaskDone)
                {
                    IsTaskDone = false;
                    if (UpdatesTask.DateCompleted == null)
                        UpdatesTask.DateCompleted = DateTime.Now;

                    var result2 = await taskService.UpdateTask(UpdatesTask, UpdatesTask.Priority);

                    SelectedTaskTypeList.RemoveAt(SelectedTaskTypeList.FindIndex(x => x.Id == UpdatesTask.Id));
                    mainTasksModel.RemoveAt(mainTasksModel.FindIndex(x => x.Id == UpdatesTask.Id));
                }

                UploadFile = new UploadFiles();
                IsTaskDone = false;

                SelectedTaskTypeList = SelectedTaskTypeList.OrderBy(x => x.Priority).ToList();

                if (UpdateGrid != null)
                {
                    UpdatesTask.TasksTaskUpdates.Add(tasksTaskUpdate);
                    UpdatesTask.TasksTaskUpdates = UpdatesTask.TasksTaskUpdates.OrderByDescending(x => x.UpdateDate.Date).ToList();
                    await UpdateGrid.Refresh();
                    await UpdateGrid.RefreshColumnsAsync();
                }

                await UploadObj.ClearAllAsync();

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
                Array.Empty<string>(), Subject, body, string.Empty, new string[] { "rarnold@camcomfginc.com" });
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

        protected async Task LoadEmployeeData(SelectEventArgs<TasksTasksTaskTypeViewModel> args)
        {
            if (args.ItemData != null)
            {
                SelectedTaskTypeList = MainTasksViewModel.Where(x => x.TaskType == args.ItemData.TaskType).ToList();
                await TasksGrid.RefreshColumnsAsync();
            }
        }
    }
}
