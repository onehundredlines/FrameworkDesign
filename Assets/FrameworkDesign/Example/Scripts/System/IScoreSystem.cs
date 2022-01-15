using System;
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

            this.RegisterEvent<OnGamePassEvent>(e =>
            {
                var countDownSystem = this.GetSystem<ICountDownSystem>();
                var timeScore = countDownSystem.CurrentRemainSeconds * 10;
                gameMode.Score.Value += timeScore;
                Debug.Log($"历史最佳分数 {gameMode.BestScore.Value}");
                Debug.Log($"Score {gameMode.Score.Value}");
                if (gameMode.Score.Value > gameMode.BestScore.Value)
                {
                    Debug.Log($"历史最佳分数 {gameMode.BestScore.Value}");
                    gameMode.BestScore.Value = gameMode.Score.Value;
                    Debug.Log($"新纪录{Environment.NewLine}最佳分数 {gameMode.BestScore.Value}");
                }
            });
            this.RegisterEvent<OnKillEnemyEvent>(e =>
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