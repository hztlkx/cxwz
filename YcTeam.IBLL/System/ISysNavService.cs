using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using YcTeam.DTO.Master;
using YcTeam.DTO.System;
using YcTeam.Models.Sys;

namespace YcTeam.IBLL.System
{
    public interface ISysNavService
    {
        int GetMaxOrd();

        Task CreateSysNav(SysNav sysNav);

        Task EditSysNav(SysNav sysNav);

        Task<List<DTO.System.SysNavDto>> GetAllSysNav(int pageIndex, int pageSize, bool asc);

        Task<List<SysNavItemDto>> GetAllSysNav();

        Task<int> GetDataCount();

        Task RemoveSysNav(Guid id);

        Task<SysNavDto> GetOneSysNavById(Guid id);
    }
}
