//using Blazored.Toast.Services;
//using ERP.Data.CustomModels.Other;
//using CamcoTasks.Service.IService;
//using CamcoTasks.ViewModels.MetricsGagingDataDTO;
//using Microsoft.AspNetCore.Components;
//using Syncfusion.Blazor.Grids;

//namespace CamcoTasks.Pages
//{
//    public class QualityComponentModel : ComponentBase, IDisposable
//    {
//        //[Inject]
//        //protected IMetricService metricService { get; set; }
//        [Inject]
//        protected IToastService _toastService { get; set; }

//        protected List<MetricsDataCustom> MetricsCustomData { get; set; } = new List<MetricsDataCustom>();
//        protected List<MetricsDataCustom> GraphMetricsCustomData { get; set; } = new List<MetricsDataCustom>();
//        protected List<MetricsGagingDataViewModel> NewMetricsData { get; set; } = new List<MetricsGagingDataViewModel>();
//        protected SfGrid<MetricsGagingDataViewModel> CalibRefGrid { get; set; }
//        protected SfGrid<RmaCustom> RMARefGrid { get; set; }
//        protected SfGrid<SelectedRMAs> rmalistGrid { get; set; }
//        protected List<RmaCustom> rmaCustoms { get; set; } = new List<RmaCustom>();
//        protected List<RmaCustom> GraphRmaCustoms { get; set; } = new List<RmaCustom>();
//        protected List<RmaCustom> ModalrmaCustoms { get; set; } = new List<RmaCustom>();

//        public string[] gridToolBar = new string[] { "Add", "Edit", "Delete", "Cancel", "Update" };
//        public bool IsLoadingCalibration { get; set; } = true;
//        public bool IsLoadingRMA { get; set; } = true;
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
//        }

//        //private async Task TimerElapse()
//        //{
//        //    IsLoadingCalibration = true;
//        //    IsLoadingRMA = true;
//        //    await InvokeAsync(StateHasChanged);

//        //    try
//        //    {
//        //        MetricsCustomData = await metricService.GetAllCustomGageData();
//        //        GraphMetricsCustomData = MetricsCustomData.ToList();
//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        if (ex.InnerException != null)
//        //        {
//        //            _toastService.ShowError(ex.InnerException.Message);
//        //        }
//        //        else
//        //        {
//        //            _toastService.ShowError(ex.Message);
//        //        }
//        //    }

//        //    IsLoadingCalibration = false;
//        //    await InvokeAsync(StateHasChanged);

//        //    try
//        //    {
//        //        rmaCustoms = await metricService.GetTotalRMA2();
//        //        GraphRmaCustoms = rmaCustoms.TakeLast(45).ToList();
//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        if (ex.InnerException != null)
//        //        {
//        //            _toastService.ShowError(ex.InnerException.Message);
//        //        }
//        //        else
//        //        {
//        //            _toastService.ShowError(ex.Message);
//        //        }
//        //    }
//        //    IsLoadingRMA = false;
//        //    await InvokeAsync(StateHasChanged);
//        //}

//        protected async Task RefreshDataModal()
//        {
//            NewMetricsData = new List<MetricsGagingDataViewModel>();
//            //NewMetricsData = (await metricService.GetAllGageData()).ToList();
//            NewMetricsData = NewMetricsData.OrderByDescending(x => x.DateReported).ToList();
//            StateHasChanged();
//        }
//        public async Task ActionBeginCalibration(ActionEventArgs<MetricsGagingDataViewModel> arg)
//        {
//            try
//            {
//                if (arg.RequestType.Equals(Syncfusion.Blazor.Grids.Action.Save))
//                {
//                    if (arg.Data.Id == 0)
//                    {
//                        arg.Data.IsDeleted = false;
//                        //var result = await metricService.AddMetricData(arg.Data);
//                        await CalibRefGrid.Refresh();
//                        await CalibRefGrid.RefreshColumnsAsync();
//                        //MetricsCustomData = await metricService.GetAllCustomGageData();
//                        StateHasChanged();
//                        _toastService.ShowSuccess("New Calibration Added!");
//                    }
//                    else
//                    {
//                        //var result = await metricService.UpdateMetricData(arg.Data);
//                        NewMetricsData[NewMetricsData.FindIndex(x => x.Id == arg.Data.Id)] = arg.Data;
//                        await CalibRefGrid.Refresh();
//                        await CalibRefGrid.RefreshColumnsAsync();
//                        //MetricsCustomData = await metricService.GetAllCustomGageData();
//                        StateHasChanged();
//                        _toastService.ShowSuccess("Calibration Edited Successfully!");
//                    }
//                }
//                else if (arg.RequestType.Equals(Syncfusion.Blazor.Grids.Action.Delete))
//                {
//                    NewMetricsData.Remove(arg.Data);
//                    arg.Data.IsDeleted = true;
//                    //var result = await metricService.UpdateMetricData(arg.Data);
//                    await CalibRefGrid.Refresh();
//                    await CalibRefGrid.RefreshColumnsAsync();
//                    //MetricsCustomData = await metricService.GetAllCustomGageData();
//                    StateHasChanged();
//                    _toastService.ShowSuccess("Calibration Removed!");
//                }
//            }
//            catch (Exception ex)
//            {

//            }
//        }

//        protected async Task RefreshRMAModal()
//        {
//            ModalrmaCustoms = rmaCustoms.OrderByDescending(x => x.ActualDate.Date).ToList();
//            await Task.Delay(1);
//            StateHasChanged();
//        }
//        public void Dispose()
//        {
//            InsideComponent = false;
//        }
//    }
//}
