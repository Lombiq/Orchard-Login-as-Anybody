using Lombiq.Tests.UI.Services;
using System;
using System.Linq;

namespace Lombiq.LoginAsAnybody.Tests.UI.Extensions;

public static class Configurations
{
    // We are checking a page with Unauthorized status code, so we need to make an exception here.
    public static readonly Action<OrchardCoreUITestExecutorConfiguration> IgnoreUnauthorizedBrowserLogEntries =
        configuration =>
            configuration.AssertBrowserLog =
                logEntries =>
                    OrchardCoreUITestExecutorConfiguration.AssertBrowserLogIsEmpty(
                        logEntries.Where(logEntry =>
                            !logEntry.Message.ContainsOrdinalIgnoreCase("the server responded with a status of 401")));
}
