using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YcTeam.Models.Master;

namespace YcTeam.IBLL.Master
{
    public interface IMaterialService
    {
        Task CreateMaterial(string code, string largyCategory, string smallCategory, string describe, string unit, string note);

        Task EditMaterial(Guid materialId, string code, string largyCategory, string smallCategory, string describe, string unit, string note);

        Task<List<Material>> SearchMaterial(string text);

        Task RemoveMaterial(Guid id);

        Task<List<DTO.Master.MaterialDto>> GetAllMaterial(int pageSize, int pageIndex, bool asc);

        Task<int> GetDataCount();
        Task<bool> ExistsMaterial(Guid materialId);
    }
}
