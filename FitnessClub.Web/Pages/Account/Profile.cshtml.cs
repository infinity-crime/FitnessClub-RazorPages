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
        public SubscriptionDto? ActiveSubscriptions { get; set; }

        public ProfileModel(IUserService userService, ISubscriptionService subscriptionService)
        {
            _userService = userService;
            _subscriptionService = subscriptionService;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
            {
                ModelState.AddModelError(string.Empty, "Авторизируйтесь заново!");
                return Page();
            }

            var userResult = await _userService.GetByIdAsync(userId, HttpContext.RequestAborted);
            if (!userResult.IsSuccess)
                return RedirectToPage("/Account/Login");

            UserDto = userResult.Value!;

            var subs = await _subscriptionService.GetByUserIdAsync(userId, HttpContext.RequestAborted);
            if(!subs.IsSuccess)
            {
                ModelState.AddModelError(string.Empty, subs.Error!);
                return Page();
            }

            ActiveSubscriptions = subs.Value!;

            return Page();
        }
    }
}
