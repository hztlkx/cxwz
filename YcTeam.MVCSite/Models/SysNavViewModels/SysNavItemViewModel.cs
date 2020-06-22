using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace YcTeam.MVCSite.Models.SysNavViewModels
{
    public class SysNavItemViewModel
    {
        public Guid Id { get; set; }

        [Display(Name = "节点名称")]
        public string NodeName { get; set; }

        [Display(Name = "父节点")]
        public int Pid { get; set; }

        [Display(Name = "根节点")]
        public int RootId { get; set; }

        [Display(Name = "深度")]
        public int Deep { get; set; }

        [Display(Name = "节点链接")]
        public string NodeUrl { get; set; }

        [Display(Name = "节点排序")]
        public int NodeOrd { get; set; }

        [Display(Name = "所属导航")]
        public Guid NavId { get; set; }

        [Display(Name = "导航图标")]
        public string NodeIcons { get; set; }

        [Display(Name = "所属导航名称")]
        public string NavName { get; set; }

    }
}