using System.Collections.Generic;
using MyVanity.Model.PatientProcedureModels.Impl;
using MyVanity.Model.Results;
using MyVanity.Views.Filters;

namespace MyVanity.Views.Repositories.PatientProcedureViewsRepository
{
    public interface IPatientProcedureViewRepository : IViewRepository<PatientProcedureEditModel>
    {
        /// <summary>
        /// Gets all existing user procedures scheduled for the users added by the given agent
        /// </summary>
        /// <param name="agentId">Agent id</param>
        /// <param name="info">Paging info</param>
        /// <returns></returns>
        PagedResult<IEnumerable<PatientProcedureEditModel>> GetUserProcedures(int agentId, FilterInformation info);

        /// <summary>
        /// Gets existing user procedures the given patient
        /// </summary>
        /// <param name="name">Patient name (can be First name, Middle name, Username, or Patient number)</param>
        /// /// <param name="agentId">Agent creator of given patient</param>
        /// <returns></returns>
        IEnumerable<PatientProcedureEditModel> GetUserProcedures(int agentId, string name);
    }
}
