namespace QFramework.Example
{
    public interface IGameModel : IModel
    {
        BindableProperty<int> KillCount { get; }
        BindableProperty<int> Gold { get; }
        BindableProperty<int> Score { get; }
        BindableProperty<int> BestScore { get; }
        BindableProperty<int> HighScore { get; }
        BindableProperty<int> Life { get; }
    }
    public class GameModel : AbstractModel, IGameModel
    {
        public BindableProperty<int> KillCount { get; } = new BindableProperty<int>();
        public BindableProperty<int> Gold { get; } = new BindableProperty<int>();
        public BindableProperty<int> Score { get; } = new BindableProperty<int>();
        public BindableProperty<int> BestScore { get; } = new BindableProperty<int>();
        public BindableProperty<int> HighScore { get; } = new BindableProperty<int>();
        public BindableProperty<int> Life { get; } = new BindableProperty<int>(3);
        //这里写存储的代码
        protected override void OnInit()
        {
            var storage = this.GetUtility<IStorage>();
            BestScore.Value = storage.LoadInt(nameof(BestScore));
            BestScore.Register(bestScore => storage.SaveInt(nameof(BestScore), bestScore));
            BestScore.Register(lift => storage.SaveInt(nameof(Life), lift));
            BestScore.Register(gold => storage.SaveInt(nameof(Gold), gold));
        }
    }
}