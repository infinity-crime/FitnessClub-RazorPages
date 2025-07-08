using FitnessClub.Application.DTOs.Commands;
using FitnessClub.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace FitnessClub.Web.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly IUserService _userService;

        public RegisterModel(IUserService userService)
        {
            _userService = userService;
        }

        [BindProperty]
        public UserInputModel Input { get; set; }

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            var command = new RegisterUserCommand
            {
                Email = Input.Email,
                PhoneNumber = Input.PhoneNumber,
                Password = Input.Password,
                FirstName = Input.FirstName,
                Surname = Input.Surname,
                Patronymic = Input.Patronymic
            };

            var result = await _userService.RegisterAsync(command, HttpContext.RequestAborted);
            if(!result.IsSuccess)
            {
                ModelState.AddModelError(string.Empty, result.Error!);
                return Page();
            }

            return RedirectToPage("/Index");
        }

        public class UserInputModel
        {
            [Required(ErrorMessage = "Email обязателен для регистрации!")]
            [EmailAddress(ErrorMessage = "Введенный email некорректен!")]
            public string Email { get; set; } = default!;

            [Required(ErrorMessage = "Номер телефона обязателен для регистрации!")]
            [Phone(ErrorMessage = "Номер телефона некорректен")]
            public string PhoneNumber { get; set; } = default!;

            [Required(ErrorMessage = "Введите пароль")]
            [MinLength(8, ErrorMessage = "Минимальная длина - 8 символов")]
            public string Password { get; set; } = default!;

            [Required(ErrorMessage = "Введите имя")]
            public string FirstName { get; set; } = default!;

            [Required(ErrorMessage = "Введите фамилию")]
            public string Surname { get; set; } = default!;

            [Required(ErrorMessage = "Введите отчество")]
            public string Patronymic { get; set; } = default!;
        }
    }
}
