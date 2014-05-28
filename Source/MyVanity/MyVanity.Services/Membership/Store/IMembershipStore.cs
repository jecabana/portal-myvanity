using Microsoft.AspNet.Identity;
using MyVanity.Common.Autofac;

namespace MyVanity.Services.Membership
{
    public interface IMembershipStore : IUserStore<ApplicationUser>, IPerRequestDependency
    {
        MembershipDbContext MembershipDbContext { get; set; }
    }
}
