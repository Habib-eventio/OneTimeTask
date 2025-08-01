using CamcoTasks.Service.IService;
using CamcoTasks.ViewModels.TasksTaskUpdatesDTO;
using CamcoTasks.ViewModels.UpdateNotesDTO;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Syncfusion.Blazor.Grids;

namespace CamcoTasks.Pages.RecurringTasks.ViewRecurringTasks
{
    public partial class AddNoteComponent
    {
        [Inject]
        private IJSRuntime jSRuntime { get; set; }
        [Inject]
        protected IUpdateNotesService notesService { get; set; }
        [Inject]
        private ILogger<AddNoteComponent> logger { get; set; }

        [Parameter]
        public EventCallback CloseAddNoteComponent { get; set; }
        [Parameter]
        public EventCallback<string> SuccessMessageAddNoteComponent { get; set; }

        protected UpdateNotesViewModel updateNotesViewModel = new UpdateNotesViewModel() { NoteDate = DateTime.Today };

        protected bool IsTaskDone { get; set; }

        protected string ErrorMessage { get; set; }

        protected UpdateNotesViewModel NoteEditDelete { get; set; } = new UpdateNotesViewModel();
        protected TasksTaskUpdatesViewModel NoteUpdateEditDelete { get; set; } = new TasksTaskUpdatesViewModel() { Update = string.Empty };

        protected SfGrid<UpdateNotesViewModel> NotesGrid { get; set; }

        protected Dictionary<string, object> htmlAttribute = new Dictionary<string, object>() { { "rows", "4" } };


        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await LoadAddNoteData();

                StateHasChanged();
            }
        }

        protected async Task LoadAddNoteData()
        {
            await jSRuntime.InvokeAsync<object>("ShowModal", "#AddUpdateNote");
        }

        public async Task AddNewUpdateNote()
        {
            try
            {
                if (updateNotesViewModel.NoteDate < DateTime.Today)
                {
                    ErrorMessage = "Please Select Valid Date";
                    await jSRuntime.InvokeVoidAsync("AddRedBox", "NoteDate");
                    return;
                }

                if (string.IsNullOrEmpty(updateNotesViewModel.Notes))
                {
                    ErrorMessage = "Please Enter a Note";
                    await jSRuntime.InvokeVoidAsync("AddRedBox", "Notes");
                    return;
                }

                updateNotesViewModel.UpdateID = NoteUpdateEditDelete.UpdateId;
                updateNotesViewModel.ID = await notesService.Insert(updateNotesViewModel);
                IsTaskDone = false;

                if (NotesGrid != null)
                {
                    NoteUpdateEditDelete.Notes.Add(updateNotesViewModel);
                    await NotesGrid.Refresh();
                    await NotesGrid.RefreshColumnsAsync();
                }

                await jSRuntime.InvokeAsync<object>("HideModal", "#AddUpdateNote");

                updateNotesViewModel = new UpdateNotesViewModel() { NoteDate = DateTime.Now };

                await SuccessMessageAddNoteComponent.InvokeAsync("Add Note Successfull...");
                await CloseAddNoteComponent.InvokeAsync();
            }
            catch (Exception ex)
            {
                if (ex != null)
                {
                    logger.LogWarning(ex, "Update Add Error!", ex);
                }
            }
        }

        protected async Task CloseComponent()
        {
            await jSRuntime.InvokeAsync<object>("HideModal", "#AddUpdateNote");
            await CloseAddNoteComponent.InvokeAsync();
        }
    }
}
