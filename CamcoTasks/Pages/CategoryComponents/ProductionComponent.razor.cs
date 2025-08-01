//using Blazored.Toast.Services;
//using ERP.Data.CustomModels.Other;
//using CamcoTasks.Service.IService;
//using CamcoTasks.ViewModels.PlanningOverheadDTO;
//using Microsoft.AspNetCore.Components;
//using Syncfusion.Blazor.Grids;

//namespace CamcoTasks.Pages.CategoryComponents
//{
//    public class ProductionComponentModel : ComponentBase, IDisposable
//    {
//        public object[] gridToolBar = new object[] { "Add", "Edit", "Delete", "Cancel", "Update" };
//        protected bool IsLoadingShopOutPut { get; set; } = true;
//        protected bool IsLoadingOH { get; set; } = true;
//        protected bool IsLoadingMetricsHistory { get; set; } = true;
//        protected List<TempShopDailyOutput> shopDailyOutputs { get; set; }
//        protected List<TempShopDailyOutput> _shopDailyOutputs { get; set; } = new List<TempShopDailyOutput>();
//        protected SfGrid<PlanningOverheadViewModel> OverHeadGridRef { get; set; }
//        protected List<PlanningOverheadViewModel> OverheadData { get; set; }
//        protected List<CustomOverHeads> CustomOverheadData { get; set; }

//        protected List<MetricsHistoryCustomData> MetricsHistories = new List<MetricsHistoryCustomData>();

//        protected List<MetricsHistoryCustomData> WIPMetricsHistories = new List<MetricsHistoryCustomData>();
//        protected List<MetricsHistoryCustomData> ModalWIPMetricsHistories = new List<MetricsHistoryCustomData>();

//        protected List<MetricsHistoryCustomData> SHOPOutPutMetricsHistories = new List<MetricsHistoryCustomData>();
//        protected List<MetricsHistoryCustomData> ModalSHOPOutPutMetricsHistories = new List<MetricsHistoryCustomData>();

//        protected List<MetricsHistoryCustomData> InventoryMetricsHistories = new List<MetricsHistoryCustomData>();
//        protected List<MetricsHistoryCustomData> ModalInventoryMetricsHistories = new List<MetricsHistoryCustomData>();

//        protected SfGrid<MetricsHistoryCustomData> HistoryGrid { get; set; }
//        protected SfGrid<MetricsHistoryCustomData> ShopOutPutGrid { get; set; }
//        protected SfGrid<MetricsHistoryCustomData> InventoryGrid { get; set; }
//        protected SfGrid<MetricsHistoryCustomData> WIPGrid { get; set; }


//        //[Inject]
//        //protected IMetricService metricService { get; set; }

//        [Inject]
//        protected IToastService _toastService { get; set; }

//        private bool InsideComponent = true;

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
//            IsLoadingShopOutPut = true;
//            IsLoadingOH = true;
//            IsLoadingMetricsHistory = true;
//            await InvokeAsync(StateHasChanged);

//            try
//            {
//                shopDailyOutputs = await metricService.GetFinalDailyShopOutput();
//                await InvokeAsync(StateHasChanged);
//            }
//            catch (Exception ex)
//            {
//                if (ex.InnerException != null)
//                {
//                    _toastService.ShowError(ex.InnerException.Message + "Load Error");
//                }
//                else
//                {
//                    _toastService.ShowError(ex.Message + "Load Error");
//                }
//            }
//            IsLoadingShopOutPut = false;
//            await InvokeAsync(StateHasChanged);

//            try
//            {
//                CustomOverheadData = await metricService.GetAllCustomOverheadData();
//                await InvokeAsync(StateHasChanged);
//            }
//            catch (Exception ex)
//            {
//                if (ex.InnerException != null)
//                {
//                    _toastService.ShowError(ex.InnerException.Message + "OverHead Load Error");
//                }
//                else
//                {
//                    _toastService.ShowError(ex.Message + "OverHead Load Error");
//                }
//            }
//            IsLoadingOH = false;
//            await InvokeAsync(StateHasChanged);

//            try
//            {
//                MetricsHistories = await metricService.GetMetricsHistory();

//                WIPMetricsHistories = MetricsHistories.Where(x => x.WIPTotal != 0).ToList();

//                SHOPOutPutMetricsHistories = MetricsHistories.Where(x => x.ShopOutputExpenses != 0 &&
//                x.ShopOutputTotal != 0).ToList();

//                InventoryMetricsHistories = MetricsHistories.Where(x => x.InventoryTotal != 0 &&
//                x.PkgInventoryTotal != 0).ToList();

//                await InvokeAsync(StateHasChanged);
//            }
//            catch (Exception ex)
//            {
//                if (ex.InnerException != null)
//                {
//                    _toastService.ShowError(ex.InnerException.Message + "Metrics Load Error");
//                }
//                else
//                {
//                    _toastService.ShowError(ex.Message + "Metrics Load Error");
//                }
//            }
//            IsLoadingMetricsHistory = false;
//            await InvokeAsync(StateHasChanged);
//        }

//        protected async Task RefreshDataModalOverHead()
//        {
//            OverheadData = new List<PlanningOverheadViewModel>();
//            OverheadData = (await metricService.GetAllOverheadData()).ToList();
//            OverheadData = OverheadData.OrderByDescending(x => x.OverheadDate.Value.Date).ToList();
//            foreach (var item in OverheadData)
//                if (item.OverheadPerHour != null)
//                    item.OverheadPerHour = decimal.Round((decimal)item.OverheadPerHour, 2, MidpointRounding.ToZero);

//            StateHasChanged();
//        }

//        protected async Task ShopOutPutRefreshDataModal()
//        {
//            ModalSHOPOutPutMetricsHistories = SHOPOutPutMetricsHistories.OrderByDescending(x => x.ActualDate.Date).ToList();
//            await Task.Delay(1);
//            StateHasChanged();
//        }
//        protected async Task WIPRefreshDataModal()
//        {
//            ModalWIPMetricsHistories = WIPMetricsHistories.OrderByDescending(x => x.ActualDate.Date).ToList();
//            await Task.Delay(1);
//            StateHasChanged();
//        }
//        protected async Task InventoryRefreshDataModal()
//        {
//            ModalInventoryMetricsHistories = InventoryMetricsHistories.OrderByDescending(x => x.ActualDate.Date).ToList();
//            await Task.Delay(1);
//            StateHasChanged();
//        }

//        public async Task ActionBeginOverHead(ActionEventArgs<PlanningOverheadViewModel> arg)
//        {
//            try
//            {
//                if (arg.RequestType.Equals(Syncfusion.Blazor.Grids.Action.Save))
//                {
//                    if (arg.Data.OverheadId == 0)
//                    {
//                        arg.Data.IsDeleted = false;
//                        var result = await metricService.AddOverheadData(arg.Data);
//                        await OverHeadGridRef.Refresh();
//                        await OverHeadGridRef.RefreshColumnsAsync();
//                        CustomOverheadData = await metricService.GetAllCustomOverheadData();
//                        StateHasChanged();
//                        _toastService.ShowSuccess("OverHead Added!");
//                    }
//                    else
//                    {
//                        var result = await metricService.UpdateOverheadData(arg.Data);
//                        OverheadData[OverheadData.FindIndex(x => x.OverheadId == arg.Data.OverheadId)] = arg.Data;
//                        await OverHeadGridRef.Refresh();
//                        CustomOverheadData = await metricService.GetAllCustomOverheadData();
//                        await OverHeadGridRef.RefreshColumnsAsync();
//                        StateHasChanged();
//                        _toastService.ShowSuccess("OverHead Edited Successfully!");
//                    }
//                }
//                else if (arg.RequestType.Equals(Syncfusion.Blazor.Grids.Action.Delete))
//                {
//                    OverheadData.Remove(arg.Data);
//                    arg.Data.IsDeleted = true;
//                    var result = await metricService.UpdateOverheadData(arg.Data);
//                    OverHeadGridRef.Refresh();
//                    CustomOverheadData = await metricService.GetAllCustomOverheadData();
//                    await OverHeadGridRef.RefreshColumns();
//                    StateHasChanged();
//                    _toastService.ShowSuccess("OverHead Removed!");
//                }
//            }
//            catch (Exception)
//            {
//                throw;
//            }
//        }
//        public void Dispose()
//        {
//            InsideComponent = false;
//        }
//    }

//}
