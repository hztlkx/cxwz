using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using YcTeam.Models.Sys;

namespace YcTeam.Models.WorkFlow
{
    public class FlowNode : BaseEntity
    {
        /// <summary>
        /// 节点编号
        /// </summary>
        public int NodeNumber { get; set; }

        /// <summary>
        /// 节点名称
        /// </summary>
        public string NodeName { get; set; }

        /// <summary>
        /// 所属权限
        /// </summary>
        [ForeignKey(nameof(SysRole))]
        public Guid SysRoleId { get; set; }

        /// <summary>
        ///操作人实体
        /// </summary>
        public SysRole SysRole { get; set; }

        /// <summary>
        /// 下节点编号（提交）
        /// </summary>
        public int NextNodeNumber { get; set; }

        /// <summary>
        /// 上节点编号（退回）
        /// </summary>
        public int PreNodeNumber { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }
    }
}
