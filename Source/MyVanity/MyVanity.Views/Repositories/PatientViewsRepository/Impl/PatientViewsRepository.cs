using System.Collections.Generic;
using System.Linq;
using MyVanity.Common.Helpers;
using MyVanity.Domain;
using MyVanity.Domain.UoW;
using MyVanity.Model;
using MyVanity.Model.PatientModels.Impl;
using MyVanity.Views.Repositories.UserViewsRepository;

namespace MyVanity.Views.Repositories.PatientViewsRepository.Impl
{
    public class PatientViewsRepository : UserViewRepository<Patient, PatientEditModel>, IPatientViewRepository
    {
        private readonly IModelConverter<Patient, PatientEditModel> _modelConverter;
        private readonly IUnitOfWork _unitOfWork;

        public PatientViewsRepository(IModelConverter<Patient, PatientEditModel> modelConverter, IUnitOfWork unitOfWork) : base(modelConverter, unitOfWork)
        {
            _modelConverter = modelConverter;
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<PatientEditModel> GetPatientsForAgent(int agentId, string patientName = null)
        {
            var agentRepository = UnitOfWork.GetRepository<Agent>();
            var agent = agentRepository.FindById(agentId);

            var patients = agent.Patients;

            if (!string.IsNullOrEmpty(patientName))
            {
                patientName = patientName.ToLower();

                patients = patients.Where(x => x.Profile.FirstName.IgnoreCase(patientName) ||
                                               x.Profile.LastName.IgnoreCase(patientName) ||
                                               x.Profile.MiddleName.IgnoreCase(patientName) ||
                                               x.Email.IgnoreCase(patientName) ||
                                               x.Id.ToString() == patientName).ToList();
            }

            return patients.Select(x => _modelConverter.ConvertToModel(x));
        }

        public void ReassignToAgent(int patientId, int agentId)
        {
            var repository = _unitOfWork.GetRepository<Patient>();
            var patient = repository.FindById(patientId);

            patient.Agent = null;
            patient.AgentId = agentId;

            _unitOfWork.SaveChanges();
        }
    }
}
