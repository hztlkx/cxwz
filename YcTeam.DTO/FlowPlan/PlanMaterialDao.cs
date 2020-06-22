using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using YcTeam.Models.Master;

namespace YcTeam.DTO.FlowPlan
{
    public class PlanMaterialDto
    {
        /// <summary>
        /// 流程实体编号
        /// </summary>
        public Guid Id { get; set; }

        [Display(Name = "技术规范书编号")]
        public string TechNumber { get; set; }

        [Display(Name = "价格")]
        public decimal Price { get; set; }

        [Display(Name = "数量")]
        public int Num { get; set; }

        [Display(Name = "备注")]
        public string Note { get; set; }

        [Display(Name = "需求时间")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime PlanDate { get; set; } = DateTime.Now;

        [Display(Name = "更新时间")]
        public DateTime UpdateTime { get; set; } = DateTime.Now;

        [Display(Name = "创建时间")]
        public DateTime CreateTime { get; set; } = DateTime.Now;
    }
}
