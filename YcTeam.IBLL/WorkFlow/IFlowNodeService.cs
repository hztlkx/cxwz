using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using YcTeam.DTO.Master;
using YcTeam.DTO.System;
using YcTeam.DTO.WorkFlow;
using YcTeam.Models.Sys;
using YcTeam.Models.WorkFlow;

namespace YcTeam.IBLL.WorkFlow
{
    public interface IFlowNodeService
    {
        Task<List<SysUser>> NextSysUser(int nextNodeNumber);

        Task CreateFlowNode(FlowNode flowNode);

        Task EditFlowNode(FlowNode flowNode);

        Task<int> GetDataCount();

        Task<FlowNodeDto> GetOneById(Guid id);

        Task<int> MaxNodeNumber();

        Task<FlowNode> GetFlowNodeByNodeName(string nodeName);

        Task<List<FlowNodeDto>> GetAllFlowNode(int pageIndex, int pageSize, bool asc);

        Task<List<FlowNodeDto>> GetAllFlowNode();

        Task RemoveFlowNode(Guid id);
    }
}
