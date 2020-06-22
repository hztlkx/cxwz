using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YcTeam.DTO.System
{
    public class SysUserRoleDto
    {
        public Guid Id { get; set; }

        /// <summary>
        /// 用户编号
        /// </summary>
        public Guid SysUserId { get; set; }

        /// <summary>
        /// 角色编号
        /// </summary>
        public Guid SysRoleId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
