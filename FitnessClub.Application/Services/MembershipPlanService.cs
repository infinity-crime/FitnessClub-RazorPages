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
        private readonly IUnitOfWork _unitOfWork;

        public MembershipPlanService(IMembershipPlanRepository membershipPlanRepository, IUnitOfWork unitOfWork)
        {
            _membershipPlanRepository = membershipPlanRepository;
            _unitOfWork = unitOfWork;
        }

        public Task<Result<IEnumerable<MembershipPlanDto>>> AllPlansAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Result<MembershipPlanDto>> PlanByIdAsync()
        {
            throw new NotImplementedException();
        }
    }
}
