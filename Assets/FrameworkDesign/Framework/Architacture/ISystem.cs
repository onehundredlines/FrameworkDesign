namespace FrameworkDesign
{
    public interface ISystem : IBelongToArchitecture, ICanSetArchitecture
    {
        /// <summary>
        /// System本身需要有状态，需要有初始化
        /// </summary>
        void Init();
    }
    public abstract class AbstractSystem : ISystem
    {
        private IArchitecture mArchitecture;
        public IArchitecture GetArchitecture()
        {
            return mArchitecture;
        }
        public void SetArchitecture(IArchitecture architecture)
        {
            mArchitecture = architecture;
        }
        /// <summary>
        /// 这里使用接口的显式实现，为了隔离功能，限制子类的调用
        /// </summary>
        void ISystem.Init()
        {
            OnInit();
        }
        protected abstract void OnInit();
    }
}
