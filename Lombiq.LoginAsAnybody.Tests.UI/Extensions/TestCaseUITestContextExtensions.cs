using Lombiq.Tests.UI.Extensions;
using Lombiq.Tests.UI.Models;
using Lombiq.Tests.UI.Services;
using OpenQA.Selenium;
using Shouldly;
using System.Threading.Tasks;

namespace Lombiq.LoginAsAnybody.Tests.UI.Extensions;

public static class TestCaseUITestContextExtensions
{
    public static async Task TestLoginAsAnybodyAsync(this UITestContext context, bool checkBuildLink = false)
    {
        await context.SignInDirectlyAndGoToDashboardAsync();

        var userParameters = UserRegistrationParameters.CreateDefault();
        await context.CreateUserAsync(userParameters.UserName, userParameters.Password, userParameters.Email);

        await context.GoToUsersAsync();
        await context.ClickReliablyOnAsync(By.XPath("//a[contains(.,'Log in as user')]"));

        (await context.GetCurrentUserNameAsync()).ShouldBe(userParameters.UserName);
    }
}
