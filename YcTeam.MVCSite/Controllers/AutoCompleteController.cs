using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using YcTeam.BLL.Master;
using System.Threading.Tasks;

namespace YcTeam.MVCSite.Controllers
{
    public class AutoCompleteController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 查询物料
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<JsonResult> MaterialSearch(string key)
        {
            var materialService = new MaterialService();
            var list = await materialService.SearchMaterial(key);

            List<Object> jsonList = new List<Object>();
            foreach (var m in list)
            {
                var json = new
                {
                    id = m.Id,
                    text = m.Code + " : " + m.Describe,
                    highlight = m.Code + " : " + m.Describe
                };
                jsonList.Add(json);
            }

            return Json(Json(jsonList), JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 查询供应商
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<JsonResult> ProviderSearch(string key)
        {
            var providerService = new ProviderService();
            var list = await providerService.SearchProvider(key);

            List<Object> jsonList = new List<Object>();
            foreach (var m in list)
            {
                var json = new
                {
                    id = m.Id,
                    text = m.Name,
                    highlight = m.Name
                };
                jsonList.Add(json);
            }

            return Json(Json(jsonList), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SysDepartSearch(string key)
        {
            List<Object> list = new List<Object>();

            if (key.Equals("554724"))
            {
                var json1 = new
                {
                    id = "1",
                    text = "抢到一双43码的黑白熊猫!",
                    highlight = "554724-001 Air Jordan 黑白熊猫 43码"
                };
                list.Add(json1);

            }

            if (key.Equals("554725"))
            {
                for (var i = 1; i < 15; i++)
                {
                    var json2 = new {
                        id = i,
                        text = "恭喜，抢购成功！",
                        highlight = "554725-00"+i.ToString()+"Air Jordan gs 黑白熊猫 34码"
                    };
                    list.Add(json2);
                }
            }

            if (key.Equals("1"))
            {
                for (var i = 1; i < 515; i++)
                {
                    var json2 = new
                    {
                        id = i,
                        text = "恭喜，抢购成功！",
                        highlight = "黑白熊猫"+i.ToString()
                    };
                    list.Add(json2);
                }
            }

            return Json(Json(list), JsonRequestBehavior.AllowGet);
        }
    }
}