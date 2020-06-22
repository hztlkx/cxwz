using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YcTeam.Models.Master;
using YcTeam.Models.Sys;

namespace YcTeam.Models.FlowInto
{
    public class InStorageReceive:BaseEntity
    {
        /// <summary>
        /// 需求批次
        /// </summary>
        public string Batch { get; set; }
        /// <summary>
        /// 供应商编号
        /// </summary>
        [ForeignKey(nameof(Provider))]
        public Guid ProviderId { get; set; }
        /// <summary>
        /// 供应商实体
        /// </summary>
        public Provider Provider { get; set; }
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
        /// 入库数量
        /// </summary>
        public int PutNumber { get; set; }
        /// <summary>
        /// 附件
        /// </summary>
        public string File { get; set; }
        /// <summary>
        /// 订单创建人
        /// </summary>
        [ForeignKey(nameof(SysUser))]
        public Guid UserId { get; set; }
        /// <summary>
        /// 用户实体类
        /// </summary>
        public SysUser SysUser { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Note { get; set; }
    }
}
