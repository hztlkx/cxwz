using System;
using System.Net;
using YcTeam.BLL.WorkFlow.Base;
using YcTeam.DTO;

namespace YcTeam.BLL.WorkFlow.Users
{
    public class ProjectUser : WorkFlowMan, IWorkFlowMan
    {
        //public UserProjectDto UserProjectDto = new UserProjectDto();

        public ProjectUser(int level,string name):base(level,name)
        {
            
        }

        public override void Request(Message message)
        {
            var messageDrive = new MessageDrive(message,this);
        }

        public override void Handle(Message message)
        {
            //UserProjectDto = new UserProjectDto { Content = this.Name + "：处理了" + message.Title + "文件"};
        }
    }
}
