using Lombiq.Tests.UI.Extensions;
using Lombiq.Tests.UI.Services;
using OpenQA.Selenium;
using Shouldly;
using System.Threading.Tasks;
using static Lombiq.Tests.UI.Constants.TestUser;

namespace Lombiq.LoginAsAnybody.Tests.UI.Extensions;

public static class TestCaseUITestContextExtensions
{
    public static async Task SwitchingUserShouldWorkCorrectlyAsync(this UITestContext context)
    {
        await context.CreateUserAsync();

        await context.SignInDirectlyAsync();
        await context.GoToUsersAsync();
        await context.ClickReliablyOnAsync(By.XPath("//a[contains(.,'Log in as user')]"));

        (await context.GetCurrentUserNameAsync()).ShouldBe(UserName);
    }

    public static async Task PermissionCheckShouldWorkCorrectlyAsync(this UITestContext context)
    {
        await context.CreateUserAsync();

        // The role needs this permission to visit the users page.
        await context.AddPermissionToRoleAsync("ManageUsers", "Moderator");
        await context.AddUserToRoleAsync(UserName, "Moderator");

        await context.SignInDirectlyAsync(UserName);
        await context.GoToUsersAsync();

        context.Missing(By.XPath("//a[contains(.,'Log in as user')]"));
    }
}
