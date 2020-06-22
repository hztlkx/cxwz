using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YcTeam.DTO.Master
{
    public class InStorageDto
    {
        public Guid Id { get; set; }

        [Display(Name = "中转库编码")]
        public string Code { get; set; }

        [Display(Name = "仓库名称")]
        public string Name { get; set; }


        [Display(Name = "仓库类型")]
        public string Category { get; set; }


        [Display(Name = "库房描述")]
        public string Describe { get; set; }

        [Display(Name = "创建时间")]
        public DateTime CreateTime { get; set; }
    }
}
