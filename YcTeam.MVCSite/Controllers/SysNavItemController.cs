using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Webdiyer.WebControls.Mvc;
using YcTeam.BLL;
using YcTeam.BLL.Master;
using YcTeam.BLL.System;
using YcTeam.DTO;
using YcTeam.DTO.Master;
using YcTeam.DTO.System;
using YcTeam.IBLL.Master;
using YcTeam.IBLL.System;
using YcTeam.Models.Sys;
using YcTeam.MVCSite.Filters;
using YcTeam.MVCSite.Models.SysNavViewModels;

namespace YcTeam.MVCSite.Controllers
{
    [UserAuth]
    public class SysNavItemController : Controller
    {
        readonly ISysNavItemService _sysNavItemSvc = new SysNavItemService();

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 导航列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> SysNavItemList(int pageIndex = 1, int pageSize = 20)
        {
            //总页码、当前页码、可显示总页码
            //当前第n页数据
            var rst = await _sysNavItemSvc.GetAllSysNavItem(pageIndex, pageSize, false);
            //总个数
            var dataCount = await _sysNavItemSvc.GetDataCount();
            //绑定分页
            var list = new PagedList<SysNavItemDto>(rst, pageIndex, pageSize, dataCount);

            return View(list);
        }

        /// <summary>
        /// 添加导航
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> CreateSysNavItem()
        {
            var userId = Common.GetUserId();
            var selectList = new List<SelectListItem>();
            foreach (var item in await new SysNavService().GetAllSysNav())
            {
                selectList.Add(new SelectListItem { Text = item.NavName, Value = item.NavId.ToString() });
            }
            ViewBag.SysNavs = selectList;

            return View(new SysNavItemViewModel()
            {
                NodeOrd = _sysNavItemSvc.GetMaxOrd()+1
            });
        }

        /// <summary>
        /// 增加导航
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateSysNavItem(SysNavItemViewModel model)
        {
            if (ModelState.IsValid)
            {
                _sysNavItemSvc.CreateSysNavItem(new SysNavItem()
                {
                    NodeName = model.NodeName,
                    NodeUrl = model.NodeUrl,
                    NodeOrd = model.NodeOrd,
                    NodeIcons = model.NodeIcons,
                    Pid = model.Pid,
                    RootId = model.RootId,
                    Deep = model.Deep,
                    NavId = model.NavId,
                    CreateTime = DateTime.Now
                });
                return RedirectToAction(nameof(SysNavItemList));
            }
            ModelState.AddModelError("", @"您录入的信息有误");
            return View();
        }

        /// <summary>
        /// 编辑导航
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> SysNavItemEdit(Guid id)
        {
            var model = await _sysNavItemSvc.GetOneSysNavItemById(id);

            //权限集合
            List<SelectListItem> selectList = new List<SelectListItem>();
            var list = await new SysNavService().GetAllSysNav();
            foreach (var item in list)
            {
                selectList.Add(model.SysNav.Id == item.Id
                    ? new SelectListItem { Text = item.NavName, Value = item.Id.ToString(), Selected = true }
                    : new SelectListItem { Text = item.NavName, Value = item.Id.ToString() });
            }
            ViewBag.SysSelectList = selectList;

            return View(new SysNavItemViewModel()
            {
                Id = model.Id,
                NodeName = model.NodeName,
                NodeUrl = model.NodeUrl,
                NodeIcons = model.NodeIcons,
                NodeOrd = model.NodeOrd,
                Pid = model.Pid,
                RootId = model.RootId,
                Deep = model.Deep,
                NavId = model.NavId,
                NavName = model.SysNav.NavName
            });
        }

        /// <summary>
        /// 编辑导航
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> SysNavItemEdit(SysNavItemViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _sysNavItemSvc.EditSysNavItem(new SysNavItem()
                {
                    Id = model.Id,
                    NodeName = model.NodeName,
                    NodeUrl = model.NodeUrl,
                    NodeIcons = model.NodeIcons,
                    NodeOrd = model.NodeOrd,
                    Pid = model.Pid,
                    RootId = model.RootId,
                    Deep = model.Deep,
                    NavId = model.NavId,
                    CreateTime = DateTime.Now
                });
            }
            return RedirectToAction(nameof(SysNavItemList));
        }

        [HttpGet]
        public async Task<ActionResult> SysNavItemDetails(Guid id)
        {
            var data = await _sysNavItemSvc.GetOneSysNavItemById(id);

            if (data == null)
            {
                return RedirectToAction(nameof(SysNavItemList));
            }

            return View(new SysNavItemViewModel() {
                Id = data.Id,
                NodeName = data.NodeName,
                Pid = data.Pid,
                RootId = data.RootId,
                Deep = data.Deep,
                NodeUrl = data.NodeUrl,
                NodeIcons = data.NodeIcons,
                NavName = data.SysNav.NavName
            });
        }

        [HttpGet]
        public async Task<ActionResult> SysNavItemDelete(Guid id)
        {
            await _sysNavItemSvc.RemoveSysNavItem(id);
            return RedirectToAction(nameof(SysNavItemList));
        }
    }
}