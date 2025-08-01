using CamcoTasks.Service.IService;
using CamcoTasks.ViewModels.CamcoProjectsDTO;
using CamcoTasks.Infrastructure.Common.Email;
using CamcoTasks.ViewModels.EmployeeDTO;
using CamcoTasks.ViewModels.PageLoadTimeDTO;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Syncfusion.Blazor.Inputs;
using System.Text;
using CamcoTasks.Infrastructure.Defaults;

namespace CamcoTasks.Pages.CamcoProject
{
    public partial class ApprovalClosureComponent
    {
        protected CamcoProjectsViewModel Project = new CamcoProjectsViewModel();

        protected List<CamcoProjectsViewModel> productSoList = new List<CamcoProjectsViewModel>();

        protected List<CamcoProjectsViewModel> inProgressProjects = new List<CamcoProjectsViewModel>();

        protected List<CamcoProjectsViewModel> completedProjects = new List<CamcoProjectsViewModel>();

        private List<EmployeeViewModel> EmployeeModel = new List<EmployeeViewModel>();
        protected string proCodeId;

        [Inject]
        private IFileManagerService FileManagerService { get; set; }
        [Inject]
        protected IEmployeeService EmployeeService { get; set; }
        [Inject]
        protected IEmailService EmailService { get; set; }
        [Inject]
        protected ICamcoProjectService CamcoProjectService { get; set; }
        [Inject]
        private IJSRuntime JsRuntime { get; set; }
        [Inject]
        protected IPageLoadTimeService PageLoadTimeService { get; set; }
        [Parameter]
        public EventCallback<int> RefreshToParentDeleteRecUpdateComponent { get; set; }
        [Parameter]
        public EventCallback CloseApprovalClosureComponent { get; set; }
        [Parameter]
        public int CamcoProjectId { get; set; }

        protected string ErrorMessage { get; set; } = string.Empty;

        protected PageLoadTimeViewModel PageLoadTime { get; set; } = new PageLoadTimeViewModel()
        {
            PageName = "ViewCamcoProject",
            SectionName = "ApprovalClosureComponent",
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
                await PageLoadTimeCalculation();

                await LoadData();
            }
        }

        protected async Task PageLoadTimeCalculation()
        {
            PageLoadTime.EndTime = DateTime.Now;
            await PageLoadTimeService.InsertAsync(PageLoadTime);
        }
        protected async Task LoadData()
        {
            await JsRuntime.InvokeAsync<object>("ShowModal", "#approvalClosure");
            Project = await CamcoProjectService.GetByIdAsync(CamcoProjectId);
            productSoList = await CamcoProjectService.GetProductListAsync();
            EmployeeModel = (await EmployeeService.GetListAsync(true, false)).ToList();
        }
        protected async Task ReloadData()
        {
            productSoList = await CamcoProjectService.GetProductListAsync();
        }
        protected async Task SelectHowToFile(UploadChangeEventArgs args)
        {
            foreach (var file in args.Files)
            {
                if (!FileManagerService.IsValidSize(file.FileInfo.Size)
                    || !FileManagerService.IsFile(file.FileInfo.Name)
                    && !FileManagerService.IsImage(file.FileInfo.Name))
                {
                    var size = Convert.ToDouble(file.FileInfo.Size / 1000000);
                    return;
                }

                var fileName = $"{Guid.NewGuid()}.{file.FileInfo.Type}";
                var resultPath = FileManagerService.CreateRecurringTaskDirectory("CamcoProjects") + fileName;
                FileManagerService.WriteToFile(file.Stream, resultPath);

                Project.Notes = resultPath;

                await CamcoProjectService.UpdateAsync(Project);

                break;
            }
        }
        
        protected async Task SendCompletedEmail()
        {
            inProgressProjects = productSoList.Where(x => x.Status == "PROGRESS").ToList();
            completedProjects = productSoList.Where(x => x.Status == "COMPLETE").ToList();
            var employeeEntered = EmployeeModel.Where(s => s.Id == Project.EnteredByEmployeeId).FirstOrDefault();
            var employeeChampion = EmployeeModel.Where(s => s.Id == Project.ChampionEmployeeId).FirstOrDefault();
            proCodeId = "P" + (Project.Id).ToString("0000");
            string emailSubject = "COMPLETED CAMCO PROJECT";
            StringBuilder customBody = new();
            customBody.Append($"<p><b>TITLE: </b>{Project.Title.ToUpper()}</p>");
            customBody.Append($"<p><b>PROJECT TYPE: </b>{Project.ProjectType}</p>");
            customBody.Append($"<p><b>PROJECT CODE: </b>{proCodeId}</p>");
            customBody.Append($"<p><b>DATE ENTERED: </b>{Project.DateCreated.Date.ToString("M/d/yyyy")}</p>");
            customBody.Append($"<p><b>DESCRIPTION: </b>{Project.Description}</p>");
            customBody.Append($"<p><b>ENTERED BY: </b>{employeeEntered?.FullName}</p>");
            customBody.Append($"<p><b>CHAMPION: </b>{employeeChampion?.FullName}</p>");
            customBody.Append($"<br>");
            customBody.Append($"<p><b>TOTAL NUMBER OF OPEN PROJECTS: </b>{inProgressProjects.Count}</p>");
            customBody.Append($"<p><b>TOTAL NUMBER OF PROJECTS COMPLETED: </b>{completedProjects.Count}</p>");

            string body = EmailDefaults.GenerateEmailTemplate("Tasks", customBody.ToString());
            await EmailService.SendEmailAsync(EmailTypes.ActionBasedCamcoProjectCompleted,
                Array.Empty<string>(), emailSubject, body, Project.Notes, new string[] { "trinity.purdy@camcomfginc.com", "mike.brown@camcomfginc.com", "rarnold@camcomfginc.com;" });

        }

        protected async Task RemoveUploadedFile(BeforeRemoveEventArgs args)
        {
            Project.Notes = "";
            await CamcoProjectService.UpdateAsync(Project);
        }
        protected async void StatusChangeCheck()
        {
            if (string.IsNullOrEmpty(Project.Notes))
            {
                ErrorMessage = "PLEASE UPLOAD REQUIRE DOCUMENT TO COMPLETE";
            }
            else
            {
                Project.Status = "COMPLETE";
                await CamcoProjectService.UpdateAsync(Project);
                await JsRuntime.InvokeAsync<object>("HideModal", "#approvalClosure");
                await CloseApprovalClosureComponent.InvokeAsync();
                await ReloadData();
                await SendCompletedEmail();

            }
        }

        protected async Task CloseComponent()
        {
            await JsRuntime.InvokeAsync<object>("HideModal", "#approvalClosure");
            await CloseApprovalClosureComponent.InvokeAsync();
        }
    }
}
