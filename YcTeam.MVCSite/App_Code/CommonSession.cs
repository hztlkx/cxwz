using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YcTeam.MVCSite
{
    public static class CommonSession
    {
        /// <summary>
        /// 登录用户名
        /// </summary>
        public const string SysUserName = "SysUserName";

        /// <summary>
        /// 登录用户姓名
        /// </summary>
        public const string SysUserRealName = "SysUserRealName";

        /// <summary>
        /// 登录用户编号
        /// </summary>
        public const string SysUserId = "SysUserId";

        /// <summary>
        /// 登录用户部门
        /// </summary>
        public const string SysDepartId = "SysDepartId";

        /// <summary>
        /// 登录用户部门名称
        /// </summary>
        public const string SysDepartName = "SysDepartName";

        /// <summary>
        /// 登录用户部门类型
        /// </summary>
        public const string SysDepartType = "SysDepartType";

        /// <summary>
        /// 登录用户所属城市
        /// </summary>
        public const string RegionCity = "RegionCity";

        /// <summary>
        /// 登录用户所属县区
        /// </summary>
        public const string RegionCounty = "RegionCounty";

        /// <summary>
        /// 登录用户信息
        /// </summary>
        public const string CurrentUser = "CurrentUser";

        /// <summary>
        /// 登录用户权限
        /// </summary>
        public const string SysPermission = "SysPermission";

        /// <summary>
        /// 用户角色
        /// </summary>
        public const string SysRoles = "SysRoles";
    }
}