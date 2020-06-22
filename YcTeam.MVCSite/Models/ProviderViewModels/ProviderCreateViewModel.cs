using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace YcTeam.MVCSite.Models.ProviderViewModels
{
    public class ProviderCreateViewModel
    {
        public Guid Id { get; set; }

        /// <summary>
        /// 类型名称
        /// </summary>
        [Required]
        [Display(Name = "供应商名称")]
        public string Name { get; set; }

        [Display(Name = "创建时间")]
        public DateTime CreateTime { get; set; }
    }
}