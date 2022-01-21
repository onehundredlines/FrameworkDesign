using UnityEngine;
namespace QFramework.Example
{
    public class Enemy : MonoBehaviour, IController
    {
        private void OnMouseDown()
        {
            gameObject.SetActive(false);
            this.SendCommand<KillEnemyCommand>();
        }
        public IArchitecture GetArchitecture() => PointGame.Interface;
    }
}