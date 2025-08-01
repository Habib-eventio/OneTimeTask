using Blazored.Toast.Services;
using CamcoTasks.Data.Services;
using CamcoTasks.Service.IService;
using CamcoTasks.ViewModels.TasksTasksDTO;
using CamcoTasks.ViewModels.TasksTasksTaskTypeDTO;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Syncfusion.Blazor.Grids;

namespace CamcoTasks.Pages.Tasks
{
    public class ReAssignTasksModel : ComponentBase
    {
        [Inject]
        protected ITasksService taskService { get; set; }

        protected string TypeDropdownVal = "All";

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
        protected List<TasksTasksViewModel> Tasks { get; set; }
        protected List<TasksTasksTaskTypeViewModel> taskTypes { get; set; } = new List<TasksTasksTaskTypeViewModel>();
        public TasksTasksViewModel SelectedTask { get; set; }
        public TasksTasksTaskTypeViewModel SelectedEmployee { get; set; } = new TasksTasksTaskTypeViewModel();

        protected SfGrid<TasksTasksViewModel> TasksGrid { get; set; }
        protected async Task StartDeleteTask(TasksTasksViewModel Model)
        {
            SelectedTask = Model;
            await Task.Delay(10);
        }
        protected async Task DeleteTask(TasksTasksViewModel task)
        {
            SelectedTask = task;
            await Task.Delay(10);
        }
        protected async Task ConfirmDelete()
        {
            try
            {
                Tasks.Remove(SelectedTask);

                SelectedTask.IsDeleted = true;
                await taskService.UpdateOneTask(SelectedTask);

                await TasksGrid.Refresh();
                await TasksGrid.RefreshColumnsAsync();
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

            SelectedTask = new TasksTasksViewModel();

            await jSRuntime.InvokeAsync<object>("HideModal", "#deleteRecurringTask");
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
                    _toastService.ShowError(ex.Message + "Error");
                }
            }

            IsSpinner = false;

            StateHasChanged();
        }

        async Task LoadPageDetails()
        {
            taskTypes = (await taskService.GetTaskTypes()).ToList();
            Tasks = (await taskService.GetEmptyTaskTypes()).ToList();
            StateHasChanged();
        }

        protected async Task EditItem(TasksTasksViewModel task)
        {
            SelectedTask = task;
            await Task.Delay(5);
            StateHasChanged();
        }

        protected async Task StartFixTask(TasksTasksViewModel Model)
        {
            SelectedTask = Model;
            await Task.Delay(5);
            StateHasChanged();
        }
        protected void ReturnTasks()
        {
            navigationManager.NavigateTo($"/viewtasks/");
        }

        protected async Task CheckReAssignOneTimeTask()
        {
            if (SelectedEmployee.Id == 0 || SelectedEmployee.Id == null)
            {
                await jSRuntime.InvokeVoidAsync("AddRedBox", "ReAssignOneTimeTask");
            }
            else
            {
                await jSRuntime.InvokeVoidAsync("RemoveRedBox", "ReAssignOneTimeTask");
            }

            StateHasChanged();
        }
        protected async Task FixAllTasks()
        {
            try
            {
                if (SelectedEmployee.Id == 0 || SelectedEmployee.Id == null)
                {
                    _toastService.ShowError("Please Select Valid Employee");
                    await jSRuntime.InvokeVoidAsync("AddRedBox", "ReAssignOneTimeTask");
                    return;
                }

                else
                {
                    await jSRuntime.InvokeVoidAsync("RemoveRedBox", "ReAssignOneTimeTask");
                }

                var type = taskTypes.FirstOrDefault(x => x.Id == SelectedEmployee.Id);

                SelectedTask.TaskType = type.TaskType;
                var _b = await taskService.UpdateOneTask(SelectedTask);

                await TasksGrid.Refresh();
                await TasksGrid.RefreshColumnsAsync();

                _toastService.ShowSuccess("Task Assigned Successfully");
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

            SelectedTask = new TasksTasksViewModel();
            StateHasChanged();
            await jSRuntime.InvokeAsync<object>("HideModal", "#FixModel");
        }
    }

}
