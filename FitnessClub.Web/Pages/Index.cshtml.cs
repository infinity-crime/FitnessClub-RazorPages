using FitnessClub.Application.DTOs.Commands;
using FitnessClub.Application.Interfaces;
using FitnessClub.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FitnessClub.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IUserService _userService;

        public IndexModel(IUserService userService) => _userService = userService;

        [BindProperty]
        public RegisterUserCommand Command { get; set; }

        public string? Message { get; private set; }

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            var result = await _userService.RegisterAsync(Command, HttpContext.RequestAborted);
            Message = result.IsSuccess ? "Registration successful!" : result.Error;
            return Page();
        }
    }
}
