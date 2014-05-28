using MyVanity.Domain.Repositories.UsersRepository.Impl;

namespace MyVanity.Domain.Repositories.PatientsRepository.Impl
{
    public class PatientRepository : UsersRepository<Patient> , IPatientRepository
    {
        private readonly ModelContainer _context;

        public PatientRepository(ModelContainer context) : base(context)
        {
            _context = context;
        }

        /*public override void Delete(Patient patient)
        {
            //Workaround for many to many relations issues
            if (patient.Procedures != null && patient.Procedures.Count != 0)
            {
                var procedures = new List<UserProcedure>(patient.Procedures);
                foreach (var procedure in procedures)
                {
                    var prc = _context.UserProcedures.Include("Doctors.Agents").Single(x => x.Id == procedure.Id);
                    _context.UserProcedures.Remove(prc);
                }
            }

            base.Delete(patient);
        }*/
    }
}
