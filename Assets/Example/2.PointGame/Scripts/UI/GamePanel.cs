using UnityEngine;
using UnityEngine.UI;
namespace QFramework.Example
{
    public class GamePanel : MonoBehaviour, IController
    {
        private ICountDownSystem mCountDownSystem;
        private IGameModel mGameModel;
        private void Awake()
        {
            mCountDownSystem = this.GetSystem<ICountDownSystem>();
            mGameModel = this.GetModel<IGameModel>();
            mGameModel.Gold.Register(OnGoldValueChanged);
            mGameModel.Life.Register(OnLifeValueChanged);
            mGameModel.Score.Register(OnScoreValueChanged);
        }
        private void OnEnable()
        {
            //开始需要调用一次
            OnLifeValueChanged(mGameModel.Life.Value);
            OnGoldValueChanged(mGameModel.Gold.Value);
            OnScoreValueChanged(mGameModel.Score.Value);
        }
        private void Update()
        {
            //每20帧更新一次
            if (Time.frameCount % 20 != 0) return;
            transform.Find("TextCountDown").GetComponent<Text>().text = $"{mCountDownSystem.CurrentRemainSeconds}s";
            mCountDownSystem.Update();
        }
        private void OnDestroy()
        {
            mGameModel.Gold.Register(OnGoldValueChanged);
            mGameModel.Life.Register(OnLifeValueChanged);
            mGameModel.Score.Register(OnScoreValueChanged);
            mGameModel = null;
            mCountDownSystem = null;
        }
        private void OnLifeValueChanged(int life) => transform.Find("TextLife").GetComponent<Text>().text = $"生命：{life}";
        private void OnGoldValueChanged(int gold) => transform.Find("TextGold").GetComponent<Text>().text = $"金币：{gold}";
        private void OnScoreValueChanged(int score) => transform.Find("TextScore").GetComponent<Text>().text = $"分数：{score}";
        IArchitecture IBelongToArchitecture.GetArchitecture() => PointGame.Interface;
    }
}