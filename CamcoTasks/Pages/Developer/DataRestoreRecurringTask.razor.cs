using Blazored.Toast.Services;
using CamcoTasks.Library;
using CamcoTasks.Service.IService;
using CamcoTasks.ViewModels.TasksFrequencyListDTO;
using CamcoTasks.ViewModels.TasksRecTasksDTO;
using CamcoTasks.ViewModels.TasksTaskUpdatesDTO;
using Microsoft.AspNetCore.Components;

namespace CamcoTasks.Pages.Developer
{
    partial class DataRestoreRecurringTask
    {
        [Inject] protected ITasksService taskService { get; set; }
        [Inject] private IToastService _toastService { get; set; }

        protected List<TasksRecTasksViewModel> Tasks { get; set; } = new List<TasksRecTasksViewModel>();
        protected IEnumerable<TasksFrequencyListViewModel> TasksFrequency { get; set; }

        protected bool IsAuthenticate { get; set; } = false;
        protected bool IsComplete { get; set; } = false;
        protected string Error { get; set; } = null;
        private string Password { get; set; }

        private void Authenticate(string password)
        {
            if (AppInformation.CheckLogerAuthentication(password))
            {
                IsAuthenticate = true;
                LoadData();
                Error = null;
            }
            else
                Error = "Your password is worng.";
        }

        protected async Task LoadData()
        {
            Tasks = (taskService.GetRecurringTasksSync(x => !x.IsDeleted && x.IsDeactivated == false && x.ParentTaskId == null)).ToList();
            TasksFrequency = await taskService.GetTaskFreqsSync();
        }

        protected void FixSingleTaskDueDate(TasksRecTasksViewModel task)
        {
            TasksFrequencyListViewModel frequency = TasksFrequency.FirstOrDefault(x => x.Frequency == task.Frequency);

            if (task.DateCompleted != null)
            {
                DateTime newUpcommingDate = task.DateCompleted.Value.Date.AddDays(frequency.Days);

                if (task.UpcomingDate != null)
                {
                    if (task.UpcomingDate != newUpcommingDate)
                        task.UpcomingDate = newUpcommingDate;
                }
                else
                {
                    task.UpcomingDate = newUpcommingDate;
                }
            }
            else
            {
                task.DateCompleted = DateTime.Now;
                task.UpcomingDate = task.DateCompleted.Value.Date.AddDays(frequency.Days);

            }

            var response = taskService.UpdateRecurringTaskSync(task);
            if (response != null)
                _toastService.ShowSuccess("Completed Date and Due Date are fixed");
            else
                _toastService.ShowSuccess("There is an issue with update.");
        }

        protected async void FixSingleTaskUpdate(TasksRecTasksViewModel task)
        {
            List<TasksTaskUpdatesViewModel> updateList = (await taskService.GetTaskUpdatesWithAllFieldSync(task.Id)).ToList();
            int updateListLength = updateList.Count();

            if (updateList.Any())
            {

                TasksTaskUpdatesViewModel update = updateList[0];
                if (update.DueDate != task.UpcomingDate)
                {
                    update.DueDate = task.UpcomingDate;
                    var response = taskService.UpdateTaskUpdate(update);

                    if (response != null)
                        _toastService.ShowSuccess("Last update due date fixed fixed");
                    else
                        _toastService.ShowSuccess("There is an issue with update.");
                }
            }
        }

    }
}
