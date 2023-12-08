using OrchardCore.DisplayManagement.Handlers;
using OrchardCore.DisplayManagement.Views;
using OrchardCore.Users.Models;
using OrchardCore.Users.ViewModels;

namespace Lombiq.LoginAsAnybody.Drivers;

public class UserSwitcherDisplayDriver : DisplayDriver<User>
{
    public override IDisplayResult Display(User model)
        => Initialize<SummaryAdminUserViewModel>("UserSwitcherButton", summaryModel => summaryModel.User = model)
            .Location("SummaryAdmin", "Actions:2");
}
