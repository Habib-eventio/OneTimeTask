using Blazored.Toast.Services;
using CamcoTasks.Data.Services;
using CamcoTasks.Service.IService;
using CamcoTasks.ViewModels.PageLoadTimeDTO;
using CamcoTasks.ViewModels.TasksImagesDTO;
using CamcoTasks.ViewModels.TasksRecTasksDTO;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace CamcoTasks.Pages.RecurringTasks.ViewRecurringTasks
{
    public partial class MultipleFileComponent
    {
        protected bool isActiveSingleFileComponent;

        [Inject]
        private FileManagerService fileManagerService { get; set; }
        [Inject]
        private IJSRuntime jSRuntime { get; set; }
        [Inject]
        protected ITasksService taskService { get; set; }
        [Inject]
        protected IPageLoadTimeService pageLoadTimeService { get; set; }
        [Inject]
        private IToastService _ToastService { get; set; }

        [Parameter]
        public TasksRecTasksViewModel TaskViewModel { get; set; }
        [Parameter]
        public EventCallback CloseMultipleFileComponent { get; set; }
        [Parameter]
        public EventCallback<int> ReturnRecurringTaskId { get; set; }

        protected List<TasksImagesViewModel> TaskFiles { get; set; } = new List<TasksImagesViewModel>();

        protected bool IsRenderingImages = true;

        protected string TaskImage { get; set; } = null;
        protected string TaskFile { get; set; } = null;
        protected string ErrorMessage { get; set; }

        protected PageLoadTimeViewModel PageLoadTime { get; set; } = new PageLoadTimeViewModel()
        {
            PageName = "RecurringTask",
            SectionName = "MultipleFileComponent",
            DateCreated = DateTime.Now
        };


        protected override async Task OnInitializedAsync()
        {
            await Task.Run(() => PageLoadTime.StartTime = DateTime.Now);
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await LoadMultipleFileData();
                await Task.Run(() => IsRenderingImages = false);

                StateHasChanged();

                await PageLoadTimeCalculation();
            }
        }

        protected async Task PageLoadTimeCalculation()
        {
            PageLoadTime.EndTime = DateTime.Now;
            await pageLoadTimeService.InsertAsync(PageLoadTime);
        }

        protected async Task LoadMultipleFileData()
        {
            await jSRuntime.InvokeAsync<object>("ShowModal", "#MultFilesModal");

            if (TaskViewModel != null)
                TaskFiles = (await taskService.GetRecurringTaskImagesAsync(TaskViewModel.Id)).ToList();
        }

        protected async Task StartModifyingImageNote(TasksImagesViewModel model)
        {
            if (model != null)
                await taskService.UpdateTaskImageAsync(model);
        }

        protected void ViewFiles(TasksRecTasksViewModel Model)
        {
            if (Model != null)
            {
                isActiveSingleFileComponent = true;

                if (Model.InstructionFileLink != null)
                {
                    if (fileManagerService.IsImage(Model.InstructionFileLink))
                        TaskFile = fileManagerService.ConvertImagetoBase64(Model.InstructionFileLink);
                    else
                        TaskFile = Model.InstructionFileLink;
                }
                else
                {
                    TaskFile = null;
                }
            }
        }

        protected async Task SuccessMessageFromSingleFile(bool IsSuccess)
        {
            if (IsSuccess)
                await Task.Run(() => isActiveSingleFileComponent = false);
        }

        protected async Task CloseComponent()
        {
            await jSRuntime.InvokeAsync<object>("HideModal", "#MultFilesModal");
            await CloseMultipleFileComponent.InvokeAsync();
        }

        protected async Task SuccessMessageFromDeleteFileComponent(TasksImagesViewModel model)
        {
            if (model != null && TaskFiles.Any())
                TaskFiles.Remove(model);

            if (!TaskFiles.Any())
            {
                TaskViewModel.IsPicture = false;
                await taskService.UpdateRecurringTask(TaskViewModel);
                await ReturnRecurringTaskId.InvokeAsync(model.RecurringId);
            }
        }
    }
}
