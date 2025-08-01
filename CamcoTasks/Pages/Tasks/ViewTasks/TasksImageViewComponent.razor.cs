using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace CamcoTasks.Pages.Tasks.ViewTasks
{
    public partial class TasksImageViewComponent
    {
        [Inject]
        private IJSRuntime jSRuntime { get; set; }

        [Parameter]
        public string TaskImagePath { get; set; }
        [Parameter]
        public EventCallback<bool> IsCloseImageViewComponent { get; set; }


        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await LoadImageData();
            }
        }

        protected async Task LoadImageData()
        {
            await jSRuntime.InvokeAsync<object>("ShowModal", "#ImageModal");
        }

        protected async Task PrintingImage(string FilePath)
        {
            await jSRuntime.InvokeAsync<object>("PrintImage", FilePath);
        }

        protected async Task CloseComponent()
        {
            await IsCloseImageViewComponent.InvokeAsync(true);
        }
    }
}
