using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using YcTeam.IDAL.Master;
using YcTeam.IDAL.System;
using YcTeam.Models;
using YcTeam.Models.Master;
using YcTeam.Models.Sys;

namespace YcTeam.DAL.System
{
    public class SysNavDao : BaseService<SysNav>, ISysNavDao
    {
        public SysNavDao() : base(new YcContext())
        {

        }

        public async Task<int> GetMaxOrd()
        {
            return await GetAllAsync().MaxAsync(m => m.NavOrd);
        }
    }
}
