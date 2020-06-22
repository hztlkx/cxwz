using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YcTeam.Models.Sys
{
    /// <summary>
    /// 系统用户
    /// </summary>
    public class SysUser : BaseEntity
    {
        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 用户密码
        /// </summary>
        [Required(ErrorMessage = "密码不能为空")]
        public string Password { get; set; }

        /// <summary>
        /// 用户姓名
        /// </summary>
        [Required(ErrorMessage = "姓名不能为空")]
        public string RealName { get; set; }

        /// <summary>
        /// 所属部门
        /// </summary>
        [ForeignKey(nameof(SysDepart))]
        public Guid SysDepartId { get; set; }

        public SysDepart SysDepart { get; set; }

        /// <summary>
        /// 所属角色配置
        /// </summary>
        public List<SysUserRole> SysUserRoles { get; set; }
    }
}
