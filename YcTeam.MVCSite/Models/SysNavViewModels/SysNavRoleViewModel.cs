using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace YcTeam.MVCSite.Models.SysNavViewModels
{
    public class SysNavRoleViewModel
    {
        public Guid Id { get; set; }

        [Display(Name = "节点编号")]
        public Guid NavItemId { get; set; }

        [Display(Name = "节点名称")]
        public string NodeName { get; set; }

        [Display(Name = "滑块编号")]
        public Guid NavId { get; set; }

        [Display(Name = "滑块路径")]
        public string NavName { get; set; }

        [Display(Name = "节点关联数组")]
        public Guid[] NavItemIds { get; set; }
    }
}