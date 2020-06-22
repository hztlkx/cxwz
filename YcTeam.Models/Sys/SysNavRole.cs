using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YcTeam.Models.Sys
{
    /// <summary>
    /// 系统导航
    /// </summary>
    public class SysNavRole : BaseEntity
    {
        /// <summary>
        /// 用户角色
        /// </summary>
        [ForeignKey(nameof(SysRole))]
        public Guid RoleId { get; set; }

        public SysRole SysRole { get; set; }

        /// <summary>
        /// 导航节点 
        /// </summary>
        [ForeignKey(nameof(SysNavItem))]
        public Guid NavItemId { get; set; }

        /// <summary>
        /// 节点深度
        /// </summary>
        public SysNavItem SysNavItem { get; set; }
    }
}
