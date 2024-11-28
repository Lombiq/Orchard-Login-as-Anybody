using Lombiq.HelpfulLibraries.OrchardCore.Users;
using OrchardCore.Security.Permissions;
using System.Collections.Generic;

namespace Lombiq.LoginAsAnybody.Permissions;

public sealed class LoginAsAnybodyPermissions : AdminPermissionBase
{
    public static readonly Permission LoginAsAnybody = new(nameof(LoginAsAnybody), "Log in as any user.", isSecurityCritical: true);

    protected override IEnumerable<Permission> AdminPermissions => [LoginAsAnybody];
}
