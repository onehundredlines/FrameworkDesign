using FrameworkDesign;
namespace CounterApp
{
    public struct SubCountCommand : ICommand
    {
        /// <summary>
        /// 在Execute中写真正的逻辑
        /// </summary>
        public void Execute() { CounterModel.Instance.Count.Value--; }
    }
}