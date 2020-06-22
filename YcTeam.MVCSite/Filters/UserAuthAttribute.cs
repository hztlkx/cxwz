using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using YcTeam.IBLL.System;
using YcTeam.Models.Sys;

namespace YcTeam.MVCSite.Filters
{
    [UserAuth]
    public class BaseController : Controller
    {
        
    }

    public class UserAuthAttribute:AuthorizeAttribute
    {
        public static bool IsAllowedRole = false;
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            var session = filterContext.HttpContext.Session;
            //当用户存储在cookie中，且Session数据为空时（关闭浏览器），把cookie的数据同步到session中
            if (filterContext.HttpContext.Request.Cookies[CommonSession.SysUserName] != null && session[CommonSession.SysUserName] == null)
            {
                //同步用户名
                session[CommonSession.SysUserName] = filterContext.HttpContext.Request.Cookies[CommonSession.SysUserName]?.Value;
                //同步用户编号
                session[CommonSession.SysUserId] = filterContext.HttpContext.Request.Cookies[CommonSession.SysUserId]?.Value;
            }

            //判断用户登录Session与Cookie是否存在
            if (session[CommonSession.SysUserName] == null &&
                filterContext.HttpContext.Request.Cookies[CommonSession.SysUserName] == null)
            {
                filterContext.Result = new RedirectResult("~/Home/Login");
            }

            //用户认证
            var actionDescriptor = filterContext.ActionDescriptor;
            var controllerDescriptor = actionDescriptor.ControllerDescriptor;
            var controllerName = controllerDescriptor.ControllerName;
            var actionName = actionDescriptor.ActionName;

            //ValidateRoles(controllerName,actionName);
        }

        /// <summary>
        /// 用户认证
        /// </summary>
        /// <param name="controllerName"></param>
        /// <param name="actionName"></param>
        /// <returns></returns>
        public static bool ValidateRoles(string controllerName,string actionName)
        {
            return IsAllowed(HttpContext.Current.Session[CommonSession.CurrentUser] as SysUser, controllerName, actionName);
        }

        /// <summary>
        /// 判断权限
        /// </summary>
        /// <param name="sysUser">用户实体</param>
        /// <param name="controllerName">控制器名称</param>
        /// <param name="actionName">方法名称</param>
        /// <returns></returns>
        public static bool IsAllowed(SysUser sysUser, string controllerName, string actionName)
        {
            if (HttpContext.Current.Session[CommonSession.SysPermission] is List<SysPermission> list)
            {
                //获取对应的controller 
                SysPermission permissionController = list.FirstOrDefault(c => 
                        c.ControllerName == controllerName);

                //controller存在
                if (permissionController == null) return false;
                {
                    // 获取对应的action
                    SysPermission permissionAction = list.FirstOrDefault(c => 
                        c.ActionName == actionName && c.IsController == 0 
                                                   && c.ControllerName == controllerName);

                    return permissionAction == null ? IsAllowed(sysUser, permissionController) 
                        : IsAllowed(sysUser, permissionAction);
                }
            }
            //没有定义controller的权限，表示无权限控制 
            return false;
        }


        /// <summary>
        /// 具体一个Action的功能权限
        /// </summary>
        /// <param name="sysUser"></param>
        /// <param name="sysPermission"></param>
        /// <returns></returns>
        private static bool IsAllowed(SysUser sysUser, SysPermission sysPermission)
        {
            // 游客权限
            if (sysPermission.IsAllowedNoneRole == 1)
            {
                return true;
            }

            // 允许有角色：只要有角色，允许访问
            if (sysPermission.IsAllowedAllRole == 1)
            {
                return sysUser.SysUserRoles.Count > 0;//是否存在角色
            }

            //无权限、无登录
            if (sysUser == null || sysUser.SysUserRoles.Count == 0)
            {
                return false;
            }


            ////选出action对应的角色
            var roles = sysPermission.SysRolePermissions.Select(a => a.SysRole).ToList();
            if (roles.Count == 0)
            {
                // 角色数量为0，也就是说没有定义访问规则，默认不允许访问 
                return false;
            }

            //获取所有权限配置
            var userHavesRoadsides = sysUser.SysUserRoles.Select(r => r.Id).ToList();

            var roleList = sysPermission.SysRolePermissions.ToList();

            // 查找禁止的角色 
            var notAllowedRoles = roleList.FindAll(r => r.IsAllowed == 0).Select(ca => ca.SysRole).ToList();
            if (notAllowedRoles.Count > 0)
            {
                foreach (SysRole role in notAllowedRoles)
                {
                    // 用户的角色在禁止访问列表中，不允许访问 
                    if (userHavesRoadsides.Contains(role.Id))
                    {
                        return false;
                    }
                }
            }

            // 查找允许访问的角色列表 
            var allowRoles = roleList.FindAll(r => r.IsAllowed == 1).Select(ca => ca.SysRole).ToList();
            if (allowRoles.Count > 0)
            {
                foreach (SysRole role in allowRoles)
                {
                    // 用户的角色在访问的角色列表 
                    if (userHavesRoadsides.Contains(role.Id))
                    {
                        return true;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// 判断权限存在
        /// </summary>
        /// <param name="controllerName"></param>
        /// <param name="actionName"></param>
        /// <returns></returns>
        public static bool IsPermission(string controllerName,string actionName)
        {
            controllerName = controllerName.Replace("Controller", "");
            actionName = actionName.Replace("Action", "");

            if (HttpContext.Current.Session[CommonSession.SysRoles] is List<SysRole> rolesList)
            {
                foreach (var roles in rolesList)
                {
                    foreach (var rolePermission in roles.SysRolePermissions)
                    {
                        if (rolePermission.IsAllowed == 1)
                        {
                            if (rolePermission.SysPermission.ControllerName == controllerName
                                && rolePermission.SysPermission.ActionName == actionName)
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            else
            {
                RedirectLogin();
            }

            return false;
        }

        /// <summary>
        /// 返回登录界面
        /// </summary>
        private static void RedirectLogin()
        {
            HttpContext.Current.Response.RedirectToRoute(new
            {
                controller = "Home", action = "Login"
            });
        }
    }
}