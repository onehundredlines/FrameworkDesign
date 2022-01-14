using UnityEngine;

namespace FrameworkDesign.Example
{
    public class UI : MonoBehaviour, IController
    {
        private void Awake()
        {
            this.RegisterEvent<GamePassEvent>(OnGamePass);
            transform.Find("Canvas/GameStartPanel").gameObject.SetActive(true);
            transform.Find("Canvas/GamePassPanel").gameObject.SetActive(false);
        }
        private void OnDestroy() => this.UnregisterEvent<GamePassEvent>(OnGamePass);
        private void OnGamePass(GamePassEvent e) => transform.Find("Canvas/GamePassPanel").gameObject.SetActive(true);
        public IArchitecture GetArchitecture() => PointGame.Interface;
    }
}