using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YcTeam.BLL.WorkFlow;
using YcTeam.BLL.WorkFlow.Base;
using YcTeam.BLL.WorkFlow.Users;
using YcTeam.DTO;
using YcTeam.MVCSite.Models;

namespace YcTeam.MVCSite.Controllers
{
    public class PlanController : Controller
    {
        // GET: Plan
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult PlanList()
        {
            return View();
        }
    }
}