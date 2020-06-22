using System;
using System.Data.Entity;
using System.Threading.Tasks;
using YcTeam.IDAL.Master;
using YcTeam.IDAL.System;
using YcTeam.Models;
using YcTeam.Models.Master;
using YcTeam.Models.Sys;

namespace YcTeam.DAL.System
{
    public class SysUserRoleDao : BaseService<SysUserRole>, ISysUserRoleDao
    {
        public SysUserRoleDao() : base(new YcContext())
        {
            
        }

        
        /// <summary>
        ///  按用户编号、角色编号查找中间表数据
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <param name="roleId">角色编号</param>
        /// <param name="saved">是否执行保存</param>
        /// <returns></returns>

        public async Task<SysUserRole> GetOneByUserAndRoleIdAsync(Guid userId, Guid roleId, bool saved = true)
        {
            return await GetAllAsync().FirstAsync(m => m.SysUserId == userId && m.SysRoleId == roleId);
        }
    }
}
