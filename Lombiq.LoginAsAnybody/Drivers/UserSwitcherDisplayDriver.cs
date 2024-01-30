using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using OrchardCore.DisplayManagement.Handlers;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.DisplayManagement.Views;
using OrchardCore.Security;
using OrchardCore.Users.Models;
using OrchardCore.Users.ViewModels;
using System.Threading.Tasks;

namespace Lombiq.LoginAsAnybody.Drivers;

public class UserSwitcherDisplayDriver : DisplayDriver<User>
{
    private readonly IHttpContextAccessor _hca;
    private readonly IAuthorizationService _authorizationService;

    public UserSwitcherDisplayDriver(IHttpContextAccessor hca, IAuthorizationService authorizationService)
    {
        _hca = hca;
        _authorizationService = authorizationService;
    }

    public override async Task<IDisplayResult> DisplayAsync(User model, IUpdateModel updater) =>
        await _authorizationService.AuthorizeAsync(_hca.HttpContext.User, StandardPermissions.SiteOwner) &&
            _hca.HttpContext.User.Identity.Name != model.UserName
                ? Initialize<SummaryAdminUserViewModel>("UserSwitcherButton", summaryModel => summaryModel.User = model)
                    .Location("SummaryAdmin", "Actions:2")
                : null;
}
