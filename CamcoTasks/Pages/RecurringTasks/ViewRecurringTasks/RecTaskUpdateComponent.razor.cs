using Blazored.Toast.Services;
using CamcoTasks.Data.Services;
using CamcoTasks.Service.IService;
using CamcoTasks.ViewModels.PageLoadTimeDTO;
using CamcoTasks.ViewModels.TasksImagesDTO;
using CamcoTasks.ViewModels.TasksRecTasksDTO;
using CamcoTasks.ViewModels.TasksTaskUpdatesDTO;
using CamcoTasks.ViewModels.UpdateNotesDTO;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Syncfusion.Blazor.Charts;
using Syncfusion.Blazor.Grids;

namespace CamcoTasks.Pages.RecurringTasks.ViewRecurringTasks
{
    public partial class RecTaskUpdateComponent
    {
        protected TasksTaskUpdatesViewModel updateTaskFormultipleFile;

        protected TasksRecTasksViewModel SelectedUpdateTaskViewModel { get; set; } = new();

        protected TasksTaskUpdatesViewModel SelectedUpdateViewModel { get; set; } = new TasksTaskUpdatesViewModel();
        protected TasksRecTasksViewModel TaskForCreate { get; set; }
        protected TasksTaskUpdatesViewModel UpdateForCreate { get; set; }
        protected TasksTaskUpdatesViewModel DeleteUpdateViewModel { get; set; } = new TasksTaskUpdatesViewModel()
        { Update = string.Empty, UpdateDate = DateTime.Today, IsAudit = false, IsPass = false };
        protected TasksTaskUpdatesViewModel NoteUpdateEditDelete { get; set; } = new TasksTaskUpdatesViewModel() { Update = string.Empty };

        protected UpdateNotesViewModel NoteEditDelete { get; set; } = new UpdateNotesViewModel();
        protected UpdateNotesViewModel updateNotesViewModel = new();

        protected PageLoadTimeViewModel PageLoadTime { get; set; } = new PageLoadTimeViewModel()
        {
            PageName = "RecurringTask",
            SectionName = "TaskUpdateComponent",
            DateCreated = DateTime.Now
        };

        protected bool IsLoadTask { get; set; } = true;
        protected bool IsTaskGraphRequired { get; set; }
        protected bool IsQuestionRequired { get; set; }
        protected bool IsDoingTask { get; set; } = false;
        protected bool IsDonePass { get; set; } = true;
        protected bool IsUploading = false;
        protected bool IsActiveRecTaskUpdateComponent { get; set; } = false;
        protected bool IsActiveCreateReUpdateComponent { get; set; } = false;
        protected bool IsActiveViewEmailComponent { get; set; } = false;
        protected bool IsActiveRecUpdateEmailComponent { get; set; } = false;
        protected bool IsActiveDeleteRecUpdateComponent { get; set; } = false;
        protected bool IsActiveViewGraphComponent { get; set; } = false;
        protected bool IsActiveAddNoteComponent { get; set; } = false;
        protected bool IsActiveTaskUpdateMultipleFileComponent { get; set; } = false;
        protected bool IsActiveJobTitlesFilter { get; set; } = false;

        protected int CompletedSingleTaskCount1 { get; set; } = 0;
        protected int CompletedSingleTaskCount3 { get; set; } = 0;
        protected int CompletedSingelTaskCount6 { get; set; } = 0;
        protected int CompletedSingleTaskCount12 { get; set; } = 0;
        protected int TaskId { get; set; }

        protected Dictionary<string, object> htmlAttributeBig = new Dictionary<string, object>() { { "rows", "7" }, { "spellcheck", "true" } };
        protected List<TasksRecTasksViewModel> Tasks { get; set; } = new List<TasksRecTasksViewModel>();
        protected List<string> TasksCompleted { get; set; } = new List<string>();
        protected List<TasksTaskUpdatesViewModel> TaskUpdateList { get; set; }
        protected List<TasksImagesViewModel> SelectedTaskFiles { get; set; } = new List<TasksImagesViewModel>();
        protected List<string> ActiveJobTitles { get; set; } = new ();
        protected List<string> ActiveFilterJobTitles { get; set; } = new ();

        private string ChartImage = string.Empty;
        private string Graph { get; set; }

        protected SfGrid<TasksTaskUpdatesViewModel> RecUpdateGrid { get; set; }

        protected SfChart ChartTempObj { get; set; }

        [Inject]
        private IJSRuntime jSRuntime { get; set; }
        [Inject]
        protected ITasksService taskService { get; set; }
        [Inject]
        protected IEmployeeService employeeService { get; set; }
        [Inject]
        protected IJobDescriptionsService jobDescriptionService { get; set; }
        [Inject]
        protected IUpdateNotesService notesService { get; set; }
        [Inject]
        private FileManagerService fileManagerService { get; set; }
        [Inject]
        private ILogger<RecTaskUpdateComponent> logger { get; set; }
        [Inject]
        private NavigationManager navigationManager { get; set; }
        [Inject]
        private IToastService _toastService { get; set; }
        [Inject]
        protected IPageLoadTimeService pageLoadTimeService { get; set; }

        [Parameter]
        public TasksRecTasksViewModel TaskForUpdate { get; set; }
        [Parameter]
        public string EditBy { get; set; }
        [Parameter]
        public EventCallback CloseRecTaskUpdateComponent { get; set; }
        [Parameter]
        public EventCallback<string> SuccessMessagRecUpdateComponent { get; set; }
        [Parameter]
        public EventCallback<int> RefreshToParentRecTaskUpdateComponent { get; set; }
        [CascadingParameter]
        public ViewRecurringTasks ViewRecurringTasksComponentRef { get; set; }


        protected override async Task OnInitializedAsync()
        {
            await Task.Run(() => PageLoadTime.StartTime = DateTime.Now);
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await LoadRecTaskUpdateData();
                await Task.Run(() => IsLoadTask = false);

                StateHasChanged();

                await PageLoadTimeCalculation();
            }
        }

        protected async Task PageLoadTimeCalculation()
        {
            PageLoadTime.EndTime = DateTime.Now;
            await pageLoadTimeService.InsertAsync(PageLoadTime);
        }

        protected async Task LoadRecTaskUpdateData()
        {
            await jSRuntime.InvokeAsync<object>("ShowModal", "#TaskUpdatesModal");
            await LoadRecUpdateData();
        }

        protected async Task LoadRecUpdateData()
        {
            if (TaskForUpdate != null)
            {
                TaskForUpdate = taskService.GetRecurringTaskByIdSync(TaskForUpdate.Id);

                IsTaskGraphRequired = TaskForUpdate.IsGraphRequired;
                IsQuestionRequired = TaskForUpdate.IsQuestionRequired.HasValue ? (bool)TaskForUpdate.IsQuestionRequired : false;

                var updatesModel = await taskService.GetTaskUpdates(TaskForUpdate.Id, false);

                await CalculationSingleTaskPercentage(updatesModel.ToList());

                TaskUpdateList = updatesModel.OrderByDescending(x => x.UpdateDate.Date).ToList();
                var selectedJobIds = TaskForUpdate.EmailsListJobId?.Split(';').Select(id => long.Parse(id)).ToList();
                if(selectedJobIds!=null && selectedJobIds.Any())
                {
                    ActiveJobTitles = (await jobDescriptionService.GetListAsync(false))
                    .Where(job => selectedJobIds.Contains(job.Id))
                    .Select(job => job.Name).ToList();
                    ActiveFilterJobTitles = ActiveJobTitles;
                }
                SelectedTaskFiles = (await taskService.GetRecurringTaskImagesAsync(TaskForUpdate.Id)).ToList();

                TaskForUpdate.GraphTitle = string.IsNullOrEmpty(TaskForUpdate.GraphTitle) ? "" : TaskForUpdate.GraphTitle;
                TaskForUpdate.VerticalAxisTitle = string.IsNullOrEmpty(TaskForUpdate.VerticalAxisTitle) ? "" : TaskForUpdate.VerticalAxisTitle;
            }
        }

        protected async Task CalculationSingleTaskPercentage(List<TasksTaskUpdatesViewModel> updatesModel)
        {
            try
            {
                var freq = await taskService.GetFrequency(TaskForUpdate.TasksFreq.Frequency);
                var updates = updatesModel.Where(x => x.UpdateDate <= DateTime.Now.Date && x.UpdateDate >= DateTime.Now.AddMonths(-1).Date
                && !x.Update.Contains("system generated update")
                && !x.Update.Contains("Rich Arnold ''NUDGED'' you to get the recurring Task")
                && !x.Update.Contains("Rich Arnold ''Email'' you to get the recurring Task"));

                var percentageTable = taskService.RecTaskUpdatePercentageCalculation(
                        TaskForUpdate, updates, freq);
                int lastRow;
                if (percentageTable != null && percentageTable.Rows.Count > 0)
                {
                    lastRow = percentageTable.Rows.Count;
                    CompletedSingleTaskCount1 = Convert.ToInt32(percentageTable.Rows[lastRow - 1]["Overall On Time Percentage"]);
                }
                else
                {
                    CompletedSingleTaskCount1 = 0;
                }

                updates = updatesModel.Where(x => x.UpdateDate <= DateTime.Now.Date && x.UpdateDate >= DateTime.Now.AddMonths(-3).Date
                && !x.Update.Contains("system generated update")
                && !x.Update.Contains("Rich Arnold ''NUDGED'' you to get the recurring Task")
                && !x.Update.Contains("Rich Arnold ''Email'' you to get the recurring Task"));
                percentageTable = taskService.RecTaskUpdatePercentageCalculation(
                        TaskForUpdate, updates, freq);
                if (percentageTable != null && percentageTable.Rows.Count > 0)
                {
                    lastRow = percentageTable.Rows.Count;
                    CompletedSingleTaskCount3 = Convert.ToInt32(percentageTable.Rows[lastRow - 1]["Overall On Time Percentage"]);
                }
                else
                {
                    CompletedSingleTaskCount3 = 0;
                }

                updates = updatesModel.Where(x => x.UpdateDate <= DateTime.Now.Date && x.UpdateDate >= DateTime.Now.AddMonths(-6).Date);
                percentageTable = taskService.RecTaskUpdatePercentageCalculation(
                        TaskForUpdate, updates, freq);
                if (percentageTable != null && percentageTable.Rows.Count > 0)
                {
                    lastRow = percentageTable.Rows.Count;
                    CompletedSingelTaskCount6 = Convert.ToInt32(percentageTable.Rows[lastRow - 1]["Overall On Time Percentage"]);
                }
                else
                {
                    CompletedSingelTaskCount6 = 0;
                }

                updates = updatesModel.Where(x => x.UpdateDate <= DateTime.Now.Date && x.UpdateDate >= DateTime.Now.AddMonths(-12).Date
                && !x.Update.Contains("system generated update")
                && !x.Update.Contains("Rich Arnold ''NUDGED'' you to get the recurring Task")
                && !x.Update.Contains("Rich Arnold ''Email'' you to get the recurring Task"));
                percentageTable = taskService.RecTaskUpdatePercentageCalculation(
                        TaskForUpdate, updates, freq);
                if (percentageTable != null && percentageTable.Rows.Count > 0)
                {
                    lastRow = percentageTable.Rows.Count;
                    CompletedSingleTaskCount12 = Convert.ToInt32(percentageTable.Rows[lastRow - 1]["Overall On Time Percentage"]);
                }
                else
                {
                    CompletedSingleTaskCount12 = 0;
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "rec task update", ex);
            }
        }

        public async Task DeleteSuccessFromDeleteFileComponent(TasksImagesViewModel model)
        {
            if (model != null)
            {
                SelectedTaskFiles.Remove(model);

                if (!SelectedTaskFiles.Any())
                {
                    if (TaskForUpdate != null)
                    {
                        TaskForUpdate.IsPicture = false;
                        await taskService.UpdateRecurringTask(TaskForUpdate);
                        await RefreshToParentRecTaskUpdateComponent.InvokeAsync(TaskForUpdate.Id);
                    }
                }
            }
        }

        protected async Task ImageNoteComponentCallback(TasksImagesViewModel model)
        {
            if (model != null)
            {
                _toastService.ShowSuccess("Successfully Saved");
                await taskService.UpdateTaskImageAsync(model);
            }
        }

        protected async Task PrintingImage(string FilePath)
        {
            await jSRuntime.InvokeAsync<object>("PrintImage", FilePath);
        }

        protected void StartEditTask()
        {
            IsActiveRecTaskUpdateComponent = true;
        }

        protected void StartTaskUpdate(TasksRecTasksViewModel taskViewModel, TasksTaskUpdatesViewModel taskUpdate)
        {
            IsActiveCreateReUpdateComponent = true;
            TaskForCreate = taskViewModel;
            UpdateForCreate = taskUpdate;
        }

        protected void DeleteUpdateConfirm(TasksRecTasksViewModel recTask, TasksTaskUpdatesViewModel tasksTaskUpdates)
        {
            IsActiveDeleteRecUpdateComponent = true;
            SelectedUpdateTaskViewModel = recTask;
            SelectedUpdateViewModel = tasksTaskUpdates;
        }

        protected void RefreshNoteModal(TasksTaskUpdatesViewModel Update)
        {
            IsActiveAddNoteComponent = true;
        }

        protected void StartRespond(TasksTaskUpdatesViewModel task)
        {
            IsActiveRecUpdateEmailComponent = true;
            SelectedUpdateViewModel = task;
        }

        protected void GetUpdateEmail(int? EmailId)
        {
            IsActiveViewEmailComponent = true;
            TaskId = EmailId.Value;
        }

        public static bool IsBase64String(string base64)
        {
            Span<byte> buffer = new Span<byte>(new byte[base64.Length]);
            return Convert.TryFromBase64String(base64, buffer, out int bytesParsed);
        }

        protected void ViewUpdateImage(TasksTaskUpdatesViewModel model)
        {
            IsActiveTaskUpdateMultipleFileComponent = true;
            updateTaskFormultipleFile = model;
        }

        protected void ViewTaskGraph()
        {
            IsActiveViewGraphComponent = true;
            SelectedUpdateTaskViewModel = TaskForUpdate;
        }

        public async Task CloseComponent()
        {
            await jSRuntime.InvokeAsync<object>("HideModal", "#TaskUpdatesModal");
            await CloseRecTaskUpdateComponent.InvokeAsync();
        }

        public async Task RefreshRecTaskUpdateComponent(int updateId)
        {
            if (updateId > 0)
            {
                var newUpdate = await taskService.GetTaskUpdatesByIdAsync(updateId);
                var oldUpdate = TaskUpdateList.FirstOrDefault(x => x.UpdateId == updateId);

                if (newUpdate != null && newUpdate.IsDeleted == true && TaskUpdateList.Any())
                {
                    TaskUpdateList.Remove(oldUpdate);
                }
                else
                {
                    if (oldUpdate != null && TaskUpdateList.Any())
                    {
                        int updateIndex = TaskUpdateList.IndexOf(oldUpdate);

                        if (updateIndex > -1)
                        {
                            TaskUpdateList[updateIndex] = newUpdate;
                        }
                    }
                    else
                    {
                        TaskUpdateList.Add(newUpdate);
                    }
                }

                TaskUpdateList = TaskUpdateList.OrderByDescending(u => u.UpdateDate).ToList();

                if (RecUpdateGrid != null)
                {
                    await RecUpdateGrid.Refresh();
                }

                if(TaskForUpdate != null)
                    await RefreshToParentRecTaskUpdateComponent.InvokeAsync(TaskForUpdate.Id);
            }
        }

        public void SuccessMessageRecUpdateComponent(string message)
        {
            _toastService.ShowSuccess(message);
        }

        public async Task DeactivateEditRecTaskComponent()
        {
            await LoadRecUpdateData();
            await Task.Run(() => IsActiveRecTaskUpdateComponent = false);
        }

        public async Task DeactivateCreateRecUpdateComponent()
        {
            await Task.Run(() => IsActiveCreateReUpdateComponent = false);
        }

        protected async Task SuccessMessageFromViewEmailComponent(bool isSuccess)
        {
            if (isSuccess)
                await Task.Run(() => IsActiveViewEmailComponent = false);
        }

        public async Task DeactivateRecUpdateEmailComponent()
        {
            await Task.Run(() => IsActiveRecUpdateEmailComponent = false);
        }

        public async Task DeactivateDeleteRecUpdateComponent()
        {
            await Task.Run(() => IsActiveDeleteRecUpdateComponent = false);
        }

        protected async Task SuccessMessageFromViewGraphComponent(bool isSuccess)
        {
            if (isSuccess)
                await Task.Run(() => IsActiveViewGraphComponent = false);
        }

        public async Task DeactivateAddNoteComponent()
        {
            await Task.Run(() => IsActiveAddNoteComponent = false);
        }

        protected async Task DeactivateFromMultipleFile()
        {
            await Task.Run(() => IsActiveTaskUpdateMultipleFileComponent = false);
        }

        protected async Task ActiveJobTitlesFilter(ChangeEventArgs args)
        {
            await Task.Run(() => IsActiveJobTitlesFilter = true);
            string key = Convert.ToString(args.Value).ToUpper();

            if (string.IsNullOrEmpty(key))
            {
                ActiveFilterJobTitles = ActiveJobTitles;
            }
            else
            {
                ActiveFilterJobTitles = ActiveJobTitles.Where(x => x.Contains(key)).ToList();
            }

            await Task.Run(() => IsActiveJobTitlesFilter = false);
        }
    }
}
