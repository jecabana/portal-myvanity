using System.Collections.Generic;
using System.Linq;
using MyVanity.Common.Helpers;
using MyVanity.Domain;
using MyVanity.Domain.UoW;
using MyVanity.Model;
using MyVanity.Model.ConsentFormModels.Impl;
using MyVanity.Model.PatientProcedureModels.Impl;
using MyVanity.Views.Repositories.PatientProcedure;

namespace MyVanity.Views.Repositories.PatientProcedureViewsRepository.Impl
{
    public class PatientProcedureViewRepository : ViewRepository<UserProcedure, PatientProcedureEditModel>, IPatientProcedureViewRepository
    {
        private readonly IModelConverter<UserProcedure, PatientProcedureEditModel> _modelConverter;
        private readonly IUnitOfWork _unitOfWork;

        public PatientProcedureViewRepository(IModelConverter<UserProcedure, PatientProcedureEditModel> modelConverter, IUnitOfWork unitOfWork) : base(modelConverter, unitOfWork)
        {
            _modelConverter = modelConverter;
            _unitOfWork = unitOfWork;

        }

        public override void Insert(PatientProcedureEditModel model)
        {
            var entity = _modelConverter.ConvertToSource(model);
            Save(entity, model);

            UnitOfWork.GetRepository<UserProcedure>().Insert(entity);
            UnitOfWork.SaveChanges();
        }

        private void Save(UserProcedure entity, PatientProcedureEditModel model)
        {
            var doctorRepository = UnitOfWork.GetRepository<Doctor>();
            var consentRepository = UnitOfWork.GetRepository<ConsentForm>();

            if (entity.Doctors != null)
                entity.Doctors.Clear();
            else 
                entity.Doctors = new List<Doctor>();
            
            foreach (var item in model.SelectedDoctors.Select(doctorRepository.FindById))
                entity.Doctors.Add(item);

            var existingProcedureConsents = new List<int>(entity.UserProcedureConsentSigns.Select(x => x.Id));

            var modelConsents = model.SelectedConsents ?? new List<ProcedureConsentViewModel>();
            foreach (var consent in modelConsents.Where(consent => existingProcedureConsents.All(x => x != consent.Id)))
            {
                entity.UserProcedureConsentSigns.Add(new UserProcedureConsentSign
                                                     {
                                                         ConsentFormId = consent.ConsentId,
                                                         ConsentForm = consentRepository.FindById(consent.ConsentId),
                                                         UserProcedure = entity,
                                                         Id = consent.Id,
                                                         Signed = consent.Signed,
                                                         UserProcedureId = entity.Id
                                                     });
            }

            foreach (var item in existingProcedureConsents)
            {
                if (modelConsents.All(x => x.Id != item))
                    entity.UserProcedureConsentSigns.Remove(entity.UserProcedureConsentSigns.Single(x => x.Id == item));
            }

            if (entity.SharedDocuments != null)
                entity.SharedDocuments.Clear();
            else
                entity.SharedDocuments = new List<SharedDocument>();

            var filesRepository = UnitOfWork.GetRepository<SharedDocument>();

            var modelFiles = model.SelectedFiles ?? new List<int>();
            foreach (var item in modelFiles.Select(filesRepository.FindById))
                entity.SharedDocuments.Add(item);
                
            var agents = new List<Agent>();
            var agentRepository = _unitOfWork.GetRepository<Agent>();   

            var financial = agentRepository.FindById(model.SelectedFinancialCoordinator);
            financial.UserProcedures.Add(entity);

            var medical = agentRepository.FindById(model.SelectedMedicalAssistant);
            medical.UserProcedures.Add(entity);

            agents.Add(financial);
            agents.Add(medical);
            agents.Add(entity.Patient.Agent);

            if (model.SelectedHomeAwayAssistant != null && model.SelectedHomeAwayAssistant != -1)
            {
                var homeAway = agentRepository.FindById(model.SelectedHomeAwayAssistant.Value);
                homeAway.UserProcedures.Add(entity);
                agents.Add(homeAway);
            }
            
            entity.Agents.Clear(); 
            entity.Agents = agents;

            var procedureRepository = UnitOfWork.GetRepository<Procedure>();
            entity.Procedure = procedureRepository.FindById(model.SelectedProcedure);
        }

        public override void Update(PatientProcedureEditModel model)
        {
            var entity = _modelConverter.ConvertToSource(model);
            Save(entity, model);

            UnitOfWork.GetRepository<UserProcedure>().Update(entity);
            UnitOfWork.SaveChanges();
        }

        public IEnumerable<PatientProcedureEditModel> GetUserProcedures(int agentId)
        {
            var agentRepository = _unitOfWork.GetRepository<Agent>();
            var agent = agentRepository.FindById(agentId);

            var procedures = new List<PatientProcedureEditModel>();

            foreach (var proceduresModel in agent.Patients.Select(patient => patient.Procedures.Select(x => _modelConverter.ConvertToModel(x))))
                procedures.AddRange(proceduresModel);

            return procedures;
        }

        public IEnumerable<PatientProcedureEditModel> GetUserProcedures(int agentId, string name)
        {
            var agentRepository = _unitOfWork.GetRepository<Agent>();
            var agent = agentRepository.FindById(agentId);

            var patients = agent.Patients.Where(x => x.Profile.FirstName.IgnoreCase(name)
                                                     || x.Profile.MiddleName.IgnoreCase(name)
                                                     || x.Profile.LastName.IgnoreCase(name)
                                                     || x.Id.ToString() == name).ToList();

            var result = new List<PatientProcedureEditModel>();
            
            foreach (var patient in patients)
                result.AddRange(patient.Procedures.Select(x => _modelConverter.ConvertToModel(x)));

            return result;
        }
    }
}
