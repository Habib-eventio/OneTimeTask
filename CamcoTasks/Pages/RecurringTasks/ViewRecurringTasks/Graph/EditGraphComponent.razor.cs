using CamcoTasks.Service.IService;
using CamcoTasks.ViewModels.PageLoadTimeDTO;
using CamcoTasks.ViewModels.TasksRecTasksDTO;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;

namespace CamcoTasks.Pages.RecurringTasks.ViewRecurringTasks
{
    public partial class EditGraphComponent
    {
        protected TasksRecTasksViewModel RecTaskForEditGraphComponent = new TasksRecTasksViewModel();

        protected string ErrorMessageForEditGraphComponent = string.Empty;
        protected string XAxisIntervalType;

        protected string content = "THIS SETS THE GRAPHS VERTICAL HEIGHT AND MAINTAINS A CONSTANT GRAPH HEIGHT FOR EASY READING. FOR EXAMPLE, IF 10 WERE THE GRAPH MAX, THE GRAPH WOULDN'T SHOW ANY POINTS THAT ARE ABOVE 10, AND WOULD MAKE SURE THE GRAPH STAYS AT THAT SAME HEIGHT EVEN IF THE ALL POINTS ARE BELOW 10 ON THE GRAPH.";

        protected List<string> XAxisIntervalTypeList = new List<string>();

        [Inject]
        private IJSRuntime JsRuntime { get; set; }
        [Inject]
        protected IPageLoadTimeService pageLoadTimeService { get; set; }

        [Parameter]
        public TasksRecTasksViewModel TaskForGraph { get; set; }
        [Parameter]
        public string ErrorMessage { get; set; }
        [Parameter]
        public EventCallback<TasksRecTasksViewModel> SuccessMessageGraphComponent { get; set; }

        protected PageLoadTimeViewModel PageLoadTime { get; set; } = new PageLoadTimeViewModel()
        {
            PageName = "RecurringTask",
            SectionName = "GraphComponent",
            DateCreated = DateTime.Now
        };


        protected override async Task OnInitializedAsync()
        {
            await Task.Run(() => PageLoadTime.StartTime = DateTime.Now);
        }

        protected override async void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                await LoadContent();

                StateHasChanged();

                await PageLoadTimeCalculation();
            }
        }

        protected async Task PageLoadTimeCalculation()
        {
            PageLoadTime.EndTime = DateTime.Now;
            await pageLoadTimeService.InsertAsync(PageLoadTime);
        }

        protected async Task LoadContent()
        {
            await JsRuntime.InvokeAsync<object>("ShowModal", "#EditGraphModal");
            LoadGraphData();
        }

        protected void LoadGraphData()
        {
            if (TaskForGraph != null)
                RecTaskForEditGraphComponent = TaskForGraph;

            if (!string.IsNullOrEmpty(ErrorMessage))
                ErrorMessageForEditGraphComponent = ErrorMessage;

            var graphXintervalData = GraphData.GraphXAxisIntervalList;
            XAxisIntervalTypeList = graphXintervalData
            .Select(x => x.XAxisIntervalType).ToList();

            if (RecTaskForEditGraphComponent.IsXAxisInterval.HasValue
                && RecTaskForEditGraphComponent.IsXAxisInterval.Value)
            {
                XAxisIntervalType = (graphXintervalData
                    .FirstOrDefault(x => x.Id == RecTaskForEditGraphComponent.XAxisIntervalTypeId))
                    .XAxisIntervalType;
            }
        }

        protected async void SetViewModelGraph(TasksRecTasksViewModel model)
        {
            if (model == null)
            {
                return;
            }

            if(model.IsXAxisInterval.HasValue && model.IsXAxisInterval.Value)
            {
                if(string.IsNullOrEmpty(XAxisIntervalType))
                {
                    ErrorMessageForEditGraphComponent = "Please select x axis interval type";
                    return;
                }

                if (model.XAxisIntervalRange == null
                    || model.XAxisIntervalRange <= 0)
                {
                    ErrorMessageForEditGraphComponent = "Please input correct x axis interval range";
                    return;
                }
            }

            if (model.MaxYAxisValue != null || model.MaxYAxisValue > 0)
            {
                model.IsMaxValueRequired = true;
            }
            else
            {
                model.IsMaxValueRequired = false;
            }

           var data = GraphData.GraphXAxisIntervalList
                .FirstOrDefault(x => x.XAxisIntervalType == XAxisIntervalType);

            if (data != null)
                model.XAxisIntervalTypeId = data.Id;

           await JsRuntime.InvokeAsync<object>("HideModal", "#EditGraphModal");
            await SuccessMessageGraphComponent.InvokeAsync(model);
        }

        protected async Task CloseComponent(KeyboardEventArgs eventArgs)
        {
            if (eventArgs.Code == "Escape")
            {
                TasksRecTasksViewModel model = null;
                await JsRuntime.InvokeAsync<object>("HideModal", "#EditGraphModal");
                await SuccessMessageGraphComponent.InvokeAsync(model);
            }
        }
    }
}
