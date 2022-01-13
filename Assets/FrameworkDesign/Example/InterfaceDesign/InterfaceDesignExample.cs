using UnityEngine;

namespace FrameworkDesign.Example
{
    public interface ICanSayHello
    {
        void SayHello();
        void SayOther();
    }
    public class InterfaceDesignExample : MonoBehaviour, ICanSayHello
    {
        /// <summary>
        /// 接口的隐式实现
        /// </summary>
        public void SayHello() { print("Hello"); }
        /// <summary>
        /// 接口的显式实现
        /// </summary>
        void ICanSayHello.SayOther() { print("What can I tell you"); }

        private void OnEnable()
        {
            SayHello();
            //显式实现的函数，必须通过接口对象来调用
            (this as ICanSayHello).SayOther();
        }
    }
}