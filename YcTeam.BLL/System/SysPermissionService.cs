using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using YcTeam.DAL.System;
using YcTeam.IBLL.Master;
using YcTeam.IBLL.System;
using YcTeam.IDAL.System;
using YcTeam.Models;
using YcTeam.Models.Sys;

namespace YcTeam.BLL.System
{
    public class SysPermissionService : ISysPermissionService
    {
        public List<SysPermission> GetSysPermission(Guid permissionId)
        {
            using (IDAL.System.ISysPermission sysPermissionDao = new SysPermissionDao())
            {
                return sysPermissionDao.GetAllAsync().Where(m => m.Id == permissionId)
                    .Include(m=>m.SysRolePermissions).Where(r => !r.IsRemoved)
                    .Include(m=>m.SysRolePermissions.Select(a=>a.SysRole)).Where(r => !r.IsRemoved)
                    .ToList();
            }
        }

        public async Task CreateSysPermission(string name, string regionCity, string regionCounty)
        {
            throw new NotImplementedException();
        }

        public async Task EditSysPermission(Guid sysPermissionId, string name, string regionCity, string regionCounty)
        {
            throw new NotImplementedException();
        }

        public async Task<int> GetDataCount()
        {
            throw new NotImplementedException();
        }

        public async Task RemoveSysPermission(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> ExistsSysPermission(Guid sysPermissionId)
        {
            throw new NotImplementedException();
        }
    }
}
