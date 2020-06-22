using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YcTeam.DTO.System
{
    public class SysRoleDto
    {
        public Guid Id { get; set; }

        //public Guid UserRoleId { get; set; }

        [Display(Name = "角色名称")]
        public string RoleName { get; set; }

        [Display(Name = "排序")]
        public int SortOrder { get; set; }

        [Display(Name = "操作时间")]
        public DateTime CreateTime { get; set; }
        
    }
}
