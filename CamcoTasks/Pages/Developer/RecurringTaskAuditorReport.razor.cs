using Blazored.Toast.Services;
using CamcoTasks.Library;
using CamcoTasks.Service.IService;
using Microsoft.AspNetCore.Components;

namespace CamcoTasks.Pages.Developer
{
    public partial class RecurringTaskAuditorReport
    {
        [Inject]
        private IToastService _toastService { get; set; }
        [Inject]
        protected IRecurringTaskReportService recurringTaskReportService { get; set; }

        protected bool IsAuthenticate { get; set; } = false;
        protected bool IsDoing { get; set; } = false;

        protected DateTime ReportDate { get; set; } = DateTime.Now;

        private string Password { get; set; }
        protected string Error { get; set; } = null;


        private void Authenticate(string password)
        {
            if (string.IsNullOrEmpty(Password))
            {
                Error = "Please enter password...";
                return;
            }

            if (AppInformation.CheckLogerAuthentication(password))
            {
                IsAuthenticate = true;
                Error = null;
            }
            else
                Error = "Your password is worng.";
        }

        protected async void GenareatRepoart()
        {
            await Task.Run(() => IsDoing = true);
            bool isSuccess = await recurringTaskReportService.SendRecurringTaskAuditReportAsync(ReportDate);
            await Task.Run(() => IsDoing = false);
            StateHasChanged();

            if (isSuccess)
                _toastService.ShowSuccess("Repoart Generat Successfull...");
            else
                _toastService.ShowError("No Report Found. Repoart Isn't Generat Successfull...");
        }
    }
}
