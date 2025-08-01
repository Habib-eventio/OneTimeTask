// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using CamcoTasks.Service.IService;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CamcoTasks.Areas.Identity.Pages.Account
{
    public class LogoutModel : PageModel
    {
        private ILoginLogsService _loginLogsService;
        private ILogger<LogoutModel> _logger;

        public LogoutModel(ILoginLogsService loginLogsService, ILogger<LogoutModel> logger)
        {
            _loginLogsService = loginLogsService;
            _logger = logger;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                var currentLoginId = User.Claims.FirstOrDefault(a => a.Type == "sub")?.Value;
                var currentEmployeeId = User.Claims.FirstOrDefault(a => a.Type == "employeeId")?.Value;

                if (string.IsNullOrEmpty(currentEmployeeId))
                {
                    var loginLog = await _loginLogsService.GetAsync(11, Convert.ToInt64(currentEmployeeId));

                    if (loginLog != null)
                    {
                        loginLog.SignedOutTime = DateTime.Now;
                        await _loginLogsService.UpdateAsync(loginLog);
                    }
                } 
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "LogOut error:", ex);
            }

            return SignOut(new AuthenticationProperties
            {
                RedirectUri = "/"
            }, "CAMCO.ClientTasks", "oidc");
        }
    }
}
