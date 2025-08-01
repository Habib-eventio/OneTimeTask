using CamcoTasks.Data.Services;
using CamcoTasks.Service.IService;
using CamcoTasks.ViewModels.PageLoadTimeDTO;
using CamcoTasks.ViewModels.TasksImagesDTO;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace CamcoTasks.Pages.RecurringTasks.ViewRecurringTasks.Files
{
    public partial class FileViewComponent
    {
        protected bool isActiveImageViewComponent = false;
        protected bool IsActiveUploadFileNoteComponent = false;
        protected bool isActiveDeleteFileComponent = false;

        protected string TaskImage = string.Empty;
        protected string FilePath = null;
        public string FileNote = string.Empty;

        protected TasksImagesViewModel SelectedImage = new TasksImagesViewModel();

        protected PageLoadTimeViewModel PageLoadTime { get; set; } = new PageLoadTimeViewModel()
        {
            PageName = "FileViewComponent",
            SectionName = "DeleteFileComponent",
            DateCreated = DateTime.Now
        };

        [Parameter]
        public TasksImagesViewModel File { get; set; }
        [Parameter]
        public EventCallback<TasksImagesViewModel> UploadFileNoteComponentEventCallback { get; set; }
        [Parameter]
        public EventCallback<TasksImagesViewModel> DeleteFileComponentEventCallback { get; set; }
        [Parameter]
        public EventCallback<string> DeleteFileComponentEventCallbackForUploadFile { get; set; }

        [Inject]
        private FileManagerService fileManagerService { get; set; }
        [Inject]
        private IJSRuntime _jSRuntime { get; set; }
        [Inject]
        protected IPageLoadTimeService pageLoadTimeService { get; set; }


        protected override async Task OnInitializedAsync()
        {
            await Task.Run(() => PageLoadTime.StartTime = DateTime.Now);
        }

        protected override async void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                await PageLoadTimeCalculation();
            }
        }

        protected async Task PageLoadTimeCalculation()
        {
            PageLoadTime.EndTime = DateTime.Now;
            await pageLoadTimeService.InsertAsync(PageLoadTime);
        }

        protected void ViewFiles(TasksImagesViewModel Model)
        {
            if (Model != null)
            {
                isActiveImageViewComponent = true;

                if (fileManagerService.IsImage(Model.PictureLink))
                    TaskImage = fileManagerService.ConvertImagetoBase64(Model.PictureLink);
            }
        }

        protected async Task SuccessMessageFromImageViewComponent(bool Issuccess)
        {
            if (Issuccess)
                await Task.Run(() => isActiveImageViewComponent = false);
        }

        protected void StartUploadFileImageNote(TasksImagesViewModel file)
        {
            IsActiveUploadFileNoteComponent = true;
            FileNote = file.ImageNote;
        }

        protected async Task UploadFileNoteComponentSucessMessage(string imageNote)
        {
                File.ImageNote = imageNote;
                File.FileName = imageNote;
                await UploadFileNoteComponentEventCallback.InvokeAsync(File);
        }

        protected void DeactiveUploadFileNoteComponent()
        {
            IsActiveUploadFileNoteComponent = false;
        }

        protected void StartDeleteTaskFile(TasksImagesViewModel file)
        {
            isActiveDeleteFileComponent = true;
            FilePath = null;
            SelectedImage = file;

        }

        protected void StartDeleteTaskFile(string filePath)
        {
            isActiveDeleteFileComponent = true;
            FilePath = filePath;
            SelectedImage = null;
        }

        protected async Task SuccessMessageFromDeleteFileComponent(TasksImagesViewModel model)
        {
            if (model != null)
            {
                await DeleteFileComponentEventCallback.InvokeAsync(model);
            }

            await Task.Run(() => isActiveDeleteFileComponent = false);
        }

        protected async Task DeleteSuccessFromDeleteFileComponent(string path)
        {
            if (!string.IsNullOrEmpty(path))
            {
                await DeleteFileComponentEventCallbackForUploadFile.InvokeAsync(path);
            }

            await Task.Run(() => isActiveDeleteFileComponent = false);
        }

        protected async Task PrintingImage(string filePath)
        {
            await _jSRuntime.InvokeAsync<object>("PrintImage", filePath);
        }
    }
}
