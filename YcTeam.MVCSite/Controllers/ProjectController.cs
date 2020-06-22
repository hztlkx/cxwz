using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Webdiyer.WebControls.Mvc;
using YcTeam.BLL;
using YcTeam.BLL.FlowPlan;
using YcTeam.BLL.Master;
using YcTeam.BLL.System;
using YcTeam.DTO.FlowPlan;
using YcTeam.DTO.Master;
using YcTeam.IBLL.Master;
using YcTeam.IBLL.FlowPlan;
using YcTeam.IBLL.System;
using YcTeam.Models.FlowPlan;
using YcTeam.Models.Master;
using YcTeam.MVCSite.Filters;
using YcTeam.BLL.WorkFlow;
using YcTeam.IBLL.WorkFlow;
using YcTeam.Models.Sys;
using YcTeam.Models.WorkFlow;
using YcTeam.MVCSite.Models.ProjectViewModels;

namespace YcTeam.MVCSite.Controllers
{
    [UserAuth]
    public class ProjectController : Controller
    {
        private readonly IProjectService _projectSvc = new ProjectService();
        private readonly ISysDepartService _sysDepartService = new SysDepartService();
        private readonly IPlanService _planService = new PlanService();
        private readonly IFlowInstanceService _flowInstanceSvc = new FlowInstanceService();
        private readonly IFlowNodeService _flowNodeService = new FlowNodeService();

        #region 项目信息
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> ProjectList(int pageIndex = 1, int pageSize = 20)
        {
            ViewBag.AllowShow = Common.HasRole(Common.RoleType.Inspection);

            //总页码、当前页码、可显示总页码
            var projectSvc = new ProjectService();
            //当前第n页数据
            var data = await projectSvc.GetAllProject(pageIndex, pageSize, false);
            //总个数
            var dataCount = await projectSvc.GetDataCount();
            //绑定分页
            var list = new PagedList<DTO.Master.ProjectDto>(data, pageIndex, pageSize, dataCount);

            return View(list);
        }

        [HttpGet]
        public async Task<ActionResult> CreateProject()
        {
            await GetSysDepart(Guid.NewGuid());
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateProject(ProjectCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                //添加项目
                var project = new Project()
                {
                    ProjectName = model.ProjectName,
                    WBSCode = model.WBSCode,
                    SmallProjectName = model.SmallProjectName,
                    SmallWBSCode = model.SmallWBSCode,
                    Funds = model.Funds,
                    VoltageGrade = model.VoltageGrade,
                    SysDepartId = model.SysDepartId,
                    PickingPeople = model.PickingPeople,
                    ContactNumber = model.ContactNumber
                };

                //添加计划
                var plan = new Plan()
                {
                    Project = project, //添加项目
                    UpdateTime = DateTime.Now,
                    CreateTime = DateTime.Now
                };

                //添加流程实体
                var flowInstance = new FlowInstance()
                {
                    NodeNumber = _flowNodeService.GetFlowNodeByNodeName("工程申请计划").Result.Id,
                    StatusName = "待提交",
                    Plan = plan,
                    UpdateTime = DateTime.Now,
                    CreateTime = DateTime.Now
                };

                //插入数据库
                _flowInstanceSvc.CreateFlowInstance(flowInstance);

                return RedirectToAction(nameof(ProjectList));
            }
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> ProjectEdit(Guid id)
        {
            var projectService = new ProjectService();
            var data = await projectService.GetOneProjectById(id);

            var planList = _planService.GetAllByProjectId(id);

            if (planList.Result.Count == 0)
            {
                //添加计划
                var plan = new Plan()
                {
                    ProjectId = id, //添加项目
                    UpdateTime = DateTime.Now,
                    CreateTime = DateTime.Now
                };

                //添加流程实体
                var flowInstance = new FlowInstance()
                {
                    NodeNumber = _flowNodeService.GetFlowNodeByNodeName("工程申请计划").Result.Id,
                    StatusName = "待提交",
                    Plan = plan,
                    UpdateTime = DateTime.Now,
                    CreateTime = DateTime.Now
                };

                //插入数据库
                await _flowInstanceSvc.CreateFlowInstance(flowInstance);
            }

            //权限集合
            List<SelectListItem> selectList = new List<SelectListItem>();
            var list = await _sysDepartService.GetAllSysDepart();
            foreach (var item in list)
            {
                if (item.DepartType == 2)
                {
                    selectList.Add(data.SysDepartId == item.Id
                        ? new SelectListItem { Text = item.DepartName, Value = item.Id.ToString(), Selected = true }
                        : new SelectListItem { Text = item.DepartName, Value = item.Id.ToString() });
                }
            }
            ViewBag.SysDepartList = selectList;

            return View(new ProjectEditViewModel()
            {
                ProjectName = data.ProjectName,
                WBSCode = data.WBSCode,
                SmallProjectName = data.SmallProjectName,
                SmallWBSCode = data.SmallWBSCode,
                Funds = data.Funds,
                VoltageGrade = data.VoltageGrade,
                SysDepartId = data.SysDepartId,
                PickingPeople = data.PickingPeople,
                ContactNumber = data.ContactNumber,
            });
        }

        [HttpPost]
        public async Task<ActionResult> ProjectEdit(Models.ProjectViewModels.ProjectEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var projectService = new ProjectService();
                await projectService.EditProject(new ProjectDto()
                {
                    Id = model.Id,
                    ProjectName = model.ProjectName,
                    WBSCode = model.WBSCode,
                    SmallProjectName = model.SmallProjectName,
                    SmallWBSCode = model.SmallWBSCode,
                    Funds = model.Funds,
                    VoltageGrade = model.VoltageGrade,
                    SysDepartId = model.SysDepartId,
                    PickingPeople = model.PickingPeople,
                    ContactNumber = model.ContactNumber
                });
                return RedirectToAction(nameof(ProjectList));
            }

            return View();
        }

        [HttpGet]
        public async Task<ActionResult> ProjectDetails(Guid? id)
        {
            var projectService = new ProjectService();
            if (id == null || !await projectService.ExistsProject(id.Value))
            {
                return RedirectToAction(nameof(ProjectList));
            }
            return View(await projectService.GetOneProjectById(id.Value));
        }

        [HttpGet]
        public async Task<ActionResult> ProjectDelete(Guid id)
        {
            var planList = _planService.GetAllByProjectId(id);
            foreach (var plan in planList.Result)
            {
                await _flowInstanceSvc.RemoveFlowInstanceByPlanId(id);//删除流程实体
                await _planService.RemovePlan(plan.Id);//删除计划单
            }

            var projectService = new ProjectService();
            await projectService.RemoveProject(id);

            return RedirectToAction(nameof(ProjectList));
        }
        #endregion

        #region 施工队分配
        /// <summary>
        /// 施工项目部清单
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ConstructionList(int pageIndex = 1, int pageSize = 20)
        {
            ViewBag.AllowShow = Common.HasRole(Common.RoleType.NoConstruction);

            var departType = Session[CommonSession.SysDepartType].ToString();
            var departId = Guid.Parse(Session[CommonSession.SysDepartId].ToString());
            //获取项目分配情况
            var data = _planService.GetAllPlan(departId, departType);

            //总个数
            var dataCount = data.Count;
            //绑定分页
            var list = new PagedList<PlanDto>(data, pageIndex, pageSize, dataCount);

            return View(list);
        }

        /// <summary>
        /// 修改分配施工项目部
        /// </summary>
        /// <param name="plan">计划单实体</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ConstructionEdit(PlanDto plan)
        {
            await GetOtherSysDepart(1, plan.SysDepartId);
            

            return View(new PlanDto()
            {
                Id = plan.Id,
                ProjectId = plan.ProjectId,
                SysDepartOwnerName = plan.SysDepartName,
                ProjectName = plan.ProjectName
            });
        }

        /// <summary>
        /// 修改分配施工项目部
        /// </summary>
        /// <param name="id">计划单编号</param>
        /// <param name="projectId">项目编号</param>
        /// <param name="sysDepartId">施工项目部编号</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> ConstructionEdit(Guid id,Guid projectId,Guid sysDepartId)
        {
            await _planService.EditPlan(new Plan()
            {
                Id = id,
                ProjectId = projectId,
                SysDepartId = sysDepartId,
                UpdateTime = DateTime.Now,
                CreateTime = DateTime.Now
            });
            return RedirectToAction(nameof(ConstructionList));
        }



        /// <summary>
        /// 新增分配施工项目部
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="projectName"></param>
        /// <param name="departName"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ConstructionCreate(Guid projectId, string projectName,string departName)
        {
            await GetNewDepartList(projectId);

            return View(new PlanDto()
            {
                ProjectId = projectId,
                SysDepartName = departName,
                ProjectName = projectName
            });
        }

        /// <summary>
        /// 新增分配施工项目部
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="sysDepartId"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> ConstructionCreate(Guid projectId, Guid sysDepartId)
        {
            //添加计划
            var plan = new Plan()
            {
                SysDepartId = sysDepartId,
                ProjectId = projectId,
                UpdateTime = DateTime.Now,
                CreateTime = DateTime.Now
            };

            //添加流程实体
            var flowInstance = new FlowInstance()
            {
                NodeNumber = _flowNodeService.GetFlowNodeByNodeName("工程申请计划").Result.Id,
                StatusName = "待提交",
                Plan = plan,
                UpdateTime = DateTime.Now,
                CreateTime = DateTime.Now
            };
            await _flowInstanceSvc.CreateFlowInstance(flowInstance);
            
            return RedirectToAction(nameof(ConstructionList));
        }

        /// <summary>
        /// 删除施工项目部分配信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ConstructionDelete(Guid id)
        {
            await _flowInstanceSvc.RemoveFlowInstanceByPlanId(id);
            await _planService.RemovePlan(id);
            return RedirectToAction(nameof(ConstructionList));
        }

        /// <summary>
        /// 获取所有业主项目部部门
        /// </summary>
        /// <returns></returns>
        private async Task GetSysDepart(Guid departId)
        {
            //权限集合
            var selectList = new List<SelectListItem>();
            var list = await _sysDepartService.GetAllSysDepart(2);
            foreach (var item in list)
            {
                selectList.Add(item.Id == departId
                    ? new SelectListItem { Text = item.DepartName, Value = item.Id.ToString(), Selected = true }
                    : new SelectListItem { Text = item.DepartName, Value = item.Id.ToString() });
            }
            ViewBag.SysDepartList = selectList;
        }

        /// <summary>
        /// 根据类型获取部门
        /// </summary>
        /// <param name="typeNumber"></param>
        /// <param name="departId"></param>
        /// <returns></returns>
        private async Task GetOtherSysDepart(int typeNumber,Guid? departId)
        {
            //权限集合
            var selectList = new List<SelectListItem>();
            var list = await _planService.GetOtherSysDepart(departId);
            foreach (var item in list)
            {
                if (item.DepartType == typeNumber)
                {
                    selectList.Add(item.Id == departId
                        ? new SelectListItem {Text = item.DepartName, Value = item.Id.ToString(), Selected = true}
                        : new SelectListItem {Text = item.DepartName, Value = item.Id.ToString()});
                }
            }
            ViewBag.SysDepartList = selectList;
        }

        /// <summary>
        /// 获取未新增的施工项目部
        /// </summary>
        /// <param name="projectId"></param>
        private async Task GetNewDepartList(Guid projectId)
        {
            //权限集合
            var selectList = new List<SelectListItem>();
            var list = await _planService.GetNewSysDepart(projectId);
            foreach (var item in list)
            {
                selectList.Add(new SelectListItem { Text = item.DepartName, Value = item.Id.ToString() });
            }
            ViewBag.SysDepartList = selectList;
        }

        #endregion
    }
}