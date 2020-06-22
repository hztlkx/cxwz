using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.WebPages;
using Webdiyer.WebControls.Mvc;
using YcTeam.BLL.FlowPlan;
using YcTeam.BLL.System;
using YcTeam.BLL.WorkFlow;
using YcTeam.DTO.FlowPlan;
using YcTeam.DTO.System;
using YcTeam.DTO.WorkFlow;
using YcTeam.IBLL.FlowPlan;
using YcTeam.IBLL.System;
using YcTeam.IBLL.WorkFlow;
using YcTeam.Models.FlowPlan;
using YcTeam.Models.Master;
using YcTeam.Models.Sys;
using YcTeam.Models.WorkFlow;
using YcTeam.MVCSite.Filters;

namespace YcTeam.MVCSite.Controllers
{
    [UserAuth]
    //工作流控制器
    public class FlowController : Controller
    {
        readonly ISysDepartService _sysDepartSvc = new SysDepartService();
        readonly ISysRoleService _sysRoleSvc = new SysRoleService();
        readonly IFlowInstanceService _flowInstanceSvc = new FlowInstanceService();
        readonly IFlowNodeService _flowNodeService = new FlowNodeService();
        readonly IPlanService _planService = new PlanService();

        // GET: Flow
        public ActionResult Index()
        {
            return View();
        }

        #region 工作流节点
        [HttpGet]
        public async Task<ActionResult> FlowNodeList(int pageIndex = 1, int pageSize = 20)
        {
            //当前第n页数据
            var data = await _flowNodeService.GetAllFlowNode(pageIndex, pageSize, true);
            //总个数
            var dataCount = await _flowNodeService.GetDataCount();
            //绑定分页
            var list = new PagedList<FlowNodeDto>(data, pageIndex, pageSize, dataCount);

            return View(list);
        }

        /// <summary>
        /// 新增流程节点
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> FlowNodeCreate()
        {
            await SelectRole(Guid.Empty);//设置角色
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> FlowNodeCreate(FlowNodeDto model)
        {
            if (ModelState.IsValid)
            {
                await _flowNodeService.CreateFlowNode(new FlowNode()
                {
                    NextNodeNumber = model.NextNodeNumber,
                    PreNodeNumber = model.PreNodeNumber,
                    NodeName = model.NodeName,
                    NodeNumber = model.NodeNumber,
                    SysRoleId = model.SysRoleId,
                    UpdateTime = DateTime.Now
                });
                return RedirectToAction(nameof(FlowNodeList));
            }

            await SelectRole(Guid.Empty);//设置角色
            return View();
        }

        /// <summary>
        /// 更新流程节点
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> FlowNodeEdit(Guid id)
        {
            var model = await _flowNodeService.GetOneById(id);
            await SelectRole(model.SysRoleId);//设置角色

            return View(model);
        }

        /// <summary>
        /// 更新流程节点
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> FlowNodeEdit(FlowNodeDto model)
        {
            if (ModelState.IsValid)
            {
                await _flowNodeService.EditFlowNode(new FlowNode()
                {
                    Id = model.Id,
                    NextNodeNumber = model.NextNodeNumber,
                    PreNodeNumber = model.PreNodeNumber,
                    NodeName = model.NodeName,
                    NodeNumber = model.NodeNumber,
                    SysRoleId = model.SysRoleId,
                    UpdateTime = DateTime.Now
                });
                return RedirectToAction(nameof(FlowNodeList));
            }
            await SelectRole(Guid.Empty);//设置角色
            return View(model);
        }

        /// <summary>
        /// 删除流程节点
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> FlowNodeDelete(Guid id)
        {
            await _flowNodeService.RemoveFlowNode(id);
            return RedirectToAction(nameof(FlowNodeList));
        }
        #endregion


        #region 计划申请
        /// <summary>
        /// 计划流程清单
        /// </summary>
        /// <returns></returns>
        public ActionResult PlanList(int pageIndex = 1, int pageSize = 20)
        {
            Guid? sysDepartId = Guid.Parse(Session[CommonSession.SysDepartId].ToString());
            var isSuper = Common.HasRole(Common.RoleType.Super);
            if (isSuper)
            {
                sysDepartId = null;
            }
            var data = _planService.GetAllPlan(2020,6,sysDepartId, pageSize, pageIndex, false);

            var flowNode = _flowNodeService.GetAllFlowNode();
            var selectList = new List<SelectListItem>();
            foreach (var item in flowNode.Result)
            {
                selectList.Add(new SelectListItem { Text = item.NodeName, Value = item.Id.ToString()});
            }
            ViewBag.FlowNodeList = selectList;

            var list = data.ToList();
            return View(list);
        }

        /// <summary>
        /// 计划提报
        /// </summary>
        /// <param name="planId">计划编号</param>
        /// <param name="departId">部门编号</param>
        /// <param name="projectName">项目名称</param>
        /// <param name="nextNodeNumber">工作流编号</param>
        /// <param name="statusName">状态名称</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> PlanCreate(Guid planId,Guid departId,
            string projectName,int nextNodeNumber, string statusName)
        {
            var flowList = _flowInstanceSvc.GetAllFlowInstanceByPlanId(planId);
            var list = await _planService.GetMaterialByPlan(planId);

            await SelectUser(nextNodeNumber, departId);

            return View(new PlanDto()
            {
                PlanId = planId,
                ProjectName = projectName,
                StatusName = statusName,
                PlanMaterialDto = new PlanMaterialDto(),
                PlanMaterialList = list
            });
        }

        [HttpPost]
        public async Task<ActionResult> PlanCreate(PlanDto model,string materialHidden)
        {
            if (!materialHidden.IsEmpty())
            {
                await _planService.CreatePlanMaterial(new PlanMaterial()
                {
                    PlanId = model.PlanId,
                    MaterialId= Guid.Parse(materialHidden),
                    TechNumber = model.PlanMaterialDto.TechNumber,
                    Price = model.PlanMaterialDto.Price,
                    Num = model.PlanMaterialDto.Num,
                    Note = model.PlanMaterialDto.Note,
                    PlanDate = model.PlanMaterialDto.PlanDate,
                    UpdateTime = DateTime.Now,
                    CreateTime = DateTime.Now
                });
            }

            var list = await _planService.GetMaterialByPlan(model.PlanId);

            return View(new PlanDto()
            {
                PlanId = model.PlanId,
                ProjectName = model.ProjectName,
                PlanMaterialList = list
            });
        }

        /// <summary>
        /// 修改计划物料
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> PlanMaterialEdit()
        {
            StreamReader sr = new StreamReader(Request.InputStream);
            string str = sr.ReadToEnd();
            JavaScriptSerializer js = new JavaScriptSerializer();
            var list = js.Deserialize<List<PlanMaterial>>(str);

            var result = false;
            foreach (var planMaterial in list)
            {
                result = await _planService.PlanMaterialEdit(new PlanMaterial()
                {
                    Id = planMaterial.Id,
                    PlanId = planMaterial.PlanId,
                    MaterialId = planMaterial.MaterialId,
                    TechNumber = planMaterial.TechNumber,
                    Price = planMaterial.Price,
                    Num = planMaterial.Num,
                    Note = planMaterial.Note,
                    StatusName = planMaterial.StatusName,
                    PlanDate = planMaterial.PlanDate,
                    UpdateTime = DateTime.Now,
                    CreateTime = DateTime.Now
                });
            }

            return Json(Json(result), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 删除计划物料
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> PlanMaterialDelete(string id)
        {
            var result = await _planService.PlanMaterialDelete(Guid.Parse(id));
            return Json(Json(result), JsonRequestBehavior.AllowGet);
        }
        #endregion

        /// <summary>
        /// 提交工作流
        /// </summary>
        /// <returns></returns>
        [UserAuth]
        public ActionResult FlowSubmit(int flowType,int flowInstanceId)
        {
            switch (flowType)
            {
                case 1: break;
                case 2: break;
                case 3: break;
                case 4: break;
            }

            //当前登录人员信息
            var userInfo = Session[CommonSession.CurrentUser] as SysUser;
            if (userInfo != null)
            {
                //2.创建工作流
                var flowInstance = new FlowInstance
                {
                    //工作流当前节点
                    NodeNumber = _flowNodeService.GetFlowNodeByNodeName("工程申请计划").Result.Id,
                    //申请处理状态
                    StatusName = "已申请",
                    //申请人（流程发起人）
                    StartUserId = userInfo.Id,
                    //当前操作者
                    OperatingUserId = userInfo.Id,
                     //下一个节点处理人
                    ToDoUserId = _flowNodeService.GetFlowNodeByNodeName("工程审核计划").Result.SysRoleId,
                    UpdateTime = DateTime.Now,
                    CreateTime = DateTime.Now,
                    //已操作过的人
                    OperatedUserId = userInfo.Id
                };
                
                //添加
                _flowInstanceSvc.CreateFlowInstance(flowInstance);

                ////3.新建流操作记录
                var flowRecord = new FlowRecord
                {
                    FlowInstanceId = flowInstance.Id,//流程实例Id
                    IsRead = true,////是否已读
                    IsPass = true,////是否通过
                    UpdateTime = DateTime.Now
                };
            }
            return View();
        }

        /// <summary>
        /// 获取待办审批
        /// </summary>
        /// <returns></returns>
        public ActionResult GetList(int page)
        {
            //当前登录人员信息
            if (Session[CommonSession.CurrentUser] is SysUser userInfo)
            {
                //此处获取待办人列表，根据待办人Id 等于 当前登录用户Id获取
                //var list = _flowInstanceSvc.GetAllFlowInstance().Result
                //    .Where(x => userInfo.SysUserRoles.Contains(x.ToDoRole.RoleName))
                //    .OrderByDescending(x => x.CreateTime)
                //    .ToList(); 

                //var count = list.Count();
                //var pagedList = list.ToPage(page, count).ToList();

                //var todoList = pagedList.Select(x => new
                //{
                //    x.Id,
                //    x.Starter,//申请人
                //    x.Operator, //上一操作人
                //    UpdateTime = x.UpdateTime.Format("yyyy年MM月dd日 hh:mm"), //更新时间,
                //    x.RequisitionId //对应申请单id
                //}).ToList();
            }
            return View();
        }


        /// <summary>
        /// 设置角色
        /// </summary>
        /// <returns></returns>
        private async Task SelectRole(Guid id)
        {
            //获取角色
            var selectList = new List<SelectListItem>();
            foreach (var item in await _sysRoleSvc.GetAllSysRole())
            {
                bool isSelected = item.Id == id;
                selectList.Add(new SelectListItem { Text = item.RoleName, Value = item.Id.ToString(),Selected = isSelected });
            }
            ViewBag.SysRoleList = selectList;
        }

        /// <summary>
        ///  设置下一流程用户
        /// </summary>
        /// <param name="nextNodeNumber">工作流编号</param>
        /// <param name="departId">部门名称</param>
        /// <returns></returns>
        private async Task SelectUser(int nextNodeNumber,Guid departId)
        {
            var selectList = new List<SelectListItem>();
            List<SysUser> userList = await _flowNodeService.NextSysUser(nextNodeNumber);
            foreach (var item in userList)
            {
                if (item.SysDepartId == departId && nextNodeNumber<104)
                {
                    selectList.Add(new SelectListItem { Text = item.RealName, Value = item.Id.ToString() });
                }
            }
            ViewBag.SysUserList = selectList;
        }
    }
}