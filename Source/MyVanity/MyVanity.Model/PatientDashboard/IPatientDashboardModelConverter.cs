using MyVanity.Common.Autofac;
using MyVanity.Model.PatientDashboard.Impl;

namespace MyVanity.Model.PatientDashboard
{
    public interface IPatientDashboardModelConverter : IPerRequestDependency
    {
        PatientDashboardViewModel BuildModel(int patientId, int procedureId);
    }
}
