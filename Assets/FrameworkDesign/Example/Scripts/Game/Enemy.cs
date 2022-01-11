using UnityEngine;

namespace FrameworkDesign.Example
{
    public class Enemy : MonoBehaviour
    {
        private void OnMouseDown()
        {
            Destroy(gameObject);
            GameModel.KillCount.Value++;
        }
    }
}