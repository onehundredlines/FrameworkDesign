using UnityEngine;
namespace FrameworkDesign.Example
{
    public class MissArea : MonoBehaviour, IController
    {
        private void OnMouseDown()
        {
            this.SendCommand<MissCommand>();
        }

        public IArchitecture GetArchitecture() => PointGame.Interface;
    }
}
