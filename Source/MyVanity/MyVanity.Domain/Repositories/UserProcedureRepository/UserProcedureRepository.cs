using System.Data.Entity;
using System.Linq;
using MyVanity.Domain.Repositories.Base.Impl;

namespace MyVanity.Domain.Repositories.UserProcedureRepository
{
    public class UserProcedureRepository : RepositoryBase<UserProcedure>
    {
        private readonly ModelContainer _context;

        public UserProcedureRepository(ModelContainer context) : base(context)
        {
            _context = context;
        }

        public override void Delete(int id)
        {
            var procedure = _context.UserProcedures.Include(p => p.Doctors).Include(p => p.Agents).SingleOrDefault(x => x.Id == id);
            _context.UserProcedures.Remove(procedure);
            _context.SaveChanges();
        }
    }
}
