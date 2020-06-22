using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YcTeam.DTO.System
{
    public class SysNavDto
    {
        public Guid Id { get; set; }

        [Display(Name = "导航名称")]
        public string NavName { get; set; }

        [Display(Name = "导航路径")]
        public string NavUrl { get; set; }

        [Display(Name = "导航图片")]
        public string NavIcons { get; set; }

        [Display(Name = "导航排序")]
        public int NavOrd { get; set; }

        [Display(Name = "操作时间")]
        public DateTime CreateTime { get; set; }
    }
}
