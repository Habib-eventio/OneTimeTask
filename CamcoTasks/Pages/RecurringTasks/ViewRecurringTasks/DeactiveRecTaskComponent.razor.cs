using CamcoTasks.Service.IService;
using CamcoTasks.ViewModels.PageLoadTimeDTO;
using CamcoTasks.ViewModels.TasksRecTasksDTO;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace CamcoTasks.Pages.RecurringTasks.ViewRecurringTasks
{
    public partial class DeactiveRecTaskComponent
    {
        [Inject]
        protected ITasksService taskService { get; set; }
        [Inject]
        private IJSRuntime jSRuntime { get; set; }
        [Inject]
        protected ILogger<DeactiveRecTaskComponent> logger { get; set; }
        [Inject]
        protected IPageLoadTimeService pageLoadTimeService { get; set; }

        [Parameter]
        public TasksRecTasksViewModel TaskViewModel { get; set; }
        [CascadingParameter]
        public ViewRecurringTasks ViewRecurringTasksComponentRef { get; set; }

        protected PageLoadTimeViewModel PageLoadTime { get; set; } = new PageLoadTimeViewModel()
        {
            PageName = "RecurringTask",
            SectionName = "DeactivateRecTaskComponent",
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
            await jSRuntime.InvokeAsync<object>("ShowModal", "#DeactivateRecurringTask");
        }

        protected async Task DeactivateTask()
        {
            try
            {
                await jSRuntime.InvokeAsync<object>("HideModal", "#DeactivateRecurringTask");
                await ViewRecurringTasksComponentRef.DeactivateDeactiveRecTaskComponent();

                TaskViewModel.IsDeactivated = true;
                TaskViewModel.TaskDeactivatedDate = DateTime.Today;
                await taskService.UpdateRecurringTaskSync(TaskViewModel);

                await ViewRecurringTasksComponentRef.SuccessMessageViewRecurringTasksComponent("Task Deactivated Successfully");
                await ViewRecurringTasksComponentRef.RefreshViewRecurringTaskComponent(TaskViewModel.Id);
                await ViewRecurringTasksComponentRef.PercentageComponentRef.ReloadComponentAsync(TaskViewModel.Id, null);
            }
            catch (Exception ex)
            {
                if (ex != null)
                {
                    logger.LogError(ex, "Task Deactivate Error", ex);
                }
                else
                {
                    logger.LogError("Task Deactivate Error");
                }
            }
        }

        protected async Task CloseComponent()
        {
            await jSRuntime.InvokeAsync<object>("HideModal", "#DeactivateRecurringTask");
            await ViewRecurringTasksComponentRef.DeactivateDeactiveRecTaskComponent();
        }
    }
}
