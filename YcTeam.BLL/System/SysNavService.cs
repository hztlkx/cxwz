using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YcTeam.DAL.System;
using YcTeam.DTO.System;
using YcTeam.IBLL.System;
using YcTeam.Models.Sys;

namespace YcTeam.BLL.System
{
    public class SysNavService : ISysNavService
    {
        public int GetMaxOrd()
        {
            using (var sysNavDao = new SysNavDao())
            {
                var query = sysNavDao.GetAllAsync().Select(m => m.NavOrd).DefaultIfEmpty();
                return query.Max();
            }
        }

        public async Task CreateSysNav(SysNav sysNav)
        {
            using (var sysNavDao = new SysNavDao())
            {
                await sysNavDao.CreateAsync(sysNav);
            }
        }

        public async Task EditSysNav(SysNav sysNav)
        {
            using (var sysNavDao = new SysNavDao())
            {
                await sysNavDao.EditAsync(sysNav);
            }
        }

        public async Task<List<SysNavDto>> GetAllSysNav(int pageIndex, int pageSize, bool asc)
        {
            using (var sysNavDao = new SysNavDao())
            {
                return await sysNavDao.GetAllByPageOrderAsync(pageIndex - 1, pageSize, asc)
                    .OrderByDescending(m=>m.NavOrd)
                    .Select(m => new DTO.System.SysNavDto()
                {
                    Id = m.Id,
                    NavIcons = m.NavIcons,
                    NavName = m.NavName,
                    NavOrd = m.NavOrd,
                    NavUrl = m.NavUrl,
                    CreateTime = m.CreateTime
                }).OrderBy(m => m.NavOrd).ToListAsync();
            }
        }

        /// <summary>
        /// 获取所有导航
        /// </summary>
        /// <returns></returns>
        public async Task<List<SysNavItemDto>> GetAllSysNav()
        {
            using (var sysSysNavDao = new SysNavDao())
            {
                return await sysSysNavDao.GetAllOrderAsync(false)
                    .Select(m => new SysNavItemDto()
                    {
                        Id = m.Id,
                        NavId = m.Id,
                        NavName = m.NavName,
                    }).ToListAsync();
            }
        }

        public async Task<int> GetDataCount()
        {
            using (var sysNavDao = new SysNavDao())
            {
                return await sysNavDao.GetAllAsync().CountAsync();
            }
        }

        public async Task RemoveSysNav(Guid id)
        {
            using (var sysNavDao = new SysNavDao())
            {
                await sysNavDao.RemoveAsync(id);
            }
        }

        public async Task<DTO.System.SysNavDto> GetOneSysNavById(Guid id)
        {
            using (var sysNavDao = new SysNavDao())
            {
                var model = await sysNavDao.GetOneByIdAsync(id);

                return new SysNavDto()
                {
                    Id = model.Id,
                    NavUrl = model.NavUrl,
                    NavIcons = model.NavIcons,
                    NavName = model.NavName,
                    NavOrd = model.NavOrd,
                    CreateTime = model.CreateTime
                };
            }
        }
    }
}
