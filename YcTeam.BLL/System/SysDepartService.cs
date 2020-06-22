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
    public class SysDepartService : ISysDepartService
    {
        /// <summary>
        /// 查询角色
        /// </summary>
        /// <param name="sysDepartId"></param>
        /// <returns></returns>
        public async Task<DTO.System.SysDepartDto> GetOneSysDepartById(Guid sysDepartId)
        {
            using (IDAL.System.ISysDepartDao sysDepartDao = new SysDepartDao())
            {
                return await sysDepartDao.GetAllAsync()
                    .Where(m => m.Id == sysDepartId)
                    .Select(m => new DTO.System.SysDepartDto()
                    {
                        Id = m.Id,
                        DepartName = m.DepartName,
                        DepartType = m.DepartType,
                        RegionCounty = m.RegionCounty,
                        RegionCity = m.RegionCity,
                        CreateTime = m.CreateTime,
                    }).FirstAsync();
            }
        }

        /// <summary>
        /// 创建部门
        /// </summary>
        /// <param name="departName"></param>
        /// <param name="regionCity"></param>
        /// <param name="regionCounty"></param>
        /// <param name="departType"></param>
        /// <returns></returns>
        public async Task CreateSysDepart(string departName, string regionCity, string regionCounty,int departType)
        {
            using (var sysDepartDao = new SysDepartDao())
            {
                await sysDepartDao.CreateAsync(new SysDepart()
                {
                    DepartType = departType,
                    DepartName = departName,
                    RegionCity = regionCity,
                    RegionCounty = regionCounty
                });
            }
        }

        /// <summary>
        /// 修改部门
        /// </summary>
        /// <param name="sysDepartId"></param>
        /// <param name="departName"></param>
        /// <param name="regionCity"></param>
        /// <param name="regionCounty"></param>
        /// <param name="departType"></param>
        /// <returns></returns>
        public async Task EditSysDepart(Guid sysDepartId, string departName, string regionCity, string regionCounty,int departType)
        {
            using (var sysDepartDao = new SysDepartDao())
            {
                var sysDepart = await sysDepartDao.GetOneByIdAsync(sysDepartId);
                sysDepart.DepartName = departName;
                sysDepart.DepartType = departType;
                sysDepart.RegionCity = regionCity;
                sysDepart.RegionCounty = regionCounty;
                await sysDepartDao.EditAsync(sysDepart);
            }
        }

        /// <summary>
        /// 判断部门存在
        /// </summary>
        /// <param name="sysDepartId"></param>
        /// <returns></returns>
        public async Task<bool> ExistsSysDepart(Guid sysDepartId)
        {
            using (IDAL.System.ISysDepartDao sysDepartDao = new SysDepartDao())
            {
                return await sysDepartDao.GetAllAsync().AnyAsync(m => m.Id == sysDepartId);
            }
        }

        public async Task RemoveSysDepart(Guid id)
        {
            using (var sysDepartDao = new SysDepartDao())
            {
                await sysDepartDao.RemoveAsync(id);
            }
        }

        /// <summary>
        /// 部门查询
        /// </summary>
        /// <returns></returns>
        public async Task<List<DTO.System.SysDepartDto>> GetAllSysDepart()
        {
            using (var sysDepartDao = new SysDepartDao())
            {
                return await sysDepartDao.GetAllAsync().Select(m=>new SysDepartDto
                {
                    Id = m.Id,
                    DepartType = m.DepartType,
                    RegionCity = m.RegionCity,
                    RegionCounty = m.RegionCounty,
                    DepartName = m.DepartName,
                    CreateTime = m.CreateTime
                }).OrderByDescending(m=>m.DepartType).ThenByDescending(m=>m.CreateTime).ToListAsync();
            }
        }

        /// <summary>
        /// 按部门类型获取部门信息
        /// </summary>
        /// <returns></returns>
        public async Task<List<SysDepart>> GetAllSysDepart(int departType)
        {
            using (var sysDepartDao = new SysDepartDao())
            {
                return await sysDepartDao.GetAllAsync()
                    .Where(m=>m.DepartType == departType)
                    .OrderByDescending(m => m.DepartType).ThenByDescending(m => m.CreateTime).ToListAsync();
            }
        }


        /// <summary>
        /// 部门查询
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="asc"></param>
        /// <returns></returns>
        public async Task<List<DTO.System.SysDepartDto>> GetAllSysDepart(int pageIndex = 1,int pageSize = 10, bool asc = true)
        {
            using (var sysDepartDao = new SysDepartDao())
            {
                return await sysDepartDao.GetAllByPageOrderAsync(pageIndex - 1, pageSize,asc).Select(m => new DTO.System.SysDepartDto()
                {
                    Id = m.Id,
                    RegionCity = m.RegionCity,
                    RegionCounty = m.RegionCounty,
                    DepartName = m.DepartName,
                    DepartType = m.DepartType,
                    CreateTime = m.CreateTime
                }).OrderByDescending(m=>m.DepartType).ThenByDescending(m=>m.CreateTime).ToListAsync();
            }
        }

        public async Task<int> GetDataCount()
        {
            using (var sysDepartDao = new SysDepartDao())
            {
                return await sysDepartDao.GetAllAsync().CountAsync();
            }
        }
    }
}
