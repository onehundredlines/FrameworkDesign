using UnityEngine;

namespace FrameworkDesign.Example
{
    public class Game : MonoBehaviour
    {
        private void Awake()
        {
            transform.Find("Enemies").gameObject.SetActive(false);
            GameStartEvent.RegisterEvent(OnGameStart);
            KilledOneEnemyEvent.RegisterEvent(OnEnemyKilled);    
        }
        private void OnDestroy()
        {
            GameStartEvent.UnRegisterEvent(OnGameStart); 
            KilledOneEnemyEvent.UnRegisterEvent(OnEnemyKilled);
        }
        private void OnGameStart() { transform.Find("Enemies").gameObject.SetActive(true); }
        private static void OnEnemyKilled()
        {
            GameModel.KillCount++;
            if (GameModel.KillCount < 10) return;
            GamePassEvent.Trigger();
        }
    }
}