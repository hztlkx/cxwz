using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace YcTeam.MVCSite.Models.SysNavViewModels
{
    public class SysNavViewModel
    {
        public Guid Id { get; set; }

        [Display(Name = "导航名称")]
        public string NavName { get; set; }

        [Display(Name = "导航路径")]
        public string NavUrl { get; set; }

        [Display(Name = "导航图标")]
        public string NavIcon { get; set; }

        [Display(Name = "导航排序")]
        public int NavOrd { get; set; }
    }
}