using Blazored.Toast.Services;
using CamcoTasks.Service.IService;
using CamcoTasks.ViewModels.TasksTasksTaskTypeDTO;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Syncfusion.Blazor.Grids;
using System.Linq;
using static CamcoTasks.Pages.Tasks.ViewOneTimeSubTasks;

namespace CamcoTasks.Pages.Tasks
{
    public class ViewTaskTypesModel : ComponentBase
    {
        [Inject]
        protected ITasksService taskService { get; set; }

        [Inject]
        protected IEmployeeService employeeService { get; set; }

        [Inject]
        protected NavigationManager navigationManager { get; set; }

        [Inject]
        private IToastService _toastService { get; set; }

        [Inject]
        private IJSRuntime jSRuntime { get; set; }
        public List<TasksTasksTaskTypeViewModel> Tasks { get; set; }
        public List<DDData> TaskTypes { get; set; } = new List<DDData>();
        public TasksTasksTaskTypeViewModel SelectedTask { get; set; } = new();
        public TasksTasksTaskTypeViewModel NewTask { get; set; } = new();
        public List<string> TaskTypesAll { get; set; } = new List<string>();
        public int TaskTypeValues { get; set; } = 1;
        protected SfGrid<TasksTasksTaskTypeViewModel> TaskGrid { get; set; }
        public TasksTasksTaskTypeViewModel NewTaskType { get; set; } = new TasksTasksTaskTypeViewModel();
        protected string TypeDropdownVal { get; set; } = "All";
        protected string OldTypeValue { get; set; } = "";
        protected void ReturnToTasks() => navigationManager.NavigateTo($"/viewtasks/");

        protected async Task ConfirmDelete()
        {
            try
            {
                await taskService.RemoveTaskType(SelectedTask);

                // var _b = await taskService.ClearTaskType(SelectedTask.TaskType);

                SelectedTask = new TasksTasksTaskTypeViewModel();
                NewTask = new TasksTasksTaskTypeViewModel();

                Tasks = (await taskService.GetTaskTypes()).ToList();
                Tasks = Tasks.OrderBy(a => a.TaskType).ToList();
                StateHasChanged();

                _toastService.ShowSuccess("Type Delete Successfully");
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    StateHasChanged();
                    _toastService.ShowError(ex.InnerException.Message);
                }
                else
                {
                    StateHasChanged();
                    _toastService.ShowError("Type Delete Error");
                }
            }
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                try
                {
                    await LoadPageDetails();
                }
                catch (Exception ex)
                {
                    _toastService.ShowError(ex.Message);
                }
            }

            StateHasChanged();
        }

        protected async Task CheckAddTaskType()
        {
            if (string.IsNullOrEmpty(NewTaskType.TaskType))
            {
                await jSRuntime.InvokeVoidAsync("AddRedBox", "AddTaskTypeId");
            }
            else
            {
                await jSRuntime.InvokeVoidAsync("RemoveRedBox", "AddTaskTypeId");
            }

            StateHasChanged();
        }

        protected async Task CheckAddTaskTypeFirstEmail()
        {
            if (string.IsNullOrEmpty(NewTaskType.Email))
            {
                await jSRuntime.InvokeVoidAsync("AddRedBox", "FirstEmailId");
            }
            else
            {
                await jSRuntime.InvokeVoidAsync("RemoveRedBox", "FirstEmailId");
            }

            StateHasChanged();
        }

        protected async Task HandleValidAddType()
        {
            try
            {
                bool isValid = true;
                if (string.IsNullOrEmpty(NewTaskType.TaskType))
                {
                    isValid = false;
                    await jSRuntime.InvokeVoidAsync("AddRedBox", "AddTaskTypeId");
                    StateHasChanged();
                }
                else
                {
                    await jSRuntime.InvokeVoidAsync("RemoveRedBox", "AddTaskTypeId");
                    StateHasChanged();
                }

                if (string.IsNullOrEmpty(NewTaskType.Email))
                {
                    await jSRuntime.InvokeVoidAsync("AddRedBox", "FirstEmailId");
                    isValid = false;
                    StateHasChanged();
                }
                else
                {
                    await jSRuntime.InvokeVoidAsync("RemoveRedBox", "FirstEmailId");
                    StateHasChanged();
                }

                if (!isValid)
                {
                    _toastService.ShowError("Please Fill All Required Field..!");
                    return;
                }

                if (TaskTypesAll.Contains(NewTaskType.TaskType))
                {
                    _toastService.ShowError("Error: Task Type Already Exists!");
                    return;
                }

                NewTaskType.Id = 0;
                var _b = await taskService.AddTaskType(NewTaskType);
                NewTaskType.Id = _b;
                if (NewTaskType.Id != 0)
                {

                    TaskTypesAll.Add(NewTaskType.TaskType);
                    TaskTypesAll = TaskTypesAll.OrderBy(x => x).ToList();

                    TaskTypeValues = 1;
                    TaskTypes = TaskTypesAll.Select(a => new DDData { Text = a, Value = TaskTypeValues++.ToString() }).OrderBy(a => a.Text).ToList();
                    TaskTypes.Insert(0, new DDData { Text = "ALL", Value = "0" });

                    await SetNewDefaults();

                    await jSRuntime.InvokeAsync<object>("HideModal", "#AddTypeDialog");

                    StateHasChanged();
                    _toastService.ShowSuccess("Task Type Added Successfully");
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    StateHasChanged();
                    _toastService.ShowError(ex.InnerException.Message);
                }
                else
                {
                    StateHasChanged();
                    _toastService.ShowError(ex.Message);
                }
            }

        }
        protected async Task SetNewDefaults()
        {
            //SelectedTask = new TasksTasksViewModel
            //{
            //    TasksTaskUpdates = new List<TasksTaskUpdatesViewModel>()
            //};

            //NewTask = new TasksTasksViewModel
            //{
            //    Initiator = "ARNOLD, RICHARD",
            //    Priority = 10,
            //    DateAdded = DateTime.Now,
            //    IsReviewed = false
            //-};
            //NewTask.TaskType = "April Worthington";

            NewTaskType = new TasksTasksTaskTypeViewModel();

            await InvokeAsync(StateHasChanged);
        }
        async Task LoadPageDetails()
        {
            Tasks = (await taskService.GetTaskTypes()).ToList();
            Tasks = Tasks.OrderBy(a => a.TaskType).ToList();

            var OneTimeTasks = (await taskService.GetAllTasks1());

            foreach (var task in Tasks)
            {
                task.TasksCount = OneTimeTasks.Where(x => x.TaskType == task.TaskType).Count();
            }

            StateHasChanged();
        }

        protected void EditItem(TasksTasksTaskTypeViewModel task)
        {
            SelectedTask = task;
            OldTypeValue = SelectedTask.TaskType;
            StateHasChanged();
        }

        protected async Task HandleValidSubmit()
        {
            try
            {
                var _b = await taskService.UpdateTaskType(SelectedTask);

                if (_b)
                {
                    if (OldTypeValue != SelectedTask.TaskType)
                    {
                        var models = await taskService.GetAllTasks(OldTypeValue);

                        foreach (var task in models)
                        {
                            task.TaskType = SelectedTask.TaskType;
                            var _c = await taskService.UpdateOneTask(task);
                        }
                    }

                    SelectedTask = new TasksTasksTaskTypeViewModel();
                    NewTask = new TasksTasksTaskTypeViewModel();
                    SelectedTask = new TasksTasksTaskTypeViewModel();

                    Tasks = (await taskService.GetTaskTypes()).ToList();
                    Tasks = Tasks.OrderBy(a => a.TaskType).ToList();

                    await TaskGrid.Refresh();
                    await TaskGrid.RefreshColumnsAsync();

                    await jSRuntime.InvokeAsync<object>("HideModal", "#EditTypeDialog");
                    StateHasChanged();
                    _toastService.ShowSuccess("Task Updated Successfully");
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    StateHasChanged();
                    _toastService.ShowError("Type Update Error");
                }
                else
                {
                    StateHasChanged();
                    _toastService.ShowError("Type Update Error");
                }
            }
        }

        protected void StartDeleteTaskType(TasksTasksTaskTypeViewModel task)
        {
            SelectedTask = task;
            StateHasChanged();
        }
        protected async Task DeleteTaskType()
        {
            try
            {
                var _b = await taskService.RemoveTaskType(SelectedTask);

                if (_b)
                {
                    SelectedTask = new TasksTasksTaskTypeViewModel();
                    NewTask = new TasksTasksTaskTypeViewModel();

                    Tasks = (await taskService.GetTaskTypes()).ToList();
                    Tasks = Tasks.OrderBy(a => a.TaskType).ToList();

                    await TaskGrid.Refresh();
                    await TaskGrid.RefreshColumnsAsync();

                    await jSRuntime.InvokeAsync<object>("HideModal", "#EditTypeDialog");
                    StateHasChanged();
                    _toastService.ShowSuccess("Task Updated Successfully");
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    StateHasChanged();
                    _toastService.ShowError("Type Update Error");
                }
                else
                {
                    StateHasChanged();
                    _toastService.ShowError("Type Update Error");
                }
            }
        }
    }
}
