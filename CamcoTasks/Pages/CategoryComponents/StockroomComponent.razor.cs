//using Blazored.Toast.Services;
//using ERP.Data.CustomModels.Other;
//using CamcoTasks.Service.IService;
//using CamcoTasks.ViewModels.PurchasingPurchaseRequestsDTO;
//using Microsoft.AspNetCore.Components;
//using Syncfusion.Blazor.Grids;

//namespace CamcoTasks.Pages.CategoryComponents
//{
//    public class StockroomComponentModel : ComponentBase, IDisposable
//    {
//        protected List<MetricsHistoryCustomData> DaysPackedMetricsHistories { get; set; } = new List<MetricsHistoryCustomData>();
//        protected List<MetricsHistoryCustomData> ModalDaysPackedMetricsHistories { get; set; } = new List<MetricsHistoryCustomData>();

//        protected List<TempOverweightBoxModel> OverweightBoxes { get; set; }
//        protected List<PurchasingPurchaseRequestsViewModel> PurchaseRequests { get; set; }

//        protected SfGrid<MetricsHistoryCustomData> DPARefGrid { get; set; }

//        protected string axisVisible { get; set; } = "DaysPackedAhead";

//        //[Inject]
//        //protected IMetricService metricService { get; set; }

//        [Inject]
//        private IToastService _toastService { get; set; }
//        public bool IsLoadingDPA { get; set; } = true;
//        public bool IsLoadingOW { get; set; } = true;
//        public bool InsideComponent { get; set; } = true;

//        private System.Timers.Timer timer1 { get; } = new System.Timers.Timer(120000);
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
//            //while (InsideComponent)
//            //{
//            //    await TimerElapse();
//            //    StateHasChanged();
//            //    await Task.Delay(120000);
//            //}
//        }

//        private async Task TimerElapse()
//        {
//            IsLoadingDPA = true;
//            IsLoadingOW = true;
//            await InvokeAsync(StateHasChanged);


//            try
//            {
//                //var MetricsHistories = await metricService.GetMetricsHistoryDaysPackedAhead();

//                DaysPackedMetricsHistories = MetricsHistories.Where(x => x.DaysPackedAhead != 0 &&
//                x.SixDaysPackedAhead != 0).ToList();
//            }
//            catch (Exception ex)
//            {
//                if (ex.InnerException != null)
//                {
//                    _toastService.ShowError(ex.InnerException.Message);
//                }
//                else
//                {
//                    _toastService.ShowError(ex.Message);
//                }
//            }
//            IsLoadingDPA = false;
//            await InvokeAsync(StateHasChanged);

//            try
//            {
//                //OverweightBoxes = await metricService.GetOverweightBoxes();
//                //foreach (var box in OverweightBoxes)
//                //{
//                //box.ExpectedWeightThreeDecimals = ConvertDecimalToThreePlaces(Convert.ToDecimal(box.ExpectedWeight));
//                //box.CustomDate = box.TransactionDate.ToString("MMMM", CultureInfo.InvariantCulture) + " " + box.TransactionDate.Day;
//                //box.CustomPercentOff = Math.Round(box.PercentOff, 0, MidpointRounding.ToZero).ToString() + "%";
//                //}
//            }
//            catch (Exception ex)
//            {
//                if (ex.InnerException != null)
//                {
//                    _toastService.ShowError(ex.InnerException.Message);
//                }
//                else
//                {
//                    _toastService.ShowError(ex.Message);
//                }
//            }
//            IsLoadingOW = false;
//            await InvokeAsync(StateHasChanged);
//        }

//        protected async Task RefreshDataModalDPA()
//        {
//            ModalDaysPackedMetricsHistories = DaysPackedMetricsHistories.OrderByDescending(x => x.ActualDate.Date).ToList();
//            await Task.Delay(1);
//            StateHasChanged();
//        }
//        private static string ConvertDecimalToThreePlaces(decimal input)
//        {
//            return Math.Abs(input - decimal.Parse(string.Format("{0:0.000}", input))) > 0 ?
//                input.ToString() :
//                string.Format("{0:0.000}", input);
//        }
//        public void Dispose()
//        {
//            InsideComponent = false;
//        }
//    }
//}
