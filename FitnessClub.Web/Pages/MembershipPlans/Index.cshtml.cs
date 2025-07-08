using FitnessClub.Application.DTOs;
using FitnessClub.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FitnessClub.Web.Pages.MembershipPlans
{
    public class IndexModel : PageModel
    {
        private readonly IMembershipPlanService _membershipPlanService;

        public IndexModel(IMembershipPlanService membershipPlanService)
        {
            _membershipPlanService = membershipPlanService;
        }

        public IEnumerable<MembershipPlanDto> Plans { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var result = await _membershipPlanService.AllPlansAsync(HttpContext.RequestAborted);
            if(!result.IsSuccess)
            {
                ModelState.AddModelError(string.Empty, result.Error!);
                return Page();
            }

            Plans = result.Value!;
            return Page();
        }
    }
}
