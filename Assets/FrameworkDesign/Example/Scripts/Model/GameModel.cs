namespace FrameworkDesign.Example
{
    public interface IGameModel
    {
        BindableProperty<int> KillCount { get; }
        BindableProperty<int> Gold { get; }
        BindableProperty<int> Score { get; }
        BindableProperty<int> HighScore { get; }
    }
    public class GameModel : IGameModel
    {
        public BindableProperty<int> KillCount { get; } = new BindableProperty<int> {Value = 0};
        public BindableProperty<int> Gold { get; } = new BindableProperty<int> {Value = 0};
        public BindableProperty<int> Score { get; } = new BindableProperty<int> {Value = 0};
        public BindableProperty<int> HighScore { get; } = new BindableProperty<int> {Value = 0};
    }
}