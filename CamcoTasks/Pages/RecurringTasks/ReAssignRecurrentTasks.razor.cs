using Blazored.Toast.Services;
using CamcoTasks.Data.Services;
using CamcoTasks.Service.IService;
using CamcoTasks.ViewModels.TasksRecTasksDTO;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Syncfusion.Blazor.Grids;
using Syncfusion.Blazor.Inputs;

namespace CamcoTasks.Pages.RecurringTasks
{
    public class ReAssignRecurrentTasksModel : ComponentBase
    {
        [Inject]
        protected ITasksService taskService { get; set; }

        [Inject]
        protected IEmployeeService employeeService { get; set; }
        protected string TypeDropdownVal = "All";
        protected SfUploader UploadObj;
        protected UploadFiles uploadFile = new UploadFiles();
        protected SfUploader UploadObjNew;
        protected UploadFiles uploadFileNew = new UploadFiles();

        private const string IMAGEFORMATS = @".jpg|.png|.gif|.jpeg|.bmp|.svg|.jfif|.apng|.ico$";

        [Inject]
        private IJSRuntime jSRuntime { get; set; }
        [Inject]
        private NavigationManager navigationManager { get; set; }
        [Inject]
        private FileManagerService fileManagerService { get; set; }
        [Inject]
        private IWebHostEnvironment webHostEnvironment { get; set; }
        public bool IsSpinner { get; set; } = true;

        [Inject]
        private IToastService _toastService { get; set; }

        public List<TasksRecTasksViewModel> Tasks { get; set; }
        public List<EmployeeTasks> employeeTasks { get; set; }

        public List<string> activeEmployees;
        public List<string> inActiveEmployees;

        public TasksRecTasksViewModel SelectedTask { get; set; }
        public EmployeeTasks selectedEmployeeTask { get; set; }
        public TasksRecTasksViewModel NewTask = new TasksRecTasksViewModel();

        public string SelectedEmployee { get; set; } = "";

        protected SfGrid<EmployeeTasks> RecurringTasksGrid { get; set; }
        protected SfGrid<TasksRecTasksViewModel> childRecurringTasksGrid { get; set; }

        protected int InActiveTasksCount { get; set; }
        protected int InActiveEmployeeCount { get; set; }

        public bool IsAll { get; set; } = false;

        protected async Task StartDeleteTasks(EmployeeTasks Model)
        {
            selectedEmployeeTask = Model;
            IsAll = true;
            await Task.Delay(10);
        }
        protected async Task DeleteTask(TasksRecTasksViewModel task, EmployeeTasks tasks)
        {
            selectedEmployeeTask = tasks;
            IsAll = false;
            SelectedTask = task;
            await Task.Delay(10);
        }
        protected async Task ConfirmDelete()
        {
            if (IsAll)
            {
                employeeTasks.Remove(selectedEmployeeTask);

                foreach (var task in selectedEmployeeTask.TasksRecTasks)
                {
                    task.IsDeleted = true;
                    var _b = await taskService.UpdateRecurringTask(task);
                }

                await RecurringTasksGrid.Refresh();
                await RecurringTasksGrid.RefreshColumnsAsync();

                _toastService.ShowSuccess("Tasks Deleted Successfully");
                return;
            }
            else
            {
                try
                {
                    SelectedTask.IsDeleted = true;
                    await taskService.UpdateRecurringTask(SelectedTask);

                    if (selectedEmployeeTask.TasksCount == 1)
                    {
                        employeeTasks.Remove(selectedEmployeeTask);
                    }

                    selectedEmployeeTask.TasksCount--;
                    selectedEmployeeTask.TasksRecTasks.Remove(SelectedTask);

                    await childRecurringTasksGrid.Refresh();
                    await childRecurringTasksGrid.RefreshColumnsAsync();

                    await RecurringTasksGrid.Refresh();
                    await RecurringTasksGrid.RefreshColumnsAsync();

                    await RecurringTasksGrid.ExpandCollapseDetailRowAsync(selectedEmployeeTask);

                    StateHasChanged();

                    _toastService.ShowSuccess("Task Deleted Successfully");
                }
                catch (Exception ex)
                {
                    if (ex.InnerException != null)
                    {
                        _toastService.ShowError(ex.InnerException.Message + "Task Delete Error");
                    }
                    else
                    {
                        _toastService.ShowError(ex.InnerException.Message + "Task Delete Error");
                    }
                    StateHasChanged();
                }
            }
            selectedEmployeeTask = new EmployeeTasks();
            SelectedTask = new TasksRecTasksViewModel();

            await jSRuntime.InvokeAsync<object>("HideModal", "#deleteRecurringTask");
            IsAll = false;
        }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                await LoadPageDetails();
            }
            catch (Exception ex)
            {
                _toastService.ShowError(ex.Message + "Error");
            }

            IsSpinner = false;

            StateHasChanged();
        }

        protected async Task LoadPageDetails()
        {
            var _employees = (await employeeService.GetListAsync()).ToList();
            inActiveEmployees = _employees.Where(x => !x.IsActive).Select(x => x.FullName).ToList();
            InActiveEmployeeCount = inActiveEmployees.Count();
            inActiveEmployees = inActiveEmployees.OrderBy(x => x).ToList();

            var _iemployees = _employees.Where(x => x.IsActive);
            activeEmployees = _iemployees.Select(x => x.FullName).ToList();
            activeEmployees = activeEmployees.OrderBy(x => x).ToList();

            if (inActiveEmployees != null)
            {
                Tasks = (await taskService.GetRecurringTasks()).ToList();
                var inactive = Tasks.Where(a => inActiveEmployees.Contains(a.PersonResponsible)).ToList();
                InActiveTasksCount = inactive.Count;
                Tasks = Tasks.Where(a => inActiveEmployees.Contains(a.PersonResponsible)).ToList();
                Tasks = Tasks.OrderByDescending(a => a.UpcomingDate).ToList();
            }

            var EmployeeGroups = Tasks.GroupBy(x => x.PersonResponsible).ToList();
            employeeTasks = new List<EmployeeTasks>();
            foreach (var item in EmployeeGroups)
            {
                var itemList = item.ToList();
                if (itemList.Count > 0)
                {
                    employeeTasks.Add(new EmployeeTasks()
                    {
                        Name = item.Key,
                        TasksCount = itemList.Count,
                        TasksRecTasks = itemList.ToList()
                    });
                }
            }

            StateHasChanged();
        }

        protected async Task EditItem(TasksRecTasksViewModel task, EmployeeTasks tasks)
        {
            selectedEmployeeTask = tasks;
            SelectedTask = task;
            IsAll = false;
            await Task.Delay(5);
            StateHasChanged();
        }

        protected async Task StartFixAllTasks(EmployeeTasks Model)
        {
            selectedEmployeeTask = Model;
            IsAll = true;
            await Task.Delay(5);
            StateHasChanged();
        }

        protected async Task FixAllTasks()
        {
            try
            {
                if (IsAll)
                {
                    employeeTasks.Remove(selectedEmployeeTask);

                    if (string.IsNullOrEmpty(SelectedEmployee))
                    {
                        _toastService.ShowError("Please Select Valid Employee");
                        return;
                    }
                    if (!activeEmployees.Contains(SelectedEmployee))
                    {
                        _toastService.ShowError("Please Select Valid Employee");
                        return;
                    }
                    foreach (var task in selectedEmployeeTask.TasksRecTasks)
                    {
                        task.PersonResponsible = SelectedEmployee;
                        var _b = await taskService.UpdateRecurringTask(task);
                    }

                    await RecurringTasksGrid.Refresh();
                    await RecurringTasksGrid.RefreshColumnsAsync();

                    _toastService.ShowSuccess("Tasks Assigned Successfully");

                }
                else
                {

                    if (string.IsNullOrEmpty(SelectedEmployee))
                    {
                        _toastService.ShowError("Please Select Valid Employee");
                        return;
                    }
                    if (!activeEmployees.Contains(SelectedEmployee))
                    {
                        _toastService.ShowError("Please Select Valid Employee");
                        return;
                    }

                    if (selectedEmployeeTask.TasksCount == 1)
                    {
                        employeeTasks.Remove(selectedEmployeeTask);
                    }

                    selectedEmployeeTask.TasksCount--;
                    selectedEmployeeTask.TasksRecTasks.Remove(SelectedTask);

                    SelectedTask.PersonResponsible = SelectedEmployee;
                    var _b = await taskService.UpdateRecurringTask(SelectedTask);

                    await childRecurringTasksGrid.Refresh();
                    await childRecurringTasksGrid.RefreshColumnsAsync();

                    await RecurringTasksGrid.Refresh();
                    await RecurringTasksGrid.RefreshColumnsAsync();

                    await RecurringTasksGrid.ExpandCollapseDetailRowAsync(selectedEmployeeTask);

                    _toastService.ShowSuccess("Task Assigned Successfully");
                }
            }
            catch (Exception Ex)
            {
                if (!string.IsNullOrEmpty(Ex.InnerException.Message))
                {
                    _toastService.ShowError(Ex.InnerException.Message + "Error");
                }
                else
                {
                    _toastService.ShowError(Ex.Message + "Error");
                }
            }

            selectedEmployeeTask = new EmployeeTasks();
            SelectedTask = new TasksRecTasksViewModel();

            StateHasChanged();
            await jSRuntime.InvokeAsync<object>("HideModal", "#FixModel");
            IsAll = false;
        }
    }

    public class EmployeeTasks
    {
        public string Name { get; set; }
        public int TasksCount { get; set; }
        public List<TasksRecTasksViewModel> TasksRecTasks { get; set; }
    }

}
