using CamcoTasks.Service.IService;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace CamcoTasks.Shared.Component
{
    public partial class EmployeeListComponent : ComponentBase
    {
        protected bool IsLoading = true;

        protected List<string> Employees = new List<string>();

        protected String EmployeeName = string.Empty;

        [Inject]
        protected IJSRuntime JSRuntime { get; set; }
        [Inject]
        protected IEmployeeService EmployeeService { get; set; }

        [Parameter]
        public EventCallback<string> EventCallbackEmployeeListComponent { get; set; }


        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await LoadData();

                IsLoading = false;

                StateHasChanged();
            }
        }

        protected async Task LoadData()
        {
            await JSRuntime.InvokeAsync<object>("ShowModal", "#EmployeeListModel");

            var EmployeesList = await EmployeeService.GetListAsync(true);
            Employees = EmployeesList.Where(x => x.IsActive).Select(a => a.FullName).OrderBy(a => a).ToList();
        }

        protected async Task GoTo()
        {
            await JSRuntime.InvokeAsync<object>("HideModal", "#EmployeeListModel");

            await EventCallbackEmployeeListComponent.InvokeAsync(EmployeeName);
        }

        protected async Task Close()
        {
            await JSRuntime.InvokeAsync<object>("HideModal", "#EmployeeListModel");
            await EventCallbackEmployeeListComponent.InvokeAsync(string.Empty);
        }
    }
}
