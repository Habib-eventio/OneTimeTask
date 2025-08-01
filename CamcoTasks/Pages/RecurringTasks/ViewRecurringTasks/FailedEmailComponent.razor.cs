using CamcoTasks.Service.IService;
using CamcoTasks.ViewModels.EmployeeDTO;
using CamcoTasks.ViewModels.PageLoadTimeDTO;
using CamcoTasks.ViewModels.TasksRecTasksDTO;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;

namespace CamcoTasks.Pages.RecurringTasks.ViewRecurringTasks
{
    public partial class FailedEmailComponent
    {
        protected List<EmployeeEmail> EmployeeEmails = new List<EmployeeEmail>();

        protected PageLoadTimeViewModel PageLoadTime { get; set; } = new PageLoadTimeViewModel()
        {
            PageName = "RecurringTask",
            SectionName = "FaildEmailComponent",
            DateCreated = DateTime.Now
        };

        [Inject]
        private IJSRuntime JsRuntime { get; set; }
        [Inject]
        protected IPageLoadTimeService pageLoadTimeService { get; set; }

        [Parameter]
        public List<EmployeeEmail> FailedemployeeEmails { get; set; }
        [Parameter]
        public bool IsHandDeliveredDocument { get; set; }
        [Parameter]
        public string ErrorMessage { get; set; }
        [Parameter]
        public EventCallback<List<EmployeeEmail>> SuccessMessageFaildEmailComponent { get; set; }


        protected override async Task OnInitializedAsync()
        {
            await Task.Run(() => PageLoadTime.StartTime = DateTime.Now);
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await LoadFailedEmaildData();

                StateHasChanged();

                await PageLoadTimeCalculation();
            }
        }

        protected async Task PageLoadTimeCalculation()
        {
            PageLoadTime.EndTime = DateTime.Now;
            await pageLoadTimeService.InsertAsync(PageLoadTime);
        }

        protected async Task LoadFailedEmaildData()
        {
            await JsRuntime.InvokeAsync<object>("ShowModal", "#FailedEmailModal");

            if (FailedemployeeEmails.Any())
                EmployeeEmails = FailedemployeeEmails;
        }

        protected void MarkFailedCheck(ChangeEventArgs args, EmployeeEmail e) => e.IsSelected = (bool)args.Value;

        protected async Task SaveFalidEmail()
        {
            await JsRuntime.InvokeAsync<object>("HideModal", "#FailedEmailModal");
            await SuccessMessageFaildEmailComponent.InvokeAsync(EmployeeEmails);
        }

        protected async Task CloseComponent(KeyboardEventArgs eventArgs)
        {
            if (eventArgs.Code == "Escape")
            {
                List<EmployeeEmail> model = null;
                await JsRuntime.InvokeAsync<object>("HideModal", "#FailedEmailModal");
                await SuccessMessageFaildEmailComponent.InvokeAsync(model);
            }
        }
    }
}
