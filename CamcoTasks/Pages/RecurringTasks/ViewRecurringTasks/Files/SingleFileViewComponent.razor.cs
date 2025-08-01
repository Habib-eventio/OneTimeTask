using CamcoTasks.Service.IService;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace CamcoTasks.Pages.RecurringTasks.ViewRecurringTasks.Files
{
    public partial class SingleFileViewComponent
    {
        protected bool IsActiveImageViewComponent = false;

        [Parameter]
        public string FilePath { get; set; }

        [Inject]
        private IFileManagerService FileManagerService { get; set; }
        [Inject]
        private IJSRuntime JsRuntime { get; set; }


        protected async Task PrintingImage(string FilePath)
        {
            await JsRuntime.InvokeAsync<object>("PrintImage", FilePath);
        }

        protected void ViewFiles(string path)
        {
            IsActiveImageViewComponent = true;
        }

        protected async Task SuccessMessageFromImageViewComponent(bool IsSuccess)
        {
            if (IsSuccess)
                await Task.Run(() => IsActiveImageViewComponent = false);
        }

    }
}
