using System;

namespace FrameworkDesign.Example
{
    public static class GameStartEvent
    {
        private static Action onEvent;
        public static void RegisterEvent(Action onEvent)
        {
            GameStartEvent.onEvent += onEvent;
        }
        public static void UnRegisterEvent(Action onEvent)
        {
            GameStartEvent.onEvent -= onEvent;
        }
        public static void Trigger()
        {
            onEvent?.Invoke();
        }
    }
}
