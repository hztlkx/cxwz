using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YcTeam.DTO.FlowInto;
using YcTeam.Models.FlowInto;

namespace YcTeam.IBLL.FlowInto
{
    public interface IInStorageReceiveService
    {
        //创建接收入库任务
        Task CreateInStorageReceive(string Batch, Guid ProviderId, Guid MaterialId, int PutNumber,string File, Guid SysUserId, string Note);
       
    }
}
