using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using YcTeam.IDAL.Master;
using YcTeam.IDAL.System;
using YcTeam.Models;
using YcTeam.Models.Master;
using YcTeam.Models.Sys;

namespace YcTeam.DAL.System
{
    public class SysNavItemDao : BaseService<SysNavItem>, ISysNavItemDao
    {
        public SysNavItemDao() : base(new YcContext())
        {

        }

        public List<SysNavItem> JoinNav()
        {
            var t1 = Db.SysNavItem.Where(m => !m.IsRemoved);
            var t2 = Db.SysNav.Where(m => !m.IsRemoved);

            var list =
                t1.Join(t2,
                        navItem => navItem.NavId,
                        nav => nav.Id,
                        (item, nav) => new { item, nav })
                    .ToListAsync();
            list.Wait();
            foreach (var x in list.Result)
            {
                if (x.item.SysNav != null)
                {
                    x.item.SysNav.Id = x.nav.Id;
                    x.item.SysNav.NavName = x.nav.NavName;
                }
            }

            return list.Result.Select(m=>m.item).ToList();
        }
    }
}
