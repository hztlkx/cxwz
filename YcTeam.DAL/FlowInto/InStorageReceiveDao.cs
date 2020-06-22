using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YcTeam.IDAL.FlowInto;
using YcTeam.Models;
using YcTeam.Models.FlowInto;

namespace YcTeam.DAL.FlowInto
{
    public class InStorageReceiveDao : BaseService<InStorageReceive>, IInStorageReceiveDao
    {
        public InStorageReceiveDao() : base(new YcContext())
        {
        }
    }
}
