using Blazored.Toast.Services;
using ERP.Data.Entities.HR;
using CamcoTasks.Service.IService;
using CamcoTasks.ViewModels.EmployeeDTO;
using CamcoTasks.ViewModels.ModelsViewModel;
using CamcoTasks.ViewModels.PageLoadTimeDTO;
using CamcoTasks.ViewModels.TasksFrequencyListDTO;
using CamcoTasks.ViewModels.TasksRecTasksDTO;
using CamcoTasks.ViewModels.TasksTaskUpdatesDTO;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using Syncfusion.Blazor.Data;

namespace CamcoTasks.Pages.RecurringTasks.ViewRecurringTasks
{
    public partial class RecurringTaskPercentageComponent
    {
        private bool _isActiveFilter = false;

        private string _filterQueary = string.Empty;
        
        private bool _pending = false;

        protected List<Tuple<string, int>> TaskArea = new ();
        protected List<TasksRecTasksViewModel> Tasks { get; set; } = new List<TasksRecTasksViewModel>();
        protected List<TasksRecTasksViewModel> PendingTasks { get; set; } = new List<TasksRecTasksViewModel>();
        protected List<TasksRecTasksViewModel> TasksList { get; set; } = new List<TasksRecTasksViewModel>();
        protected List<TaskCompletionViewModel> EmployeesButtons { get; set; } = new List<TaskCompletionViewModel>();

        protected IEnumerable<TasksTaskUpdatesViewModel> UpdateTasksList { get; set; }
        protected IEnumerable<TasksFrequencyListViewModel> TasksFrequency { get; set; }

        protected int RecTasksCount { get; set; }
        protected int CompletedOnTimeCount1 { get; set; } = 0;
        protected int CompletedOnTimeCount2 { get; set; } = 0;
        protected int CompletedOnTimeTotalCount { get; set; } = 0;
        protected int TotalPasDueTasks { get; set; }
        protected int TotalPendingTasks { get; set; }
        protected int TotalApprovedTasks{ get; set; }
        protected int ReAssignTaskseCount { get; set; }
        protected int MonthDuration { get; set; } = -2;

        protected long SelectedEmpId { get; set; }

        protected bool IsLoadData { get; set; } = true;
        protected bool IsFirstExecute { get; set; } = true;

        protected PageLoadTimeViewModel PageLoadTime { get; set; } = new PageLoadTimeViewModel()
        {
            PageName = "RecurringTask",
            SectionName = "TaskPercentage",
            DateCreated = DateTime.Now
        };

        [Inject]
        protected ITasksService taskService { get; set; }
        [Inject]
        protected IEmployeeService employeeService { get; set; }
        [Inject]
        protected ILogger<RecurringTaskPercentageComponent> logger { get; set; }
        [Inject]
        protected IPageLoadTimeService pageLoadTimeService { get; set; }
        [Inject]
        protected ITimeClockEmployeeSettingService TimeClockEmployeeSettingService { get; set; }
        [Inject]
        protected IDepartmentService DepartmentService { get; set; }
        [Inject]
        protected IJSRuntime JsRuntime { get; set; }
        [Inject]
        protected IToastService toastService { get; set; }
        [Inject]
        protected NavigationManager navigationManager { get; set; }

        [Parameter]
        public string ParamTaskId { get; set; }
        [Parameter]
        public long EmployeeId { get; set; }
        [Parameter]
        public string FirstName { get; set; }
        [Parameter]
        public string Lastname { get; set; }
        [Parameter]
        public bool IsPending { get; set; }
        [Parameter]
        public EventCallback<string> EmployeeButtonCallback { get; set; }
        [Parameter]
        public EventCallback<string> TaskAreaCallback { get; set; }



        protected override async Task OnInitializedAsync()
        {
            await Task.Run(() => PageLoadTime.StartTime = DateTime.Now);
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await LoadData(true);

                await LoadContext();

                await CountDataAsync();

                IsLoadData = false;

                StateHasChanged();

                await PageLoadTimeCalculation();
            }
        }

        protected async Task LoadData(bool IsReload)
        {
            if (IsReload)
            {
                Tasks = (await taskService.GetRecurringTasksForPercentageAsync(false, false, null)).ToList();
                TotalPendingTasks = Tasks.Where(x => !x.IsApproved).Count();
                if (IsPending)
                {
                    TotalApprovedTasks = await taskService.CountApprovedLastMonthRecurringTasks();
                } else
                {
                    Tasks = Tasks.Where(x => x.IsApproved).ToList();
                }
                UpdateTasksList = await taskService.GetTaskUpdatesForPercentageAsync("system generated update",
                "Rich Arnold ''NUDGED'' you to get the recurring Task", "Rich Arnold ''Email'' you to get the recurring Task", true, true, MonthDuration, false);
                TasksFrequency = await taskService.GetTaskFreqs();

                var timeClockEmployeesId = (await TimeClockEmployeeSettingService.GetEmpIdListAsync(true)).ToList();
                var departmentId = (await employeeService.GetListAsync(true, false, timeClockEmployeesId)).ToList();
                TaskArea = (await DepartmentService.GetListAsync(false, departmentId))
                    .Select(department => new Tuple<string, int>(department, Tasks.Count(x => !x.IsApproved && x.TaskArea == department))).ToList();
            }
        }

        protected async Task PageLoadTimeCalculation()
        {
            PageLoadTime.EndTime = DateTime.Now;
            await pageLoadTimeService.InsertAsync(PageLoadTime);
        }

        protected async Task LoadContext()
        {
            try
            {
                RecTasksCount = Tasks.Count();

                if (EmployeeId > 0)
                {
                    var EmployeeLookUp = await employeeService.GetByIdAsync(EmployeeId);
                    if (EmployeeLookUp != null)
                    {
                        var EmpName = EmployeeLookUp.FullName;
                        Tasks = Tasks.Where(a => a.PersonResponsible == EmpName).ToList();

                        SelectedEmpId = EmployeeId;
                    }
                }

                if (!string.IsNullOrEmpty(FirstName) && !string.IsNullOrEmpty(Lastname))
                {
                    string Name = Lastname + ", " + FirstName;
                    Tasks = Tasks.Where(a => a.PersonResponsible == Name).ToList();
                }

                if (!string.IsNullOrEmpty(ParamTaskId) && !string.IsNullOrEmpty(ParamTaskId))
                {
                    int PId = Convert.ToInt32(ParamTaskId);
                    Tasks = Tasks.Where(a => a.Id == PId).ToList();
                }

                TasksList = Tasks.Where(a => a.DateCompleted.HasValue
                    && a.UpcomingDate.HasValue)
                    .OrderBy(a => a.UpcomingDate)
                    .ToList();
                GetTaskPercentage();

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
                        var percentageTable = taskService.PercentageCalculation(TotalTasks, UpdateTasksList.ToList(), TasksFrequency.ToList(), MonthDuration);
                        int lastRow = percentageTable.Rows.Count;
                        Emp.Percentage = Convert.ToInt32(percentageTable.Rows[lastRow - 1]["Overall On Time Percentage"]);
                    }
                    Emp.Color = Emp.Percentage >= 50 ? "#179B09" : "#CA0000";
                }

                EmployeesButtons.Add(new TaskCompletionViewModel() { Color = CompletedOnTimeTotalCount >= 50 ? "#179B09" : "#CA0000", EmployeeName = "ALL EMPLOYEES", Percentage = CompletedOnTimeTotalCount });
                EmployeesButtons = Enumerable.Reverse(EmployeesButtons).ToList();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Recurring Task Percentage Error", ex);
            }
        }

        protected async Task CountDataAsync()
        {
            ReAssignTaskseCount = await taskService.GetRecurringTasksCountAsync(false, true);
            TotalPasDueTasks = GetTotalDueTasks();
        }

        public int GetTotalDueTasks()
        {
            int TotalDueTasks = 0;

            TotalDueTasks = Tasks.Count(x => x.UpcomingDate?.Date < DateTime.Today);

            return TotalDueTasks;
        }

        private void GetTaskPercentage()
        {
            var percentageTable = taskService.PercentageCalculation(TasksList, UpdateTasksList.ToList(), TasksFrequency.ToList(), -1);
            int lastRow = 0;
            if (percentageTable != null && percentageTable.Rows.Count > 0)
            {
                lastRow = percentageTable.Rows.Count;
                CompletedOnTimeCount1 = Convert.ToInt32(percentageTable.Rows[lastRow - 1]["Overall On Time Percentage"]);
            }
            else
            {
                CompletedOnTimeCount1 = 0;
            }

            percentageTable = taskService.PercentageCalculation(TasksList, UpdateTasksList.ToList(), TasksFrequency.ToList(), MonthDuration);
            if (percentageTable != null && percentageTable.Rows.Count > 0)
            {
                lastRow = percentageTable.Rows.Count;
                CompletedOnTimeCount2 = Convert.ToInt32(percentageTable.Rows[lastRow - 1]["Overall On Time Percentage"]);
            }
            else
            {
                CompletedOnTimeCount2 = 0;
            }

            percentageTable = taskService.PercentageCalculation(TasksList, UpdateTasksList.ToList(), TasksFrequency.ToList(), 0);
            if (percentageTable != null && percentageTable.Rows.Count > 0)
            {
                lastRow = percentageTable.Rows.Count;
                CompletedOnTimeTotalCount = Convert.ToInt32(percentageTable.Rows[lastRow - 1]["Overall On Time Percentage"]);
            }
            else
            {
                CompletedOnTimeTotalCount = 0;
            }
        }

        public async Task ReloadComponentAsync()
        {
            await Task.Run(() => IsLoadData = true);

            await LoadData(true);
            await LoadContext();
            await CountDataAsync();

            await Task.Run(() => IsLoadData = false);

            StateHasChanged();
        }

        public async Task ReloadComponentAsync(int? recurringId, int? updateId)
        {
            await Task.Run(() => IsLoadData = true);

            if (recurringId != null && recurringId.Value > 0)
            {
                var updateTask = await taskService.GetRecurringTaskById(recurringId.Value);
                var oldTaks = Tasks.FirstOrDefault(x => x.Id == recurringId.Value);

                if (updateTask == null || (updateTask != null && (updateTask.IsDeactivated || updateTask.IsDeleted)) && Tasks.Any())
                {
                    Tasks.Remove(oldTaks);
                }
                else
                {
                    if (oldTaks != null && Tasks.Any())
                    {
                        int updateTaskIndex = Tasks.IndexOf(oldTaks);

                        if (updateTaskIndex > -1)
                        {
                            Tasks[updateTaskIndex] = updateTask;
                        }
                    }
                    else
                    {
                        Tasks.Add(updateTask);
                    }
                }
            }

            if (updateId != null && updateId.Value > 0)
            {
                var update = await taskService.GetTaskUpdatesByIdAsync(updateId.Value);
                var oldUpdate = UpdateTasksList.FirstOrDefault(x => x.UpdateId == updateId.Value);
                var updateList = UpdateTasksList.ToList();

                if (update != null && update.IsDeleted == true && updateList.Any())
                {
                    updateList.Remove(oldUpdate);
                }
                else
                {
                    if (oldUpdate != null && updateList.Any())
                    {
                        int updateIndex = updateList.IndexOf(oldUpdate);

                        if (updateIndex > -1)
                        {
                            updateList[updateIndex] = update;
                        }
                    }
                    else
                    {
                        if (update.DueDate.HasValue && update.DueDate >= DateTime.Today.AddMonths(MonthDuration))
                        {
                            updateList.Add(update);
                        }
                    }
                }

                UpdateTasksList = updateList;
            }

            await LoadData(false);
            await LoadContext();
            await CountDataAsync();

            await Task.Run(() => IsLoadData = false);

            StateHasChanged();
        }

        public async Task ReloadComponentAfterFilterAsync(List<TasksRecTasksViewModel> FilterTaskList)
        {
            await Task.Run(() => IsLoadData = true);

            if (FilterTaskList.Any())
            {
                Tasks = FilterTaskList;
                Tasks = Tasks.Where(x => x.IsApproved).ToList();
                await LoadData(false);
                await LoadContext();
            }

            await Task.Run(() => IsLoadData = false);

            StateHasChanged();
        }

        private async Task PercentageFilterQueary()
        {
            if (_filterQueary == "ALL EMPLOYEES")
            {
                await LoadData(true);
            }
            else
            {
                Tasks = (await taskService.GetRecurringTasksBySearchAsync(false, false,
                    _filterQueary)).ToList();
                Tasks = Tasks.Where(x => x.IsApproved).ToList();
                RecTasksCount = Tasks.Count;
                UpdateTasksList = await taskService.GetTaskUpdatesForPercentageAsync("system generated update",
                "Rich Arnold ''NUDGED'' you to get the recurring Task", "Rich Arnold ''Email'' you to get the recurring Task", true, true, MonthDuration, false);
            }

            TasksList = Tasks.Where(a => a.DateCompleted.HasValue).ToList();
            GetTaskPercentage();

            RecTasksCount = Tasks.Count();

            await EmployeeButtonCallback.InvokeAsync(_filterQueary);
        }

        protected async Task PercentageFilterFields(string PersonResponsible)
        {
            await Task.Run(() => IsLoadData = true);
            StateHasChanged();

            if (_isActiveFilter)
            {
                if (PersonResponsible == _filterQueary)
                {
                    _isActiveFilter = false;
                    _filterQueary = string.Empty;

                    await LoadData(true);

                    await LoadContext();

                    await CountDataAsync();

                    await EmployeeButtonCallback.InvokeAsync("ALL EMPLOYEES");
                }
                else
                {
                    _filterQueary = PersonResponsible;
                    await PercentageFilterQueary();
                }
            }
            else
            {
                _isActiveFilter = true;
                _filterQueary = PersonResponsible;
                await PercentageFilterQueary();
            }

            await Task.Run(() => IsLoadData = false);

            if (_isActiveFilter)
                await JsRuntime.InvokeVoidAsync("SelecElement", PersonResponsible, "lightgreen", "lightblue");
        }

        private async Task DepartmentFilterQueary()
        {
            Tasks = (await taskService.GetRecurringTasksByTaskAreaAsync(false, false, !IsPending, null, _filterQueary)).ToList();
            RecTasksCount = Tasks.Count;
            UpdateTasksList = await taskService.GetTaskUpdatesForPercentageAsync("system generated update",
                "Rich Arnold ''NUDGED'' you to get the recurring Task", "Rich Arnold ''Email'' you to get the recurring Task", true, true, MonthDuration, false);

            TasksList = Tasks.Where(a => a.DateCompleted.HasValue).ToList();
            GetTaskPercentage();

            RecTasksCount = Tasks.Count();

            await TaskAreaCallback.InvokeAsync(_filterQueary);
        }

        protected async Task DepartmentFilterFields(string department)
        {
            await Task.Run(() => IsLoadData = true);
            StateHasChanged();

            if (_isActiveFilter)
            {
                if (department == _filterQueary)
                {
                    _isActiveFilter = false;
                    _filterQueary = string.Empty;

                    await LoadData(true);

                    await LoadContext();

                    await CountDataAsync();

                    await EmployeeButtonCallback.InvokeAsync("ALL EMPLOYEES");
                }
                else
                {
                    _filterQueary = department;
                    await DepartmentFilterQueary();
                }
            }
            else
            {
                _isActiveFilter = true;
                _filterQueary = department;
                await DepartmentFilterQueary();
            }

            await Task.Run(() => IsLoadData = false);

            if (_isActiveFilter)
                await JsRuntime.InvokeVoidAsync("SelecElement", department, "lightgreen", "lightblue");
        }
    }
}