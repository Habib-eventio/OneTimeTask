using CamcoTasks.Service.IService;
using CamcoTasks.ViewModels.PageLoadTimeDTO;
using CamcoTasks.ViewModels.TasksRecTasksDTO;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace CamcoTasks.Pages.RecurringTasks.ViewRecurringTasks
{
    public partial class DeleteRecTaskComponent
    {
        [Inject]
        protected ITasksService taskService { get; set; }
        [Inject]
        private IJSRuntime jSRuntime { get; set; }
        [Inject]
        protected ILogger<DeleteRecTaskComponent> Logger { get; set; }
        [Inject]
        protected IPageLoadTimeService pageLoadTimeService { get; set; }

        [Parameter]
        public TasksRecTasksViewModel TaskViewModel { get; set; }
        [Parameter]
        public EventCallback CloseDeleteRecTaskComponent { get; set; }
        [Parameter]
        public EventCallback<string> MessageFromDeleteRecTaskComponent { get; set; }
        [Parameter]
        public EventCallback<int> RefreshParentComponent { get; set; }

        protected PageLoadTimeViewModel PageLoadTime { get; set; } = new PageLoadTimeViewModel()
        {
            PageName = "RecurringTask",
            SectionName = "DeleteRecTaskComponent",
            DateCreated = DateTime.Now
        };


        protected override async Task OnInitializedAsync()
        {
            await Task.Run(() => PageLoadTime.StartTime = DateTime.Now);
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await LoadDeactiveComponentData();

                await PageLoadTimeCalculation();
            }
        }

        protected async Task PageLoadTimeCalculation()
        {
            PageLoadTime.EndTime = DateTime.Now;
            await pageLoadTimeService.InsertAsync(PageLoadTime);
        }

        protected async Task LoadDeactiveComponentData()
        {
            await jSRuntime.InvokeAsync<object>("ShowModal", "#deleteRecurringTask");
        }

        protected async Task DeleteTask()
        {
            try
            {
                TaskViewModel.IsDeleted = true;
                var response = taskService.UpdateRecurringTaskSync(TaskViewModel);

                await MessageFromDeleteRecTaskComponent.InvokeAsync("Task Deleted Successfully");
                await RefreshParentComponent.InvokeAsync(TaskViewModel.Id);
                await CloseDeleteRecTaskComponent.InvokeAsync();
            }
            catch (Exception ex)
            {
                if (ex != null)
                {
                    Logger.LogError(ex, "Task Delete Error", ex);
                }
                else
                {
                    Logger.LogError("Task Delete Error");
                }
            }
        }

        protected async Task CloseComponent()
        {
            await jSRuntime.InvokeAsync<object>("HideModal", "#deleteRecurringTask");
            await CloseDeleteRecTaskComponent.InvokeAsync();
        }
    }
}
