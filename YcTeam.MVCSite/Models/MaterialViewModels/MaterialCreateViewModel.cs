using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace YcTeam.MVCSite.Models.MaterialCreateViewModels
{
    public class MaterialCreateViewModel
    {
        public Guid Id { get; set; }

        [Display(Name = "物料编码")]
        public string Code { get; set; }
        [Display(Name = "物料大类")]
        public string LargyCategory { get; set; }
        [Display(Name = "物料小类")]
        public string SmallCategory { get; set; }
        [Display(Name = "物料描述")]
        public string Describe { get; set; }
        [Display(Name = "计量单位")]
        public string Unit { get; set; }
        [Display(Name = "备注")]
        public string Note { get; set; }
        [Display(Name = "创建时间")]
        public DateTime CreateTime { get; set; }
    }
}