using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YcTeam.Models.Sys
{
    /// <summary>
    /// 系统导航
    /// </summary>
    public class SysNavItem : BaseEntity
    {
        /// <summary>
        /// 节点名称
        /// </summary>
        public string NodeName { get; set; }

        /// <summary>
        /// 父编号
        /// </summary>
        public int Pid { get; set; }

        /// <summary>
        /// 根编号 
        /// </summary>
        public int RootId { get; set; }

        /// <summary>
        /// 节点深度
        /// </summary>
        public int Deep { get; set; }

        /// <summary>
        ///  节点链接
        /// </summary>
        public string NodeUrl { get; set; }

        /// <summary>
        /// 节点排序
        /// </summary>
        [DefaultValue(0)]
        public int NodeOrd { get; set; }

        /// <summary>
        /// 节点图标
        /// </summary>
        public string NodeIcons { get; set; }

        /// <summary>
        /// 所属导航
        /// </summary>
        [ForeignKey(nameof(SysNav))]
        public Guid NavId { get; set; }

        public SysNav SysNav { get; set; }
    }
}
