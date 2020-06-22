using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YcTeam.DAL.FlowInto;
using YcTeam.DAL.Master;
using YcTeam.DTO.FlowInto;
using YcTeam.DTO.Master;
using YcTeam.IBLL.FlowInto;
using YcTeam.IDAL.FlowInto;
using YcTeam.IDAL.Master;
using YcTeam.Models.FlowInto;

namespace YcTeam.BLL.FlowInto
{
    public class InStorageTaskService : IInStorageTaskService
    {
        IProviderDao _providerDao = new ProviderDao();

        public async Task<InStorageTaskDto> GetOneMaterialById(Guid inStorageTaskId)
        {
            using (InStorageTaskDao inStorageTaskDao = new InStorageTaskDao())
            {
                return await inStorageTaskDao.GetAllAsync()
                    .Where(m => m.Id == inStorageTaskId)
                    .Select(m => new InStorageTaskDto()
                    {
                        Id = m.Id,
                        Organization = m.Organization,
                        SysBatch=m.SysBatch,
                        MaterialId = m.Material.Id,
                        Code = m.Material.Code,
                        Describe = m.Material.Describe,
                        Unit = m.Material.Unit,
                        PlanNumber = m.Number,
                        State = m.State,
                        Note = m.Note,
                        CreateTime = m.CreateTime
                    }).FirstAsync();
            }

        }

        public async Task<int> GetDataCount()
        {
            using (var inStorageTaskDao = new InStorageTaskDao())
            {
                return await inStorageTaskDao.GetAllAsync().CountAsync();
            }
        }

        public async Task RemoveInStorage(Guid id)
        {
            using (var inStorageTaskDao = new InStorageTaskDao())
            {
                await inStorageTaskDao.RemoveAsync(id);
            }
        }

        public async Task CreateInStorageTask(string Organization, string SysBatch, string State, Guid InStorageId, Guid MaterialId, int Number, DateTime PlanTime, Guid ProviderId, string Note)
        {
            using (var inStorageTaskDao = new InStorageTaskDao())
            {
                await inStorageTaskDao.CreateAsync(new InStorageTask()
                {
                    Organization = Organization,
                    SysBatch=SysBatch,
                    State = State,
                    InStorageId=InStorageId,
                    MaterialId = MaterialId,
                    Number = Number,
                    PlanTime=PlanTime,
                    ProviderId=ProviderId,
                    Note = Note,
                });
            }
        }

        public List<InStorageTaskDto> getAllInStorage(int pageSize, int pageIndex, bool asc)
        {
            using (var inStorageTaskDao = new InStorageTaskDao())
            {
                return inStorageTaskDao.GetAllByPageOrderAsync(pageIndex - 1, pageSize, asc).Select(m => new InStorageTaskDto()
                {
                        Id = m.Id,
                        Organization = m.Organization,
                        SysBatch=m.SysBatch,
                        State = m.State,
                        InStorageName=m.InStorage.Name,
                        Code = m.Material.Code,
                        Describe = m.Material.Describe,
                        Unit = m.Material.Unit,
                        PlanNumber = m.Number,
                        PlanTime=m.PlanTime,
                        ProviderName=m.Provider.Name,
                        Note = m.Note,
                        CreateTime = m.CreateTime
                    }).ToList();
            }
        }

        public async Task<List<ProviderDto>> GetAllProvider()
        {
            using (_providerDao)
            {
                return await _providerDao.GetAllOrderAsync(false)
                    .Select(m => new ProviderDto()
                    {
                        Id = m.Id,
                        Name = m.Name,
                    }).ToListAsync();
            }
        }

        public async Task<List<InStorageDto>> GetAllInStorage()
        {
            using (var inStorageDao = new InStorageDao())
            {
                return await inStorageDao.GetAllOrderAsync(false)
                    .Select(m => new InStorageDto()
                    {
                        Id = m.Id,
                        Name = m.Name,
                    }).ToListAsync();
            }
        }
    }
}
