using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using YcTeam.Models.Sys;

namespace YcTeam.Models.WorkFlow
{
    public class FlowRecord : BaseEntity
    {
        /// <summary>
        /// 流程实例Id
        /// </summary>
        [ForeignKey(nameof(FlowInstance))]
        public Guid FlowInstanceId { get; set; }

        //流程实例
        public FlowInstance FlowInstance { get; set; }

        /// <summary>
        /// 已完成流程的用户编号
        /// </summary>
        [ForeignKey(nameof(FlowUser))]
        public Guid FlowUserId { get; set; }

        /// <summary>
        /// 已完成流程的用户
        /// </summary>
        public SysUser FlowUser { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }


        /// <summary>
        /// 是否读取
        /// </summary>
        public bool IsRead { get; set; }

        /// <summary>
        /// 是否通过
        /// </summary>
        public bool IsPass { get; set; }
    }
}
