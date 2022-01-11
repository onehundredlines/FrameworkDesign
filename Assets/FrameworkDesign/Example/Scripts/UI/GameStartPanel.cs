using System;
using UnityEngine;
using UnityEngine.UI;

namespace FrameworkDesign.Example
{
    public class GameStartPanel : MonoBehaviour
    {
        private void Start()
        {
            transform.Find("StartButton").GetComponent<Button>().onClick.AddListener(() =>
            {
                GameStartEvent.Trigger();
                gameObject.SetActive(false);
            });
        }
        private void OnDestroy() { transform.Find("StartButton").GetComponent<Button>().onClick.RemoveAllListeners(); }
    }
}