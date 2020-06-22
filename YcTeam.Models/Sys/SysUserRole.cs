using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YcTeam.Models.Sys
{
    /// <summary>
    /// 用户角色中间表
    /// </summary>
    public class SysUserRole : BaseEntity
    {
        /// <summary>
        /// 用户编号
        /// </summary>
        [ForeignKey(nameof(SysUser))]
        public Guid SysUserId { get; set; }

        public SysUser SysUser { get; set; }

        /// <summary>
        /// 角色编号
        /// </summary>
        [ForeignKey(nameof(SysRole))]
        public Guid SysRoleId { get; set; }

        public SysRole SysRole { get; set; }
    }
}
