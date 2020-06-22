using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YcTeam.Models.Sys
{
    /// <summary>
    /// 用户角色
    /// </summary>
    public class SysRolePermission : BaseEntity
    {
        /// <summary>
        /// 角色编号
        /// </summary>
        [ForeignKey(nameof(SysRole))]
        public Guid RoleId { get; set; }

        public SysRole SysRole { get; set; }

        /// <summary>
        /// 权限配置
        /// </summary>
        [ForeignKey(nameof(SysPermission))]
        public Guid PermissionId { get; set; }

        public SysPermission SysPermission { get; set; }

        /// <summary>
        /// 权限编号
        /// </summary>
        public int IsAllowed { get; set; }

        /// <summary>
        /// 显示排序
        /// </summary>
        public int SortOrder { get; set; }
    }
}
