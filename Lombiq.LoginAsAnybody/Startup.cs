using Lombiq.LoginAsAnybody.Drivers;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.DisplayManagement.Handlers;
using OrchardCore.Modules;
using OrchardCore.Users.Models;

namespace Lombiq.LoginAsAnybody;

public class Startup : StartupBase
{
    public override void ConfigureServices(IServiceCollection services) =>
        services.AddScoped<IDisplayDriver<User>, ImpersonationDisplayDriver>();
}
