using System.Linq;
using BlazorDownloadFile;
using Blazored.Toast.Services;
using CamcoTasks.Helpers;
using CamcoTasks.Service.IService;
using CamcoTasks.ViewModels.EmployeeDTO;
using CamcoTasks.ViewModels.TasksImagesDTO;
using CamcoTasks.ViewModels.TasksTasksDTO;
using CamcoTasks.ViewModels.TasksTasksTaskTypeDTO;
using CamcoTasks.ViewModels.TasksTaskUpdatesDTO;
using CamcoTasks.Infrastructure.Common.Email;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using OfficeOpenXml;
using Syncfusion.Blazor.Data;
using Syncfusion.Blazor.DropDowns;
using Syncfusion.Blazor.Grids;
using Syncfusion.Blazor.Navigations;
using System.Text.Json;
using Syncfusion.Blazor.Popups;
using CamcoTasks.Infrastructure.Entities.TaskInfo;
using CamcoTasks.Service.Service;
using System.Threading.Tasks;
using CamcoTasks.Infrastructure.EnumHelper.Enums.Task;
using CamcoTasks.Infrastructure.Defaults;
using CamcoTasks.Services;



namespace CamcoTasks.Pages.Tasks.ViewTasks
{
    public class ViewTasksModel : ComponentBase
    {
        [Inject] private NavigationManager NavigationManager { get; set; }

        [Inject] private IEmailService emailService { get; set; }

        [Inject] private IWebHostEnvironment WebHostEnvironment { get; set; }

        //[Inject] private IJSRuntime jSRuntime { get; set; }

        [Inject] private IBlazorDownloadFileService blazorDownloadFileService { get; set; }

        [Inject] private IToastService _toastService { get; set; }
        [Inject] protected ITasksService taskService { get; set; }
        [Inject] private IJSRuntime JSRuntime { get; set; }
        [Inject] protected IEmployeeService EmployeeService { get; set; }
        [Inject] protected IHttpContextAccessor HttpContextAccessor { get; set; }
        [Inject] protected IUserContextService UserContextService { get; set; }
        [Inject] private TaskStateService TaskStateService { get; set; }
        public OnetimeTaskBorderModel BorderModel { get; set; } = new OnetimeTaskBorderModel();
        protected SfComboBox<string, EmployeeViewModel> SelectEmployeeDropDown { get; set; }
        protected string StatusDropdownVal { get; set; } = "2";
        protected string TaskImage { get; set; }
        protected string TaskTypeValue { get; set; }
        protected string OldTaskFile { get; set; }
        public string Fwidth { get; set; } = "455";
        public string LWidth { get; set; } = "330";
        public string ColumnWidth { get; set; } = "455";
        public string modalClass = "show";
        public string personModalDisplay = "none";
        public string personModalTitle = "";
        public string managerModalDisplay = "none";
        public string managerModalTitle = "";
        public int selectedTaskId = 0;
        public int selectedManagerId = 0;
        public int selectTaskId = 0;
        public bool isModalOpen = false;
        protected bool sideBarModalPopUpOpen = false;
        public string statusModalDisplay = "none";
        public string statusModalTitle = "";
        public int? selectedStatusId = 0;
        protected TasksTasksViewModel tasksViewModel { get; set; }
        protected SfGrid<TasksTasksViewModel> TaskGrid { get; set; }
        private static List<TasksTasksViewModel> mainTasksModel { get; set; } = new List<TasksTasksViewModel>();
        public List<TasksTasksViewModel> Tasks { get; set; }
        public List<EmployeeViewModel> Employees { get; set; }
        public List<string> TaskTypes { get; set; }
        public List<string> ResponsiblePerson { get; set; }
        public List<string> Initiator { get; set; }
        public List<string> StatusDisplay { get; set; }
        public string ResponsiveClass = "col-10";
        public string Dnone = "";
        public string Zindex = "-1000";
        public string padding = "";
        public string ButtonClass = "";
        protected TasksTaskUpdatesViewModel TaskFile { get; set; }
        public List<TasksTasksViewModel> SelectedTypeTasks { get; set; }
        protected List<TasksImagesViewModel> SelectedTaskFiles { get; set; } = new List<TasksImagesViewModel>();
        protected bool IsInnerUpdate { get; set; } = false;
        protected bool IsEditTaskType { get; set; } = false;
        public bool IsLoadData { get; set; } = true;
        protected bool IsActiveTasksCreateComponent { get; set; } = false;
        protected bool ForceRenderComponent { get; set; } = false;
        protected bool IsActiveTaskDeleteComponent { get; set; } = false;
        protected bool IsActiveTasksSimpleEmailComponent { get; set; } = false;
        protected bool IsActiveTasksUpdateViewComponent { get; set; } = false;
        protected bool IsActiveViewTaskTypesComponent { get; set; } = false;
        protected bool IsActiveTypeDialogComponent { get; set; } = false;
        protected bool IsDoneReview { get; set; } = true;
        public int TaskCount { get; set; } = 0;
        protected int ProgressMax { get; set; } = 100;
        public TasksTasksViewModel SelectedEmployeeTask { get; set; } = new TasksTasksViewModel();
        public TasksTasksViewModel SelectedTask { get; set; } = new TasksTasksViewModel() { DateAdded = DateTime.Now };
        protected TasksTasksViewModel TasksCreateComponent { get; set; }
        protected TasksTaskUpdatesViewModel SelectedUpdateViewModel { get; set; } = new TasksTaskUpdatesViewModel();

        public TasksTaskUpdatesViewModel NewUpdate { get; set; } =
            new TasksTaskUpdatesViewModel() { UpdateDate = DateTime.Today };

        public TasksTasksTaskTypeViewModel NewTaskType { get; set; } = new TasksTasksTaskTypeViewModel();
        protected TasksTasksViewModel TaskForDeleteComponent { get; set; }
        protected TasksTasksViewModel TaskForSimpleEmailComponent { get; set; }
        protected TasksTasksViewModel TaskForUpdateViewComponent { get; set; }
        protected TasksTasksViewModel TaskForViewTaskTypesComponent { get; set; }
        public List<int> Progresses { get; set; } = new List<int>() { 0, 10, 20, 30, 40, 50, 60, 70, 80, 90, 100 };
        public string SearchText = string.Empty;
        protected List<TasksTasksViewModel> filteredPersons;
        protected string selectedPerson;
        protected string selectedManager;
        public bool isButtonClicked = false;
        public SfSidebar sidebarObj;
        public bool SidebarToggle = true;
        public TasksTasksViewModel CurrentTask { get; set; }
        public string modalStyle = "top: 0px; left: 0px;";
        public bool IsEditorVisible = false;
        public string DocumentContent { get; set; } = string.Empty;
        public string DocumentTitle { get; set; } = "";
        public List<DropdownsOption> dropdownOption = new();
        public Helpers.Folders myFolder = new();
        public FiltersCriteria criteria = new();
        public SortColumns sortColumns = new();
        private List<TasksTasksViewModel> GridData = new();
        private List<TasksTasksViewModel> OriginalData = new();
        public const int maxSortingColumns = 5;
        protected List<string> AvailableConditions = new List<string>
            { "Equals", "Not Equals", "Contains", "Does Not Contain" };

        public List<Helpers.Folders> Folders = new List<Helpers.Folders>();
        const string DoneStatus = "3";
        const string InProgressStatus = "2";
        protected bool IsSortPopupVisible;
        public bool ImportFilePopup { get; set; } = false;
        protected readonly List<string> _sortDirections = new() { "Ascending" };

        protected readonly List<SortDirectionOption> SortingDirections = new()
        {
            new() { DisplayText  = "Ascending", Direction   = SortDirection.Ascending },
            new() { DisplayText  = "Descending", Direction   = SortDirection.Descending }
        };

        protected class SortDirectionOption
        {
            public string DisplayText { get; set; } = "";
            public SortDirection Direction { get; set; }
        }

        protected List<GridSortColumn> SortingColumns { get; set; } = new()
        {
            new GridSortColumn { Field = "ID", Direction = SortDirection.Ascending }
        };

        protected async Task ChangeSortingField(string selectedField, GridSortColumn sortColumn)
        {
            if (sortColumn == null || string.IsNullOrEmpty(selectedField)) return;

            sortColumn.Field = selectedField;

            await ApplySortingAsync();
        }

        protected async Task ChangeSortingDirection(SortDirection selectedDirection, GridSortColumn column)
        {
            if (column == null) return;

            column.Direction = selectedDirection;
            await ApplySortingAsync();
        }


        private async Task ApplySortingAsync()
        {
            if (TaskGrid == null) return;

            List<GridSortColumn> sortColumns = new();

            foreach (var column in SortingColumns)
            {
                if (!string.IsNullOrEmpty(column.Field))
                {
                    sortColumns.Add(new GridSortColumn
                    {
                        Field = column.Field,
                        Direction = column.Direction
                    });
                }
            }

            if (sortColumns.Count > 0)
            {
                TaskGrid.SortSettings.Columns = sortColumns;
                await TaskGrid.Refresh();
            }
        }

        protected void ToggleSortPopup()
        {
            IsSortPopupVisible = !IsSortPopupVisible;
        }

        protected void CloseSortPopup()
        {
            IsSortPopupVisible = false;
            StateHasChanged();
        }

        protected void AddSortingColumn()
        {
            if (SortingColumns.Count >= maxSortingColumns)
                return;

            if (!SortingColumns.Any(x => string.IsNullOrEmpty(x.Field)))
            {
                SortingColumns.Add(new GridSortColumn() { Field = string.Empty, Direction = SortDirection.Ascending });
            }

        }

        protected void RemoveSortingColumn(GridSortColumn columnToRemove)
        {
            SortingColumns.Remove(columnToRemove);
            StateHasChanged();
        }

        protected List<DropdownsOption> DropdownOptions = new()
        {
            new DropdownsOption { DisplayText = "ID", Direction = "ID" },
            new DropdownsOption { DisplayText = "LAST UPDATE", Direction = "LastUpdate" },
            new DropdownsOption { DisplayText = "DATE ADDED", Direction = "DateAdded" },
            new DropdownsOption { DisplayText = "PERSON RESPONSIBLE", Direction = "PersonResponsible" },
            new DropdownsOption { DisplayText = "MANAGER", Direction = "Initiator" }
        };

        protected async Task OnDirectionChange(Microsoft.AspNetCore.Components.ChangeEventArgs args, SortColumns item)
        {
            item.Direction = args?.Value?.ToString();
            if (!string.IsNullOrWhiteSpace(item.Field) && !string.IsNullOrWhiteSpace(item.Direction))
            {
                var sortDirection = item.Direction.Equals("Ascending", StringComparison.OrdinalIgnoreCase)
                    ? SortDirection.Ascending
                    : SortDirection.Descending;
                await TaskGrid.SortColumnAsync(item.Field, sortDirection);
                StateHasChanged();
            }
        }

        public string errorMessage = "";
        public bool isEditing = false;
        public int editingTaskId;
        protected IEnumerable<EmployeeViewModel> employeeList { get; set; }
        public string SidebarTop = "0px";
        public string SidebarLeft = "0px";
        public bool isDragging = false;
        public string sidebarLeft = "0px";
        public string sidebarTop = "0px";
        public double startX, startY;
        public int selectedCount = 0;
        protected List<TasksTasksViewModel> SelectedTasks = new List<TasksTasksViewModel>();
        protected string SelectedTaskType { get; set; } = "ALL";
        private IBrowserFile? uploadedFile;
        protected StatusDisplay _statusDisplay = new StatusDisplay();
        protected bool IsSelectedTaskModalVisible = false;
        protected TasksTasksViewModel SelectedRows = new();
        protected TasksTasksTaskTypeViewModel SelectedTaskDetail { get; set; } = new TasksTasksTaskTypeViewModel();
        public List<TasksTasksTaskTypeViewModel> TaskTypeModelList { get; set; } = new();
        public bool IsPersonDialogVisible = false;
        public List<string> Person { get; set; }
        public string SelectedEmployeeName { get; set; }
        private bool ShowPopup { get; set; } = true;
        public List<string> EmployeeSelect { get; set; }
        protected SfDialog LoginDialog;
        protected bool IsLoginDialogVisible = true;
        protected bool isCostingEdit;
        protected int costingTaskId;
        protected string editCostingValue;
        protected bool isEditingCosting = false;
        protected List<TasksTasksViewModel> DeleteSelectedTasks { get; set; }
        public GridSelectionSettings SelectionSettings { get; set; } = new()
        {
            Type = SelectionType.Multiple,
            Mode = Syncfusion.Blazor.Grids.SelectionMode.Row,
            PersistSelection = true
        };

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await LoadContext();
                await JSRuntime.InvokeAsync<object>("modalDraggable");
                StateHasChanged();
            }
        }

        protected async Task LoadContext()
        {
            await LoadData();

            TaskTypeValue = "2";

            IsLoadData = false;
        }

        protected async Task LoadData()
        {
            employeeList = await EmployeeService.GetListAsync(true, false);
            Employees = employeeList.Where(a => a.FullName != null).ToList();

            if (UserContextService.CurrentEmployeeId != 0)
            {
                var currentEmployee = employeeList.FirstOrDefault(a => a.Id == UserContextService.CurrentEmployeeId);
                if (currentEmployee != null)
                {
                    var personName = currentEmployee.FullName?.Trim();
                    var tasksByPerson = await taskService.GetTasksByPerson(personName);
                    mainTasksModel = tasksByPerson
                        .OrderByDescending(x => x.Id)
                        .ToList();
                    mainTasksModel = (await taskService.GetTasksByPerson(personName))
                    mainTasksModel = (await taskService.GetTasksByPerson(currentEmployee.FullName))
                        .OrderByDescending(x => x.Id).ToList();
                }
                else
                {
                    mainTasksModel = new();
                }
            }
            else
            {
                mainTasksModel = new();
            }

            await AssignLatestUpdates();
            Tasks = mainTasksModel.Where(a => !a.DateCompleted.HasValue).ToList();
            TaskTypes = mainTasksModel.Where(a => !a.DateCompleted.HasValue && !string.IsNullOrWhiteSpace(a.TaskType))
                .Select(a => a.TaskType.ToUpper()).Distinct().OrderBy(a => a).ToList();
            ResponsiblePerson = employeeList.Select(a => a.FullName).OrderBy(a => a).ToList();
            Initiator = mainTasksModel.Select(x => x.Initiator).Distinct().OrderBy(a => a).ToList();
            TaskTypeModelList = (await taskService.GetTaskTypes()).ToList();
            EmployeeSelect = employeeList.Select(a => a.FullName).OrderBy(a => a).ToList();
            TaskCount = Tasks.Count;
            ResetPersonList();
            StateHasChanged();
        }

        private async Task AssignLatestUpdates()
        {
            var updates = await taskService.GetLatestUpdates();
            foreach (var item in updates)
            {
                var mainModel = mainTasksModel.FirstOrDefault(x => x.Id == item.TaskID);
                if (mainModel != null)
                {
                    mainModel.LatestUpdate = item.UpdateDate;
                }
            }
        }

        public async Task<int> GetTotalDueTasks()
        {
            int TotalDueTasks = 0;
            if (TaskGrid != null)
            {
                var rows = await TaskGrid.GetFilteredRecordsAsync();
                var TasksList =
                    JsonConvert.DeserializeObject<List<TasksTasksViewModel>>(JsonConvert.SerializeObject(rows));

                TotalDueTasks = TasksList.Count(x => x.DateCompleted?.Date < DateTime.Today);
            }
            else
            {
                TotalDueTasks = Tasks.Count(x => x.DateCompleted?.Date < DateTime.Today);
            }

            return TotalDueTasks;
        }

        public class DDData
        {
            public string Text { get; set; }
            public string Value { get; set; }
        }

        public class OnetimeTaskBorderModel
        {
            public string TaskType { get; set; } = "7px solid white";
            public string Initiator { get; set; } = "7px solid white";
            public string DateAdded { get; set; } = "7px solid white";
            public string CostingCode { get; set; } = "7px solid white";
            public string Department { get; set; } = "7px solid white";
            public string SelectFile { get; set; } = "7px solid white";
            public string Description { get; set; } = "7px solid white";

        }

        public List<DDData> DLData { get; set; } = new List<DDData>()
        {
            new DDData() { Text = "Show Incomplete", Value = "InProgressStatus" },
            new DDData() { Text = "Show Completed", Value = "DoneStatus" }
        };

        protected async Task FilterTaskType(ChangeEventArgs<string, DDData> args)
        {
            await TaskGrid.ClearFilteringAsync();
            if (args.ItemData.Text != "ALL")
            {
                await TaskGrid.FilterByColumnAsync(nameof(TasksTasksViewModel.TaskType),
                    "equal", args.ItemData, "or");
            }
        }

        public async Task ChangeData(ChangeEventArgs<string, DDData> args)
        {
            StatusDropdownVal = args.Value;

            if (StatusDropdownVal == "DoneStatus")
            {
                ColumnWidth = LWidth;
                Tasks = mainTasksModel.Where(a => a.TaskStatusId == (int)StatusType.Done).ToList();
            }
            else if (StatusDropdownVal == "InProgressStatus")
            {
                ColumnWidth = Fwidth;
                Tasks = mainTasksModel.Where(a =>
                    a.TaskStatusId == (int)StatusType.Default ||
                    a.TaskStatusId == (int)StatusType.TemporaryTabled ||
                    a.TaskStatusId == (int)StatusType.Tabled ||
                    a.TaskStatusId == (int)StatusType.WaitingForReview ||
                    a.TaskStatusId == (int)StatusType.InProgress
                ).ToList();
            }
            else
            {
                ColumnWidth = Fwidth;
                Tasks = mainTasksModel;
            }

            Tasks = Tasks.OrderBy(x => x.Priority).ToList();
            await Task.Delay(5);
        }
        protected void ViewTypes()
        {
            NavigationManager.NavigateTo($"/viewtasktypes/");
        }

        protected async Task FilterFields(int DepNum)
        {
            switch (DepNum)
            {
                case 0:
                    {
                        if (StatusDropdownVal == "2")
                        {
                            Tasks = mainTasksModel.Where(a => !a.DateCompleted.HasValue).ToList();
                        }
                        else if (StatusDropdownVal == "3")
                        {
                            Tasks = mainTasksModel.Where(a => a.DateCompleted.HasValue).ToList();
                        }
                        else
                        {
                            Tasks = mainTasksModel;
                        }

                        await TaskGrid.RefreshColumnsAsync();
                        break;
                    }
                case 1:
                    {
                        if (StatusDropdownVal == "2")
                        {
                            Tasks = mainTasksModel.Where(a =>
                                !a.DateCompleted.HasValue && !string.IsNullOrEmpty(a.Department) &&
                                a.Department.Contains("LATHE")).ToList();
                        }
                        else if (StatusDropdownVal == "3")
                        {
                            Tasks = mainTasksModel.Where(a =>
                                a.DateCompleted.HasValue && !string.IsNullOrEmpty(a.Department) &&
                                a.Department.Contains("LATHE")).ToList();
                        }
                        else
                        {
                            Tasks = mainTasksModel
                                .Where(a => !string.IsNullOrEmpty(a.Department) && a.Department.Contains("LATHE")).ToList();
                        }

                        await TaskGrid.RefreshColumnsAsync();
                        break;

                    }
                case 2:
                    {
                        if (StatusDropdownVal == "2")
                        {
                            Tasks = mainTasksModel.Where(a =>
                                !a.DateCompleted.HasValue && !string.IsNullOrEmpty(a.Department) &&
                                a.Department.Contains("MILL")).ToList();
                        }
                        else if (StatusDropdownVal == "3")
                        {
                            Tasks = mainTasksModel.Where(a =>
                                a.DateCompleted.HasValue && !string.IsNullOrEmpty(a.Department) &&
                                a.Department.Contains("MILL")).ToList();
                        }
                        else
                        {
                            Tasks = mainTasksModel
                                .Where(a => !string.IsNullOrEmpty(a.Department) && a.Department.Contains("MILL")).ToList();
                        }

                        await TaskGrid.RefreshColumnsAsync();
                        break;
                    }
                case 3:
                    {
                        if (StatusDropdownVal == "2")
                        {
                            Tasks = mainTasksModel.Where(a =>
                                !a.DateCompleted.HasValue && !string.IsNullOrEmpty(a.Department) &&
                                a.Department.Contains("QUALITY")).ToList();
                        }
                        else if (StatusDropdownVal == "3")
                        {
                            Tasks = mainTasksModel.Where(a =>
                                a.DateCompleted.HasValue && !string.IsNullOrEmpty(a.Department) &&
                                a.Department.Contains("QUALITY")).ToList();
                        }
                        else
                        {
                            Tasks = mainTasksModel.Where(a =>
                                !string.IsNullOrEmpty(a.Department) && a.Department.Contains("QUALITY")).ToList();
                        }

                        await TaskGrid.RefreshColumnsAsync();
                        break;
                    }
                default:
                    break;
            }
        }

        protected async Task RefreshUpdateModal(bool IsInner)
        {
            IsInnerUpdate = IsInner;
            SelectedUpdateViewModel = new TasksTaskUpdatesViewModel() { UpdateDate = DateTime.Today };
            await Task.Delay(1);
        }

        protected void StartDelete(TasksTasksViewModel task)
        {
            IsActiveTaskDeleteComponent = true;
            IsSelectedTaskModalVisible = false;
            TaskForDeleteComponent = task;
        }
        protected async Task DeleteSelectedTask()
        {
            IsSelectedTaskModalVisible = false;
            var taskIds = SelectedTasks.Select(t => t.Id).ToList();
            if (!taskIds.Any())
                return;
            await taskService.DeleteTaskAsync(taskIds);
            Tasks = Tasks.Where(t => !taskIds.Contains(t.Id)).ToList();
            SelectedTasks.Clear();
            await TaskGrid.Refresh();
        }

        protected async Task CallbackMessageFromTaskDeleteComponent(TasksTasksViewModel task)
        {
            if (task != null)
            {
                try
                {
                    task.IsDeleted = true;

                    await taskService.RemoveOneTask(task);

                    _toastService.ShowSuccess("Task Removed!");
                }
                catch (Exception ex)
                {

                    if (ex.InnerException != null)
                    {
                        _toastService.ShowError(ex.InnerException.Message);
                    }
                    else
                    {
                        _toastService.ShowError(ex.Message);
                    }
                }
            }

            await Task.Run(() => IsActiveTaskDeleteComponent = false);
        }

        protected async Task SendEmail(TasksTasksViewModel task, bool Nudeged, string RespondBody = null)
        {
            var tasktype = await taskService.GetTaskType(task.TaskType);

            if (tasktype != null)
            {
                var taskTypeEmail = string.IsNullOrEmpty(tasktype.Email)
                    ? (string.IsNullOrEmpty(tasktype.Email2) ? null : tasktype.Email2)
                    : tasktype.Email;

                if (string.IsNullOrEmpty(taskTypeEmail))
                {
                    _toastService.ShowWarning("Email Sending Error, Employee Has No Email");
                    return;
                }

                string body = "";

                if (!string.IsNullOrEmpty(RespondBody))
                    body = "Note: " + RespondBody + "<br>";

                body += "<label style=\"font-weight:bold\">Description: </label>" + task.Description +
                        "<br><label style=\"font-weight:bold\">Date Added: </label>" +
                        task.DateAdded?.Date.ToString("MM/dd/yyyy") +
                        "<br><label style=\"font-weight:bold\">Priority: </label>" + task.Priority +
                        "<br><label style=\"font-weight:bold\">Link To Mark Completed:</label>" +
                        NavigationManager.BaseUri + "viewtasks/";

                string Subject = "FRIENDLY REMINDER TO GET THIS TASK DONE";
                body = EmailDefaults.GenerateEmailTemplate("Tasks", body);
                await emailService.SendEmailAsync(EmailTypes.ActionBasedFriendlyReminderOneTimeTask,
                    Array.Empty<string>(), Subject, body, string.Empty, new string[] { taskTypeEmail });

                if (Nudeged)
                {
                    task.NudgeCount++;
                    await taskService.UpdateOneTask(task);
                }

                _toastService.ShowInfo("An Email Has Been Added To Queue");
            }
            else
            {
                _toastService.ShowError("Task Type Couldn't be Found");
                return;
            }

            IsSelectedTaskModalVisible = false;
        }

        public void ViewTaskUpdates(TasksTasksViewModel args)
        {
            IsActiveTasksUpdateViewComponent = true;
            TaskForUpdateViewComponent = args;
        }

        protected async void StartEmail(TasksTasksViewModel task)
        {
            IsActiveTasksSimpleEmailComponent = true;
            TaskForSimpleEmailComponent = task;
            IsSelectedTaskModalVisible = false;
            SelectedTasks.Clear();
            await TaskGrid.ClearSelectionAsync();
            StateHasChanged();
        }

        public async Task ToolbarClickHandler(Syncfusion.Blazor.Navigations.ClickEventArgs args)
        {
            if (args.Item.Text == "PRINT REPORT")
            {
                PdfExportProperties ExportProperties = new();
                ExportProperties.FileName = "Metrics Tasks - " + DateTime.Today.ToString("MM/dd/yyyy") + ".pdf";
                ExportProperties.DisableAutoFitWidth = true;
                ExportProperties.AllowHorizontalOverflow = true;


                ExportProperties.Columns = new List<GridColumn>()
                {
                    new GridColumn()
                    {
                        Field = nameof(TasksTasksViewModel.Id), HeaderText = "ID", TextAlign = TextAlign.Left,
                        Width = "40"
                    },
                    new GridColumn()
                    {
                        Field = nameof(TasksTasksViewModel.TaskType), HeaderText = " Task Type",
                        TextAlign = TextAlign.Left, Width = "75"
                    },
                    new GridColumn()
                    {
                        Field = nameof(TasksTasksViewModel.Description), HeaderText = "Description",
                        TextAlign = TextAlign.Left, Width = "290"
                    },
                    new GridColumn()
                    {
                        Field = nameof(TasksTasksViewModel.DateAdded), HeaderText = "Date Added",
                        TextAlign = TextAlign.Left, Width = "60", Format = "MM/dd/yyyy"
                    },
                    new GridColumn()
                    {
                        Field = nameof(TasksTasksViewModel.LatestUpdate), HeaderText = " Latest Update",
                        TextAlign = TextAlign.Left, Width = "60", Format = "MM/dd/yyyy"
                    }
                };

                List<PdfHeaderFooterContent> HeaderContent = new()
                {
                    new PdfHeaderFooterContent()
                    {
                        Type = ContentType.Text,
                        Value = "Metrics One-Time Tasks - " + DateTime.Today.ToString("MM/dd/yyyy"),
                        Position = new PdfPosition() { X = 0, Y = 0 },
                        Style = new PdfContentStyle()
                            { TextBrushColor = "#1C4084", FontSize = 24, DashStyle = PdfDashStyle.Solid }
                    }
                };

                PdfHeader Header = new()
                {
                    FromTop = 0,
                    Height = 50,
                    Contents = HeaderContent,
                };
                ExportProperties.Header = Header;

                await TaskGrid.ExportToPdfAsync(ExportProperties);
            }
            else if (args.Item.Text == "Excel Export")
            {
                ExcelExportProperties ExcelProperties = new ExcelExportProperties();
                ExcelProperties.FileName = "Metrics Tasks - " + DateTime.Today.ToString("MM/dd/yyyy") + ".csv";

                await TaskGrid.ExportToCsvAsync(ExcelProperties);
            }
        }

        public void SearchTaskType(Syncfusion.Blazor.Inputs.InputEventArgs search)
        {
            if (!string.IsNullOrWhiteSpace(search.Value))
            {
                TaskTypes = mainTasksModel
                    .Where(a => a.TaskType.ToLower().Contains(search.Value.Trim().ToLower()) &&
                                !a.DateCompleted.HasValue)
                    .Select(a => a.TaskType.ToUpper())
                    .Distinct()
                    .OrderBy(a => a)
                    .ToList();
            }
            else
            {
                TaskTypes = mainTasksModel
                    .Where(a => !a.DateCompleted.HasValue)
                    .Select(a => a.TaskType.ToUpper())
                    .Distinct()
                    .OrderBy(a => a)
                    .ToList();
            }
        }

        public void SearchValueChangeResponsiblePerson(Syncfusion.Blazor.Inputs.ChangedEventArgs args)
        {
            var list = employeeList.Where(a => a.FullName != null).ToList();
            ResponsiblePerson = list.Where(a => a.FullName.ToLower().Contains(args.Value.ToLower()))
                .Select(a => a.FullName.ToUpper())
                .OrderBy(a => a).ToList();
        }

        public void SearchValueChangeManager(Syncfusion.Blazor.Inputs.ChangedEventArgs args)
        {
            var list = mainTasksModel.Where(a => a.Initiator != null).ToList();
            Initiator = list.Where(a => a.Initiator.ToLower().Contains(args.Value.ToLower()))
                .Select(a => a.Initiator.ToUpper())
                .Distinct().OrderBy(a => a)
                .ToList();
        }

        public void OnTaskTypeClicked(string taskType)
        {
            if (string.IsNullOrWhiteSpace(taskType))
            {
                SelectedTaskType = "ALL";
                Tasks = mainTasksModel.Where(a => !a.DateCompleted.HasValue).ToList();
            }
            else
            {
                SelectedTaskType = taskType;
                Tasks = mainTasksModel.Where(a =>
                    !string.IsNullOrEmpty(a.TaskType)
                    && a.TaskType.ToUpper() == taskType.ToUpper()
                    && !a.DateCompleted.HasValue).ToList();

                SelectedTaskDetail = TaskTypeModelList.FirstOrDefault(t => t.TaskType.ToUpper() == taskType.ToUpper())
                    ?? new TasksTasksTaskTypeViewModel();
            }
        }

        public async Task DetailDataBoundHandler(DetailDataBoundEventArgs<TasksTasksViewModel> args)
        {
            var updatesModel = await taskService.GetTaskUpdates(args.Data.Id);

            args.Data.TasksTaskUpdates = updatesModel.ToList();
        }

        public async Task ChangePriorityVal(Microsoft.AspNetCore.Components.ChangeEventArgs args,
            TasksTasksViewModel EventTask)
        {
            var taskLook = Tasks.FirstOrDefault(x => x.Id != EventTask.Id &&
                                                     x.TaskType == EventTask.TaskType &&
                                                     x.Priority == Convert.ToInt32(args.Value));

            var difference = Convert.ToInt32(args.Value) - Convert.ToInt32(EventTask.Priority);

            if (taskLook != null && Math.Abs(difference) == 1)
            {
                taskLook.Priority = EventTask.Priority;

                await taskService.UpdateOneTask(taskLook);
                Tasks[Tasks.FindIndex(x => x.Id == taskLook.Id)] = taskLook;
                mainTasksModel[mainTasksModel.FindIndex(x => x.Id == taskLook.Id)] = taskLook;

                EventTask.Priority = Convert.ToInt32(args.Value);
                await taskService.UpdateOneTask(EventTask);
                Tasks[Tasks.FindIndex(x => x.Id == EventTask.Id)] = EventTask;
                mainTasksModel[mainTasksModel.FindIndex(x => x.Id == EventTask.Id)] = EventTask;

            }
            else if (taskLook != null && Math.Abs(difference) > 1)
            {
                EventTask.Priority = Convert.ToInt32(args.Value);
                await taskService.UpdateOneTask(EventTask);
                Tasks[Tasks.FindIndex(x => x.Id == EventTask.Id)] = EventTask;
                mainTasksModel[mainTasksModel.FindIndex(x => x.Id == EventTask.Id)] = EventTask;

                int PrevPriority = Convert.ToInt32(args.Value) + 1;
                while (taskLook != null)
                {
                    taskLook.Priority = PrevPriority;
                    await taskService.UpdateOneTask(taskLook);

                    Tasks[Tasks.FindIndex(x => x.Id == taskLook.Id)] = taskLook;
                    mainTasksModel[mainTasksModel.FindIndex(x => x.Id == taskLook.Id)] = taskLook;

                    taskLook = Tasks.FirstOrDefault(x => x.Id != taskLook.Id &&
                                                         x.TaskType == taskLook.TaskType &&
                                                         x.Priority == PrevPriority);

                    PrevPriority++;
                }
            }
            else if (taskLook == null)
            {
                EventTask.Priority = Convert.ToInt32(args.Value);
                await taskService.UpdateOneTask(EventTask);
                Tasks[Tasks.FindIndex(x => x.Id == EventTask.Id)] = EventTask;
                mainTasksModel[mainTasksModel.FindIndex(x => x.Id == EventTask.Id)] = EventTask;
            }

            Tasks = Tasks.OrderBy(x => x.Priority).ToList();
        }

        protected async Task ReviewCheckedChanged(Microsoft.AspNetCore.Components.ChangeEventArgs args,
            TasksTasksViewModel selectedTask)
        {
            while (!IsDoneReview)
                await Task.Delay(25);

            IsDoneReview = false;

            selectedTask.IsReviewed = (bool)args.Value;
            await taskService.UpdateOneTask(selectedTask);
            if (selectedTask.IsReviewed)
                _toastService.ShowSuccess("Task Marked Successfully");
            else
                _toastService.ShowSuccess("Task UnMarked Successfully");

            IsDoneReview = true;
        }

        protected async void StartOneTimeTask(TasksTasksViewModel task)
        {
            IsActiveTasksCreateComponent = true;
            ForceRenderComponent = true;
            TasksCreateComponent = task;
            await JSRuntime.InvokeAsync<object>("ShowModal", "#AddEditTaskModal");
            IsSelectedTaskModalVisible = false;
            StateHasChanged();
        }

        protected void SelectTaskByType(TasksTasksViewModel SelectedTask)
        {
            IsActiveViewTaskTypesComponent = true;
            TaskForViewTaskTypesComponent = SelectedTask;
        }

        protected async Task CallbackFromViewTaskTypesComponent()
        {
            await Task.Run(() => IsActiveViewTaskTypesComponent = false);
        }

        public void ViewOneTimeSubTasks(int Id)
        {
            NavigationManager.NavigateTo($"/ViewOneTimeSubTasks/{Id}");
        }

        public async Task StartFilteringGrid(ActionEventArgs<TasksTasksViewModel> args)
        {
            var rows = await TaskGrid.GetFilteredRecordsAsync();
            var TasksList = JsonConvert.DeserializeObject<List<TasksTasksViewModel>>(JsonConvert.SerializeObject(rows));
            TaskCount = TasksList.Count;
        }

        protected async Task CallbackMessageFromTasksCerateComponent(Dictionary<string, string> message)
        {
            if (message != null && message.Any())
            {
                foreach (var item in message)
                {
                    _toastService.ShowSuccess(item.Value + " " + item.Key);
                }
            }

            await Task.Run(() => IsActiveTasksCreateComponent = false);
        }

        protected async Task RefreshComponent(bool isReload)
        {
            if (isReload)
            {
                await LoadData();
                await TaskGrid.Refresh();
                StateHasChanged();
            }
        }

        protected async Task CallbackMessageFromTasksSimpleEmailComponent(Dictionary<string, string> message)
        {
            if (message != null && message.Any())
            {
                foreach (var item in message)
                {
                    _toastService.ShowInfo(item.Value + " " + item.Key);
                }
            }

            await Task.Run(() => IsActiveTasksSimpleEmailComponent = false);
        }

        protected async Task CallbackMessageFromTasksUpdateViewComponent(Dictionary<string, string> message)
        {
            if (message != null && message.Any())
            {
                foreach (var item in message)
                {
                    _toastService.ShowSuccess(item.Value + " " + item.Key);
                }
            }

            await Task.Run(() => IsActiveTasksUpdateViewComponent = false);
        }

        protected void TaskType()
        {
            IsActiveTypeDialogComponent = true;
        }

        protected async Task CallbackMessageFromTypeDialogComponent(Dictionary<string, string> message)
        {
            if (message != null && message.Any())
            {
                foreach (var item in message)
                {
                    _toastService.ShowSuccess(item.Value + " " + item.Key);
                }
            }

            await Task.Run(() => IsActiveTypeDialogComponent = false);
        }

        public void OnCommandClicked(TasksTasksViewModel args)
        {
            modalClass = "show";
            personModalDisplay = "block";
            personModalTitle = args.PersonResponsible;
            selectedTaskId = args.Id;
        }

        public void OnCommandClickedManager(TasksTasksViewModel args)
        {
            modalClass = "show";
            managerModalDisplay = "block";
            managerModalTitle = args.Initiator;
            selectedManagerId = args.Id;
        }

        public void OnCommandStatusType(TasksTasksViewModel taskViewModel)
        {
            CurrentTask = taskViewModel;
            modalClass = "show";
            statusModalDisplay = "block";

        }

        public void ModalClosePerson()
        {

            personModalDisplay = "none";
            modalClass = "";
        }

        public void ModalCloseManager()
        {

            managerModalDisplay = "none";
            modalClass = "";
        }

        public void SelectPerson(string personResponsible)
        {
            selectedPerson = personResponsible;
        }

        public void SelectManger(string initiator)
        {
            selectedManager = initiator;
        }

        public async Task SaveChanges()
        {
            if (!string.IsNullOrEmpty(selectedPerson))
            {
                var taskToUpdate = Tasks.FirstOrDefault(a => a.Id == selectedTaskId);
                if (taskToUpdate != null)
                {
                    taskToUpdate.PersonResponsible = selectedPerson;
                    var IsDone = await taskService.UpdateOneTask(taskToUpdate);
                    if (IsDone)
                    {
                        await TaskGrid.Refresh();
                        _toastService.ShowSuccess("Task Updated SuccessFully");
                        personModalTitle = "";
                        personModalDisplay = "none";
                    }
                }
            }
        }

        public async Task SaveChangesManager()
        {
            if (!string.IsNullOrEmpty(selectedManager))
            {
                var taskToUpdateManager = Tasks.FirstOrDefault(a => a.Id == selectedManagerId);
                if (taskToUpdateManager != null)
                {
                    taskToUpdateManager.Initiator = selectedManager;
                    var IsDone = await taskService.UpdateOneTask(taskToUpdateManager);
                    if (IsDone)
                    {
                        await TaskGrid.Refresh();
                        _toastService.ShowSuccess("Task Updated SuccessFully");
                        managerModalTitle = "";
                        managerModalDisplay = "none";
                    }
                }
            }
        }

        public async Task SaveTaskStatusAsync(TasksTasksViewModel task)
        {
            var statusType = mainTasksModel.FirstOrDefault(t => t.Id == task.Id);
            if (statusType != null)
            {
                statusType.TaskStatusId = task.TaskStatusId;
                bool updateSuccess = await taskService.UpdateOneTask(statusType);
                if (updateSuccess)
                {
                    await TaskGrid.Refresh();
                    _toastService.ShowSuccess("Task status updated successfully.");
                    statusModalDisplay = "none";
                    await TaskStateService.NotifyStateChangedAsync();
                }
                else
                {
                    _toastService.ShowError("Failed to update task status.");
                }
            }
        }


        protected void HandleBackdropClick(MouseEventArgs e)
        {
            if (personModalDisplay == "block")
            {
                personModalDisplay = "none";
                personModalTitle = string.Empty;
            }
            else if (managerModalDisplay == "block")
            {
                managerModalDisplay = "none";
                managerModalTitle = string.Empty;
            }
            else if (statusModalDisplay == "block")
            {
                statusModalDisplay = "none";
                statusModalTitle = string.Empty;
            }
        }

        public void Close()
        {
            SidebarToggle = false;
            ResponsiveClass = "col-10";
        }

        public void Toggle()
        {

            SidebarToggle = !SidebarToggle;
            if (!SidebarToggle)
            {
                ResponsiveClass = "col-12";
                Dnone = "d-none";
                Zindex = "1000";
                padding = "pl-4-5 pr-0";
                ButtonClass = "pin-button-col-12";
            }
            else
            {
                ResponsiveClass = "col-10";
                Dnone = "";
                Zindex = "-1000";
                padding = "p-0";
            }

        }

        protected string pinClass => SidebarToggle ? "fa-solid fa-thumbtack" : "fa-solid fa-thumbtack fa-rotate-90";

        protected void ChangeCostingCodeValue(Microsoft.AspNetCore.Components.ChangeEventArgs eventArgs,
            TasksTasksViewModel task)
        {
            var costingCodeChangeValue = eventArgs.Value?.ToString();
            if (costingCodeChangeValue != null && costingCodeChangeValue.StartsWith("P") &&
                int.TryParse(costingCodeChangeValue.Substring(1), out int parsedValue))
            {
                task.CostingCode = parsedValue;
            }
            else if (int.TryParse(costingCodeChangeValue, out parsedValue))
            {
                task.CostingCode = parsedValue;
            }
        }

        protected void ShowModalSidebar()
        {
            sideBarModalPopUpOpen = true;
        }

        protected void ClosePlusModal()
        {
            sideBarModalPopUpOpen = false;
        }

        protected void CloseModal()
        {
            isModalOpen = false;
        }

        protected void OpenStatusModal(TasksTasksViewModel task)
        {
            modalClass = "show";
            statusModalDisplay = "block";
            CurrentTask = task;
            Console.WriteLine(
                $"Opening modal for Task ID: {CurrentTask?.Id}, Current Status ID: {CurrentTask?.TaskStatusId}");

        }

        protected void CloseStatusModal()
        {
            modalClass = "hide";
            statusModalDisplay = "none";
        }

        protected async Task ChangeStatus(int status)
        {
            try
            {
                if (CurrentTask != null)
                {
                    CurrentTask.TaskStatusId = status;
                    await SaveTaskStatusAsync(CurrentTask);
                }

                CloseStatusModal();
            }

            catch (Exception ex)
            {
                throw new Exception("An error occurred while processing the task update.", ex);
            }
        }

        public async void RefreshGrid()
        {
            await TaskGrid.Refresh();
        }

        protected string GetStatusColor(StatusType status)
        {
            return status switch
            {
                StatusType.Done => "status-button Done",
                StatusType.TemporaryTabled => "status-button TemporaryTabled",
                StatusType.WaitingForReview => "status-button WaitingForReview",
                StatusType.InProgress => "status-button in-progress-color",
                StatusType.Tabled => "status-button Tabled",
                StatusType.Pending => "status-button Pending",
                _ => "status-button Default"
            };
        }

        protected int GetEnumValue(StatusType status)
        {
            return status switch
            {
                StatusType.Done => 5,
                StatusType.TemporaryTabled => 4,
                StatusType.WaitingForReview => 2,
                StatusType.InProgress => 0,
                StatusType.Tabled => 3,
                StatusType.Default => 6,
                StatusType.Pending => 1,
            };
        }

        protected string GetTaskStatusClass(string taskStatus) =>
            taskStatus switch
            {
                "In Progress" => "status-in-progress",
                "Pending" => "status-pending",
                "Waiting for Review" => "status-review",
                "Tabled" => "status-tabled",
                "Temporary Tabled" => "status-temp-tabled",
                "Done" => "status-done",
                _ => "status-default"
            };

        protected string GetInitials(string fullName)
        {
            if (string.IsNullOrWhiteSpace(fullName)) return string.Empty;
            var nameParts = fullName.Split(',');
            if (nameParts.Length < 2) return string.Empty;
            var lastName = nameParts[0].Trim();
            var firstName = nameParts[1].Trim();
            if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName))
                return string.Empty;

            return $"{firstName[0]}{lastName[0]}".ToUpper();
        }

        protected List<FiltersCriteria> Filters { get; set; } = new();

        private List<TasksTasksViewModel> filteredTasksModel { get; set; } = new();

        public string Value { get; set; }

        protected readonly List<string> AvailableColumns = new()
            { "Id", "Description", "PersonResponsible", "Initiator", "TaskStatusDisplay" };

        protected void AddNewFilter()
        {
            Filters.Add(new FiltersCriteria());
        }


        protected void RemoveFilter(FiltersCriteria filter)
        {
            Filters.Remove(filter);
        }

        protected void ClearAllFilters()
        {
            Filters.Clear();
            Tasks = mainTasksModel.AsQueryable().ToList();
        }

        protected async Task ApplyFilters()
        {
            Tasks = GetFilteredTasksAsync(Filters);
            StateHasChanged();
            await TaskGrid.Refresh();
        }

        protected async Task LoadColumnValues(FiltersCriteria filter)
        {
            if (string.IsNullOrEmpty(filter.Column) || !AvailableColumns.Contains(filter.Column))
            {
                filter.AvailableValues.Clear();
                return;
            }

            filter.AvailableValues.Clear();

            var columnValues = await FetchValuesFromDatabase(filter.Column);

            StateHasChanged();
        }

        protected async Task<List<string>> FetchValuesFromDatabase(string column)
        {
            Console.WriteLine($"Fetching values for column: {column}");

            if (!AvailableColumns.Contains(column))
            {
                return new List<string>();
            }

            var tasksFilter = await taskService.GetAllTasks();

            return column switch
            {
                "Id" => tasksFilter.Select(task => task.Id.ToString()).Distinct().ToList(),
                "Description" => tasksFilter.Select(task => task.Description)
                    .Where(description => !string.IsNullOrEmpty(description))
                    .Distinct()
                    .ToList(),
                "PersonResponsible" => tasksFilter.Select(task => task.PersonResponsible)
                    .Where(person => !string.IsNullOrEmpty(person))
                    .Distinct()
                    .ToList(),
                "Initiator" => tasksFilter.Select(task => task.Initiator)
                    .Where(initiator => !string.IsNullOrEmpty(initiator))
                    .Distinct()
                    .ToList(),
                "TaskStatusDisplay" => tasksFilter.Select(task => task.TaskStatusDisplay)
                    .Where(status => !string.IsNullOrEmpty(status))
                    .Distinct()
                    .ToList(),
                _ => new List<string>()
            };

        }

        protected async Task OnColumnChanged(Microsoft.AspNetCore.Components.ChangeEventArgs eventArgs,
            FiltersCriteria filter)
        {
            filter.Column = eventArgs.Value?.ToString() ?? string.Empty;

            if (!string.IsNullOrWhiteSpace(filter.Column))
            {
                filter.AvailableValues = await GetAvailableColumnValuesAsync(filter.Column);
            }

            await ApplyFilters();
        }

        protected async Task OnConditionChanged(Microsoft.AspNetCore.Components.ChangeEventArgs eventArgs,
            FiltersCriteria filter)
        {
            filter.Condition = eventArgs.Value?.ToString() ?? string.Empty;
            await ApplyFilters();
        }

        private async Task<List<string>> GetAvailableColumnValuesAsync(string column)
        {
            var tasks = mainTasksModel;
            var propertyInfo = typeof(TasksTasksViewModel).GetProperty(column);
            if (propertyInfo == null) return new List<string>();

            var values = tasks
                .Select(t => propertyInfo.GetValue(t)?.ToString())
                .Distinct()
                .Where(val => !string.IsNullOrEmpty(val))
                .ToList();

            return values;
        }

        private List<TasksTasksViewModel> GetFilteredTasksAsync(List<FiltersCriteria> filters)
        {
            var tasks = mainTasksModel.AsQueryable();

            foreach (var filter in filters)
            {
                if (!string.IsNullOrWhiteSpace(filter.Column) &&
                    !string.IsNullOrWhiteSpace(filter.Condition) &&
                    !string.IsNullOrWhiteSpace(filter.Value))
                {
                    tasks = ApplyFilter(tasks, filter);
                }
            }

            return tasks.OrderByDescending(x => x.Id).ToList();
        }

        private IQueryable<TasksTasksViewModel> ApplyFilter(IQueryable<TasksTasksViewModel> tasks,
            FiltersCriteria filter)
        {
            switch (filter.Condition)
            {
                case "Equals":
                    tasks = tasks.Where(t =>
                        GetPropertyValue(t, filter.Column) != null &&
                        GetPropertyValue(t, filter.Column).ToString() == filter.Value);
                    break;

                case "Not Equals":
                    tasks = tasks.Where(t =>
                        GetPropertyValue(t, filter.Column) != null &&
                        GetPropertyValue(t, filter.Column).ToString() != filter.Value);
                    break;

                case "Contains":
                    tasks = tasks.Where(t =>
                        GetPropertyValue(t, filter.Column) != null && GetPropertyValue(t, filter.Column).ToString()
                            .Contains(filter.Value, StringComparison.OrdinalIgnoreCase));
                    break;

                case "Does Not Contain":
                    tasks = tasks.Where(t =>
                        GetPropertyValue(t, filter.Column) != null && !GetPropertyValue(t, filter.Column).ToString()
                            .Contains(filter.Value, StringComparison.OrdinalIgnoreCase));
                    break;
            }

            return tasks;
        }

        private object GetPropertyValue(object objProperty, string propertyName)
        {
            if (objProperty == null || string.IsNullOrWhiteSpace(propertyName))
                return null;

            var propertyInfo = objProperty.GetType().GetProperty(propertyName);
            return propertyInfo?.GetValue(objProperty, null);
        }

        protected async Task OnValueChanged(Microsoft.AspNetCore.Components.ChangeEventArgs eventArgs,
            FiltersCriteria filter)
        {
            filter.Value = eventArgs.Value?.ToString() ?? string.Empty;
            await ApplyFilters();
        }

        protected void CreateFolder()
        {
            Folders.Add(new Helpers.Folders { Name = "New Folder", IsEditing = true });
        }

        public void EditFolder(Helpers.Folders folder)
        {
            folder.IsEditing = true;
        }

        public void RenameFolder(KeyboardEventArgs e, Helpers.Folders folder)
        {
            if (e.Key == "Enter")
            {
                folder.IsEditing = false;
            }
        }

        public async Task ShowEditor()
        {
            IsEditorVisible = true;
        }

        public void CloseEditor()
        {
            IsEditorVisible = false;
        }

        protected void SaveContentToDatabase()
        {
            CloseEditor();
        }

        protected void EnableEditing(int taskId)
        {
            isEditing = true;
            editingTaskId = taskId;
        }

        protected async Task SaveTaskSummary(TasksTasksViewModel taskViewModel)
        {
            if (taskViewModel != null)
            {
                var existingTask = mainTasksModel.FirstOrDefault(t => t.Id == taskViewModel.Id);
                if (existingTask != null)
                {
                    existingTask.Summary = taskViewModel.Summary;
                    bool isUpdated = await taskService.UpdateOneTask(existingTask);
                    await taskService.UpdateOneTask(existingTask);
                }

                isEditing = false;
                editingTaskId = 0;
            }
        }

        protected void UpdateTaskSummary(string value, TasksTasksViewModel task)
        {
            task.Summary = value;
        }

        protected async Task AddNewTaskType()
        {
            var newTask = new TasksTasksViewModel
            {
                Id = 0,
                Description = "Description",
                PersonResponsible = string.Empty,
                Initiator = string.Empty,
                TaskStatusId = 6,
                CostingCode = 000,
                Summary = "Summary",
                DateAdded = DateTime.UtcNow,
            };
            int savedTaskId = await taskService.AddTask(newTask);
            newTask.Id = savedTaskId;
            Tasks.Insert(0, newTask);
            await TaskGrid.Refresh();

        }

        protected void UpdateFilteredTasks()
        {
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                Tasks = mainTasksModel;
            }
            else
            {
                string lowerSearchText = SearchText.ToLower();
                Tasks = mainTasksModel.Where(task =>
                        (task.Id.ToString().Contains(lowerSearchText)) ||
                        (task.Description?.ToLower().Contains(lowerSearchText) ?? false) ||
                        (task.Update?.ToLower().Contains(lowerSearchText) ?? false) ||
                        (task.Initiator?.ToLower().Contains(lowerSearchText) ?? false) ||
                        (task.PersonResponsible?.ToLower().Contains(lowerSearchText) ?? false) ||
                        (task.TaskStatusDisplay?.ToLower().Contains(lowerSearchText) ?? false) ||
                        (task.Summary?.ToLower().Contains(lowerSearchText) ?? false) ||
                        (!string.IsNullOrEmpty(task.CostingCode?.ToString()) &&
                         ($"P{task.CostingCode?.ToString("D4")}".ToLower().Contains(lowerSearchText))))
                    .ToList();
            }
        }

        protected void OnSearchTextChanged(Microsoft.AspNetCore.Components.ChangeEventArgs search)
        {
            SearchText = search.Value.ToString();
            UpdateFilteredTasks();
        }

        protected void ClosePopup()
        {
            SelectedTasks.Clear();
            StateHasChanged();
        }

        protected async Task HandleFileSelected(InputFileChangeEventArgs e)
        {
            uploadedFile = e.File;
            if (uploadedFile != null)
            {
                Console.WriteLine($"File selected: {uploadedFile.Name}");
                Console.WriteLine($"File size: {uploadedFile.Size} bytes");
            }
        }

        protected async Task ImportTasks()
        {
            if (uploadedFile == null)
            {
                _toastService.ShowError("No File Found");
                return;
            }

            try
            {
                var tempFilePath = Path.GetTempFileName();
                using (var fileStream = new FileStream(tempFilePath, FileMode.Create))
                {
                    await uploadedFile.OpenReadStream(maxAllowedSize: 10 * 1024 * 1024).CopyToAsync(fileStream);
                }

                using (var stream = new FileStream(tempFilePath, FileMode.Open, FileAccess.Read))
                {
                    await ImportExcelFile(stream);
                }

                File.Delete(tempFilePath);
                await LoadData();
                _toastService.ShowInfo("Import successful!");

            }
            catch (Exception ex)
            {
                _toastService.ShowError($"Error importing tasks: {ex.Message}");
            }
        }

        protected async Task ImportExcelFile(Stream fileStream)
        {
            try
            {
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using var package = new ExcelPackage(fileStream);
                var workSheet = package.Workbook.Worksheets[0];
                var rows = workSheet.Dimension.Rows;
                var taskTypeValue = workSheet.Cells[1, 1].Text?.Trim();

                for (int row = 4; row <= rows; row++)
                {
                    var rawPersonResponsible = workSheet.Cells[row, 2].Text;
                    var rawInitiator = workSheet.Cells[row, 3].Text;

                    var formattedPersonResponsible = FormatString.FormatName(rawPersonResponsible);
                    var formattedInitiator = FormatString.FormatName(rawInitiator);
                    var rawStatus = workSheet.Cells[row, 5].Text;
                    var statusTypeId = _statusDisplay.GetStatusTypeId(rawStatus);
                    var rawCostingCode = workSheet.Cells[row, 7].Text?.Trim();
                    var numericCostingCode =
                        int.TryParse(rawCostingCode?.Replace("P", ""), out var code) ? code : (int?)null;


                    var task = new TasksTasksViewModel
                    {
                        TaskType = taskTypeValue,
                        Description = workSheet.Cells[row, 1].Text,
                        PersonResponsible = FormatString.FormatName(rawPersonResponsible),
                        Initiator = FormatString.FormatName(rawInitiator),
                        TaskStatusId = statusTypeId,
                        Summary = workSheet.Cells[row, 6].Text,
                        CostingCode = numericCostingCode,
                        DateAdded = Extractdate(workSheet.Cells[row, 11].Text),
                        LastUpdate = DateTime.TryParse(workSheet.Cells[row, 12].Text, out var lastUpdate)
                            ? lastUpdate
                            : DateTime.Now.Date,
                    };
                    await taskService.AddTask(task);
                }

                _toastService.ShowSuccess("Import Successful!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing Excel file: {ex.Message}");

            }
        }

        protected DateTime? Extractdate(string cellDateValue)
        {
            if (string.IsNullOrWhiteSpace(cellDateValue))
            {
                return null;
            }

            var datePartStart = cellDateValue.IndexOfAny("0123456789".ToCharArray());
            if (datePartStart >= 0)
            {
                var dateString = cellDateValue.Substring(datePartStart).Trim();
                if (DateTime.TryParse(dateString, out var parsedDate))
                {
                    return parsedDate;
                }
            }

            return null;
        }

        protected async Task ExportToExcel()
        {
            try
            {
                var tasks = mainTasksModel;
                using var memoryStream = new MemoryStream();
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using var package = new ExcelPackage(memoryStream);
                var workSheet = package.Workbook.Worksheets.Add("Tasks");

                workSheet.Cells[1, 1].Value = "Task Type";
                workSheet.Cells[1, 2].Value = "Description";
                workSheet.Cells[1, 3].Value = "Person Responsible";
                workSheet.Cells[1, 4].Value = "Manager";
                workSheet.Cells[1, 5].Value = "Status";
                workSheet.Cells[1, 6].Value = "Summary";
                workSheet.Cells[1, 7].Value = "Costing Code";
                workSheet.Cells[1, 8].Value = "Date Added";
                workSheet.Cells[1, 9].Value = "Last Update";

                using (var range = workSheet.Cells[1, 1, 1, 9])
                {
                    range.Style.Font.Bold = true;
                    range.Style.Font.Size = 14;
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    range.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                }

                int row = 2;
                foreach (var task in tasks)
                {
                    workSheet.Cells[row, 1].Value = task.TaskType;
                    workSheet.Cells[row, 2].Value = task.Description;
                    workSheet.Cells[row, 3].Value = task.PersonResponsible;
                    workSheet.Cells[row, 4].Value = task.Initiator;
                    workSheet.Cells[row, 5].Value = _statusDisplay.GetStatusName(task.TaskStatusId);
                    workSheet.Cells[row, 6].Value = task.Summary;
                    workSheet.Cells[row, 7].Value = task.CostingCode.HasValue ? $"P{task.CostingCode.Value:D4}" : "";
                    workSheet.Cells[row, 8].Value = task.DateAdded?.ToString("dd MMM, yyyy hh:mm tt");
                    workSheet.Cells[row, 9].Value = task.LastUpdate?.ToString("dd MMM, yyyy hh:mm tt");

                    row++;

                }

                workSheet.Cells[workSheet.Dimension.Address].AutoFitColumns();
                package.SaveAs(memoryStream);
                string timestamp = DateTime.Now.ToString("MM-dd-yyyy, hh:mm:ss").Replace(":", "-");
                string fileName = $"Tasks-{timestamp}.xlsx";
                memoryStream.Position = 0;
                await DownloadFile(fileName, memoryStream.ToArray());
            }

            catch (Exception ex)
            {
                _toastService.ShowError($"Error exporting tasks: {ex.Message}");
            }
        }

        protected async Task DownloadFile(string fileName, byte[] fileData)
        {
            var base64Data = Convert.ToBase64String(fileData);
            var fileContent =
                $"data:application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;base64,{base64Data}";

            await JSRuntime.InvokeVoidAsync("downloadFile", fileName, fileContent);
        }

        protected void OnValueChecked(int taskId, bool isChecked)
        {
            var selectedTaskIndex = mainTasksModel.FindIndex(s => s.Id == taskId);
            if (selectedTaskIndex != -1)
            {
                mainTasksModel[selectedTaskIndex].IsSelected = isChecked;
                if (isChecked)
                {
                    SelectedTask = mainTasksModel[selectedTaskIndex];
                    SelectedTasks.Add(mainTasksModel[selectedTaskIndex]);
                }
                else
                {
                    SelectedTasks.Remove(mainTasksModel[selectedTaskIndex]);
                    SelectedTask = null;
                }

                IsSelectedTaskModalVisible = isChecked;
                StateHasChanged();
            }
        }

        protected void OnTaskSelectionChanged(Microsoft.AspNetCore.Components.ChangeEventArgs e)
        {
            StateHasChanged();
        }

        protected void OnRowSelected(RowSelectEventArgs<TasksTasksViewModel> args)
        {
            if (args.Data != null && !SelectedTasks.Contains(args.Data))
            {
                SelectedTasks.Add(args.Data);
            }

            IsSelectedTaskModalVisible = SelectedTasks.Count > 0;
            StateHasChanged();
        }

        protected void OnRowDeselected(RowDeselectEventArgs<TasksTasksViewModel> args)
        {
            if (args.Data != null)
            {
                SelectedTasks.Remove(args.Data);
            }

            IsSelectedTaskModalVisible = SelectedTasks.Count > 0;
            StateHasChanged();
        }

        protected void CloseSelectedModal()
        {
            IsSelectedTaskModalVisible = false;
            SelectedTasks.Clear();
            SelectedTask = null;
            StateHasChanged();
        }
        protected void ShowImportPopup()
        {
            ImportFilePopup = true;
            StateHasChanged();
        }
        protected void CloseImportFilePopup()
        {
            ImportFilePopup = false;
            StateHasChanged();
        }
        protected async void OnRowDropped(RowDragEventArgs<TasksTasksViewModel> rowDrag)
        {
            var draggedRow = rowDrag.Data.FirstOrDefault();
            var droppedOnRow = rowDrag.Data.FirstOrDefault();
            if (draggedRow != null && droppedOnRow != null)
            {
                int oldIndex = Tasks.IndexOf(draggedRow);
                int newIndex = Tasks.IndexOf(droppedOnRow);
                if (oldIndex >= 0 && newIndex >= 0)
                {
                    Tasks.RemoveAt(oldIndex);
                    Tasks.Insert(newIndex, droppedOnRow);
                    await SaveTasksOrderAsync();

                }
            }
        }
        private async Task SaveTasksOrderAsync()
        {
            var jsonData = System.Text.Json.JsonSerializer.Serialize(Tasks);
            await JSRuntime.InvokeVoidAsync("localStorageHelper.setItem", "taskOrder", jsonData);
        }
        public void OnSelectedTaskTypeClicked(string taskType)
        {
            if (string.IsNullOrWhiteSpace(taskType))
            {
                SelectedTaskType = "ALL";
                SelectedTaskDetail = new TasksTasksTaskTypeViewModel();
            }
            else
            {
                SelectedTaskType = taskType;
                SelectedTaskDetail = TaskTypeModelList
                    .FirstOrDefault(t => t.TaskType.ToUpper() == taskType.ToUpper())
                    ?? new TasksTasksTaskTypeViewModel();
            }
            Console.WriteLine($"SelectedTaskDetail.Email: {SelectedTaskDetail.Email}");
            Console.WriteLine($"SelectedTaskDetail.CreatedAt: {SelectedTaskDetail.CreatedAt}");
            InvokeAsync(() => JSRuntime.InvokeVoidAsync("openTaskTypeModal"));
        }
        public void OnPersonClicked(TasksTasksViewModel args)
        {
            modalClass = "show";
            personModalDisplay = "block";
            personModalTitle = args.PersonResponsible;
            selectedTaskId = args.Id;
        }
        public async Task ShowPersonDialog()
        {
            Value = string.Empty;
            ResetPersonList();
            IsPersonDialogVisible = true;
        }
        protected void OnSearchInput(Microsoft.AspNetCore.Components.ChangeEventArgs args)
        {
            Value = args.Value?.ToString() ?? string.Empty;

            Person = employeeList
                     .Where(e => !string.IsNullOrEmpty(e.FullName)
                              && e.FullName.IndexOf(Value, StringComparison.OrdinalIgnoreCase) >= 0)
                     .Select(e => e.FullName)
                     .ToList();
        }
        protected void ResetPersonList()
        {
            Person = employeeList
                     .Where(e => !string.IsNullOrEmpty(e.FullName))
                     .Select(e => e.FullName)
                     .ToList();
        }
        protected void ClearPersonFilter()
        {
            filteredTasksModel = mainTasksModel.ToList();

            Value = string.Empty;

            ResetPersonList();
        }
        protected async void OnPersonSelected(string fullName)
        {
            IsPersonDialogVisible = false;
            await TaskGrid.Search(fullName);
        }
        protected async Task Login()
        {
            if (string.IsNullOrEmpty(SelectedEmployeeName))
                return;
            var employee = employeeList
            .FirstOrDefault(e => e.FullName == SelectedEmployeeName);

            if (employee == null)
            {
                _toastService.ShowError("Unknown employee.");
                return;
            }
            UserContextService.CurrentEmployeeId = (int)employee.Id;
            ShowPopup = false;
            await LoadData();
        }

        protected async Task OnLoginClick()
        {
            IsLoginDialogVisible = false;

            if (string.IsNullOrEmpty(SelectedEmployeeName))
            {
                _toastService.ShowError("Please select an employee.");
                return;
            }
            await Login();
        }
        protected int GetUserIdByName(string employeeName)
        {
            var employee = employeeList
                .FirstOrDefault(e => e.FullName == employeeName);

            return employee != null
                ? Convert.ToInt32(employee.Id)
                : 0;
        }
        protected void UpdateTaskDescription(string value, TasksTasksViewModel task)
        {
            task.Description = value;
        }

        protected async Task SaveTaskDescription(TasksTasksViewModel taskViewModel)
        {
            if (taskViewModel == null) return;

            var taskDescription = mainTasksModel.FirstOrDefault(t => t.Id == taskViewModel.Id);
            if (taskDescription != null)
            {
                taskDescription.Description = taskViewModel.Description;
                var success = await taskService.UpdateOneTask(taskDescription);
            }

            isEditing = false;
            editingTaskId = 0;
            StateHasChanged();
        }
        protected void BeginCostingEdit(int taskId, int? existingCode)
        {
            isCostingEdit = true;
            costingTaskId = taskId;
            editCostingValue = existingCode.HasValue && existingCode > 0
                ? $"P{existingCode.Value:D4}"
                : "";
        }
        protected void UpdateCostingValue(string value, TasksTasksViewModel task)
        {
            editCostingValue = value;
        }

        protected async Task SaveCostingCode(TasksTasksViewModel task)
        {
            var trimmedCositngCode = editCostingValue?.TrimStart('P') ?? "";
            int? parsedCode = int.TryParse(trimmedCositngCode, out var c) ? c : (int?)null;
            await taskService.UpdateTaskCostingCodeAsync(task.Id, parsedCode);
            isEditing = false;
            editingTaskId = 0;
            editCostingValue = null;

            await TaskGrid.Refresh();
        }
    }
}
