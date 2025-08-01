using BlazorDownloadFile;
using CamcoTasks.Data.Services;
using CamcoTasks.Service.IService;
using CamcoTasks.ViewModels.TasksImagesDTO;
using CamcoTasks.ViewModels.TasksTasksDTO;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Net;

namespace CamcoTasks.Pages.Tasks.ViewTasks
{
    public partial class TasksMultipleFilesComponent
    {
        [Inject]
        private FileManagerService fileManagerService { get; set; }
        [Inject]
        private IJSRuntime jSRuntime { get; set; }
        [Inject]
        private IBlazorDownloadFileService blazorDownloadFileService { get; set; }
        [Inject]
        protected ITasksService taskService { get; set; }

        [Parameter]
        public TasksTasksViewModel TaskViewModel { get; set; }
        [Parameter]
        public EventCallback<bool> CallbackMessageTasksMultipleFiles { get; set; }

        protected TasksImagesViewModel SelectedTMV { get; set; } = new TasksImagesViewModel();
        protected List<TasksImagesViewModel> TaskFiles { get; set; } = new List<TasksImagesViewModel>();
        protected TasksImagesViewModel SelectedImageNote { get; set; } = new TasksImagesViewModel();

        protected bool IsRenderingImages = true;
        protected bool isActiveImageViewComponent { get; set; } = false;
        protected bool isActiveImageNoteComponent { get; set; } = false;
        protected bool isActiveDeleteFileComponent { get; set; } = false;

        protected string TaskImage { get; set; } = null;
        protected string TaskFile { get; set; } = null;
        protected string ErrorMessage { get; set; }


        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await LoadMultipleFileData();
                await Task.Run(() => IsRenderingImages = false);

                StateHasChanged();
            }
        }

        protected async Task LoadMultipleFileData()
        {
            await jSRuntime.InvokeAsync<object>("ShowModal", "#MultFilesModal");

            if (TaskViewModel != null)
                TaskFiles = (await taskService.GetOneTimeTaskImagesCountAsync(TaskViewModel.Id)).ToList();
        }

        protected string ConvertImagetoBase64(string path)
        {
            if (string.IsNullOrEmpty(path))
                return string.Empty;
            try
            {
                using (var webClient = new WebClient())
                {
                    byte[] imageBytes = webClient.DownloadData(path);
                    return $"data:image/{Path.GetExtension(path).Replace(".", "").ToLower()};base64, " + Convert.ToBase64String(imageBytes);
                }
            }
            catch
            {
                return string.Empty;
            }
        }

        protected async Task PrintingImage(string FilePath)
        {
            await jSRuntime.InvokeAsync<object>("PrintImage", FilePath);
        }

        protected void StartModifyingImageNote(TasksImagesViewModel model)
        {
            isActiveImageNoteComponent = true;
            SelectedImageNote = model;
        }

        protected void StartDeleteTaskFile(TasksImagesViewModel TMV)
        {
            isActiveDeleteFileComponent = true;
            TaskFile = null;
            SelectedTMV = TMV;

        }

        protected async Task StartDownloadFile(string filePath)
        {
            var OriginalPath = filePath;
            if (!File.Exists(OriginalPath))
            {
                ErrorMessage = "Fie Doesn't Exist, Deleted or have been moved";
                return;
            }
            await blazorDownloadFileService.DownloadFile(Path.GetFileName(OriginalPath), File.ReadAllBytes(OriginalPath), "application/octet-stream");
        }

        protected void ViewFiles(TasksImagesViewModel Model)
        {
            if (Model != null)
            {
                isActiveImageViewComponent = true;

                if (fileManagerService.IsImage(Model.PictureLink))
                    TaskImage = ConvertImagetoBase64(Model.PictureLink);
            }
        }

        protected async Task ViewFiles(TasksTasksViewModel Model)
        {
            if (Model != null)
            {
                isActiveImageViewComponent = true;

                if (Model.PictureLink != null)
                {
                    await Task.Run(() => TaskImage = ConvertImagetoBase64(Model.PictureLink));
                }
            }
        }

        protected async Task CloseComponent()
        {
            await jSRuntime.InvokeAsync<object>("HideModal", "#MultFilesModal");
            await CallbackMessageTasksMultipleFiles.InvokeAsync(true);
        }

        protected async Task SuccessMessageFromImageViewComponent(bool Issuccess)
        {
            if (Issuccess)
                await Task.Run(() => isActiveImageViewComponent = false);
        }

        protected async Task SuccessMessageFromImageNoteComponent(bool isSuccess)
        {
            await Task.Run(() => isActiveImageNoteComponent = false);
        }

        protected async Task SuccessMessageFromDeleteFileComponent(TasksImagesViewModel model)
        {
            if (model != null)
                TaskFiles.Remove(model);

            await Task.Run(() => isActiveDeleteFileComponent = false);
        }
    }
}
