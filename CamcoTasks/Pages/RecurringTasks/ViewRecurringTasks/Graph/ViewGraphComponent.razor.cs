using CamcoTasks.Pages.RecurringTasks.ViewRecurringTasks.Graph;
using CamcoTasks.Service.IService;
using CamcoTasks.ViewModels.PageLoadTimeDTO;
using CamcoTasks.ViewModels.TasksRecTasksDTO;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace CamcoTasks.Pages.RecurringTasks.ViewRecurringTasks
{
    public partial class ViewGraphComponent
    {
        protected bool IsLoading = true;

        protected TasksRecTasksViewModel RecTaskForGraphComponent = new TasksRecTasksViewModel();

        protected PageLoadTimeViewModel PageLoadTime = new PageLoadTimeViewModel()
        {
            PageName = "RecurringTask",
            SectionName = "ViewGraphComponent",
            DateCreated = DateTime.Now
        };

        public GraphComponent GraphComponentRef { get; set; }

        [Inject]
        protected IPageLoadTimeService pageLoadTimeService { get; set; }
        [Inject]
        private IJSRuntime JsRuntime { get; set; }

        [Parameter]
        public TasksRecTasksViewModel TaskForGraph { get; set; }
        [Parameter]
        public EventCallback<bool> SuccessMessageViewGraph { get; set; }


        protected override async Task OnInitializedAsync()
        {
            await Task.Run(() => PageLoadTime.StartTime = DateTime.Now);
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await LoadContext();

                await Task.Run(() => IsLoading = false);

                StateHasChanged();

                await PageLoadTimeCalculation();
            }
        }
        protected async Task PageLoadTimeCalculation()
        {
            PageLoadTime.EndTime = DateTime.Now;
            await pageLoadTimeService.InsertAsync(PageLoadTime);
        }

        protected async Task LoadContext()
        {
            await JsRuntime.InvokeAsync<object>("ShowModal", "#GraphModel");

            if (TaskForGraph != null)
                RecTaskForGraphComponent = TaskForGraph;
        }

        protected async Task PrintChart() => await GraphComponentRef.ChartObj.PrintAsync();

        protected async Task CloseComponent()
        {
            await JsRuntime.InvokeAsync<object>("HideModal", "#GraphModel");
            await SuccessMessageViewGraph.InvokeAsync(true);
        }
    }
}
