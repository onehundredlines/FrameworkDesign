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
                gameMode.Score.Value = Random.Range(1, 50);
                Debug.Log($"Score {gameMode.Score.Value}");
                Debug.Log($"BestScore {gameMode.BestScore.Value}");
                if (gameMode.Score.Value > gameMode.BestScore.Value)
                {
                    Debug.Log($"新纪录");
                    gameMode.BestScore.Value = gameMode.Score.Value;
                }
            });
        }
    }
}