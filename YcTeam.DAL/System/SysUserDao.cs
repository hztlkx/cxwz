using System;
using System.Collections.Generic;
using System.Linq;
using YcTeam.IDAL.Master;
using YcTeam.IDAL.System;
using YcTeam.Models;
using YcTeam.Models.Master;
using YcTeam.Models.Sys;

namespace YcTeam.DAL.System
{
    public class SysUserDao : BaseService<SysUser>, ISysUserDao
    {
        public SysUserDao() : base(new YcContext())
        {

        }

        public SysUser JoinDepart(string username, string password)
        {
            var t0 = Db.SysUser.Where(m => !m.IsRemoved && m.UserName == username & m.Password == password);
            var t1 = Db.SysDepart.Where(m => !m.IsRemoved);

            //linq
            var list = (
                from user in t0
                join depart in t1
                    on user.SysDepartId equals depart.Id
                select new {user, depart}
            ).ToList();

            return list.Select(m => m.user).FirstOrDefault();
        }

        public List<SysUserRole> JoinUserRole(Guid userId)
        {
            var t1 = Db.SysUserRole.Where(m => !m.IsRemoved && m.SysUserId ==userId);
            var t2 = Db.SysRole.Where(m=>!m.IsRemoved);

            var list = (
                from userRole in t1
                join role in t2
                    on userRole.SysRoleId equals role.Id
                select new { userRole, role }
            ).ToList();

            return list.Select(m => m.userRole).ToList();
        }
    }
}
