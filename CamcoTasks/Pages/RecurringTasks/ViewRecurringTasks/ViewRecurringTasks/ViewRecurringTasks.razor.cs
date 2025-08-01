using Append.Blazor.Printing;
using Blazored.Toast.Services;
using CamcoTasks.Data.Services;
using CamcoTasks.Library;
using CamcoTasks.Service.IService;
using CamcoTasks.ViewModels.ModelsViewModel;
using CamcoTasks.ViewModels.PageLoadTimeDTO;
using CamcoTasks.ViewModels.TasksRecTasksDTO;
using CamcoTasks.ViewModels.TasksTaskUpdatesDTO;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using Syncfusion.Blazor.Data;
using Syncfusion.Blazor.Grids;
using Syncfusion.Blazor.Navigations;
using Query = Syncfusion.Blazor.Data.Query;
using Syncfusion.XlsIO;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Data;
using CamcoTasks.ViewModels.EmployeeDTO;
using CamcoTasks.ViewModels.JobDescriptions;
using CamcoTasks.ViewModels.TasksFrequencyListDTO;
using Microsoft.AspNetCore.Components.Web;
using NuGet.Packaging;

namespace CamcoTasks.Pages.RecurringTasks.ViewRecurringTasks
{
    public partial class ViewRecurringTasksModel : ComponentBase
    {
        protected bool IsActiveMiscTasksComponent = false;
        protected bool IsActiveViewSingleRecurringTask = false;
        protected bool IsFilterGridDueToday = false;
        protected bool IsFilteGridPastDueDate = false;
        protected bool IsFilteGridFirstFiveCloumn = false;

        public int RecurringTaskIdForViewSingleRecurringTask;

        protected List<TasksRecTasksViewModel> DeactivatedTasks { get; set; } = new List<TasksRecTasksViewModel>();
        protected List<TasksRecTasksViewModel> Tasks { get; set; } = new List<TasksRecTasksViewModel>();
        protected List<TasksRecTasksViewModel> AllRecurringTasks { get; set; } = new List<TasksRecTasksViewModel>();
        protected List<TasksRecTasksViewModel> SubTasks { get; set; } = new List<TasksRecTasksViewModel>();
        protected List<EmployeeViewModel> Employees { get; set; }

        protected IEnumerable<TasksRecMenuFilterList> FilterMenuList { get; set; }

        protected string TypeDropdownVal { get; set; } = "All";
        protected long SelectedEmpId { get; set; }

        protected string TaskFile { get; set; } = null;
        protected string AllEmployees { get; set; } = "ALL EMPLOYEES";
        protected string TaskModalName { get; set; }
        protected string TaskMethodName { get; set; }
        protected string SearchPattern { get; set; } = null;
        protected string SecondSearchPattern { get; set; } = null;
        protected string filterDueToday = "btn-primary";
        protected string filterPastDueDqate = "btn-primary";
        protected string filterFirstColumn = "btn-primary";

        public RecurringTaskPercentageComponent PercentageComponentRef { get; set; }

        public int pageNo { get; set; } = 0;
        protected int perPageItem { get; set; }
        public int totalPages { get; set; }
        protected int RecTasksCount { get; set; }

        public bool DisableBackIcon = false;
        public bool DisableForwardIcon = false;
        public bool DisableFirstIcon = false;
        public bool DisableLastIcon = false;
        protected bool IsAuthenticate { get; set; } = false;
        protected bool IsActivePercentageFilter { get; set; } = false;
        protected bool _isRecuringTaskLoad = true;
        protected bool IsActiveCreateTask { get; set; } = false;
        protected bool IsActiveSingleFileComponent { get; set; } = false;
        protected bool IsActiveMultipleFileComponent { get; set; } = false;
        protected bool IsActiveDeactivateComponent { get; set; } = false;
        protected bool IsActiveDeleteComponent { get; set; } = false;
        protected bool IsActiveSimpleEmailComponent { get; set; } = false;
        protected bool IsActiveRecTaskUpdateComponent { get; set; } = false;
        protected bool IsActiveActiveRecComponent { get; set; } = false;
        protected bool IsActivePagination { get; set; } = true;
        protected bool IsGridSpinner { get; set; } = false;
        protected bool ComboBoxVisibility { get; set; } = true;
        protected bool isOpenAllRecurringTasks { get; set; } = false;
        protected string ChangeByEmployeeName { get; set; } = null;
        protected int TotalPendingTasks { get; set; }

        protected SfGrid<TasksRecTasksViewModel> RecurringTasksGrid { get; set; }

        protected TasksRecTasksViewModel SelectedTaskViewModel { get; set; } = new TasksRecTasksViewModel() { ParentTaskId = null };
        protected TasksRecTasksViewModel SelectTaskForSimpleEmail { get; set; }
        protected TasksRecTasksViewModel RecurringTask { get; set; }
        protected TasksRecTasksViewModel TaskViewModel { get; set; }
        protected TasksRecTasksViewModel SelectedTaskForUpdate { get; set; }
        protected PageLoadTimeViewModel PageLoadTime { get; set; } = new PageLoadTimeViewModel()
        {
            PageName = "RecurringTask",
            SectionName = "TaskList",
            DateCreated = DateTime.Now
        };

        private List<EmployeeViewModel> _employees;
        private List<JobDescriptionsViewModal> _jobs;
        private List<TasksFrequencyListViewModel> _frequency;

        public Query GridQuery { get; set; }

        [Inject]
        protected ITasksService taskService { get; set; }

        [Inject]
        protected IEmployeeService employeeService { get; set; }

        [Inject]
        protected IPrintingService PrintingService { get; set; }

        [Inject]
        private IJSRuntime jSRuntime { get; set; }

        [Inject]
        private NavigationManager navigationManager { get; set; }

        [Inject]
        private FileManagerService fileManagerService { get; set; }

        [Inject]
        private IToastService _toastService { get; set; }

        [Inject]
        protected ILogger<ViewRecurringTasksModel> logger { get; set; }
        [Inject]
        protected IPageLoadTimeService pageLoadTimeService { get; set; }
        [Inject]
        private AuthenticationStateProvider _AuthenticationStateProvider { get; set; }
        [Inject]
        protected IJobDescriptionsService JobDescriptionsService { get; set; }
        [Inject]
        private ILogingService _loging { get; set; }

        [Parameter]
        public string FirstName { get; set; }

        [Parameter]
        public string Lastname { get; set; }

        [Parameter]
        public string ParamTaskId { get; set; }
        [Parameter]
        public long? ApproverId { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await Task.Run(() => PageLoadTime.StartTime = DateTime.Now);
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await LoadContext();

                await jSRuntime.InvokeAsync<object>("modalDraggable");

                StateHasChanged();
                
                await PageLoadTimeCalculation();
                
                _employees = (await employeeService.GetListWithoutUserAsync(true)).Where(x => x.IsActive).ToList();
                _jobs = (await JobDescriptionsService.GetListAsync(false)).ToList();
                _frequency = (await taskService.GetTaskFreqs()).ToList();
            }
        }

        protected async Task PageLoadTimeCalculation()
        {
            PageLoadTime.EndTime = DateTime.Now;
            await pageLoadTimeService.InsertAsync(PageLoadTime);
        }

        protected async Task LoadContext()
        {
            await LoadData();

            await Pagination();

            await Task.Run(() => _isRecuringTaskLoad = false);
        }

        protected async Task LoadData()
        {
            perPageItem = AppInformation.PerPageItem;
            Tasks = (await taskService.GetRecurringTasks(false, false, null, pageNo * perPageItem, perPageItem)).ToList();
            totalPages = await taskService.CountRecurringTasks(false, false, null);

            if (!string.IsNullOrEmpty(FirstName) || !string.IsNullOrEmpty(Lastname))
            {
                IsActivePagination = false;
                string Name = Lastname + ", " + FirstName;
                Tasks = (await taskService.GetRecurringTasks(a => a.IsDeleted == false && a.IsDeactivated == false && a.IsApproved == true && a.ParentTaskId == null && a.PersonResponsible == Name)).ToList();
            }
            AllRecurringTasks = (await taskService.GetRecurringTasks(a => a.IsDeleted == false && a.IsDeactivated == false)).OrderBy(x=>x.Id).ToList();
            if (!string.IsNullOrEmpty(ParamTaskId))
            {
                IsActivePagination = false;
                int PId = Convert.ToInt32(ParamTaskId);
                Tasks = AllRecurringTasks.Where(a => a.IsApproved == true && a.Id == PId).ToList();
            }
            var pendingTasksList = AllRecurringTasks.Where(a => a.IsApproved == false && a.ParentTaskId == null).ToList();
            TotalPendingTasks = pendingTasksList.Count;
            if (ApproverId != null)
            {
                IsActivePagination = false;
                Tasks = pendingTasksList;
            }
            Employees = (await employeeService.GetListAsync(true, false)).Where(x => x.IsActive).OrderBy(a => a.FullName).DistinctBy(a => a.FullName).ToList();
        }

        protected async Task Pagination()
        {
            if (totalPages > 0)
            {
                await Task.Run(() => totalPages = totalPages / perPageItem);
            }

            if (pageNo == totalPages)
            {
                DisableForwardIcon = true;
                DisableLastIcon = true;
            }
            else
            {
                DisableForwardIcon = false;
                DisableLastIcon = false;
            }

            if (pageNo == 0)
            {
                DisableBackIcon = true;
                DisableFirstIcon = true;
            }
            else
            {
                DisableBackIcon = false;
                DisableFirstIcon = false;
            }
        }

        public string ValidateFirst()
        {
            if (DisableFirstIcon)
            {
                return "disableFirst";
            }
            return "";
        }

        public string ValidateLast()
        {
            if (DisableLastIcon)
            {
                return "disableLast";
            }
            return "";
        }

        public string ValidateForward()
        {
            if (DisableForwardIcon)
            {
                return "disableFront";
            }
            return "";
        }

        public string ValidateBack()
        {
            if (DisableBackIcon)
            {
                return "disableBack";
            }
            return "";
        }

        public async Task ShowNextPage()
        {
            pageNo = pageNo + 1;
            Tasks = (await taskService.GetRecurringTasks(false, false, null, pageNo * perPageItem, perPageItem)).ToList();
            totalPages = await taskService.CountRecurringTasks(false, false, null);
            await Pagination();
        }

        public async Task ShowPreviousPage()
        {
            pageNo = pageNo - 1;
            Tasks = (await taskService.GetRecurringTasks(false, false, null, pageNo * perPageItem, perPageItem)).ToList();
            totalPages = await taskService.CountRecurringTasks(false, false, null);
            await Pagination();
        }

        public async Task ShowFirstPage()
        {
            pageNo = 0;
            Tasks = (await taskService.GetRecurringTasks(false, false, null, pageNo * perPageItem, perPageItem)).ToList();
            totalPages = await taskService.CountRecurringTasks(false, false, null);
            await Pagination();
        }

        public async Task ShowLastPage()
        {
            pageNo = totalPages;
            Tasks = (await taskService.GetRecurringTasks(false, false, null, pageNo * perPageItem, perPageItem)).ToList();
            totalPages = await taskService.CountRecurringTasks(false, false, null);
            await Pagination();
        }

        public async Task LaunchEnteredPage(Microsoft.AspNetCore.Components.ChangeEventArgs page)
        {
            if (page.Value == null || page.Value.ToString() == "")
            {
                return;
            }
            else
            {
                int enteredPage = Convert.ToInt32(page.Value);
                if (enteredPage <= totalPages)
                {
                    pageNo = enteredPage;
                    Tasks = (await taskService.GetRecurringTasks(false, false, null, pageNo * perPageItem, perPageItem)).ToList();
                    totalPages = await taskService.CountRecurringTasks(false, false, null);
                    await Pagination();
                }

            }
        }

        protected void ActiveViewSingleRecurringTaskComponent(int id)
        {
            IsActiveViewSingleRecurringTask = true;
            RecurringTaskIdForViewSingleRecurringTask = id;
        }

        public void DeactiveViewSingleRecurringTaskComponent()
        {
            IsActiveViewSingleRecurringTask = false;
            StateHasChanged();
        }

        protected void StartRecTask(TasksRecTasksViewModel RecTask)
        {
            IsActiveCreateTask = true;
            RecurringTask = RecTask;
        }

        protected void StartDeleteTask(TasksRecTasksViewModel task)
        {
            IsActiveDeleteComponent = true;
            SelectedTaskViewModel = task;
        }

        public void DeactiveDeleteRecTaskComponent()
        {
            IsActiveDeleteComponent = false;
        }

        protected void StartDeactivateTask(TasksRecTasksViewModel vieWModel)
        {
            IsActiveDeactivateComponent = true;
            SelectedTaskViewModel = vieWModel;
        }

        public async Task DeactivateDeactiveRecTaskComponent()
        {
            await Task.Run(() => IsActiveDeactivateComponent = false);
            StateHasChanged();
        }

        protected void GetDeactivatedTasks()
        {
            IsActiveActiveRecComponent = true;
        }

        protected void GetMiscTasks()
        {
            IsActiveMiscTasksComponent = true;
        }

        protected void DeactiveMiscTasks()
        {
            IsActiveMiscTasksComponent = false;
        }

        protected async Task SendNudgeEmail(TasksRecTasksViewModel task)
        {
            PageLoadTime.StartTime = DateTime.Now;
            PageLoadTime.SectionName = "NudgeLoad";
            string emailSendTo = string.Empty;

            string Subject = "FRIENDLY REMINDER TO GET THIS TASK DONE";
            bool isSend = await taskService.SendEmail(task, false, navigationManager.BaseUri, Subject);

            if (isSend)
            {
                _toastService.ShowInfo("An Email Has Been Added To Queue");
                task.NudgeCount++;
                await taskService.UpdateRecurringTask(task);
                var SystemUpdate = new TasksTaskUpdatesViewModel
                {
                    DueDate = task.UpcomingDate,
                    RecurringID = task.Id,
                    Update = $"Rich Arnold ''NUDGED'' you to get the recurring Task # " + task.Id.ToString() + " done.",
                    UpdateDate = DateTime.Now,
                    IsDeleted = false,
                    UpdateId = 0,
                };
                SystemUpdate.UpdateId = await taskService.AddTaskUpdate(SystemUpdate);
            }
            else
            {
                _toastService.ShowWarning("Email Sending Error, Employee Has No Email");
            }

            await PageLoadTimeCalculation();
        }

        public async Task SearchValueChange(Syncfusion.Blazor.Inputs.ChangedEventArgs args)
        {
            await RecurringTasksGrid.SearchAsync(args.Value);
        }

        protected async Task StartPrinting(ClickEventArgs args)
        {
            PageLoadTime.StartTime = DateTime.Now;
            PageLoadTime.SectionName = "ExportReport";

            if (args.Item.Text == "PRINT REPORT")
            {
                _toastService.ShowSuccess("Generating Report Started, Please Wait.");
                var filteredData = await RecurringTasksGrid.GetFilteredRecordsAsync();
                List<TasksRecTasksViewModel> filteredList = JsonConvert
                    .DeserializeObject<List<TasksRecTasksViewModel>>(JsonConvert.SerializeObject(filteredData));
                var pdf = fileManagerService.CreatePdfInMemory(filteredList, SearchPattern);
                await jSRuntime.InvokeVoidAsync("jsSaveAsFile", "RecurringTasks.pdf", Convert.ToBase64String(pdf));
            }
            else if (args.Item.Text == "EXCEL EXPORT")
            {
                await ExportExcel();
            }
            else if (args.Item.Text == "CLEAR FILTERS")
            {
                SearchPattern = string.Empty;
                await RecurringTasksGrid.SearchAsync("");
                filterDueToday = "btn-primary";
                filterPastDueDqate = "btn-primary";
                filterFirstColumn = "btn-primary";
                await RecurringTasksGrid.ClearFilteringAsync();
                await LoadData();
                await Pagination();
                await PercentageComponentRef.ReloadComponentAsync();
                IsActivePagination = true;
            }

            await PageLoadTimeCalculation();
        }
        protected async Task ExportExcel()
        {
            try
            {
                using ExcelEngine excelEngine = new ExcelEngine();

                IApplication application = excelEngine.Excel;

                application.DefaultVersion = ExcelVersion.Xlsx;
                // Create a new workbook
                IWorkbook workbook = application.Workbooks.Create(1);
                IWorksheet worksheet = workbook.Worksheets[0];

                DataTable excelData = new();

                excelData.Columns.Add("ID");
                excelData.Columns.Add("TASK SUBJECT");
                excelData.Columns.Add("DESCRIPTION");
                excelData.Columns.Add("DUE DATE");
                excelData.Columns.Add("INITIATOR");
                excelData.Columns.Add("PERSON RESPONSIBLE");
                excelData.Columns.Add("LAST DATE COMPLETED");
                excelData.Columns.Add("FREQUENCY");
                excelData.Columns.Add("LATEST VALUE");

                foreach(var data in AllRecurringTasks)
                {
                    excelData.Rows.Add(
                        data.Id,
                        data.TaskDescriptionSubject,
                        data.Description,
                        data.UpcomingDate,
                        data.Initiator,
                        data.PersonResponsible,
                        data.DateCompleted,
                        data.Frequency,
                        data.LatestGraphValue
                        );
                }

                // Add title row: RECURRING TASKS REPORT
                worksheet.Range["A1:I1"].Merge();
                worksheet.Range["A1"].Text = "RECURRING TASKS REPORT";
                worksheet.Range["A1:I1"].CellStyle.Font.Bold = true;
                worksheet.Range["A1:I1"].CellStyle.Font.Size = 16;
                worksheet.Range["A1:I1"].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignCenter;

                // Add title row: ACTIVE FILTERS: 
                worksheet.Range["A2:I2"].Merge();
                worksheet.Range["A2"].Text = "ACTIVE FILTERS: " + SearchPattern;
                worksheet.Range["A2:I2"].CellStyle.Font.Bold = true;
                worksheet.Range["A2:I2"].CellStyle.Font.Size = 16;
                worksheet.Range["A2:I2"].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignCenter;

                // Add title row: REPORT GENERATED DATE:
                worksheet.Range["A3:I3"].Merge();
                worksheet.Range["A3"].Text = "REPORT GENERATED DATE: " + DateTime.Now.ToString("M/dd/yyyy");
                worksheet.Range["A3:I3"].CellStyle.Font.Bold = true;
                worksheet.Range["A3:I3"].CellStyle.Font.Size = 16;
                worksheet.Range["A3:I3"].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignCenter;
                DataTable table = excelData;

                worksheet.ImportDataTable(table, true, 4, 1);

                //this is giving font size = 8 to records

                for (int row = 4; row <= excelData.Rows.Count + 3; row++)
                {
                    for (int col = 1; col <= excelData.Columns.Count; col++)
                    {
                        worksheet.Rows[row].CellStyle.Font.Size = 8;
                        worksheet.Rows[row].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignLeft;
                    }
                }
                worksheet.Columns[6].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignCenter;
                worksheet.Columns[3].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignCenter;

                worksheet.Range["A4:I4"].CellStyle.Font.Bold = true;

                worksheet.Range["A4:I4"].CellStyle.Font.Size = 10;

                //Setting Column width of specific columns

                worksheet.Columns[1].ColumnWidth = 26;
                worksheet.Columns[2].ColumnWidth = 67;
                worksheet.Columns[3].ColumnWidth = 11;
                worksheet.Columns[4].ColumnWidth = 16;
                worksheet.Columns[5].ColumnWidth = 16;
                worksheet.Columns[6].ColumnWidth = 12;
                
                //Wrapping specific columns
                worksheet.Columns[1].CellStyle.WrapText = true;
                worksheet.Columns[2].CellStyle.WrapText = true;
                worksheet.Columns[4].CellStyle.WrapText = true;
                worksheet.Columns[5].CellStyle.WrapText = true;
                worksheet.Columns[6].CellStyle.WrapText = true;

                worksheet.UsedRange.AutofitRows();
                worksheet.UsedRange.AutofitColumns();

                

                MemoryStream stream = new MemoryStream();

                workbook.SaveAs(stream);

                stream.Seek(0, SeekOrigin.Begin);
                byte[] bytes = stream.ToArray();
                string base64String = Convert.ToBase64String(bytes);

                await jSRuntime.InvokeVoidAsync("jsSaveAsFile", "Recurring" + ".xlsx", base64String);

                _toastService.ShowSuccess("Generating Report Started, Please Wait.");

            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Rec task Excel print error:", ex);
            }
        }

        public async Task RecurringTaskDueToday()
        {
            if (IsFilterGridDueToday)
            {
                IsGridSpinner = true;

                IsFilterGridDueToday = false;
                filterDueToday = "btn-primary";

                await LoadData();
                await Pagination();
                await PercentageComponentRef.ReloadComponentAsync();
                IsActivePagination = true;

                IsGridSpinner = false;
            }
            else
            {
                IsGridSpinner = true;

                IsActivePagination = false;
                IsFilterGridDueToday = true;
                filterDueToday = "btn-danger";

                StateHasChanged();
                Tasks = AllRecurringTasks.Where(x => x.IsDeleted == false
                && x.IsDeactivated == false && x.IsApproved && x.UpcomingDate == DateTime.Today).ToList();
                perPageItem = Tasks.Count();

                SearchPattern = "TASK DUE TODAY";
            }
        }

        public async Task FilterGridPastDueDate()
        {
            if (IsFilteGridPastDueDate)
            {
                IsGridSpinner = true;

                IsFilteGridPastDueDate = false;
                filterPastDueDqate = "btn-primary";

                await LoadData();
                await Pagination();
                await PercentageComponentRef.ReloadComponentAsync();
                IsActivePagination = true;

                IsGridSpinner = false;
            }
            else
            {
                IsGridSpinner = true;

                IsFilteGridPastDueDate = true;
                IsActivePagination = false;
                filterPastDueDqate = "btn-danger";
                
                StateHasChanged();
                Tasks = AllRecurringTasks.Where(x => x.IsDeleted == false
                && x.IsDeactivated == false && x.IsApproved && x.UpcomingDate < DateTime.Today).ToList();
                
                perPageItem = Tasks.Count();

                SearchPattern = "PAST DUE TASKS";                
            }
        }

        protected void StartEmail(TasksRecTasksViewModel task)
        {
            IsActiveSimpleEmailComponent = true;
            SelectTaskForSimpleEmail = task;
        }

        public async Task DeactivateSimpleEmailComponent()
        {
            await Task.Run(() => IsActiveSimpleEmailComponent = false);
            StateHasChanged();
        }

        protected async Task FilterFields(string PersonResponsible)
        {
            await RecurringTasksGrid.ClearFilteringAsync();
            if (PersonResponsible != AllEmployees)
            {
                await RecurringTasksGrid.FilterByColumnAsync(nameof(TasksRecTasksViewModel.PersonResponsible),
             "equal", PersonResponsible, "or");
            }
        }

        protected void LoadFiles(TasksRecTasksViewModel TaskModel)
        {
            IsActiveMultipleFileComponent = true;
            SelectedTaskViewModel = TaskModel;
        }

        protected void ViewFiles(TasksRecTasksViewModel Model)
        {
            if (Model != null)
            {
                IsActiveSingleFileComponent = true;
                TaskFile = Model.InstructionFileLink;
            }
        }

        protected void ReturnTo5STasks()
        {
            navigationManager.NavigateTo(AppInformation.fivesUrl);
        }

        protected void ReturnToJarvis()
        {
            navigationManager.NavigateTo(AppInformation.jarvisUrl);
        }

        public void ViewTaskUpdates(TasksRecTasksViewModel args, bool IsEdit)
        {
            IsActiveRecTaskUpdateComponent = true;

            SelectedTaskForUpdate = args;
        }

        public void ViewTaskSubTasks(int Id)
        {
            navigationManager.NavigateTo($"/ViewRecSubTasks/{Id}");
        }

        protected void ReAssignTasks()
        {
            navigationManager.NavigateTo($"/ReAssignRecTasks/");
        }

        protected async Task BeforeStartFilterGrid(ActionEventArgs<TasksRecTasksViewModel> args)
        {
            PageLoadTime.StartTime = DateTime.Now;
            PageLoadTime.SectionName = "FilteringTasks";

            if (args.RequestType == Syncfusion.Blazor.Grids.Action.Searching)
            {
                IsGridSpinner = true;
            }
            else if (args.RequestType == Syncfusion.Blazor.Grids.Action.FilterBeforeOpen)
            {
                if (args.ColumnName == "Initiator" || args.ColumnName == "PersonResponsible"
                    || args.ColumnName == "AuditPerson")
                {
                    FilterMenuList = _employees.Select(e => new TasksRecMenuFilterList
                    {
                        Name = e.FullName
                    });
                }
                else if (args.ColumnName == "JobTitle")
                {
                    FilterMenuList = _jobs.Select(e => new TasksRecMenuFilterList
                    {
                        Name = e.Name
                    });
                }
                else if (args.ColumnName == "Frequency")
                {
                    FilterMenuList = _frequency.Select(f => new TasksRecMenuFilterList
                    {
                        Name = f.Frequency
                    });
                }
                else if (args.CurrentFilterObject != null)
                {
                    IsGridSpinner = true;
                }
            }
        }

        protected async Task StartFilteringGrid(ActionEventArgs<TasksRecTasksViewModel> args)
        {
            if (args.RequestType == Syncfusion.Blazor.Grids.Action.Searching)
            {
                IsActivePagination = false;

                Tasks = (await taskService.GetRecurringTasksBySearchAsync(false, false, args.SearchString)).ToList();

                perPageItem = Tasks.Count();
                SearchPattern = Convert.ToString(args.SearchString);

                IsGridSpinner = false;

                await PercentageComponentRef.ReloadComponentAfterFilterAsync(Tasks);
            }
            else if (args.RequestType == Syncfusion.Blazor.Grids.Action.Filtering
                && args.CurrentFilterObject != null
                && args.CurrentFilteringColumn != null
                && string.IsNullOrEmpty(SearchPattern))
            {
                IsActivePagination = false;
                Tasks = (await taskService.GetRecurringTasksBySearchAsync(false, false,
                    Convert.ToString(args.CurrentFilterObject.Value)))
                    .ToList();

                perPageItem = Tasks.Count();
                SearchPattern = Convert.ToString(args.CurrentFilterObject.Value);

                IsGridSpinner = false;

                await PercentageComponentRef.ReloadComponentAfterFilterAsync(Tasks);
            }
            else if (args.RequestType == Syncfusion.Blazor.Grids.Action.Filtering
                && args.CurrentFilterObject != null
                && args.CurrentFilteringColumn != null)
            {
                IsActivePagination = false;

                if (args.CurrentFilteringColumn == "PersonResponsible")
                {
                    var filteredData = await RecurringTasksGrid.GetFilteredRecordsAsync();
                    List<TasksRecTasksViewModel> filteredList = JsonConvert
                        .DeserializeObject<List<TasksRecTasksViewModel>>(JsonConvert.SerializeObject(filteredData));
                    Tasks = filteredList;
                }
                else
                {
                    Tasks = (await taskService.GetRecurringTasksBySearchAsync(false, false,
                        Convert.ToString(args.CurrentFilterObject.Value)))
                        .ToList();
                }

                perPageItem = Tasks.Count();
                SecondSearchPattern = Convert.ToString(args.CurrentFilterObject.Value);

                SearchPattern = SearchPattern + ", " + SecondSearchPattern;
                SecondSearchPattern = string.Empty;
                IsGridSpinner = false;

                await PercentageComponentRef.ReloadComponentAfterFilterAsync(Tasks);
            }
            else if (args.CurrentFilterObject != null
                && args.CurrentFilteringColumn == null)
            {
                IsActivePagination = true;
                SearchPattern = null;
                await LoadData();
                await Pagination();

                await PercentageComponentRef.ReloadComponentAsync();
            }
            else if (IsActivePercentageFilter)
            {
                IsGridSpinner = false;
                await RecurringTasksGrid.ClearFilteringAsync();
                await PercentageComponentRef.ReloadComponentAfterFilterAsync(Tasks);
                AllRecurringTasks = Tasks;
                IsActivePercentageFilter = false;
            }
            else if (!string.IsNullOrEmpty(SearchPattern))
            {
                IsGridSpinner = false;
                var filteredData = await RecurringTasksGrid.GetFilteredRecordsAsync();
                List<TasksRecTasksViewModel> filteredList = JsonConvert
                    .DeserializeObject<List<TasksRecTasksViewModel>>(JsonConvert.SerializeObject(filteredData));
                AllRecurringTasks = filteredList;
                if (!string.IsNullOrEmpty(SecondSearchPattern))
                {
                    SearchPattern = SearchPattern + ", " + SecondSearchPattern;
                }
                SecondSearchPattern = string.Empty;
                await PercentageComponentRef.ReloadComponentAfterFilterAsync(filteredList);
            }

            await PageLoadTimeCalculation();
        }

        public async Task RefreshViewRecurringTaskComponent(int recurringId)
        {
            if (recurringId > 0)
            {
                if (ApproverId != null) {
                    navigationManager.NavigateTo(navigationManager.Uri, true);
                }
                var newUpdate = await taskService.GetRecurringTaskById(recurringId);
                var oldUpdate = Tasks.FirstOrDefault(x => x.Id == recurringId);

                if (newUpdate == null || (newUpdate != null && (newUpdate.IsDeleted || newUpdate.IsDeactivated)) && Tasks.Any())
                {
                    Tasks.Remove(oldUpdate);
                }
                else
                {
                    if (oldUpdate != null && Tasks.Any())
                    {
                        int updateIndex = Tasks.IndexOf(oldUpdate);

                        if (updateIndex > -1)
                        {
                            Tasks[updateIndex] = newUpdate;
                        }
                    }
                }

                if (RecurringTasksGrid != null)
                {
                    await RecurringTasksGrid.RefreshColumnsAsync();
                    await RecurringTasksGrid.Refresh();
                }

                StateHasChanged();
            }
        }

        public async Task RefreshViewRecurringWithPercentageComponent(int recTaskId)
        {
            await RefreshViewRecurringTaskComponent(recTaskId);
            await PercentageComponentRef.ReloadComponentAsync(recTaskId, null);
        }

        protected async Task RecurringTaskPercantageCallBackEvent(string PersonResponsible)
        {
            if (PersonResponsible == "ALL EMPLOYEES")
            {
                IsActivePagination = true;
                await LoadData();
                await Pagination();
            }
            else
            {
                IsActivePagination = false;
                Tasks = (await taskService.GetRecurringTasks(false, false, null, PersonResponsible)).ToList();
            }

            SearchPattern = PersonResponsible;
        }

        protected async Task RecurringTaskTaskAreaCallBackEvent(string taskAreak)
        {
            if (!string.IsNullOrEmpty(taskAreak))
            {
                IsActivePagination = false;
                Tasks = (await taskService.GetRecurringTasksByTaskAreaAsync(false, false, ApproverId == null, null, taskAreak)).ToList();
                SearchPattern = taskAreak;
            }
        }

        public async Task SuccessMessageViewRecurringTasksComponent(string message)
        {
            await Task.Run(() => _toastService.ShowSuccess(message));
        }

        public async Task DeactiveCreateComponent()
        {
            await Task.Run(() => IsActiveCreateTask = false);
        }

        protected async Task SuccessMessageFromSingleFile(bool IsSuccess)
        {
            if (IsSuccess)
                await Task.Run(() => IsActiveSingleFileComponent = false);
        }

        protected async void TaskAuthentication(TasksRecTasksViewModel RecTask, string actionName)
        {
            var authUser = _AuthenticationStateProvider.GetAuthenticationStateAsync().Result.User;

            if (authUser == null) return;

            string userName = authUser.Claims.FirstOrDefault(c => c.Type == "name")?.Value;

            if (string.IsNullOrEmpty(userName)) return;

            string userEmail = (await _loging.GetUserByUserNameAsync(userName))?.Email;

            if (string.IsNullOrEmpty(userEmail)) _toastService.ShowError("Email Not Found.");

            string[] authorizationList = RecTask.AuthorizationList.Split(";");

            if (authorizationList.Any() && authorizationList.Contains(userEmail))
            {
                await OpenRecurringTask(RecTask, actionName);
            }
            else
            {
                _toastService.ShowError("Access Denied");
            }
        }

        protected async Task OpenRecurringTask(TasksRecTasksViewModel task, string actionName)
        {
            if (task != null)
            {
                switch (actionName)
                {
                    case "edit":
                        IsActiveCreateTask = true;
                        RecurringTask = task;
                        break;

                    case "delete":
                        IsActiveDeleteComponent = true;
                        SelectedTaskViewModel = task;
                        break;

                    case "nudge":
                        await SendNudgeEmail(task);
                        break;

                    case "loadFiles":
                        IsActiveMultipleFileComponent = true;
                        SelectedTaskViewModel = task;
                        break;

                    case "sendEmail":
                        IsActiveSimpleEmailComponent = true;
                        SelectTaskForSimpleEmail = task;
                        break;

                    case "update":
                        IsActiveRecTaskUpdateComponent = true;
                        SelectedTaskForUpdate = task;
                        break;

                    case "subTask":
                        ViewTaskSubTasks(task.Id);
                        break;

                    case "deactivete":
                        IsActiveDeactivateComponent = true;
                        SelectedTaskViewModel = task;
                        break;

                    case "instruction":
                        IsActiveSingleFileComponent = true;
                        ViewFiles(task);
                        break;

                    default:
                        logger.LogWarning("Please enter valid information");
                        break;
                }
            }
        }

        protected async Task DeactivateFromMultipleFile()
        {
            await Task.Run(() => IsActiveMultipleFileComponent = false);
        }

        public async Task DeactivateRecTaskUpdateComponent()
        {
            await Task.Run(() => IsActiveRecTaskUpdateComponent = false);
            StateHasChanged();
        }

        public async Task DeactivateActiveRecTaskComponent()
        {
            await Task.Run(() => IsActiveActiveRecComponent = false);
        }
        protected void DialogClose()
        {
            ComboBoxVisibility = false;
        }
        protected void Keydown(KeyboardEventArgs args)
        {
            if (args.Code == "Enter" && ChangeByEmployeeName != null)
            {
                OpenRecurringTaskByEmployee();
            }
        }

        protected async void OpenRecurringTaskByEmployee()
        {
            if (ChangeByEmployeeName != null)
            {
                DialogClose();
                isOpenAllRecurringTasks = true;
                await RecurringTasksGrid.SearchAsync(ChangeByEmployeeName);
            }
            else
            {
                ComboBoxVisibility = true;
                _toastService.ShowError("PLEASE SELECT USERNAME");
            }
        }
        protected void OpenAllRecurringTask()
        {
            if (ChangeByEmployeeName != null)
            {
                DialogClose();
                isOpenAllRecurringTasks = false;
            }
            else
            {
                ComboBoxVisibility = true;
                _toastService.ShowError("PLEASE SELECT USERNAME");
            }

        }
        protected async void OpenPendingTasks()
        {
            if (ChangeByEmployeeName != null)
            {
                var employee = await employeeService.GetEmployee(ChangeByEmployeeName);
                if (employee != null)
                {
                    ApproverId = employee.Id;
                    navigationManager.NavigateTo("/viewrecurringtasks/pending/" + ApproverId, true);
                }
            }
            else
            {
                _toastService.ShowError("PLEASE SELECT USERNAME TO APPROVE THE TASKS..!");
            }

        }
    }
}
