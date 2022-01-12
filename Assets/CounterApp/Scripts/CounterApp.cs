using FrameworkDesign;
namespace CounterApp
{
    public class CounterApp : Architecture<CounterApp>
    {
        protected override void Init()
        {
            Register<ICounterModel>(new CounterModel());            
        }
    }
}