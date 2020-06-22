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
using YcTeam.IDAL.Master;
using YcTeam.Models.FlowInto;

namespace YcTeam.BLL.FlowInto
{
    public class InStorageReceiveService : IInStorageReceiveService
    {
        IProviderDao _providerDao = new ProviderDao();

        public async Task<InStorageReceiveDto> GetOneStorageById(Guid inStorageId)
        {
            using (InStorageReceiveDao inStorageReceiveDao = new InStorageReceiveDao())
            {
                return await inStorageReceiveDao.GetAllAsync()
                    .Where(m => m.Id == inStorageId)
                    .Select(m => new InStorageReceiveDto()
                    {
                        Id = m.Id,
                        Batch = m.Batch,
                        ProviderId = m.Provider.Id,
                        ProviderName=m.Provider.Name,
                        MaterialId = m.Material.Id,
                        Code = m.Material.Code,
                        Describe = m.Material.Describe,
                        Unit = m.Material.Unit,
                        PutNumber = m.PutNumber,
                        CreateTime = m.CreateTime,
                        File = m.File,
                        SysUserId=m.SysUser.Id,
                        SysUserName=m.SysUser.RealName,
                        Note = m.Note
                    }).FirstAsync();
            }

        }

        public async Task<int> GetDataCount()
        {
            using (var inStorageReceiveDao = new InStorageReceiveDao())
            {
                return await inStorageReceiveDao.GetAllAsync().CountAsync();
            }
        }

        public async Task RemoveInStorage(Guid id)
        {
            using (var inStorageReceiveDao = new InStorageReceiveDao())
            {
                await inStorageReceiveDao.RemoveAsync(id);
            }
        }
        public async Task CreateInStorageReceive(string Batch, Guid ProviderId, Guid MaterialId, int PutNumber, string File,Guid SysUserId, string Note)
        {
            using (var inStorageReceiveDao = new InStorageReceiveDao())
            {
                await inStorageReceiveDao.CreateAsync(new InStorageReceive()
                {                  
                    Batch = Batch,
                    ProviderId = ProviderId,
                    MaterialId = MaterialId,
                    PutNumber = PutNumber,
                    File = File,
                    UserId= SysUserId,
                    Note=Note
                });
            }
        }

        public List<InStorageReceiveDto> getAllInStorageReceive(int pageSize, int pageIndex, bool asc)
        {
            using (var inStorageReceiveDao = new InStorageReceiveDao())
            {
                return inStorageReceiveDao.GetAllByPageOrderAsync(pageIndex - 1, pageSize, asc).Select(m => new InStorageReceiveDto()
                {

                    Id = m.Id,
                    Batch = m.Batch,
                    ProviderId = m.Provider.Id,
                    ProviderName = m.Provider.Name,
                    MaterialId = m.Material.Id,
                    Code = m.Material.Code,
                    Describe = m.Material.Describe,
                    Unit = m.Material.Unit,
                    PutNumber = m.PutNumber,
                    CreateTime = m.CreateTime,
                    File = m.File,
                    SysUserId = m.SysUser.Id,
                    SysUserName = m.SysUser.RealName,
                    Note = m.Note
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
     
    }
}
