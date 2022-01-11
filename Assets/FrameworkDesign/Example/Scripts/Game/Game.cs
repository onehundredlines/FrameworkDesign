using UnityEngine;

namespace FrameworkDesign.Example
{
    public class Game : MonoBehaviour
    {
        private void Awake()
        {
            transform.Find("Enemies").gameObject.SetActive(false);
            GameStartEvent.RegisterEvent(OnGameStart);
            GameModel.KillCount.OnValueChanged += OnEnemyKilled;
        }
        private void OnDestroy()
        {
            GameStartEvent.UnRegisterEvent(OnGameStart); 
            GameModel.KillCount.OnValueChanged -= OnEnemyKilled;
        }
        private void OnGameStart() { transform.Find("Enemies").gameObject.SetActive(true); }
        private static void OnEnemyKilled(int killCount)
        {
            if (killCount < 10) return;
            GamePassEvent.Trigger();
        }
    }
}