using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using YcTeam.IDAL.Master;
using YcTeam.IDAL.System;
using YcTeam.Models;
using YcTeam.Models.Master;
using YcTeam.Models.Sys;

namespace YcTeam.DAL.System
{
    public class SysNavRoleDao : BaseService<SysNavRole>, ISysNavRoleDao
    {
        public SysNavRoleDao() : base(new YcContext())
        {
           
        }

        public List<SysNavRole> GetHomeNavRoles(Guid[] roleId)
        {
            var t1 = Db.SysNavRole.Where(m => !m.IsRemoved && roleId.Contains(m.RoleId));
            var t2 = Db.SysNavItem.Where(m => !m.IsRemoved);
            var t3 = Db.SysNav.Where(m => !m.IsRemoved);

            //内连接inner join，返回所有字段
            var list =
                t1.Join(t2,
                        navRole => navRole.NavItemId,
                        navItem => navItem.Id,
                        (navRole, navItem) => new { navRole, navItem }) //内连接表NavItem
                .Join(t3,
                        group => group.navItem.SysNav.Id,
                        nav => nav.Id,
                        (group, nav) => new { group.navRole, group.navItem, nav })//内连接表Nav
                .ToList();

            //外键实体填充
            foreach (var x in list)
            {
                if (x.navRole?.SysNavItem != null)
                {
                    x.navRole.SysNavItem = x.navItem;
                    x.navRole.SysNavItem.SysNav = x.nav;
                }
            }

            return list.Select(m => m.navRole).OrderBy(m => m.SysNavItem.SysNav.NavOrd)
                .ThenBy(m=>m.SysNavItem.NodeOrd).ToList();
        }

        public async Task<List<SysNavRole>> GetNavRoles(Guid[] roleId)
        {
            return await GetAllAsync().Where(m => roleId.Contains(m.RoleId)).ToListAsync();
        }

        public List<SysNavRole> GetNavRoles()
        {
            var t1 = Db.SysNavRole.Where(m=>!m.IsRemoved);
            var t2 = Db.SysNavItem.Where(m => !m.IsRemoved);
            var t3 = Db.SysNav.Where(m => !m.IsRemoved);

            //linq
            var list = (
                from navItem in t2
                join navRole in t1
                    on navItem.Id equals navRole.NavItemId into g1
                from navRole in g1.DefaultIfEmpty()
                join nav in t3
                    on navItem.NavId equals nav.Id into g2
                from nav in g2.DefaultIfEmpty()
                select new {navRole, navItem, nav}
            ).ToList();


            //foreach (var x in list)
            //{
            //    if (x.navRole?.SysNavItem != null)
            //    {
            //        x.navRole.SysNavItem = x.navItem;
            //        x.navRole.SysNavItem.SysNav = x.nav;
            //    }
            //}
            return list.Select(m => m.navRole).ToList();

            //var list4 = (from navItem in Db.SysNavItem.DefaultIfEmpty()
            //    join navRole in Db.SysNavRole.DefaultIfEmpty()
            //        on navItem.Id equals navRole.NavItemId into GuaiTai
            //    where navItem.IsRemoved == false && navItem.IsRemoved == false
            //    select new
            //    {
            //        //NavRole = navRole.RoleId,
            //        //NavItemId = navRole.NavItemId,
            //        GuaiTai = GuaiTai,
            //        NavItemId2 = navItem.Id,
            //        NavItemName = navItem.NodeName
            //    }).ToList();

            //var t1 = Db.SysNavItem.Where(m => !m.IsRemoved);
            //var t2 = Db.SysNavRole.Where(m => !m.IsRemoved);

            ////左连接，返回所有字段
            //var list2 = t1.GroupJoin(t2,
            //    a => a.Id,
            //    b => b.NavItemId,
            //    (a, b) => new {a,b}).ToList();

            ////左连接后，再选择字段
            //var list3 = t1
            //    .GroupJoin(t2, 
            //    (SysNavItem p) => p.Id,
            //    (SysNavRole c) => c.NavItemId,
            //    (prod, cs) => new { prod, cs }) // cs is IEnumerable<Category>
            //    .SelectMany(prodCats => prodCats.cs.DefaultIfEmpty(), (prodCats, c) =>
            //        new
            //        {
            //            NavRoleId = prodCats.cs.Count(role =>role.RoleId != null),
            //            prodCats.prod.NodeName,
            //            prodCats.prod.SysNav.NavName
            //        }).ToList();

        }
    }
}
