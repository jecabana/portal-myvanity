using System.Collections.Generic;
using MyVanity.Common.Autofac;

namespace MyVanity.Services.UserProcedureConsentServices
{
    public interface IUserProcedureConsentService : IPerRequestDependency
    {
        void SignConsents(int procedureId, List<int> consents);
    }
}
