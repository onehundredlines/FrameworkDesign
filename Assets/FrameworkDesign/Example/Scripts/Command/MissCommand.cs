namespace FrameworkDesign.Example
{
    public class MissCommand : AbstractCommand
    {

        protected override void OnExecute()
        {
            this.SendEvent<OnMissEvent>();
        }
    }
}