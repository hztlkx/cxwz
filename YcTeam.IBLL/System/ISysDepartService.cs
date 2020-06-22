using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using YcTeam.DTO.Master;
using YcTeam.Models.Sys;

namespace YcTeam.IBLL.System
{
    public interface ISysDepartService
    {
        Task CreateSysDepart(string name, string regionCity, string regionCounty,int departType);

        Task EditSysDepart(Guid sysDepartId, string name, string regionCity, string regionCounty, int departType);

        Task<List<DTO.System.SysDepartDto>> GetAllSysDepart(int pageSize, int pageIndex, bool asc);

        Task<List<DTO.System.SysDepartDto>> GetAllSysDepart();

        Task<List<SysDepart>> GetAllSysDepart(int departType);

        Task<int> GetDataCount();

        Task RemoveSysDepart(Guid id);

        Task<bool> ExistsSysDepart(Guid sysDepartId);
    }
}
