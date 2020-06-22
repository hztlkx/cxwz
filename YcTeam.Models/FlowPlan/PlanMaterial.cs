using System;
using System.ComponentModel.DataAnnotations.Schema;
using YcTeam.Models.Master;
using YcTeam.Models.Sys;

namespace YcTeam.Models.FlowPlan
{
    public class PlanMaterial : BaseEntity
    {
        /// <summary>
        /// 计划申请编号
        /// </summary>
        [ForeignKey(nameof(Plan))]
        public Guid PlanId { get; set; }
        
        public Plan Plan { get; set; }

        /// <summary>
        /// 物料编号
        /// </summary>
        [ForeignKey(nameof(Material))]
        public Guid MaterialId { get; set; }

        public Material Material { get; set; }

        /// <summary>
        /// 技术规范编号
        /// </summary>
        public string TechNumber { get; set; }

        /// <summary>
        /// 参考价格
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public int Num { get; set; }

        /// <summary>
        /// 需求日期
        /// </summary>
        public DateTime PlanDate { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public string StatusName { get; set; }

        /// <summary>
        /// 物料备注
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }
    }
}
