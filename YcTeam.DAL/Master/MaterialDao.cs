using YcTeam.DAL;
using YcTeam.IDAL.Master;
using YcTeam.Models;
using YcTeam.Models.Master;

namespace YcTeam.DAL.Master
{
    public class MaterialDao :BaseService<Material>,IMaterialDao
    {
        public MaterialDao() : base(new YcContext())
        {

        }
    }
}
