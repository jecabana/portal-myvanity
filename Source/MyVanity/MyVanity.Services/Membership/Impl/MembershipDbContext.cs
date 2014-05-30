using Microsoft.AspNet.Identity.EntityFramework;
using MyVanity.Common.Autofac;

namespace MyVanity.Services.Membership
{
    public class MembershipDbContext : IdentityDbContext<ApplicationUser>, ISingleInstanceDependency
    {
        public MembershipDbContext()
            : base("DefaultConnection")
        {
        }
    }
}