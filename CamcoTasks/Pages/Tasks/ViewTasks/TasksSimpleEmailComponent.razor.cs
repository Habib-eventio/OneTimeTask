using CamcoTasks.Service.IService;
using CamcoTasks.Infrastructure.Common.Email;
using CamcoTasks.ViewModels.TasksTasksDTO;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Syncfusion.Blazor.Inputs;
using DocumentFormat.OpenXml.Wordprocessing;
using CamcoTasks.Infrastructure.Defaults;

namespace CamcoTasks.Pages.Tasks.ViewTasks
{
    public partial class TasksSimpleEmailComponent
    {
        [Inject]
        private IJSRuntime jSRuntime { get; set; }
        [Inject]
        protected ITasksService taskService { get; set; }
        [Inject]
        protected NavigationManager navigationManager { get; set; }
        [Inject]
        private IEmailService emailService { get; set; }
        [Inject]
        private NavigationManager NavigationManager { get; set; }

        [Parameter]
        public TasksTasksViewModel EmailTask { get; set; }
        [Parameter]
        public EventCallback<Dictionary<string, string>> CallbackMessageTasksSimpleEmail { get; set; }

        protected SfTextBox SimpleEmailNoteRef { get; set; }

        protected Dictionary<string, object> htmlAttribute = new Dictionary<string, object>() { { "rows", "4" } };

        protected string RespondBody { get; set; } = "";
        protected string EmailError { get; set; }


        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await LoadContext();

                StateHasChanged();
            }
        }

        protected async Task LoadContext()
        {
            await jSRuntime.InvokeAsync<object>("ShowModal", "#SimpleEmailModal");
        }

        protected async Task SendTaskEmail()
        {
            if (EmailTask != null)
            {
                if (string.IsNullOrEmpty(RespondBody))
                {
                    EmailError = "Please Enter a Note";
                    await jSRuntime.InvokeVoidAsync("AddRedBox", "OneTimeTaskSendMailId");
                    return;
                }

                await jSRuntime.InvokeAsync<object>("HideModal", "#SimpleEmailModal");
                var employee = await taskService.GetTaskType(EmailTask.TaskType);

                if (employee != null)
                {
                    if (employee.Email != null && !string.IsNullOrEmpty(employee.Email))
                    {
                        string body = "<br><h2 style=\"text-decoration: underline; font-weight:bold; margin-bottom:5px;\">" +
                    "Recurring Task Description: </h2>"
                    + "<h4 style=\"margin-top: 9px; margin-left: 5px;\">" + EmailTask.Description + "</h4>" +
                    "<br><h2 style=\"text-decoration: underline; font-weight:bold; margin-bottom:5px;\">" +
                    "Rich Arnold Says Regarding This Task: </h2>" + "<h4 style=\"margin-top: 9px; margin-left: 5px;\">"
                    + RespondBody + "</h4>"

                    + "<br><br><label style=\"font-weight:bold; font-size: 20px;\">Link To One-Time Tasks:</label> " +
                       $"<a href=\"{NavigationManager.BaseUri}viewtasks\" target=\"_blank\"> "
                       + $" Recurring Task #{EmailTask.Id}</a>";

                        string Subject = "RICH ARNOLD SENT A RESPONSE";

                        // EmailQueueViewModel emailqueue = new()
                        // {
                        //     Body = body,
                        //     HasBeenSent = false,
                        //     SendTo = !string.IsNullOrEmpty(employee.Email) ? employee.Email : employee.Email2,
                        //     Subject = Subject,
                        //     EmailTypeId = 723
                        // };

                        body = EmailDefaults.GenerateEmailTemplate("Tasks", body);
                        await emailService.SendEmailAsync(EmailTypes.ActionBasedResponseForOneTimeTask,
                            Array.Empty<string>(), Subject, body, string.Empty, new string[] { !string.IsNullOrEmpty(employee.Email) ? employee.Email : employee.Email2 });

                        if (EmailTask.EmailCount == null)
                            EmailTask.EmailCount = 0;

                        EmailTask.EmailCount++;

                        await taskService.UpdateOneTask(EmailTask);

                        await CallbackMessageTasksSimpleEmail.InvokeAsync(new Dictionary<string, string>() {
                            { "Email Send Information", "An Email Has Been Added To Queue"} });
                    }
                    else
                    {
                        await CallbackMessageTasksSimpleEmail.InvokeAsync(new Dictionary<string, string>() {
                        { "Email Send Information", "Employee is not found!"} });
                    }
                }
                else
                {
                    await CallbackMessageTasksSimpleEmail.InvokeAsync(new Dictionary<string, string>() {
                    { "Email Send Information", "Employee is not found!"} });
                }
            }
        }

        protected async Task CloseComponent()
        {
            await jSRuntime.InvokeAsync<object>("HideModal", "#SimpleEmailModal");
            await CallbackMessageTasksSimpleEmail.InvokeAsync(new Dictionary<string, string>());
        }
    }
}
