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
    public async Task<IActionResult> SwitchUser(string id)
    {
        if (!await _authorizationService.AuthorizeAsync(User, StandardPermissions.SiteOwner)) return Unauthorized();

        var selectedUser = await _userManager.FindByIdAsync(id);

        if (selectedUser == null) return NotFound();

        await _signInManager.SignOutAsync();
        await _signInManager.SignInAsync(selectedUser, isPersistent: false);

        await _notifier.SuccessAsync(H["Successfully logged in as <b>{0}</b>.", selectedUser.UserName]);

        return Redirect("~/");
    }
}
