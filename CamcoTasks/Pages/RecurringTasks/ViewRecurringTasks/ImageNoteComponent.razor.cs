using CamcoTasks.Service.IService;
using CamcoTasks.ViewModels.PageLoadTimeDTO;
using CamcoTasks.ViewModels.TasksImagesDTO;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Syncfusion.Blazor.Inputs;

namespace CamcoTasks.Pages.RecurringTasks.ViewRecurringTasks
{
    public partial class ImageNoteComponent
    {
        [Inject]
        protected ITasksService taskService { get; set; }
        [Inject]
        private IJSRuntime jSRuntime { get; set; }
        [Inject]
        protected IPageLoadTimeService pageLoadTimeService { get; set; }

        [Parameter]
        public TasksImagesViewModel ImageNoteModel { get; set; }
        [Parameter]
        public EventCallback<bool> ImageNoteComponentSuccessMessage { get; set; }

        protected SfTextBox ImageNoteBox { get; set; }

        protected PageLoadTimeViewModel PageLoadTime { get; set; } = new PageLoadTimeViewModel()
        {
            PageName = "RecurringTask",
            SectionName = "ImageNoteComponent",
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
                await LoadImageNoteData();

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
            await jSRuntime.InvokeAsync<object>("ShowModal", "#ImageNoteModal");
        }

        protected async Task ModifyImageNote()
        {
            await taskService.UpdateTaskImageAsync(ImageNoteModel);
            await jSRuntime.InvokeAsync<object>("HideModal", "#ImageNoteModal");
            await ImageNoteComponentSuccessMessage.InvokeAsync(true);
        }

        protected async Task CloseComponent()
        {
            ImageNoteModel.ImageNote = null;
            await ImageNoteComponentSuccessMessage.InvokeAsync(false);
        }
    }
}
