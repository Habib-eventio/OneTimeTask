using CamcoTasks.Service.IService;
using CamcoTasks.ViewModels.TasksFrequencyListDTO;
using CamcoTasks.ViewModels.TasksRecTasksDTO;
using CamcoTasks.ViewModels.TasksTaskUpdatesDTO;
using Microsoft.AspNetCore.Components;
using System.Data;

namespace CamcoTasks.Pages.RecurringTasks.Reports
{
    public partial class RecPercentageMonthlyReport
    {
        protected TasksRecTasksViewModel Tasks = new TasksRecTasksViewModel();

        protected int numberofMisses = 0;
        protected int totalNumber = 0;
        protected int totalnumberofMisses = 0;

        protected List<TasksTaskUpdatesViewModel> UpdateTasksList { get; set; }
        protected TasksFrequencyListViewModel TasksFrequency = new TasksFrequencyListViewModel();

        protected bool IsLoading { get; set; } = true;
        protected bool IsDoing { get; set; } = false;

        protected DataTable PercentageTable = new DataTable();

        protected DateTime FromDate { get; set; } = DateTime.Now;
        protected DateTime ToDate { get; set; } = DateTime.Now;

        [Inject] protected ITasksService taskService { get; set; }
        [Inject] protected ILogger<RecPercentageMonthlyReport> logger { get; set; }

        [Parameter] public int TaskId { get; set; }
        [Parameter] public int MonthDuration { get; set; }


        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await DataLoad();
                CreateReport();
                await Task.Run(() => IsLoading = false);

                StateHasChanged();
            }
        }

        protected async Task DataLoad()
        {
            Tasks = await taskService.GetRecurringTaskById(TaskId);
            UpdateTasksList = (await taskService.GetTaskUpdatesAsync(TaskId, "system generated update",
                "Rich Arnold ''NUDGED'' you to get the recurring Task", "Rich Arnold ''Email'' you to get the recurring Task",
                DateTime.Now.Date, DateTime.Now.AddMonths(MonthDuration).Date, false)).ToList();
            TasksFrequency = await taskService.GetFrequency(Tasks.TasksFreq.Frequency);
        }

        protected void CreateReport()
        {
            PercentageTable = taskService.RecTaskUpdatePercentageCalculation(Tasks, UpdateTasksList, TasksFrequency);
        }

        protected async Task PercentageByDateRange()
        {
            await Task.Run(() => IsDoing = true);

            MonthDuration = ((ToDate.Month + ToDate.Year * 12) - (FromDate.Month + FromDate.Year * 12));
            UpdateTasksList = (await taskService.GetTaskUpdatesAsync(TaskId, "system generated update",
                "Rich Arnold ''NUDGED'' you to get the recurring Task", "Rich Arnold ''Email'' you to get the recurring Task",
                DateTime.Now.Date, DateTime.Now.AddMonths(MonthDuration).Date, false)).ToList();
            CreateReport();

            await Task.Run(() => IsDoing = false);
        }
    }
}
