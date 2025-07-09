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
        Task<Result<SubscriptionDto>> PurchaseMembershipAsync(PurchaseMembershipCommand command, CancellationToken cancellationToken);
    }
}
