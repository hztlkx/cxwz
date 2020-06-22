using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YcTeam.Models.Sys
{
    /**优先级[1-10]，1最高，以此类推
     *
     *如，SysPermission已配置，则：
     *【优先级1】：判断游客访问IsAllowedNoneRole
     *【优先级2】：判断角色访问IsAllowedAllRole
     *【优先级3】：判断用户信息->是否存在
     *【优先级4】：判断SysRolePermission->是否配置
     *【优先级5】：判断禁用的角色SysRolePermission.IsAllowed=0->是否存在
     *【优先级6】：判断允许的角色SysRolePermission.IsAllowed=1->是否存在
     **/

    /// <summary>
    /// 用户角色
    /// </summary>
    public class SysPermission : BaseEntity
    {
        /// <summary>
        /// 控制器名称
        /// </summary>
        public string ControllerName { get; set; }

        /// <summary>
        /// 控制器特性
        /// </summary>
        public string ControllerAttribute { get; set; }

        /// <summary>
        /// 方法名称
        /// </summary>
        public string ActionName { get; set; }

        /// <summary>
        /// 方法特性
        /// </summary>
        public string ActionAttribute { get; set; }

        /// <summary>
        /// 是否为控制器
        /// </summary>
        public int IsController { get; set; }

        /// <summary>
        /// 是否游客访问【优先级1】
        /// </summary>
        public int IsAllowedNoneRole { get; set; }

        /// <summary>
        /// 是否角色访问（只要有角色即可访问）【优先级2】
        /// </summary>
        public int IsAllowedAllRole { get; set; }

        /// <summary>
        /// 权限外键集合
        /// </summary>
        public List<SysRolePermission> SysRolePermissions { get; set; }

        /// <summary>
        /// 显示排序
        /// </summary>
        public int SortOrder { get; set; }
    }
}
