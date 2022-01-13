namespace FrameworkDesign
{
    public interface ICanGetUtility : IBelongToArchitecture
    {
    }
    public static class CanGetUtilityExtension
    {
        public static T GetUtility<T>(this IBelongToArchitecture self) where T : class, IUtility => self.GetArchitecture().GetUtility<T>();
    }
}