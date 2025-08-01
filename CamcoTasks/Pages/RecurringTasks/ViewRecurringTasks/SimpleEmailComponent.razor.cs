using Blazored.Toast.Services;
using CamcoTasks.Service.IService;
using CamcoTasks.Infrastructure.Common.Email;
using CamcoTasks.ViewModels.PageLoadTimeDTO;
using CamcoTasks.ViewModels.TasksRecTasksDTO;
using CamcoTasks.ViewModels.TasksTaskUpdatesDTO;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Syncfusion.Blazor.Inputs;
using CamcoTasks.Infrastructure.Defaults;

namespace CamcoTasks.Pages.RecurringTasks.ViewRecurringTasks
{
    public partial class SimpleEmailComponent
    {
        [Inject]
        private IJSRuntime jSRuntime { get; set; }
        [Inject]
        protected ITasksService taskService { get; set; }
        [Inject]
        protected IEmployeeService employeeService { get; set; }
        [Inject]
        private IEmailService emailService { get; set; }
        [Inject]
        private NavigationManager navigationManager { get; set; }
        [Inject]
        private IToastService _toastService { get; set; }
        [Inject]
        protected IPageLoadTimeService pageLoadTimeService { get; set; }

        [Parameter]
        public TasksRecTasksViewModel EmailTask { get; set; }
        [CascadingParameter]
        public ViewRecurringTasks ViewRecurringTasksRef { get; set; }

        protected SfTextBox SimpleEmailNoteRef { get; set; }
        protected TasksTaskUpdatesViewModel LatestRecurringTaskUpdate { get; set; }

        protected string ErrorMessage { get; set; }
        protected string SimpleRespondBody { get; set; } = "";

        protected Dictionary<string, object> htmlAttribute = new Dictionary<string, object>() { { "rows", "4" } };

        protected bool IsDoing { get; set; } = false;

        protected PageLoadTimeViewModel PageLoadTime { get; set; } = new PageLoadTimeViewModel()
        {
            PageName = "RecurringTask",
            SectionName = "SimpleEmailComponnnet",
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
                await LoadSimpleEmailComponentData();

                StateHasChanged();

                await PageLoadTimeCalculation();
            }
        }
        protected async Task PageLoadTimeCalculation()
        {
            PageLoadTime.EndTime = DateTime.Now;
            await pageLoadTimeService.InsertAsync(PageLoadTime);
        }


        protected async Task LoadSimpleEmailComponentData()
        {
            await jSRuntime.InvokeAsync<object>("ShowModal", "#SimpleEmailModal");
            if (EmailTask != null)
            {
                LatestRecurringTaskUpdate = await taskService.GetRecurringTaskLatestUpdateAsync(EmailTask.Id);
            }
        }

        protected async Task SendTaskEmail()
        {
            IsDoing = true;
            string emailSendTo = string.Empty;

            if (string.IsNullOrEmpty(SimpleRespondBody))
            {
                await jSRuntime.InvokeVoidAsync("AddRedBox", "EmailBodyNote");
                ErrorMessage = "Please Enter a Note";
                IsDoing = false;
                return;
            }
            else if (!string.IsNullOrEmpty(SimpleRespondBody))
            {
                await jSRuntime.InvokeVoidAsync("RemoveRedBox", "EmailBodyNote");
            }

            await jSRuntime.InvokeAsync<object>("HideModal", "#SimpleEmailModal");

            string[] employeResp = EmailTask.PersonResponsible.Split(";");

            if (!employeResp.Any())
            {
                return;
            }

            foreach (string emp in employeResp)
            {
                var employee = await employeeService.GetEmployee(emp);

                if (employee != null && !string.IsNullOrEmpty(employee.Email))
                {
                    emailSendTo += employee.Email + ";";
                }
                else
                {
                    _toastService.ShowWarning("Email Sending Error, Employee Has No Email");
                }
            }

            if (!string.IsNullOrEmpty(emailSendTo))
                emailSendTo = emailSendTo.Remove(emailSendTo.Length - 1);

            if (!string.IsNullOrEmpty(emailSendTo))
            {
                string body = "<br><h2 style=\"text-decoration: underline; font-weight:bold; margin-bottom:5px;\">" +
            "RECURRING TASK DESCRIPTION: </h2>"
            + "<h4 style=\"margin-top: 9px; margin-left: 5px;\">" + EmailTask.Description.ToUpper() + "</h4>" +
            "<br><h2 style=\"text-decoration: underline; font-weight:bold; margin-bottom:5px;\">" +
            "RICH ARNOLD SAYS REGARDING THIS TASK: </h2>" + "<h4 style=\"margin-top: 9px; margin-left: 5px;\">"
            + SimpleRespondBody + "</h4>" +
             "<br><h2 style=\"text-decoration: underline; font-weight:bold; margin-bottom:5px;\">" +
            "LAST UPDATE: </h2>" + "<h4 style=\"margin-top: 9px; margin-left: 5px;\">"
            + LatestRecurringTaskUpdate.Update + "</h4>" 
            + "<br><br><label style=\"font-weight:bold; font-size: 20px;\">Link To Task:</label> " +
               $"<a href=\"{navigationManager.BaseUri}viewrecurringtasks/OpenTask/{EmailTask.Id}\" target=\"_blank\"> "
               + $" Recurring Task #{EmailTask.Id}</a>";

                string Subject = "RICH ARNOLD SENT A RESPONSE";

                body = EmailDefaults.GenerateEmailTemplate("Tasks", body);
                await emailService.SendEmailAsync(EmailTypes.ActionBasedResponseForRecurringTask,
                Array.Empty<string>(), Subject, body, string.Empty, new string[] { emailSendTo });

                if (EmailTask.EmailCount == null) EmailTask.EmailCount = 0;
                EmailTask.EmailCount++;
                await taskService.UpdateRecurringTask(EmailTask);

                var SystemUpdate = new TasksTaskUpdatesViewModel
                {
                    DueDate = EmailTask.UpcomingDate,
                    RecurringID = EmailTask.Id,
                    Update = $"Rich Arnold ''Email'' you to get the recurring Task # " + EmailTask.Id.ToString() + " done.",
                    UpdateDate = DateTime.Now,
                    IsDeleted = false,
                    UpdateId = 0,
                };
                SystemUpdate.UpdateId = await taskService.AddTaskUpdate(SystemUpdate);

                SimpleRespondBody = string.Empty;
                EmailTask = new TasksRecTasksViewModel();

                await ViewRecurringTasksRef.SuccessMessageViewRecurringTasksComponent("An Email Has Been Added To Queue");
            }
            else
            {
                await ViewRecurringTasksRef.SuccessMessageViewRecurringTasksComponent("Email is not found!");
            }



            await ViewRecurringTasksRef.DeactivateSimpleEmailComponent();
        }

        protected async Task CheckEmailBodyNote()
        {
            if (string.IsNullOrEmpty(SimpleRespondBody))
            {
                await jSRuntime.InvokeVoidAsync("AddRedBox", "EmailBodyNote");
            }
            else
            {
                await jSRuntime.InvokeVoidAsync("RemoveRedBox", "EmailBodyNote");
            }
        }

        protected async Task CloseComponent()
        {
            await jSRuntime.InvokeAsync<object>("HideModal", "#SimpleEmailModal");
            await ViewRecurringTasksRef.DeactivateSimpleEmailComponent();
        }
    }
}
