namespace FrameworkDesign.Example
{
    public interface IGameModel : IModel
    {
        BindableProperty<int> KillCount { get; }
        BindableProperty<int> Gold { get; }
        BindableProperty<int> Score { get; }
        BindableProperty<int> HighScore { get; }
    }
    public class GameModel : AbstractModel, IGameModel
    {
        public BindableProperty<int> KillCount { get; } = new BindableProperty<int> {Value = 0};
        public BindableProperty<int> Gold { get; } = new BindableProperty<int> {Value = 0};
        public BindableProperty<int> Score { get; } = new BindableProperty<int> {Value = 0};
        public BindableProperty<int> HighScore { get; } = new BindableProperty<int> {Value = 0};
        protected override void OnInit()
        {
            //这里写存储的代码
        }
    }
}