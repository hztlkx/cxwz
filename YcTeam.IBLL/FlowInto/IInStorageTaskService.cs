using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YcTeam.DTO.FlowInto;

namespace YcTeam.IBLL.FlowInto
{
    public interface IInStorageTaskService
    {
        //创建入库任务
        Task CreateInStorageTask(string Organization,  string SysBatch, string State, Guid InStorageId, Guid MaterialId, int  Number, DateTime PlanTime,Guid ProviderId, string Note);
        //查询入库任务
        List<InStorageTaskDto> getAllInStorage(int pageSize, int pageIndex, bool asc);
        //获取总数量
        Task<int> GetDataCount();
        //删除入库任务
        Task RemoveInStorage(Guid id);
    }
}
