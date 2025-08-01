using CamcoTasks.Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading.Tasks;

namespace SetupApp.Web.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;

        public LoginModel(SignInManager<User> signInManager, UserManager<User> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [BindProperty]
        public Credential credential { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            var user = await _userManager.FindByEmailAsync(credential.Email);

            if (user == null)
            {
                ModelState.AddModelError("credential.Email", "Email address not registered.");
                return Page();
            }

            var result = await _signInManager.PasswordSignInAsync(
                user.UserName,
                credential.Password,
                credential.RememberMe,
                lockoutOnFailure: false);

            if (result.Succeeded)
            {
                return Redirect("/");
            }

            ModelState.AddModelError("credential.Password", "Incorrect password.");
            return Page();
        }


        public class Credential
        {
            [Required]
            [BindProperty]
            [EmailAddress] // Add email validation
            [Display(Name = "Email Address")]
            public string Email { get; set; }

            [Required]
            [BindProperty]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [BindProperty]
            [Display(Name = "Remember Me!")]
            public bool RememberMe { get; set; }
        }
    }
}