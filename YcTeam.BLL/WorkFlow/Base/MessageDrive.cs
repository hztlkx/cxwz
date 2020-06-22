using System.Collections.Generic;
using YcTeam.BLL.WorkFlow.Users;

namespace YcTeam.BLL.WorkFlow.Base
{
    public class MessageDrive
    {
        public List<IWorkFlowMan> LevelIWorkFlowMan { get; set; }

        public Message Message { get; set; }

        public IWorkFlowMan WorkFlowMan { get; set; }

        public MessageDrive(Message message, IWorkFlowMan requestWorkFlowMan)
        {
            this.Message = message;
            this.WorkFlowMan = requestWorkFlowMan;
            Handle(requestWorkFlowMan);
        }

        private int i = 0;
        public void Handle(IWorkFlowMan requestWorkFlowMan)
        {
            i++;
            List<IWorkFlowMan> levelIwWorkFlowMan = new List<IWorkFlowMan>();

            int index = levelIwWorkFlowMan.FindIndex(d => d.Level == requestWorkFlowMan.Level);
            requestWorkFlowMan.Handle(Message);
            if (index + 1 < levelIwWorkFlowMan.Count)
                Handle(levelIwWorkFlowMan[index + 1]);
        }
    }
}
