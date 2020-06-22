using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using YcTeam.Models.Sys;

namespace YcTeam.Models.WorkFlow
{
    public class FlowInstance : BaseEntity
    {
        /// <summary>
        /// 当前节点
        /// </summary>
        [Required]
        [ForeignKey(nameof(FlowNode))]
        public Guid NodeNumber { get; set; }

        public FlowNode FlowNode { get; set; }

        /// <summary>
        /// 流状态
        /// </summary>
        public string StatusName { get; set; }


        /// <summary>
        /// 流程发起人userId
        /// </summary>
        [ForeignKey(nameof(StartUser))]
        public Guid? StartUserId { get; set; }

        /// <summary>
        /// 流程发起人姓名
        /// </summary>
        public SysUser StartUser { get; set; }

        /// <summary>
        /// 当前操作人userId
        /// </summary>
        [ForeignKey(nameof(OperatingUser))]
        public Guid? OperatingUserId { get; set; }

        /// <summary>
        /// 当前操作人姓名
        /// </summary>
        public SysUser OperatingUser { get; set; }

        /// <summary>
        /// 待办角色Id
        /// </summary>
        [ForeignKey(nameof(ToDoUser))]
        public Guid? ToDoUserId { get; set; }

        /// <summary>
        /// 待办角色名称
        /// </summary>
        public SysUser ToDoUser { get; set; }

        /// <summary>
        /// 已操作人编号
        /// </summary>
        [ForeignKey(nameof(OperatedUser))]
        public Guid? OperatedUserId { get; set; }

        /// <summary>
        /// 已操作人
        /// </summary>
        public SysUser OperatedUser { get; set; }

        /// <summary>
        /// 流程更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// 计划流程Id
        /// </summary>
        [ForeignKey(nameof(Plan))]
        public Guid PlanId { get; set; }

        /// <summary>
        /// 计划流程实体
        /// </summary>
        public FlowPlan.Plan Plan { get; set; }
    }
}
