using System;
using System.ComponentModel.DataAnnotations;

namespace YcTeam.DTO.WorkFlow
{
    public class FlowNodeDto
    {
        public Guid Id { get; set; }

        [Display(Name = "节点编号")]
        [Required(ErrorMessage = "下节点编号不能为空")]
        public int NodeNumber { get; set; }

        [Display(Name = "节点名称")]
        [Required(ErrorMessage = "节点名称不能为空")]
        public string NodeName { get; set; }

        [Display(Name = "角色编号")]
        [Required(ErrorMessage = "权限不能为空")]
        public Guid SysRoleId { get; set; }

        [Display(Name = "角色名称")]
        public string SysRoleName { get; set; }

        [Display(Name = "下节点(提交)")]
        [Required(ErrorMessage = "下节点编号不能为空")]
        public int NextNodeNumber { get; set; }

        [Display(Name = "上节点(退回)")]
        [Required(ErrorMessage = "上节点编号不能为空")]
        public int PreNodeNumber { get; set; }

        [Display(Name = "更新时间")]
        public DateTime UpdateTime { get; set; }

        [Display(Name = "创建时间")]
        public DateTime CreateTime { get; set; }
    }
}