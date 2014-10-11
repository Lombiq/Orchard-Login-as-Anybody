using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Orchard.Security;
using Orchard.Mvc.Extensions;
using Orchard.UI.Admin;

namespace Lombiq.LoginAsAnybody.Controllers
{
    [Admin]
    public class AdminController : Controller
    {
        private readonly IAuthorizer _authorizer;
        private readonly IMembershipService _membershipService;
        private readonly IAuthenticationService _authenticationService;


        public AdminController(
            IAuthorizer authorizer,
            IMembershipService membershipService,
            IAuthenticationService authenticationService)
        {
            _authorizer = authorizer;
            _membershipService = membershipService;
            _authenticationService = authenticationService;
        }


        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string userName, string returnUrl = null)
        {
            if (!_authorizer.Authorize(StandardPermissions.SiteOwner)) return new HttpUnauthorizedResult();

            var user = _membershipService.GetUser(userName);

            if (user == null) return HttpNotFound();

            _authenticationService.SignIn(user, false);

            if (string.IsNullOrEmpty(returnUrl)) returnUrl = "~/";

            return this.RedirectLocal(returnUrl);
        }
    }
}