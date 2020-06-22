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
    public class SysRole : BaseEntity
    {
        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleName { get; set; }

        /// <summary>
        /// 显示排序
        /// </summary>
        public int SortOrder { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<SysNavRole> SysNavRoles { get; set; }

        /// <summary>
        /// 所属权限
        /// </summary>
        public List<SysRolePermission> SysRolePermissions { get; set; }

        /// <summary>
        /// 所属角色配置
        /// </summary>
        public List<SysUserRole> SysUserRoles { get; set; }
    }
}
