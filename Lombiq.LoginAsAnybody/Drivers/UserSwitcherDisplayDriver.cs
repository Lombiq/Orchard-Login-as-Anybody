using Lombiq.LoginAsAnybody.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using OrchardCore.DisplayManagement.Handlers;
using OrchardCore.DisplayManagement.Views;
using OrchardCore.Users.Models;
using OrchardCore.Users.ViewModels;
using System.Threading.Tasks;

namespace Lombiq.LoginAsAnybody.Drivers;

public sealed class UserSwitcherDisplayDriver : DisplayDriver<User>
{
    private readonly IHttpContextAccessor _hca;
    private readonly IAuthorizationService _authorizationService;

    public UserSwitcherDisplayDriver(IHttpContextAccessor hca, IAuthorizationService authorizationService)
    {
        _hca = hca;
        _authorizationService = authorizationService;
    }

    public override async Task<IDisplayResult> DisplayAsync(User model, BuildDisplayContext context) =>
        _hca.HttpContext?.User != null &&
        _hca.HttpContext.User.Identity.Name != model.UserName &&
        await _authorizationService.AuthorizeAsync(_hca.HttpContext.User, LoginAsAnybodyPermissions.LoginAsAnybody)
            ? Initialize<SummaryAdminUserViewModel>("UserSwitcherButton", summaryModel => summaryModel.User = model)
                .Location("SummaryAdmin", "Actions:2")
            : null;
}
