using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using YcTeam.DTO.Master;
using YcTeam.DTO.System;
using YcTeam.Models.Sys;

namespace YcTeam.IBLL.System
{
    public interface ISysRoleService
    {
        int GetMaxOrd();
        Task CreateSysRole(string roleName , int sortOrder);

        Task EditSysRole(Guid sysRoleId, string roleName, int sortOrder);

        Task<List<DTO.System.SysRoleDto>> GetAllSysRole();

        Task<List<DTO.System.SysRoleDto>> GetAllSysRole(int pageSize, int pageIndex, bool asc);

        Task<int> GetDataCount();

        Task RemoveSysRole(Guid id);

        Task<bool> ExistsSysRole(Guid sysRoleId);

        Task<List<SysNavRole>> GetSysNavRole(Guid[] sysRoleId);

        List<SysNavRole> GetSysNavRole();
    }
}
