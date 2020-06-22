using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using YcTeam.DTO.Master;
using YcTeam.DTO.System;
using YcTeam.Models.Sys;

namespace YcTeam.IBLL.System
{
    public interface ISysNavItemService
    {
        int GetMaxOrd();

        Task CreateSysNavItem(SysNavItem sysNavItem);

        Task EditSysNavItem(SysNavItem sysNavItem);

        Task<List<DTO.System.SysNavItemDto>> GetAllSysNavItem(int pageIndex, int pageSize, bool asc);

        Task<List<SysNavItemDto>> GetAllSysNavItem();

        Task<int> GetDataCount();

        Task RemoveSysNavItem(Guid id);

        Task<SysNavItem> GetOneSysNavItemById(Guid id);

        List<SysNavItem> JoinNavItemAndNav();
    }
}
