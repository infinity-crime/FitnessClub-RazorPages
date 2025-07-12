using FitnessClub.Domain.Exceptions;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessClub.Domain.Entities
{
    public class Subscription
    {
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public Guid MembershipPlanId { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public SubscriptionStatus Status { get; private set; }
        public DateTime? LastModifiedDate { get; private set; }

        // Навигационные свойства для удобства запросов EF Core
        public User? User { get; private set; }
        public MembershipPlan? MembershipPlan { get; private set; }

        private Subscription() { }

        private Subscription(Guid id, Guid userId, Guid membershipPlanId, DateTime startDate, 
            DateTime endDate, SubscriptionStatus status)
        {
            Id = id;
            UserId = userId;
            MembershipPlanId = membershipPlanId;
            StartDate = startDate;
            EndDate = endDate;
            Status = status;
            LastModifiedDate = DateTime.UtcNow;
        }

        /// <summary>
        /// Завершает активную подписку, если ее срок закончился
        /// </summary>
        /// <exception cref="DomainException"></exception>
        public void Finish()
        {
            if (Status != SubscriptionStatus.Active)
                throw new DomainException("Невозможно завершить неактивную подписку!");

            if(DateTime.UtcNow <= EndDate)
                throw new DomainException("Срок действия подписки еще не закончился!");

            Status = SubscriptionStatus.Expired;

            LastModifiedDate = DateTime.UtcNow;
        }

        /// <summary>
        /// Отменяет подписку в течении 1 дня после ее активации.
        /// </summary>
        /// <exception cref="DomainException"></exception>
        public void Cancel()
        {
            if (Status != SubscriptionStatus.Active)
                throw new DomainException("Отменить можно только активную подписку!");

            if(StartDate.AddDays(1) < DateTime.UtcNow)
                throw new DomainException("Отменить подписку можно только в течении 1 дня после ее активации");

            Status = SubscriptionStatus.Canceled;

            LastModifiedDate = DateTime.UtcNow;
        }

        /// <summary>
        /// Замораживает подписку на указанный период (максимум 2 недели).
        /// </summary>
        /// <param name="freezeDuration"></param>
        /// <exception cref="DomainException"></exception>
        public void Freeze(TimeSpan freezeDuration)
        {
            if(Status != SubscriptionStatus.Active)
                throw new DomainException("Можно заморозить только активную подписку!");

            if(freezeDuration.Days > 14)
                throw new DomainException("Подписку нельзя заморозить больше чем на 14 дней!");

            if(freezeDuration.TotalSeconds <= 0)
                throw new DomainException("Длительность заморозки должна быть положительной!");

            Status = SubscriptionStatus.Frozen;
            EndDate = EndDate.Add(freezeDuration);

            LastModifiedDate = DateTime.UtcNow;
        }

        /// <summary>
        /// Размораживает замороженную подписку
        /// </summary>
        /// <exception cref="DomainException"></exception>
        public void Unfreeze()
        {
            if(Status != SubscriptionStatus.Frozen)
                throw new DomainException("Незамороженную подписку нельзя разморозить!");

            Status = SubscriptionStatus.Active;
            LastModifiedDate = DateTime.UtcNow;
        }

        public static Subscription Create(User user, MembershipPlan plan)
        {
            if ((user is null) || (plan is null))
                throw new DomainException("Пользователь и план абонемента обязательны при создании подписки!");

            var id = Guid.NewGuid();
            var startDate = DateTime.Now;
            var endDate = startDate.AddMonths(plan.DurationInMonths);

            return new Subscription(id, user.Id, plan.Id, startDate, endDate, SubscriptionStatus.Active);
        }

        public enum SubscriptionStatus
        {
            Active, 
            Frozen, 
            Expired, 
            Canceled 
        }
    }
}
