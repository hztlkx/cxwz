using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using YcTeam.DAL.System;
using YcTeam.DAL.WorkFlow;
using YcTeam.IBLL.Master;
using YcTeam.IBLL.System;
using YcTeam.IBLL.WorkFlow;
using YcTeam.IDAL.System;
using YcTeam.Models;
using YcTeam.Models.Sys;
using YcTeam.Models.WorkFlow;

namespace YcTeam.BLL.WorkFlow
{
    public class FlowInstanceService : IFlowInstanceService
    {
        /// <summary>
        /// 创建工作流实例
        /// </summary>
        /// <returns></returns>
        public async Task CreateFlowInstance(FlowInstance flowInstance)
        {
            using (var flowInstanceDao = new FlowInstanceDao())
            {
                await flowInstanceDao.CreateAsync(flowInstance);
            }
        }

        public async Task<List<FlowInstance>> GetAllFlowInstance()
        {
            using (var flowInstanceDao = new FlowInstanceDao())
            {
                return await flowInstanceDao.GetAllAsync().ToListAsync();
            }
        }

        public async Task<List<FlowInstance>> GetAllFlowInstanceByPlanId(Guid id)
        {
            using (var flowInstanceDao = new FlowInstanceDao())
            {
                return await flowInstanceDao.GetAllAsync().Where(m=>m.PlanId == id).ToListAsync();
            }
        }

        public async Task RemoveFlowInstanceByPlanId(Guid planId)
        {
            using (var flowInstanceDao = new FlowInstanceDao())
            {
                var instanceList = flowInstanceDao.GetAllAsync().Where(m => m.PlanId == planId).ToList();
                foreach (var flowInstance in instanceList)
                {
                    using (var flowRecordDao = new FlowRecordDao())
                    {
                        var recordList = flowRecordDao.GetAllAsync().Where(m => m.FlowInstanceId == flowInstance.Id).ToList();

                        foreach (var record in recordList)
                        {
                           await flowRecordDao.RemoveAsync(record);
                        }
                    }

                    await flowInstanceDao.RemoveAsync(flowInstance);
                }
            }
        }
    }
}
