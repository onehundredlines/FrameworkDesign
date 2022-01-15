namespace FrameworkDesign
{
    public interface ICanGetModel : IBelongToArchitecture 
    {
    }
    public static class CanGetModelExtension
    {
        public static M GetModel<M>(this ICanGetModel self) where M : class, IModel => self.GetArchitecture().GetModel<M>();
    }
}