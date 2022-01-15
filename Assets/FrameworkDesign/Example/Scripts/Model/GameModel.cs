using System;
namespace FrameworkDesign.Example
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
        public BindableProperty<int> KillCount { get; } = new BindableProperty<int> {Value = 0};
        public BindableProperty<int> Gold { get; } = new BindableProperty<int> {Value = 0};
        public BindableProperty<int> Score { get; } = new BindableProperty<int> {Value = 0};
        public BindableProperty<int> BestScore { get; } = new BindableProperty<int> {Value = 0};
        public BindableProperty<int> HighScore { get; } = new BindableProperty<int> {Value = 0};
        public BindableProperty<int> Life { get; } = new BindableProperty<int> {Value = 3};

        //这里写存储的代码
        protected override void OnInit()
        {
            var storage = this.GetUtility<IStorage>();
            BestScore.Value = storage.LoadInt(nameof(BestScore), 0);
            BestScore.RegisterOnValueChanged(bestScore => storage.SaveInt(nameof(BestScore), bestScore));
            BestScore.RegisterOnValueChanged(lift => storage.SaveInt(nameof(Life), lift));
            BestScore.RegisterOnValueChanged(gold => storage.SaveInt(nameof(Gold), gold));
        }
    }
}