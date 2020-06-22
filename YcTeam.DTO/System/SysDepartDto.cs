using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YcTeam.DTO.System
{
    public class SysDepartDto
    {
        public Guid Id { get; set; }

        [Display(Name = "部门名称")]
        public string DepartName { get; set; }

        /// <summary>
        /// 部门类型（1：施工项目部 2：业主项目部 3：机关单位）
        /// </summary>
        [Display(Name = "部门类型")]
        public int DepartType { get; set; }

        [Display(Name = "市级单位名称")]
        public string RegionCity { get; set; }

        [Display(Name = "县级单位名称")]
        public string RegionCounty { get; set; }

        [Display(Name = "创建时间")]
        public DateTime CreateTime { get; set; }
    }
}
