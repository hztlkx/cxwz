using YcTeam.IDAL.Master;
using YcTeam.IDAL.System;
using YcTeam.Models;
using YcTeam.Models.Master;
using YcTeam.Models.Sys;

namespace YcTeam.DAL.System
{
    public class SysDepartDao : BaseService<SysDepart>, ISysDepartDao
    {
        public SysDepartDao() : base(new YcContext())
        {

        }
    }
}
