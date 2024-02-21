using Lombiq.Tests.UI.Extensions;
using Lombiq.Tests.UI.Models;
using Lombiq.Tests.UI.Services;
using OpenQA.Selenium;
using Shouldly;
using System.Threading.Tasks;

namespace Lombiq.LoginAsAnybody.Tests.UI.Extensions;

public static class TestCaseUITestContextExtensions
{
    public static async Task TestLoginAsAnybodyAsync(this UITestContext context)
    {
        await context.SignInDirectlyAndGoToDashboardAsync();

        var userParameters = UserRegistrationParameters.CreateDefault();
        await context.CreateUserAsync(userParameters.UserName, userParameters.Password, userParameters.Email);

        await context.GoToUsersAsync();
        await context.ClickReliablyOnAsync(By.XPath("//button[contains(.,'Log in as user')]"));

        (await context.GetCurrentUserNameAsync()).ShouldBe(userParameters.UserName);
    }

    public static async Task TestLoginAsAnybodyAuthorizationAsync(this UITestContext context)
    {
        var userParameters = UserRegistrationParameters.CreateDefault();
        await context.CreateUserAsync(userParameters.UserName, userParameters.Password, userParameters.Email);

        // The role needs this permission to visit the users page.
        await context.AddPermissionToRoleAsync("ManageUsers", "Moderator");
        await context.AddUserToRoleAsync(userParameters.UserName, "Moderator");

        await context.SignInDirectlyAndGoToDashboardAsync(userParameters.UserName);
        await context.GoToUsersAsync();

        await context.ClickReliablyOnAsync(By.XPath("//button[contains(.,'Log in as user')]"));
        context.Exists(By.XPath("//h1[contains(text(),'You are not authorized to view this content.')]"));
    }
}
