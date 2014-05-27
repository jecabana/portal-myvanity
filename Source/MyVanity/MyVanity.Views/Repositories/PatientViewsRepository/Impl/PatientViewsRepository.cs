using System.Collections.Generic;
using System.Linq;
using MyVanity.Common.Helpers;
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
                filter.Filter = x => x.AgentId == agentId && (x.Profile.FirstName.IgnoreCase(patientName) ||
                                     x.Profile.LastName.IgnoreCase(patientName) ||
                                     x.Profile.MiddleName.IgnoreCase(patientName) ||
                                     x.Email.IgnoreCase(patientName) ||
                                     x.Id.ToString() == patientName);
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
