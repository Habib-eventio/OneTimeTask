using Append.Blazor.Printing;
using Blazored.Toast.Services;
using CamcoTasks.Data.Services;
using CamcoTasks.Service.IService;
using CamcoTasks.ViewModels.EmployeeDTO;
using CamcoTasks.ViewModels.ModelsViewModel;
using CamcoTasks.ViewModels.TasksFrequencyListDTO;
using CamcoTasks.ViewModels.TasksRecTasksDTO;
using CamcoTasks.ViewModels.TasksTaskUpdatesDTO;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using Syncfusion.Blazor.Data;
using Syncfusion.Blazor.Grids;
using Syncfusion.Blazor.Inputs;
using Syncfusion.Blazor.Navigations;



namespace CamcoTasks.Pages.RecurringTasks.Reports
{
    partial class RecurringTaskReport
    {
        [Inject] private IToastService _toastService { get; set; }
        [Inject] protected ITasksService taskService { get; set; }
        [Inject] protected IEmployeeService employeeService { get; set; }
        [Inject] private FileManagerService fileManagerService { get; set; }
        [Inject] protected IPrintingService PrintingService { get; set; }
        [Inject] private IJSRuntime jSRuntime { get; set; }

        protected List<TasksRecTasksViewModel> Tasks { get; set; } = new List<TasksRecTasksViewModel>();
        protected List<string> Employees { get; set; }
        protected List<TaskCompletionViewModel> EmployeesButtons { get; set; } = new List<TaskCompletionViewModel>();
        protected List<string> EmployeesFilter { get; set; }
        protected List<TasksTaskUpdatesViewModel> TasksUpdates { get; set; } = new List<TasksTaskUpdatesViewModel>();
        protected List<EmployeeEmail> employeeEmails { get; set; } = new List<EmployeeEmail>();
        protected List<EmployeeEmail> FailedemployeeEmails { get; set; } = new List<EmployeeEmail>();
        protected List<TasksRecTasksViewModel> DeactivatedTasks { get; set; } = new List<TasksRecTasksViewModel>();
        protected List<TasksRecTasksViewModel> TasksList { get; set; } = new List<TasksRecTasksViewModel>();

        protected IEnumerable<TasksTaskUpdatesViewModel> UpdateTasksList { get; set; }
        protected IEnumerable<TasksFrequencyListViewModel> TasksFrequency { get; set; }

        protected bool IsSpinner { get; set; } = true;

        public Query GridQuery { get; set; }

        protected TasksRecTasksViewModel SelectedTaskViewModel { get; set; } = new TasksRecTasksViewModel() { ParentTaskId = null };
        protected TasksTaskUpdatesViewModel SelectedUpdateViewModel { get; set; } = new TasksTaskUpdatesViewModel()
        { Update = string.Empty, UpdateDate = DateTime.Today, IsAudit = false, IsPass = false };

        protected int TotalPasDueTasks { get; set; }
        protected int RecTasksCount { get; set; }
        protected int InActiveEmployeeCount { get; set; }
        protected int InActiveTasksCount { get; set; }
        protected int ReAssignTaskseCount { get; set; }

        protected int CompletedOnTimeCount1 { get; set; } = 0;
        protected int CompletedOnTimeCount3 { get; set; } = 0;
        protected int CompletedOnTimeCount6 { get; set; } = 0;
        protected int CompletedOnTimeCount12 { get; set; } = 0;
        protected int CompletedOnTimeTotalCount { get; set; } = 0;
        protected int AllEmployeesPercentage { get; set; } = 0;

        protected string AllEmployeesColor { get; set; } = "ALL EMPLOYEES";
        protected string AllEmployees { get; set; } = "ALL EMPLOYEES";

        protected SfGrid<TasksRecTasksViewModel> RecurringTasksGrid { get; set; }
        protected UploadFiles UploadFileUpdate { get; set; } = new UploadFiles();

        protected override async Task OnInitializedAsync()
        {
            await LoadDataAsync();

            LoadData();
            LoadContext();

            IsSpinner = false;
        }

        protected async Task LoadDataAsync()
        {
            Tasks = (await taskService.GetRecurringTasks(x => !x.IsDeleted && x.IsDeactivated == false && x.ParentTaskId == null)).ToList();
        }

        protected async Task LoadData()
        {
            UpdateTasksList = await taskService.GetTaskUpdatesSync();
            TasksFrequency = await taskService.GetTaskFreqsSync();
        }

        protected void LoadContext()
        {
            try
            {
                Tasks = Tasks.OrderBy(a => a.UpcomingDate).ToList();
                Tasks = Tasks.Where(a => a.DateCompleted.HasValue).ToList();
                RecTasksCount = Tasks.Count;

                TasksList = Tasks;

                CalculationOfTaskPercentage();

                AllEmployeesPercentage = CompletedOnTimeTotalCount;

                AllEmployeesColor = AllEmployeesPercentage >= 50 ? "#179B09" : "#CA0000";

                EmployeesButtons = Tasks.GroupBy(x => x.PersonResponsible).Where(x => x.Count() > 10)
                    .Select(x => new TaskCompletionViewModel
                    {
                        EmployeeName = x.Key,
                        Color = "",
                        Percentage = 0,
                        CompletedCount = 0,
                        FailedCount = 0,
                        TotalCount = 0
                    }).ToList();

                //Calculate Percentage for each employee
                foreach (var Emp in EmployeesButtons)
                {
                    var TotalTasks = TasksList.Where(x => x.PersonResponsible == Emp.EmployeeName && x.DateCompleted.HasValue).ToList();
                    if (TotalTasks != null)
                    {

                        var empTasks = TotalTasks.Where(x => x.DateCompleted >= DateTime.Today.AddMonths(-2));
                        if (empTasks != null)
                        {
                            var percentageTable = taskService.PercentageCalculation(TotalTasks, UpdateTasksList.ToList(), TasksFrequency.ToList(), -2);
                            int lastRow = percentageTable.Rows.Count;
                            Emp.Percentage = Convert.ToInt32(percentageTable.Rows[lastRow - 1]["Overall On Time Percentage"]);
                        }
                    }
                    Emp.Color = Emp.Percentage >= 50 ? "#179B09" : "#CA0000";
                }
            }
            catch (Exception ex)
            {
                _toastService.ShowError(ex.Message + "Error");
            }
        }

        protected void CalculationOfTaskPercentage()
        {
            var percentageTable = taskService.PercentageCalculation(TasksList, UpdateTasksList.ToList(), TasksFrequency.ToList(), -1);
            int lastRow = percentageTable.Rows.Count;
            CompletedOnTimeCount1 = Convert.ToInt32(percentageTable.Rows[lastRow - 1]["Overall On Time Percentage"]);

            percentageTable = taskService.PercentageCalculation(TasksList, UpdateTasksList.ToList(), TasksFrequency.ToList(), -3);
            lastRow = percentageTable.Rows.Count;
            CompletedOnTimeCount3 = Convert.ToInt32(percentageTable.Rows[lastRow - 1]["Overall On Time Percentage"]);

            percentageTable = taskService.PercentageCalculation(TasksList, UpdateTasksList.ToList(), TasksFrequency.ToList(), -6);
            lastRow = percentageTable.Rows.Count;
            CompletedOnTimeCount6 = Convert.ToInt32(percentageTable.Rows[lastRow - 1]["Overall On Time Percentage"]);

            percentageTable = taskService.PercentageCalculation(TasksList, UpdateTasksList.ToList(), TasksFrequency.ToList(), -12);
            lastRow = percentageTable.Rows.Count;
            CompletedOnTimeCount12 = Convert.ToInt32(percentageTable.Rows[lastRow - 1]["Overall On Time Percentage"]);

            percentageTable = taskService.PercentageCalculation(TasksList, UpdateTasksList.ToList(), TasksFrequency.ToList(), 0);
            lastRow = percentageTable.Rows.Count;
            CompletedOnTimeTotalCount = Convert.ToInt32(percentageTable.Rows[lastRow - 1]["Overall On Time Percentage"]);
        }

        protected List<object> Toolbaritems = new()
        {
            "Search",
            new ItemModel() { Text = "Print Report", TooltipText = "Print", PrefixIcon = "e-print", Id = "Print" },
            new ItemModel() { Text = "TASK DUE IN NEXT 7 DAYS", TooltipText = "Filter", PrefixIcon = "e-filter", Id = "Filter7" },
            new ItemModel() { Text = "PAST DUE TASKS", TooltipText = "Filter", PrefixIcon = "e-filter", Id = "Filter8" },
            new ItemModel() { Text = "Clear Filters", TooltipText = "Filter", PrefixIcon = "e-erase", Id = "FilterClear" }
        };

        protected async Task FilterFields(string PersonResponsible)
        {
            await RecurringTasksGrid.ClearFilteringAsync();
            if (PersonResponsible != AllEmployees)
            {
                await RecurringTasksGrid.FilterByColumnAsync(nameof(TasksRecTasksViewModel.PersonResponsible),
             "equal", PersonResponsible, "or");
            }
        }

        protected async Task StartFilteringGrid()
        {
            var rows = await RecurringTasksGrid.GetFilteredRecordsAsync();
            var TaskList = JsonConvert.DeserializeObject<List<TasksRecTasksViewModel>>(JsonConvert.SerializeObject(rows));
            TasksList = TaskList.Where(a => a.DateCompleted.HasValue).ToList();
            RecTasksCount = TaskList.Count;

            CalculationOfTaskPercentage();
        }

        protected async Task StartPrinting(ClickEventArgs args)
        {
            if (args.Item.Text == "Print Report")
            {
                _toastService.ShowSuccess("Generating Report Started, Please Wait.");
                var rows = await RecurringTasksGrid.GetFilteredRecordsAsync();
                var TasksList = JsonConvert.DeserializeObject<List<TasksRecTasksViewModel>>(JsonConvert.SerializeObject(rows));
                fileManagerService.CreatePdfRecurringTaskReport(TasksList);

                //await PrintingService.Print("MetricsPrint\\RecurringTaskReport.pdf");
                await jSRuntime.InvokeAsync<object>("Print", "MetricsPrint\\RecurringTaskReport.pdf");
            }
            else if (args.Item.Text == "TASK DUE IN NEXT 7 DAYS")
            {
                await FilteGrid7DaysDueDate();
            }
            else if (args.Item.Text == "PAST DUE TASKS")
            {
                await FilteGridPastDueDate();
            }


            else if (args.Item.Text == "Clear Filters")
            {
                await RecurringTasksGrid.ClearFilteringAsync();
                ClearFilter();
            }
        }

        public async Task FilteGrid7DaysDueDate()
        {
            GridQuery = new Query();
            await RecurringTasksGrid.ClearFilteringAsync();
            List<WhereFilter> Predicate = new();
            Predicate.Add(new WhereFilter()
            {
                Field = nameof(TasksRecTasksViewModel.UpcomingDate),
                value = DateTime.Today,
                Operator = "greaterthanorequal",
                IgnoreCase = true
            });
            Predicate.Add(new WhereFilter()
            {
                Field = nameof(TasksRecTasksViewModel.UpcomingDate),
                value = DateTime.Today.AddDays(7),
                Operator = "lessthanorequal",
                IgnoreCase = true
            });
            var ColPre = WhereFilter.And(Predicate);
            GridQuery = new Query().Where(ColPre);
        }

        public async Task FilteGridPastDueDate()
        {
            ClearFilter();
            await RecurringTasksGrid.ClearFilteringAsync();
            GridQuery = new Query();
            List<WhereFilter> Predicate = new List<WhereFilter>();
            Predicate.Add(new WhereFilter()
            {
                Field = nameof(TasksRecTasksViewModel.UpcomingDate),
                value = DateTime.Today,
                Operator = "lessthanorequal",
                IgnoreCase = true
            });
            WhereFilter ColPre = WhereFilter.And(Predicate);
            GridQuery = new Query().Where(ColPre);
        }

        public void ClearFilter()
        {
            GridQuery = new Query();
        }
    }
}
