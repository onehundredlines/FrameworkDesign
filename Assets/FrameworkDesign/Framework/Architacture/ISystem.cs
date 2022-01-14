namespace FrameworkDesign
{
    public interface ISystem : ICanSetArchitecture, ICanGetModel, ICanGetUtility, ICanRegisterEvent, ICanSendEvent
    {
        /// <summary>
        /// System本身需要有状态，需要有初始化
        /// </summary>
        void Init();
    }
    public abstract class AbstractSystem : ISystem
    {
        private IArchitecture mArchitecture;
        IArchitecture IBelongToArchitecture.GetArchitecture() => mArchitecture;
        void ICanSetArchitecture.SetArchitecture(IArchitecture architecture) => mArchitecture = architecture;
        /// <summary>
        /// 这里使用接口的显式实现，为了隔离功能，限制子类的调用
        /// </summary>
        void ISystem.Init() => OnInit();
        protected abstract void OnInit();
    }
}