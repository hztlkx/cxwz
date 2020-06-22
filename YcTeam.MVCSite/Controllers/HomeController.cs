using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using YcTeam.BLL;
using YcTeam.BLL.System;
using YcTeam.IBLL.System;
using YcTeam.Models.Sys;
using YcTeam.MVCSite.Filters;
using YcTeam.MVCSite.Models.UserViewModels;

namespace YcTeam.MVCSite.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// 登录首页
        /// </summary>
        /// <returns></returns>
        [UserAuth]
        public ActionResult Success()
        {
            ViewBag.SysUser = "【"+Session[CommonSession.SysDepartName].ToString() +"】 "+
                              Session[CommonSession.SysUserRealName].ToString();
            return View();
        }

        [UserAuth]
        public ActionResult Index()
        {
            //var isAllowed = UserAuthAttribute.IsAllowedRole;
            //var rst = UserAuthAttribute.IsPermission(nameof(HomeController), nameof(Index));

            return View();
        }

        /// <summary>
        /// 用户注册
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> Register()
        {
            await SetDepartList();
            await SetRoleList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(Models.SysUserViewModels.RegisterViewModel model)
        {
            await SetDepartList();
            await SetRoleList();
            if (!model.SysDepartId.Equals("0"))
            {
                //通过校验
                if (!ModelState.IsValid) return View(model);
                var userService = new SysUserService();
                await userService.Register(new SysUser()
                {
                    UserName = model.UserName,
                    Password = model.Password,
                    RealName = model.RealName,
                    SysDepartId = Guid.Parse(model.SysDepartId)
                },model.SysRoleIds);
            }
            return RedirectToAction(nameof(SysUserController.SysUserList), nameof(SysUser));
        }

        [HttpGet]
        public ActionResult Login()
        {
            Session["LayoutIsLoaded"] = null;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userSvc = new SysUserService();
                var permissionSvc = new SysPermissionService();

                //判断登录
                if (userSvc.Login(model.LoginName, model.LoginPwd))
                {
                    Session[CommonSession.SysUserName] = model.LoginName;//当前登录用户名
                    Session[CommonSession.SysUserRealName] = userSvc.SysUser.RealName;//当前登录用户名
                    Session[CommonSession.SysUserId] = userSvc.UserId;//当前登录用户编号
                    Session[CommonSession.SysDepartId] = userSvc.SysDepartId;//当前登录用户所属部门
                    Session[CommonSession.SysDepartName] = userSvc.SysDepartName;//当前登录用户所属部门名称
                    Session[CommonSession.SysDepartType] = userSvc.SysDepartType;//当前登录用户所属部门类型
                    Session[CommonSession.RegionCity] = userSvc.RegionCity;//当前登录用户所属城市
                    Session[CommonSession.RegionCounty] = userSvc.RegionCounty;//当前登录用户所属县区
                    Session[CommonSession.CurrentUser] = userSvc.SysUser;//当前登录的用户实体
                    Session[CommonSession.SysRoles] = userSvc.SysRole;//当前登录用户角色
                    model.LoginName = userSvc.SysUser.RealName;
                    return RedirectToAction(nameof(Success));//跳转
                }
                else
                {
                    ModelState.AddModelError("",@"您的账号密码有误");
                }
            }
            return View(model);
        }

        public JsonResult SysMenu()
        {
            Session["LayoutIsLoaded"] = true;//系统只加载一次

            var currentUser = Session[CommonSession.CurrentUser] as SysUser;

            var sysRole = Session[CommonSession.SysRoles] as List<SysRole>;
            if (sysRole == null) return Json(null, JsonRequestBehavior.AllowGet);

            List<Guid> roleIds = new List<Guid>();
            foreach (var role in sysRole)
            {
                roleIds.Add(role.Id);
            }
            
            var navRoleSvc = new SysNavRoleService();
            var list = navRoleSvc.GetHomeNavRoles(roleIds.ToArray());

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Logout()
        {
            Session.Clear();
            Session.RemoveAll();
            Session.Abandon();
            return RedirectToAction(nameof(Login));//跳转
        }

        private async Task SetDepartList()
        {
            List<SelectListItem> selectList = new List<SelectListItem>();
            var list = await new SysDepartService().GetAllSysDepart();
            foreach (var item in list)
            {
                selectList.Add(new SelectListItem { Text = item.DepartName, Value = item.Id.ToString() });
            }
            ViewBag.SysDepartList = selectList;
        }

        private async Task SetRoleList()
        {
            //获取所有角色
            var sysRoleService = new SysRoleService();
            ViewBag.SysRoleList = await sysRoleService.GetAllSysRole();
        }
    }
}