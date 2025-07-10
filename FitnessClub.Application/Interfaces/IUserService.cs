using FitnessClub.Application.Common;
using FitnessClub.Application.DTOs;
using FitnessClub.Application.DTOs.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessClub.Application.Interfaces
{
    public interface IUserService
    {
        Task<Result<UserDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<Result<UserDto>> RegisterAsync(RegisterUserCommand command, CancellationToken cancellationToken);
        Task<Result<UserDto>> LoginAsync(LoginUserCommand command, CancellationToken cancellationToken);
    }
}
