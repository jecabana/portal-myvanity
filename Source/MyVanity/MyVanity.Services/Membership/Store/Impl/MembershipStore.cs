using System.Data.Entity;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MyVanity.Services.Membership.Store.Impl
{
    public class MembershipStore : UserStore<ApplicationUser> , IMembershipStore
    {
        public MembershipDbContext MembershipDbContext { get; set; }

        public MembershipStore(MembershipDbContext membershipDbContext)
        {
            MembershipDbContext = membershipDbContext;
        }

        public override Task DeleteAsync(ApplicationUser user)
        {
            var entry = MembershipDbContext.Entry(user);

            if (entry.State == EntityState.Detached)
                MembershipDbContext.Users.Attach(user);

            MembershipDbContext.Users.Remove(user);

            return MembershipDbContext.SaveChangesAsync();
        }
    }
}
