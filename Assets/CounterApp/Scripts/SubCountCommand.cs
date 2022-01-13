using FrameworkDesign;
namespace CounterApp
{
    public class SubCountCommand : AbstractCommand
    {
        /// <summary>
        /// 在OnExecute中写真正的逻辑
        /// </summary>
        protected override void OnExecute()
        {
            GetArchitecture().GetModel<ICounterModel>().Count.Value--;
        }
    }
}