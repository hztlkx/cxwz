using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Threading.Tasks;
using YcTeam.DAL.System;
using YcTeam.DTO.System;
using YcTeam.IBLL.Master;
using YcTeam.IBLL.System;
using YcTeam.IDAL.System;
using YcTeam.Models;
using YcTeam.Models.Sys;

namespace YcTeam.BLL.System
{
    public class SysNavRoleService : ISysNavRoleService
    {
        public List<SysNav> SysNav = null;
        public List<SysNavItem> SysNavItem  = null;

        public async Task CreateSysNavRole(Guid roleId, Guid navItemId)
        {
            using (var sysNavRoleDao = new SysNavRoleDao())
            {
                var model = new SysNavRole()
                {
                    NavItemId = navItemId,
                    RoleId = roleId,
                    CreateTime = DateTime.Now
                };
                await sysNavRoleDao.CreateAsync(model);
            }
        }

        public async Task RemoveSysNavRole(Guid id)
        {
            using (var sysNavRoleDao = new SysNavRoleDao())
            {
                await sysNavRoleDao.RemoveAsync(id);
            }
        }

        public List<SysNavRole> GetHomeNavRoles(Guid[] sysRoleId)
        {
            using (var sysNavRoleDao = new SysNavRoleDao())
            {
                //返回主键实体
                return  sysNavRoleDao.GetHomeNavRoles(sysRoleId);
            }
        }

        public Task<List<SysNavRole>> GetSysNavRole(Guid[] sysRoleId)
        {
            using (var sysNavRoleDao = new SysNavRoleDao())
            {
                //返回主键实体
                return sysNavRoleDao.GetNavRoles(sysRoleId);
            }
        }

        public object GetSysNavRole()
        {
            using (var sysNavRoleDao = new SysNavRoleDao())
            {
                //返回主键实体
                return sysNavRoleDao.GetNavRoles();
            }
        }

        public async Task<List<SysNavRole>> GetSysNavItemOneById(Guid roleId)
        {
            using (var sysNavRoleDao = new SysNavRoleDao())
            {
                return await sysNavRoleDao.GetAllAsync()
                    .Where(m => m.Id == roleId)
                    .Include(m => m.SysNavItem).ToListAsync();
            }
        }
    }
}
