using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Webdiyer.WebControls.Mvc;
using YcTeam.BLL.FlowInto;
using YcTeam.DTO.FlowInto;
using YcTeam.IBLL.FlowInto;
using YcTeam.Models.FlowInto;
using YcTeam.MVCSite.Filters;

namespace YcTeam.MVCSite.Controllers
{
    [UserAuth]
    public class InStorageReceiveController:Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<ActionResult> InStorageReceiveTaskList(int pageIndex = 5, int pageSize = 1)
        {
            //总页码、当前页码、可显示总页码
            var inStorageReceiveService = new InStorageReceiveService();
            //当前第n页数据
            var articles = inStorageReceiveService.getAllInStorageReceive(pageIndex, pageSize, false);
            //总个数
            var dataCount = await inStorageReceiveService.GetDataCount();
            //绑定分页
            var list = new PagedList<InStorageReceiveDto>(articles, pageIndex, pageSize, dataCount);
            return View(list);
        }

        [HttpGet] 
        public async Task<ActionResult> CreateInStorageReceive()
        {
            //加载数据
            List<SelectListItem> selectList = new List<SelectListItem>();
            var list = await new InStorageTaskService().GetAllProvider();
            foreach (var item in list)
            {
                selectList.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
            }
            ViewBag.ProviderList = selectList;
            DateTime dt1 = Convert.ToDateTime("1970 - 01 - 01 00:00:00");
            TimeSpan ts = DateTime.Now-dt1;
            return View(new InStorageReceiveDto()
            {
                Batch="PC"+ts
            });

        }
        /// <summary>
        /// 创建新的入库任务
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateInStorageReceive(InStorageReceiveDto model, string materialHidden)
        {
            var userId =Common.GetUserId();
            if (ModelState.IsValid)
            {
                IInStorageReceiveService inStorageReceiveService = new InStorageReceiveService();
                inStorageReceiveService.CreateInStorageReceive(model.Batch, Guid.Parse(model.ProviderId.ToString()), Guid.Parse(materialHidden), model.PutNumber,model.File, Guid.Parse(userId.ToString()),model.Note);
                return RedirectToAction(nameof(InStorageReceiveTaskList));
            }
            ModelState.AddModelError("", @"您录入的信息有误");
            return View();
        }
    }
}