using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessClub.Application.DTOs.Commands
{
    public class RegisterUserCommand
    {
        public string Email { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string FirstName { get; set; } = default!;
        public string Surname { get; set; } = default!;
        public string Patronymic { get; set; } = default!;
    }
}
