using UnityEngine;
namespace QFramework.Example
{
    public class GameOverPanel : MonoBehaviour, IController
    {
        public IArchitecture GetArchitecture() => PointGame.Interface;
    }
}