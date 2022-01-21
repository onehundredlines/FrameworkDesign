using UnityEngine;
namespace QFramework.Example
{
    public class UI : MonoBehaviour, IController
    {
        private void Awake()
        {
            transform.Find("Canvas/GameStartPanel").gameObject.SetActive(true);
            transform.Find("Canvas/GamePassPanel").gameObject.SetActive(false);
            transform.Find("Canvas/GamePanel").gameObject.SetActive(false);
            this.RegisterEvent<OnGameStartEvent>(OnGameStart);
            this.RegisterEvent<OnGamePassEvent>(OnGamePass);
            this.RegisterEvent<OnCountDownEndEvent>(e =>
            {
                transform.Find("Canvas/GamePanel").gameObject.SetActive(false);
                transform.Find("Canvas/GameOverPanel").gameObject.SetActive(true);
            }).CancelWhenGameObjectDestroy(gameObject);
        }
        private void OnGameStart(OnGameStartEvent mobj) { transform.Find("Canvas/GamePanel").gameObject.SetActive(true); }
        private void OnDestroy() => this.CancelEvent<OnGamePassEvent>(OnGamePass);
        private void OnGamePass(OnGamePassEvent e)
        {
            transform.Find("Canvas/GamePanel").gameObject.SetActive(false);
            transform.Find("Canvas/GamePassPanel").gameObject.SetActive(true);
        }
        public IArchitecture GetArchitecture() => PointGame.Interface;
    }
}