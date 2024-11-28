using Lombiq.LoginAsAnybody.Drivers;
using Lombiq.LoginAsAnybody.Permissions;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.DisplayManagement.Handlers;
using OrchardCore.Modules;
using OrchardCore.Security.Permissions;
using OrchardCore.Users.Models;

namespace Lombiq.LoginAsAnybody;

public sealed class Startup : StartupBase
{
    public override void ConfigureServices(IServiceCollection services)
    {
        services.AddScoped<IDisplayDriver<User>, UserSwitcherDisplayDriver>();
        services.AddPermissionProvider<LoginAsAnybodyPermissions>();
    }
}
