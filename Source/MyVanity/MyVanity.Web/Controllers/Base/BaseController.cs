using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using MyVanity.Services.Membership;

namespace MyVanity.Web.Controllers.Base
{
    public class BaseController : Controller
    {
        private readonly IMembershipService _membershipService;
        private ApplicationUser _applicationUser;

        protected ApplicationUser CurrentUser
        {
            get 
            {
                return _applicationUser ?? (_applicationUser = _membershipService.FindByName(HttpContext.User.Identity.Name));
            }
        }

        public BaseController(IMembershipService membershipService)
        {
            _membershipService = membershipService;
        }

        protected void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
    }
}