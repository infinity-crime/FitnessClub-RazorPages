using FitnessClub.Application.DTOs.Commands;
using FitnessClub.Application.Interfaces;
using FitnessClub.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Globalization;

namespace FitnessClub.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IUserService _userService;

        public IndexModel(IUserService userService) => _userService = userService;

        [BindProperty]
        public LoginUserCommand LoginCommand { get; set; }

        public string? LoginMessage { get; private set; }

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            var result = await _userService.LoginAsync(LoginCommand, HttpContext.RequestAborted);
            LoginMessage = result.IsSuccess ? "Log in successful!" : result.Error;
            return Page();
        }       
    }
}
