using CamcoTasks.Service.IService;
using CamcoTasks.ViewModels.PageLoadTimeDTO;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace CamcoTasks.Pages.RecurringTasks.ViewRecurringTasks.Files.ExcelFile
{
    public partial class ExcelFileInputDataComponent
    {
        [Inject]
        private IJSRuntime _JsRuntime { get; set; }
        [Inject]
        protected IPageLoadTimeService PageLoadTimeService { get; set; }

        [Parameter]
        public string Title { get; set; }
        [Parameter]
        public EventCallback<string> SuccessMessage { get; set; }

        protected bool IsDoingTask { get; set; } = false;

        protected string InputData { get; set; }
        protected string Error { get; set; }

        protected PageLoadTimeViewModel PageLoadTime { get; set; } = new PageLoadTimeViewModel()
        {
            PageName = "RecurringTask",
            SectionName = "ViewFileComponent",
            DateCreated = DateTime.Now
        };


        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await LoadContext();
                await PageLoadTimeCalculation();

                StateHasChanged();
            }
        }


        protected async Task PageLoadTimeCalculation()
        {
            PageLoadTime.EndTime = DateTime.Now;
            await PageLoadTimeService.InsertAsync(PageLoadTime);
        }

        protected async Task LoadContext()
        {
            await _JsRuntime.InvokeAsync<object>("ShowModal", "#ExcelFileInputComponentId");
        }

        protected async Task SetData()
        {
            await Task.Run(() => IsDoingTask = true);
            await Task.Run(() => IsDoingTask = false);
            await _JsRuntime.InvokeAsync<object>("HideModal", "#ExcelFileInputComponentId");
            await SuccessMessage.InvokeAsync(InputData);
        }

        protected async Task CloseComponent()
        {
            await _JsRuntime.InvokeAsync<object>("HideModal", "#ExcelFileInputComponentId");
            await SuccessMessage.InvokeAsync();
        }
    }
}