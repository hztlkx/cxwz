using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using YcTeam.DAL.System;
using YcTeam.DAL.WorkFlow;
using YcTeam.DTO.WorkFlow;
using YcTeam.IBLL.Master;
using YcTeam.IBLL.System;
using YcTeam.IBLL.WorkFlow;
using YcTeam.IDAL.System;
using YcTeam.Models;
using YcTeam.Models.Sys;
using YcTeam.Models.WorkFlow;

namespace YcTeam.BLL.WorkFlow
{
    public class FlowNodeService : IFlowNodeService
    {
        public async Task<FlowNode> NextFlowNode(Guid id)
        {
            using (var flowNodeDao = new FlowNodeDao())
            {
                return await flowNodeDao.GetOneByIdAsync(id);
            }
        }

        /// <summary>
        /// 获取下一操作人
        /// </summary>
        /// <param name="nextNodeNumber"></param>
        /// <returns></returns>

        public async Task<List<SysUser>> NextSysUser(int nextNodeNumber)
        {
            using (var flowNodeDao = new FlowNodeDao())
            {
                return await flowNodeDao.JoinNextUserAsync(nextNodeNumber);
            }
        }

        public async Task CreateFlowNode(FlowNode flowNode)
        {
            using (var flowNodeDao = new FlowNodeDao())
            {
                await flowNodeDao.CreateAsync(flowNode);
            }
        }

        public async Task EditFlowNode(FlowNode flowNode)
        {
            using (var flowNodeDao = new FlowNodeDao())
            {
                await flowNodeDao.EditAsync(flowNode);
            }
        }

        /// <summary>
        ///  获取节点（联表：SysRole）
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<FlowNodeDto> GetOneById(Guid id)
        {
            using (var flowNodeDao = new FlowNodeDao())
            {
                var model = await flowNodeDao.JoinRoleAsync(id);

                return new FlowNodeDto()
                {
                     Id = model.Id,
                     NextNodeNumber = model.NextNodeNumber,
                     PreNodeNumber = model.PreNodeNumber,
                     NodeName = model.NodeName,
                     SysRoleId = model.SysRoleId,
                     SysRoleName = model.SysRole.RoleName,
                     NodeNumber = model.NodeNumber,
                     UpdateTime = model.UpdateTime,
                     CreateTime = model.CreateTime
                };
            }
        }

        /// <summary>
        /// 获取最大节点
        /// </summary>
        /// <returns></returns>
        public async Task<int> MaxNodeNumber()
        {
            using (var flowNodeDao = new FlowNodeDao())
            {
                var list = await flowNodeDao.GetAllAsync().Where(m => !m.IsRemoved).ToListAsync();
                return list.Count > 0 ? list.Max(m=>m.NodeNumber) : 101;
            }
        }

        /// <summary>
        /// 创建工作流节点
        /// </summary>
        /// <returns></returns>

        public async Task<FlowNode> GetFlowNodeByNodeName(string nodeName)
        {
            using (var flowNodeDao = new FlowNodeDao())
            {
                return await flowNodeDao.GetAllAsync()
                    .Where(m => m.NodeName.Equals(nodeName)).FirstAsync();
            }
        }

        /// <summary>
        /// 获取所有工作流节点
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="asc"></param>
        /// <returns></returns>
        public async Task<List<FlowNodeDto>> GetAllFlowNode(int pageIndex, int pageSize, bool asc)
        {
            using (var flowNodeDao = new FlowNodeDao())
            {
                return await flowNodeDao.GetAllByPageOrderAsync(pageIndex - 1, pageSize, asc)
                    .OrderBy(m => m.NodeNumber).ThenBy(m=>m.PreNodeNumber).Select(m=>new FlowNodeDto
                    {
                        Id = m.Id,
                        NodeName = m.NodeName,
                        NodeNumber = m.NodeNumber,
                        SysRoleId = m.SysRoleId,
                        SysRoleName = m.SysRole.RoleName,
                        NextNodeNumber = m.NextNodeNumber,
                        PreNodeNumber = m.PreNodeNumber,
                        UpdateTime = m.UpdateTime,
                        CreateTime = m.CreateTime
                    }).ToListAsync();
            }
        }


        public async Task<List<FlowNodeDto>> GetAllFlowNode()
        {
            using (var flowNodeDao = new FlowNodeDao())
            {
                return await flowNodeDao.GetAllAsync()
                    .OrderBy(m => m.NodeNumber).ThenBy(m => m.PreNodeNumber).Select(m => new FlowNodeDto
                    {
                        Id = m.Id,
                        NodeName = m.NodeName,
                        NodeNumber = m.NodeNumber,
                        SysRoleId = m.SysRoleId,
                        SysRoleName = m.SysRole.RoleName,
                        NextNodeNumber = m.NextNodeNumber,
                        PreNodeNumber = m.PreNodeNumber,
                        UpdateTime = m.UpdateTime,
                        CreateTime = m.CreateTime
                    }).ToListAsync();
            }
        }

        /// <summary>
        /// 获取数量
        /// </summary>
        /// <returns></returns>
        public async Task<int> GetDataCount()
        {
            using (var flowNodeDao = new FlowNodeDao())
            {
                return await flowNodeDao.GetAllAsync().CountAsync();
            }
        }


        /// <summary>
        /// 删除流程节点
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task RemoveFlowNode(Guid id)
        {
            using (var flowNodeDao = new FlowNodeDao())
            {
                await flowNodeDao.RemoveAsync(id);
            }
        }
    }
}
