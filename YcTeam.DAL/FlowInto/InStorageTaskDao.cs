using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YcTeam.IDAL;
using YcTeam.IDAL.FlowInto;
using YcTeam.Models;
using YcTeam.Models.FlowInto;

namespace YcTeam.DAL.FlowInto
{

    public class InStorageTaskDao : BaseService<InStorageTask>, IInStorageTaskDao
    {
        public InStorageTaskDao() : base(new YcContext())
        {
        }
        /// <summary>
        /// 连表查询，获取数据
        /// </summary>
        /// <returns></returns>
        public List<InStorageTask> AllInStorageTask() 
        {
            var t1 = Db.InStorageTask.Where(m => !m.IsRemoved);
            var t2 = Db.Provider.Where(m => !m.IsRemoved);
            var t3 = Db.Material.Where(m => !m.IsRemoved);

            //var providerList = (
            //    from inStorageTask in t1
            //        //关联供应商
            //    join provider in t2
            //        on inStorageTask.ProviderId equals provider.Id
            //        //关联物料
            //    join material in t3
            //        on inStorageTask.MaterialId equals material.Id
            //    select new { inStorageTask, provider, material }
            //).ToList();
            ////外键实体填充
            //foreach (var x in providerList)
            //{
            //    x.inStorageTask.ProviderId = x.provider.Id;
            //    x.inStorageTask.MaterialId = x.material.Id;
            //}
            //return providerList.Select(m => m.inStorageTask).OrderByDescending(m => m.CreateTime).ToList();
            return null;

        }

    }
}
