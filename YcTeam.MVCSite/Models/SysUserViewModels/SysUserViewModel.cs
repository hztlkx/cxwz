using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using YcTeam.Models.Sys;

namespace YcTeam.MVCSite.Models.SysUserViewModels
{
    public class SysUserViewModel
    {
       public Guid Id { get; set; }

       [Display(Name= "用户名称")]
       public string UserName { get; set; }

       [Display(Name = "用户姓名")]
        public string RealName { get; set; }

        [Display(Name = "所属角色")]
        public Guid[] SysRoleIds { get; set; }

        [Display(Name = "角色名称")]
        public string RoleName { get; set; }

        [Display(Name = "所属部门")]
        public Guid SysDepartId { get; set; }

       [Display(Name = "创建时间")]
        public string CreateTime { get; set; }
    }
}