using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
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
    public class SysRoleService : ISysRoleService
    {
        /// <summary>
        /// 查询角色
        /// </summary>
        /// <param name="sysRoleId"></param>
        /// <returns></returns>
        public async Task<DTO.System.SysRoleDto> GetOneSysRoleById(Guid sysRoleId)
        {
            using (IDAL.System.ISysRoleDao sysRoleDao = new SysRoleDao())
            {
                return await sysRoleDao.GetAllAsync()
                    .Where(m => m.Id == sysRoleId)
                    .Select(m => new DTO.System.SysRoleDto()
                    {
                        Id = m.Id,
                        RoleName = m.RoleName,
                        SortOrder = m.SortOrder,
                        CreateTime = m.CreateTime,
                    }).FirstAsync();
            }
        }

        public int GetMaxOrd()
        {
            using (var sysNavDao = new SysNavDao())
            {
                var query = sysNavDao.GetAllAsync().Select(m => m.NavOrd).DefaultIfEmpty();
                return query.Max();
            }
        }

        /// <summary>
        /// 创建角色
        /// </summary>
        /// <param name="roleName"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        public async Task CreateSysRole(string roleName , int sortOrder)
        {
            using (var sysRoleDao = new SysRoleDao())
            {
                await sysRoleDao.CreateAsync(new SysRole()
                {
                    RoleName = roleName,
                    SortOrder = sortOrder
                });
            }
        }

        //public async Task EditSysRole(Guid sysRoleId, string name)
        //{
        //    throw new NotImplementedException();
        //}

        /// <summary>
        /// 修改角色
        /// </summary>
        /// <param name="sysRoleId"></param>
        /// <param name="roleName"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        public async Task EditSysRole(Guid sysRoleId, string roleName, int sortOrder)
        {
            using (var sysRoleDao = new SysRoleDao())
            {
                var sysRole = await sysRoleDao.GetOneByIdAsync(sysRoleId);
                sysRole.RoleName = roleName;
                sysRole.SortOrder = sortOrder;
                await sysRoleDao.EditAsync(sysRole);
            }
        }

        /// <summary>
        /// 判断角色存在
        /// </summary>
        /// <param name="sysRoleId">用户角色主键</param>
        /// <returns></returns>
        public async Task<bool> ExistsSysRole(Guid sysRoleId)
        {
            using (IDAL.System.ISysRoleDao sysRoleDao = new SysRoleDao())
            {
                return await sysRoleDao.GetAllAsync().AnyAsync(m => m.Id == sysRoleId);
            }
        }

        //public async Task CreateSysRole(string roleName, int sortOrder)
        //{
        //    using (var sysRoleDao = new SysRoleDao())
        //    {
        //        await sysRoleDao.CreateAsync(new SysRole()
        //        {
        //            RoleName = roleName,
        //            SortOrder = sortOrder,
                    
        //        });
        //    }
        //}


        public async Task RemoveSysRole(Guid id)
        {
            using (var sysRoleDao = new SysRoleDao())
            {
                await sysRoleDao.RemoveAsync(id);
            }
        }

        /// <summary>
        /// 角色查询
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="asc"></param>
        /// <returns></returns>
        public async Task<List<DTO.System.SysRoleDto>> GetAllSysRole(int pageIndex = 1,int pageSize = 10, bool asc = true)
        {
            using (var sysRoleDao = new SysRoleDao())
            {
                return await sysRoleDao.GetAllByPageOrderAsync(pageIndex - 1, pageSize,asc).Select(m => new DTO.System.SysRoleDto()
                {
                    Id = m.Id,
                    RoleName = m.RoleName,
                    SortOrder = m.SortOrder,
                    CreateTime = m.CreateTime
                }).OrderByDescending(m=>m.SortOrder).ToListAsync();
            }
        }

        public async Task<List<DTO.System.SysRoleDto>> GetAllSysRole()
        {
            using (var sysRoleDao = new SysRoleDao())
            {
                return await sysRoleDao.GetAllAsync().Select(m => new DTO.System.SysRoleDto()
                {
                    Id = m.Id,
                    RoleName = m.RoleName,
                    SortOrder = m.SortOrder,
                    CreateTime = m.CreateTime
                }).OrderByDescending(m => m.SortOrder).ToListAsync();
            }
        }

        public async Task<int> GetDataCount()
        {
            using (var sysRoleDao = new SysRoleDao())
            {
                return await sysRoleDao.GetAllAsync().CountAsync();
            }
        }

        public List<SysNavRole> GetSysNavRole()
        {
            using (var sysNavRoleDao = new SysNavRoleDao())
            {
                var list = sysNavRoleDao.GetNavRoles();
                
                //返回主键实体
                return sysNavRoleDao.GetNavRoles();
            }
        }

        public async Task<List<SysNavRole>> GetSysNavRole(Guid[] sysRoleId)
        {
            using (var sysNavRoleDao = new SysNavRoleDao())
            {
                //返回主键实体
                return await sysNavRoleDao.GetNavRoles(sysRoleId);
            }
        }
    }
}
