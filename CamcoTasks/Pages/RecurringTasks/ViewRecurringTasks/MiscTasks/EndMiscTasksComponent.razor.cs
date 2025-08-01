using Blazored.Toast.Services;
using CamcoTasks.Service.IService;
using CamcoTasks.ViewModels.TimeTrackingTransactionTimelogDTO;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Components;

namespace CamcoTasks.Pages.RecurringTasks.ViewRecurringTasks.MiscTasks
{
    public partial class EndMiscTasksComponent : IAsyncDisposable
    {
        protected bool IsVisibility = true;
        protected bool IsLoading = true;

        private TimeTrackingTransactionTimelogViewModel EndtimeLog = new TimeTrackingTransactionTimelogViewModel();

        protected string EmployeeName = string.Empty;

        protected DateTime CalculatedTime = new DateTime();

        private System.Threading.Timer aTimer;

        [Parameter]
        public string Transaction { get; set; }
        [Parameter]
        public int CategoryId { get; set; }
        [Parameter]
        public EventCallback EventCallbackEndMiscTasks { get; set; }

        [Inject]
        private ITimeTrackingTransactionTimelogService _timeTrackingTransactionTimelogService { get; set; }
        [Inject]
        private IToastService _toastService { get; set; }
        [Inject]
        private ILogger<EndMiscTasksComponent> _logger { get; set; }
        [Inject]
        private IEmployeeService _employeeService { get; set; }


        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await LoadDate();

                IsLoading = false;

                StateHasChanged();
            }
        }

        protected void StartTime()
        {
            aTimer = new System.Threading.Timer(Tick, null, 0, 1000);
        }

        private async void Tick(object _)
        {
            CalculatedTime = CalculatedTime.AddSeconds(1);

            await InvokeAsync(StateHasChanged);
        }

        protected async Task LoadDate()
        {
            EmployeeName = (await _employeeService.GetByCustomEmployeeIdAsync(Transaction)).FullName;
            EndtimeLog = await _timeTrackingTransactionTimelogService.GetAsync(Transaction, CategoryId,
                DateTime.Now, false, false);

            CalculatedTime = new DateTime() + DateTime.Now.Subtract(Convert.ToDateTime(EndtimeLog.TimeStart));

            StartTime();
        }

        public async Task Close()
        {
            await EventCallbackEndMiscTasks.InvokeAsync();
        }

        private async Task Save(bool isCancle, bool isComplete)
        {
            EndtimeLog.TimeEnd = DateTime.Now;
            EndtimeLog.IsCancel = isCancle;
            EndtimeLog.IsComplete = isComplete;

            await _timeTrackingTransactionTimelogService.UpdateAsync(EndtimeLog);

            await EventCallbackEndMiscTasks.InvokeAsync();
        }

        public async ValueTask DisposeAsync()
        {
            try
            {
                await Task.Run(() => aTimer?.Dispose());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "isc tasks dispose error:", ex);
            }
        }
    }
}
