namespace FrameworkDesign
{
    public interface ICommand : ICanSetArchitecture, ICanGetModel, ICanGetSystem, ICanGetUtility
    {
        void Execute();
    }
    public abstract class AbstractCommand : ICommand
    {
        private IArchitecture mArchitecture;
        IArchitecture IBelongToArchitecture.GetArchitecture() => mArchitecture;
        void ICanSetArchitecture.SetArchitecture(IArchitecture architecture) => mArchitecture = architecture;
        /// <summary>
        /// 这里使用接口的显式实现，为了隔离功能，限制子类的调用
        /// </summary>
        void ICommand.Execute() => OnExecute();
        protected abstract void OnExecute();
    }
}