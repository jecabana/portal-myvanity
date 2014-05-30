using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using MyVanity.Domain;
using MyVanity.Domain.UoW;
using MyVanity.Model;
using MyVanity.Model.PatientModels.Impl;
using MyVanity.Model.Results;
using MyVanity.Views.Filters;
using MyVanity.Views.Repositories.UserViewsRepository;

namespace MyVanity.Views.Repositories.PatientViewsRepository.Impl
{
    public class PatientViewsRepository : UserViewRepository<Patient, PatientEditModel>, IPatientViewRepository
    {
        private readonly IUnitOfWork _unitOfWork;

        public PatientViewsRepository(IModelConverter<Patient, PatientEditModel> modelConverter, IUnitOfWork unitOfWork) : base(modelConverter, unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public PagedResult<IEnumerable<PatientEditModel>> GetPatientsForAgent(int agentId, FilterInformation info , string patientName = null)
        {   
            var filter = new TypedFilter<Patient>(info.PageSize, info.Page)
                         {
                             OrderColumn = info.OrderColumn,
                             SortMode = info.SortMode
                         };

            if (!string.IsNullOrEmpty(patientName))
            {
                patientName = patientName.ToLower();
                filter.Filter = x => x.AgentId == agentId && (x.Profile.FirstName.ToLower().Contains(patientName) ||
                                     x.Profile.LastName.ToLower().Contains(patientName) ||
                                     x.Profile.MiddleName.ToLower().Contains(patientName) ||
                                     x.Email.ToLower().Contains(patientName) ||
                                     SqlFunctions.StringConvert((double) x.Id).ToLower() == patientName);
            }

            return FilterModel(filter);
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
