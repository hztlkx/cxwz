using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YcTeam.Models.Sys;

namespace YcTeam.Models.Master
{
    public class Project:BaseEntity
    {
        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjectName { get; set; }

        /// <summary>
        /// 项目编码
        /// </summary>
        public string WBSCode{ get;set; }

        /// <summary>
        /// 子项目名称
        /// </summary>
        public string SmallProjectName { get; set; }

        /// <summary>
        /// 子项目编码
        /// </summary>
        public string SmallWBSCode { get; set; }

        /// <summary>
        /// 批复资金
        /// </summary>
        public string Funds { get; set; }

        /// <summary>
        /// 业主项目部
        /// </summary>
        [ForeignKey(nameof(SysDepart))]
        public Guid? SysDepartId { get; set; }

        public SysDepart SysDepart { get; set; }

        /// <summary>
        /// 电压等级
        /// </summary>
        public string VoltageGrade { get; set; }

        /// <summary>
        /// 领料人
        /// </summary>
        public string PickingPeople { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        public string ContactNumber { get; set; }
    }
}
