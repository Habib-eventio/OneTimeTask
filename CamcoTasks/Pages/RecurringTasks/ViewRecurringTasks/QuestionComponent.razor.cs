using CamcoTasks.Service.IService;
using CamcoTasks.ViewModels.PageLoadTimeDTO;
using CamcoTasks.ViewModels.TasksRecTasksDTO;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;

namespace CamcoTasks.Pages.RecurringTasks.ViewRecurringTasks
{
    public partial class QuestionComponent
    {
        public TasksRecTasksViewModel RecTaskForQuestion = new TasksRecTasksViewModel();

        protected PageLoadTimeViewModel PageLoadTime { get; set; } = new PageLoadTimeViewModel()
        {
            PageName = "RecurringTask",
            SectionName = "QuestionComponent",
            DateCreated = DateTime.Now
        };

        [Inject]
        private IJSRuntime JsRuntime { get; set; }
        [Inject]
        protected IPageLoadTimeService pageLoadTimeService { get; set; }

        [Parameter]
        public TasksRecTasksViewModel TaskForQuestion { get; set; }
        [Parameter]
        public string ErrorMessage { get; set; }
        [Parameter]
        public EventCallback<TasksRecTasksViewModel> SuccessMessageQutionComponent { get; set; }


        protected override async Task OnInitializedAsync()
        {
            await Task.Run(() => PageLoadTime.StartTime = DateTime.Now);
        }

        protected override async void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                await LoadQuestionComponentData();

                await PageLoadTimeCalculation();
            }
        }

        protected async Task PageLoadTimeCalculation()
        {
            PageLoadTime.EndTime = DateTime.Now;
            await pageLoadTimeService.InsertAsync(PageLoadTime);
        }

        protected async Task LoadQuestionComponentData()
        {
            await JsRuntime.InvokeAsync<object>("ShowModal", "#AddQuestionModal");
        }

        protected async void SetQuestionViewModel(TasksRecTasksViewModel model, bool IsRequired)
        {
            if (IsRequired)
            {
                model.IsQuestionRequired = IsRequired;
            }
            else
            {
                model = null;
            }

            await JsRuntime.InvokeAsync<object>("HideModal", "#AddQuestionModal");
            await SuccessMessageQutionComponent.InvokeAsync(model);
        }

        protected async Task CloseComponent(KeyboardEventArgs eventArgs)
        {
            if (eventArgs.Code == "Escape")
            {
                TasksRecTasksViewModel model = null;
                await JsRuntime.InvokeAsync<object>("HideModal", "#AddQuestionModal");
                await SuccessMessageQutionComponent.InvokeAsync(model);
            }
        }
    }
}
