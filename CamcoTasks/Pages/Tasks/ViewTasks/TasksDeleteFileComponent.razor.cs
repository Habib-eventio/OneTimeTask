using CamcoTasks.Data.Services;
using CamcoTasks.Service.IService;
using CamcoTasks.ViewModels.TasksImagesDTO;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace CamcoTasks.Pages.Tasks.ViewTasks
{
    public partial class TasksDeleteFileComponent
    {
        [Inject]
        private FileManagerService fileManagerService { get; set; }
        [Inject]
        protected ITasksService taskService { get; set; }
        [Inject]
        private IJSRuntime jSRuntime { get; set; }

        [Parameter]
        public TasksImagesViewModel? SelectImageModel { get; set; }
        [Parameter]
        public string? FilePath { get; set; }
        [Parameter]
        public EventCallback<TasksImagesViewModel> DeleteSuccessModal { get; set; }
        [Parameter]
        public EventCallback<string> DeleteSuccessPath { get; set; }

        protected bool IsActiveModel { get; set; }


        protected override async Task OnParametersSetAsync()
        {
            await CheckParameter();
        }

        protected override async void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                await LoadDeleteFileData();
            }
        }

        protected async Task CheckParameter()
        {
            if (SelectImageModel == null && !string.IsNullOrEmpty(FilePath))
            {
                SelectImageModel = new TasksImagesViewModel();
                SelectImageModel.PictureLink = FilePath;
                await Task.Run(() => IsActiveModel = false);
            }
            else
            {
                await Task.Run(() => IsActiveModel = true);
            }
        }

        protected async Task LoadDeleteFileData()
        {
            await jSRuntime.InvokeAsync<object>("ShowModal", "#DeleteFilesModal");
        }

        protected async Task DeleteTaskFile(TasksImagesViewModel SelectImageModel)
        {
            if (IsActiveModel)
            {
                await taskService.DeleteTaskImageAsync(SelectImageModel);
                await DeleteSuccessModal.InvokeAsync(SelectImageModel);
            }
            else
            {
                await DeleteSuccessPath.InvokeAsync(SelectImageModel.PictureLink);
            }

            await jSRuntime.InvokeAsync<object>("HideModal", "#DeleteFilesModal");

        }

        protected async Task CloseComponent()
        {
            await jSRuntime.InvokeAsync<object>("HideModal", "#DeleteFilesModal");
            await DeleteSuccessModal.InvokeAsync(null);
        }
    }
}
