using System;
using System.Reflection;
namespace FrameworkDesign
{
    public class Singleton<T> where T : Singleton<T>
    {
        private static T instance;
        public static T Instance {
            get {
                if (instance == null)
                {
                    var type = typeof(T);
                    var constructorInfos = type.GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic);
                    var constructor = Array.Find(constructorInfos, info => info.GetParameters().Length == 0);
                    if (constructor == null)
                    {
                        throw new Exception($"NonPublic construct not find in {type.Name}");
                    }
                    instance = constructor.Invoke(null) as T;
                }
                return instance;
            }
        }
        // private protected Singleton(){}
    }
}