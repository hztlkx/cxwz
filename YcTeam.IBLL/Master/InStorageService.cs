using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YcTeam.IBLL.Master
{
    public interface IInStorageService
    {
        Task CreateInStorage(string code,string name,string category, string describe);

        Task EditInStorage(Guid inStorageId, string code, string name, string category, string describe);

        Task RemoveInStorage(Guid id);

        Task<List<DTO.Master.InStorageDto>> GetAllInStorage(int pageSize, int pageIndex, bool asc);

        Task<int> GetDataCount();
        Task<bool> ExistsInStorage(Guid inStorageId);
    }
}
