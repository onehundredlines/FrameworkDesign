namespace FrameworkDesign
{
    public interface ICanGetSystem : IBelongToArchitecture
    {
    }
    public static class CanGetSystemExtension
    {
        public static T GetSystem<T>(this ICanGetModel self) where T : class, ISystem => self.GetArchitecture().GetSystem<T>();
    }
}