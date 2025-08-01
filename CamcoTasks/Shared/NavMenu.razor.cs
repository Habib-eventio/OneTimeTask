using CamcoTasks.Library;
using CamcoTasks.Service.IService;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;

namespace CamcoTasks.Shared
{
    public partial class NavMenu
    {
        protected bool IsClick = false;

        protected List<string> Employees = new List<string>();

        [Inject]
        protected IJSRuntime JSRuntime { get; set; }
        [Inject]
        private IEmployeeService _employeeService { get; set; }
        [Inject]
        private NavigationManager _NavigationManager { get; set; }
        [Inject]
        private AuthenticationStateProvider _AuthenticationStateProvider { get; set; }
        [Inject]
        private ILogingService _loging { get; set; }


        protected void LoadViewTaskByEmployeePopup()
        {
            IsClick = true;
        }

        protected async Task GoBack()
        {
            await JSRuntime.InvokeVoidAsync("history.back");
        }

        protected async Task GoToJarvisAPP()
        {
            var authUser = _AuthenticationStateProvider.GetAuthenticationStateAsync().Result.User;

            if (authUser == null) return;

            string userName = authUser.Claims.FirstOrDefault(c => c.Type == "name")?.Value;

            if (string.IsNullOrEmpty(userName)) return;

            long userId = (await _loging.GetUserByUserNameAsync(userName)).Id;

            var employee = await _employeeService.GetByEmployeeIdAsync(userId);

            if (employee != null)
            {
                _NavigationManager.NavigateTo(AppInformation.jarvisUrl + "checkInAndOut2/" + employee.CustomEmployeeId, true);
            }
        }
    }
}
