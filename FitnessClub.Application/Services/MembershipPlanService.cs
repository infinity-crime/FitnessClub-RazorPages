using FitnessClub.Application.Common;
using FitnessClub.Application.DTOs;
using FitnessClub.Application.Interfaces;
using FitnessClub.Domain.Repositories;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessClub.Application.Services
{
    public class MembershipPlanService : IMembershipPlanService
    {
        private readonly IMembershipPlanRepository _membershipPlanRepository;

        public MembershipPlanService(IMembershipPlanRepository membershipPlanRepository, IUnitOfWork unitOfWork)
        {
            _membershipPlanRepository = membershipPlanRepository;
        }

        public async Task<Result<IEnumerable<MembershipPlanDto>>> AllPlansAsync(CancellationToken cancellationToken)
        {
            var plans = await _membershipPlanRepository.GetAllAsync(cancellationToken);
            if(plans != null)
            {
                var plansDto = plans.Select(x => new MembershipPlanDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Price = x.Price,
                    DurationInMonths = x.DurationInMonths
                });

                return Result<IEnumerable<MembershipPlanDto>>.Success(plansDto);
            }

            return Result<IEnumerable<MembershipPlanDto>>.Failure("Нет доступных абонементов");
        }

        public async Task<Result<MembershipPlanDto>> PlanByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var plan = await _membershipPlanRepository.GetByIdAsync(id, cancellationToken);
            if (plan == null)
                return Result<MembershipPlanDto>.Failure("Такого абонемента нет");

            var response = new MembershipPlanDto
            {
                Id = plan.Id,
                Name = plan.Name,
                Description = plan.Description,
                Price = plan.Price,
                DurationInMonths = plan.DurationInMonths
            };

            return Result<MembershipPlanDto>.Success(response);
        }
    }
}
