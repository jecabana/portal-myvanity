﻿using System.Collections.Generic;
using MyVanity.Model.PatientModels.Impl;
using MyVanity.Model.Results;
using MyVanity.Views.Filters;
using MyVanity.Views.Repositories.UserViewsRepository;

namespace MyVanity.Views.Repositories.PatientViewsRepository
{
    public interface IPatientViewRepository : IUserViewRepository<PatientEditModel>
    {
        /// <summary>
        /// Gets the patients which belongs to the given agent
        /// </summary>
        /// <param name="agentId">Agent id</param>
        /// <param name="info">Filter info</param>
        /// <param name="patientName">Optional for filtering patients by name</param>
        /// <returns></returns>
        PagedResult<IEnumerable<PatientEditModel>> GetPatientsForAgent(int agentId, FilterInformation info, string patientName = null);

        /// <summary>
        /// Assigns a new surgical coordinator to the given patient
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="agentId"></param>
        void ReassignToAgent(int patientId, int agentId);
    }
}
