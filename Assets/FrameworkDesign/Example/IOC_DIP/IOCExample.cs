using UnityEngine;

namespace FrameworkDesign.Example
{
    public class IOCExample : MonoBehaviour
    {
        private IOCContainer container;
        private IBluetoothManager bluetoothManager;
        private void Awake()
        {
            //创建IOC容器
            container = new IOCContainer();
            //注册一个蓝牙管理器的实例
            container.Register<IBluetoothManager>(new BluetoothManager());
            //根据类型获取蓝牙管理器的实例
            bluetoothManager = container.Get<IBluetoothManager>();
        }
        private void OnEnable()
        {
            //连接蓝牙
            bluetoothManager.Connect();
        }
    }
    public interface IBluetoothManager
    {
        void Connect();
        void Disconnect();
    }
    public class BluetoothManager : IBluetoothManager
    {
        public void Connect() { Debug.Log("蓝牙连接成功"); }
        public void Disconnect() { Debug.Log("蓝牙断开连接"); }
    }
}