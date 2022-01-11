using System;
using UnityEngine;

namespace FrameworkDesign.Example
{
    public class GamePassEvent : MonoBehaviour
    {
        private static Action onEvent;
        public static void RegisterEvent(Action onEvent)
        {
            GamePassEvent.onEvent += onEvent;
        }
        public static void UnRegisterEvent(Action onEvent)
        {
            GamePassEvent.onEvent -= onEvent;
        }
        public static void Trigger()
        {
            onEvent?.Invoke();
        }
    }
}