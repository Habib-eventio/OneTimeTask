using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace CamcoTasks.Pages.RecurringTasks.ViewRecurringTasks
{
    public partial class CameraComponent
    {
        [Inject]
        private IJSRuntime jSRuntime { get; set; }

        [Parameter]
        public EventCallback<Dictionary<Guid, string>> SuccessMessageCamera { get; set; }

        protected Dictionary<Guid, string> CapturedImgaesList { get; set; }


        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await LoadCameraData();

                StateHasChanged();
            }
        }

        protected async Task LoadCameraData()
        {
            await jSRuntime.InvokeAsync<object>("ShowModal", "#CameraModal");
            await UseDeviceCamera();
        }

        protected async Task UseDeviceCamera()
        {
            CapturedImgaesList = new Dictionary<Guid, string>();
            await jSRuntime.InvokeVoidAsync("ready", this);
        }

        protected async Task Capture()
        {
            CapturedImgaesList.Add(Guid.NewGuid(), await jSRuntime.InvokeAsync<string>("take_snapshot"));
        }

        protected void RemoveCamImage(KeyValuePair<Guid, string> Kvp)
        {
            CapturedImgaesList.Remove(Kvp.Key);
        }

        protected async Task CloseComponent(bool isClose)
        {
            await jSRuntime.InvokeAsync<object>("HideModal", "#CameraModal");

            if (isClose)
                CapturedImgaesList = null;

            await SuccessMessageCamera.InvokeAsync(CapturedImgaesList);
        }
    }
}
