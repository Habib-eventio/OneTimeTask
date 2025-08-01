using Blazored.Toast.Services;
using CamcoTasks.Data.ModelsViewModel;
using CamcoTasks.Data.Services;
using CamcoTasks.Infrastructure.Entities;
using CamcoTasks.Service.IService;
using CamcoTasks.ViewModels.PageLoadTimeDTO;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Syncfusion.Blazor.Grids;
using Syncfusion.Blazor.Inputs;
using Syncfusion.Blazor.Navigations;

namespace CamcoTasks.Pages.CamcoProject
{
    public partial class CamcoProjectCosting
    {
        private bool _isRender = true;

        protected PageLoadTimeViewModel pageLoadTime = new PageLoadTimeViewModel()
        {
            PageName = "ViewCamcoProjectCost",
            SectionName = "TaskList",
            DateCreated = DateTime.Now
        };

        private SfGrid<ProjectCosting> _proGrid;

        private List<ProjectCosting> _productSoCosting = new List<ProjectCosting>();

        [Inject]
        protected IPageLoadTimeService PageLoadTimeService { get; set; }
        [Inject]
        protected ICamcoProjectService CamcoProjectService { get; set; }
        [Inject]
        private IToastService _toastService { get; set; }
        [Inject]
        private IFileManagerService _fileManagerService { get; set; }
        [Inject]
        private IJSRuntime _jSRuntime { get; set; }


        protected override async Task OnInitializedAsync()
        {
            await Task.Run(() => pageLoadTime.StartTime = DateTime.Now);
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await LoadData();

                _isRender = false;

                StateHasChanged();

                await PageLoadTimeCalculation();
            }
        }

        protected async Task PageLoadTimeCalculation()
        {
            pageLoadTime.EndTime = DateTime.Now;
            await PageLoadTimeService.InsertAsync(pageLoadTime);
        }

        protected async Task LoadData()
        {
            _productSoCosting = await CamcoProjectService.GetProjectCostsListAsync();
        }

        public async Task SearchValueChange(ChangedEventArgs args)
        {
            await _proGrid.SearchAsync(args.Value);
        }

        protected async Task StartPrinting(ClickEventArgs args)
        {
            pageLoadTime.StartTime = DateTime.Now;
            pageLoadTime.SectionName = "ExportReport";

            if (args.Item.Text == "PRINT REPORT")
            {
                _toastService.ShowSuccess("Generating Report Started, Please Wait.");
                var pdf = _fileManagerService.CreateCamcoProjectPdfInMemory(_productSoCosting);
                await _jSRuntime.InvokeVoidAsync("jsSaveAsFile", "CamcoProjectCosting.pdf", Convert.ToBase64String(pdf));
            }
            else if (args.Item.Text == "EXCEL EXPORT")
            {
                _toastService.ShowSuccess("Generating Report Started, Please Wait.");
                ExcelExportProperties exportProperties = new ExcelExportProperties();
                exportProperties.IncludeTemplateColumn = true;
                exportProperties.FileName = "CamcoProjectCosting.xlsx";
                await _proGrid.ExportToExcelAsync(exportProperties);
            }

            await PageLoadTimeCalculation();
        }
    }
}
