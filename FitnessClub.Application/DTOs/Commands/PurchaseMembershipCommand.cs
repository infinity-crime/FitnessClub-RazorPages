using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessClub.Application.DTOs.Commands
{
    public class PurchaseMembershipCommand
    {
        public Guid UserId { get; set; }
        public Guid PlanId { get; set; }
    }
}
