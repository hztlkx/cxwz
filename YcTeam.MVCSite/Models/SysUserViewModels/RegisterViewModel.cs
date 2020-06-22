using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace YcTeam.MVCSite.Models.SysUserViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "用户名称")]
        public string  UserName { get; set; }

        [Display(Name = "用户姓名")]
        public string RealName { get; set; }

        [Required]
        [StringLength(maximumLength: 50, MinimumLength = 6)]
        [DataType(dataType: DataType.Password)]
        [Display(Name = "用户密码")]
        public string  Password { get; set; }

        [Required]
        [Compare(nameof(Password))]
        [Display(Name = "密码确认")]
        public string  Confirm { get; set; }

        [Display(Name = "所属部门")]
        public String SysDepartId { get; set; }

        [Display(Name = "所属角色")]
        public Guid[] SysRoleIds { get; set; }
    }
}