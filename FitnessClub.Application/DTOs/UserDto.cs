using FitnessClub.Domain.ValueObjects;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessClub.Application.DTOs
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public Email Email { get; set; } = default!;
        public FullName FullName { get; set; } = default!;
        public PhoneNumber PhoneNumber { get; set; } = default!;
    }
}
