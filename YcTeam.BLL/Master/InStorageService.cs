using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using YcTeam.DAL.Master;
using YcTeam.IDAL.Master;
using YcTeam.DTO.Master;
using YcTeam.IBLL.Master;
using YcTeam.Models.Master;

namespace YcTeam.BLL.Master
{
    public class InStorageService : IInStorageService
    {
        public async Task<DTO.Master.InStorageDto> GetOneInStorageById(Guid inStorageId)
        {
            using (IDAL.Master.IInStorageDao inStorageDao = new InStorageDao())
            {
                return await inStorageDao.GetAllAsync()
                    .Where(m => m.Id == inStorageId)
                    .Select(m => new DTO.Master.InStorageDto()
                    {
                        Code=m.Code,
                        Name = m.Name,
                        Category=m.Category,
                        Describe=m.Describe,
                        CreateTime = m.CreateTime,
                    }).FirstAsync();
            }
        }
        public async Task CreateInStorage(string code, string name, string category, string describe)
        {
            using (var inStorageDao = new InStorageDao())
            {
                await inStorageDao.CreateAsync(new InStorage()
                {
                    Code = code,
                    Name = name,
                    Category = category,
                    Describe = describe
                });
            }
        }

        public async Task EditInStorage(Guid inStorageId, string code, string name, string category, string describe)
        {
            using (var inStorageDao = new InStorageDao())
            {
                var inStorage = await inStorageDao.GetOneByIdAsync(inStorageId);
                inStorage.Code = code;
                inStorage.Name = name;
                inStorage.Category = category;
                inStorage.Describe = describe;
                await inStorageDao.EditAsync(inStorage);

            }
        }

        public async Task<bool> ExistsInStorage(Guid inStorageId)
        {
            using (IDAL.Master.IInStorageDao inStorageDao = new InStorageDao()) 
            {
                return await inStorageDao.GetAllAsync().AnyAsync(m => m.Id == inStorageId);
            }
        }

        public async Task<List<InStorageDto>> GetAllInStorage(int pageIndex, int pageSize, bool asc=true)
        {
            using (var inStorageDao = new InStorageDao())
            {
                return await inStorageDao.GetAllByPageOrderAsync(pageIndex - 1, pageSize, asc).Select(m => new DTO.Master.InStorageDto()
                {
                    Id = m.Id,
                    Code = m.Code,
                    Name = m.Name,
                    Category = m.Category,
                    Describe = m.Describe,                
                    CreateTime = m.CreateTime,
                }).ToListAsync();
            }
        }

        public async Task<int> GetDataCount()
        {
            using (var inStorageDao = new InStorageDao())
            {
                return await inStorageDao.GetAllAsync().CountAsync();
            }
        }

        public async Task RemoveInStorage(Guid id)
        {
            using (var inStorageDao = new InStorageDao())
            {
                await inStorageDao.RemoveAsync(id);
            }
        }
    }
}
