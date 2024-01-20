using Lombiq.LoginAsAnybody.Constants;
using Lombiq.LoginAsAnybody.Controllers;
using Lombiq.LoginAsAnybody.Drivers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using OrchardCore.Admin;
using OrchardCore.DisplayManagement.Handlers;
using OrchardCore.Modules;
using OrchardCore.Mvc.Core.Utilities;
using OrchardCore.Users.Models;
using System;

namespace Lombiq.LoginAsAnybody;

public class Startup(IOptions<AdminOptions> adminOptions) : StartupBase
{
    private readonly AdminOptions _adminOptions = adminOptions.Value;

    public override void ConfigureServices(IServiceCollection services) =>
        services.AddScoped<IDisplayDriver<User>, UserSwitcherDisplayDriver>();

    public override void Configure(
        IApplicationBuilder app,
        IEndpointRouteBuilder routes,
        IServiceProvider serviceProvider) =>
            routes.MapAreaControllerRoute(
                name: "UserSwitcher",
                areaName: FeatureIds.LoginAsAnybody,
                pattern: _adminOptions.AdminUrlPrefix + "/Users/SwitchUser/{id}",
                defaults: new
                {
                    controller = typeof(UserSwitcherController).ControllerName(),
                    action = nameof(UserSwitcherController.SwitchUser),
                }
            );
}
