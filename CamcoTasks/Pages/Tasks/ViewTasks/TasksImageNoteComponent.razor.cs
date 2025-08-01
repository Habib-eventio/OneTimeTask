using CamcoTasks.Service.IService;
using CamcoTasks.ViewModels.TasksImagesDTO;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Syncfusion.Blazor.Inputs;

namespace CamcoTasks.Pages.Tasks.ViewTasks
{
    public partial class TasksImageNoteComponent
    {
        [Inject]
        protected ITasksService taskService { get; set; }
        [Inject]
        private IJSRuntime jSRuntime { get; set; }

        [Parameter]
        public TasksImagesViewModel ImageNoteModel { get; set; }
        [Parameter]
        public EventCallback<bool> ImageNoteComponentSuccessMessage { get; set; }

        protected SfTextBox ImageNoteBox { get; set; }


        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await LoadImageNoteData();
            }
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
