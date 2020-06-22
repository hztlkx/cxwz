using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Webdiyer.WebControls.Mvc;
using YcTeam.BLL.Master;
using YcTeam.BLL.System;
using YcTeam.DTO.Master;
using YcTeam.DTO.System;
using YcTeam.IBLL.Master;
using YcTeam.IBLL.System;
using YcTeam.Models.Sys;
using YcTeam.MVCSite.Filters;
using YcTeam.MVCSite.Models.SysDepartViewModels;
using YcTeam.MVCSite.Models.SysUserViewModels;

namespace YcTeam.MVCSite.Controllers
{
    [UserAuth]
    public class SysUserController : Controller
    {
        // GET: SysUser
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 部门清单
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> SysUserList(int pageIndex = 1, int pageSize = 20)
        {
            //总页码、当前页码、可显示总页码
            var sysUserSvc = new SysUserService();
            //当前第n页数据
            var sysUser = await sysUserSvc.GetAllSysUser(pageIndex, pageSize, false);
            //总个数
            var dataCount = await sysUserSvc.GetDataCount();

            //绑定分页
            var list = new PagedList<SysUserDto>(sysUser, pageIndex, pageSize, dataCount);

            return View(list);
        }

        /// <summary>
        /// 部门添加
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> CreateSysUser()
        {
            var userId = Common.GetUserId();
            var list = await new SysUserService().GetAllSysDeparts(Common.GetUserRegionCityName(),Common.GetUserRegionCountyName());
            ViewBag.SysDeparts = list;

            return View();
        }

        /// <summary>
        /// 用户修改
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> SysUserEdit(Guid id)
        {
            //获取用户信息
            var sysUserService = new SysUserService();
            var data = await sysUserService.GetOneSysUserById(id);

            List<Guid> roleIds = new List<Guid>();
            foreach (var t in data.SysUserRoles.Where(a => !a.IsRemoved))
            {
                roleIds.Add(t.SysRoleId);
            }

            //权限集合
            var selectList = new List<SelectListItem>();
            var list = await new SysDepartService().GetAllSysDepart();
            foreach (var item in list)
            {
                selectList.Add(data.SysDepartId == item.Id
                    ? new SelectListItem {Text = item.DepartName, Value = item.Id.ToString(), Selected = true}
                    : new SelectListItem {Text = item.DepartName, Value = item.Id.ToString()});
            }
            ViewBag.SysDepartList = selectList;

            //获取所有角色
            var sysRoleService = new SysRoleService();
            ViewBag.SysRoleList = await sysRoleService.GetAllSysRole();

            return View(new SysUserViewModel()
            {
                Id = data.Id,
                RealName = data.RealName,
                UserName = data.UserName,
                SysRoleIds = roleIds.ToArray(),
                CreateTime = data.CreateTime.ToString("yyyy-dd-MM")
            });
        }

        /// <summary>
        /// 用户修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> SysUserEdit(Models.SysUserViewModels.SysUserViewModel model)
        {
            //设置中间表
            var sysUserService = new SysUserService();
            await sysUserService.EditSysUser(new SysUser()
            {
                Id = model.Id,
                UserName = model.UserName,
                RealName = model.RealName,
                SysDepartId = model.SysDepartId
            },model.SysRoleIds);

            return RedirectToAction(nameof(SysUserList));
        }

        /// <summary>
        /// 用户详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> SysUserDetails(Guid? id)
        {
            var sysUserService = new SysUserService();
            if (id == null || !await sysUserService.ExistsSysUser(id.Value))
            {
                return RedirectToAction(nameof(SysUserList));
            }
            var m = await sysUserService.GetOneSysUserById(id.Value);

            string roleName = "";
            foreach (var t in m.SysUserRoles.Where(r=>!r.IsRemoved))
            {
                roleName += t.SysRole.RoleName + '、';
            }
            return View(new SysUserDto() {
                Id = m.Id,
                UserName = m.UserName,
                RealName = m.RealName,
                SysRoleName = roleName.TrimEnd('、'),
                SysDepartName = m.SysDepart.DepartName,
                CreateTime = m.CreateTime
            });
        }

        [HttpGet]
        public async Task<ActionResult> SysUserDelete(Guid id)
        {
            var sysUserService = new SysUserService();
            await sysUserService.RemoveSysUser(id);
            return RedirectToAction(nameof(SysUserList));
        }
    }
}