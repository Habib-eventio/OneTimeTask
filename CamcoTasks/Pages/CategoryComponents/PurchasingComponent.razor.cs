//using Blazored.Toast.Services;
//using ERP.Data.CustomModels.Other;
//using CamcoTasks.Service.IService;
//using CamcoTasks.ViewModels.PlanningOverheadDTO;
//using Microsoft.AspNetCore.Components;
//using Syncfusion.Blazor.Grids;

//namespace CamcoTasks.Pages.CategoryComponents
//{
//    public class PurchasingComponentModel : ComponentBase, IDisposable
//    {
//        protected SfGrid<PlanningOverheadViewModel> OverHeadGridRef { get; set; }
//        protected List<PlanningOverheadViewModel> OverheadData { get; set; }
//        protected List<POsTotals> customPOsTotals { get; set; } = new List<POsTotals>();
//        protected List<POsTotals> ModalcustomPOsTotals { get; set; }
//        protected List<SelectedPo> SelectedModalcustomPOsTotals { get; set; }
//        protected SfGrid<POsTotals> POsGrid { get; set; }
//        protected SfGrid<SelectedPo> oisListGrid { get; set; }
//        //[Inject]
//        //protected IMetricService metricService { get; set; }

//        public bool IsLoadingPOs { get; set; } = true;
//        public bool InsideComponent { get; set; } = true;

//        [Inject]
//        private IToastService _toastService { get; set; }

//        private System.Timers.Timer timer1 { get; } = new System.Timers.Timer(117000);
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
//            try
//            {
//                customPOsTotals = await metricService.GetPORequestsTotals();
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

//            IsLoadingPOs = false;
//            await InvokeAsync(StateHasChanged);
//        }

//        protected async Task RefreshPOModal()
//        {
//            ModalcustomPOsTotals = customPOsTotals.OrderByDescending(x => x.ActualDate.Date).ToList();
//            await Task.Delay(1);
//            StateHasChanged();
//        }
//        protected async Task RefreshInnerPOModal(POsTotals pos)
//        {
//            SelectedModalcustomPOsTotals = pos.PoList;
//            await Task.Delay(35);
//            StateHasChanged();
//        }
//        public async Task ToolbarClickHandler(Syncfusion.Blazor.Navigations.ClickEventArgs args)
//        {
//            if (args.Item.Text == "Excel Export")
//            {
//                ExcelExportProperties ExcelProperties = new ExcelExportProperties();
//                ExcelProperties.FileName = "Metrics PRs - " + DateTime.Today.ToString("MM/dd/yyyy") + ".xlsx";

//                await oisListGrid.ExportToExcelAsync(ExcelProperties);
//            }
//            if (args.Item.Text == "PDF Export")
//            {
//                PdfExportProperties ExportProperties = new PdfExportProperties();
//                ExportProperties.FileName = "Metrics PRs - " + DateTime.Today.ToString("MM/dd/yyyy") + ".pdf";

//                List<PdfHeaderFooterContent> HeaderContent = new List<PdfHeaderFooterContent>
//                {
//                    new PdfHeaderFooterContent() {
//                        Type = ContentType.Text, Value = "Metrics PRs -" + DateTime.Today.ToString("MM/dd/yyyy"),
//                        Position = new PdfPosition() { X = 0, Y = 50 },
//                        Style = new PdfContentStyle() { TextBrushColor = "#1C4084", FontSize = 15, DashStyle= PdfDashStyle.Solid } }
//                };

//                PdfHeader Header = new PdfHeader()
//                {
//                    FromTop = 0,
//                    Height = 130,
//                    Contents = HeaderContent
//                };
//                ExportProperties.Header = Header;

//                await oisListGrid.ExportToPdfAsync(ExportProperties);
//            }
//            if (args.Item.Text == "CSV Export")
//            {
//                ExcelExportProperties ExcelProperties = new ExcelExportProperties();
//                ExcelProperties.FileName = "Metrics PO - " + DateTime.Today.ToString("MM/dd/yyyy") + ".csv";

//                await oisListGrid.ExportToCsvAsync(ExcelProperties);
//            }
//        }
//        public void Dispose()
//        {
//            InsideComponent = false;
//        }
//    }
//}
