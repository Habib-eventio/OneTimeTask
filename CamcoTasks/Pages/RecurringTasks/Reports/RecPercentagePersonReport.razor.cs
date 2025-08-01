using CamcoTasks.Service.IService;
using CamcoTasks.ViewModels.TasksFrequencyListDTO;
using CamcoTasks.ViewModels.TasksRecTasksDTO;
using CamcoTasks.ViewModels.TasksTaskUpdatesDTO;
using Microsoft.AspNetCore.Components;
using System.Data;

namespace CamcoTasks.Pages.RecurringTasks.Reports
{
    public partial class RecPercentagePersonReport
    {
        [Inject] protected ITasksService taskService { get; set; }
        [Inject] protected ILogger<RecPercentagePersonReport> logger { get; set; }

        [Parameter] public string PersonResponsible { get; set; }
        [Parameter] public int MonthDuration { get; set; }

        protected List<TasksRecTasksViewModel> Tasks { get; set; } = new List<TasksRecTasksViewModel>();
        protected List<TasksTaskUpdatesViewModel> UpdateTasksList { get; set; }
        protected List<TasksFrequencyListViewModel> TasksFrequency { get; set; }

        protected bool IsLoading { get; set; } = true;
        protected bool IsDoing { get; set; } = false;

        protected int numberofMisses = 0;
        protected int totalNumber = 0;
        protected int totalnumberofMisses = 0;

        protected DataTable PercentageTable = new DataTable();

        protected DateTime FromDate { get; set; } = DateTime.Now;
        protected DateTime ToDate { get; set; } = DateTime.Now;


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
            if (PersonResponsible == "ALL EMPLOYEES")
                Tasks = (await taskService.GetRecurringTasks(false, false, null)).ToList();
            else
                Tasks = (await taskService.GetRecurringTasks(false, false, null, PersonResponsible)).ToList();

            UpdateTasksList = (await taskService.GetTaskUpdatesAsync("system generated update",
                "Rich Arnold ''NUDGED'' you to get the recurring Task", "Rich Arnold ''Email'' you to get the recurring Task", 
                true, true, MonthDuration, false)).ToList();
            TasksFrequency = (await taskService.GetTaskFreqs()).ToList();
        }

        protected void CreateReport()
        {
            PercentageTable = taskService.PercentageCalculation(Tasks, UpdateTasksList, TasksFrequency, MonthDuration);
        }

        protected async Task PercentageByDateRange()
        {
            await Task.Run(() => IsDoing = true);

            MonthDuration = ((ToDate.Month + ToDate.Year * 12) - (FromDate.Month + FromDate.Year * 12));
            UpdateTasksList = (await taskService.GetTaskUpdatesAsync("system generated update",
                "Rich Arnold ''NUDGED'' you to get the recurring Task", "Rich Arnold ''Email'' you to get the recurring Task", 
                true, true, MonthDuration, false)).ToList();
            CreateReport();

            await Task.Run(() => IsDoing = false);
        }
    }
}
