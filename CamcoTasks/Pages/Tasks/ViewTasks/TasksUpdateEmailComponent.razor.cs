using CamcoTasks.Service.IService;
using CamcoTasks.Infrastructure.Common.Email;
using CamcoTasks.ViewModels.TasksTaskUpdatesDTO;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Syncfusion.Blazor.Inputs;
using CamcoTasks.Infrastructure.Defaults;

namespace CamcoTasks.Pages.Tasks.ViewTasks
{
    public partial class TasksUpdateEmailComponent
    {
        [Inject]
        private IJSRuntime jSRuntime { get; set; }
        [Inject]
        protected ITasksService taskService { get; set; }
        [Inject]
        private IEmailService emailService { get; set; }

        [Parameter]
        public TasksTaskUpdatesViewModel SelectedUpdateViewModel { get; set; }
        [Parameter]
        public EventCallback<Dictionary<string, string>> CallbackMessageTasksUpdateEmail { get; set; }

        protected SfTextBox EmailNoteRef { get; set; }

        protected string RespondBody2 { get; set; } = "";
        protected string ErrorEmail { get; set; }


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
            await jSRuntime.InvokeAsync<object>("ShowModal", "#RespondModal");
        }

        protected async Task SendRespond()
        {
            if (string.IsNullOrEmpty(RespondBody2))
            {
                ErrorEmail = "Please Enter a Note";
                await jSRuntime.InvokeVoidAsync("AddRedBox", "OneTaskEditNoteId");
                return;
            }
            else
            {
                await jSRuntime.InvokeVoidAsync("RemoveRedBox", "OneTaskEditNoteId");
            }

            var Task = await taskService.GetTaskById((int)SelectedUpdateViewModel.TaskID);
            await jSRuntime.InvokeAsync<object>("HideModal", "#RespondModal");

            var taskType = await taskService.GetTaskType(Task.TaskType);

            if (taskType == null)
            {
                await CallbackMessageTasksUpdateEmail.InvokeAsync(new Dictionary<string, string>()
                {
                    {"Update Email Information", "Task Type not found Error, can't send email." }
                });
                return;
            }

            if (string.IsNullOrEmpty(taskType.Email) && string.IsNullOrEmpty(taskType.Email2))
            {
                await CallbackMessageTasksUpdateEmail.InvokeAsync(new Dictionary<string, string>()
                {
                    {"Update Email Information", "No Email found for task type." }
                });
                return;
            }

            string body = "<br><h2 style=\"text-decoration: underline; font-weight:bold; margin-bottom:5px;\">" +
                "One-Time Task Description: </h2>"
                + "<h4 style=\"margin-top: 9px; margin-left: 5px;\">" + Task.Description + "</h4>" +
                "<br><h2 style=\"text-decoration: underline; font-weight:bold; margin-bottom:5px;\">" +
                "Rich Arnold Response: </h2>" + "<h4 style=\"margin-top: 9px; margin-left: 5px;\">" + RespondBody2 + "</h4>";

            if (!string.IsNullOrEmpty(SelectedUpdateViewModel.FileLink))
                body += "<br><label style=\"font-weight:bold\"> File: </label>" + SelectedUpdateViewModel.FileLink;


            string Subject = "RICH ARNOLD SENT A RESPONSE";

            // EmailQueueViewModel emailqueue = new()
            // {
            //     Body = body,
            //     HasBeenSent = false,
            //     SendTo = string.IsNullOrEmpty(taskType.Email) ? taskType.Email2 : taskType.Email,
            //     Subject = Subject,
            //     EmailTypeId = 723
            // };

            body = EmailDefaults.GenerateEmailTemplate("Tasks", body);
            await emailService.SendEmailAsync(EmailTypes.ActionBasedResponseForOneTimeTask,
            Array.Empty<string>(), Subject, body, string.Empty, new string[] { string.IsNullOrEmpty(taskType.Email) ? taskType.Email2 : taskType.Email });

            await CallbackMessageTasksUpdateEmail.InvokeAsync(new Dictionary<string, string>()
            {
                {"Update Email Information", "An Email Has Been Added To Queue" }
            });
        }

        protected async Task CloseComponent()
        {
            await jSRuntime.InvokeAsync<object>("HideModal", "#RespondModal");
            await CallbackMessageTasksUpdateEmail.InvokeAsync(new Dictionary<string, string>());
        }
    }
}
