using CamcoTasks.Service.IService;
using CamcoTasks.ViewModels.EmailQueue;
using CamcoTasks.ViewModels.PageLoadTimeDTO;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Text.RegularExpressions;

namespace CamcoTasks.Pages.RecurringTasks.ViewRecurringTasks
{
    public partial class ViewEmailComponent
    {
        [Inject]
        private IJSRuntime jSRuntime { get; set; }
        [Inject]
        private ITasksService taskService { get; set; }
        [Inject]
        protected IPageLoadTimeService pageLoadTimeService { get; set; }

        [Parameter]
        public int EmailTaskId { get; set; }
        [Parameter]
        public EventCallback<bool> SuccessMessageViewEmail { get; set; }

        protected EmailQueueViewModel UpdateEmail { get; set; } = new();
        protected PageLoadTimeViewModel PageLoadTime { get; set; } = new PageLoadTimeViewModel()
        {
            PageName = "RecurringTask",
            SectionName = "ViewEmailComponent",
            DateCreated = DateTime.Now
        };


        protected override async Task OnInitializedAsync()
        {
            await Task.Run(() => PageLoadTime.StartTime = DateTime.Now);
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await LoadViewEmailData();

                StateHasChanged();

                await PageLoadTimeCalculation();
            }
        }

        protected async Task PageLoadTimeCalculation()
        {
            PageLoadTime.EndTime = DateTime.Now;
            await pageLoadTimeService.InsertAsync(PageLoadTime);
        }

        protected async Task LoadViewEmailData()
        {
            await jSRuntime.InvokeAsync<object>("ShowModal", "#viewEmailModal");
            UpdateEmail = await taskService.GetEmailByEmailIdAsync(EmailTaskId);

            if(!string.IsNullOrEmpty(UpdateEmail.Subject))
            {
                UpdateEmail.Subject = UpdateEmail.Subject.ToUpper();
            }

            if(!string.IsNullOrEmpty(UpdateEmail.Body))
            {
                UpdateEmail.Body = Regex.Replace(UpdateEmail.Body, "</label>", "</label> ");
            }
        }

        protected async Task CloseViewEmailModal()
        {
            await jSRuntime.InvokeAsync<object>("HideModal", "#viewEmailModal");
            await SuccessMessageViewEmail.InvokeAsync(true);
        }
    }
}
