using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YcTeam.DTO.Master;
using YcTeam.Models.Sys;

namespace YcTeam.IBLL.System
{
    public interface ISysPermissionService
    {
        List<SysPermission> GetSysPermission(Guid permissionId);

        Task CreateSysPermission(string name, string regionCity, string regionCounty);

        Task EditSysPermission(Guid sysPermissionId, string name, string regionCity, string regionCounty);

        Task<int> GetDataCount();

        Task RemoveSysPermission(Guid id);

        Task<bool> ExistsSysPermission(Guid sysPermissionId);
    }
}
