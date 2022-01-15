using UnityEngine;
namespace FrameworkDesign.Example
{
    public interface IScoreSystem : ISystem
    {
    }
    public class ScoreSystem : AbstractSystem, IScoreSystem
    {
        protected override void OnInit()
        {
            var gameMode = this.GetModel<IGameModel>();
            this.RegisterEvent<GamePassEvent>(e =>
            {
                Debug.Log($"Score {gameMode.Score.Value}");
                Debug.Log($"BestScore {gameMode.BestScore.Value}");
                if (gameMode.Score.Value > gameMode.BestScore.Value)
                {
                    Debug.Log($"新纪录");
                    Debug.Log($"原来最佳分数 {gameMode.BestScore.Value}");
                    gameMode.BestScore.Value = gameMode.Score.Value;
                    Debug.Log($"最佳分数 {gameMode.BestScore.Value}");
                }
            });
            this.RegisterEvent<OnEnemyKilledEvent>(e =>
            {
                gameMode.Score.Value += 10;
                Debug.Log("+10分");
                Debug.Log($"当前分数->{gameMode.Score.Value}");
            });
            this.RegisterEvent<OnMissEvent>(e =>
            {
                gameMode.Score.Value -= 5;
                Debug.Log("-5分");
                Debug.Log($"当前分数->{gameMode.Score.Value}");
            });
        }
    }
}