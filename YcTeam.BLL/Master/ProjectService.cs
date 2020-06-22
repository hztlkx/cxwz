using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using YcTeam.DAL.Master;
using YcTeam.IDAL.Master;
using YcTeam.DTO.Master;
using YcTeam.IBLL.Master;
using YcTeam.Models;
using YcTeam.Models.Master;

namespace YcTeam.BLL.Master
{
    public class ProjectService : IProjectService
    {
        public async Task<DTO.Master.ProjectDto> GetOneProjectById(Guid projectId)
        {
            using (IDAL.Master.IProjectDao projectDao = new ProjectDao())
            {
                return await projectDao.GetAllAsync()
                    .Where(m => m.Id == projectId)
                    .Select(m => new DTO.Master.ProjectDto()
                    {
                        Id = m.Id,
                        SysDepartId = m.SysDepartId,
                        ProjectName = m.ProjectName,
                        WBSCode = m.WBSCode,
                        SmallProjectName = m.SmallProjectName,
                        SmallWBSCode = m.SmallWBSCode,
                        Funds = m.Funds,
                        VoltageGrade = m.VoltageGrade,
                        SysDepartName = m.SysDepart.DepartName,
                        PickingPeople = m.PickingPeople,
                        ContactNumber = m.ContactNumber,
                        CreateTime = m.CreateTime,
                    }).FirstAsync();
            }
        }

        public async Task CreateProject(Project model)
        {
            using (var projectDao = new ProjectDao())
            {
                await projectDao.CreateAsync(new Project()
                {
                    ProjectName = model.ProjectName,
                    WBSCode = model.WBSCode,
                    SmallProjectName = model.SmallProjectName,
                    SmallWBSCode = model.SmallWBSCode,
                    Funds = model.Funds,
                    VoltageGrade = model.VoltageGrade,
                    SysDepartId = model.SysDepartId,
                    PickingPeople = model.PickingPeople,
                    ContactNumber = model.ContactNumber,
                });
            }
        }

        public async Task EditProject(ProjectDto model)
        {
            using (var projectDao = new ProjectDao())
            {
                await projectDao.EditAsync(new Project()
                {
                    Id = model.Id,
                    ProjectName = model.ProjectName,
                    WBSCode = model.WBSCode,
                    SmallProjectName = model.SmallProjectName,
                    SmallWBSCode = model.SmallWBSCode,
                    Funds = model.Funds,
                    VoltageGrade = model.VoltageGrade,
                    SysDepartId = model.SysDepartId,
                    PickingPeople = model.PickingPeople,
                    ContactNumber = model.ContactNumber,
                });
            }
        }

        public async Task<bool> ExistsProject(Guid projectId)
        {
            using (IDAL.Master.IProjectDao projectDao = new ProjectDao())
            {
                return await projectDao.GetAllAsync().AnyAsync(m => m.Id == projectId);
            }
        }

        public async Task<List<ProjectDto>> GetAllProject(int pageIndex, int pageSize, bool asc = true)
        {
            using (var projectDao = new ProjectDao())
            {
                return await projectDao.GetAllByPageOrderAsync(pageIndex - 1, pageSize, asc).Select(m => new DTO.Master.ProjectDto()
                {
                    Id=m.Id,
                    ProjectName = m.ProjectName,
                    WBSCode = m.WBSCode,
                    SmallProjectName = m.SmallProjectName,
                    SmallWBSCode = m.SmallWBSCode,
                    Funds = m.Funds,
                    VoltageGrade = m.VoltageGrade,
                    SysDepartId = m.SysDepartId,
                    SysDepartName = m.SysDepart.DepartName,
                    PickingPeople = m.PickingPeople,
                    ContactNumber = m.ContactNumber,
                    CreateTime = m.CreateTime,
                }).ToListAsync();
            }
        }

        public async Task<int> GetDataCount()
        {
            using (var projectDao = new ProjectDao())
            {
                return await projectDao.GetAllAsync().CountAsync();
            }
        }

        public async Task RemoveProject(Guid id)
        {
            using (var projectDao = new ProjectDao())
            {
                await projectDao.RemoveAsync(id);
            }
        }
    }
}
