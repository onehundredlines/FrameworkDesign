using UnityEngine;
using UnityEngine.UI;
namespace QFramework.Example
{
    public class GameStartPanel : MonoBehaviour, IController
    {
        private IGameModel mGameModel;
        private void Awake()
        {
            mGameModel = this.GetModel<IGameModel>();
            mGameModel.BestScore.Register(OnBestScoreValueChanged);
            mGameModel.Life.Register(OnLifeValueChanged);
            mGameModel.Gold.Register(OnGoldValueChanged);
        }
        private void OnEnable()
        {
            OnGoldValueChanged(mGameModel.Gold.Value);
            OnLifeValueChanged(mGameModel.Life.Value);
            OnBestScoreValueChanged(mGameModel.BestScore.Value);
        }
        private void Start()
        {
            transform.Find("StartButton").GetComponent<Button>().onClick.AddListener(() =>
            {
                this.SendCommand<StartGameCommand>();
                gameObject.SetActive(false);
            });
            transform.Find("ButtonBuyLife").GetComponent<Button>().onClick.AddListener(() =>
            {
                this.SendCommand<BuyLifeCommand>();
                OnGoldValueChanged(mGameModel.Gold.Value);
            });
        }
        private void OnBestScoreValueChanged(int bestScore) => transform.Find("TextBestScore").GetComponent<Text>().text = $"最高分：{bestScore}";
        private void OnLifeValueChanged(int life) => transform.Find("TextLife").GetComponent<Text>().text = $"生命值：{life}";
        private void OnGoldValueChanged(int gold)
        {
            transform.Find("ButtonBuyLife").gameObject.SetActive(gold > 0);
            transform.Find("TextGold").GetComponent<Text>().text = $"金币：{gold}";
        }
        private void OnDestroy()
        {
            transform.Find("StartButton").GetComponent<Button>().onClick.RemoveAllListeners();
            transform.Find("ButtonBuyLife").GetComponent<Button>().onClick.RemoveAllListeners();
        }
        IArchitecture IBelongToArchitecture.GetArchitecture() => PointGame.Interface;
    }
}