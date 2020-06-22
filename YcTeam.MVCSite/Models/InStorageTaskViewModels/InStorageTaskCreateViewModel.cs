using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace YcTeam.MVCSite.Models.InStorageTaskViewModels
{
    public class InStorageTaskCreateViewModel
    {
        public Guid Id { get; set; }

        [Display(Name = "组织机构名称")]
        public string Organization { get; set; }

        [Display(Name = "需求批次")]
        public string SysBatch { get; set; }

        [Display(Name = "物料")]
        public string MaterialId { get; set; }

        [Display(Name = "计划数量")]
        public int Number { get; set; }

        [Display(Name = "系统状态")]
        public string State { get; set; }

        [Display(Name = "备注")]
        public string Note { get; set; }

    }
}