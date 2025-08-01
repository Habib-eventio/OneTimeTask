using CamcoTasks.Service.IService;
using CamcoTasks.ViewModels.PageLoadTimeDTO;
using CamcoTasks.ViewModels.TasksRecTasksDTO;
using CamcoTasks.ViewModels.TasksTaskUpdatesDTO;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace CamcoTasks.Pages.RecurringTasks.ViewRecurringTasks
{
    public partial class DeleteRecUpdateComponent
    {
        [Inject]
        protected ITasksService taskService { get; set; }
        [Inject]
        private IJSRuntime jSRuntime { get; set; }
        [Inject]
        private ILogger<DeleteRecUpdateComponent> logger { get; set; }
        [Inject]
        protected IPageLoadTimeService pageLoadTimeService { get; set; }

        [Parameter]
        public TasksRecTasksViewModel TaskForDelete { get; set; }
        [Parameter]
        public TasksTaskUpdatesViewModel UpdateTaskForDelete { get; set; }
        [Parameter]
        public EventCallback<string> SuccessMessageDeleteRecUpdateComponent { get; set; }
        [Parameter]
        public EventCallback<int> RefreshToParentDeleteRecUpdateComponent { get; set; }
        [Parameter]
        public EventCallback CloseDeleteRecUpdateComponent { get; set; }

        protected PageLoadTimeViewModel PageLoadTime { get; set; } = new PageLoadTimeViewModel()
        {
            PageName = "RecurringTask",
            SectionName = "DeleteRecUpdateComponent",
            DateCreated = DateTime.Now
        };

        protected bool IsDoingTask { get; set; } = false;


        protected override async Task OnInitializedAsync()
        {
            await Task.Run(() => PageLoadTime.StartTime = DateTime.Now);
        }

        protected override async void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                await LoadDeleteRecUpdateData();

                await PageLoadTimeCalculation();
            }
        }

        protected async Task PageLoadTimeCalculation()
        {
            PageLoadTime.EndTime = DateTime.Now;
            await pageLoadTimeService.InsertAsync(PageLoadTime);
        }

        protected async Task LoadDeleteRecUpdateData()
        {
            await jSRuntime.InvokeAsync<object>("ShowModal", "#deleteupdateModal");
        }

        protected async Task DeleteUpdate()
        {
            try
            {
                await Task.Run(() => IsDoingTask = true);

                List<TasksTaskUpdatesViewModel> updateList = (await taskService
                    .GetTaskUpdatesWithAllFieldSync(TaskForDelete.Id, false))
                    .OrderByDescending(x => x.UpdateDate).ToList();
                int updateListLength = updateList.Count();

                TasksTaskUpdatesViewModel LastupdateTasks = updateList
                    .FirstOrDefault(x => !x.UpdateId.Equals(UpdateTaskForDelete.UpdateId));
                var _freq = await taskService.GetFrequency(TaskForDelete.Frequency);
                int days = taskService.RecurringUpcommingDate(_freq);

                if (updateListLength == 1)
                {
                    TaskForDelete.StartDate = TaskForDelete.StartDate == null ? DateTime.Now : TaskForDelete.StartDate.Value;

                    TaskForDelete.DateCompleted = null;
                    TaskForDelete.UpcomingDate = TaskForDelete.StartDate.Value.Date.AddDays(days);

                    await taskService.UpdateRecurringTask(TaskForDelete);
                }
                else
                {
                    int currentTaskUpdateIndex = updateList.FindIndex(x => x.UpdateId.Equals(UpdateTaskForDelete.UpdateId));

                    if (currentTaskUpdateIndex == 0)
                    {
                        TaskForDelete.DateCompleted = LastupdateTasks.UpdateDate;
                        TaskForDelete.UpcomingDate = TaskForDelete.DateCompleted?.Date.AddDays(days);
                        LastupdateTasks.DueDate = TaskForDelete.UpcomingDate;

                        await taskService.UpdateTaskUpdate(LastupdateTasks);
                        await taskService.UpdateRecurringTask(TaskForDelete);
                    }
                }

                UpdateTaskForDelete.IsDeleted = true;
                var result = await taskService.UpdateTaskUpdate(UpdateTaskForDelete);

                await jSRuntime.InvokeAsync<object>("HideModal", "#deleteupdateModal");

                if (result)
                {
                    await SuccessMessageDeleteRecUpdateComponent.InvokeAsync("Update Has Been Deleted!");
                    if (UpdateTaskForDelete != null)
                        await RefreshToParentDeleteRecUpdateComponent.InvokeAsync(UpdateTaskForDelete.UpdateId);
                }
                else
                {
                    await SuccessMessageDeleteRecUpdateComponent.InvokeAsync("Update Has Not Been Deleted!");
                }

                await CloseDeleteRecUpdateComponent.InvokeAsync();
            }
            catch (Exception ex)
            {
                if (ex != null)
                {
                    logger.LogWarning(ex, "Update Delete Error!", ex);
                }
                else
                {
                    logger.LogWarning("Update Delete Error!");
                }
            }
        }

        protected async Task CloseComponent()
        {
            await jSRuntime.InvokeAsync<object>("HideModal", "#deleteupdateModal");
            await CloseDeleteRecUpdateComponent.InvokeAsync();
        }
    }
}