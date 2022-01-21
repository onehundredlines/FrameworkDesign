#if UNITY_EDITOR
using UnityEditor;
#endif
using QFramework;
using UnityEngine;
namespace CounterApp
{
    public interface IStorage : IUtility
    {
        void SaveInt(string key, int value);
        int LoadInt(string key, int value = default);
    }
    public class PlayerPrefsStorage : IStorage
    {

        public void SaveInt(string key, int value) { PlayerPrefs.SetInt(key, value); }
        public int LoadInt(string key, int value = default) { return PlayerPrefs.GetInt(key, value); }
    }
    public class EditorPrefsStorage : IStorage
    {
        public void SaveInt(string key, int value)
        {
#if UNITY_EDITOR
            EditorPrefs.SetInt(key, value);
#endif
        }
        public int LoadInt(string key, int value = default)
        {
#if UNITY_EDITOR
            return EditorPrefs.GetInt(key, value);
#else
            return 0;
#endif
        }
    }
}