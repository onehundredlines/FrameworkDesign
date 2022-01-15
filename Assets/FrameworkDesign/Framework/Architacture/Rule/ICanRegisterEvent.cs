using System;
namespace FrameworkDesign
{
    public interface ICanRegisterEvent : IBelongToArchitecture
    {
    }
    public static class CanRegisterEventExtension
    {
        public static IUnregister RegisterEvent<E>(this ICanRegisterEvent self,Action<E> onEvent) => self.GetArchitecture().RegisterEvent<E>(onEvent);
        public static void UnregisterEvent<E>(this ICanRegisterEvent self, Action<E> onEvent) => self.GetArchitecture().CancelEvent(onEvent);
    }
}