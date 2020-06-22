using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using YcTeam.Models.FlowPlan;
using YcTeam.Models.Master;

namespace YcTeam.DTO.FlowPlan
{
    public class PlanDto
    {
        #region 计划单信息
        [Display(Name = "计划单编号")]
        public Guid PlanId { get; set; }

        [Display(Name = "施工队")]
        public string SysDepartName { get; set; }

        [Display(Name = "施工队编号")]
        public Guid? SysDepartId { get; set; }

        [Display(Name = "备注")]
        public string Note { get; set; }
        #endregion

        #region 计划物料信息
        public PlanMaterialDto PlanMaterialDto { get; set; }
        #endregion

        #region 项目信息
        [Display(Name = "工程编号")]
        public Guid ProjectId { get; set; }

        [Display(Name = "工程名称")]
        public string ProjectName { get; set; }

        [Display(Name = "工程项目组")]
        public List<int> ProjectList { get; set; }

        [Display(Name = "业主项目部")]
        public string SysDepartOwnerName { get; set; }

        [Display(Name = "业主项编号")]
        public Guid? SysDepartOwnerId { get; set; }
        #endregion

        #region 物料信息
        [Display(Name = "物料编号")]
        public Guid? MaterialId { get; set; }

        [Display(Name = "物料编码")]
        public string Code { get; set; }

        [Display(Name = "物料描述")]
        public string Describe { get; set; }

        [Display(Name = "计量单位")]
        public string Unit { get; set; }

        [Display(Name = "计划物料列表")]
        public List<PlanMaterial> PlanMaterialList { get; set; }
        #endregion

        #region 流程信息
        [Display(Name = "流程实体编号")]
        public Guid Id { get; set; }

        [Display(Name = "流程节点编号")]
        public Guid NodeNumber { get; set; }

        [Display(Name = "下一节点编号")]
        public int NextNodeNumber { get; set; }

        [Display(Name = "流程节点名称")]
        public Guid NodeName { get; set; }

        [Display(Name = "发起人")]
        public string StartUser { get; set; }

        [Display(Name = "发起人编号")]
        public Guid StartUserId { get; set; }

        [Display(Name = "上一操作人")]
        public string OperatedUser { get; set; }

        [Display(Name = "上一操作人编号")]
        public Guid OperatedUserId { get; set; }

        [Display(Name = "操作人")]
        public string OperatingUser { get; set; }

        [Display(Name = "操作人编号")]
        public Guid OperatingUserId { get; set; }

        [Display(Name = "下一操作人")]
        public string ToDoUser { get; set; }

        [Display(Name = "下一操作人编号")]
        public Guid ToDoUserId { get; set; }

        [Display(Name = "状态")]
        public string StatusName { get; set; }
        #endregion

        [Display(Name = "更新时间")]
        public DateTime UpdateTime { get; set; }

        [Display(Name = "创建时间")]
        public DateTime CreateTime { get; set; }
    }
}
