using FitnessClub.Application.DTOs.Commands;
using FitnessClub.Application.Interfaces;
using FitnessClub.Application.Services;
using FitnessClub.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Globalization;
using System.Security.Claims;

namespace FitnessClub.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ISubscriptionService _subscriptionService;

        public IndexModel(ISubscriptionService subscriptionService)
        {
            _subscriptionService = subscriptionService;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            if(User.Identity!.IsAuthenticated)
            {
                var claim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if(Guid.TryParse(claim, out var userId))
                {
                    var sub = await _subscriptionService.GetCurrentUserSubscriptionAsync(userId, HttpContext.RequestAborted);
                    if (sub.IsSuccess)
                    {
                        var daysLeft = (sub.Value!.EndDate.Date - DateTime.UtcNow.Date).Days;
                        ViewData["DaysLeft"] = daysLeft;
                    }
                }
            }
            return Page();
        }
    }
}
