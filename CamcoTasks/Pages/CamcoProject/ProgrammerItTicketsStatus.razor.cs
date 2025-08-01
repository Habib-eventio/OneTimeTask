using CamcoTasks.Data.ModelsViewModel;
using CamcoTasks.Service.IService;
using CamcoTasks.ViewModels.CostingProgrammersClosedITTicketsDTO;
using CamcoTasks.ViewModels.PageLoadTimeDTO;
using Microsoft.AspNetCore.Components;

namespace CamcoTasks.Pages.CamcoProject
{
    public partial class ProgrammerItTicketsStatus
    {
        private bool _isRender = true;

        protected PageLoadTimeViewModel pageLoadTime = new PageLoadTimeViewModel()
        {
            PageName = "ViewCamcoProject",
            SectionName = "TaskList",
            DateCreated = DateTime.Now
        };

        protected List<CostingProgrammersClosedITTicketsViewModel> ClosedTickets =
            new List<CostingProgrammersClosedITTicketsViewModel>();
        protected List<SeriesData> SeriesCollection = new List<SeriesData>();

        [Inject]
        protected IPageLoadTimeService PageLoadTimeService { get; set; }
        [Inject]
        protected ICostingProgrammersClosedITTicketsServices CostingProgrammersClosedITTicketsServices { get; set; }


        protected override async Task OnInitializedAsync()
        {
            await Task.Run(() => pageLoadTime.StartTime = DateTime.Now);
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await LoadData();

                _isRender = false;

                StateHasChanged();

                await PageLoadTimeCalculation();
            }
        }

        protected async Task PageLoadTimeCalculation()
        {
            pageLoadTime.EndTime = DateTime.Now;
            await PageLoadTimeService.InsertAsync(pageLoadTime);
        }

        protected async Task LoadData()
        {
            ClosedTickets = (await CostingProgrammersClosedITTicketsServices.GetListAsync()).ToList();
            SeriesCollection = (await CostingProgrammersClosedITTicketsServices
                .GetProgrammerItTicketsStatusSeriesData()).ToList();
        }

        public async Task UpdateProgrammerStatus()
        {
            DateTime currentDate = System.DateTime.Now.AddDays(-7);
            var sunday = currentDate.AddDays(Convert.ToDouble(7 - DateTime.Now.AddDays(-7).DayOfWeek)).Date;
            await CostingProgrammersClosedITTicketsServices.UpdateProgrammerItTicketsStatus(sunday);
        }
    }
}
