using Blazored.Toast.Services;
using CamcoTasks.Service.IService;
using CamcoTasks.ViewModels.PageLoadTimeDTO;
using CamcoTasks.ViewModels.TasksImagesDTO;
using Microsoft.AspNetCore.Components;
using System.Reflection.Metadata;

namespace CamcoTasks.Pages.RecurringTasks.ViewRecurringTasks.Files
{
    public partial class PdfFileView
    {
        [Inject]
        private ILogger<PdfFileView> _Logger { get; set; }
        [Inject]
        protected IPageLoadTimeService PageLoadTimeService { get; set; }
        [Inject]
        protected ITasksService taskService { get; set; }
        [Inject]
        protected IFileManagerService FileManager { get; set; }
        [Inject]
        private IToastService _toastService { get; set; }

        [Parameter]
        public string FileId { get; set; }

        protected bool IsLoading { get; set; } = true;

        protected string Data { get; set; }

        protected TasksImagesViewModel SelectFile { get; set; } = new TasksImagesViewModel();

        protected PageLoadTimeViewModel PageLoadTime { get; set; } = new PageLoadTimeViewModel()
        {
            PageName = "RecurringTask",
            SectionName = "ViewFileComponent",
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
                await LoadFileData();

                await PageLoadTimeCalculation();

                await Task.Run(() => IsLoading = false);

                StateHasChanged();
            }
        }

        protected async Task PageLoadTimeCalculation()
        {
            PageLoadTime.EndTime = DateTime.Now;
            await PageLoadTimeService.InsertAsync(PageLoadTime);
        }

        protected async Task LoadFileData()
        {
            try
            {
                SelectFile = await taskService.GetUpdateTaskImageAsync(Convert.ToInt32(FileId));
                await GetDataFromFiles();
            }
            catch(Exception ex)
            {
                _Logger.LogError(ex, "Pdf file view error", ex);
            }
        }

        protected async Task GetDataFromFiles()
        {
            try
            {
                if (SelectFile == null || string.IsNullOrEmpty(SelectFile.PictureLink))
                {
                    _toastService.ShowWarning("No Data Found in Pdf File.");
                    return;
                }

                string ext = Path.GetExtension(SelectFile.PictureLink).ToLower();

                if (!string.IsNullOrEmpty(ext) && (ext == ".pdf"))
                {
                    var fileBytet = await File.ReadAllBytesAsync(SelectFile.PictureLink);

                    if (fileBytet.Any())
                        Data = "data:application/pdf;base64," + Convert.ToBase64String(fileBytet);
                }
                else
                {
                    _toastService.ShowWarning("No Data Found in Pdf File.");
                }
            }
            catch (Exception ex)
            {
                _toastService.ShowWarning("The file is busy with another Process. Please open it " +
                            "again after some time Or the file extension is not correct.");
                _Logger.LogError(ex, "Pdf file view error", ex);
            }

        }
    }
}