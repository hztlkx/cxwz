using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YcTeam.DTO.Master
{
    public class ProviderDto
    {
        public Guid Id { get; set; }

        [Display(Name = "供应商名称")]
        public string Name { get; set; }

        [Display(Name = "创建时间")]
        public DateTime CreateTime { get; set; }
    }
}
