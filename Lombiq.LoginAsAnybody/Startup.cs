using Lombiq.LoginAsAnybody.Constants;
using Lombiq.LoginAsAnybody.Controllers;
using Lombiq.LoginAsAnybody.Drivers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.DisplayManagement.Handlers;
using OrchardCore.Modules;
using OrchardCore.Mvc.Core.Utilities;
using OrchardCore.Users.Models;
using System;

namespace Lombiq.LoginAsAnybody;

public class Startup : StartupBase
{
    public override void ConfigureServices(IServiceCollection services) =>
        services.AddScoped<IDisplayDriver<User>, UserSwitcherDisplayDriver>();

    public override void Configure(
        IApplicationBuilder app,
        IEndpointRouteBuilder routes,
        IServiceProvider serviceProvider) =>
            routes.MapAreaControllerRoute(
                name: "UserSwitcher",
                areaName: FeatureIds.LoginAsAnybody,
                pattern: "UserSwitcher/SwitchUser",
                defaults: new
                {
                    controller = typeof(UserSwitcherController).ControllerName(),
                    action = nameof(UserSwitcherController.SwitchUser),
                }
            );
}
