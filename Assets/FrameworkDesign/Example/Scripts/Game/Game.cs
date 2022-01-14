using UnityEngine;

namespace FrameworkDesign.Example
{
    public class Game : MonoBehaviour, IController
    {
        private void Awake()
        {
            transform.Find("Enemies").gameObject.SetActive(false);
            this.RegisterEvent<GameStartEvent>(OnGameStart);
        }
        private void OnDestroy() => this.UnregisterEvent<GameStartEvent>(OnGameStart);
        private void OnGameStart(GameStartEvent gameStartEvent) => transform.Find("Enemies").gameObject.SetActive(true);
        public IArchitecture GetArchitecture() => PointGame.Interface;
    }
}