using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace YcTeam.MVCSite.Models.FlowNodeViewModels
{
    public class FlowNodeViewModel
    {
        public Guid Id { get; set; }

        [Display(Name = "节点编号")]
        public Guid NodeNumber { get; set; }

        [Display(Name = "节点名称")]
        public string NodeName { get; set; }

        [Display(Name = "执行人Id")]
        public Guid? OperateUserId { get; set; }

        [Display(Name = "执行人名称")]
        public string OperateUser { get; set; }

        [Display(Name = "下一节点编号")]
        public string NextNodeNumber { get; set; }

        [Display(Name = "上一节点编号（退回节点）")]
        public string LastNodeNumber { get; set; }

        [Display(Name = "更新时间")]
        public DateTime UpdateTime { get; set; }
    }
}