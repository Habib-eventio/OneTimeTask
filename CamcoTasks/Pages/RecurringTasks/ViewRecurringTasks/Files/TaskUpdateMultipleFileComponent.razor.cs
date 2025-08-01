using Blazored.Toast.Services;
using CamcoTasks.Data.Services;
using CamcoTasks.Service.IService;
using CamcoTasks.ViewModels.PageLoadTimeDTO;
using CamcoTasks.ViewModels.TasksImagesDTO;
using CamcoTasks.ViewModels.TasksTaskUpdatesDTO;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace CamcoTasks.Pages.RecurringTasks.ViewRecurringTasks.Files
{
    public partial class TaskUpdateMultipleFileComponent
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
        public TasksTaskUpdatesViewModel TaskUpdateViewModel { get; set; }
        [Parameter]
        public EventCallback CloseTaskUpdateMultipleFileComponent { get; set; }

        protected List<TasksImagesViewModel> TaskFiles { get; set; } = new List<TasksImagesViewModel>();

        protected bool IsRenderingImages = true;

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
            await jSRuntime.InvokeAsync<object>("ShowModal", "#TaskUpdateMultFilesModal");

            if (TaskUpdateViewModel != null)
                TaskFiles = (await taskService.GetUpdatesImagesAsync(TaskUpdateViewModel.UpdateId)).ToList();
        }

        protected async Task PrintingImage(string FilePath)
        {
            await jSRuntime.InvokeAsync<object>("PrintImage", FilePath);
        }

        protected void ViewFiles(TasksTaskUpdatesViewModel Model)
        {
            if (Model != null)
            {
                isActiveSingleFileComponent = true;

                if (Model.PictureLink != null)
                {
                    if (fileManagerService.IsImage(Model.PictureLink))
                        TaskFile = fileManagerService.ConvertImagetoBase64(Model.PictureLink);
                    else
                        TaskFile = Model.PictureLink;
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
            await jSRuntime.InvokeAsync<object>("HideModal", "#TaskUpdateMultFilesModal");
            await CloseTaskUpdateMultipleFileComponent.InvokeAsync();
        }

        protected async Task SuccessMessageFromImageNoteComponent(TasksImagesViewModel model)
        {
            if (model != null)
            {
                await taskService.UpdateTaskImageAsync(model);
            }
        }

        protected async Task SuccessMessageFromDeleteFileComponent(TasksImagesViewModel model)
        {
            if (model != null && TaskFiles.Any())
                TaskFiles.Remove(model);

            if (!TaskFiles.Any())
            {
                TaskUpdateViewModel.IsPicture = false;
                await taskService.UpdateTaskUpdate(TaskUpdateViewModel);
            }
        }
    }
}
