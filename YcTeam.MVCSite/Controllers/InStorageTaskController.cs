using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Webdiyer.WebControls.Mvc;
using YcTeam.BLL.FlowInto;
using YcTeam.BLL.Master;
using YcTeam.DTO.FlowInto;
using YcTeam.IBLL.FlowInto;
using YcTeam.Models.FlowInto;
using YcTeam.Models.Master;
using YcTeam.MVCSite.Filters;
using YcTeam.MVCSite.Models.InStorageTaskViewModels;

namespace YcTeam.MVCSite.Controllers
{
    [UserAuth]
    public class InStorageTaskController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<ActionResult> InStorageTaskList(int pageIndex = 5, int pageSize = 1)
        {
            //总页码、当前页码、可显示总页码
            var inStorageTaskService = new InStorageTaskService();
            //当前第n页数据
            var articles = inStorageTaskService.getAllInStorage(pageIndex, pageSize, false);
            //总个数
            var dataCount = await inStorageTaskService.GetDataCount();
            //绑定分页
            var list = new PagedList<InStorageTaskDto>(articles, pageIndex, pageSize, dataCount);
            return View(list);
        }

        [HttpGet]
        public async Task<ActionResult> CreateInStorageTask()
        {
            //权限集合
            var selectList = new List<SelectListItem>();
            var list = await new InStorageTaskService().GetAllInStorage();
            foreach (var item in list)
            {
                selectList.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
            }
            ViewBag.SysDepartList = selectList;
            return View();
        }
        /// <summary>
        /// 创建新的入库任务
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  ActionResult CreateInStorageTask(InStorageTaskDto model, string materialHidden,string providerHidden)
        {
            if (ModelState.IsValid)              
            {            
                IInStorageTaskService iInStorageTaskService = new InStorageTaskService();
                iInStorageTaskService.CreateInStorageTask(model.Organization,model.SysBatch, model.State, Guid InStorageId, Guid.Parse(materialHidden), model.PlanNumber, model.PlanTime, Guid.Parse(providerHidden), model.Note);
                return RedirectToAction(nameof(InStorageTaskList));
            }
            ModelState.AddModelError("", @"您录入的信息有误");
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> InStorageTaskDelete(Guid id)
        {
            var inStorageTaskService = new InStorageTaskService();
            await inStorageTaskService.RemoveInStorage(id);
            return RedirectToAction(nameof(InStorageTaskList));
        }

        [HttpGet]
        public async Task<ActionResult> InsertInStorageTask(Guid id)
        {
            var inStorageTaskService = new InStorageTaskService();
            var data = await inStorageTaskService.GetOneMaterialById(id);

            //加载数据
            List<SelectListItem> selectList = new List<SelectListItem>();
            var list = await new InStorageTaskService().GetAllProvider();
            foreach (var item in list)
            {
                selectList.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
            }
            ViewBag.ProviderList = selectList;          
            return View(new InStorageTaskCreateViewModel()
            {
                SysBatch=data.SysBatch,
                Organization=data.Organization
            });
        }
        /// <summary>
        /// 添加物料
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> InsertInStorageTask(InStorageTaskDto model, string materialHidden)
        {
            if (ModelState.IsValid)
            {
                var inStorageTaskService = new InStorageTaskService();
               // await inStorageTaskService.CreateInStorageTask(model.Organization,model.SysBatch,  Guid.Parse(materialHidden), model.PlanNumber, model.State, model.Note);
                return RedirectToAction(nameof(InStorageTaskList));
            }
            else
            {
               // await new InStorageTaskService().CreateInStorageTask(model.Organization,  model.SysBatch,  Guid.Parse(materialHidden), model.PlanNumber, model.State, model.Note);
                return View(model);
            }
        }

    }
}