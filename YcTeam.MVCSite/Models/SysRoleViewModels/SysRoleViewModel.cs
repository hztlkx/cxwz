using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace YcTeam.MVCSite.Models.SysRoleViewModels
{
    public class SysRoleViewModel
    {
        public Guid Id { get; set; }
        [Display(Name = "角色名称")]
        public string RoleName { get; set; }
        [Display(Name = "排序")]
        public int SortOrder { get; set; }
        
    }
    
}