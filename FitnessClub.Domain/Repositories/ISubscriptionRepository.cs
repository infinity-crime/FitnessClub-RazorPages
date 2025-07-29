using FitnessClub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessClub.Domain.Repositories
{
    public interface ISubscriptionRepository
    {
        /// <summary>
        /// Получает единственную текущую подписку пользователя (активную или замороженную).
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Subscription?</returns>
        Task<Subscription?> GetCurrentForUserAsync(Guid userId, CancellationToken cancellationToken);

        /// <summary>
        /// Получает всю историю подписок пользователя за все время, отсортированную по дате покупки (убывание).
        /// Не рекомендуется изменение подписок, полученных этим методом, так как отслеживание сущностей не используется.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IEnumerable<Subscription>?> GetHistoryForUserAsync(Guid userId, CancellationToken cancellationToken);

        /// <summary>
        /// Добавляет подписку для пользователя.
        /// </summary>
        /// <param name="subscription"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task AddAsync(Subscription subscription, CancellationToken cancellationToken);
    }
}
