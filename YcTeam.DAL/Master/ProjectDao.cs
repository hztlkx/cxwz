using YcTeam.DAL;
using YcTeam.IDAL.Master;
using YcTeam.Models;
using YcTeam.Models.Master;

namespace YcTeam.DAL.Master
{
    public class ProjectDao : BaseService<Project>,IProjectDao
    {
        public ProjectDao() : base(new YcContext())
        {

        }
    }
}
