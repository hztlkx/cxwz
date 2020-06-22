namespace YcTeam.BLL.WorkFlow.Base
{
    public interface IWorkFlowMan
    {
        int Level { get; set; }

        string Name { get; set; }

        void Request(Message message);

        void Handle(Message message);
    }
}
