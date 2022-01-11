using UnityEngine;
using UnityEngine.UI;

namespace FrameworkDesign.Example
{
    public class GameStartPanel : MonoBehaviour
    {
        [SerializeField]
        private Button startButton;
        private void Start()
        {
            startButton.onClick.AddListener(() =>
            {
                GameStartEvent.Trigger();
                gameObject.SetActive(false);
            });
        }
    }
}