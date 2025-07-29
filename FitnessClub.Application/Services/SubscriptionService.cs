using FitnessClub.Application.Common;
using FitnessClub.Application.DTOs;
using FitnessClub.Application.DTOs.Commands;
using FitnessClub.Application.Interfaces;
using FitnessClub.Application.Mappings;
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

        public async Task<Result<SubscriptionDto>> GetCurrentUserSubscriptionAsync(Guid userId, CancellationToken cancellationToken)
        {
            var result = await _subscriptionRepository.GetCurrentForUserAsync(userId, cancellationToken);
            if (result == null)
                return Result<SubscriptionDto>.Failure("Нет текущих абонементов");

            var resultDto = SubscriptionMapper.MapToDto(result);

            return Result<SubscriptionDto>.Success(resultDto);
        }

        public async Task<Result<IEnumerable<SubscriptionDto>>> GetUserSubscriptionHistoryAsync(Guid userId, CancellationToken cancellationToken)
        {
            var result = await _subscriptionRepository.GetHistoryForUserAsync(userId, cancellationToken);
            if (result == null)
                return Result<IEnumerable<SubscriptionDto>>.Failure("История абонементов пуста");

            var resultDto = result.Select(s => SubscriptionMapper.MapToDto(s));

            return Result<IEnumerable<SubscriptionDto>>.Success(resultDto);
        }

        public async Task<Result<SubscriptionDto>> PurchaseMembershipAsync(PurchaseMembershipCommand command, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(command.UserId, cancellationToken);

            var membershipPlan = await _membershipPlanRepository.GetByIdAsync(command.PlanId, cancellationToken);

            try
            {
                var sub = Subscription.Create(user!, membershipPlan!);
                await _subscriptionRepository.AddAsync(sub, cancellationToken);

                await _unitOfWork.SaveChangesAsync(cancellationToken);

                SubscriptionDto response = SubscriptionMapper.MapToDto(sub);

                return Result<SubscriptionDto>.Success(response);
            }
            catch (DomainException ex)
            {
                return Result<SubscriptionDto>.Failure(ex.Message);
            }
        }

        public Task<Result<SubscriptionDto>> CancelSubscriptionAsync(Guid subscriptionId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<SubscriptionDto>> FreezeSubscriptionAsync(Guid userId, int freezeDays, CancellationToken cancellationToken)
        {
            var sub = await _subscriptionRepository.GetCurrentForUserAsync(userId, cancellationToken);
            if (sub!.Status == Subscription.SubscriptionStatus.Frozen)
                return Result<SubscriptionDto>.Failure($"Subscription status {sub.Status}");

            try
            {
                sub.Freeze(TimeSpan.FromDays(freezeDays));
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                SubscriptionDto response = SubscriptionMapper.MapToDto(sub);

                return Result<SubscriptionDto>.Success(response);
            }
            catch (DomainException ex)
            {
                return Result<SubscriptionDto>.Failure(ex.Message);
            }
        }
    }
}