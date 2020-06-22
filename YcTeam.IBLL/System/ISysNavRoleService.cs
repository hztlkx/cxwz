using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Threading.Tasks;
using YcTeam.DTO.Master;
using YcTeam.DTO.System;
using YcTeam.Models.Sys;

namespace YcTeam.IBLL.System
{
    public interface ISysNavRoleService
    {
        Task CreateSysNavRole(Guid roleId, Guid navItemId);

        Task RemoveSysNavRole(Guid id);

        object GetSysNavRole();

        List<SysNavRole> GetHomeNavRoles(Guid[] sysRoleId);

        Task<List<SysNavRole>> GetSysNavRole(Guid[] sysRoleId);

        Task<List<SysNavRole>> GetSysNavItemOneById(Guid roleId);
    }
}
