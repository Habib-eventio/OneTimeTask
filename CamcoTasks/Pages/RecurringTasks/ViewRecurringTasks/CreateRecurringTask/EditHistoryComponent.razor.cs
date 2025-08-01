using Blazored.Toast.Services;
using CamcoTasks.Service.IService;
using CamcoTasks.Service.Service;
using CamcoTasks.ViewModels.EmployeeDTO;
using CamcoTasks.ViewModels.LoggingChangeLogDTO;
using CamcoTasks.ViewModels.PageLoadTimeDTO;
using CamcoTasks.ViewModels.TasksRecTasksDTO;
using CamcoTasks.Infrastructure.Entities.TaskInfo;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Syncfusion.Blazor.Grids;
using Syncfusion.Blazor.Inputs;
using Syncfusion.Blazor.Navigations;

namespace CamcoTasks.Pages.RecurringTasks.ViewRecurringTasks.CreateRecurringTask
{
    public partial class EditHistoryComponent
    {
        protected bool IsLoadTask = true;

        protected string EditRecordType;

        protected int EditRecordId;

        protected List<LoggingChangeLogViewModel> EditHistoryLogs = new List<LoggingChangeLogViewModel>();

        protected TasksRecTasksViewModel RecTask = new TasksRecTasksViewModel();

        protected EmployeeViewModel EmployeeByApprovedId = new EmployeeViewModel();
        protected PageLoadTimeViewModel PageLoadTime { get; set; } = new PageLoadTimeViewModel()
        {
            PageName = "RecurringTask",
            SectionName = "RecurringTaskEditHistory",
            DateCreated = DateTime.Now
        };

        protected SfGrid<LoggingChangeLogViewModel> ProGrid;

        [Inject]
        protected IPageLoadTimeService PageLoadTimeService { get; set; }
        [Inject]
        private IJSRuntime JsRuntime { get; set; }
        [Inject]
        protected ILoggingChangeLogService LoggingChangeLogService { get; set; }
        [Inject]
        private IToastService ToastService { get; set; }
        [Inject]
        private IFileManagerService FileManagerService { get; set; }

        [Inject]
        protected ITasksService TaskService { get; set; }

        [Inject]
        protected IEmployeeService EmployeeService { get; set; }

        [Parameter]
        public string RecordType { get; set; }
        [Parameter]
        public int RecordId { get; set; }
        [Parameter]
        public EventCallback CloseEditHistoryComponent { get; set; }


        protected override async Task OnInitializedAsync()
        {
            await Task.Run(() => PageLoadTime.StartTime = DateTime.Now);
        }

        protected override async void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                await LoadContext();
                await LoadData();

                IsLoadTask = false;

                StateHasChanged();

                await PageLoadTimeCalculation();
            }
        }

        protected async Task PageLoadTimeCalculation()
        {
            PageLoadTime.EndTime = DateTime.Now;
            await PageLoadTimeService.InsertAsync(PageLoadTime);
        }

        protected async Task LoadContext()
        {
            await JsRuntime.InvokeAsync<object>("ShowModal", "#EditHistoryModal");
        }

        protected async Task LoadData()
        {
            EditRecordType = RecordType;
            EditRecordId = RecordId;
            EditHistoryLogs = (await LoggingChangeLogService.GetListAsync(EditRecordType, EditRecordId)).
            OrderByDescending(a => a.UpdateDate).ToList();
            if (RecordId != 0)
            {
                RecTask = await TaskService.GetRecurringTaskById(RecordId);
            }
            if(RecTask?.ApprovedByEmployeeId != 0)
            {
                EmployeeByApprovedId =  await EmployeeService.GetByIdAsync(RecTask?.ApprovedByEmployeeId ?? 0);
            }
        }


        public async Task SearchValueChange(ChangedEventArgs args)
        {
            await ProGrid.SearchAsync(args.Value);
        }

        protected async Task StartPrinting(ClickEventArgs args)
        {
            PageLoadTime.StartTime = DateTime.Now;
            PageLoadTime.SectionName = "ExportReport";

            if (args.Item.Text == "PRINT REPORT")
            {
                ToastService.ShowSuccess("Generating Report Started, Please Wait.");
                var pdf = FileManagerService.CreateEditHistoryPdfInMemory(EditHistoryLogs);
                await JsRuntime.InvokeVoidAsync("jsSaveAsFile", "RecurringTaskEditHistory.pdf", Convert.ToBase64String(pdf));
            }
            else if (args.Item.Text == "EXCEL EXPORT")
            {
                ToastService.ShowSuccess("Generating Report Started, Please Wait.");
                ExcelExportProperties exportProperties = new ExcelExportProperties();
                exportProperties.IncludeTemplateColumn = true;
                exportProperties.FileName = "RecurringTaskEditHistory.xlsx";
                await ProGrid.ExportToExcelAsync(exportProperties);
            }

            await PageLoadTimeCalculation();
        }

        protected async Task CloseComponent()
        {
            await JsRuntime.InvokeAsync<object>("HideModal", "#EditHistoryModal");
            await CloseEditHistoryComponent.InvokeAsync();
        }
    }
}
