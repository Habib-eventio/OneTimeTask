using CamcoTasks.Data.ModelsViewModel;
using CamcoTasks.Service.IService;
using CamcoTasks.ViewModels.PageLoadTimeDTO;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Grids;
using CamcoTasks.ViewModels.CamcoProjectsDTO;
using CamcoTasks.Infrastructure.Entities;

namespace CamcoTasks.Pages.CamcoProject
{
    public partial class ViewCamcoProjectCosts
    {
        private bool _isRender = true;

        protected PageLoadTimeViewModel pageLoadTime = new PageLoadTimeViewModel()
        {
            PageName = "ViewCamcoProjectCost",
            SectionName = "TaskList",
            DateCreated = DateTime.Now
        };

        private SfGrid<ProjectCosting> _proGrid;

        private List<ProjectCosting> _productCosting = new List<ProjectCosting>();

        protected CamcoProjectsViewModel CamcoProject = new CamcoProjectsViewModel();

        [Inject]
        protected IPageLoadTimeService PageLoadTimeService { get; set; }
        [Inject]
        protected ICamcoProjectService CamcoProjectService { get; set; }

        [Parameter]
        public int ProCodeId { get; set; }


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
            _productCosting = (await CamcoProjectService.GetProjectCostsListAsync(ProCodeId)).OrderByDescending(p => p.LastActivityDate).ToList();
            CamcoProject = await CamcoProjectService.GetByIdAsync(ProCodeId);
        }
    }
}
