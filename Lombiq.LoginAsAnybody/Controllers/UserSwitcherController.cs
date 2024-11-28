using Lombiq.LoginAsAnybody.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.Extensions.Logging;
using OrchardCore.Admin;
using OrchardCore.DisplayManagement.Notify;
using OrchardCore.Users;
using System.Threading.Tasks;

namespace Lombiq.LoginAsAnybody.Controllers;

public sealed class UserSwitcherController : Controller
{
    private readonly IAuthorizationService _authorizationService;
    private readonly SignInManager<IUser> _signInManager;
    private readonly UserManager<IUser> _userManager;
    private readonly INotifier _notifier;
    private readonly IHtmlLocalizer<UserSwitcherController> H;
    private readonly ILogger _logger;

    public UserSwitcherController(
        IAuthorizationService authorizationService,
        SignInManager<IUser> signInManager,
        UserManager<IUser> userManager,
        INotifier notifier,
        IHtmlLocalizer<UserSwitcherController> htmlLocalizer,
        ILogger<UserSwitcherController> logger)
    {
        _authorizationService = authorizationService;
        _signInManager = signInManager;
        _userManager = userManager;
        _notifier = notifier;
        _logger = logger;
        H = htmlLocalizer;
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Admin("Users/SwitchUser/{id}", "UserSwitcher")]
    public async Task<IActionResult> SwitchUser(string id)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        if (!await _authorizationService.AuthorizeAsync(User, LoginAsAnybodyPermissions.LoginAsAnybody)) return Unauthorized();

        var selectedUser = await _userManager.FindByIdAsync(id);

        if (selectedUser == null) return NotFound();

        await _signInManager.SignOutAsync();
        await _signInManager.SignInAsync(selectedUser, isPersistent: false);

        await _notifier.SuccessAsync(H["Successfully logged in as <b>{0}</b>.", selectedUser.UserName]);

        _logger.LogInformation("User {UserName} logged in asr {SelectedUserName}.", User.Identity.Name, selectedUser.UserName);

        return Redirect("~/");
    }
}
