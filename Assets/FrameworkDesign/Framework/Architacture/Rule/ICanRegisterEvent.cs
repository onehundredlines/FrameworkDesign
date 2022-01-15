using System;
namespace FrameworkDesign
{
    public interface ICanRegisterEvent : IBelongToArchitecture
    {
    }
    public static class CanRegisterEventExtension
    {
        public static ICancel RegisterEvent<E>(this ICanRegisterEvent self,Action<E> onEvent) => self.GetArchitecture().RegisterEvent<E>(onEvent);
        public static void CancelEvent<E>(this ICanRegisterEvent self, Action<E> onEvent) => self.GetArchitecture().CancelEvent(onEvent);
    }
}