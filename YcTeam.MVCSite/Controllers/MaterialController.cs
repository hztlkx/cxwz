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
using YcTeam.MVCSite.Models.MaterialCreateViewModels;
using YcTeam.MVCSite.Models.MaterialViewModels;


namespace YcTeam.MVCSite.Controllers
{
    [UserAuth]
    public class MaterialController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> MaterialList(int pageIndex = 1, int pageSize = 5)
        {
            //总页码、当前页码、可显示总页码
            var materialSvc = new MaterialService();
            //当前第n页数据
            var articles = await materialSvc.GetAllMaterial(pageIndex, pageSize, false);
            //总个数
            var dataCount = await materialSvc.GetDataCount();
            //绑定分页
            var list = new PagedList<MaterialDto>(articles, pageIndex, pageSize, dataCount);

            return View(list);
        }
        
        [HttpGet]
        public ActionResult CreateMaterial()
        {
            return View();
        }

        /// <summary>
        /// 添加新物料
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateMaterial(MaterialCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                IMaterialService materialSvc = new MaterialService();
                materialSvc.CreateMaterial(model.Code, model.LargyCategory, model.SmallCategory, model.Describe, model.Unit, model.Note);
                return RedirectToAction(nameof(MaterialList));
            }
            ModelState.AddModelError("", @"您录入的信息有误");
            return View();
        }
        /// <summary>
        /// 获取修改物料信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> MaterialEdit(Guid id)
        {
            var materialService = new MaterialService();
            var data = await materialService.GetOneMaterialById(id);

            return View(new MaterialEditViewModel()
            {
                Id = data.Id,
                Code = data.Code,
                LargyCategory = data.LargeCategory,
                SmallCategory = data.SmallCategory,
                Describe = data.Describe,
                Unit = data.Unit,
                Note = data.Note
            });
        }
        /// <summary>
        /// 修改物料信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> MaterialEdit(Models.MaterialViewModels.MaterialEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var materialService = new MaterialService();
                await materialService.EditMaterial(model.Id, model.Code, model.LargyCategory, model.SmallCategory, model.Describe, model.Unit, model.Note);
                return RedirectToAction(nameof(MaterialList));
            }
            else
            {
                await new MaterialService().CreateMaterial(model.Code, model.LargyCategory, model.SmallCategory, model.Describe, model.Unit, model.Note);
                return View(model);
            }
        }
        /// <summary>
        /// 物料详细信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> MaterialDetails(Guid? id)
        {
            var materialService = new MaterialService();
            if (id == null || !await materialService.ExistsMaterial(id.Value))
            {
                return RedirectToAction(nameof(MaterialList));
            }
            return View(await materialService.GetOneMaterialById(id.Value));
        }

        [HttpGet]
        public async Task<ActionResult> MaterialDelete(Guid id)
        {
            var materialService = new MaterialService();
            await materialService.RemoveMaterial(id);
            return RedirectToAction(nameof(MaterialList));
        }
    }
}