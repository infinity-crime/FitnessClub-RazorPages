using FitnessClub.Application.DTOs.Commands;
using FitnessClub.Application.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace FitnessClub.Web.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly IUserService _userService;

        public LoginModel(IUserService userService)
        {
            _userService = userService;
        }

        [BindProperty]
        public UserInputModel Input { get; set; }

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            var command = new LoginUserCommand { Email = Input.Email, Password = Input.Password };

            var result = await _userService.LoginAsync(command, HttpContext.RequestAborted);
            if(!result.IsSuccess)
            {
                ModelState.AddModelError(string.Empty, result.Error!);
                return Page();
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, result.Value!.Id.ToString()),
                new Claim(ClaimTypes.Email, result.Value.Email.Value),
                new Claim(ClaimTypes.Name, result.Value.FullName.Name)
            };

            var claimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimIdentity));

            return RedirectToPage("/Index");
        }

        public class UserInputModel
        {
            [Required(ErrorMessage = "Email обязателен для входа!")]
            [EmailAddress(ErrorMessage = "Введенный email некорректен!")]
            public string Email { get; set; }

            [Required(ErrorMessage = "Пароль обязателен для входа!")]
            [DataType(DataType.Password)]
            public string Password { get; set; }
        }
    }
}
