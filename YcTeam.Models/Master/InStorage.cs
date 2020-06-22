using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YcTeam.Models.Master
{
    public class InStorage:BaseEntity
    { 
        /// <summary>
        /// 中转库编码
        /// </summary>
        public string  Code { get; set; }

        /// <summary>
        /// 仓库名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 仓库类别
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// 库房描述
        /// </summary>
        public string Describe { get; set; }
    }
}
