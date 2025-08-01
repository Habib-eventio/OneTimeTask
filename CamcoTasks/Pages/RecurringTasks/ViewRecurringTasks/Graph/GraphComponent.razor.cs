using CamcoTasks.Data.ModelsViewModel;
using CamcoTasks.Data.Services;
using CamcoTasks.Service.IService;
using CamcoTasks.ViewModels.PageLoadTimeDTO;
using CamcoTasks.ViewModels.TasksRecTasksDTO;
using CamcoTasks.ViewModels.TasksTaskUpdatesDTO;
using DocumentFormat.OpenXml.Presentation;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using Syncfusion.Blazor.Charts;

namespace CamcoTasks.Pages.RecurringTasks.ViewRecurringTasks.Graph
{
    public partial class GraphComponent
    {
        private string chartWidth;
        private string chartHeight;
        private double zoomFactor;
        private double zoomPosition;

        protected List<List<TasksTaskUpdatesViewModel>> SeriesCollection = new List<List<TasksTaskUpdatesViewModel>>();

        protected bool IsLoading = true;

        protected bool isScrollbarEnabled = false;

        protected IntervalType XAxisIntervalName;

        protected TasksRecTasksViewModel RecTaskForGraphComponent = new TasksRecTasksViewModel();

        protected PageLoadTimeViewModel PageLoadTime = new PageLoadTimeViewModel()
        {
            PageName = "RecurringTask",
            SectionName = "GraphComponent",
            DateCreated = DateTime.Now
        };

        [Parameter]
        public SfChart ChartObj { get; set; }

        [Inject]
        private ILogger<ViewGraphComponent> _Logger { get; set; }
        [Inject]
        private IJSRuntime JsRuntime { get; set; }
        [Inject]
        protected ITasksService taskService { get; set; }
        [Inject]
        private FileManagerService FileManagerService { get; set; }
        [Inject]
        protected IPageLoadTimeService PageLoadTimeService { get; set; }

        [Parameter]
        public TasksRecTasksViewModel RecurringTask { get; set; }

        [Parameter]
        public bool WithoutModel { get; set; }


        protected override async Task OnInitializedAsync()
        {
            await Task.Run(() => PageLoadTime.StartTime = DateTime.Now);
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await GetScreenRegulations();
                await LoadData();

                IsLoading = false;

                StateHasChanged();

                await PageLoadTimeCalculation();
            }
        }

        protected async Task GetScreenRegulations()
        {
            try
            {
                var result = await JsRuntime.InvokeAsync<object>("GetScreenRegulations");
                var jsonString = System.Text.Json.JsonSerializer.Serialize(result);
                var screenRegulationsObject = JsonConvert.DeserializeObject<ScreenRegulations>(jsonString);

                int width = Convert.ToInt32(screenRegulationsObject.width);
                int height = Convert.ToInt32(screenRegulationsObject.height);

                if (width >= 1920 && height >= 1080)
                {
                    chartWidth = "1500px";
                    chartHeight = "540px";
                }
                else if ((width >= 1280 && width < 1920) && (height >= 720 && height < 1080))
                {
                    chartWidth = "1050px";
                    chartHeight = "400px";
                }
                else
                {
                    chartWidth = "480px";
                    chartHeight = "220px";
                }
                if (WithoutModel)
                {
                    chartWidth = "100%";
                }
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, "View graph error", ex);
            }
        }

        protected async Task PageLoadTimeCalculation()
        {
            PageLoadTime.EndTime = DateTime.Now;
            await PageLoadTimeService.InsertAsync(PageLoadTime);
        }

        public async Task LoadData()
        {
            await ViewTaskGraph();
        }

        protected async Task ViewTaskGraph()
        {
            try
            {
                if (RecurringTask != null)
                {
                    RecTaskForGraphComponent = RecurringTask;
                    RecTaskForGraphComponent = await taskService.GetRecurringTaskById(RecTaskForGraphComponent.Id) ??
                                               new TasksRecTasksViewModel();

                    RecTaskForGraphComponent.GraphTitle = string.IsNullOrEmpty(RecTaskForGraphComponent.GraphTitle) ? "No Title" :
                    RecTaskForGraphComponent.GraphTitle.ToUpper();

                    RecTaskForGraphComponent.VerticalAxisTitle = string.IsNullOrEmpty(RecTaskForGraphComponent.VerticalAxisTitle) ? "No Title" :
                        RecTaskForGraphComponent.VerticalAxisTitle.ToUpper();

                    var recurringTaskUpdates = await taskService.GetTaskUpdates(RecTaskForGraphComponent.Id, "Rich Arnold ''NUDGED'' you to get the recurring Task", "Rich Arnold ''Email'' you to get the recurring Task", false);

                    recurringTaskUpdates = recurringTaskUpdates.Where(x => x.GraphNumber >= 0);

                    if (recurringTaskUpdates != null)
                    {
                        var graphUpdates = recurringTaskUpdates.OrderByDescending(x => x.UpdateDate.Date).ToList();

                        SeriesCollection.Add(graphUpdates);

                        foreach (var update in graphUpdates)
                        {
                            update.GraphDate = update.UpdateDate.ToString("MM/dd/yyyy");
                        }

                        if (RecTaskForGraphComponent.IsTrendLine.HasValue && RecTaskForGraphComponent.IsTrendLine.Value)
                        {
                            RecTaskForGraphComponent.TrendLineTitle = "8 Week Trendline";
                            var graphTrendLineUpdates = await taskService.RecTaskGraphTrendLineCalculation(-2, graphUpdates);
                            SeriesCollection.Add(graphTrendLineUpdates);
                        }
                        var totalCount = graphUpdates.Count;
                        if (totalCount > 8)
                        {
                            isScrollbarEnabled = true;
                            zoomPosition = 0.0;
                            zoomFactor = 8.0 / totalCount;
                        }
                        else
                        {
                            zoomPosition = 1.0;
                            zoomFactor = 1.0;
                        }
                    }

                    if (RecTaskForGraphComponent.IsXAxisInterval.HasValue
                        && RecTaskForGraphComponent.IsXAxisInterval.Value)
                    {
                        var data = GraphData.GraphXAxisIntervalList
                            .FirstOrDefault(x => x.Id == RecTaskForGraphComponent.XAxisIntervalTypeId);

                        if (data != null)
                            XAxisIntervalName = data.XAxisIntervalName;
                    }

                    if (ChartObj != null)
                    {
                        await ChartObj.RefreshAsync();
                    }
                }
            }
            catch(Exception ex)
            {
                _Logger.LogError(ex, "Rec graph error:", ex);
            }
        }

        public async Task<string> ExportGraphAsync()
        {
            string ResultPath = string.Empty;

            try
            {
                //await LoadData();
                //await Task.Delay(2000);
                if (ChartObj != null)
                {
                    var graphBase64 =
                        await JsRuntime.InvokeAsync<string>("TakeScreenshotAndCopy", "recurringTaskGraphId");

                    if (!string.IsNullOrEmpty(graphBase64))
                    {
                        string[] data = graphBase64.Split(",");

                        string time = DateTime.Now.TimeOfDay.ToString();
                        time = time.Replace(".", "_").Replace(":", "_");

                        string ImageFileName = Convert.ToString(RecTaskForGraphComponent.Id) + "_"
                            + time + ".PNG";
                        ResultPath = FileManagerService.CreateRecurringTaskDirectory("GraphForEmail") + ImageFileName;

                        byte[] bytes = Convert.FromBase64String(data[1]);
                        Stream stream = new MemoryStream(bytes);

                        FileManagerService.WriteToFile((MemoryStream)stream, ResultPath);
                    }
                }
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, "recurring task graph error:", ex);
            }

            return ResultPath;
        }
    }
}
