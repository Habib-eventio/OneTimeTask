using CamcoTasks.Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SetupApp.Web.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly UserManager<User> _userManager;

        public RegisterModel(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        [BindProperty]
        public RegisterInputModel Input { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = new User
            {
                UserName = Input.UserName,
                Email = Input.Email,
                PhoneNumber = Input.PhoneNumber
            };

            var result = await _userManager.CreateAsync(user, Input.Password);

            if (result.Succeeded)
            {
                TempData["PrefillEmail"] = Input.Email;
                TempData["PrefillPassword"] = Input.Password;
                TempData["ShowWelcome"] = true;

                return Redirect("/Account/Login");
            }

            foreach (var error in result.Errors)
                ModelState.AddModelError(string.Empty, error.Description);

            return Page();
        }
    }

    public class RegisterInputModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
