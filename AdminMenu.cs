using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.Localization;
using Orchard.Security;
using Orchard.UI.Navigation;

namespace Lombiq.LoginAsAnybody
{
    public class AdminMenu : INavigationProvider
    {
        public Localizer T { get; set; }
        public string MenuName { get { return "admin"; } }

        public void GetNavigation(NavigationBuilder builder)
        {
            builder
                .Add(T("Users"), "11",
                    menu => menu
                        .Add(T("Login as anybody"), "5.0", item => item.Action("Index", "Admin", new { area = "Lombiq.LoginAsAnybody" })
                            .LocalNav().Permission(StandardPermissions.SiteOwner)));
        }
    }
}