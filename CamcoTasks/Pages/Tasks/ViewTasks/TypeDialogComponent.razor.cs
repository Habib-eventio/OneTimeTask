using CamcoTasks.Service.IService;
using CamcoTasks.ViewModels.TasksTasksDTO;
using CamcoTasks.ViewModels.TasksTasksTaskTypeDTO;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using static CamcoTasks.Pages.Tasks.ViewOneTimeSubTasks;

namespace CamcoTasks.Pages.Tasks.ViewTasks
{
    public partial class TypeDialogComponent
    {
        [Inject]
        private IJSRuntime jSRuntime { get; set; }
        [Inject]
        protected ITasksService taskService { get; set; }
        [Inject]
        protected ILogger<TypeDialogComponent> logger { get; set; }

        [Parameter]
        public EventCallback<Dictionary<string, string>> CallbackMessageTypeDialogComponent { get; set; }

        public TasksTasksTaskTypeViewModel NewTaskType { get; set; } = new TasksTasksTaskTypeViewModel();

        public List<string> TaskTypesAll { get; set; } = new List<string>();
        public List<DDData> TaskTypes { get; set; } = new List<DDData>();
        public TasksTasksViewModel SelectedTask { get; set; } = new TasksTasksViewModel() { DateAdded = DateTime.Now };

        public int TaskTypeValues { get; set; } = 1;

        protected bool IsEditeTaskType { get; set; } = false;

        protected string ErrorMessage { get; set; }


        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await LoadContext();

                StateHasChanged();
            }
        }

        protected async Task LoadContext()
        {
            await jSRuntime.InvokeAsync<object>("ShowModal", "#AddTypeDialog");
        }

        protected async Task HandleValidAddType()
        {
            try
            {
                bool isValid = true;
                ErrorMessage = null;

                if (string.IsNullOrEmpty(NewTaskType.TaskType))
                {
                    isValid = false;
                    ErrorMessage = "Please Fill The Task Type...";
                    await jSRuntime.InvokeVoidAsync("AddRedBox", "AddTaskTypeId");
                    return;
                }
                else if (TaskTypesAll.Contains(NewTaskType.TaskType))
                {
                    ErrorMessage = ("Error: Task Type Already Exists!");
                    await jSRuntime.InvokeVoidAsync("AddRedBox", "AddTaskTypeId");
                    isValid = false;
                    return;
                }
                else
                {
                    await jSRuntime.InvokeVoidAsync("RemoveRedBox", "AddTaskTypeId");
                    ErrorMessage = null;
                }

                if (string.IsNullOrEmpty(NewTaskType.Email))
                {
                    await jSRuntime.InvokeVoidAsync("AddRedBox", "FirstEmailId");
                    isValid = false;
                    ErrorMessage = "Please Fill The Email...";
                    return;
                }
                else
                {
                    await jSRuntime.InvokeVoidAsync("RemoveRedBox", "FirstEmailId");
                    ErrorMessage = null;
                }

                NewTaskType.Id = 0;
                var _b = await taskService.AddTaskType(NewTaskType);
                NewTaskType.Id = _b;

                await jSRuntime.InvokeAsync<object>("HideModal", "#AddTypeDialog");

                if (NewTaskType.Id != 0)
                {

                    TaskTypesAll.Add(NewTaskType.TaskType);
                    TaskTypesAll = TaskTypesAll.OrderBy(x => x).ToList();

                    TaskTypeValues = 1;
                    TaskTypes = TaskTypesAll.Select(a => new DDData { Text = a, Value = TaskTypeValues++.ToString() }).OrderBy(a => a.Text).ToList();
                    TaskTypes.Insert(0, new DDData { Text = "ALL", Value = "0" });

                    await CallbackMessageTypeDialogComponent.InvokeAsync(new Dictionary<string, string>()
                    {
                        {"Task Type Information", "Task Type Added Successfully" }
                    });
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    logger.LogError(ex.InnerException.Message, "Task Type Add Error");
                }
                else
                {
                    logger.LogError(ex.Message, "Task  Type Add Error");
                }
            }

        }

        protected async Task CloseComponent()
        {
            await jSRuntime.InvokeAsync<object>("HideModal", "#AddTypeDialog");
            await CallbackMessageTypeDialogComponent.InvokeAsync(new Dictionary<string, string>());
        }
    }
}
