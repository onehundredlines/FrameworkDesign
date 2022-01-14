namespace FrameworkDesign
{
    public interface ICanSendEvent : IBelongToArchitecture
    {
    }
    public static class CanSendEventExtension
    {
        public static void SendEvent<E>(this ICanSendEvent self) where E : new() => self.GetArchitecture().SendEvent<E>();
        public static void SendEvent<E>(this ICanSendEvent self, E onEvent) => self.GetArchitecture().SendEvent<E>(onEvent);
    }
}