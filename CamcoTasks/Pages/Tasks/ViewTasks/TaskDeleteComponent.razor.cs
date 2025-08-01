using CamcoTasks.Service.IService;
using CamcoTasks.ViewModels.TasksTasksDTO;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace CamcoTasks.Pages.Tasks.ViewTasks
{
    public partial class TaskDeleteComponent
    {
        [Inject]
        protected ITasksService taskService { get; set; }
        [Inject]
        private IJSRuntime jSRuntime { get; set; }
        [Inject]
        protected ILogger<TaskDeleteComponent> Logger { get; set; }

        [Parameter]
        public TasksTasksViewModel TaskViewModel { get; set; }
        [Parameter]
        public EventCallback<TasksTasksViewModel> SuccessMessageDelete { get; set; }
        [Parameter]
        public EventCallback<bool> RefreshParentComponent { get; set; }


        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await LoadDeactiveComponentData();
            }
        }

        protected async Task LoadDeactiveComponentData()
        {
            await jSRuntime.InvokeAsync<object>("ShowModal", "#deleteTask");
        }

        protected async Task DeleteTask()
        {
            try
            {
                TaskViewModel.IsDeleted = true;
                var response = taskService.RemoveOneTask(TaskViewModel);

                await SuccessMessageDelete.InvokeAsync(TaskViewModel);

                await RefreshParentComponent.InvokeAsync(true);
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    Logger.LogWarning(ex.InnerException.Message, "Task Delete Error");
                }
                else
                {
                    Logger.LogWarning(ex.Message, "Task Delete Error");
                }
            }
        }

        protected async Task CloseComponent()
        {
            await jSRuntime.InvokeAsync<object>("HideModal", "#deleteTask");
            TasksTasksViewModel model = null;
            await SuccessMessageDelete.InvokeAsync(model);
        }
    }
}
