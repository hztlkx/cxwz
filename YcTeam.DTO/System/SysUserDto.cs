using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YcTeam.DTO.System
{
    public class SysUserDto
    {
        public Guid Id { get; set; }

        [Display(Name = "用户名称")]
        public string UserName { get; set; }

        [Display(Name = "用户密码")]
        public string Password { get; set; }

        [Display(Name = "用户姓名")]
        public string RealName { get; set; }

        [Display(Name = "所属部门")]
        public string SysDepartName { get; set; }

        [Display(Name = "所属部门编号")]
        public Guid SysDepartId { get; set; }

        [Display(Name = "部门类型")]
        public int SysDepartType { get; set; }

        [Display(Name = "所属角色")]
        public string SysRoleName { get; set; }

        [Display(Name = "所属权限编号")]
        public Guid[] SysRoleIds { get; set; }

        [Display(Name = "用户权限编号")]
        public Guid[] SysUserRoleIds { get; set; }

        [Display(Name = "创建时间")]
        public DateTime CreateTime { get; set; }
    }
}
