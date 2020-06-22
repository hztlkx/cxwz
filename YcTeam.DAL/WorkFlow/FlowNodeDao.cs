using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using YcTeam.IDAL.Master;
using YcTeam.IDAL.System;
using YcTeam.IDAL.WorkFlow;
using YcTeam.Models;
using YcTeam.Models.Master;
using YcTeam.Models.Sys;
using YcTeam.Models.WorkFlow;

namespace YcTeam.DAL.WorkFlow
{
    public class FlowNodeDao : BaseService<FlowNode>, IFlowNodeDao
    {
        public FlowNodeDao() : base(new YcContext())
        {

        }

        public async Task<List<SysUser>> JoinNextUserAsync(int nextNodeNumber)
        {
            var t1 = Db.FlowNode.Where(m => !m.IsRemoved && m.NodeNumber == nextNodeNumber);
            var t2 = Db.SysRole.Where(m => !m.IsRemoved);

            //内连接inner join
            var data =
                await t1.Join(t2,
                        flowNode => flowNode.SysRoleId,
                        sysRole => sysRole.Id,
                        (flowNode, sysRole) => new { flowNode, sysRole })
                    .ToListAsync();

            var x = data.FirstOrDefault();

            if (x!=null)
            {
                var roleId = x.sysRole.Id;

                var t3 = Db.SysUserRole.Where(m => !m.IsRemoved && m.SysRoleId == roleId);
                var t4 = Db.SysUser.Where(m => !m.IsRemoved);

                var list = t3.Join(t4,
                    sysUserRole => sysUserRole.SysUserId,
                    sysUser => sysUser.Id,
                    (sysUserRole, sysUser) => new { sysUser });
                return await list.Select(m => m.sysUser).ToListAsync();
            }
            return null;
        }

        /// <summary>
        /// 多表查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<FlowNode> JoinRoleAsync(Guid id)
        {
            var t1 = Db.FlowNode.Where(m => !m.IsRemoved && m.Id == id);
            var t2 = Db.SysRole.Where(m => !m.IsRemoved);

            //内连接inner join，返回所有字段
            var data =
                await t1.Join(t2,
                        flowNode => flowNode.SysRoleId,
                        sysRole => sysRole.Id,
                        (flowNode, sysRole) => new {flowNode, sysRole}) //内连接表NavItem
                      .ToListAsync();

            var x = data.FirstOrDefault();
            //外键实体填充
            if (x?.flowNode != null)
            {
                x.flowNode.SysRole = x.sysRole;
            }
            return x?.flowNode;
        }
    }
}
