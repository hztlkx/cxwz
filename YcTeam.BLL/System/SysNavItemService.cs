using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
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
    public class SysNavItemService : ISysNavItemService
    {
        public int GetMaxOrd()
        {
            using (var sysNavItemDao = new SysNavItemDao())
            {
                var query = sysNavItemDao.GetAllAsync().Select(m=>m.NodeOrd).DefaultIfEmpty();
                return query.Max();
            }
        }

        public async Task CreateSysNavItem(SysNavItem sysNavItem)
        {
            using (var sysNavItemDao = new SysNavItemDao())
            {
                await sysNavItemDao.CreateAsync(sysNavItem);
            }
        }

        public async Task EditSysNavItem(SysNavItem sysNavItem)
        {
            using (var sysNavItemDao = new SysNavItemDao())
            {
                await sysNavItemDao.EditAsync(sysNavItem);
            }
        }

        public async Task<List<SysNavItemDto>> GetAllSysNavItem(int pageIndex, int pageSize, bool asc)
        {
            using (var sysNavItemDao = new SysNavItemDao())
            {
                return await sysNavItemDao.GetAllByPageOrderAsync(pageIndex - 1, pageSize, asc)
                    .Include(m=>m.SysNav)
                    .OrderBy(m=>m.SysNav.NavOrd).ThenBy(m=>m.NodeOrd)
                    .Select(m => new DTO.System.SysNavItemDto()
                {
                    Id = m.Id,
                    NodeName = m.NodeName,
                    NodeUrl = m.NodeUrl,
                    NodeIcons = m.NodeIcons,
                    NavId = m.SysNav.Id,
                    NavName = m.SysNav.NavName,
                    Pid = m.Pid,
                    RootId = m.RootId,
                    Deep = m.Deep,
                    NodeOrd = m.NodeOrd,
                    CreateTime = m.CreateTime
                }).ToListAsync();
            }
        }

        public async Task<int> GetDataCount()
        {
            using (var sysNavItemDao = new SysNavItemDao())
            {
                return await sysNavItemDao.GetAllAsync().CountAsync();
            }
        }

        public async Task RemoveSysNavItem(Guid id)
        {
            using (var sysNavItemDao = new SysNavItemDao())
            {
                await sysNavItemDao.RemoveAsync(id);
            }
        }

        public async Task<SysNavItem> GetOneSysNavItemById(Guid id)
        {
            using (var sysNavItemDao = new SysNavItemDao())
            {
                return await sysNavItemDao.GetAllAsync()
                    .Where(m=>m.Id == id)
                    .Include(m=>m.SysNav).FirstAsync();
            }
        }

        public List<SysNavItem> JoinNavItemAndNav()
        {
            using (var sysNavItemDao = new SysNavItemDao())
            {
                return sysNavItemDao.JoinNav();
            }
        }

        public async Task<List<SysNavItemDto>> GetAllSysNavItem()
        {
            using (var sysNavItemDao = new SysNavItemDao())
            {
                return await sysNavItemDao.GetAllAsync().Select(m => new DTO.System.SysNavItemDto()
                {
                    Id = m.Id,
                    NodeName = m.NodeName,
                    NavName = m.SysNav.NavName,
                    NavId = m.NavId,
                    CreateTime = m.CreateTime
                }).OrderByDescending(m => m.NavId).ToListAsync();
            }
        }
    }
}
