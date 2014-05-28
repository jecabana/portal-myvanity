using System.Collections.Generic;
using MyVanity.Model.AgentModels.Impl;

namespace MyVanity.Model.AdminModels
{
    public class AdminViewModel
    {
        public AdminViewModel(IEnumerable<AgentEditModel> agentViewModel)
        {
            Agents = agentViewModel;
        }

        public IEnumerable<AgentEditModel> Agents { get; set; }
    }
}
