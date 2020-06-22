using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YcTeam.DTO.FlowPlan;
using YcTeam.Models.FlowPlan;
using YcTeam.Models.Master;
using YcTeam.Models.Sys;

namespace YcTeam.IBLL.FlowPlan
{
    public interface IPlanService
    {
        Task CreatePlan(Models.FlowPlan.Plan plan);

        Task<PlanMaterial> GetPlanMaterialById(Guid planId);

        Task CreatePlanMaterial(PlanMaterial planMaterial);

        Task<List<PlanMaterial>> GetMaterialByPlan(Guid planId);

        Task<bool> PlanMaterialEdit(PlanMaterial planMaterial);

        Task<bool> PlanMaterialDelete(Guid id);

        Task EditPlan(Models.FlowPlan.Plan plan);

        Task<List<SysDepart>> GetNewSysDepart(Guid projectId);

        Task<List<SysDepart>> GetOtherSysDepart(Guid? departId);

        List<PlanDto> GetAllPlan(Guid sysDepart, string departType);

        List<PlanDto> GetAllPlan(int year,int month,Guid? sysDepart, int pageSize, int pageIndex, bool asc);

        Task<List<Plan>> GetAllByProjectId(Guid id);

        Task<int> GetDataCount();

        Task RemovePlan(Guid id);

        Task SubmitPlan(SysUser sysUser,int status);//提交：1.工程部提交；2.业主项目部提交；3.运检部提交

        Task BackPlan(SysUser sysUser, int status);//提交：1.工程部内审退回；2.业主项目部退回；3.运检部提交

        Task GatherPlan();//汇总
    }
}
