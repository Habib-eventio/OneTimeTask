using Blazored.Toast.Services;
using CamcoTasks.Data.Services;
using CamcoTasks.Service.IService;
using CamcoTasks.Service.Service;
using CamcoTasks.ViewModels.PageLoadTimeDTO;
using CamcoTasks.ViewModels.TasksImagesDTO;
using CamcoTasks.ViewModels.TasksRecTasksDTO;
using CamcoTasks.ViewModels.TasksTaskUpdatesDTO;
using CamcoTasks.Infrastructure.Entities.TaskInfo;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Syncfusion.Blazor.DropDowns;
using Syncfusion.Blazor.Inputs;

namespace CamcoTasks.Pages.RecurringTasks.ViewRecurringTasks
{
    public partial class ViewSingleRecurringTask
    {
        protected bool IsLoadTask = true;
        protected bool IsActiveRecTaskUpdateComponent = false;
        protected bool IsActiveCreateRecurringTaskComponent = false;
        protected bool IsActiveViewGraphComponent = false;
        protected bool IsActiveTaskUpdateMultipleFileComponent = false;
        protected bool IsActiveCameraComponent = false;
        protected bool IsActiveRecurringTaskPostponeComponent = false;
        protected bool ActivePostponedTask = false;

        protected TasksRecTasksViewModel RecurringTask = new TasksRecTasksViewModel();
        protected TasksTaskUpdatesViewModel RecurringTaskUpdate = new TasksTaskUpdatesViewModel();

        protected PageLoadTimeViewModel PageLoadTime = new PageLoadTimeViewModel()
        {
            PageName = "RecurringTask",
            SectionName = "ViewSingleRe4curringTask",
            DateCreated = DateTime.Now
        };

        protected Dictionary<string, object> htmlAttributeBig = new Dictionary<string, object>() { { "rows", "7" }, { "spellcheck", "true" } };

        [Inject]
        private IJSRuntime JsRuntime { get; set; }
        [Inject]
        protected IPageLoadTimeService pageLoadTimeService { get; set; }
        [Inject]
        protected ITasksService TaskService { get; set; }
        [Inject]
        private IFileManagerService FileManagerService { get; set; }
        [Inject]
        private IToastService ToastService { get; set; }

        [Parameter]
        public int RecTaskId { get; set; }
        [Parameter]
        public EventCallback EventCallbackViewSingleRecTask { get; set; }
        [Parameter]
        public EventCallback<int> RefreshParentComponent { get; set; }
        [Parameter]
        public EventCallback<string> SendMessageToParent { get; set; }

        [CascadingParameter]
        public ViewRecurringTasks ViewRecurringTasksComponentRef { get; set; }


        protected override async Task OnInitializedAsync()
        {
            await Task.Run(() => PageLoadTime.StartTime = DateTime.Now);
        }

        protected override async void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                await LoadContext();
                await LoadData();

                IsLoadTask = false;

                StateHasChanged();

                await PageLoadTimeCalculation();
            }
        }

        protected async Task LoadContext()
        {
            await JsRuntime.InvokeAsync<object>("ShowModal", "#ViewSingleRecurringTaskModal");
        }

        protected async Task LoadData()
        {
            RecurringTask = await TaskService.GetRecurringTaskById(RecTaskId);
            var RecurringTaskUpdateLatest = await TaskService.GetRecurringTaskLatestUpdateAsync(RecTaskId);
            if(RecurringTaskUpdateLatest != null)
            {
                RecurringTaskUpdate = RecurringTaskUpdateLatest;
            }
            else { RecurringTaskUpdate = new TasksTaskUpdatesViewModel(); }
        }

        protected async Task PageLoadTimeCalculation()
        {
            PageLoadTime.EndTime = DateTime.Now;
            await pageLoadTimeService.InsertAsync(PageLoadTime);
        }

        protected void ActiveRecTaskUpdateComponent()
        {
            IsActiveRecTaskUpdateComponent = true;
        }

        protected void DeactiveRecTaskUpdateComponent()
        {
            IsActiveRecTaskUpdateComponent = false;
        }

        protected void ActiveCreateRecurringTaskComponent()
        {
            IsActiveCreateRecurringTaskComponent = true;

        }

        protected void DeactiveCreateRecurringTaskComponent()
        {
            IsActiveCreateRecurringTaskComponent = false;
        }

        protected void ActiveViewGraphComponent()
        {
            IsActiveViewGraphComponent = true;

        }

        protected void DeactiveViewGraphComponent()
        {
            IsActiveViewGraphComponent = false;
        }

        protected void ActiveTaskUpdateMultipleFileComponent()
        {
            IsActiveTaskUpdateMultipleFileComponent = true;

        }

        protected void DeactiveTaskUpdateMultipleFileComponent()
        {
            IsActiveTaskUpdateMultipleFileComponent = false;
        }

        protected void ActiveRecurringTaskPostponeComponent(bool setPostponed)
        {
            IsActiveRecurringTaskPostponeComponent = true;
            if (setPostponed)
            {
                ActivePostponedTask = true;
            }
            else
            {
                ActivePostponedTask = false;
            }
        }

        protected async Task EventCallbackRecurringTaskPostponeComponent()
        {
            if (ActivePostponedTask)
            {
                RecurringTask.DelayReason = "Postponed reason: " + RecurringTask.DelayReason;
                var taskFrequency = await TaskService.GetFrequency(RecurringTask.Frequency);
                DateTime? lastDateCompleted = RecurringTask.DateCompleted;
                RecurringTask.DateCompleted = DateTime.Now;
                TaskService.RecurringUpcommingDate(RecurringTask,taskFrequency, RecurringTaskUpdate);
                RecurringTask.DateCompleted = lastDateCompleted;
                await TaskService.UpdateRecurringTask(RecurringTask);
            }
            if(!ActivePostponedTask && RecurringTask.TaskDelayedDays != 0)
            {
                RecurringTask.DelayReason = "Unable to complete reason: " + RecurringTask.DelayReason;
                RecurringTask.UpcomingDate = RecurringTask.UpcomingDate?.Date.AddDays(RecurringTask.TaskDelayedDays);
                await TaskService.UpdateRecurringTask(RecurringTask);
            }
        }

        protected void SuccessMessage(string message)
        {
            ToastService.ShowSuccess(message);
        }

        protected void DeactiveRecurringTaskPostponeComponent()
        {
            IsActiveRecurringTaskPostponeComponent = false;
        }

        protected void ActiveCameraComponent()
        {
            IsActiveCameraComponent = true;

        }

        protected async Task DeactiveCameraComponent(Dictionary<Guid, string> capturedImgaesList)
        {
            IsActiveCameraComponent = false;
            RecurringTaskUpdate = await TaskService.SaveCaptureImageAsync(capturedImgaesList,
                    RecTaskId, RecurringTaskUpdate);
            await TaskService.UpdateTaskUpdate(RecurringTaskUpdate);
        }

        protected async Task RefreshComponent()
        {
            await LoadData();
            await RefreshParentComponent.InvokeAsync(RecTaskId);
        }

        protected async Task SelectHowToFile(UploadChangeEventArgs args)
        {
            foreach (var file in args.Files)
            {
                if (!FileManagerService.IsValidSize(file.FileInfo.Size)
                    || !FileManagerService.IsFile(file.FileInfo.Name)
                    && !FileManagerService.IsImage(file.FileInfo.Name))
                {
                    var size = Convert.ToDouble(file.FileInfo.Size / 1000000);
                    return;
                }

                var FileName = $"{Guid.NewGuid()}.{file.FileInfo.Type}";
                var ResultPath = FileManagerService.CreateRecurringTaskDirectory(RecurringTask.Id.ToString()) + FileName;
                FileManagerService.WriteToFile(file.Stream, ResultPath);

                RecurringTask.InstructionFileLink = ResultPath;

                await TaskService.UpdateRecurringTask(RecurringTask);

                break;
            }
        }

        protected async Task RemoveHowToFile(BeforeRemoveEventArgs args)
        {
            RecurringTask.InstructionFileLink = "";
            await TaskService.UpdateRecurringTask(RecurringTask);
        }

        protected async Task CloseComponent()
        {
            await JsRuntime.InvokeAsync<object>("HideModal", "#ViewSingleRecurringTaskModal");
            await EventCallbackViewSingleRecTask.InvokeAsync();
        }
    }
}
