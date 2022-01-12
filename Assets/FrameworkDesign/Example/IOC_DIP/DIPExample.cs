#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
namespace FrameworkDesign.Example
{
    public class DIPExample : MonoBehaviour
    {
        private IOCContainer mContainer;
        private IStorage mStorage;
        private void Awake()
        {
            //创建一个IOC容器
            mContainer = new IOCContainer();
        }
        private void OnEnable()
        {
            //注册运行时模块
            mContainer.Register<IStorage>(new PlayerPrefsStorage());
            mStorage = mContainer.Get<IStorage>();
            mStorage.SaveString("name", "name运行时存储");
            Debug.Log($"mStorage.LoadString(\"name\") {mStorage.LoadString("name")}");
            Debug.Log($"mStorage.LoadString(\"Name\") {mStorage.LoadString("Name")}");
            //切换实现
            mContainer.Register<IStorage>(new EditorPrefsStorage());
            mStorage = mContainer.Get<IStorage>();
            mStorage.SaveString("name2", "name2编辑器存储");
            Debug.Log($"mStorage.LoadString(\"name\") {mStorage.LoadString("name")}");
            Debug.Log($"mStorage.LoadString(\"Name2\") {mStorage.LoadString("Name2")}");
            Debug.Log($"mStorage.LoadString(\"name2\") {mStorage.LoadString("name2")}");
            Debug.Log($"mStorage.LoadString(\"Name2\") {mStorage.LoadString("Name2")}");
        }

        /// <summary>
        /// 存取接口
        /// </summary>
        public interface IStorage
        {
            void SaveString(string key, string value);
            string LoadString(string key, string value = default);
        }
        /// <summary>
        /// 实现接口
        /// 运行时存储
        /// </summary>
        public class PlayerPrefsStorage : IStorage
        {
            public void SaveString(string key, string value) { PlayerPrefs.SetString(key, value); }
            public string LoadString(string key, string value = default) { return PlayerPrefs.GetString(key, value); }
        }
        /// <summary>
        /// 实现接口
        /// 编辑器存储
        /// </summary>
        public class EditorPrefsStorage : IStorage
        {
            public void SaveString(string key, string value)
            {
#if UNITY_EDITOR
                EditorPrefs.SetString(key, value);
#endif
            }
            public string LoadString(string key, string value = default)
            {
#if UNITY_EDITOR
                return EditorPrefs.GetString(key, value);
#else
                return String.Empty;
#endif
            }
        }
    }
}