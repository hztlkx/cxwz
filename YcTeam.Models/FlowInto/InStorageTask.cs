using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using YcTeam.Models.Master;

namespace YcTeam.Models.FlowInto
{
    public class InStorageTask : BaseEntity
    {
        /// <summary>
        /// 组织机构名称
        /// </summary>
        public string Organization { get; set; }        
        /// <summary>
        /// 需求批次
        /// </summary>
        public string SysBatch { get; set; }     
        /// <summary>
        /// 物料编号
        /// </summary>
        [ForeignKey(nameof(Material))]
        public Guid MaterialId { get; set; }

        /// <summary>
        /// 物料实体
        /// </summary>
        public Material Material { get; set; }    
        /// <summary>
        /// 计划数量
        /// </summary>
        public int Number { get; set; } 
        /// <summary>
        /// 系统状态
        /// </summary>
        public string State { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        /// 仓库Id
        /// </summary>
        [ForeignKey(nameof(InStorage))]
        public Guid InStorageId { get; set; }
        /// <summary>
        /// 仓库实体
        /// </summary>
        public InStorage InStorage { get; set; }

        /// <summary>
        /// 供应商Id
        /// </summary>
        [ForeignKey(nameof(Provider))]
        public Guid ProviderId { get; set; }
        /// <summary>
        /// 供应商实体
        /// </summary>
        public Provider Provider { get; set; }
        /// <summary>
        /// 计划时间
        /// </summary>
        public DateTime PlanTime { get; set; }
    }
}
