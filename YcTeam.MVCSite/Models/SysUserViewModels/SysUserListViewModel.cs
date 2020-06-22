using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using YcTeam.Models.Sys;

namespace YcTeam.MVCSite.Models.SysUserViewModels
{
    public class SysUserListViewModel
    {
       public Guid Id { get; set; }

       /// <summary>
       /// 用户名称
       /// </summary>
       public string UserName { get; set; }

       /// <summary>
       /// 用户姓名
       /// </summary>
       public string RealName { get; set; }

       /// <summary>
       /// 部门名称
       /// </summary>
       public string SysDepartName { get; set; }

       /// <summary>
       /// 角色名称
       /// </summary>
       public string SysRoleName { get; set; }

       /// <summary>
       /// 创建时间
       /// </summary>
       public string CreateTime { get; set; }
    }
}