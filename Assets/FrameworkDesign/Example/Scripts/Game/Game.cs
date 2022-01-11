using UnityEngine;

namespace FrameworkDesign.Example
{
    public class Game : MonoBehaviour
    {
        private void Awake()
        {
            transform.Find("Enemies").gameObject.SetActive(false);
            GameStartEvent.RegisterEvent(OnGameStart);
        }
        private void OnDestroy()
        {
            GameStartEvent.UnRegisterEvent(OnGameStart); 
        }
        private void OnGameStart() { transform.Find("Enemies").gameObject.SetActive(true); }
    }
}