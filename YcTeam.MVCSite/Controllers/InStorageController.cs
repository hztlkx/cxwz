using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Webdiyer.WebControls.Mvc;
using YcTeam.BLL.Master;
using YcTeam.DTO.Master;
using YcTeam.IBLL.Master;
using YcTeam.MVCSite.Filters;
using YcTeam.MVCSite.Models.InStorageViewModels;

namespace YcTeam.MVCSite.Controllers
{
    [UserAuth]
    public class InStorageController:Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> InStorageList(int pageIndex = 1, int pageSize = 20)
        {
            //总页码、当前页码、可显示总页码
            var inStorageSvc = new InStorageService();
            //当前第n页数据
            var articles = await inStorageSvc.GetAllInStorage(pageIndex, pageSize, false);
            //总个数
            var dataCount = await inStorageSvc.GetDataCount();
            //绑定分页
            var list = new PagedList<InStorageDto>(articles, pageIndex, pageSize, dataCount);

            return View(list);
        }

        [HttpGet]
        public ActionResult CreateInStorage()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateInStorage(InStorageCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                IInStorageService inStorageSvc = new InStorageService();
                inStorageSvc.CreateInStorage(model.Code,model.Name,model.Category,model.Describe);
                return RedirectToAction(nameof(InStorageList));
            }
            ModelState.AddModelError("", @"您录入的信息有误");
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> InStorageEdit(Guid id)
        {
            var inStorageService = new InStorageService();
            var data = await inStorageService.GetOneInStorageById(id);

            return View(new InStorageEditViewModel()
            {
                Id = data.Id,
                Code = data.Code,
                Name=data.Name,
                Category=data.Category,
                Describe=data.Describe
            });
        }

        [HttpPost]
        public async Task<ActionResult> InStorageEdit(Models.InStorageViewModels.InStorageEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var inStorageService = new InStorageService();
                await inStorageService.EditInStorage(model.Id, model.Code,model.Name,model.Category,model.Describe);
                return RedirectToAction(nameof(InStorageList));
            }
            else
            {
                await new InStorageService().CreateInStorage(model.Code, model.Name, model.Category, model.Describe);
                return View(model);
            }
        }

        [HttpGet]
        public async Task<ActionResult> InStorageDetails(Guid? id)
        {
            var inStorageService = new InStorageService();
            if (id == null || !await inStorageService.ExistsInStorage(id.Value))
            {
                return RedirectToAction(nameof(InStorageList));
            }
            return View(await inStorageService.GetOneInStorageById(id.Value));
        }

        [HttpGet]
        public async Task<ActionResult> InStorageDelete(Guid id)
        {
            var inStorageService = new InStorageService();
            await inStorageService.RemoveInStorage(id);
            return RedirectToAction(nameof(InStorageList));
        }
    }
}