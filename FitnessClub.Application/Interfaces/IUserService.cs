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
        /// <summary>
        /// Возвращает результат поиска пользователя по его id.
        /// В случае неудачи возвращает результат ошибки с сообщением.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Result<UserDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Регистрирует пользователя на сайте.
        /// В случае неправильно введенных данных, возвращает результат ошибки с сообщением.
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Result<UserDto>> RegisterAsync(RegisterUserCommand command, CancellationToken cancellationToken);

        /// <summary>
        /// Проверяет наличие данного пользователя в системе БД.
        /// В случае неудачи возвращает результат ошибки с сообщением.
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Result<UserDto>> LoginAsync(LoginUserCommand command, CancellationToken cancellationToken);
    }
}
