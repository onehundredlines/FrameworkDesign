using UnityEngine;
namespace FrameworkDesign.Example
{
    public class InterfaceStructExample : MonoBehaviour
    {
        public interface ICustomScript
        {
            void Start();
            void Update();
            void Destroy();
        }
        public abstract class CustomScript : ICustomScript
        {

            void ICustomScript.Start() { OnStart(); }
            void ICustomScript.Update() { OnUpdate(); }
            void ICustomScript.Destroy() { OnDestroy(); }
            protected abstract void OnStart();
            protected abstract void OnUpdate();
            protected abstract void OnDestroy();
        }
        public class MyScript : CustomScript
        {
            protected override void OnStart()
            {
                //如果不对CustomScript的Start函数进行显示实现，也就是不对Start函数进行限制
                //那么如果在此调用CustomScript的Start函数，将出现循环调用
                // Start();
                print(nameof(OnStart));
            }
            protected override void OnUpdate()
            {
                //如果不对CustomScript的Update函数进行显示实现，也就是不对Update函数进行限制
                //那么如果在此调用CustomScript的Update函数，将出现循环调用
                // Update();
                print(nameof(OnUpdate));
            }
            protected override void OnDestroy()
            {
                //如果不对CustomScript的Destroy函数进行显示实现，也就是不对Destroy函数进行限制
                //那么如果在此调用CustomScript的Destroy函数，将出现循环调用
                // Destroy();                   
                print(nameof(OnDestroy));
            }
        }
        private void Start()
        {
            // MyScript myScript = new MyScript();
            // myScript.Start();
            // myScript.Update();
            // myScript.Destroy();

            //一般是底层代码会用到
            ICustomScript myScript = new MyScript();
            myScript.Start();
            myScript.Update();
            myScript.Destroy();
        }
    }
}