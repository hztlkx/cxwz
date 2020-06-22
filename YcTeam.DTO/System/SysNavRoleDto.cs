using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YcTeam.DTO.System
{
    public class SysNavRoleDto
    {
        public Guid Id { get; set; }

        public bool IsChecked { get; set; }

        public Guid NavItemId { get; set; }

        public string NavItemName { get; set; }

        public Guid NavId { get; set; }

        public string NavName { get; set; }

        public Guid RoleId { get; set; }

        [Display(Name = "操作时间")]
        public DateTime CreateTime { get; set; }
    }
}
