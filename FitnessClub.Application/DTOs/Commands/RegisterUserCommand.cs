using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessClub.Application.DTOs.Commands
{
    public class RegisterUserCommand
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = default!;

        [Required]
        [Phone]
        public string PhoneNumber { get; set; } = default!;

        [Required]
        [MaxLength(32)]
        public string Password { get; set; } = default!;

        [Required]
        public string FirstName { get; set; } = default!;

        [Required]
        public string Surname { get; set; } = default!;

        [Required]
        public string Patronymic { get; set; } = default!;
    }
}
