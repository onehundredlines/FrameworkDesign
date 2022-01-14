namespace FrameworkDesign
{
    public interface IController : ICanGetModel, ICanGetSystem, ICanSendCommand, ICanRegisterEvent
    {
    }
}