using CamcoTasks.Service.IService;
using Microsoft.AspNetCore.Components;

namespace CamcoTasks.Pages.RecurringTasks.ViewRecurringTasks
{
    public partial class CompletedRecTask
    {
        [Inject]
        protected ITasksService TasksService { get; set; }
        [Parameter]
        public int TaskId { get; set; }
        [Parameter]
        public int CompleteValue { get; set; }

        protected string Message { get; set; } = string.Empty;

        protected bool IsLoading { get; set; } = true;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await CompleteTask();

                IsLoading = false;


                StateHasChanged();
            }
        }

        protected async Task CompleteTask()
        {
            var task = await TasksService.GetRecurringTaskById(TaskId);
            if (task == null)
            {
                Message = "No Task Found.";
                return;
            }

            task.CompletedOnTime += ";" + Convert.ToString(CompleteValue);
            task.CompletedOnTime = task.CompletedOnTime.Trim(';');

            var result = await TasksService.UpdateRecurringTask(task);
            if (result != null)
            {
                Message = "MARK SUCCESSFULLY COMPLETED.";
            }
            else
            {
                Message = "MARK ISN'T COMPLETED.";
            }
        }
    }
}
