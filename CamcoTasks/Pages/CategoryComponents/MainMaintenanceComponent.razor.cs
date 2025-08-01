//using Blazored.Toast.Services;
//using CamcoTasks.Infrastructure.CustomModels.IT;
//using CamcoTasks.Infrastructure.CustomModels.Other;
//using CamcoTasks.Service.IService;
//using CamcoTasks.ViewModels.ItTicket;
//using CamcoTasks.ViewModels.MaintenanceWorkOrderDataDTO;
//using Microsoft.AspNetCore.Components;

//namespace CamcoTasks.Pages.CategoryComponents
//{
//    public class MainMaintenanceComponentModel : ComponentBase, IDisposable
//    {
//        protected List<MaintenanceOpenTicketDates> MaintenanceOpenTickets { get; set; } = new List<MaintenanceOpenTicketDates>();
//        protected List<MaintenanceOpenTicketDates> MaintenanceOpenTicketsDesc { get; set; } = new List<MaintenanceOpenTicketDates>();
//        protected List<MaintenanceWorkOrderDataViewModel> MaintenanceTicketsByDate { get; set; } = new List<MaintenanceWorkOrderDataViewModel>();
//        protected List<ItTicketsViewModel> TicketViewModels { get; set; } = new List<ItTicketsViewModel>();
//        protected List<OpenTicketsModel> ViewTicketsData { get; set; } = new List<OpenTicketsModel>();
//        protected List<ItTicketsViewModel> _TicketViewModels { get; set; } = new List<ItTicketsViewModel>();

//        //[Inject]
//        //protected IMetricService metricService { get; set; }
//        [Inject]
//        protected ITicketService ticketService { get; set; }

//        public bool IsLoadingMaintenance { get; set; } = true;
//        public bool IsLoadingIT { get; set; } = true;

//        [Inject]
//        private IToastService _toastService { get; set; }
//        private bool InsideComponente = true;

//        private System.Timers.Timer timer1 { get; } = new System.Timers.Timer(120000);
//        public object metricService { get; private set; }

//        public async Task InitTimer()
//        {
//            await Task.Delay(0);
//            if (!timer1.Enabled)
//            {
//                timer1.Elapsed += async (sender, e) => await TimerElapse();
//                timer1.Start();
//            }
//        }

//        protected override async Task OnParametersSetAsync()
//        {
//            _toastService.ShowInfo("Data Loading Might Take Some Time, Please Wait");
//            await TimerElapse();
//            await InitTimer();
//        }

//        private async Task TimerElapse()
//        {
//            IsLoadingMaintenance = true;
//            IsLoadingIT = true;

//            try
//            {
//                MaintenanceOpenTickets = await metricService.GetCustomMaintenanceOpenTickets();
//                MaintenanceOpenTicketsDesc = MaintenanceOpenTickets.OrderByDescending(x => x.ActualDate).ToList();
//            }
//            catch (Exception ex)
//            {
//                if (ex.InnerException != null)
//                {
//                    _toastService.ShowError(ex.InnerException.Message + "Maintenance Load Error");
//                }
//                else
//                {
//                    _toastService.ShowError(ex.Message + "Maintenance Load Error");
//                }
//            }
//            IsLoadingMaintenance = false;
//            await InvokeAsync(StateHasChanged);
//        }

//        public async Task ViewMaintenanceTicketHandler(MaintenanceOpenTicketDates maintenanceOpenTicket)
//        {
//            MaintenanceTicketsByDate = new List<MaintenanceWorkOrderDataViewModel>();
//            if (maintenanceOpenTicket == null)
//            {
//                _toastService.ShowError("Please Select A Record, Error Viewing");
//                return;
//            }
//            else
//            {
//                MaintenanceTicketsByDate = (await metricService.GetCustomMaintenanceOpenTicketsByDate(maintenanceOpenTicket.ActualDate))
//                    .OrderByDescending(x => x.DateTimeOpen).ToList();
//                StateHasChanged();
//            }
//        }
//        public async Task ViewMaintenanceTicketPriorityOneHandler(MaintenanceOpenTicketDates maintenanceOpenTicket)
//        {
//            MaintenanceTicketsByDate = new List<MaintenanceWorkOrderDataViewModel>();
//            if (maintenanceOpenTicket == null)
//            {
//                _toastService.ShowError("Please Select A Record, Error Viewing");
//                return;
//            }
//            else
//            {
//                MaintenanceTicketsByDate = (await metricService.GetMaintenancePriorityOneByDate(maintenanceOpenTicket.ActualDate))
//                    .OrderByDescending(x => x.DateTimeOpen).ToList();
//                StateHasChanged();
//            }
//        }

//        public void Dispose()
//        {
//            InsideComponente = false;
//        }
//    }
//}
