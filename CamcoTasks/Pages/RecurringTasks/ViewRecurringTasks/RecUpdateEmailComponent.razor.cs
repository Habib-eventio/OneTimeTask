using CamcoTasks.Service.IService;
using CamcoTasks.Infrastructure.Common.Email;
using CamcoTasks.ViewModels.PageLoadTimeDTO;
using CamcoTasks.ViewModels.TasksTaskUpdatesDTO;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Syncfusion.Blazor.Inputs;
using System.Text;
using CamcoTasks.Infrastructure.Defaults;

namespace CamcoTasks.Pages.RecurringTasks.ViewRecurringTasks
{
    public partial class RecUpdateEmailComponent
    {
        [Inject]
        protected IEmployeeService employeeService { get; set; }
        [Inject]
        private IEmailService emailService { get; set; }
        [Inject]
        protected ITasksService taskService { get; set; }
        [Inject]
        private NavigationManager navigationManager { get; set; }
        [Inject]
        private IJSRuntime jSRuntime { get; set; }
        [Inject]
        protected IPageLoadTimeService pageLoadTimeService { get; set; }

        [Parameter]
        public TasksTaskUpdatesViewModel UpdateTaskForEmail { get; set; }
        [Parameter]
        public EventCallback<string> SuccessMessageRecUpedateEmailComponent { get; set; }
        [Parameter]
        public EventCallback CloseRecUpedateEmailComponent { get; set; }

        protected SfTextBox EmailNoteRef { get; set; }
        protected Dictionary<string, object> htmlAttribute = new Dictionary<string, object>() { { "rows", "4" } };

        protected string UpdateRespondBody { get; set; } = "";
        protected string RespondBody { get; set; } = "";
        protected string UpdateMailError { get; set; }

        protected PageLoadTimeViewModel PageLoadTime { get; set; } = new PageLoadTimeViewModel()
        {
            PageName = "RecurringTask",
            SectionName = "UpdateEmailComponnet",
            DateCreated = DateTime.Now
        };


        protected override async Task OnInitializedAsync()
        {
            await Task.Run(() => PageLoadTime.StartTime = DateTime.Now);
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await jSRuntime.InvokeAsync<object>("ShowModal", "#RespondModal");

                await PageLoadTimeCalculation();
            }
        }

        protected async Task PageLoadTimeCalculation()
        {
            PageLoadTime.EndTime = DateTime.Now;
            await pageLoadTimeService.InsertAsync(PageLoadTime);
        }

        protected async Task SendUpdatedEmail()
        {
            if (string.IsNullOrEmpty(UpdateRespondBody))
            {
                await jSRuntime.InvokeVoidAsync("AddRedBox", "UpdateMailBody");
                UpdateMailError = "Please Enter a Note";
                return;
            }
            else
            {
                await jSRuntime.InvokeVoidAsync("RemoveRedBox", "UpdateMailBody");
            }

            var Task = await taskService.GetRecurringTaskById((int)UpdateTaskForEmail.RecurringID);

            var employee = await employeeService.GetEmployee(Task.PersonResponsible);

            StringBuilder customBody = new();
            customBody.Append($"<p>Note: {UpdateRespondBody}</p>");
            customBody.Append($@"<h2 style=""text-decoration: underline; font-weight:bold; margin-bottom:5px;"">Recurring Task Description: </h2>
                <h4 style=""margin-top: 9px; margin-left: 5px;"">{Task.Description.ToUpper()}</h4>");
            customBody.Append($@"<h2 style=""text-decoration: underline; font-weight:bold; margin-bottom:5px;"">Rich Arnold Response: </h2>
                <h4 style=""margin-top: 9px; margin-left: 5px;"">{RespondBody}</h4>");
            customBody.Append($@"<p><b>Link To Task: </b>
                <a 
                 href=""{navigationManager.BaseUri}viewrecurringtasks/{employee.FirstName}/{employee.LastName}""
                 target=""_blank"">{employee.FirstName} {employee.LastName}</a>
            </p>");

            if (!string.IsNullOrEmpty(UpdateTaskForEmail.FileLink))
                customBody.Append($"<p><b>File: </b>{UpdateTaskForEmail.FileLink}</p>");


            string Subject = "RICH ARNOLD SENT A RESPONSE";

            string body = EmailDefaults.GenerateEmailTemplate("Tasks", customBody.ToString());
            await emailService.SendEmailAsync(EmailTypes.ActionBasedResponseForRecurringTask,
                Array.Empty<string>(), Subject, body, string.Empty, new string[] { employee.Email });

            await taskService.UpdateRecurringTask(Task);

            await jSRuntime.InvokeAsync<object>("HideModal", "#RespondModal");

            await SuccessMessageRecUpedateEmailComponent.InvokeAsync("Email Send Successfull..");
            await CloseRecUpedateEmailComponent.InvokeAsync();
        }

        protected async Task CloseComponent()
        {
            await jSRuntime.InvokeAsync<object>("HideModal", "#RespondModal");
            await CloseRecUpedateEmailComponent.InvokeAsync();
        }
    }
}
