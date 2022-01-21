using UnityEngine;
namespace QFramework.Example
{
    public class Game : MonoBehaviour, IController
    {
        private void Awake()
        {
            transform.Find("Enemies").gameObject.SetActive(false);
            this.RegisterEvent<OnGameStartEvent>(OnGameStart);
            this.RegisterEvent<OnCountDownEndEvent>(mevent => transform.Find("Enemies").gameObject.SetActive(false));
            this.RegisterEvent<OnGamePassEvent>(mevent => transform.Find("Enemies").gameObject.SetActive(false));
        }
        private void OnDestroy() => this.CancelEvent<OnGameStartEvent>(OnGameStart);
        private void OnGameStart(OnGameStartEvent gameStartEvent)
        {

            var enemies = transform.Find("Enemies");
            enemies.gameObject.SetActive(true);
            foreach(Transform enemy in enemies)
            {
                enemy.gameObject.SetActive(true);
            }
        }
        public IArchitecture GetArchitecture() => PointGame.Interface;
    }
}