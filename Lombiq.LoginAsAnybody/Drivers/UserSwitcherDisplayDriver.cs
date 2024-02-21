using Microsoft.AspNetCore.Http;
using OrchardCore.DisplayManagement.Handlers;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.DisplayManagement.Views;
using OrchardCore.Users.Models;
using OrchardCore.Users.ViewModels;

namespace Lombiq.LoginAsAnybody.Drivers;

public class UserSwitcherDisplayDriver : DisplayDriver<User>
{
    private readonly IHttpContextAccessor _hca;

    public UserSwitcherDisplayDriver(IHttpContextAccessor hca) => _hca = hca;

    public override IDisplayResult Display(User model, IUpdateModel updater) =>
        _hca.HttpContext.User.Identity.Name != model.UserName
            ? Initialize<SummaryAdminUserViewModel>("UserSwitcherButton", summaryModel => summaryModel.User = model)
                .Location("SummaryAdmin", "Actions:2")
            : null;
}
