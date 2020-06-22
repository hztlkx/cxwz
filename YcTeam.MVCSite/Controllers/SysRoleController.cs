using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Webdiyer.WebControls.Mvc;
using YcTeam.BLL.System;
using YcTeam.DTO.System;
using YcTeam.IBLL.System;
using YcTeam.Models.Sys;
using YcTeam.MVCSite.Filters;
using YcTeam.MVCSite.Models.SysNavViewModels;
using YcTeam.MVCSite.Models.SysRoleViewModels;

namespace YcTeam.MVCSite.Controllers
{
    public class SysRoleController : Controller
    {
        readonly ISysRoleService _sysRoleSvc = new SysRoleService();
        readonly ISysNavRoleService _sysNavRoleSvc = new SysNavRoleService();
        readonly ISysNavItemService _sysNavItemService = new SysNavItemService();

        // GET: SysRole
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 角色清单
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> SysRoleList(int pageIndex = 1, int pageSize = 20)
        {
            //总页码、当前页码、可显示总页码
            var sysRoleSvc = new SysRoleService();
            //当前第n页数据
            var sysRole = await sysRoleSvc.GetAllSysRole(pageIndex, pageSize, false);
            //总个数
            var dataCount = await sysRoleSvc.GetDataCount();
            //绑定分页
            var list = new PagedList<SysRoleDto>(sysRole, pageIndex, pageSize, dataCount);

            return View(list);
        }

        [HttpGet]
        public async Task<ActionResult> CreateSysRole()
        {
            var userId = Common.GetUserId();
            var selectList = new List<SelectListItem>();
            foreach (var item in await new SysRoleService().GetAllSysRole())
            {
                selectList.Add(new SelectListItem {Text = item.RoleName, Value = item.SortOrder.ToString()});
            }

            ViewBag.SysNavs = selectList;

            return View(new SysRoleViewModel()
            {
                SortOrder = _sysRoleSvc.GetMaxOrd() + 1
            });

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateSysRole(SysRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                ISysRoleService sysRoleSvc = new SysRoleService();
                await sysRoleSvc.CreateSysRole(model.RoleName, model.SortOrder);
                return RedirectToAction(nameof(SysRoleList));
            }

            ModelState.AddModelError("", @"您录入的信息有误");
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> EditSysRole(Guid id)
        {
            var sysRoleService = new SysRoleService();
            var data = await sysRoleService.GetOneSysRoleById(id);

            return View(new SysRoleViewModel()
            {
                Id = data.Id,
                RoleName = data.RoleName,
                SortOrder = data.SortOrder,

            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult>EditSysRole(SysRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var sysRoleService = new SysRoleService();
                await sysRoleService.EditSysRole(model.Id, model.RoleName, model.SortOrder);

            }

            return RedirectToAction(nameof(SysRoleList));

        }

        [HttpGet]
        public async Task<ActionResult> DetailsSysRole(Guid? id)
        {
            var sysRoleService = new SysRoleService();
            if (id == null || !await sysRoleService.ExistsSysRole(id.Value))
            {
                return RedirectToAction(nameof(SysRoleList));
            }

            return View(await sysRoleService.GetOneSysRoleById(id.Value));
        }

        [HttpGet]
        public async Task<ActionResult> DeleteSysRole(Guid id)
        {
            var sysRoleService = new SysRoleService();
            await sysRoleService.RemoveSysRole(id);
            return RedirectToAction(nameof(SysRoleList));
        }

        [HttpGet]
        [UserAuth]
        public ActionResult SetNav(Guid id, string roleName,Guid roleId)
        {
            ViewBag.roleId = roleId;
            ViewBag.roleName = roleName;
            var navItemList = _sysNavItemService.JoinNavItemAndNav();
            var navRoleList = _sysNavRoleSvc.GetSysNavRole(new Guid[] {id});
            var list = new List<SysNavRoleDto>();

            if (navRoleList == null || navRoleList.Result.Count == 0)
            {
                foreach (var navItem in navItemList)
                {
                    var m = new SysNavRoleDto
                    {
                        IsChecked = false,
                        NavItemId = navItem.Id,
                        NavItemName = navItem.NodeName,
                        NavId = navItem.SysNav.Id,
                        NavName = navItem.SysNav?.NavName
                    };
                    list.Add(m);
                }
            }
            else
            {
                //导航菜单
                foreach (var navItem in navItemList)
                {
                    //用户导航配置
                    foreach (var navRole in navRoleList.Result)
                    {
                        var m = new SysNavRoleDto
                        {
                            IsChecked = false,
                            Id = navRole.Id,
                            RoleId = navRole.RoleId,
                            NavItemId = navItem.Id,
                            NavItemName = navItem.NodeName,
                            NavId = navItem.SysNav.Id,
                            NavName = navItem.SysNav?.NavName
                        };


                        if (navRole.NavItemId == navItem.Id)
                        {
                            var isExists = list.Where(a => a.NavItemId == m.NavItemId).ToList();
                            if (isExists.Any())
                            {
                                foreach (var ex in isExists)
                                {
                                    ex.Id = navRole.Id;
                                    ex.IsChecked = true;
                                }
                            }
                            else
                            {
                                m.Id = navRole.Id;
                                m.IsChecked = true;
                                list.Add(m);
                            }

                            break;
                        }
                        else
                        {
                            //判断历史处理过程中，是否已经包含
                            if (list.Count(a => a.NavItemId == m.NavItemId) == 0)
                            {
                                m.Id = new Guid();
                                list.Add(m); //添加没有匹配的NavRole
                            }
                        }
                    }
                }
            }

            return View(list.OrderBy(m => !m.IsChecked));
        }

        [HttpPost]
        public ActionResult SetNav(string addRids, string removeRids,Guid roleId)
        {
            string[] addArray = addRids.Split(',');
            string[] removeArray = removeRids.Split(',');

            foreach (var aid in addArray)
            {
                if (!aid.Equals(""))
                {
                    _sysNavRoleSvc.CreateSysNavRole(roleId, Guid.Parse(aid));
                }
            }

            foreach (var rid in removeArray)
            {
                if (!rid.Equals(""))
                {
                    _sysNavRoleSvc.RemoveSysNavRole(Guid.Parse(rid));
                }
            }

            return RedirectToAction(nameof(SysRoleList));
        }
    }
}