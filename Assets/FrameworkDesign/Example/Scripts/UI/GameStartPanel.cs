using UnityEngine;
using UnityEngine.UI;

namespace FrameworkDesign.Example
{
    public class GameStartPanel : MonoBehaviour, IController
    {
        private void Start()
        {
            transform.Find("StartButton").GetComponent<Button>().onClick.AddListener(() =>
            {
                this.SendCommand<StartGameCommand>();
                gameObject.SetActive(false);
            });
        }
        private void OnDestroy() => transform.Find("StartButton").GetComponent<Button>().onClick.RemoveAllListeners();
        public IArchitecture GetArchitecture() => PointGame.Interface;
    }
}