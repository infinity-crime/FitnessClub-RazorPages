using FitnessClub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessClub.Domain.Repositories
{
    public interface IUserRepository
    {
        /// <summary>
        /// Получает пользователя по его id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Получает пользователя по его email.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken);

        /// <summary>
        /// Добавляет нового пользователя в БД.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task AddAsync(User user, CancellationToken cancellationToken);
    }
}
