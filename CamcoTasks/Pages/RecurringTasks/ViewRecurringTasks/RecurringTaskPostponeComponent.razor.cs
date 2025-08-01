using CamcoTasks.Service.IService;
using CamcoTasks.Service.Service;
using CamcoTasks.ViewModels.PageLoadTimeDTO;
using CamcoTasks.ViewModels.TasksRecTasksDTO;
using CamcoTasks.ViewModels.TasksTaskUpdatesDTO;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace CamcoTasks.Pages.RecurringTasks.ViewRecurringTasks
{
    public partial class RecurringTaskPostponeComponent
    {
        [Inject]
        private IJSRuntime jSRuntime { get; set; }
        [Inject]
        protected IPageLoadTimeService pageLoadTimeService { get; set; }
        [Inject]
        protected IEmployeeService employeeService { get; set; }

        [Parameter]
        public bool isActivePostponedTask { get; set; }
        
        [Parameter]
        public TasksRecTasksViewModel RecurringTask { get; set; }

        [Parameter]
        public EventCallback EventCallbackPostponeComponent { get; set; }
        [Parameter]
        public EventCallback ClosePostponeComponent { get; set; }
        [Parameter]
        public EventCallback<string> SuccessMessagePostponeComponent { get; set; }

        protected bool IsDoing { get; set; } = false;

        protected Dictionary<string, object> htmlAttribute = new Dictionary<string, object>() { { "rows", "4" } };

        protected TasksTaskUpdatesViewModel UpdatesTask = new TasksTaskUpdatesViewModel();
        protected string PostponeReason { get; set; }
        protected string Error { get; set; }

        protected int PostponeDays { get; set; }

        protected IEnumerable<string> Auditemployee { get; set; }


        protected PageLoadTimeViewModel PageLoadTime { get; set; } = new PageLoadTimeViewModel()
        {
            PageName = "RecurringTask",
            SectionName = "PostponeComponent",
            DateCreated = DateTime.Now
        };


        protected override async Task OnInitializedAsync()
        {
            await Task.Run(() => PageLoadTime.StartTime = DateTime.Now);
        }

        protected override async Task OnParametersSetAsync()
        {
            if (UpdatesTask.PostponeReason != null && UpdatesTask.PostponeDays != null)
            {
                await Task.Run(() => PostponeReason = UpdatesTask.PostponeReason);
                await Task.Run(() => PostponeDays = UpdatesTask.PostponeDays.Value);
            }
        }
        protected async Task LoadData()
        {
            UpdatesTask = new TasksTaskUpdatesViewModel();
            var EmployeesList = await employeeService.GetListAsync(true);
            Auditemployee = EmployeesList.Where(e => e.IsActive)
                    .Select(e => e.FullName)
                    .OrderBy(e => e);
        }
        protected override async void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                await LoadContext();

                await LoadData();

                StateHasChanged();

                await PageLoadTimeCalculation();
            }
        }

        protected async Task PageLoadTimeCalculation()
        {
            PageLoadTime.EndTime = DateTime.Now;
            await pageLoadTimeService.InsertAsync(PageLoadTime);
        }

        protected async Task LoadContext()
        {
            await jSRuntime.InvokeAsync<object>("ShowModal", "#RecTaskPostponeModal");
        }

        protected async Task Save()
        {
            if (string.IsNullOrEmpty(PostponeReason))
            {
                Error = "Please set the Reason...";
                return;
            }
            if (!isActivePostponedTask)
            {
                if (PostponeDays <= 0)
                {
                    Error = "Please set days of postpone task...";
                    return;
                }
            }

            RecurringTask.DelayReason = PostponeReason;
            RecurringTask.TaskDelayedDays = PostponeDays;
            RecurringTask.IsTaskDelayed = true;
            RecurringTask.TaskDelayedDate = DateTime.Now;
            await EventCallbackPostponeComponent.InvokeAsync();
            await SuccessMessagePostponeComponent.InvokeAsync("Task Postpone save Successfully...");

            await jSRuntime.InvokeAsync<object>("HideModal", "#RecTaskPostponeModal");
            await ClosePostponeComponent.InvokeAsync();
        }

        protected async Task CloseComponent()
        {
            await jSRuntime.InvokeAsync<object>("HideModal", "#RecTaskPostponeModal");
            await ClosePostponeComponent.InvokeAsync();
        }
    }
}
