using FitnessClub.Application.DTOs;
using FitnessClub.Application.DTOs.Commands;
using FitnessClub.Application.Interfaces;
using FitnessClub.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace FitnessClub.Web.Pages.MembershipPlans
{
    public class IndexModel : PageModel
    {
        private readonly IMembershipPlanService _membershipPlanService;
        private readonly ISubscriptionService _subscriptionService;

        public IndexModel(IMembershipPlanService membershipPlanService, ISubscriptionService subscriptionService)
        {
            _membershipPlanService = membershipPlanService;
            _subscriptionService = subscriptionService;
        }

        public IEnumerable<MembershipPlanDto> Plans { get; set; } = new List<MembershipPlanDto>();

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

        public async Task<IActionResult> OnPostPurchaseAsync(Guid planId)
        {
            if (User.Identity?.IsAuthenticated == false)
                return RedirectToPage("/Account/Login");

            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var existing = await _subscriptionService.GetCurrentUserSubscriptionAsync(userId, HttpContext.RequestAborted);
            if(existing.IsSuccess)
            {
                TempData["Error"] = "У вас уже есть активный/замороженный абонемент!";
                return RedirectToPage();
            }
            
            var command = new PurchaseMembershipCommand { UserId = userId, PlanId = planId };

            var result = await _subscriptionService.PurchaseMembershipAsync(command, HttpContext.RequestAborted);
            if (!result.IsSuccess)
                TempData["Error"] = result.Error;
            else
                TempData["Success"] = "Абонемент успешно оформлен!";

            return RedirectToPage();
        }
    }
}
