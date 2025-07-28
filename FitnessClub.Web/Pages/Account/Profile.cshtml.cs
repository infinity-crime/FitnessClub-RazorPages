using FitnessClub.Application.DTOs;
using FitnessClub.Application.Interfaces;
using FitnessClub.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace FitnessClub.Web.Pages.Account
{
    [Authorize]
    public class ProfileModel : PageModel
    {
        private readonly IUserService _userService;
        private readonly ISubscriptionService _subscriptionService;

        public UserDto UserDto { get; set; } = default!;
        public SubscriptionDto? CurrentSubscription { get; set; }
        public List<SubscriptionDto> AllSubscriptionsHistory { get; set; } = new();

        public ProfileModel(IUserService userService, ISubscriptionService subscriptionService)
        {
            _userService = userService;
            _subscriptionService = subscriptionService;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var userId = GetUserIdFromClaim();
            if (userId == null)
                return Page();

            var userResult = await _userService.GetByIdAsync(userId.Value, HttpContext.RequestAborted);
            if (!userResult.IsSuccess)
                return RedirectToPage("/Account/Login");

            UserDto = userResult.Value!;

            var sub = await _subscriptionService.GetCurrentUserSubscriptionAsync(userId.Value, HttpContext.RequestAborted);
            if(!sub.IsSuccess)
            {
                ModelState.AddModelError(string.Empty, sub.Error!);
                return Page();
            }

            CurrentSubscription = sub.Value!;

            return Page();
        }

        public async Task<IActionResult> OnPostFreezeSubscriptionAsync(int freezeDays)
        {
            var userId = GetUserIdFromClaim();
            if (userId == null)
                return Page();

            var userResult = await _userService.GetByIdAsync(userId.Value, HttpContext.RequestAborted);
            if (!userResult.IsSuccess)
                return RedirectToPage("/Account/Login");

            var sub = await _subscriptionService.FreezeSubscriptionAsync(userResult.Value!.Id, freezeDays, HttpContext.RequestAborted);
            if (!sub.IsSuccess)
            {
                ModelState.AddModelError(string.Empty, sub.Error!);
                return Page();
            }

            CurrentSubscription = sub.Value!;

            return RedirectToPage();
        }

        private Guid? GetUserIdFromClaim()
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
            {
                ModelState.AddModelError(string.Empty, "Авторизируйтесь заново!");
                return null;
            }

            return userId;
        }
    }
}
