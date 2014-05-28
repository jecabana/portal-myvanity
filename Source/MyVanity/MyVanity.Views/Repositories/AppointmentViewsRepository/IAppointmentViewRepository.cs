using System.Collections.Generic;
using MyVanity.Domain;
using MyVanity.Model.AppointmentModels.Impl;

namespace MyVanity.Views.Repositories.AppointmentViewsRepository
{
    public interface IAppointmentViewRepository : IViewRepository<AppointmentEditModel>
    {
        /// <summary>
        /// Gets all the appointments scheduled for patients created by the given agent
        /// </summary>
        /// <param name="agentId">Agent id</param>
        /// <returns></returns>
        IEnumerable<AppointmentEditModel> GetAppointmentsForAgent(int agentId);

        /// <summary>
        /// Change the status to newStatus of the given appointment
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="newStatus">New status</param>
        AppointmentStatus ChangeStatus(int id, AppointmentStatus newStatus);
    }
}
