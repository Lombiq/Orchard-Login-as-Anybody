using OrchardCore.DisplayManagement.Handlers;
using OrchardCore.DisplayManagement.Views;
using OrchardCore.Users.Models;
using OrchardCore.Users.ViewModels;

namespace Lombiq.LoginAsAnybody.Drivers;

public class ImpersonationDisplayDriver : DisplayDriver<User>
{
    public override IDisplayResult Display(User model)
        => Initialize<SummaryAdminUserViewModel>("ImpersonationButton", summaryModel => summaryModel.User = model)
            .Location("SummaryAdmin", "Actions:2");
}
