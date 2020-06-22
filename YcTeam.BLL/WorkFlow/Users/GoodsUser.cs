using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YcTeam.BLL.WorkFlow.Base;
using YcTeam.DTO;

namespace YcTeam.BLL.WorkFlow.Users
{
    public class GoodsUser : WorkFlowMan, IWorkFlowMan
    {
        //public UserGoodsDto UserGoodsDto = new UserGoodsDto();

        public GoodsUser(int level,string name) : base(level,name)
        {
            
        }

        public override void Request(Message message)
        {
            var messageDrive = new MessageDrive(message, this);
        }

        public override void Handle(Message message)
        {
            //UserGoodsDto = new UserGoodsDto { Content = this.Name + "：处理了" + message.Title + "文件" };
        }
    }
}
