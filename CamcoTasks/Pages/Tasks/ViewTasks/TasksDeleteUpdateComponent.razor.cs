using CamcoTasks.Service.IService;
using CamcoTasks.ViewModels.TasksTaskUpdatesDTO;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace CamcoTasks.Pages.Tasks.ViewTasks
{
    public partial class TasksDeleteUpdateComponent
    {
        [Inject]
        protected ITasksService taskService { get; set; }
        [Inject]
        private IJSRuntime jSRuntime { get; set; }
        [Inject]
        protected ILogger<TasksDeleteUpdateComponent> logger { get; set; }

        [Parameter]
        public TasksTaskUpdatesViewModel DeleteViewModelUpdate { get; set; }
        [Parameter]
        public EventCallback<Dictionary<string, string>> CallbackMessageTasksDeleteUpdateComponent { get; set; }
        [Parameter]
        public EventCallback<bool> RefreshTasksUpdateViewComponent { get; set; }


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
            await jSRuntime.InvokeAsync<object>("ShowModal", "#deleteModal");
        }

        protected async Task DeleteUpdate(TasksTaskUpdatesViewModel tasksTaskUpdates)
        {
            try
            {
                tasksTaskUpdates.IsDeleted = true;
                var result = await taskService.UpdateTaskUpdate(tasksTaskUpdates);

                if (result)
                {
                    await CallbackMessageTasksDeleteUpdateComponent.InvokeAsync(new Dictionary<string, string>()
                    {
                        { "One Time Task Information", "Update Has Been Deleted!"}
                    });
                    await RefreshTasksUpdateViewComponent.InvokeAsync(true);
                }
                else
                {
                    await CallbackMessageTasksDeleteUpdateComponent.InvokeAsync(new Dictionary<string, string>()
                    {
                        { "One Time Task Information", "Update Has not Been Deleted!"}
                    });
                }

            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    logger.LogError(ex.InnerException.Message, "Update Delete Error!");
                }
                else
                {
                    logger.LogError(ex.Message, "Update Delete Error!");
                }
            }
        }

        protected async Task CloseComponent()
        {
            await jSRuntime.InvokeAsync<object>("HideModal", "#deleteModal");
            await CallbackMessageTasksDeleteUpdateComponent.InvokeAsync(new Dictionary<string, string>());
        }
    }
}
