using Blazored.Toast.Services;
using CamcoTasks.Data.Services;
using CamcoTasks.Service.IService;
using CamcoTasks.ViewModels.TasksImagesDTO;
using CamcoTasks.ViewModels.TasksTasksDTO;
using CamcoTasks.ViewModels.TasksTaskUpdatesDTO;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Syncfusion.Blazor.Grids;
using Syncfusion.Blazor.Inputs;

namespace CamcoTasks.Pages.Tasks.ViewTasks
{
    public partial class TasksUpdateViewComponent
    {
        [Inject]
        private IJSRuntime jSRuntime { get; set; }
        [Inject]
        protected ITasksService taskService { get; set; }
        [Inject]
        private FileManagerService fileManagerService { get; set; }
        [Inject]
        private IToastService _toastService { get; set; }

        [Parameter]
        public TasksTasksViewModel OneTimeTask { get; set; }
        [Parameter]
        public EventCallback<Dictionary<string, string>> CallbackMessageTasksUpdateViewComponent { get; set; }
        [Parameter]
        public EventCallback<bool> RefreshTasksViewTasksComponent { get; set; }

        public TasksTasksViewModel SelectedUpdateTaskViewModel { get; set; } = new TasksTasksViewModel();

        protected TasksTasksViewModel SelectedTaskViewModel { get; set; } = new TasksTasksViewModel() { ParentTaskId = null };

        protected List<UploadFiles> TaskUpdateUploadFiles { get; set; } = new();

        protected SfGrid<TasksTaskUpdatesViewModel> UpdateGrid { get; set; }

        protected SfUploader UploadObj { get; set; }

        protected SfTextBox EmailNoteRef { get; set; }

        protected string UpdateTitle { get; set; } = "ADD NEW";

        protected TasksTaskUpdatesViewModel SelectedUpdateViewModel { get; set; } = new TasksTaskUpdatesViewModel();
        protected TasksTaskUpdatesViewModel TaskForTasksUpdateCreateComponent { get; set; }
        protected TasksTasksViewModel OneTimeTaskForTasksUpdateCreateComponent { get; set; }
        protected TasksTasksViewModel TaskForMultipleFileComponent { get; set; }
        protected TasksTaskUpdatesViewModel TaskForTasksDeleteUpdateComponent { get; set; }
        protected TasksTaskUpdatesViewModel TaskForTasksUpdateEmailComponent { get; set; }
        protected TasksTaskUpdatesViewModel TaskForTasksUpdateMultipleFileComponent { get; set; }

        public TasksTasksViewModel SelectedTask { get; set; } = new TasksTasksViewModel() { DateAdded = DateTime.Now };

        public bool IsEditingUpdate { get; set; } = false;
        protected bool IsLoadTask { get; set; } = true;
        protected bool IsActivateTasksUpdateCreateComponent { get; set; } = false;
        protected bool IsActivateTasksMultipleFilesComponent { get; set; } = false;
        protected bool IsActivateTasksUpdateDeleteComponent { get; set; } = false;
        protected bool IsActivateTasksUpdateEmailComponent { get; set; } = false;
        protected bool IsActivateTasksUpdateMultipleFiles { get; set; } = false;

        protected List<TasksImagesViewModel> SelectedTaskFiles { get; set; } = new List<TasksImagesViewModel>();
        protected List<TasksImagesViewModel> SelectedTaskImages { get; set; } = new List<TasksImagesViewModel>();


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
            await jSRuntime.InvokeAsync<object>("ShowModal", "#TaskUpdatesModal");

            await LoadData();

            IsLoadTask = false;
        }

        protected async Task LoadData()
        {
            if (OneTimeTask != null)
            {
                var updatesModel = await taskService.GetTaskUpdates(OneTimeTask.Id);

                SelectedUpdateTaskViewModel = OneTimeTask;
                SelectedUpdateTaskViewModel.TasksTaskUpdates = updatesModel.OrderByDescending(x => x.UpdateDate.Date).ToList();

                var TempTaskImages = await taskService.GetOneTimeTaskImagesCountAsync(SelectedUpdateTaskViewModel.Id);
                SelectedUpdateTaskViewModel.FilesCount = TempTaskImages.Count();
            }
        }

        protected void LoadTaskFiles(TasksTasksViewModel TaskModel)
        {
            IsActivateTasksMultipleFilesComponent = true;
            TaskForMultipleFileComponent = TaskModel;
        }

        protected void StartTaskUpdate(TasksTaskUpdatesViewModel selectedUpdate)
        {
            IsActivateTasksUpdateCreateComponent = true;
            OneTimeTaskForTasksUpdateCreateComponent = SelectedUpdateTaskViewModel;
            TaskForTasksUpdateCreateComponent = selectedUpdate;
        }

        protected void DeleteUpdateConfirm(TasksTaskUpdatesViewModel tasksTaskUpdates)
        {
            IsActivateTasksUpdateDeleteComponent = true;
            TaskForTasksDeleteUpdateComponent = tasksTaskUpdates;
        }

        protected void StartRespond(TasksTaskUpdatesViewModel task)
        {
            IsActivateTasksUpdateEmailComponent = true;
            TaskForTasksUpdateEmailComponent = task;
        }

        protected async Task CallbackMessageFromTasksUpdateEmailComponent(Dictionary<string, string> message)
        {
            if (message != null && message.Any())
            {
                foreach (var item in message)
                {
                    _toastService.ShowSuccess(item.Value + " " + item.Key);
                }
            }

            await Task.Run(() => IsActivateTasksUpdateEmailComponent = false);
        }

        protected void TaskUpdateFiles(TasksTaskUpdatesViewModel viewModel)
        {
            IsActivateTasksUpdateMultipleFiles = true;
            TaskForTasksUpdateMultipleFileComponent = viewModel;
        }

        protected async Task CloseComponet()
        {
            await jSRuntime.InvokeAsync<object>("HideModalWithModalBackdrop", "#TaskUpdatesModal");
            await CallbackMessageTasksUpdateViewComponent.InvokeAsync(new Dictionary<string, string>());
        }

        protected async Task CallbackMessageFromTasksUpdateCreateComponent(Dictionary<string, string> message)
        {
            if (message != null && message.Any())
            {
                foreach (var item in message)
                {
                    _toastService.ShowSuccess(item.Value + " " + item.Key);
                }
            }

            await Task.Run(() => IsActivateTasksUpdateCreateComponent = false);
        }

        protected async Task RefreshComponent(bool isRefresh)
        {
            if (isRefresh)
            {
                await LoadData();

                if (UpdateGrid != null)
                {
                    await UpdateGrid.Refresh();
                }

                await RefreshTasksViewTasksComponent.InvokeAsync(true);
            }
        }

        protected async Task RefreshTasksUpdateView(bool isRefresh)
        {
            if (isRefresh)
            {
                await LoadData();

                if (UpdateGrid != null)
                {
                    await UpdateGrid.Refresh();
                }
            }
        }

        protected async Task CallbackFromTasksUpdateCreateSaveAndClose(bool isActive)
        {
            if (isActive)
            {
                await jSRuntime.InvokeAsync<object>("HideModalWithModalBackdrop", "#TaskUpdatesModal");

                await CallbackMessageTasksUpdateViewComponent.InvokeAsync(new Dictionary<string, string>());

                await RefreshTasksViewTasksComponent.InvokeAsync(true);
            }

            await Task.Run(() => IsActivateTasksUpdateCreateComponent = false);
        }

        protected async Task CallbackFromTasksMultipleFiles(bool isSuccess)
        {
            if (isSuccess)
                await Task.Run(() => IsActivateTasksMultipleFilesComponent = false);
        }

        protected async Task CallbackFromTasksUpdateMultipleFilesComponent(bool isSuccess)
        {
            if (isSuccess)
                await Task.Run(() => IsActivateTasksUpdateMultipleFiles = false);
        }

        protected async Task SuccessMessageFromTasksUpdateDeleteRecComponent(Dictionary<string, string> message)
        {
            if (message != null && message.Any())
            {
                foreach (var item in message)
                {
                    _toastService.ShowSuccess(item.Value + " " + item.Key);
                }
            }

            await Task.Run(() => IsActivateTasksUpdateDeleteComponent = false);
        }
    }
}
