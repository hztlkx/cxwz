using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YcTeam.DTO.FlowInto
{
    public class InStorageReceiveDto
    {
        public Guid Id { get; set; }

        [Display(Name ="需求批次号")]
        public string Batch { get; set; }

        [Display(Name = "供应商编号")]
        public Guid ProviderId { get; set; }

        [Display(Name = "供应商名称")]
        public string ProviderName { get; set; }

        [Display(Name = "物料编号")]
        public Guid MaterialId { get; set; }

        [Display(Name = "物料编码")]
        public string Code { get; set; }

        [Display(Name = "物料描述")]
        public string Describe { get; set; }

        [Display(Name = "单位")]
        public string Unit { get; set; }

        [Display(Name = "入库数量")]
        public int PutNumber { get; set; }

        [Display(Name = "本次入库时间")]
        public DateTime CreateTime { get; set; }

        [Display(Name = "附件")]
        public string File { get; set; }

        [Display(Name = "订单创建人ID")]
        public Guid SysUserId { get; set; }

        [Display(Name = "订单创建人")]
        public string SysUserName { get; set; }

        [Display(Name = "备注")]
        public string Note { get; set; }

        
    }
}
