using Blazored.Toast.Services;
using CamcoTasks.Service.IService;
using CamcoTasks.ViewModels.PageLoadTimeDTO;
using CamcoTasks.ViewModels.TasksRecTasksDTO;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Syncfusion.Blazor.Grids;
using Syncfusion.Blazor.Navigations;

namespace CamcoTasks.Pages.RecurringTasks.ViewRecurringTasks
{
    public partial class ActiveRecTaskComponent
    {
        protected SfGrid<TasksRecTasksViewModel> DeactivatedGrid { get; set; }

        protected List<TasksRecTasksViewModel> DeactivatedTasks { get; set; } = new List<TasksRecTasksViewModel>();

        protected bool IsActiveCreateTask = false;
        protected bool IsActiveDeleteComponent { get; set; } = false;

        protected TasksRecTasksViewModel RecurringTask { get; set; } = new TasksRecTasksViewModel() { ParentTaskId = null };

        protected int DeactiveTaskseCount;

        [Inject]
        ILogger<RecTaskUpdateComponent> logger { get; set; }
        [Inject]
        protected ITasksService taskService { get; set; }
        [Inject]
        private IJSRuntime _jSRuntime { get; set; }
        [Inject]
        protected IPageLoadTimeService pageLoadTimeService { get; set; }
        [Inject]
        private IToastService _toastService { get; set; }
        [Inject]
        private IFileManagerService _fileManagerService { get; set; }
        [Inject]
        private NavigationManager _navigationManager { get; set; }
        [Parameter]
        public EventCallback<int> RefreshParentComponent { get; set; }
        [Parameter]
        public EventCallback CloseActiveRecurringTaskComponent { get; set; }


        protected PageLoadTimeViewModel PageLoadTime { get; set; } = new PageLoadTimeViewModel()
        {
            PageName = "RecurringTask",
            SectionName = "ActiveRecTaskComponent",
            DateCreated = DateTime.Now
        };


        protected override async Task OnInitializedAsync()
        {
            await Task.Run(() => PageLoadTime.StartTime = DateTime.Now);

            await LoadDeActiveRecTaskData();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
               
                await LoadActiveRecTaskData();

                StateHasChanged();

                await PageLoadTimeCalculation();
            }
        }

        protected async Task PageLoadTimeCalculation()
        {
            PageLoadTime.EndTime = DateTime.Now;
            await pageLoadTimeService.InsertAsync(PageLoadTime);
        }

        protected async Task LoadActiveRecTaskData()
        {
            await _jSRuntime.InvokeAsync<object>("ShowModal", "#ActivateTaskModal");
            await LoadDeactiveTask();
        }

        protected async Task LoadDeActiveRecTaskData()
        {
            DeactivatedTasks = (await taskService.GetRecurringTasks(x => !x.IsDeleted && x.IsDeactivated)).ToList();
            
        }

        public async Task LoadDeactiveTask()
        {
                await DeactivatedGrid.RefreshColumnsAsync();
                await DeactivatedGrid.Refresh();
                DeactiveTaskseCount = DeactivatedTasks.Count();
        }

        protected async Task ActivateTask(TasksRecTasksViewModel tasksRecTasks)
        {
            try
            {
                StartRecTask(tasksRecTasks);

                tasksRecTasks.IsDeactivated = false;
                await taskService.UpdateRecurringTask(tasksRecTasks);

                string emailSubject = "RECURRING TASK REACTIVE";

                bool isSend = await taskService.SendEmail(tasksRecTasks, true, _navigationManager.BaseUri, emailSubject);

                if (isSend)
                {
                    _toastService.ShowInfo("An Email Has Been Added To Queue");
                }
                else
                {
                    _toastService.ShowError("Email Sending Error, Employee Has No Email");
                }

                await LoadDeactiveTask();

                await RefreshParentComponent.InvokeAsync(tasksRecTasks.Id);
            }
            catch (Exception ex)
            {
                if (ex != null)
                {
                    logger.LogError(ex, "Task Activate Error", ex);
                }
                else
                {
                    logger.LogWarning(ex, "Task Activate Error", ex);
                }
            }
        }

        protected void StartRecTask(TasksRecTasksViewModel RecTask)
        {
            IsActiveCreateTask = true;
            RecurringTask = RecTask;
        }

        protected void DeactiveCreateRecurringTasksComponent()
        {
            IsActiveCreateTask = false;
        }

        protected void ActiveRecTaskDeleteComponent(TasksRecTasksViewModel RecTask)
        {
            IsActiveDeleteComponent = true;
            RecurringTask = RecTask;
            DeactivatedTasks.Remove(RecTask);
            StateHasChanged();
        }

        protected void DeactiveRecTaskDeleteComponent()
        {
            IsActiveDeleteComponent = false;
        }

        protected void MessageFromCreateRecurringTaskComponent(string message)
        {
            _toastService.ShowSuccess(message);
        }

        protected async Task StartPrinting(ClickEventArgs args)
        {
            PageLoadTime.StartTime = DateTime.Now;
            PageLoadTime.SectionName = "ExportReport";

            if (args.Item.Text == "PRINT REPORT")
            {
                _toastService.ShowSuccess("Generating Report Started, Please Wait.");
                var pdf = _fileManagerService.CreateDeactivePdfInMemory(DeactivatedTasks);
                await _jSRuntime.InvokeVoidAsync("jsSaveAsFile", "DeactiveRecurringTasks.pdf", Convert.ToBase64String(pdf));
            }
            else if (args.Item.Text == "EXCEL EXPORT")
            {
                _toastService.ShowSuccess("Generating Report Started, Please Wait.");
                ExcelExportProperties exportProperties = new ExcelExportProperties();
                exportProperties.IncludeTemplateColumn = true;
                exportProperties.FileName = "DeactiveRecurringTasks.xlsx";
                await DeactivatedGrid.ExportToExcelAsync(exportProperties);
            }

            await PageLoadTimeCalculation();
        }

        protected async Task CloseActiveRecTaskComponent()
        {
            await _jSRuntime.InvokeAsync<object>("HideModal", "#ActivateTaskModal");
            await CloseActiveRecurringTaskComponent.InvokeAsync();
        }
    }
}
