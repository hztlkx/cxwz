using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using YcTeam.DAL.WorkFlow;
using YcTeam.IDAL.FlowPlan;
using YcTeam.Models;
using YcTeam.Models.FlowPlan;
using YcTeam.Models.Master;
using YcTeam.Models.Sys;
using YcTeam.Models.WorkFlow;

namespace YcTeam.DAL.FlowPlan
{
    public class PlanMaterialDao : BaseService<Models.FlowPlan.PlanMaterial>, IPlanMaterialDao
    {
        public PlanMaterialDao() : base(new YcContext())
        {

        }
    }
}
