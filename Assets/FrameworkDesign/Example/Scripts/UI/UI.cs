using UnityEngine;

namespace FrameworkDesign.Example
{
    public class UI : MonoBehaviour
    {
        private void Awake() { GamePassEvent.RegisterEvent(OnGamePass); }
        private void OnDestroy() { GamePassEvent.UnRegisterEvent(OnGamePass); }
        private void OnGamePass()
        {
            transform.Find("Canvas/GamePassPanel").gameObject.SetActive(true);
        }
    }
}