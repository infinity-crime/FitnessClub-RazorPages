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
    public interface ISubscriptionService
    {
        /// <summary>
        /// Получает список IEnumerable подписок пользователя, которые закончились или отменены (история).
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Result<IEnumerable<SubscriptionDto>>> GetUserSubscriptionHistoryAsync(Guid userId, CancellationToken cancellationToken);

        /// <summary>
        /// Возвращает результат активной или замороженной подписки пользователя, которая действует в данный момент.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Result<SubscriptionDto>> GetCurrentUserSubscriptionAsync(Guid userId, CancellationToken cancellationToken);


        /// <summary>
        /// Подписывает пользователя на выбранный абонемент.
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Result<SubscriptionDto>> PurchaseMembershipAsync(PurchaseMembershipCommand command, CancellationToken cancellationToken);

        /// <summary>
        /// Замораживает активную подписку у пользователя на выбранное кол-во дней (от 1 до 14).
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="freezeDays"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Result<SubscriptionDto>> FreezeSubscriptionAsync(Guid userId, int freezeDays, CancellationToken cancellationToken);

        /// <summary>
        /// Отменяет активную подписку пользователя.
        /// </summary>
        /// <param name="subscriptionId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Result<SubscriptionDto>> CancelSubscriptionAsync(Guid subscriptionId, CancellationToken cancellationToken);
    }
}
