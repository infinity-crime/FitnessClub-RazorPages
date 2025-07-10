using FitnessClub.Application.Common;
using FitnessClub.Application.DTOs;
using FitnessClub.Application.DTOs.Commands;
using FitnessClub.Application.Interfaces;
using FitnessClub.Domain.Entities;
using FitnessClub.Domain.Exceptions;
using FitnessClub.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessClub.Application.Services
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMembershipPlanRepository _membershipPlanRepository;
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly IUnitOfWork _unitOfWork;

        public SubscriptionService(IUserRepository userRepository, IMembershipPlanRepository membershipPlanRepository,
            ISubscriptionRepository subscriptionRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _membershipPlanRepository = membershipPlanRepository;
            _subscriptionRepository = subscriptionRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<SubscriptionDto>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken)
        {
            var sub = await _subscriptionRepository.GetByUserIdAsync(userId, cancellationToken);
            if (sub == null)
                return Result<SubscriptionDto>.Failure("Список абонементов пуст!");

            var response = new SubscriptionDto
            {
                Id = sub.Id,
                UserId = sub.UserId,
                MembershipPlanId = sub.MembershipPlanId,
                MembershipPlanName = sub.MembershipPlan!.Name,
                StartDate = sub.StartDate,
                EndDate = sub.EndDate,
                Status = sub.Status,
                LastModifiedDate = sub.LastModifiedDate
            };

            return Result<SubscriptionDto>.Success(response);
        }

        public async Task<Result<SubscriptionDto>> PurchaseMembershipAsync(PurchaseMembershipCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userRepository.GetByIdAsync(command.UserId, cancellationToken);

                var membershipPlan = await _membershipPlanRepository.GetByIdAsync(command.PlanId, cancellationToken);

                var sub = Subscription.Create(user!, membershipPlan!);
                await _subscriptionRepository.AddAsync(sub, cancellationToken);

                await _unitOfWork.SaveChangesAsync(cancellationToken);

                SubscriptionDto response = new SubscriptionDto
                {
                    Id = sub.Id,
                    UserId = sub.UserId,
                    MembershipPlanId = sub.MembershipPlanId,
                    StartDate = sub.StartDate,
                    EndDate = sub.EndDate,
                    Status = sub.Status,
                    LastModifiedDate = sub.LastModifiedDate
                };

                return Result<SubscriptionDto>.Success(response);
            }
            catch(DomainException ex)
            {
                return Result<SubscriptionDto>.Failure(ex.Message);
            }
        }
    }
}
