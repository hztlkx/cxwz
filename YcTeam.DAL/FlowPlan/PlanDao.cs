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
    public class PlanDao : BaseService<Models.FlowPlan.Plan>, IPlanDao
    {
        public PlanDao() : base(new YcContext())
        {

        }

        public async Task<List<PlanMaterial>> JoinPlanId(Guid planId)
        {
            var t1 = Db.PlanMaterial.Where(m => !m.IsRemoved);
            var t2 = Db.Plan.Where(m => !m.IsRemoved & m.Id == planId);
            var t3 = Db.Material.Where(m => !m.IsRemoved);

            var list = (
                    from planMaterial in t1
                    join material in t3
                        on planMaterial.MaterialId equals material.Id
                    select new {planMaterial,material}
            );

            var s = list.ToList();

            return await list.Select(m => m.planMaterial)
                .OrderByDescending(m => m.UpdateTime)
                .ToListAsync();
        }

        /// <summary>
        /// 获取项目集合
        /// </summary>
        /// <returns></returns>
        public async Task<List<Project>> GetProject()
        {
            var t1 = Db.Plan.Where(m=>!m.IsRemoved &
                                      m.UpdateTime.Year == DateTime.Now.Year & m.UpdateTime.Month == DateTime.Now.Month);
            var t2 = Db.Project.Where(m => !m.IsRemoved);

            var list = t1.Join(t2,
                            plan => plan.ProjectId,
                            project => project.Id,
                            (navRole, navItem) => new {navRole, navItem});
            return await list.Select(m=>m.navItem).ToListAsync();
        }


        public async Task SubmitFlow(Plan plan,FlowNode flowNode,SysUser sysUser,int status)
        {
            await CreateAsync(plan);
            using (var flowInstanceDao = new FlowInstanceDao())
            {
                await flowInstanceDao.CreateAsync(new FlowInstance()
                { 
                    FlowNode = flowNode,
                    StartUser = sysUser,
                    StartUserId = sysUser.Id,
                    OperatingUser = sysUser,
                    OperatingUserId = sysUser.Id,
                    Plan = plan,
                    CreateTime = DateTime.Now
                });
            }
        }

        /// <summary>
        /// 获取单位项目的需求计划清单
        /// </summary>
        /// <returns></returns>
        public List<FlowRecord> JoinFlow(Guid currentUserId)
        {
            var t0 = Db.FlowRecord.Where(m=>!m.IsRemoved && m.FlowUserId == currentUserId && m.FlowInstance!= null);
            var t2 = Db.Plan.Where(m => !m.IsRemoved);
            var t3 = Db.FlowInstance.Where(m => !m.IsRemoved);
            var t4 = Db.FlowNode.Where(m => !m.IsRemoved);
            var t5 = Db.Project;//删除的项目，作为历史保留显示出来
            var t6 = Db.Material;//删除的物料，作为历史保留显示出来
            var t7 = Db.SysUser;//删除的用户，作为历史保留显示出来
            var t8 = Db.SysDepart;//删除的部门，作为历史保留显示出来
            var t9 = Db.PlanMaterial.Where(m=>!m.IsRemoved);

            #region 【工作流实体】关联【工作节点/系统用户】
            var flowInstanceList = (
                from flowInstance in t3
                join flowNode in t4
                    on flowInstance.NodeNumber equals flowNode.Id //into g1
                    //from flowNode in g1.DefaultIfEmpty()
                join startUser in t7
                    on flowInstance.StartUserId equals startUser.Id //into g2//发起人
                    //from startUser in g2.DefaultIfEmpty()
                join operatedUser in t7
                    on flowInstance.OperatedUserId equals operatedUser.Id //into g3//上一操作人
                    //from operatedUser in g3.DefaultIfEmpty()
                join operatingUser in t7
                    on flowInstance.OperatingUserId equals operatingUser.Id //into g4//操作人
                    //from operatingUser in g4.DefaultIfEmpty()
                join toDoUser in t7
                    on flowInstance.OperatingUserId equals toDoUser.Id //into g5//下一操作人
                    //from toDoUser in g5.DefaultIfEmpty()
                select new
                {
                    flowInstance,
                    flowNode,
                    startUser,
                    operatedUser,
                    operatingUser,
                    toDoUser
                }
            );

            //外键实体填充
            foreach (var x in flowInstanceList)
            {
                x.flowInstance.FlowNode = x.flowNode;
                x.flowInstance.StartUser = x.startUser;
                x.flowInstance.OperatingUser = x.operatingUser;
                x.flowInstance.OperatedUser = x.operatedUser;
                x.flowInstance.ToDoUser = x.toDoUser;
            }


            #endregion

            #region 【计划申请】关联【项目信息/物料信息】
            var planList = (
                from plan in t2
                //关联项目
                join project in t5
                    on plan.ProjectId equals project.Id //into g1
                //关联部门
                join sysDepart in t8
                    on plan.SysDepartId equals sysDepart.Id //into g2
                select new { plan, project, sysDepart }
            );

            //外键实体填充
            foreach (var x in planList)
            {
                x.plan.SysDepart = x.sysDepart;
                x.plan.Project = x.project;
            }
            #endregion

            #region 【工作流实体】关联【计划申请】
            var list = (
                from flowInstance in flowInstanceList.Select(m=>m.flowInstance)
                join plan in planList.Select(m=>m.plan)
                    on flowInstance.PlanId equals plan.Id
                select new { flowInstance, plan }
            ).ToList();
            
            //外键实体填充
            foreach (var x in list)
            {
                x.flowInstance.Plan = x.plan;
            }
            #endregion

            #region 【工作流记录】关联【工作流实体】
            var recordList = (
                from flowRecord in t0
                join flowInstance in flowInstanceList.Select(m=>m.flowInstance)
                    on flowRecord.FlowInstanceId equals flowInstance.Id
                select new { flowRecord , flowInstance }
            );

            //外键实体填充
            foreach (var x in recordList)
            {
                x.flowRecord.FlowInstance = x.flowInstance;
            }
            #endregion

            return recordList.Select(m => m.flowRecord).OrderByDescending(m => m.CreateTime).ToList();
        }

        /// <summary>
        /// 获取带有施工队的计划单
        /// </summary>
        /// <param name="sysDepartId">查看部门</param>
        /// <param name="departType">部门类型1：施工队查看 2：业主项目部查看</param>
        /// <returns></returns>
        public List<Plan> JoinDepart(Guid sysDepartId,string departType)
        {
            var t0 = Db.Plan.Where(m => !m.IsRemoved);//计划
            var t1 = Db.SysDepart.Where(m => !m.IsRemoved & m.DepartType == 1);//施工队
            var t2 = Db.Project.Where(m => !m.IsRemoved);//项目
            var t3 = Db.SysDepart.Where(m => !m.IsRemoved & m.DepartType == 2);//业主项目部

            if (departType.Equals("1"))
            {
                t0 = Db.Plan.Where(m => !m.IsRemoved && m.SysDepartId == sysDepartId);//计划
            }
            if (departType.Equals("2"))
            {
                t2 = Db.Project.Where(m => !m.IsRemoved && m.SysDepartId == sysDepartId);//项目
            }
            

            var projectList = (
                from project in t2
                join depart in t3
                     on project.SysDepartId equals depart.Id
                select new {project,depart}
            );

            foreach (var x in projectList)
            {
                x.project.SysDepart = x.depart;
            }

            var list = (
                from plan in t0  //计划
                join project in projectList.Select(m=>m.project)
                    on plan.ProjectId equals project.Id
                join depart in t1
                    on plan.SysDepartId equals depart.Id into g1 //施工队
                    from depart in g1.DefaultIfEmpty()
                select new { plan, depart, project}
            ).ToListAsync();

            foreach (var x in list.Result)
            {
                x.plan.SysDepart = x.depart;
            }

            return list.Result.Select(m => m.plan)
                .OrderBy(m=>m.ProjectId)
                .ThenByDescending(m=>m.CreateTime).ToList();
        }


        public async Task<List<FlowInstance>> JoinProject(int year,int month,Guid? sysDepartId)
        {
            var t0 = Db.FlowInstance.Where(m=>!m.IsRemoved);
            var t1 = Db.Plan.Where(m => !m.IsRemoved 
                                        && m.UpdateTime.Year == year
                                        && m.UpdateTime.Month == month);//计划
            var t2 = Db.Project.Where(m => !m.IsRemoved);//项目
            var t3 = Db.SysDepart.Where(m => !m.IsRemoved & m.DepartType == 1);//施工项目部
            var t4 = Db.SysDepart.Where(m => !m.IsRemoved & m.DepartType == 2);//业主项目部
            var t5 = Db.FlowNode.Where(m=>!m.IsRemoved);

            if (sysDepartId != null)
            {
                t1 = t1.Where(m=>m.SysDepartId == sysDepartId);
            }

            var list = (
                    from flowInstance in t0
                    join flowNode in t5
                        on flowInstance.NodeNumber equals flowNode.Id
                    join plan in t1
                        on flowInstance.PlanId equals plan.Id
                    join departConstruction in t3
                        on plan.SysDepartId equals departConstruction.Id
                    join project in t2
                        on plan.ProjectId equals project.Id
                    join departOwner in t4
                        on project.SysDepartId equals departOwner.Id
                    select new { flowNode,flowInstance, plan, departConstruction ,project,departOwner }
            );
            var test = list.ToList();
            return await list.Select(m => m.flowInstance)
                .OrderBy(m =>m.Plan.ProjectId)
                .ThenByDescending(m => m.CreateTime).ToListAsync();
        }

        /// <summary>
        /// 获取单位项目中，未添加的施工项目部
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public async Task<List<SysDepart>> GetNewSysDepart(Guid projectId)
        {
            var t1 = Db.Plan.Where(m=>!m.IsRemoved && m.ProjectId == projectId).Select(m=>m.SysDepartId);
            var t2 = Db.SysDepart.Where(m=>!m.IsRemoved && m.DepartType == 1);

            List<SysDepart> list = new List<SysDepart>();
            return await t2.Where(m=>!t1.Contains(m.Id)).ToListAsync();
        }

        /// <summary>
        /// 获取单位项目中，未添加的施工项目部
        /// </summary>
        /// <param name="departId"></param>
        /// <returns></returns>
        public async Task<List<SysDepart>> GetOtherSysDepart(Guid? departId)
        {
            var t1 = Db.SysDepart.Where(m => !m.IsRemoved && m.DepartType == 1 && m.Id != departId);
            return await t1.ToListAsync();
        }
    }
}
