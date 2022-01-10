using UnityEngine;

namespace FrameworkDesign.Example
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField]
        private GameObject gamePassPanel;
        private static int killedEnemyCount;
        private void Awake()
        {
            if (gamePassPanel.activeSelf)
            {
                gamePassPanel.gameObject.SetActive(false);
            }
        }
        private void OnMouseDown()
        {
            Destroy(gameObject);
            ++killedEnemyCount;
            if (killedEnemyCount >= 10)
            {
                gamePassPanel.gameObject.SetActive(true);
            }
        }
    }

}