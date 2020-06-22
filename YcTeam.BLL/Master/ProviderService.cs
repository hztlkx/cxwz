using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using YcTeam.DAL.Master;
using YcTeam.IDAL.Master;
using YcTeam.IBLL.Master;
using YcTeam.Models;
using YcTeam.Models.Master;

namespace YcTeam.BLL.Master
{
    public class ProviderService : IProviderService
    {
        public async Task<DTO.Master.ProviderDto> GetOneProviderById(Guid providerId)
        {
            using (IDAL.Master.IProviderDao providerDao = new ProviderDao())
            {
                return await providerDao.GetAllAsync()
                    .Where(m => m.Id == providerId)
                    .Select(m => new DTO.Master.ProviderDto()
                    {
                        Id = m.Id,
                        Name = m.Name,
                        CreateTime = m.CreateTime,
                    }).FirstAsync();
            }
        }

        public async Task CreateProvider(string name)
        {
            using (var providerDao = new ProviderDao())
            {
                await providerDao.CreateAsync(new Provider()
                {
                    Name = name
                });
            }
        }

        public async Task EditProvider(Guid providerId, string name)
        {
            using (var providerDao = new ProviderDao())
            {
                var provider = await providerDao.GetOneByIdAsync(providerId);
                provider.Name = name;
                await providerDao.EditAsync(provider);
            }
        }

        /// <summary>
        /// 判断是否存在供应商
        /// </summary>
        /// <param name="providerId">供应商主键</param>
        /// <returns></returns>
        public async Task<bool> ExistsProvider(Guid providerId)
        {
            using (IDAL.Master.IProviderDao providerDao = new ProviderDao())
            {
                return await providerDao.GetAllAsync().AnyAsync(m => m.Id == providerId);
            }
        }

        public async Task RemoveProvider(Guid id)
        {
            using (var providerDao = new ProviderDao())
            {
                await providerDao.RemoveAsync(id);
            }
        }

        /// <summary>
        /// 供应商查询
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="asc"></param>
        /// <returns></returns>
        public async Task<List<DTO.Master.ProviderDto>> GetAllProvider(int pageIndex = 1,int pageSize = 10, bool asc = true)
        {
            using (var providerDao = new ProviderDao())
            {
                return await providerDao.GetAllByPageOrderAsync(pageIndex - 1, pageSize,asc).Select(m => new DTO.Master.ProviderDto()
                {
                    Id = m.Id,
                    Name = m.Name,
                    CreateTime = m.CreateTime
                }).ToListAsync();
            }
        }

        public async Task<int> GetDataCount()
        {
            using (var providerDao = new ProviderDao())
            {
                return await providerDao.GetAllAsync().CountAsync();
            }
        }

        public async Task<List<Provider>> SearchProvider(string text)
        {
            using (var providerDao = new ProviderDao())
            {
                return await providerDao.GetAllAsync().Where(m => m.Name.Contains(text)).ToListAsync();
            }
        }
    }
}
