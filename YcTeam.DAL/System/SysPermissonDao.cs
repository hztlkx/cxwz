using YcTeam.IDAL.Master;
using YcTeam.IDAL.System;
using YcTeam.Models;
using YcTeam.Models.Master;
using YcTeam.Models.Sys;

namespace YcTeam.DAL.System
{
    public class SysPermissionDao : BaseService<SysPermission>, ISysPermission
    {
        public SysPermissionDao() : base(new YcContext())
        {

        }
    }
}
