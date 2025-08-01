using CamcoTasks.Data.Services;
using CamcoTasks.Service.IService;
using CamcoTasks.ViewModels.PageLoadTimeDTO;
using CamcoTasks.ViewModels.TasksRecTasksDTO;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using Syncfusion.Blazor.Inputs;

namespace CamcoTasks.Pages.RecurringTasks.ViewRecurringTasks
{
    public partial class SampleImageComponent
    {
        public TasksRecTasksViewModel RecTaskForSampleImage = new TasksRecTasksViewModel();

        protected List<UploadFiles> SampleuploadFiles { get; set; } = new List<UploadFiles>();

        protected bool IsUploading = false;
        protected bool IsRenderingImages = false;

        protected SfUploader SampleUploadObj { get; set; }

        protected Dictionary<string, object> htmlAttribute = new Dictionary<string, object>() { { "rows", "4" } };

        protected string TaskImage { get; set; } = null;

        protected PageLoadTimeViewModel PageLoadTime { get; set; } = new PageLoadTimeViewModel()
        {
            PageName = "RecurringTask",
            SectionName = "SampleImageComponent",
            DateCreated = DateTime.Now
        };


        [Inject]
        private FileManagerService fileManagerService { get; set; }
        [Inject]
        private IJSRuntime jSRuntime { get; set; }
        [Inject]
        protected IPageLoadTimeService pageLoadTimeService { get; set; }

        [Parameter]
        public TasksRecTasksViewModel TaskForSampleImage { get; set; }
        [Parameter]
        public string ErrorMessage { get; set; }
        [Parameter]
        public EventCallback<TasksRecTasksViewModel> SuccessMessageSampleImageComponent { get; set; }


        protected override async Task OnInitializedAsync()
        {
            await Task.Run(() => PageLoadTime.StartTime = DateTime.Now);
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await LoadSampleImageData();

                await PageLoadTimeCalculation();
            }
        }

        protected async Task PageLoadTimeCalculation()
        {
            PageLoadTime.EndTime = DateTime.Now;
            await pageLoadTimeService.InsertAsync(PageLoadTime);
        }

        protected async Task LoadSampleImageData()
        {
            await jSRuntime.InvokeAsync<object>("ShowModal", "#AddSampleImageModal");

            if (TaskForSampleImage != null)
                RecTaskForSampleImage = RecTaskForSampleImage;
        }

        protected async void SetImageViewModel(TasksRecTasksViewModel model, bool IsRequired)
        {
            if (IsRequired)
            {
                model.IsPicRequired = IsRequired;
            }
            else
            {
                model.IsPicRequired = IsRequired;
                model.UpdateImageDescription = null;
            }

            await jSRuntime.InvokeAsync<object>("HideModal", "#AddSampleImageModal");
            await SuccessMessageSampleImageComponent.InvokeAsync(model);
        }

        protected async Task SampleImageView()
        {
            IsRenderingImages = true;
            if (SampleuploadFiles.Count > 0)
            {
                var file = SampleuploadFiles[0];

                if (file.FileInfo != null)
                {
                    if (fileManagerService.IsImage(file.FileInfo.Name.ToLower()))
                    {
                        if (!fileManagerService.IsValidSize(file.FileInfo.Size))
                        {
                            var size = Convert.ToDouble(file.FileInfo.Size / 1000000);
                            ErrorMessage = string.Format("Image size can not be more than 20 mb. Your uploaded image size is {0} mb.", size);
                            return;
                        }
                        byte[] byteImage = file.Stream.ToArray();
                        TaskImage = $"data:image/png;base64, " + Convert.ToBase64String(byteImage);
                    }
                }
            }
            await Task.Run(() => IsRenderingImages = false);
        }

        protected void SetUpdateImageType(int Type)
        {
            RecTaskForSampleImage.UpdateImageType = Type;
        }

        protected void SelectSampleImage(UploadChangeEventArgs args)
        {
            SampleuploadFiles = args.Files;
            IsUploading = false;
        }

        protected async Task RemoveSamplePreImage(BeforeRemoveEventArgs args)
        {
            SampleuploadFiles = new();
            await SampleUploadObj.ClearAllAsync();
        }

        protected async Task CloseComponent(KeyboardEventArgs eventArgs)
        {
            if (eventArgs.Code == "Escape")
            {
                TasksRecTasksViewModel model = null;
                await jSRuntime.InvokeAsync<object>("HideModal", "#AddSampleImageModal");
                await SuccessMessageSampleImageComponent.InvokeAsync(model);
            }
        }
    }
}
