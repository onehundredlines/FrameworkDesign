namespace FrameworkDesign
{
    public interface ISystem : IBelongToArchitecture
    {
        /// <summary>
        /// System本身需要有状态，需要有初始化
        /// </summary>
        void Init();
    }
}
