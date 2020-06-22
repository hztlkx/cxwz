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
    public class SysUserService : ISysUserService
    {
        public SysUser SysUser = null;
        public List<SysUserRole> SysUserRole = null;
        public List<SysRole> SysRole = null;
        public Guid UserId;
        public Guid SysDepartId;
        public string SysDepartName;
        public int SysDepartType;
        public string RegionCity;
        public string RegionCounty;

        /// <summary>
        /// 查询用户
        /// </summary>
        /// <param name="sysUserId"></param>
        /// <returns></returns>
        public async Task<SysUser> GetOneSysUserById(Guid sysUserId)
        {
            using (IDAL.System.ISysUserDao sysUserDao = new SysUserDao())
            {
                var user = await sysUserDao.GetAllAsync()
                    .Where(m=>m.Id == sysUserId)
                    .Include(m => m.SysDepart)
                    .Include(m => m.SysUserRoles)
                    .Include(m => m.SysUserRoles
                        .Select(a => a.SysRole)).FirstAsync();

                
                return user;
            }
        }

        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="sysUser"></param>
        /// <returns></returns>
        public async Task CreateSysUser(SysUser sysUser)
        {
            using (var sysUserDao = new SysUserDao())
            {
                await sysUserDao.CreateAsync(sysUser);
            }
        }

        /// <summary>
        /// 修改用户
        /// </summary>
        /// <param name="sysUser"></param>
        /// <param name="sysRoleIds"></param>
        /// <returns></returns>
        public async Task EditSysUser(SysUser sysUser,Guid[] sysRoleIds)
        {
            using (var sysUserDao = new SysUserDao())
            {
                var m = await sysUserDao.GetOneByIdAsync(sysUser.Id);
                m.UserName = sysUser.UserName;
                m.RealName = sysUser.RealName;
                m.SysDepartId = sysUser.SysDepartId;
                await sysUserDao.EditAsync(m);

                //中间表处理
                if (sysRoleIds != null)
                {
                    using (var sysUserRoleDao = new SysUserRoleDao())
                    {
                        var sysUserRoleList = sysUserRoleDao.GetAllAsync().Where(a => a.SysUserId == sysUser.Id);
                        //先清空
                        foreach (var item in sysUserRoleList)
                        {
                            await sysUserRoleDao.RemoveAsync(item,false);
                        }
                        await sysUserRoleDao.Save();

                        //添加
                        foreach (var roleId in sysRoleIds)
                        {
                            await sysUserRoleDao.CreateAsync(new SysUserRole()
                            {
                                SysUserId = sysUser.Id,
                                SysRoleId = roleId
                            },false);
                        }
                        await sysUserRoleDao.Save();
                    }
                }
            }
        }

        /// <summary>
        /// 判断角色存在
        /// </summary>
        /// <param name="sysUserId">用户角色主键</param>
        /// <returns></returns>
        public async Task<bool> ExistsSysUser(Guid sysUserId)
        {
            using (IDAL.System.ISysUserDao sysUserDao = new SysUserDao())
            {
                return await sysUserDao.GetAllAsync().AnyAsync(m => m.Id == sysUserId);
            }
        }


        public async Task RemoveSysUser(Guid id)
        {
            using (var sysUserDao = new SysUserDao())
            {
                await sysUserDao.RemoveAsync(id);
            }
        }

        /// <summary>
        /// 角色查询
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="asc"></param>
        /// <returns></returns>
        public async Task<List<DTO.System.SysUserDto>> GetAllSysUser(int pageIndex = 1,int pageSize = 10, bool asc = true)
        {
            using (var sysUserDao = new SysUserDao())
            {
                var userList = sysUserDao.GetAllByPageOrderAsync(pageIndex - 1, pageSize, asc)
                    .Include(m=>m.SysDepart)
                    .Include(m => m.SysUserRoles)
                    .Include(m => m.SysUserRoles.Select(a => a.SysRole))
                    .ToListAsync();

                var list = new List<SysUserDto>();

                foreach (var m in await userList)
                {
                    var roleName = "";
                    foreach (var t in m.SysUserRoles.Where(a=>!a.IsRemoved))
                    {
                        roleName += t.SysRole.RoleName + '、';
                    }

                    list.Add(new SysUserDto()
                    {
                        Id = m.Id,
                        UserName = m.UserName,
                        RealName = m.RealName,
                        SysDepartType = m.SysDepart.DepartType,
                        SysDepartName = m.SysDepart?.DepartName,
                        SysRoleName = roleName.TrimEnd('、'),
                        CreateTime = m.CreateTime
                    });
                }
                return list;
            }
        }

        /// <summary>
        /// 查询总数
        /// </summary>
        /// <returns></returns>
        public async Task<int> GetDataCount()
        {
            using (var sysUserDao = new DAL.System.SysUserDao())
            {
                return await sysUserDao.GetAllAsync().CountAsync();
            }
        }


        /// <summary>
        /// 获取指定区域的部门信息
        /// </summary>
        /// <param name="regionCity"></param>
        /// <param name="regionCounty"></param>
        /// <returns></returns>
        public async Task<List<SysDepartDto>> GetAllSysDeparts(string regionCity,string regionCounty)
        {
            using (var sysDepartDao = new SysDepartDao())
            {
                return await sysDepartDao.GetAllOrderAsync(false)
                    .Where(m=>m.RegionCity == regionCity && m.RegionCounty == regionCounty)
                    .Select(m => new DTO.System.SysDepartDto()
                {
                    Id = m.Id,
                    RegionCounty = m.RegionCounty,
                    RegionCity = m.RegionCity
                }).ToListAsync();
            }
        }

        #region 登录信息
        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="sysUser"></param>
        /// <param name="sysRoleIds"></param>
        /// <returns></returns>
        public async Task Register(SysUser sysUser,Guid[] sysRoleIds)
        {
            using (var userDao = new SysUserDao())
            {
                await userDao.CreateAsync(sysUser);

                Guid userId = sysUser.Id;

                if (sysRoleIds != null)
                {
                    using (var sysUserRoleDao = new SysUserRoleDao())
                    {
                        foreach (var roleId in sysRoleIds)
                        {
                            await sysUserRoleDao.CreateAsync(new SysUserRole()
                            {
                                SysUserId = userId,
                                SysRoleId = roleId
                            });
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="userName">用户名称</param>
        /// <param name="password">用户密码</param>
        /// <returns></returns>
        public bool Login(string userName, string password)
        {
            using (var sysUserDao = new DAL.System.SysUserDao())
            {
                var user = sysUserDao.JoinDepart(userName, password);

                if (user != null)
                {
                    //SysUser = user;
                    UserId = user.Id;
                    SysDepartId = user.SysDepartId;
                    SysDepartType = user.SysDepart.DepartType;
                    SysDepartName = user.SysDepart.DepartName;
                    RegionCity = user.SysDepart.RegionCity;
                    RegionCounty = user.SysDepart.RegionCounty;

                    var userRole = sysUserDao.JoinUserRole(UserId);

                    if (userRole != null)
                    {
                        SysUserRole = userRole;
                        SysRole = userRole.Select(m => m.SysRole).ToList();
                        user.SysUserRoles = userRole;
                        SysUser = user;
                    }
                }

                return user != null;
            }
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="oldPwd"></param>
        /// <param name="newPwd"></param>
        /// <returns></returns>
        public async Task ChangePassword(string userName, string oldPwd, string newPwd)
        {
            using (ISysUserDao sysUserDao = new DAL.System.SysUserDao())
            {
                if (await sysUserDao.GetAllAsync().AnyAsync(m => m.UserName == userName && m.Password == oldPwd))
                {
                    var user = await sysUserDao.GetAllAsync().FirstAsync(m => m.UserName == userName);
                    user.Password = newPwd;
                    await sysUserDao.EditAsync(user);
                }
            }
        }
        #endregion
    }
}
