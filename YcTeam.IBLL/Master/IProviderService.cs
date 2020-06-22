using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using YcTeam.DTO.Master;

namespace YcTeam.IBLL.Master
{
    public interface IProviderService
    {
        Task CreateProvider(string name);

        Task EditProvider(Guid providerId, string name);

        Task<List<DTO.Master.ProviderDto>> GetAllProvider(int pageSize, int pageIndex, bool asc);

        Task<int> GetDataCount();

        Task RemoveProvider(Guid id);

        Task<bool> ExistsProvider(Guid providerId);
    }
}
