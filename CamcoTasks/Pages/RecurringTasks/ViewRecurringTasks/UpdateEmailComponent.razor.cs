using CamcoTasks.Service.IService;
using CamcoTasks.Infrastructure.Common.Email;
using CamcoTasks.ViewModels.PageLoadTimeDTO;
using CamcoTasks.ViewModels.TasksTaskUpdatesDTO;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Syncfusion.Blazor.Inputs;
using DocumentFormat.OpenXml.Wordprocessing;
using CamcoTasks.Infrastructure.Defaults;

namespace CamcoTasks.Pages.RecurringTasks.ViewRecurringTasks
{
    public partial class UpdateEmailComponent
    {
        [Inject]
        private IJSRuntime jSRuntime { get; set; }
        [Inject]
        protected ITasksService taskService { get; set; }
        [Inject]
        protected IEmployeeService employeeService { get; set; }
        [Inject]
        private NavigationManager navigationManager { get; set; }
        [Inject]
        private IEmailService emailService { get; set; }
        [Inject]
        protected IPageLoadTimeService pageLoadTimeService { get; set; }

        protected string UpdateRespondBody { get; set; } = "";
        protected string RespondBody { get; set; } = "";

        protected Dictionary<string, object> htmlAttribute = new Dictionary<string, object>() { { "rows", "4" } };

        protected SfTextBox EmailNoteRef { get; set; }

        protected TasksTaskUpdatesViewModel SelectedUpdateViewModel { get; set; } = new TasksTaskUpdatesViewModel();
        protected PageLoadTimeViewModel PageLoadTime { get; set; } = new PageLoadTimeViewModel()
        {
            PageName = "RecurringTask",
            SectionName = "UpdateEmailComponent",
            DateCreated = DateTime.Now
        };


        protected override async Task OnInitializedAsync()
        {
            await Task.Run(() => PageLoadTime.StartTime = DateTime.Now);
        }

        protected override async void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
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
                return;
            }
            else
            {
                await jSRuntime.InvokeVoidAsync("RemoveRedBox", "UpdateMailBody");
            }

            var Task = await taskService.GetRecurringTaskById((int)SelectedUpdateViewModel.RecurringID);

            var employee = await employeeService.GetEmployee(Task.PersonResponsible);

            string body = "Note: " + UpdateRespondBody + "<br>";
            body += "<br><h2 style=\"text-decoration: underline; font-weight:bold; margin-bottom:5px;\">" +
               "Recurring Task Description: </h2>"
               + "<h4 style=\"margin-top: 9px; margin-left: 5px;\">" + Task.Description.ToUpper() + "</h4>" +
               "<br><h2 style=\"text-decoration: underline; font-weight:bold; margin-bottom:5px;\">" +
               "Rich Arnold Response: </h2>" + "<h4 style=\"margin-top: 9px; margin-left: 5px;\">" + RespondBody + "</h4>"

               + "<br><br><label style=\"font-weight:bold; font-size: 20px;\">Link To Task:</label> " +
                  $"<a href=\"{navigationManager.BaseUri}viewrecurringtasks/" +
                  employee.FirstName + "/" + employee.LastName + "\" target=\"_blank\"> "
                  + $" {employee.FirstName} {employee.LastName} </a>";

            if (!string.IsNullOrEmpty(SelectedUpdateViewModel.FileLink))
                body += "<br><label style=\"font-weight:bold\"> File: </label>" + SelectedUpdateViewModel.FileLink;


            string Subject = "RICH ARNOLD SENT A RESPONSE";
            body = EmailDefaults.GenerateEmailTemplate("Tasks", body);
            await emailService.SendEmailAsync(EmailTypes.ActionBasedResponseForRecurringTask,
                Array.Empty<string>(), Subject, body, string.Empty, new string[] { employee.Email });

            await taskService.UpdateRecurringTask(Task);

            await jSRuntime.InvokeAsync<object>("HideModal", "#RespondModal");

            RespondBody = string.Empty;
            SelectedUpdateViewModel = new TasksTaskUpdatesViewModel() { Update = string.Empty };
        }

        protected async Task CheckUpdateMailBody()
        {
            if (string.IsNullOrEmpty(UpdateRespondBody))
            {
                await jSRuntime.InvokeVoidAsync("AddRedBox", "UpdateMailBody");
            }
            else
            {
                await jSRuntime.InvokeVoidAsync("RemoveRedBox", "UpdateMailBody");
            }
        }
    }
}
