using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using YcTeam.DTO.Master;
using YcTeam.DTO.System;
using YcTeam.Models.Sys;
using YcTeam.Models.WorkFlow;

namespace YcTeam.IBLL.WorkFlow
{
    public interface IFlowInstanceService
    {
        Task CreateFlowInstance(FlowInstance flowInstance);

        Task<List<FlowInstance>> GetAllFlowInstance();

        Task<List<FlowInstance>> GetAllFlowInstanceByPlanId(Guid id);

        Task RemoveFlowInstanceByPlanId(Guid planId);
    }
}
