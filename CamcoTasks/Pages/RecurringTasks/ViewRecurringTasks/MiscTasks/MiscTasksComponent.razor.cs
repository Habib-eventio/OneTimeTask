using Blazored.Toast.Services;
using CamcoTasks.Service.IService;
using CamcoTasks.ViewModels.EmployeeDTO;
using CamcoTasks.ViewModels.TimeTrackingMiscellaneousDTO;
using CamcoTasks.ViewModels.TimeTrackingOptionsDTO;
using Microsoft.AspNetCore.Components;

namespace CamcoTasks.Pages.RecurringTasks.ViewRecurringTasks.MiscTasks
{
    public partial class MiscTasksComponent : IAsyncDisposable
    {
        protected DateTime CalculatedTime = new DateTime();

        protected TimeTrackingMiscellaneousViewModel MiscObj = new TimeTrackingMiscellaneousViewModel();

        protected int CategoryId;

        protected bool IsActiveEndMiscTask = false;
        protected bool IsActiveEmployeeeListComponent = true;
        protected bool IsActiveMiscTaskModal = false;

        protected string EmployeeName;
        protected string UserTimerChoice;
        protected string TransactionBy;

        protected EmployeeViewModel Employee;

        protected TimeTrackingOptionsViewModel Category = new TimeTrackingOptionsViewModel();

        private System.Threading.Timer aTimer;

        [Inject]
        private ILogger<MiscTasksComponent> _logger { get; set; }
        [Inject]
        private IToastService _toastService { get; set; }
        [Inject]
        private ITimeTrackingTransactionTimelogService _timeTrackingTransactionTimelogService { get; set; }
        [Inject]
        private ITimeTrackingMiscellaneousesService _timeTrackingMiscellaneousesService { get; set; }
        [Inject]
        private IEmployeeService _employeeService { get; set; }
        [Inject]
        private ITimeTrackingOptionsService _trackingOptionsService { get; set; }

        [Parameter]
        public EventCallback EventCallbackMiscTasks { get; set; }


        private async void Tick(object _)
        {
            CalculatedTime = CalculatedTime.AddSeconds(1);

            await InvokeAsync(StateHasChanged);
        }

        protected async void SetEmployeeName(string employeeName)
        {
            if (string.IsNullOrEmpty(employeeName))
            {
                await EventCallbackMiscTasks.InvokeAsync();
                return;
            }

            Category = await _trackingOptionsService.GetAsync("CAMCO TASKS APP", "CAMCO MISC TASKS");

            EmployeeName = employeeName;
            Employee = await _employeeService.GetEmployee(EmployeeName);

            if (Category == null || Employee == null)
            {
                await EventCallbackMiscTasks.InvokeAsync();
                return;
            }

            IsActiveEmployeeeListComponent = false;
            TransactionBy = Employee.CustomEmployeeId;
            CategoryId = Category.Id;

            var miscTask = await _timeTrackingTransactionTimelogService.GetAsync(Employee.CustomEmployeeId, Category.Id,
                DateTime.Now, false, false);

            if (miscTask != null)
            {
                IsActiveEndMiscTask = true;
            }
            else
            {
                IsActiveMiscTaskModal = true;
            }

            aTimer = new System.Threading.Timer(Tick, null, 0, 1000);
        }

        private async Task Save()
        {

            if (string.IsNullOrEmpty(MiscObj.WorkDetails))
            {
                _toastService.ShowError("PLEASE ENTER WORK DETAILS!, MANDATORY FIELD");
                return;
            }

            int qid = await _timeTrackingMiscellaneousesService.InsertAsync(MiscObj);

            await _timeTrackingTransactionTimelogService.InsertAsync(Category.Id, Employee.CustomEmployeeId,
                DateTime.Now, qid, MiscObj.WorkDetails.ToUpper());

            TransactionBy = Employee.CustomEmployeeId;
            IsActiveMiscTaskModal = false;
            IsActiveEndMiscTask = true;
        }

        private async Task Cancle(bool isCancenl)
        {
            await EventCallbackMiscTasks.InvokeAsync();
        }

        protected async Task SuccessMessageFormEndMiscTaskComponent()
        {
            await EventCallbackMiscTasks.InvokeAsync();
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
