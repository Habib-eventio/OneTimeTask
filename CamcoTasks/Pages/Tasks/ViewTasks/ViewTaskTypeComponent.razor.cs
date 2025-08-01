using CamcoTasks.Service.IService;
using CamcoTasks.ViewModels.TasksTasksDTO;
using CamcoTasks.ViewModels.TasksTaskUpdatesDTO;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Syncfusion.Blazor.Grids;

namespace CamcoTasks.Pages.Tasks.ViewTasks
{
    public partial class ViewTaskTypeComponent
    {
        [Inject]
        protected ITasksService taskService { get; set; }
        [Inject]
        private NavigationManager NavigationManager { get; set; }
        [Inject]
        private IJSRuntime jSRuntime { get; set; }

        [Parameter]
        public TasksTasksViewModel Tasks { get; set; }
        [Parameter]
        public EventCallback CallbackViewTaskTypesComponent { get; set; }

        public TasksTasksViewModel SelectedEmployeeTask { get; set; } = new TasksTasksViewModel();

        protected SfGrid<TasksTaskUpdatesViewModel> TypeUpdatesGrid { get; set; }

        protected int TaskIndex { get; set; } = 0;

        protected bool IsLoadTask { get; set; } = true;
        protected bool IsDoingTask { get; set; } = false;


        public List<TasksTasksViewModel> SelectedTypeTasks { get; set; }


        protected override async void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                await LoadContext();

                StateHasChanged();
            }
        }

        protected async Task LoadContext()
        {
            await jSRuntime.InvokeAsync<object>("ShowModal", "#ViewTypeTask");

            await LoadData();

            IsLoadTask = false;
        }

        protected async Task LoadData()
        {
            if (Tasks != null)
            {
                SelectedEmployeeTask = Tasks;
                SelectedTypeTasks = (await taskService
                    .GetAllTasks())
                    .ToList();
                TaskIndex = SelectedTypeTasks.FindIndex(x => x.Id == Tasks.Id);
                SelectedEmployeeTask.TasksTaskUpdates = (await taskService
                    .GetTaskUpdates(SelectedEmployeeTask.Id))
                    .ToList();
            }
        }

        protected async Task GetPrevTask()
        {
            await StartTask();

            if (TaskIndex <= 0)
            {
                TaskIndex = SelectedTypeTasks.Count - 1;
            }
            else
            {
                TaskIndex--;
            }
            var NextTask = SelectedTypeTasks[TaskIndex];
            SelectedEmployeeTask = NextTask;
            SelectedEmployeeTask.TasksTaskUpdates = (await taskService
                    .GetTaskUpdates(SelectedEmployeeTask.Id))
                    .ToList();

            await StopTask();
        }

        protected void ViewAllTypeTasks()
        {
            NavigationManager.NavigateTo($"/viewTaskByEmployee/{SelectedEmployeeTask.TaskType}");
        }

        protected async Task StartTask()
        {
            await Task.Run(() => IsDoingTask = true);
        }
        protected async Task StopTask()
        {
            await Task.Run(() => IsDoingTask = false);
        }

        protected async Task GetNextTask()
        {
            await StartTask();

            if ((TaskIndex + 1) >= SelectedTypeTasks.Count)
            {
                TaskIndex = 0;
            }
            else
            {
                TaskIndex++;
            }

            var NextTask = SelectedTypeTasks[TaskIndex];
            SelectedEmployeeTask = NextTask;
            SelectedEmployeeTask.TasksTaskUpdates = (await taskService
                    .GetTaskUpdates(SelectedEmployeeTask.Id)).ToList();

            await StopTask();
        }

        protected async Task CloseComponent()
        {
            await jSRuntime.InvokeAsync<object>("HideModal", "#ViewTypeTask");
            await CallbackViewTaskTypesComponent.InvokeAsync();
        }
    }
}
