using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YcTeam.DTO.Master;
using YcTeam.Models.Master;

namespace YcTeam.IBLL.Master
{
    public interface IProjectService
    {
        Task CreateProject(Project model);

        Task EditProject(ProjectDto model);

        Task RemoveProject(Guid id);

        Task<List<DTO.Master.ProjectDto>> GetAllProject(int pageSize, int pageIndex, bool asc);

        Task<int> GetDataCount();
        Task<bool> ExistsProject(Guid projectId);
    }
}
