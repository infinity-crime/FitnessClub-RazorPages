using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessClub.Application.DTOs.Commands
{
    public class LoginUserCommand
    {
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
    }
}
