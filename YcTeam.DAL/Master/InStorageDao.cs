using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YcTeam.DAL;
using YcTeam.IDAL.Master;
using YcTeam.Models;
using YcTeam.Models.Master;

namespace YcTeam.DAL.Master
{
    public class InStorageDao : BaseService<InStorage>, IInStorageDao
    {
        public InStorageDao() : base(new YcContext())
        {

        }
    }
}
