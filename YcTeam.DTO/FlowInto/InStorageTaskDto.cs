using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YcTeam.DTO.FlowInto
{
    public class InStorageTaskDto
    {
        public Guid Id { get; set; }
        
        [Display(Name = "组织机构名称")]
        public string Organization { get; set; }

        [Display(Name = "需求批次")]
        public string SysBatch { get; set; }

        [Display(Name = "需求计划状态")]
        public string State { get; set; }

        [Display(Name = "仓库名称")]
        public string InStorageName { get; set; }

        [Display(Name = "物料编号")]
        public Guid MaterialId { get; set; }

        [Display(Name = "物料编码")]
        public string Code { get; set; }

        [Display(Name = "物料描述")]
        public string Describe { get; set; }

        [Display(Name = "计量单位")]
        public string Unit { get; set; }

        [Display(Name = "已入库数量")]
        public int SysNumber { get; set; }

        [Display(Name = "需求数量")]
        public int PlanNumber { get; set; }

        [Display(Name = "需求时间")]
        public DateTime PlanTime { get; set; }

        [Display(Name = "匹配供应商名称")]
        public string ProviderName { get; set; }

        [Display(Name = "备注")]
        public string Note { get; set; }

        [Display(Name = "创建时间")]
        public DateTime CreateTime { get; set; }
    }
}
