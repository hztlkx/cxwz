using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using YcTeam.DTO.Master;
using YcTeam.DTO.System;
using YcTeam.Models.Sys;

namespace YcTeam.IBLL.System
{
    public interface ISysUserService
    {
        Task CreateSysUser(SysUser sysUser);

        Task<SysUser> GetOneSysUserById(Guid sysUserId);

        Task EditSysUser(SysUser sysUser,Guid[] sysRoleIds);

        Task<List<SysUserDto>> GetAllSysUser(int pageSize, int pageIndex, bool asc);

        Task<int> GetDataCount();

        Task RemoveSysUser(Guid id);

        Task<bool> ExistsSysUser(Guid sysUserId);

        Task Register(SysUser sysUser,Guid[] sysRoleId);
    }
}
