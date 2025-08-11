using BlazorDownloadFile;
using Blazored.Toast.Services;
using CamcoTasks.Infrastructure.Common.Email;
using CamcoTasks.Data.Services;
using CamcoTasks.Service.IService;
using CamcoTasks.ViewModels.TasksImagesDTO;
using CamcoTasks.ViewModels.TasksTasksDTO;
using CamcoTasks.ViewModels.TasksTasksTaskTypeDTO;
using CamcoTasks.ViewModels.TasksTaskUpdatesDTO;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Syncfusion.Blazor.Data;
using Syncfusion.Blazor.DropDowns;
using Syncfusion.Blazor.Grids;
using Syncfusion.Blazor.Inputs;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using static iTextSharp.text.pdf.PRTokeniser;
using FileInfo = Syncfusion.Blazor.Inputs.FileInfo;
using CamcoTasks.Infrastructure.Defaults;

namespace CamcoTasks.Pages.Tasks
{
    public partial class ViewOneTimeSubTasks : ComponentBase
    {
        private const string IMAGEFORMATS = @".jpg|.png|.gif|.jpeg|.bmp|.svg|.jfif|.apng|.ico$";
        private const string FILEFORMATS = @".pdf|.xlsb|.txt|.pptx|.zip|.rar|.pdf|.xlsx|.xls|.csv|.xlsb|.pptx|.docx$";

        [Inject]
        private NavigationManager NavigationManager { get; set; }

        [Inject]
        private IEmailService emailService { get; set; }

        [Inject]
        private IWebHostEnvironment WebHostEnvironment { get; set; }

        [Inject]
        private IJSRuntime jSRuntime { get; set; }

        [Inject]
        private IBlazorDownloadFileService blazorDownloadFileService { get; set; }

        [Inject]
        private FileManagerService fileManagerService { get; set; }

        [Inject]
        private IToastService _toastService { get; set; }

        [Inject]
        protected ITasksService taskService { get; set; }

        [Inject]
        protected IEmployeeService employeeService { get; set; }

        [Inject]
        protected NavigationManager navigationManager { get; set; }
        [Inject] protected IDepartmentService departmentService { get; set; }

        [Parameter]
        public string TaskId { get; set; }

        public int TaskCount { get; set; } = 0;

        protected TasksTasksViewModel MainTaskViewModel = new TasksTasksViewModel();
        public List<string> Employees { get; set; }
        public List<DDData> TaskTypes { get; set; } = new List<DDData>();
        public List<string> TaskTypesAll { get; set; } = new List<string>();

        protected string[] PageSizes { get; set; } = new string[] { "15", "25", "35", "50" };

        protected string StatusDropdownVal { get; set; } = "2";
        protected string TaskImage { get; set; }
        protected string OldTaskFile { get; set; }
        protected string ErrorDateMessage { get; set; } = "";
        protected string ErrorUpdateMessage { get; set; } = "";
        protected string TaskTypeValue { get; set; } = "";

        protected SfTextBox SimpleEmailNoteRef { get; set; }

        protected SfGrid<TasksTasksViewModel> TaskGrid { get; set; }
        protected SfGrid<TasksTaskUpdatesViewModel> UpdateGrid { get; set; }

        protected UploadFiles UploadFile { get; set; } = new UploadFiles();
        protected SfUploader TaskUploadRef { get; set; }
        protected SfUploader UploadObj { get; set; }
        protected SfUploader UploadObjTask { get; set; }


        protected SfComboBox<string, string> TaskeTypeCombo { get; set; }

        protected TasksTaskUpdatesViewModel UpdateEditContext { get; set; } = new TasksTaskUpdatesViewModel();
        protected TasksTaskUpdatesViewModel TaskFile { get; set; }

        private static List<TasksTasksViewModel> mainTasksModel { get; set; } = new List<TasksTasksViewModel>();
        public List<TasksTasksViewModel> Tasks { get; set; }
        protected List<UploadFiles> TaskUploadFiles { get; set; } = new();
        protected List<TasksImagesViewModel> SelectedTaskImages { get; set; } = new List<TasksImagesViewModel>();
        protected List<TasksImagesViewModel> SelectedTaskFiles { get; set; } = new List<TasksImagesViewModel>();
        protected List<string> DepartmentsViewModel { get; set; } = new List<string>();

        protected Dictionary<string, object> htmlAttribute = new Dictionary<string, object>() { { "rows", "4" } };

        protected bool IsInnerUpdate { get; set; } = false;
        public TasksTasksViewModel SelectedTask { get; set; } = new TasksTasksViewModel();
        public TasksTasksViewModel UpdatesTask { get; set; } = new TasksTasksViewModel();
        public TasksTasksViewModel OldTask { get; set; } = new TasksTasksViewModel();
        public TasksTasksViewModel NewTask { get; set; } = new TasksTasksViewModel() { DateAdded = DateTime.Now };
        public TasksTaskUpdatesViewModel tasksTaskUpdate { get; set; } = new TasksTaskUpdatesViewModel();
        public TasksTaskUpdatesViewModel DeleteViewModelUpdate { get; set; } = new TasksTaskUpdatesViewModel();
        public TasksTaskUpdatesViewModel NewUpdate { get; set; } = new TasksTaskUpdatesViewModel() { UpdateDate = DateTime.Today };
        public TasksTasksTaskTypeViewModel NewTaskType { get; set; } = new TasksTasksTaskTypeViewModel();

        public int? tempPriorityValue { get; set; }
        public int TaskTypeValues { get; set; } = 1;

        public bool IsSpinner { get; set; } = true;
        public bool IsTaskDone { get; set; } = false;

        public List<int> Progresses { get; set; } = new List<int>() { 0, 10, 20, 30, 40, 50, 60, 70, 80, 90, 100 };

        public string Fwidth { get; set; } = "455";
        public string LWidth { get; set; } = "330";
        public string ColWidth { get; set; } = "455";

        public class DDData
        {
            public string Text { get; set; }
            public string Value { get; set; }
        }

        public List<DDData> DLData { get; set; } = new List<DDData>() {
            new DDData() { Text = "Show Incomplete", Value = "2" },
            new DDData() { Text = "Show Completed", Value = "3" } };


        protected override async Task OnInitializedAsync()
        {
            await LoadData();

            IsSpinner = false;
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                System.Threading.Thread.Sleep(2500);
                await jSRuntime.InvokeAsync<object>("modalDraggable");
            }
        }

        protected async Task LoadData()
        {
            if (string.IsNullOrEmpty(TaskId) || !int.TryParse(TaskId, out int value))
                return;

            try
            {
                MainTaskViewModel = await taskService.GetTaskById(Convert.ToInt32(TaskId));

                SelectedTask.TasksTaskUpdates = new List<TasksTaskUpdatesViewModel>();
                mainTasksModel = (await taskService.GetAllTasks3(TaskId)).ToList();

                foreach (var TTask in mainTasksModel)
                {
                    var allUpdates = await taskService.GetTaskUpdates1(TTask.Id);

                    if (allUpdates != null && allUpdates.Any())
                    {
                        var tempUpdate = allUpdates.Max(x => x.UpdateDate.Date);
                        TTask.LatestUpdate = tempUpdate;
                    }
                }
                Tasks = mainTasksModel.Where(a => !a.DateCompleted.HasValue).ToList();
                Tasks = Tasks.OrderBy(x => x.Priority).ToList();
                TaskCount = Tasks.Count;

                await LoadPageDetails();
            }
            catch (Exception ex)
            {
                _toastService.ShowError(ex.Message);
            }
        }

        protected async Task FilterTaskType(Syncfusion.Blazor.DropDowns.ChangeEventArgs<string, DDData> args)
        {
            await TaskGrid.ClearFiltering();
            if (args.ItemData.Text != "ALL")
            {
                await TaskGrid.FilterByColumn(nameof(TasksTasksViewModel.TaskType),
                           "equal", args.ItemData, "or");
            }
        }

        public async Task Changedata(Syncfusion.Blazor.DropDowns.ChangeEventArgs<string, DDData> args)
        {
            StatusDropdownVal = args.Value;

            if (StatusDropdownVal == "2")
            {
                ColWidth = Fwidth;
                Tasks = mainTasksModel.Where(a => !a.DateCompleted.HasValue).ToList();
            }
            else if (StatusDropdownVal == "3")
            {
                ColWidth = LWidth;
                Tasks = mainTasksModel.Where(a => a.DateCompleted.HasValue).ToList();
            }
            else
            {
                ColWidth = Fwidth;
                Tasks = mainTasksModel;
            }

            Tasks = Tasks.OrderBy(x => x.Priority).ToList();
            await Task.Delay(5);
        }
        protected void ViewTypes()
        {
            navigationManager.NavigateTo($"/viewtasktypes/");
        }

        protected async Task FilterFields(int DepNum)
        {
            switch (DepNum)
            {
                case 0:
                    {
                        if (StatusDropdownVal == "2")
                        {
                            Tasks = mainTasksModel.Where(a => !a.DateCompleted.HasValue).ToList();
                        }
                        else if (StatusDropdownVal == "3")
                        {
                            Tasks = mainTasksModel.Where(a => a.DateCompleted.HasValue).ToList();
                        }
                        else
                        {
                            Tasks = mainTasksModel;
                        }

                        await TaskGrid.RefreshColumnsAsync();
                        break;
                    }
                case 1:
                    {
                        if (StatusDropdownVal == "2")
                        {
                            Tasks = mainTasksModel.Where(a => !a.DateCompleted.HasValue && !string.IsNullOrEmpty(a.Department) &&
                            a.Department.Contains("LATHE")).ToList();
                        }
                        else if (StatusDropdownVal == "3")
                        {
                            Tasks = mainTasksModel.Where(a => a.DateCompleted.HasValue && !string.IsNullOrEmpty(a.Department) && a.Department.Contains("LATHE")).ToList();
                        }
                        else
                        {
                            Tasks = mainTasksModel.Where(a => !string.IsNullOrEmpty(a.Department) && a.Department.Contains("LATHE")).ToList();
                        }

                        await TaskGrid.RefreshColumnsAsync();
                        break;

                    }
                case 2:
                    {
                        if (StatusDropdownVal == "2")
                        {
                            Tasks = mainTasksModel.Where(a => !a.DateCompleted.HasValue && !string.IsNullOrEmpty(a.Department) && a.Department.Contains("MILL")).ToList();
                        }
                        else if (StatusDropdownVal == "3")
                        {
                            Tasks = mainTasksModel.Where(a => a.DateCompleted.HasValue && !string.IsNullOrEmpty(a.Department) && a.Department.Contains("MILL")).ToList();
                        }
                        else
                        {
                            Tasks = mainTasksModel.Where(a => !string.IsNullOrEmpty(a.Department) && a.Department.Contains("MILL")).ToList();
                        }

                        await TaskGrid.RefreshColumnsAsync();
                        break;
                    }
                case 3:
                    {
                        if (StatusDropdownVal == "2")
                        {
                            Tasks = mainTasksModel.Where(a => !a.DateCompleted.HasValue && !string.IsNullOrEmpty(a.Department) && a.Department.Contains("QUALITY")).ToList();
                        }
                        else if (StatusDropdownVal == "3")
                        {
                            Tasks = mainTasksModel.Where(a => a.DateCompleted.HasValue && !string.IsNullOrEmpty(a.Department) && a.Department.Contains("QUALITY")).ToList();
                        }
                        else
                        {
                            Tasks = mainTasksModel.Where(a => !string.IsNullOrEmpty(a.Department) && a.Department.Contains("QUALITY")).ToList();
                        }

                        await TaskGrid.RefreshColumnsAsync();
                        break;
                    }
                default:
                    break;
            }
        }

        protected async Task RefreshUpdateModal(bool IsInner)
        {
            IsInnerUpdate = IsInner;
            tasksTaskUpdate = new TasksTaskUpdatesViewModel() { UpdateDate = DateTime.Today };
            await Task.Delay(1);
        }
        protected async Task ClearImage(TasksTasksViewModel tasksTask)
        {
            try
            {
                tasksTask.PictureLink = string.Empty;
                await taskService.UpdateOneTask(tasksTask);
                _toastService.ShowSuccess("Image Cleared");
            }
            catch
            {

            }
        }
        protected void StartDelete(TasksTasksViewModel task)
        {
            SelectedTask = task;
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

                await TaskGrid.RefreshColumns();
                TaskGrid.Refresh();

                await SetNewDefaults();

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
            }
        }

        protected TasksTaskUpdatesViewModel SelectedUpdateViewModel { get; set; } = new TasksTaskUpdatesViewModel();
        protected SfTextBox EmailNoteRef { get; set; }
        protected string RespondBody { get; set; } = "";
        protected string RespondBody2 { get; set; } = "";
        protected async Task StartRespond(TasksTaskUpdatesViewModel task)
        {
            SelectedUpdateViewModel = task;
            await Task.Delay(200);
            await EmailNoteRef.FocusIn();
        }

        protected async Task CheckOneTaskEditNote()
        {
            if (string.IsNullOrEmpty(RespondBody2))
            {
                await jSRuntime.InvokeVoidAsync("AddRedBox", "OneTaskEditNoteId");
            }
            else
            {
                await jSRuntime.InvokeVoidAsync("RemoveRedBox", "OneTaskEditNoteId");
            }
        }
        protected async Task SendRespond()
        {
            if (string.IsNullOrEmpty(RespondBody2))
            {
                _toastService.ShowError("Please Enter a Note");
                await jSRuntime.InvokeVoidAsync("AddRedBox", "OneTaskEditNoteId");
                return;
            }
            else
            {
                await jSRuntime.InvokeVoidAsync("RemoveRedBox", "OneTaskEditNoteId");
            }

            var Task = await taskService.GetTaskById((int)SelectedUpdateViewModel.TaskID);

            var taskType = await taskService.GetTaskType(Task.TaskType);

            if (taskType == null)
            {
                _toastService.ShowError("Task Type not found Error, can't send email.");
                return;
            }

            if (string.IsNullOrEmpty(taskType.Email) && string.IsNullOrEmpty(taskType.Email2))
            {
                _toastService.ShowError("No Email found for task type.");
                return;
            }

            string body = "<br><h2 style=\"text-decoration: underline; font-weight:bold; margin-bottom:5px;\">" +
                "One-Time Task Description: </h2>"
                + "<h4 style=\"margin-top: 9px; margin-left: 5px;\">" + Task.Description + "</h4>" +
                "<br><h2 style=\"text-decoration: underline; font-weight:bold; margin-bottom:5px;\">" +
                "Rich Arnold Response: </h2>" + "<h4 style=\"margin-top: 9px; margin-left: 5px;\">" + RespondBody2 + "</h4>";

            //if (!string.IsNullOrEmpty(SelectedUpdateViewwModel.PictureLink))
            //    body += "<br><h3 style=\"font-weight:bold\"Image: </h3>" + SelectedUpdateViewwModel.PictureLink;

            if (!string.IsNullOrEmpty(SelectedUpdateViewModel.FileLink))
                body += "<br><label style=\"font-weight:bold\"> File: </label>" + SelectedUpdateViewModel.FileLink;


            string Subject = "RICH ARNOLD SENT A RESPONSE";

            body = EmailDefaults.GenerateEmailTemplate("Tasks", body);
            await emailService.SendEmailAsync(EmailTypes.ActionBasedResponseForOneTimeTask,
                Array.Empty<string>(), Subject, body, string.Empty, new string[] { string.IsNullOrEmpty(taskType.Email) ? taskType.Email2 : taskType.Email });

            _toastService.ShowInfo("An Email Has Been Added To Queue");

            await jSRuntime.InvokeAsync<object>("HideModal", "#RespondModal");

            RespondBody = string.Empty;
            SelectedUpdateViewModel = new TasksTaskUpdatesViewModel();
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
        }
        public async Task AddNewUpdate()
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
                    UpdateGrid.Refresh();
                    await UpdateGrid.RefreshColumns();
                }

                await TaskUploadRef.ClearAllAsync();

                await jSRuntime.InvokeAsync<object>("HideModal", "#AddTaskUpdate");

                tasksTaskUpdate = new TasksTaskUpdatesViewModel() { UpdateDate = DateTime.Today };

                _toastService.ShowSuccess("Update Has Been Added!");
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
            CleaneUpdateTaskFiles();
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
        protected void SelectImage(UploadChangeEventArgs args)
        {
            UploadFile = args.Files.FirstOrDefault();
        }

        protected List<UploadFiles> TaskUploadFile { get; set; } = new();
        protected void SelectTaskImage(UploadChangeEventArgs args)
        {
            TaskUploadFile = args.Files;
        }

        protected void UploadUpdateImage(UploadChangeEventArgs args)
        {
            UploadFile = args.Files.FirstOrDefault();
        }
        protected void GetImage(TasksTaskUpdatesViewModel tasksTaskUpdates)
        {
            UpdateEditContext = tasksTaskUpdates;
        }

        protected string PreviewImage = string.Empty;

        protected Dictionary<string, string> PreviewImages { get; set; } = new();
        protected Dictionary<string, Syncfusion.Blazor.Inputs.FileInfo> PreviewImagesList { get; set; } = new();
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

        protected Dictionary<string, string> PreviewFiles { get; set; } = new();
        protected Dictionary<string, Syncfusion.Blazor.Inputs.FileInfo> PreviewFilesList { get; set; } = new();
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
        }

        public bool IsFile(string file) => Regex.IsMatch(file, FILEFORMATS);
        public bool IsImage(string file) => Regex.IsMatch(file, IMAGEFORMATS);
        public bool IsValidSize(double length) => length / 1000000 <= 20;
        protected void DeleteUpdateConfirm(TasksTasksViewModel model, TasksTaskUpdatesViewModel tasksTaskUpdates)
        {
            SelectedTask = model;
            DeleteViewModelUpdate = tasksTaskUpdates;
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
                    UpdateGrid.Refresh();
                    await UpdateGrid.RefreshColumns();
                }

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

        protected async Task SendEmail(TasksTasksViewModel task, bool Nudeged, string RespondBody = null)
        {
            var tasktype = await taskService.GetTaskType(task.TaskType);

            if (tasktype != null)
            {
                var TEmail = string.IsNullOrEmpty(tasktype.Email) ?
                (string.IsNullOrEmpty(tasktype.Email2) ? null : tasktype.Email2) : tasktype.Email;

                if (string.IsNullOrEmpty(TEmail))
                {
                    _toastService.ShowWarning("Email Sending Error, Employee Has No Email");
                    return;
                }
                string body = "";

                if (!string.IsNullOrEmpty(RespondBody))
                    body = "Note: " + RespondBody + "<br>";

                body += "<label style=\"font-weight:bold\">Description: </label>" + task.Description +
               "<br><label style=\"font-weight:bold\">Date Added: </label>" + task.DateAdded?.Date.ToString("MM/dd/yyyy") +
               "<br><label style=\"font-weight:bold\">Priority: </label>" + task.Priority +
               "<br><label style=\"font-weight:bold\">Link To Mark Completed:</label> http://metrics.db.camco.local/viewtasks/";

                string Subject = "FRIENDLY REMINDER TO GET THIS TASK DONE";


                // EmailQueueViewModel emailqueue = new()
                // {
                //     Body = body,
                //     HasBeenSent = false,
                //     SendTo = TEmail,
                //     Subject = Subject,
                //     EmailTypeId = 723
                // };

                body = EmailDefaults.GenerateEmailTemplate("Tasks", body);
                await emailService.SendEmailAsync(EmailTypes.ActionBasedFriendlyReminderOneTimeTask,
                Array.Empty<string>(), Subject, body, string.Empty, new string[] { TEmail });

                if (Nudeged)
                {
                    task.NudgeCount++;
                    await taskService.UpdateOneTask(task);
                }

                _toastService.ShowInfo("An Email Has Been Added To Queue");
            }
            else
            {
                _toastService.ShowError("Task Type Couldn't be Found");
                return;
            }
        }

        public async Task ViewTaskUpdates(TasksTasksViewModel args)
        {
            var updatesModel = await taskService.GetTaskUpdates(args.Id);

            UpdatesTask = args;
            UpdatesTask.TasksTaskUpdates = updatesModel.OrderByDescending(x => x.UpdateDate.Date).ToList();
            var TempTaskImages = await taskService.GetOneTimeTaskImagesCountAsync(UpdatesTask.Id);
            if (TempTaskImages != null)
            {
                UpdatesTask.ImagesCount = TempTaskImages.Where(x => IsImage(x.PictureLink)).Count();
                UpdatesTask.FilesCount = TempTaskImages.Where(x => IsFile(x.PictureLink)).Count();
            }
            if (UpdateGrid != null)
            {
                UpdateGrid.Refresh();
                await UpdateGrid.RefreshColumns();
            }
        }

        TasksTasksViewModel EmailTask { get; set; } = new TasksTasksViewModel();
        protected async Task StartEmail(TasksTasksViewModel task)
        {
            RespondBody = "";
            EmailTask = task;
            await jSRuntime.InvokeAsync<object>("ShowModal", "#SimpleEmailModal");
            await Task.Delay(200);
            await SimpleEmailNoteRef.FocusIn();
        }

        protected async Task CheckOneTimeTaskSendMail()
        {
            if (string.IsNullOrEmpty(RespondBody))
            {
                await jSRuntime.InvokeVoidAsync("AddRedBox", "OneTimeTaskSendMailId");
            }
            else
            {
                await jSRuntime.InvokeVoidAsync("RemoveRedBox", "OneTimeTaskSendMailId");
            }
        }
        protected async Task SendTaskEmail()
        {
            if (string.IsNullOrEmpty(RespondBody))
            {
                _toastService.ShowError("Please Enter a Note");
                await jSRuntime.InvokeVoidAsync("AddRedBox", "OneTimeTaskSendMailId");
                return;
            }

            var employee = await taskService.GetTaskType(EmailTask.TaskType);

            if (employee != null)
            {
                if (employee.Email != null)
                {
                    string body = "<br><h2 style=\"text-decoration: underline; font-weight:bold; margin-bottom:5px;\">" +
                "Recurring Task Description: </h2>"
                + "<h4 style=\"margin-top: 9px; margin-left: 5px;\">" + EmailTask.Description + "</h4>" +
                "<br><h2 style=\"text-decoration: underline; font-weight:bold; margin-bottom:5px;\">" +
                "Rich Arnold Says Regarding This Task: </h2>" + "<h4 style=\"margin-top: 9px; margin-left: 5px;\">"
                + RespondBody + "</h4>"

                + "<br><br><label style=\"font-weight:bold; font-size: 20px;\">Link To One-Time Tasks:</label> " +
                   $"<a href=\"http://metrics.db.camco.local/viewtasks\" target=\"_blank\"> "
                   + $" Recurring Task #{EmailTask.Id}</a>";

                    string Subject = "RICH ARNOLD SENT A RESPONSE";
                    if (!string.IsNullOrEmpty(employee.Email) || !string.IsNullOrEmpty(employee.Email))
                    {
                        // EmailQueueViewModel emailqueue = new()
                        // {
                        //     Body = body,
                        //     HasBeenSent = false,
                        //     SendTo = !string.IsNullOrEmpty(employee.Email) ? employee.Email : employee.Email2,
                        //     Subject = Subject,
                        //     EmailTypeId = 723
                        // };

                        body = EmailDefaults.GenerateEmailTemplate("Tasks", body);
                        await emailService.SendEmailAsync(EmailTypes.ActionBasedResponseForOneTimeTask,
                        Array.Empty<string>(), Subject, body, string.Empty, new string[] { !string.IsNullOrEmpty(employee.Email) ? employee.Email : employee.Email2 });

                        _toastService.ShowInfo("An Email Has Been Added To Queue");

                        if (EmailTask.EmailCount == null) EmailTask.EmailCount = 0;
                        EmailTask.EmailCount++;
                        await taskService.UpdateOneTask(EmailTask);

                        await jSRuntime.InvokeAsync<object>("HideModal", "#SimpleEmailModal");

                        RespondBody = string.Empty;
                        EmailTask = new TasksTasksViewModel();
                    }
                }
                else _toastService.ShowWarning("Task Type Has No Email!");
            }
            else
            {
                await jSRuntime.InvokeAsync<object>("HideModal", "#SimpleEmailModal");
                _toastService.ShowWarning("Task Type Has No Employee!");
            }
        }

        protected void ViewImage(string ModelPicture)
        {
            TaskImage = ConvertImagetoBase64(ModelPicture);
        }

        protected void ViewFile(TasksTasksViewModel model)
        {
            if (model != null) OldTaskFile = model.FileLink;
        }

        public async Task ToolbarClickHandler(Syncfusion.Blazor.Navigations.ClickEventArgs args)
        {
            if (args.Item.Text == "Excel Export")
            {
                ExcelExportProperties ExcelProperties = new();
                ExcelProperties.FileName = "Metrics Tasks - " + DateTime.Today.ToString("MM/dd/yyyy") + ".xlsx";

                await TaskGrid.ExcelExport(ExcelProperties);
            }
            if (args.Item.Text == "PDF Export")
            {
                PdfExportProperties ExportProperties = new();
                ExportProperties.FileName = "Metrics Tasks - " + DateTime.Today.ToString("MM/dd/yyyy") + ".pdf";
                ExportProperties.DisableAutoFitWidth = true;
                ExportProperties.AllowHorizontalOverflow = true;


                ExportProperties.Columns = new List<GridColumn>()
                {
                new GridColumn(){ Field=nameof(TasksTasksViewModel.Id), HeaderText="ID", TextAlign=TextAlign.Left, Width="40"},
                new GridColumn(){ Field=nameof(TasksTasksViewModel.TaskType), HeaderText=" Task Type", TextAlign=TextAlign.Left, Width="75"},
                new GridColumn(){ Field=nameof(TasksTasksViewModel.Description), HeaderText="Description", TextAlign=TextAlign.Left, Width="290"},
                new GridColumn(){ Field=nameof(TasksTasksViewModel.DateAdded), HeaderText="Date Added", TextAlign=TextAlign.Left, Width="60" , Format="MM/dd/yyyy"},
                new GridColumn(){ Field=nameof(TasksTasksViewModel.LatestUpdate), HeaderText=" Latest Update", TextAlign=TextAlign.Left, Width="60", Format="MM/dd/yyyy"}
                };

                List<PdfHeaderFooterContent> HeaderContent = new()
                {
                    new PdfHeaderFooterContent()
                    {
                        Type = ContentType.Text,
                        Value = "Metrics One-Time Tasks - " + DateTime.Today.ToString("MM/dd/yyyy"),
                        Position = new PdfPosition() { X = 0, Y = 0 },
                        Style = new PdfContentStyle() { TextBrushColor = "#1C4084", FontSize = 24, DashStyle = PdfDashStyle.Solid }
                    }
                };

                PdfHeader Header = new()
                {
                    FromTop = 0,
                    Height = 50,
                    Contents = HeaderContent,
                };
                ExportProperties.Header = Header;

                await TaskGrid.PdfExport(ExportProperties);
            }
            if (args.Item.Text == "CSV Export")
            {
                ExcelExportProperties ExcelProperties = new ExcelExportProperties();
                ExcelProperties.FileName = "Metrics Tasks - " + DateTime.Today.ToString("MM/dd/yyyy") + ".csv";

                await TaskGrid.CsvExport(ExcelProperties);
            }
        }
        protected async Task LoadPageDetails()
        {
            var taskTypes = await taskService.GetTaskTypes();

            var _employees = (await employeeService.GetListAsync(true));
            Employees = _employees.Select(a => a.FullName).OrderBy(a => a).ToList();

            TaskTypesAll = taskTypes.Select(a => a.TaskType).OrderBy(a => a).ToList();

            TaskTypeValues = 1;
            TaskTypes = TaskTypesAll.Select(a => new DDData { Text = a, Value = TaskTypeValues++.ToString() }).OrderBy(a => a.Text).ToList();
            TaskTypes.Insert(0, new DDData { Text = "ALL", Value = "0" });

            var departmentList = await departmentService.GetListAsync();
            DepartmentsViewModel = departmentList.Select(x => x.Name).OrderBy(x => x).ToList();

            await SetNewDefaults();
        }
        public async Task DetailDataBoundHandler(DetailDataBoundEventArgs<TasksTasksViewModel> args)
        {
            var updatesModel = await taskService.GetTaskUpdates(args.Data.Id);

            args.Data.TasksTaskUpdates = updatesModel.ToList();

            if (UpdateGrid != null)
            {
                UpdateGrid.Refresh();
                await UpdateGrid.RefreshColumns();
            }

        }
        protected void ViewUpdateImage(TasksTaskUpdatesViewModel model)
        {
            if (IsFile(model.PictureLink)) TaskFile = model;
            else TaskImage = ConvertImagetoBase64(model.PictureLink);

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
        }

        protected async Task DeleteTaskFile()
        {
            SelectedTaskFiles.Remove(SelectedTMV);
            await taskService.DeleteTaskImageAsync(SelectedTMV);
        }

        protected void StartDeleteTaskPreImage(string TMV)
        {
            SelectedPreTMV = TMV;
        }

        protected async Task TaskPreImageRemove(string index)
        {
            var File = TaskUploadFiles[TaskUploadFiles.FindIndex(x => x.FileInfo?.Id == index)].FileInfo;
            var RemoveList = new FileInfo[1] { File };
            await UploadObj.RemoveAsync(RemoveList);
            PreviewImages.Remove(index);
            PreviewImagesList.Remove(index);
            File = null;
        }

        protected async Task TaskPreFileRemove(string index)
        {
            var File = TaskUploadFiles[TaskUploadFiles.FindIndex(x => x.FileInfo?.Id == index)].FileInfo;
            var RemoveList = new FileInfo[1] { File };
            await UploadObj.RemoveAsync(RemoveList);
            PreviewFiles.Remove(index);
            PreviewFilesList.Remove(index);
            File = null;
        }

        protected async Task TaskPreSaveImageRemove(string index)
        {
            var File = TaskUploadFiles[TaskUploadFiles.FindIndex(x => x.FileInfo?.Id == index)].FileInfo;
            var RemoveList = new FileInfo[1] { File };
            await TaskUploadRef.RemoveAsync(RemoveList);
            PreviewImages.Remove(index);
            PreviewImagesList.Remove(index);
            File = null;
        }

        protected async Task TaskPreSaveFileRemove(string index)
        {
            var File = TaskUploadFiles[TaskUploadFiles.FindIndex(x => x.FileInfo?.Id == index)].FileInfo;
            var RemoveList = new FileInfo[1] { File };
            await TaskUploadRef.RemoveAsync(RemoveList);
            PreviewFiles.Remove(index);
            PreviewFilesList.Remove(index);
            File = null;
        }

        protected bool IsRenderingImages = false;
        protected async Task LoadTaskImages(TasksTasksViewModel TaskModel)
        {
            IsRenderingImages = true;
            SelectedTask = TaskModel;
            SelectedTaskImages = (await taskService.GetOneTimeTaskImagesAsync(TaskModel.Id)).ToList();
            this.SelectedTaskImages = SelectedTaskImages.Where(x => IsImage(x.PictureLink)).ToList();
            IsRenderingImages = false;
        }

        protected bool IsRenderingFiles = false;
        protected async Task LoadTaskFiles(TasksTasksViewModel TaskModel)
        {
            IsRenderingFiles = true;
            SelectedTask = TaskModel;
            var SelectedTaskFiles = (await taskService.GetOneTimeTaskImagesAsync(TaskModel.Id)).ToList();
            this.SelectedTaskFiles = SelectedTaskFiles.Where(x => IsFile(x.PictureLink)).ToList();
            foreach (var file in SelectedTaskFiles)
            {
                file.FileName = Path.GetFileName(file.PictureLink);
            }
            IsRenderingFiles = false;
        }

        protected async Task EditItem(TasksTasksViewModel task)
        {
            RecentTaskImagesAndFiles();
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
        protected async Task ShowUpdateEditDialogHandler(TasksTasksViewModel model, TasksTaskUpdatesViewModel tasksTaskUpdates)
        {
            SelectedTask = model;
            UpdateEditContext = tasksTaskUpdates;
            await Task.Delay(1);
        }
        public async Task ChangePriorityVal(ChangeEventArgs args, TasksTasksViewModel EventTask)
        {
            var taskLook = Tasks.FirstOrDefault(x => x.Id != EventTask.Id &&
                x.TaskType == EventTask.TaskType &&
                x.Priority == Convert.ToInt32(args.Value));

            var difference = Convert.ToInt32(args.Value) - Convert.ToInt32(EventTask.Priority);

            if (taskLook != null && Math.Abs(difference) == 1)
            {
                taskLook.Priority = EventTask.Priority;

                await taskService.UpdateOneTask(taskLook);
                Tasks[Tasks.FindIndex(x => x.Id == taskLook.Id)] = taskLook;
                mainTasksModel[mainTasksModel.FindIndex(x => x.Id == taskLook.Id)] = taskLook;

                EventTask.Priority = Convert.ToInt32(args.Value);
                await taskService.UpdateOneTask(EventTask);
                Tasks[Tasks.FindIndex(x => x.Id == EventTask.Id)] = EventTask;
                mainTasksModel[mainTasksModel.FindIndex(x => x.Id == EventTask.Id)] = EventTask;

            }
            else if (taskLook != null && Math.Abs(difference) > 1)
            {
                EventTask.Priority = Convert.ToInt32(args.Value);
                await taskService.UpdateOneTask(EventTask);
                Tasks[Tasks.FindIndex(x => x.Id == EventTask.Id)] = EventTask;
                mainTasksModel[mainTasksModel.FindIndex(x => x.Id == EventTask.Id)] = EventTask;

                int PrevPriority = Convert.ToInt32(args.Value) + 1;
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
            else if (taskLook == null)
            {
                EventTask.Priority = Convert.ToInt32(args.Value);
                await taskService.UpdateOneTask(EventTask);
                Tasks[Tasks.FindIndex(x => x.Id == EventTask.Id)] = EventTask;
                mainTasksModel[mainTasksModel.FindIndex(x => x.Id == EventTask.Id)] = EventTask;
            }

            Tasks = Tasks.OrderBy(x => x.Priority).ToList();
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

        protected void OpenTaskDescription(TasksTasksViewModel model)
        {
            SelectedTask = model;
        }

        protected bool IsDoneReview { get; set; } = true;
        protected async Task ReviewCheckedChanged(ChangeEventArgs args, TasksTasksViewModel selectedTask)
        {
            while (!IsDoneReview)
                await Task.Delay(25);

            IsDoneReview = false;

            selectedTask.IsReviewed = (bool)args.Value;
            await taskService.UpdateOneTask(selectedTask);
            if (selectedTask.IsReviewed)
                _toastService.ShowSuccess("Task Marked Successfully");
            else
                _toastService.ShowSuccess("Task UnMarked Successfully");

            IsDoneReview = true;
        }

        protected async Task CheckEditTaskType()
        {
            if (string.IsNullOrEmpty(SelectedTask.TaskType))
            {
                await jSRuntime.InvokeVoidAsync("AddRedBox", "TaskTypeEditId");
            }
            else
            {
                await jSRuntime.InvokeVoidAsync("RemoveRedBox", "TaskTypeEditId");
            }
        }

        protected async Task CheckEditDateAdded()
        {
            if (SelectedTask.DateAdded == null)
            {
                await jSRuntime.InvokeVoidAsync("AddRedBox", "DateEditId");
            }
            else
            {
                await jSRuntime.InvokeVoidAsync("RemoveRedBox", "DateEditId");
            }
        }

        protected async Task CheckEditInitiator()
        {
            if (string.IsNullOrEmpty(SelectedTask.Initiator))
            {
                await jSRuntime.InvokeVoidAsync("AddRedBox", "InitiatorEditId");
            }
            else
            {
                await jSRuntime.InvokeVoidAsync("RemoveRedBox", "InitiatorEditId");
            }
        }
        protected async Task CheckEditDescription()
        {
            if (string.IsNullOrEmpty(SelectedTask.Description))
            {
                await jSRuntime.InvokeVoidAsync("AddRedBox", "DescriptionEditId");
            }
            else
            {
                await jSRuntime.InvokeVoidAsync("RemoveRedBox", "DescriptionEditId");
            }
        }
        protected async Task HandleValidEdit()
        {
            await StartTask();
            bool isValid = true;
            if (string.IsNullOrEmpty(SelectedTask.TaskType))
            {
                //_toastService.ShowError("Task Type Can't Be Empty");
                await jSRuntime.InvokeVoidAsync("AddRedBox", "EditTaskTypeId");
                isValid = false;
                await StopTask();
            }
            else if (!TaskTypesAll.Contains(SelectedTask.TaskType))
            {
                // _toastService.ShowError("Please Enter Correct Task Type");
                await jSRuntime.InvokeVoidAsync("AddRedBox", "EditTaskTypeId");
                isValid = false;
                await StopTask();
            }

            else
            {
                await jSRuntime.InvokeVoidAsync("RemoveRedBox", "EditTaskTypeId");
            }
            if (string.IsNullOrEmpty(SelectedTask.Initiator))
            {
                //_toastService.ShowError("Initiator Can't Be Empty");
                await jSRuntime.InvokeVoidAsync("AddRedBox", "EditTaskInitiatortId");
                isValid = false;
                await StopTask();
            }
            else if (!Employees.Contains(SelectedTask.Initiator))
            {
                // _toastService.ShowError("Please Enter Correct Initiator");
                await jSRuntime.InvokeVoidAsync("AddRedBox", "EditTaskInitiatortId");
                isValid = false;
                await StopTask();
            }

            else
            {
                await jSRuntime.InvokeVoidAsync("RemoveRedBox", "EditTaskInitiatortId");
            }
            if (SelectedTask.DateAdded == null)
            {
                // _toastService.ShowError("Date Added Can't Be Empty");
                await jSRuntime.InvokeVoidAsync("AddRedBox", "EditTaskDateId");
                isValid = false;
                await StopTask();
            }
            else
            {
                await jSRuntime.InvokeVoidAsync("RemoveRedBox", "EditTaskDateId");
            }

            if (string.IsNullOrEmpty(SelectedTask.Description))
            {
                // _toastService.ShowError("Description Can't Be Empty");
                await jSRuntime.InvokeVoidAsync("AddRedBox", "EditTaskDescriptionId");
                isValid = false;
                //await StopTask();

            }
            else
            {
                await jSRuntime.InvokeVoidAsync("RemoveRedBox", "EditTaskDescriptionId");
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

                    if (!IsValidSize(file.FileInfo.Size))
                    {
                        var size = Convert.ToDouble(file.FileInfo.Size / 1000000);
                        _toastService.ShowError(string.Format("File size can not be more than 20 mb. Your uploaded image size is {0} mb.", size));
                        return;
                    }


                    var ImageFileName = $"{Guid.NewGuid()}.{file.FileInfo.Type}";
                    var ResultPath = fileManagerService.CreateOneTimeTaskDirectory(SelectedTask.Id.ToString()) + ImageFileName;
                    fileManagerService.WriteToFile(file.Stream, ResultPath);


                    TasksImagesViewModel TM = new TasksImagesViewModel()
                    {
                        PictureLink = ResultPath,
                        OneTimeId = SelectedTask.Id,
                        IsDeleted = false,
                        RecurringId = 0
                    };
                    await taskService.InsertTaskImageAsync(TM);
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

                    await jSRuntime.InvokeAsync<object>("HideModal", "#ViewEditTask");

                    await SetNewDefaults();

                    Tasks = Tasks.OrderBy(x => x.Priority).ToList();

                    TaskGrid.Refresh();
                    await TaskGrid.RefreshColumns();

                    _toastService.ShowSuccess("Task Updated Successfully");
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
            await StopTask();
            CleaneTaskFiles();
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
                UpdateGrid.Refresh();
                await UpdateGrid.RefreshColumns();
            }

            UpdateEditContext = new TasksTaskUpdatesViewModel();
            await SetNewDefaults();

            await jSRuntime.InvokeAsync<object>("HideModal", "#EditUpdateDialog");
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
                }
                else
                {
                    await jSRuntime.InvokeVoidAsync("RemoveRedBox", "AddTaskTypeId");
                }

                if (string.IsNullOrEmpty(NewTaskType.Email))
                {
                    await jSRuntime.InvokeVoidAsync("AddRedBox", "FirstEmailId");
                    isValid = false;
                }
                else
                {
                    await jSRuntime.InvokeVoidAsync("RemoveRedBox", "FirstEmailId");
                }

                if (!isValid)
                {
                    _toastService.ShowError("Please Fill All Required Field..!");
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

                    await SetNewDefaults();

                    await jSRuntime.InvokeAsync<object>("HideModal", "#AddTypeDialog");

                    _toastService.ShowSuccess("Task Type Added Successfully");
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

        }
        protected async Task StartNewTask()
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
                IsReviewed = false,
                ParentTaskId = Convert.ToInt32(TaskId)
            };
            NewTaskType = new TasksTasksTaskTypeViewModel();

            await Task.Delay(200);
            await TaskeTypeCombo.FocusIn();
        }



        protected async Task CheckTaskType()
        {
            if (string.IsNullOrEmpty(NewTask.TaskType))
            {
                await jSRuntime.InvokeVoidAsync("AddRedBox", "TaskTypeId");
            }
            else
            {
                await jSRuntime.InvokeVoidAsync("RemoveRedBox", "TaskTypeId");
            }
        }

        protected async Task CheckDateAdded()
        {
            if (NewTask.DateAdded == null)
            {
                await jSRuntime.InvokeVoidAsync("AddRedBox", "DateId");
            }
            else
            {
                await jSRuntime.InvokeVoidAsync("RemoveRedBox", "DateId");
            }
        }

        protected async Task CheckInitiator()
        {
            if (string.IsNullOrEmpty(NewTask.Initiator))
            {
                await jSRuntime.InvokeVoidAsync("AddRedBox", "InitiatorId");
            }
            else
            {
                await jSRuntime.InvokeVoidAsync("RemoveRedBox", "InitiatorId");
            }
        }
        protected async Task CheckDescription()
        {
            if (string.IsNullOrEmpty(NewTask.Description))
            {
                await jSRuntime.InvokeVoidAsync("AddRedBox", "DescriptionId");
            }
            else
            {
                await jSRuntime.InvokeVoidAsync("RemoveRedBox", "DescriptionId");
            }
        }

        protected async Task HandleValidAdd()
        {
            await StartTask();
            bool isValid = true;
            if (string.IsNullOrEmpty(NewTask.TaskType))
            {
                //_toastService.ShowError("Task Type Can't Be Empty");
                await jSRuntime.InvokeVoidAsync("AddRedBox", "TaskTypeIdAddTask");
                isValid = false;
                await StopTask();

            }
            else if (!TaskTypesAll.Contains(NewTask.TaskType))
            {
                // _toastService.ShowError("Please Enter Correct Task Type");
                await jSRuntime.InvokeVoidAsync("AddRedBox", "TaskTypeIdAddTask");
                isValid = false;
                await StopTask();

            }
            else
            {
                await jSRuntime.InvokeVoidAsync("RemoveRedBox", "TaskTypeIdAddTask");
            }
            if (string.IsNullOrEmpty(NewTask.Initiator))
            {
                // _toastService.ShowError("Initiator Can't Be Empty");
                await jSRuntime.InvokeVoidAsync("AddRedBox", "InitiatorIdAddTask");
                isValid = false;
                await StopTask();

            }
            else if (!Employees.Contains(NewTask.Initiator))
            {
                _toastService.ShowError("Please Enter Correct Initiator");
                await jSRuntime.InvokeVoidAsync("AddRedBox", "InitiatorIdAddTask");
                isValid = false;
                await StopTask();

            }
            else
            {
                await jSRuntime.InvokeVoidAsync("RemoveRedBox", "InitiatorIdAddTask");
            }
            if (NewTask.DateAdded == null)
            {
                //_toastService.ShowError("Date Added Can't Be Empty");
                await jSRuntime.InvokeVoidAsync("AddRedBox", "DateIdAddTask");
                isValid = false;
                await StopTask();

            }
            else
            {
                await jSRuntime.InvokeVoidAsync("RemoveRedBox", "DateIdAddTask");
            }
            if (string.IsNullOrEmpty(NewTask.Description))
            {
                // _toastService.ShowError("Description Can't Be Empty");
                await jSRuntime.InvokeVoidAsync("AddRedBox", "DescriptionIdAddTask");
                isValid = false;
                await StopTask();

            }
            else
            {
                await jSRuntime.InvokeVoidAsync("RemoveRedBox", "DescriptionIdAddTask");
            }

            if (!isValid)
            {
                _toastService.ShowError(string.Format("Please Fill Required Field"));
                return;
            }
            string TaskType = NewTask.TaskType;
            try
            {
                NewTask.IsReviewed = false;
                NewTask.IsDeleted = false;
                NewTask.NudgeCount = 0;

                if (NewTask.Id == 0)
                {
                    NewTask.Id = await taskService.AddTask(NewTask);
                }

                if (NewTask.Id != 0)
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
                            var ResultPath = fileManagerService.CreateOneTimeTaskDirectory(NewTask.Id.ToString()) + ImageFileName;
                            fileManagerService.WriteToFile(file.Stream, ResultPath);


                            TasksImagesViewModel TM = new TasksImagesViewModel()
                            {
                                PictureLink = ResultPath,
                                OneTimeId = NewTask.Id,
                                IsDeleted = false
                            };
                            await taskService.InsertTaskImageAsync(TM);


                            var result = await taskService.UpdateTask(NewTask, NewTask.Priority);

                            TaskUploadFiles = new();
                        }
                    }
                    if (NewTask.Priority != null)
                    {
                        var taskLook = Tasks.FirstOrDefault(x => x.Id != NewTask.Id &&
                    x.TaskType == NewTask.TaskType &&
                    x.Priority == Convert.ToInt32(NewTask.Priority));

                        int PrevPriority = Convert.ToInt32(NewTask.Priority) + 1;
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
                        Tasks.Add(NewTask);
                    }
                    else if (StatusDropdownVal == "3" && @SelectedTask.DateCompleted.HasValue)
                    {
                        Tasks.Add(NewTask);
                    }

                    mainTasksModel.Add(NewTask);

                    await TaskUploadRef.ClearAll();
                    await jSRuntime.InvokeAsync<object>("HideModal", "#AddNewTask");

                    Tasks = Tasks.OrderBy(x => x.Priority).ToList();

                    TaskGrid.Refresh();
                    await TaskGrid.RefreshColumns();

                    await SetNewDefaults();

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
            await StopTask();
            CleaneTaskFiles();

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

        protected async Task PrintingImage(string FilePath)
        {
            await jSRuntime.InvokeAsync<object>("PrintImage", TaskImage);
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
                IsReviewed = false,
                ParentTaskId = Convert.ToInt32(TaskId)
            };

            NewTaskType = new TasksTasksTaskTypeViewModel();

            await InvokeAsync(StateHasChanged);
        }

        protected void ReturnToTasks() => navigationManager.NavigateTo($"/viewtasks/");

        protected int RecentTaskImagesCount { get; set; }
        protected int RecentTaskFilesCount { get; set; }
        protected void RecentTaskImagesAndFiles()
        {
            if (TaskUploadFiles != null)
            {
                RecentTaskImagesCount = TaskUploadFiles.Where(x => x.FileInfo != null && IsImage(x.FileInfo.Name)).Count();
                RecentTaskFilesCount = TaskUploadFiles.Where(x => x.FileInfo != null && IsFile(x.FileInfo.Name)).Count();
            }

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

        protected void CleaneTaskFiles()
        {
            TaskUploadFiles.Clear();
            RecentTaskImagesCount = 0;
            RecentTaskFilesCount = 0;
            UpdatesTask.ImagesCount = 0;
            UpdatesTask.FilesCount = 0;
        }

        protected void CleaneUpdateTaskFiles()
        {
            UploadObjTask.ClearAllAsync();
        }

        protected void ViewImage(string ImageLink, bool Converting)
        {
            TaskImage = Converting ? ConvertImagetoBase64(ImageLink) : ImageLink;
        }

        protected string SelectedPreTMV { get; set; }
        protected TasksImagesViewModel SelectedImageNote { get; set; } = new TasksImagesViewModel();
        protected async Task ModifyImageNote()
        {
            await taskService.UpdateTaskImageAsync(SelectedImageNote);
            _toastService.ShowSuccess("Successfully Saved");

            await jSRuntime.InvokeAsync<object>("HideModal", "#ImageNoteModal");
        }

        protected SfTextBox ImageNoteBox { get; set; }

        protected void StartModifyingImageNote(TasksImagesViewModel model)
        {
            SelectedImageNote = model;
        }

        protected bool IsDoingTask { get; set; } = false;

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

        protected void SelectTaskImages(UploadChangeEventArgs args)
        {
            foreach (var file in args.Files)
            {

                var fileLookUp = TaskUploadFiles.FirstOrDefault(x => x.FileInfo != null && file.FileInfo.Id == x.FileInfo.Id);
                if (fileLookUp == null)
                    TaskUploadFiles.Add(file);
            }
            RecentTaskImagesAndFiles();
        }

        protected void BeforeUploadImage()
        {
            //IsUploading = true;
        }

    }
}
