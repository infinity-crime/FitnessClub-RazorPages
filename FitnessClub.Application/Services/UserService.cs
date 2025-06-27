using FitnessClub.Application.Common;
using FitnessClub.Application.DTOs;
using FitnessClub.Application.DTOs.Commands;
using FitnessClub.Application.Interfaces;
using FitnessClub.Domain.Entities;
using FitnessClub.Domain.Exceptions;
using FitnessClub.Domain.Repositories;
using FitnessClub.Domain.Services;
using FitnessClub.Domain.ValueObjects;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessClub.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;

        public UserService (IUserRepository userRepository, IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task<Result<UserDto>> RegisterAsync(RegisterUserCommand command, CancellationToken cancellationToken)
        {
            if (await _userRepository.GetByEmailAsync(command.Email, cancellationToken) is not null)
                return Result<UserDto>.Failure("Email already taken!");

            try
            {
                var fullname = FullName.Create(command.FirstName, command.Surname, command.Patronymic);

                var user = User.Register(command.Email, command.PhoneNumber, command.Password, _passwordHasher, fullname);

                await _userRepository.AddAsync(user, cancellationToken);
                await _userRepository.SaveChangesAsync(cancellationToken);

                var responseDto = new UserDto
                {
                    Id = user.Id,
                    Email = user.Email,
                    FullName = fullname,
                    PhoneNumber = user.PhoneNumber
                };

                return Result<UserDto>.Success(responseDto);
            }
            catch (DomainException ex)
            {
                return Result<UserDto>.Failure(ex.Message);
            }
        }

        public async Task<Result<UserDto>> LoginAsync(LoginUserCommand command, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmailAsync(command.Email, cancellationToken);
            if (user is null || !user.VerifyPassword(command.Password, _passwordHasher))
                return Result<UserDto>.Failure("Invalid credentials!");

            var responseDto = new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                FullName = user.FullName,
                PhoneNumber = user.PhoneNumber
            };

            return Result<UserDto>.Success(responseDto);
        }
    }
}
