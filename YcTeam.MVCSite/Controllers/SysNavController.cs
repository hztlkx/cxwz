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
    public class SysNavController : Controller
    {
        readonly ISysNavService _sysNavSvc = new SysNavService();

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
        public async Task<ActionResult> SysNavList(int pageIndex = 1, int pageSize = 20)
        {
            //总页码、当前页码、可显示总页码
            //当前第n页数据
            var rst = await _sysNavSvc.GetAllSysNav(pageIndex, pageSize, false);
            //总个数
            var dataCount = await _sysNavSvc.GetDataCount();
            //绑定分页
            var list = new PagedList<SysNavDto>(rst, pageIndex, pageSize, dataCount);

            return View(list);
        }

        /// <summary>
        /// 添加导航
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult CreateSysNav()
        {
            return View(new SysNavViewModel()
            {
                NavOrd = _sysNavSvc.GetMaxOrd()+1
            });
        }

        /// <summary>
        /// 增加导航
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateSysNav(SysNavViewModel model)
        {
            if (ModelState.IsValid)
            {
                _sysNavSvc.CreateSysNav(new SysNav()
                {
                    NavName = model.NavName,
                    NavUrl = model.NavUrl,
                    NavIcons = model.NavIcon,
                    NavOrd = model.NavOrd
                });
                return RedirectToAction(nameof(SysNavList));
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
        public async Task<ActionResult> SysNavEdit(Guid id)
        {
            var data = await _sysNavSvc.GetOneSysNavById(id);

            return View(new SysNavViewModel()
            {
                Id = data.Id,
                NavName = data.NavName,
                NavUrl = data.NavUrl,
                NavIcon = data.NavIcons,
                NavOrd = data.NavOrd
            });
        }

        /// <summary>
        /// 编辑导航
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> SysNavEdit(SysNavViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _sysNavSvc.EditSysNav(new SysNav()
                {
                    Id = model.Id,
                    NavName = model.NavName,
                    NavIcons =  model.NavIcon,
                    NavUrl = model.NavUrl,
                    NavOrd = model.NavOrd,
                    CreateTime = DateTime.Now
                });
            }
            return RedirectToAction(nameof(SysNavList));
        }

        [HttpGet]
        public async Task<ActionResult> SysNavDetails(Guid id)
        {
            var data = await _sysNavSvc.GetOneSysNavById(id);

            if (data == null)
            {
                return RedirectToAction(nameof(SysNavList));
            }
            return View(data);
        }

        [HttpGet]
        public async Task<ActionResult> SysNavDelete(Guid id)
        {
            await _sysNavSvc.RemoveSysNav(id);
            return RedirectToAction(nameof(SysNavList));
        }
    }
}