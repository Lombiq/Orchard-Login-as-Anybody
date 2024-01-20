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
public class UserSwitcherController(
    IAuthorizationService authorizationService,
    SignInManager<IUser> signInManager,
    UserManager<IUser> userManager,
    INotifier notifier,
    IHtmlLocalizer<UserSwitcherController> htmlLocalizer) : Controller
{
    public async Task<IActionResult> SwitchUser(string id)
    {
        if (!await authorizationService.AuthorizeAsync(User, StandardPermissions.SiteOwner)) return Unauthorized();

        var selectedUser = await userManager.FindByIdAsync(id);

        if (selectedUser == null) return NotFound();

        await signInManager.SignOutAsync();
        await signInManager.SignInAsync(selectedUser, isPersistent: false);

        await notifier.InformationAsync(htmlLocalizer["Successfully logged in as <b>{0}</b>.", selectedUser.UserName]);

        return Redirect("~/");
    }
}
