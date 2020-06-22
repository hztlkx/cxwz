using System;
using System.ComponentModel.DataAnnotations.Schema;
using YcTeam.Models.Master;
using YcTeam.Models.Sys;

namespace YcTeam.Models.FlowPlan
{
    public class Plan : BaseEntity
    {
        /// <summary>
        /// 项目编号
        /// </summary>
        [ForeignKey(nameof(Project))]
        public Guid ProjectId { get; set; }

        /// <summary>
        /// 项目实体
        /// </summary>
        public Project Project { get; set; }


        /// <summary>
        /// 施工项目部
        /// </summary>
        [ForeignKey(nameof(SysDepart))]
        public Guid? SysDepartId { get; set; }

        public SysDepart SysDepart { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }
    }
}
