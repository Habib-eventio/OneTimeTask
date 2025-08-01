using CamcoTasks.Service.IService;
using CamcoTasks.ViewModels.PageLoadTimeDTO;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace CamcoTasks.Pages.RecurringTasks.ViewRecurringTasks
{
    public partial class UploadFileNoteComponent
    {
        protected string ImageNote = string.Empty;

        protected PageLoadTimeViewModel PageLoadTime { get; set; } = new PageLoadTimeViewModel()
        {
            PageName = "RecurringTask",
            SectionName = "Upload File Note Component",
            DateCreated = DateTime.Now
        };

        [Inject]
        private IJSRuntime jSRuntime { get; set; }
        [Inject]
        protected IPageLoadTimeService pageLoadTimeService { get; set; }

        [Parameter]
        public string Note { get; set; }
        [Parameter]
        public EventCallback<string> ImageNoteComponentSuccessMessage { get; set; }
        [Parameter]
        public EventCallback CloseUploadFileNoteComponent { get; set; }


        protected override async Task OnInitializedAsync()
        {
            await Task.Run(() => PageLoadTime.StartTime = DateTime.Now);
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await LoadImageNoteData();

                StateHasChanged();

                await PageLoadTimeCalculation();
            }
        }

        protected async Task PageLoadTimeCalculation()
        {
            PageLoadTime.EndTime = DateTime.Now;
            await pageLoadTimeService.InsertAsync(PageLoadTime);
        }

        protected async Task LoadImageNoteData()
        {
            await jSRuntime.InvokeAsync<object>("ShowModal", "#UploadFileNoteModal");

            if (!string.IsNullOrEmpty(Note))
            {
                ImageNote = Note;
            }
        }

        protected async Task ModifyImageNote()
        {
            await jSRuntime.InvokeAsync<object>("HideModal", "#UploadFileNoteModal");
            await ImageNoteComponentSuccessMessage.InvokeAsync(ImageNote);
        }

        protected async Task CloseComponent()
        {
            await jSRuntime.InvokeAsync<object>("HideModal", "#UploadFileNoteModal");
            await CloseUploadFileNoteComponent.InvokeAsync();
        }
    }
}
