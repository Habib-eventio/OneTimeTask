using Blazored.Toast.Services;
using CamcoTasks.Data.ModelsViewModel;
using CamcoTasks.Service.IService;
using CamcoTasks.ViewModels.CamcoProjectsDTO;
using CamcoTasks.Infrastructure.Common.Email;
using CamcoTasks.ViewModels.EmployeeDTO;
using CamcoTasks.ViewModels.LoginDTO;
using CamcoTasks.ViewModels.PageLoadTimeDTO;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Inputs;
using Microsoft.JSInterop;
using static Microsoft.AspNetCore.Razor.Language.TagHelperMetadata;
using CamcoTasks.Service.Service;
using CamcoTasks.Infrastructure.Defaults;

namespace CamcoTasks.Pages.CamcoProject
{
    public partial class CreateCamcoProject
    {
        private bool _isRender = true;

        protected PageLoadTimeViewModel PageLoadTime = new PageLoadTimeViewModel()
        {
            PageName = "CreateCamcoProject",
            SectionName = "TaskList",
            DateCreated = DateTime.Now
        };

        private CamcoProjectsViewModel ProjectObject = new CamcoProjectsViewModel() { Status = "PROGRESS" };

        private SfTextBox _titleTextBox;

        protected List<CamcoProjectType> CamcoProjectTypes = new List<CamcoProjectType>();
        private List<EmployeeViewModel> EmployeeModel = new List<EmployeeViewModel>();

        protected string ProjectCodeId;
        protected string ProjectTitle;
        protected bool isPostponedProject = true;
        private DateTime selectedDate = DateTime.Today.Date;
        protected string Error { get; set; }
        protected string UpdateByEmployeeName { get; set; }
        protected string PostponeOrDeleteReason { get; set; }

        protected Dictionary<string, object> htmlAttribute = new Dictionary<string, object>() { { "rows", "3" } };
        [Inject]
        private IJSRuntime jsRuntime { get; set; }
        [Inject]
        protected IPageLoadTimeService PageLoadTimeService { get; set; }
        [Inject]
        protected NavigationManager NavigationManager { get; set; }
        [Inject]
        protected IToastService ToastService { get; set; }
        [Inject]
        protected ICamcoProjectService CamcoProjectService { get; set; }
        [Inject]
        protected IEmployeeService EmployeeService { get; set; }
        [Inject]
        protected IEmailService EmailService { get; set; }
        [Inject]
        protected IToolCribEmailDistributionListService ToolCribEmailDistributionListService { get; set; }
        [Inject]
        protected ILogingService LogingService { get; set; }
        [Inject]
        private ILogger<CreateCamcoProject> _logger { get; set; }

        [Parameter]
        public int ProjectId { get; set; }
        
        [Parameter]
        public long? EmployeeId { get; set; }


        protected override async Task OnInitializedAsync()
        {
            await Task.Run(() => PageLoadTime.StartTime = DateTime.Now);
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await LoadContext();

                _isRender = false;

                StateHasChanged();

                await PageLoadTimeCalculation();
            }
        }

        protected async Task LoadContext()
        {
            await LoadData();
            await TaskAction();
        }

        protected async Task TaskAction()
        {
            try
            {
                if (ProjectId > 0)
                {
                    ProjectTitle = "EDIT";
                    ProjectObject = await CamcoProjectService.GetByIdAsync(ProjectId);
                    ProjectCodeId = "P" + (ProjectObject.Id).ToString("0000");
                }
                else
                {
                    await AddProject();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Create camco project error:", ex);
            }
        }

        protected async Task LoadData()
        {
            CamcoProjectTypes.Add(new CamcoProjectType { Id = 1, Type = "ONE TIME PROJECT" });
            CamcoProjectTypes.Add(new CamcoProjectType { Id = 2, Type = "RECURRING PROJECT" });
            EmployeeModel = (await EmployeeService.GetListAsync(true, false)).ToList();
            var employee = await EmployeeService.GetByIdAsync(EmployeeId ?? 0);
            if (employee != null)
            {
                UpdateByEmployeeName = employee.FullName;
            }
        }

        protected async Task AddProject()
        {
            ProjectTitle = "ADD";
            ProjectObject.DateCreated = DateTime.Now;
            ProjectCodeId = await CamcoProjectService.GetProjectCodeId();
        }

        protected async Task PageLoadTimeCalculation()
        {
            PageLoadTime.EndTime = DateTime.Now;
            await PageLoadTimeService.InsertAsync(PageLoadTime);
        }

        public async Task OnCreate()
        {
            await _titleTextBox.FocusAsync();
        }

        private void Cancel()
        {
            NavigationManager.NavigateTo("/camcoProject");
        }

        protected async void SaveManufacturer(bool addNew)
        {
            var x = ProjectObject.Description == null ? 0 : ProjectObject.Description.Count();

            if (string.IsNullOrEmpty(ProjectObject.Title))
            {
                ToastService.ShowError("PLEASE ENTER TITLE FIRST!, MANDATORY FIELD");
            }
            else if (string.IsNullOrEmpty(ProjectObject.Description))
            {
                ToastService.ShowError("PLEASE ENTER DESCRIPTION!, MANDATORY FIELD");
            }
            else if (!string.IsNullOrEmpty(ProjectObject.Description) && x <= 30)
            {
                ToastService.ShowError("DESCRIPTION MUST BE MORE THEN THIRTY CHARACTERS!, MANDATORY FIELD");
            }
            else if (ProjectObject.EnteredByEmployeeId == null)
            {
                ToastService.ShowError("PLEASE SELECT ENTERED BY!, MANDATORY FIELD");
            }
            else if (ProjectObject.ChampionEmployeeId == null)
            {
                ToastService.ShowError("PLEASE SELECT CHAMPION!, MANDATORY FIELD");
            }
            else
            {
                ProjectObject.DateUpdated = DateTime.Now;
                ProjectObject.Title = ProjectObject.Title?.ToUpper();
                ProjectObject.Description = ProjectObject.Description?.ToUpper();

                if (ProjectId > 0)
                {
                    await CamcoProjectService.UpdateAsync(ProjectObject);
                    await SendEmail("THE CAMCO PROJECT UPDATED", EmailTypes.ActionBasedCamcoProjectUpdated); // CAMCO PROJECT UPDATED
                    ToastService.ShowSuccess("UPDATE SUCCESSFULLY!");
                }
                else
                {
                    ProjectObject.IsActive = true;
                    await CamcoProjectService.InsertAsync(ProjectObject);
                    await SendEmail("THE NEW CAMCO PROJECT ADDED", EmailTypes.ActionBasedCamcoProjectAdded); // CAMCO PROJECT ADDED
                    ToastService.ShowSuccess("SAVED SUCCESSFULLY!");
                }

                if (addNew)
                {
                    ProjectObject = new CamcoProjectsViewModel();
                    await AddProject();

                    StateHasChanged();
                }
                else
                {
                    Cancel();
                }
            }
        }

        protected async Task SendEmail(string emailSubject, string emailType)
        {
            LoginViewModel user = new LoginViewModel();
            var toolEmailDistribution = await ToolCribEmailDistributionListService.GetByIdAsync(7);

            if (toolEmailDistribution != null)
            {
                user = await LogingService.GetByIdAsync(toolEmailDistribution.EmpId);
            }

            var enteredByEmployee = EmployeeModel.Where(s => s.Id == ProjectObject.EnteredByEmployeeId).FirstOrDefault();
            var champion = EmployeeModel.Where(s => s.Id == ProjectObject.ChampionEmployeeId).FirstOrDefault();
            List<string> emails = new()
            {
                user.Email
            };

            if (enteredByEmployee != null)
            {
                emails.Add((await LogingService.GetByIdAsync(enteredByEmployee.LoginUserId))?.Email);
            }

            if (champion != null)
            {
                emails.Add((await LogingService.GetByIdAsync(champion.LoginUserId))?.Email);
            }

            string body = @"<div class='row' style='margin-top: 15px !important;'>
                <h3 style='font-weight: bold; text-align: center;margin-top: 15px'>CAMCO PROJECT</h3>
                <div class='col-12' style='font-size: 16px'>";
            body += "<div align='left' style='font-weight:bold;margin:10px'>TITLE:<span style='font-size: 15px;font-weight:normal'> &nbsp;" + ProjectObject.Title + "</span></div>";

            if (ProjectObject.ProjectType != null)
            {
                body += "<div align='left' style='font-weight:bold;margin:10px'> PROJECT TYPE: <span style='font-size: 15px;font-weight:normal'> &nbsp;" + ProjectObject.ProjectType + "</span></div>";
            }

            body += "<div align='left' style='font-weight:bold;margin:10px'> PROJECT CODE: <span style='font-size: 15px;font-weight:normal;color:red;font-weight:bold'>&nbsp;" + ProjectCodeId + "</span></div>" +
            "<div align='left' style='font-weight:bold;margin:10px'> DATE ENTERED: <span style='font-size: 15px;font-weight:normal'> &nbsp;" + ProjectObject.DateCreated + "</span></div>" +
            "<div align='left' style='font-weight:bold;margin:10px'> DESCRIPTION:<span style='font-size: 15px;font-weight:normal'> &nbsp;" + ProjectObject.Description + "</span></div>" +
            "<div align='left' style='font-weight:bold;margin:10px'> ENTERED BY: <span style='font-size: 15px;font-weight:normal'> &nbsp;" + enteredByEmployee.FullName + "</span></div>" +
            "<div align='left' style='font-weight:bold;margin:10px'> CHAMPION:<span style='font-size: 15px;font-weight:normal'> &nbsp;" + champion.FullName + "</span></div>";

            body = EmailDefaults.GenerateEmailTemplate("Tasks", body);
            await EmailService.SendEmailAsync(emailType,
                Array.Empty<string>(), emailSubject, body, string.Empty, emails.ToArray());
        }

        protected async Task ProjectPostponedOrDeleted(bool isProjectPostpone)
        {
            if(isProjectPostpone)
            {
                isPostponedProject = true;
            }
            else
            {
                isPostponedProject = false;
            }
            await jsRuntime.InvokeAsync<object>("ShowModal", "#ProjectPostponeOrDeleteModal");
        }

        protected async Task Save()
        {
            if (string.IsNullOrEmpty(PostponeOrDeleteReason))
            {
                Error = "PLEASE TYPE THE REASON";
                return;
            }
            else
            {
                ProjectObject.PostponedReason = PostponeOrDeleteReason;
            }
            if (isPostponedProject)
            {
                ProjectObject.IsPostponed = true;
            }
            else
            {
                ProjectObject.IsActive = false;
            }
            ProjectObject.DateUpdated = DateTime.Now;
            ProjectObject.UpdatedById = EmployeeId;
            await CamcoProjectService.UpdateAsync(ProjectObject);
            await CloseComponent();
            ToastService.ShowSuccess("UPDATE SUCCESSFULLY!");
        }
        protected async Task CloseComponent()
        {
            await jsRuntime.InvokeAsync<object>("HideModal", "#ProjectPostponeOrDeleteModal");
            Error = null;
        }
    }
}
