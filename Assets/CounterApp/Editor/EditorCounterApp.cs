using FrameworkDesign;
using UnityEditor;
using UnityEngine;
namespace CounterApp.Editor
{
    public class EditorCounterApp : EditorWindow, IController
    {
        [MenuItem("EditorCounterApp/Open")]
        static void Open()
        {
            CounterApp.OnRegisterPatch += app => app.RegisterUtility<IStorage>(new EditorPrefsStorage());
            var window = GetWindow<EditorCounterApp>();
            window.position = new Rect(100, 100, 400, 600);
            window.titleContent = new GUIContent(nameof(EditorCounterApp));
            window.Show();
        }
        private void OnGUI()
        {
            if (GUILayout.Button("+")) this.SendCommand<AddCountCommand>();
            //GUI是实时刷新，直接渲染就行了
            GUILayout.Label(CounterApp.Get<ICounterModel>().Count.Value.ToString());
            if (GUILayout.Button("-")) this.SendCommand<SubCountCommand>();
        }
        public IArchitecture GetArchitecture() => CounterApp.Interface;
    }
}