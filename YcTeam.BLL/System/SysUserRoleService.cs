using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using YcTeam.DAL.System;
using YcTeam.IBLL.Master;
using YcTeam.IBLL.System;
using YcTeam.IDAL.System;
using YcTeam.Models;
using YcTeam.Models.Sys;

namespace YcTeam.BLL.System
{
    public class SysUserRoleService : ISysUserRoleService
    {
        /// <summary>
        /// 查询中间表信息
        /// </summary>
        /// <param name="sysUserRoleId"></param>
        /// <returns></returns>
        public async Task<DTO.System.SysUserRoleDto> GetOneSysUserRoleById(Guid sysUserRoleId)
        {
            using (IDAL.System.ISysUserRoleDao sysUserRoleDao = new SysUserRoleDao())
            {
                return await sysUserRoleDao.GetAllAsync()
                    .Where(m => m.Id == sysUserRoleId)
                    .Select(m => new DTO.System.SysUserRoleDto()
                    {
                        Id = m.Id,
                        SysUserId = m.SysUserId,
                        SysRoleId = m.SysRoleId,
                        CreateTime = m.CreateTime,
                    }).FirstAsync();
            }
        }

        /// <summary>
        /// 创建角色
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public async Task CreateSysUserRole(Guid userId,Guid roleId)
        {
            using (var sysUserRoleDao = new SysUserRoleDao())
            {
                await sysUserRoleDao.CreateAsync(new SysUserRole()
                {
                    SysUserId = userId,
                    SysRoleId = roleId
                });
            }
        }

        /// <summary>
        /// 修改角色
        /// </summary>
        /// <param name="sysUserId"></param>
        /// <param name="sysRoleId"></param>
        /// <returns></returns>
        public async Task EditSysUserRole(Guid sysUserId,Guid[] sysRoleId)
        {
            using (var sysUserRoleDao = new SysUserRoleDao())
            {
                using (var sysUserDao= new SysUserDao())
                {
                    var user = await sysUserDao.GetOneByIdAsync(sysUserId);
                    foreach (var userRole in user.SysUserRoles)
                    {
                        if (sysRoleId.Contains(userRole.SysRoleId))
                        {
                            await sysUserRoleDao.EditAsync(userRole, false);
                        }
                    }
                    await sysUserRoleDao.Save();
                }
            }
        }

        /// <summary>
        /// 判断角色存在
        /// </summary>
        /// <param name="sysUserId">用户主键</param>
        /// <param name="sysRoleId">角色主键</param>
        /// <returns></returns>
        public async Task<bool> ExistsSysUserRole(Guid sysUserId,Guid sysRoleId)
        {
            using (IDAL.System.ISysUserRoleDao sysUserRoleDao = new SysUserRoleDao())
            {
                return await sysUserRoleDao.GetAllAsync().AnyAsync(m => m.SysUserId == sysUserId && m.SysRoleId == sysRoleId);
            }
        }

        /// <summary>
        /// 删除中间表信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task RemoveSysUserRole(Guid id)
        {
            using (var sysUserRoleDao = new SysUserRoleDao())
            {
                await sysUserRoleDao.RemoveAsync(id);
            }
        }

        /// <summary>
        /// 查询所有中间表信息
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="asc"></param>
        /// <returns></returns>
        public async Task<List<DTO.System.SysUserRoleDto>> GetAllSysUserRole(int pageIndex = 1,int pageSize = 10, bool asc = true)
        {
            using (var sysUserRoleDao = new SysUserRoleDao())
            {
                return await sysUserRoleDao.GetAllByPageOrderAsync(pageIndex - 1, pageSize,asc).Select(m => new DTO.System.SysUserRoleDto()
                {
                    Id = m.Id,
                    SysUserId = m.SysUserId,
                    SysRoleId = m.SysRoleId,
                    CreateTime = m.CreateTime
                }).ToListAsync();
            }
        }

        public async Task<int> GetDataCount()
        {
            using (var sysUserRoleDao = new SysUserRoleDao())
            {
                return await sysUserRoleDao.GetAllAsync().CountAsync();
            }
        }
    }
}
