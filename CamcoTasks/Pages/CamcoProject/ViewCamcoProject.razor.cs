using Blazored.Toast.Services;
using CamcoTasks.Service.IService;
using CamcoTasks.ViewModels.CamcoProjectsDTO;
using CamcoTasks.ViewModels.PageLoadTimeDTO;
using CamcoTasks.ViewModels.TasksRecTasksDTO;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Syncfusion.Blazor.Grids;
using Syncfusion.Blazor.Inputs;
using Syncfusion.Blazor.Navigations;
using CamcoTasks.ViewModels.EmployeeDTO;
using CamcoTasks.Service.Service;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.CodeAnalysis;

namespace CamcoTasks.Pages.CamcoProject
{
    public partial class ViewCamcoProject
    {
        private bool _isRender = true;

        protected bool IsActiveApprovalClosureRequestComponent { get; set; } = false;
        protected bool ComboBoxVisible { get; set; } = false;

        protected long? changeByEmployeeId;
        protected int ProjectId { get; set; } = 0;
        protected List<EmployeeViewModel> Employees { get; set; }
        protected List<CamcoProjectsViewModel> productSoList = new List<CamcoProjectsViewModel>();

        protected List<CamcoProjectsViewModel> DeletedProjectList = new List<CamcoProjectsViewModel>();

        protected List<CamcoProjectsViewModel> PostponedProjectList = new List<CamcoProjectsViewModel>();

        public int camcoProjectIdForViewApprovalClosureRequest;

        protected PageLoadTimeViewModel pageLoadTime = new PageLoadTimeViewModel()
        {
            PageName = "ViewCamcoProject",
            SectionName = "TaskList",
            DateCreated = DateTime.Now
        };

        protected SfGrid<CamcoProjectsViewModel> ProGrid;

        [Inject]
        protected IPageLoadTimeService PageLoadTimeService { get; set; }
        [Inject]
        protected ICamcoProjectService CamcoProjectService { get; set; }
        [Inject]
        protected IEmployeeService EmployeeService { get; set; }
        [Inject]
        protected IDepartmentService DepartmentService { get; set; }
        [Inject]
        private IToastService toastService { get; set; }
        [Inject]
        private IFileManagerService _fileManagerService { get; set; }
        [Inject]
        private IJSRuntime _jSRuntime { get; set; }
        [Inject]
        protected IEmployeeService employeeService { get; set; }
        [Inject]
        protected NavigationManager navigationManager { get; set; }

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

        protected async Task LoadData()
        {
            var list = await CamcoProjectService.GetProductListAsync();
            DeletedProjectList = PostponedProjectList = list;
            productSoList = list.Where(x => x.IsActive && !x.IsPostponed).ToList();
            var employees = await employeeService.GetListAsync(true, false);
            Employees = employees.Where(x => x.IsActive).OrderBy(a => a.FullName).DistinctBy(a => a.FullName).ToList();
        }

        protected async Task PageLoadTimeCalculation()
        {
            pageLoadTime.EndTime = DateTime.Now;
            await PageLoadTimeService.InsertAsync(pageLoadTime);
        }

        public async Task SearchValueChange(ChangedEventArgs args)
        {
            await ProGrid.SearchAsync(args.Value);
        }

        protected async Task StartPrinting(ClickEventArgs args)
        {
            pageLoadTime.StartTime = DateTime.Now;
            pageLoadTime.SectionName = "ExportReport";

            if (args.Item.Text == "PRINT REPORT")
            {
                toastService.ShowSuccess("Generating Report Started, Please Wait.");
                var pdf = _fileManagerService.CreateCamcoProjectPdfInMemory(productSoList);
                await _jSRuntime.InvokeVoidAsync("jsSaveAsFile", "DeactiveRecurringTasks.pdf", Convert.ToBase64String(pdf));
            }
            else if (args.Item.Text == "EXCEL EXPORT")
            {
                toastService.ShowSuccess("Generating Report Started, Please Wait.");
                ExcelExportProperties exportProperties = new ExcelExportProperties();
                exportProperties.IncludeTemplateColumn = true;
                exportProperties.FileName = "DeactiveRecurringTasks.xlsx";
                await ProGrid.ExportToExcelAsync(exportProperties);
            }

            await PageLoadTimeCalculation();
        }
        protected async Task SetStatus(CamcoProjectsViewModel entity)
        {
            var selectedProject = await CamcoProjectService.GetByIdAsync(entity.Id);
            camcoProjectIdForViewApprovalClosureRequest = selectedProject.Id;
            IsActiveApprovalClosureRequestComponent = true;
        }
        public async Task DeactivateApprovalClosureComponent()
        {
            await Task.Run(() => IsActiveApprovalClosureRequestComponent = false);
        }
        protected void Keydown(KeyboardEventArgs args)
        {
            if (args.Code == "Enter" && changeByEmployeeId != null)
            {
                OpenEditCamcoProject();
            }
        }
        protected void CloseUserNamePopup()
        {
            ComboBoxVisible = false;
        }
        protected void UserNamePopupOpen(int projectId)
        {
            ComboBoxVisible = true;
            ProjectId = projectId;
        }
        protected void OpenEditCamcoProject()
        {
            if (changeByEmployeeId != null && ProjectId != 0)
            {
                CloseUserNamePopup();
                navigationManager.NavigateTo("/camcoProject/createCamcoProject/" + ProjectId + "/" + changeByEmployeeId, true);
            }
            else
            {
                ComboBoxVisible = true;
                toastService.ShowError("PLEASE SELECT USERNAME");
            }
        }
        protected void OpenDeletedCamcoProjects()
        {
            productSoList = DeletedProjectList.Where(x => x.IsActive == false).ToList();
        } 
        protected void OpenPostponedCamcoProjects()
        {
            productSoList = DeletedProjectList.Where(x => x.IsPostponed).ToList();
        }
    }
}
