using UnityEngine;
namespace FrameworkDesign.Example
{
    public interface IStorage : IUtility
    {
        void SaveInt(string key, int value);
        int LoadInt(string key, int value = default);
    }

    public class PlayerPrefsStorage : IStorage
    {
        public void SaveInt(string key, int value) => PlayerPrefs.SetInt(key,value);
        public int LoadInt(string key, int value = default) => PlayerPrefs.GetInt(key, value);
    }
}