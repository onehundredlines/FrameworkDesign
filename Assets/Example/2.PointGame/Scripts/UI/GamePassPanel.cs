using UnityEngine;
using UnityEngine.UI;
namespace QFramework.Example
{
    public class GamePassPanel : MonoBehaviour, IController
    {
        private void OnEnable()
        {
            var mGameModel = this.GetModel<IGameModel>();
            var mCountDownSystem = this.GetSystem<ICountDownSystem>();
            transform.Find("TextScore").GetComponent<Text>().text = $"分  数：{mGameModel.Score.Value}";
            transform.Find("TextBestScore").GetComponent<Text>().text = $"最高分数：{mGameModel.BestScore.Value}";
            transform.Find("TextRemainSeconds").GetComponent<Text>().text = $"剩余时间：{mCountDownSystem.CurrentRemainSeconds}s";
        }
        public IArchitecture GetArchitecture() => PointGame.Interface;
    }
}