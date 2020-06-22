using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace YcTeam.Models.Master
{
    public class Material:BaseEntity
    {
        /// <summary>
        /// 物料编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 大类
        /// </summary>
        public string LargeCategory { get; set; }

        /// <summary>
        /// 小类
        /// </summary>
        public string SmallCategory { get; set; }

        /// <summary>
        /// 物料描述
        /// </summary>
        public string Describe { get; set; }

        /// <summary>
        /// 计量单位
        /// </summary>
        public string Unit { get; set; }

        /// <summary>
        /// 物料备注
        /// </summary>
        public string Note { get; set; }
    }
}
