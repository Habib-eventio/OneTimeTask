using CamcoTasks.Service.IService;
using CamcoTasks.ViewModels.PageLoadTimeDTO;
using CamcoTasks.ViewModels.TasksRecTasksDTO;
using Microsoft.AspNetCore.Components;

namespace CamcoTasks.Pages.RecurringTasks.Reports
{
    public partial class RecLateReport
    {
        [Inject]
        protected IPageLoadTimeService pageLoadTimeService { get; set; }
        [Inject]
        protected ITasksService TaskService { get; set; }

        [Parameter]
        public int Month { get; set; }

        protected PageLoadTimeViewModel PageLoadTime { get; set; } = new PageLoadTimeViewModel()
        {
            PageName = "RecurringTask",
            SectionName = "TaskList",
            DateCreated = DateTime.Now
        };

        protected bool _isRecLateReportLoad = true;

        protected List<TasksRecTasksViewModel> Tasks { get; set; } = new List<TasksRecTasksViewModel>();


        protected override async Task OnInitializedAsync()
        {
            await Task.Run(() => PageLoadTime.StartTime = DateTime.Now);
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await LoadContext();

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
            await LoadData();

            await Task.Run(() => _isRecLateReportLoad = false);
        }

        protected async Task LoadData()
        {
            var taskData = (await TaskService.GetRecurringTasksAsync(DateTime.Now, DateTime.Now.AddMonths(Month),
                false, false)).ToList();

            if (!taskData.Any())
            {
                return;
            }

            foreach(var task in taskData)
            {
                var update = (await TaskService.GetTaskUpdatesAsync(task.Id,
                    "Rich Arnold ''NUDGED'' you to get the recurring Task", "Rich Arnold ''Email'' you to get the recurring Task", false, true, Month, false)).ToList();

                if (update.Any())
                {
                    Tasks.Add(task);
                }   
            }
        }
    }
}
