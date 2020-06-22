using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Webdiyer.WebControls.Mvc;
using YcTeam.BLL;
using YcTeam.BLL.Master;
using YcTeam.DTO;
using YcTeam.DTO.Master;
using YcTeam.IBLL.Master;
using YcTeam.MVCSite.Filters;
using YcTeam.MVCSite.Models.ProviderViewModels;

namespace YcTeam.MVCSite.Controllers
{
    [UserAuth]
    public class ProviderController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> ProviderList(int pageIndex = 1, int pageSize = 20)
        {
            //总页码、当前页码、可显示总页码
            var providerSvc = new ProviderService();
            //当前第n页数据
            var articles = await providerSvc.GetAllProvider(pageIndex, pageSize, false);
            //总个数
            var dataCount = await providerSvc.GetDataCount();
            //绑定分页
            var list = new PagedList<ProviderDto>(articles, pageIndex, pageSize, dataCount);

            return View(list);
        }

        [HttpGet]
        public ActionResult CreateProvider()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateProvider(ProviderCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                IProviderService providerSvc = new ProviderService();
                providerSvc.CreateProvider(model.Name);
                return RedirectToAction(nameof(ProviderList));
            }
            ModelState.AddModelError("", "您录入的信息有误");
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> ProviderEdit(Guid id)
        {
            var providerService = new ProviderService();
            var data = await providerService.GetOneProviderById(id);

            return View(new ProviderEditViewModel()
            {
                Id = data.Id,
                Name = data.Name
            });
        }

        [HttpPost]
        public async Task<ActionResult> ProviderEdit(Models.ProviderViewModels.ProviderEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var providerService = new ProviderService();
                await providerService.EditProvider(model.Id,model.Name);
                return RedirectToAction(nameof(ProviderList));
            }
            else
            {
                await new ProviderService().CreateProvider(model.Name);
                return View(model);
            }
        }

        [HttpGet]
        public async Task<ActionResult> ProviderDetails(Guid? id)
        {
            var providerService = new ProviderService();
            if (id == null || !await providerService.ExistsProvider(id.Value))
            {
                return RedirectToAction(nameof(ProviderList));
            }
            return View(await providerService.GetOneProviderById(id.Value));
        }

        [HttpGet]
        public async Task<ActionResult> ProviderDelete(Guid id)
        {
            var providerService = new ProviderService();
            await providerService.RemoveProvider(id);
            return RedirectToAction(nameof(ProviderList));
        }
    }
}