namespace YcTeam.BLL.WorkFlow.Base
{
    public class WorkFlowMan
    {
        public WorkFlowMan(int level,string name)
        {
            Level = level;
            Name = name;
        }

        public int Level { get; set; }

        public string Name { get; set; }

        public virtual void Request(Message message){}

        public virtual void Handle(Message message) {}
    }
}
