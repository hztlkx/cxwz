using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YcTeam.Models.Sys;
using static YcTeam.MVCSite.CommonSession;

namespace YcTeam.MVCSite
{
    public static class Common
    {
        /// <summary>
        /// 登录用户编号
        /// </summary>
        /// <returns></returns>
        public static Guid? GetUserId()
        {
            if (HttpContext.Current.Session["userId"] == null)
            {
                return null;
            }

            return Guid.Parse(HttpContext.Current.Session["userId"].ToString());
        }

        /// <summary>
        /// 登录用户所属部门编号
        /// </summary>
        /// <returns></returns>
        public static Guid? GetUserSysDepartId()
        {
            if (HttpContext.Current.Session["sysDepartId"] == null)
            {
                return null;
            }
            return Guid.Parse(HttpContext.Current.Session["sysDepartId"].ToString());
        }

        /// <summary>
        /// 登录用户所属城市名称
        /// </summary>
        /// <returns></returns>
        public static string GetUserRegionCityName()
        {
            if (HttpContext.Current.Session["regionCity"] == null)
            {
                return "";
            }
            return HttpContext.Current.Session["regionCity"].ToString();
        }

        /// <summary>
        /// 登录用户所属县区名称
        /// </summary>
        /// <returns></returns>
        public static string GetUserRegionCountyName()
        {
            if (HttpContext.Current.Session["regionCounty"] == null)
            {
                return "";
            }
            return HttpContext.Current.Session["regionCounty"].ToString();
        }

        /// <summary>
        /// 判断用户有哪些权限
        /// </summary>
        /// <returns></returns>
        public static List<SysRole> GetRoleList()
        {
            if (HttpContext.Current.Session[CommonSession.SysRoles] == null)
            {
                return null;
            }
            return HttpContext.Current.Session["regionCounty"] as List<SysRole>;
        }

        public static string ConvertStateName(int state)
        {
            var json = new
            {
                stateSubmit = new List<StateToName>
                {
                    new StateToName(){State = Guid.Parse(""),Name = "已提交"}
                }

            };

            return "";
        }

        private class StateToName
        {
            public Guid State { get; set; }

            public string Name { get; set; }
        }

        /// <summary>
        /// 部门类型
        /// </summary>
        public struct DepartType
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        /// <summary>
        /// 获取部门类型集合
        /// </summary>
        /// <returns></returns>
        public static List<DepartType> GetDepartTypeList()
        {
            var departTypeList = new List<DepartType>
            {
                new DepartType() {Id = 1, Name = "施工项目部"},
                new DepartType() {Id = 2, Name = "业主项目部"},
                new DepartType() {Id = 3, Name = "机关部门"}
            };
            return departTypeList;
        }

        public static bool HasRole(RoleType rt)
        {
            if (!(HttpContext.Current.Session[SysRoles] is List<SysRole> roleList)) return false;
            foreach (var role in roleList)
            {
                if (rt == RoleType.Super)
                {
                    if (role.RoleName.Contains("超级管理员")){
                        return true;
                    }
                }
                else if (rt == RoleType.NoConstruction)
                {
                    if (role.RoleName.Contains("超级管理员")
                        || role.RoleName.Contains("业主")
                        || role.RoleName.Contains("运检"))
                    {
                        return true;
                    }
                }else if (rt == RoleType.Inspection)
                {
                    if (role.RoleName.Contains("超级管理员")
                        || role.RoleName.Contains("运检"))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public enum RoleType
        {
            /// <summary>
            /// 超级管理员
            /// </summary>
            Super = 1,
            /// <summary>
            /// 施工项目部
            /// </summary>
            Construction = 2,
            /// <summary>
            /// 业主项目部
            /// </summary>
            Owner = 3,
            /// <summary>
            /// 运检部
            /// </summary>
            Inspection = 4,
            /// <summary>
            /// 物资部
            /// </summary>
            Material = 5,
            /// <summary>
            /// 仓库管理员
            /// </summary>
            InStorage =  6,
            /// <summary>
            /// 排除施工项目部
            /// </summary>
            NoConstruction = 200,
            /// <summary>
            /// 排除业主项目部
            /// </summary>
            NoOwner = 300,
            /// <summary>
            /// 排除运检部
            /// </summary>
            NoInspection = 400,
            /// <summary>
            /// 排除物资部
            /// </summary>
            NoMaterial = 500,
            /// <summary>
            /// 排除仓库管理员
            /// </summary>
            NoInStorage = 600
        }
    }
}