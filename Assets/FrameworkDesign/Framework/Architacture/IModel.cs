namespace FrameworkDesign
{
    public interface IModel : ICanSetArchitecture, ICanGetUtility
    {
        void Init();
    }
    public abstract class AbstractModel : IModel
    {
        private IArchitecture mArchitecture;
        IArchitecture IBelongToArchitecture.GetArchitecture() => mArchitecture;
        void ICanSetArchitecture.SetArchitecture(IArchitecture architecture) => mArchitecture = architecture;
        /// <summary>
        /// 这里使用接口的显式实现，为了隔离功能，限制子类的调用
        /// </summary>
        void IModel.Init() { OnInit(); }
        protected abstract void OnInit();
    }
}