using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using OrchardCore.Admin;
using OrchardCore.DisplayManagement.Notify;
using OrchardCore.Security;
using OrchardCore.Users;
using System.Threading.Tasks;

namespace Lombiq.LoginAsAnybody.Controllers;

[Admin]
public class UserSwitcherController : Controller
{
    private readonly IAuthorizationService _authorizationService;
    private readonly SignInManager<IUser> _signInManager;
    private readonly UserManager<IUser> _userManager;
    private readonly INotifier _notifier;
    private readonly IHtmlLocalizer<UserSwitcherController> H;

    public UserSwitcherController(
        IAuthorizationService authorizationService,
        SignInManager<IUser> signInManager,
        UserManager<IUser> userManager,
        INotifier notifier,
        IHtmlLocalizer<UserSwitcherController> htmlLocalizer)
    {
        _authorizationService = authorizationService;
        _signInManager = signInManager;
        _userManager = userManager;
        _notifier = notifier;
        H = htmlLocalizer;
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> SwitchUser(string userId, string returnUrl = null)
    {
        if (!await _authorizationService.AuthorizeAsync(User, StandardPermissions.SiteOwner)) return Unauthorized();

        var selectedUser = await _userManager.FindByIdAsync(userId);

        if (selectedUser == null) return NotFound();

        await _signInManager.SignOutAsync();
        await _signInManager.SignInAsync(selectedUser, isPersistent: false);

        if (string.IsNullOrEmpty(returnUrl)) returnUrl = "~/";

        await _notifier.InformationAsync(H["Successfully logged in as {0}.", selectedUser.UserName]);

        return Redirect(returnUrl);
    }
}