using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using YcTeam.DAL.FlowPlan;
using YcTeam.DAL.WorkFlow;
using YcTeam.DTO.FlowPlan;
using YcTeam.DTO.System;
using YcTeam.DTO.WorkFlow;
using YcTeam.IBLL.FlowPlan;
using YcTeam.Models.FlowPlan;
using YcTeam.Models.Master;
using YcTeam.Models.Sys;
using YcTeam.Models.WorkFlow;

namespace YcTeam.BLL.FlowPlan
{
    public class PlanService : IPlanService
    {
        public async Task CreatePlan(Plan plan)
        {
            using (var planDao = new PlanDao())
            {
               await planDao.CreateAsync(plan);
            }
        }

        public async Task<bool> PlanMaterialEdit(PlanMaterial planMaterial)
        {
            using (var planMaterialDao = new PlanMaterialDao())
            {
               var result = await planMaterialDao.EditAsync(planMaterial);
               if (result > -1)
               {
                    return true;
               }
            }
            return false;
        }

        public async Task<bool> PlanMaterialDelete(Guid id)
        {
            using (var planMaterialDao = new PlanMaterialDao())
            {
                var result = await planMaterialDao.RemoveAsync(id);
                if (result > -1)
                {
                    return true;
                }
            }
            return false;
        }

        public async Task<List<PlanMaterial>> GetMaterialByPlan(Guid planId)
        {
            using (var planDao = new PlanDao())
            {
                return await planDao.JoinPlanId(planId);
            }
        }

        public async Task<PlanMaterial> GetPlanMaterialById(Guid planId)
        {
            using (var planMaterialDao = new PlanMaterialDao())
            {
                return await planMaterialDao.GetOneByIdAsync(planId);
            }
        }

        public async Task CreatePlanMaterial(PlanMaterial planMaterial)
        {
            using (var planMaterialDao = new PlanMaterialDao())
            {
                await planMaterialDao.CreateAsync(planMaterial);
            }
        }


        public async Task EditPlan(Plan plan)
        {
            using (var planDao = new PlanDao())
            {
                await planDao.EditAsync(plan);
            }
        }

        public List<Project> GetProject()
        {
            using (var planDao = new PlanDao())
            {
                return planDao.GetAllAsync()
                    .Where(m=>m.UpdateTime.Year == DateTime.Now.Year && m.UpdateTime.Month == DateTime.Now.Month )
                    .Select(m=>new Project()
                    {
                        ProjectName = m.Project.ProjectName
                    }).ToList();
            }
        }

        public List<PlanDto> GetAllPlan(int year,int month,Guid? sysDepartId,int pageSize, int pageIndex, bool asc)
        {
            using (var planDao = new PlanDao())
            {
                return planDao.JoinProject(year,month,sysDepartId).Result
                    .Select(m => new PlanDto()
                    {
                        Id = m.Id,
                        NodeNumber = m.NodeNumber,
                        NextNodeNumber = m.FlowNode.NextNodeNumber,
                        PlanId = m.Plan.Id,
                        ProjectId = m.Plan.ProjectId,
                        ProjectName = m.Plan.Project.ProjectName,
                        StatusName = m.StatusName,
                        SysDepartId = m.Plan.SysDepartId,
                        SysDepartName = m.Plan.SysDepart.DepartName,
                        SysDepartOwnerId=m.Plan.Project.SysDepartId,
                        SysDepartOwnerName =m.Plan.Project.SysDepart.DepartName,
                        UpdateTime = m.UpdateTime,
                        CreateTime = m.CreateTime
                    })
                    .OrderByDescending(m=>m.CreateTime)
                    .ThenBy(m => m.SysDepartId)
                    .ToList();
            }
        }

        public List<PlanDto> GetAllPlan(Guid sysDepart,string departType)
        {
            using (var planDao = new PlanDao())
            {
                return planDao.JoinDepart(sysDepart, departType).Select(m=>new PlanDto()
                {
                     Id = m.Id,
                     ProjectId = m.ProjectId,
                     ProjectName = m.Project.ProjectName,
                     SysDepartOwnerName = m.Project.SysDepart.DepartName,
                     SysDepartOwnerId = m.Project.SysDepartId,
                     SysDepartId = m.SysDepartId,
                     SysDepartName = m.SysDepart?.DepartName,
                     CreateTime = m.CreateTime
                })
                .OrderByDescending(m=>m.CreateTime)
                .ThenBy(m => m.SysDepartId)
                .ToList();
            }
        }

        public async Task<List<Plan>> GetAllByProjectId(Guid id)
        {
            using (var planDao = new PlanDao())
            {
                return await planDao.GetAllAsync().Where(m => m.ProjectId == id).ToListAsync();
            }
        }

        public Task<int> GetDataCount()
        {
            throw new NotImplementedException();
        }

        public async Task RemovePlan(Guid id)
        {
            using (var planDao = new PlanDao())
            {
                await planDao.RemoveAsync(id);
            }
        }

        public Task SubmitPlan(SysUser sysUser, int status)
        {
            using (var planDao = new PlanDao())
            {
                throw new NotImplementedException();
            }
        }

        public Task BackPlan(SysUser sysUser, int status)
        {
            throw new NotImplementedException();
        }

        public Task GatherPlan()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获取单位项目中，未添加的施工项目部
        /// </summary>
        /// <param name="projectId">项目编号</param>
        /// <returns></returns>
        public async Task<List<SysDepart>> GetNewSysDepart(Guid projectId)
        {
            using (var planDao = new PlanDao())
            {
                return await planDao.GetNewSysDepart(projectId);
            }
        }

        /// <summary>
        /// 获取其他施工项目部，排除已选施工项目部
        /// </summary>
        /// <param name="departId"></param>
        /// <returns></returns>
        public async Task<List<SysDepart>> GetOtherSysDepart(Guid? departId)
        {
            using (var planDao = new PlanDao())
            {
                return await planDao.GetOtherSysDepart(departId);
            }
        }
    }
}
