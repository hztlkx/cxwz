using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YcTeam.Models.Sys
{
    /// <summary>
    /// 系统导航
    /// </summary>
    public class SysNav : BaseEntity
    {
        /// <summary>
        /// 导航名称
        /// </summary>
        public string NavName { get; set; }

        /// <summary>
        /// 导航链接
        /// </summary>
        public string NavUrl { get; set; }

        /// <summary>
        /// 导航图标 
        /// </summary>
        public string NavIcons { get; set; }

        /// <summary>
        /// 导航排序
        /// </summary>
        [DefaultValue(0)]
        public int NavOrd { get; set; }
    }
}
