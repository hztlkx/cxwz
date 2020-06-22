using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace YcTeam.DTO.Master
{
    public class ProjectDto
    {
        public Guid Id { get; set; }

        [Display(Name = "项目名称")]
        public string ProjectName { get; set; }

        [Display(Name = "WBS编码")]
        public string WBSCode { get; set; }


        [Display(Name = "子项目名称")]
        public string SmallProjectName { get; set; }


        [Display(Name = "子项目编码")]
        public string SmallWBSCode { get; set; }


        [Display(Name = "初始批复资金")]
        public string Funds { get; set; }


        [Display(Name = "电压等级")]
        public string VoltageGrade { get; set; }


        [Display(Name = "业主项目部")]
        public string SysDepartName { get; set; }

        [Display(Name = "业主项目部编号")]
        public Guid? SysDepartId { get; set; }

        [Display(Name = "领料人")]
        public string PickingPeople { get; set; }

        [Display(Name = "联系号码")]
        public string ContactNumber { get; set; }
        [Display(Name = "创建时间")]
        public DateTime CreateTime { get; set; }
    }
}
