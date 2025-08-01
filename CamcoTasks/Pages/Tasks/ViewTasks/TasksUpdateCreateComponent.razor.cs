using CamcoTasks.Data.Services;
using CamcoTasks.Service.IService;
using CamcoTasks.Infrastructure.Common.Email;
using CamcoTasks.ViewModels.TasksImagesDTO;
using CamcoTasks.ViewModels.TasksTasksDTO;
using CamcoTasks.ViewModels.TasksTasksTaskTypeDTO;
using CamcoTasks.ViewModels.TasksTaskUpdatesDTO;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Syncfusion.Blazor.Grids;
using Syncfusion.Blazor.Inputs;
using FileInfo = Syncfusion.Blazor.Inputs.FileInfo;
using DocumentFormat.OpenXml.Wordprocessing;
using CamcoTasks.Infrastructure.Entities.TaskInfo;
using CamcoTasks.Infrastructure.Defaults;

namespace CamcoTasks.Pages.Tasks.ViewTasks
{
    public partial class TasksUpdateCreateComponent
    {
        [Inject]
        protected ITasksService taskService { get; set; }
        [Inject]
        private IJSRuntime jSRuntime { get; set; }
        [Inject]
        private FileManagerService fileManagerService { get; set; }
        [Inject]
        private IEmailService emailService { get; set; }
        [Inject]
        ILogger<TasksUpdateCreateComponent> logger { get; set; }

        [Parameter]
        public TasksTaskUpdatesViewModel TaskUpdate { get; set; }
        [Parameter]
        public TasksTasksViewModel OneTimeTask { get; set; }
        [Parameter]
        public EventCallback<Dictionary<string, string>> CallbacckMessageTaskUpdateCreateComponent { get; set; }
        [Parameter]
        public EventCallback<bool> RefreshTasksUpdateViewComponent { get; set; }
        [Parameter]
        public EventCallback<bool> CallbackFromTasksUpdateCreateSaveAndClose { get; set; }

        protected bool IsDoingTask { get; set; } = false;
        public bool IsTaskDone { get; set; } = false;
        public bool IsEditingUpdate { get; set; } = false;
        protected bool IsEditeTaskType { get; set; } = false;
        protected bool IsLoadTask { get; set; } = true;

        protected string UpdateTitle { get; set; } = "ADD NEW";
        protected string ErrorDateMessage { get; set; } = "";
        protected string ErrorUpdateMessage { get; set; }

        protected bool IsActiveTasksImageShowComponent { get; set; } = false;

        protected string TaskImage { get; set; } = null;

        protected string TaskFile { get; set; } = null;

        protected TasksTaskUpdatesViewModel SelectedUpdateViewModel { get; set; } = new TasksTaskUpdatesViewModel();

        public TasksTasksViewModel SelectedUpdateTaskViewModel { get; set; }
        public TasksTasksViewModel SelectedTask { get; set; } = new TasksTasksViewModel() { DateAdded = DateTime.Now };

        public TasksTasksTaskTypeViewModel NewTaskType { get; set; } = new TasksTasksTaskTypeViewModel();

        protected Dictionary<string, object> htmlAttributeBig = new Dictionary<string, object>() { { "rows", "7" }, { "spellcheck", "true" } };

        protected SfGrid<TasksTasksViewModel> TaskGrid { get; set; }

        protected SfGrid<TasksTaskUpdatesViewModel> UpdateGrid { get; set; }

        protected SfUploader UploadObj { get; set; }

        protected List<UploadFiles> TaskUpdateUploadFiles { get; set; } = new();

        protected List<TasksImagesViewModel> UpdateUploadFiles { get; set; } = new List<TasksImagesViewModel>();


        protected override async void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                await LoadContext();

                StateHasChanged();
            }
        }

        protected async Task LoadContext()
        {
            await jSRuntime.InvokeAsync<object>("ShowModal", "#AddEditTaskUpdate");

            await LoadData();

            await StartTaskUpdate(TaskUpdate);

            IsLoadTask = false;
        }

        protected async Task LoadData()
        {
            if (OneTimeTask != null)
            {
                await Task.Run(() => SelectedUpdateTaskViewModel = OneTimeTask);
            }
        }

        protected async Task StartTaskUpdate(TasksTaskUpdatesViewModel selectedUpdate)
        {
            if (selectedUpdate == null)
            {
                UpdateTitle = "ADD NEW";
                await StartUpdateAdd();
            }
            else
            {
                UpdateTitle = "EDIT";
                await StartUpdateEdit(selectedUpdate);
            }
        }

        protected async Task StartUpdateAdd()
        {
            SelectedUpdateViewModel = new TasksTaskUpdatesViewModel() { UpdateDate = DateTime.Today };
            await Task.Delay(0);
        }

        protected async Task StartUpdateEdit(TasksTaskUpdatesViewModel selectedUpdate)
        {
            IsEditingUpdate = true;
            SelectedUpdateViewModel = selectedUpdate;
            await Task.Delay(0);
        }

        protected async Task TryHandlingUpdate(bool closeModal)
        {
            if (IsEditingUpdate)
            {
                IsEditingUpdate = false;
                await HandleValidUpdateEdit(closeModal);
            }
            else
            {
                await HandleValidUpdateAdd(closeModal);
            }
        }

        protected async Task HandleValidUpdateEdit(bool closeModal)
        {
            try
            {
                if (SelectedUpdateTaskViewModel != null)
                {
                    if (!await OneTimeTaskUpdateValidation(SelectedUpdateViewModel, true))
                        return;

                    if (TaskUpdateUploadFiles != null)
                    {
                        foreach (var file in TaskUpdateUploadFiles)
                        {
                            if (file.FileInfo != null)
                            {
                                if (!fileManagerService.IsValidSize(file.FileInfo.Size))
                                {
                                    var size = Convert.ToDouble(file.FileInfo.Size / 1000000);
                                    ErrorUpdateMessage = (string.Format("Image size can not be more than 20 mb. Your uploaded image size is {0} mb.", size));
                                    return;
                                }

                                var ImageFileName = $"{Guid.NewGuid()}.{file.FileInfo.Type}";
                                var ResultPath = fileManagerService.CreateOneTimeTaskDirectory(SelectedUpdateTaskViewModel.Id.ToString()) + ImageFileName;
                                fileManagerService.WriteToFile(file.Stream, ResultPath);
                                SelectedUpdateViewModel.IsPicture = true;

                                TasksImagesViewModel TM = new TasksImagesViewModel()
                                {
                                    PictureLink = ResultPath,
                                    UpdateId = SelectedUpdateViewModel.UpdateId,
                                    IsDeleted = false,
                                    OneTimeId = 0,
                                    RecurringId = 0
                                };
                                await taskService.InsertTaskImageAsync(TM);
                            }
                        }
                    }

                    var result = await taskService.UpdateTaskUpdate(SelectedUpdateViewModel);

                    SelectedUpdateViewModel = new TasksTaskUpdatesViewModel();

                    if (closeModal)
                    {
                        await jSRuntime.InvokeAsync<object>("HideModalWithModalBackdrop", "#TaskUpdatesModal");
                        await CallbacckMessageTaskUpdateCreateComponent.InvokeAsync(new Dictionary<string, string>()
                    {
                        {"Task Update Information", "Update Has Been Updated!" }
                    });
                        await CallbackFromTasksUpdateCreateSaveAndClose.InvokeAsync(true);
                    }
                    else
                    {
                        await jSRuntime.InvokeAsync<object>("HideModal", "#AddEditTaskUpdate");
                        await CallbacckMessageTaskUpdateCreateComponent.InvokeAsync(new Dictionary<string, string>()
                    {
                        {"Task Update Information", "Update Has Been Updated!" }
                    });
                        await RefreshTasksUpdateViewComponent.InvokeAsync(true);
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    logger.LogError(ex.InnerException.Message, "Update Add Error!");
                }
                else
                {
                    logger.LogError(ex.Message, "Update Add Error!");
                }
            }

        }

        protected async Task<bool> OneTimeTaskUpdateValidation(TasksTaskUpdatesViewModel SelectedUpdateViewModel, bool isEdit)
        {
            bool isValid = true;
            ErrorUpdateMessage = null;

            if (this.SelectedUpdateViewModel.UpdateDate < DateTime.Today && isEdit == false)
            {
                await jSRuntime.InvokeVoidAsync("AddRedBox", "UpdateDate");
                ErrorDateMessage = "Please Select Valid Date";
                isValid = false;
                return isValid;
            }
            else
            {
                await jSRuntime.InvokeVoidAsync("RemoveRedBox", "UpdateDate");
                ErrorDateMessage = string.Empty;
            }


            if (string.IsNullOrEmpty(this.SelectedUpdateViewModel.Update))
            {
                ErrorUpdateMessage = "Please Enter Update Description";
                await jSRuntime.InvokeVoidAsync("AddRedBox", "UpdateDescription");
                isValid = false;
                return isValid;
            }
            else
            {
                await jSRuntime.InvokeVoidAsync("RemoveRedBox", "UpdateDescription");
                ErrorUpdateMessage = string.Empty;
            }

            return isValid;
        }

        public async Task HandleValidUpdateAdd(bool closeModal)
        {
            try
            {
                if (SelectedUpdateTaskViewModel != null)
                {
                    if (!await OneTimeTaskUpdateValidation(SelectedUpdateViewModel, false))
                        return;

                    SelectedUpdateViewModel.DueDate = SelectedUpdateTaskViewModel.UpcomingDate;

                    if (SelectedUpdateTaskViewModel.UpcomingDate?.Date >= DateTime.Now.Date)
                        SelectedUpdateViewModel.TaskCompleted = true;
                    else
                    {
                        SelectedUpdateViewModel.TaskCompleted = false;
                    }

                    SelectedUpdateViewModel.TaskID = SelectedUpdateTaskViewModel.Id;
                    SelectedUpdateViewModel.IsDeleted = false;
                    SelectedUpdateViewModel.UpdateId = await taskService.AddTaskUpdateSync(SelectedUpdateViewModel);

                    if (TaskUpdateUploadFiles != null)
                    {
                        foreach (var file in TaskUpdateUploadFiles)
                        {
                            if (file.FileInfo != null)
                            {
                                if (!fileManagerService.IsValidSize(file.FileInfo.Size))
                                {
                                    var size = Convert.ToDouble(file.FileInfo.Size / 1000000);
                                    ErrorUpdateMessage = (string.Format("Image size can not be more than 20 mb. Your uploaded image size is {0} mb.", size));
                                    return;
                                }

                                var ImageFileName = $"{Guid.NewGuid()}.{file.FileInfo.Type}";
                                var ResultPath = fileManagerService.CreateOneTimeTaskDirectory(SelectedUpdateTaskViewModel.Id.ToString()) + ImageFileName;
                                fileManagerService.WriteToFile(file.Stream, ResultPath);
                                SelectedUpdateViewModel.IsPicture = true;

                                TasksImagesViewModel TM = new TasksImagesViewModel()
                                {
                                    PictureLink = ResultPath,
                                    UpdateId = SelectedUpdateViewModel.UpdateId,
                                    IsDeleted = false,
                                    OneTimeId = 0,
                                    RecurringId = 0
                                };
                                await taskService.InsertTaskImageAsync(TM);
                            }
                        }

                        await taskService.UpdateTaskUpdate(SelectedUpdateViewModel);
                    }

                    await SendUpdateEmail(SelectedUpdateTaskViewModel.Id);

                    if (IsTaskDone)
                    {
                        IsTaskDone = false;
                        if (SelectedUpdateTaskViewModel.DateCompleted == null)
                            SelectedUpdateTaskViewModel.DateCompleted = DateTime.Now;
                    }

                    await taskService.UpdateTask(SelectedUpdateTaskViewModel, SelectedUpdateTaskViewModel.Priority);

                    IsTaskDone = false;

                    if (closeModal)
                    {
                        await jSRuntime.InvokeAsync<object>("HideModalWithModalBackdrop", "#TaskUpdatesModal");
                        await CallbacckMessageTaskUpdateCreateComponent.InvokeAsync(new Dictionary<string, string>()
                    {
                        {"Task Update Information", "Update Has Been Added!" }
                    });
                        await CallbackFromTasksUpdateCreateSaveAndClose.InvokeAsync(true);
                    }
                    else
                    {
                        await jSRuntime.InvokeAsync<object>("HideModal", "#AddEditTaskUpdate");
                        await CallbacckMessageTaskUpdateCreateComponent.InvokeAsync(new Dictionary<string, string>()
                    {
                        {"Task Update Information", "Update Has Been Added!" }
                    });
                        await RefreshTasksUpdateViewComponent.InvokeAsync(true);
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    logger.LogError(ex.InnerException.Message, "Update Add Error!");
                }
                else
                {
                    logger.LogError(ex.Message, "Update Add Error!");
                }
            }
        }

        protected async Task SendUpdateEmail(int taskId)
        {
            var task = await taskService.GetTaskById(taskId);

            if (task == null)
            {
                string body = "<label style=\"font-weight:bold\"> Task ID: </label>" + task.Id +
                           "<br><label style=\"font-weight:bold\"> Description: </label>" + task.Description +
                           "<br><label style=\"font-weight:bold\"> Task Type: </label>" + task.TaskType +
                           "<br><label style=\"font-weight:bold\"> Priority: </label>" + task.Priority +
                           "<br><label style=\"font-weight:bold\"> Update: </label>" + SelectedUpdateViewModel.Update;

                string Subject = "ONE-TIME TASK HAS BEEN UPDATED";

                // EmailQueueViewModel emailqueue = new()
                // {
                //     Body = body,
                //     HasBeenSent = false,
                //     SendTo = "rarnold@camcomfginc.com",
                //     Subject = Subject,
                //     EmailTypeId = 723
                // };

                body = EmailDefaults.GenerateEmailTemplate("Tasks", body);
                await emailService.SendEmailAsync(EmailTypes.ActionBasedOneTimeTaskUpdated,
                    Array.Empty<string>(), Subject, body, string.Empty, new string[] { "rarnold@camcomfginc.com;" });

                // if (EmailId != 0)
                // {
                //     SelectedUpdateViewModel.EmailId = EmailId;
                //     await taskService.UpdateTaskUpdate(SelectedUpdateViewModel);
                // }
            }
        }

        protected void SelectTaskupdateFiles(UploadChangeEventArgs args)
        {
            foreach (var file in args.Files)
            {
                var fileLookUp = TaskUpdateUploadFiles.FirstOrDefault(x => x.FileInfo != null && file.FileInfo.Id == x.FileInfo.Id);
                if (fileLookUp == null)
                    TaskUpdateUploadFiles.Add(file);
            }
        }

        protected void RemoveTaskUpdateFiles(BeforeRemoveEventArgs args)
        {
            if (args.FilesData[0] == null)
                return;

            var RemoveFile = TaskUpdateUploadFiles.FirstOrDefault(x => x.FileInfo?.Id == args.FilesData[0].Id);
            if (RemoveFile != null)
            {
                TaskUpdateUploadFiles = TaskUpdateUploadFiles.Where(x => x.FileInfo?.Id != args.FilesData[0].Id).ToList();
                RemoveFile.FileInfo = null;
            }
        }
        protected void ActiveTasksImageShowComponent(string imagePath = null, string key = null)
        {
            if (fileManagerService.IsImage(key))
            {
                IsActiveTasksImageShowComponent = true;
                TaskImage = imagePath;
            }
            else if (fileManagerService.IsImage(imagePath))
            {
                IsActiveTasksImageShowComponent = true;
                TaskImage = imagePath;
            }
            else
            {
                TaskFile = imagePath;
            }
        }
        protected string ConvertImageToBase64(UploadFiles uploadedFile)
        {
            string path = "";

            if (uploadedFile != null)
            {
                byte[] byteImage = uploadedFile.Stream.ToArray();
                path = $"data:image/png;base64, " + Convert.ToBase64String(byteImage);
            }

            return path;
        }
        protected async Task CloseComponent()
        {
            await jSRuntime.InvokeAsync<object>("HideModal", "#AddEditTaskUpdate");
            await CallbacckMessageTaskUpdateCreateComponent.InvokeAsync(new Dictionary<string, string>());
        }
    }
}
