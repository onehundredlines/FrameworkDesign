using UnityEngine;
namespace QFramework.Example
{
    public class GlobalEvent : MonoBehaviour, IOnEvent<GlobalEvent.GlobalEventB>
    {
        private void Start()
        {
            //注册方式和注销事件方式1
            QFramework.TypeEventSystem.Global.Register<GlobalEventA>(OnGlobalEventA).CancelOnDestroy(gameObject);
            // IOnEvent接口注册事件
            this.Register();
        }
        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                QFramework.TypeEventSystem.Global.Send<GlobalEventA>();
            }
            if (Input.GetMouseButtonDown(1))
            {
                QFramework.TypeEventSystem.Global.Send<GlobalEventB>();
            }
        }
        private void OnDestroy()
        {
        //     // 注销事件方式2：
        //     QFramework.TypeEventSystem.Global.Cancel<GlobalEventA>(OnGlobalEventA);
                // 使用IOnEvent接口监听注销事件
                this.Cancel();
        }
        public struct GlobalEventA
        {
        }
        public struct GlobalEventB
        {
        }
        private void OnGlobalEventA(GlobalEventA obj) { Debug.Log(obj.ToString()); }
        public void OnEvent(GlobalEventB e) { Debug.Log(e.ToString()); }
        
    }
}