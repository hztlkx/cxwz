using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using YcTeam.DTO.Master;

namespace YcTeam.IBLL.System
{
    public interface ISysUserRoleService
    {
        Task CreateSysUserRole(Guid userId, Guid roleId);

        Task EditSysUserRole(Guid sysUserId, Guid[] sysRoleId);

        Task<List<DTO.System.SysUserRoleDto>> GetAllSysUserRole(int pageSize, int pageIndex, bool asc);

        Task<int> GetDataCount();

        Task RemoveSysUserRole(Guid id);

        Task<bool> ExistsSysUserRole(Guid userId, Guid roleId);
    }
}
