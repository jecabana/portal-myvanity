using System.Collections.Generic;
using MyVanity.Domain;
using MyVanity.Model.AgentModels.Impl;
using MyVanity.Services.Membership;
using MyVanity.Views.Repositories.UserViewsRepository;

namespace MyVanity.Views.Repositories.AgentViewsRepository
{
    public interface IAgentViewRepository : IUserViewRepository<AgentEditModel>
    {
        ApplicationRole MapTypeToRole(AgentType type);

        /// <summary>
        /// Gets all agents, with optional filtering by type, or distinct from "distinctFrom" field
        /// </summary>
        /// <param name="distinctFrom"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        List<AgentEditModel> FilterAgents(int? distinctFrom, AgentType? type);
    }
}
