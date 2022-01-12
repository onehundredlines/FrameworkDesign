namespace FrameworkDesign
{
    /// <summary>
    /// 架构
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class Architecture<T> where T : Architecture<T>, new()
    {
        //类似单例，仅可在内部访问。与单例没有访问限制不同，
        private static T mArchitecture;
        private IOCContainer mContainer = new IOCContainer();
        /// <summary>
        /// 确保Container有实例
        /// </summary>
        private static void MakeSureArchitecture()
        {
            if (mArchitecture != null) return;
            mArchitecture = new T();
            mArchitecture.Init();
        }
        //留给子类注册模块
        protected abstract void Init();

        //提供一个获取模块的API
        public static T Get<T>() where T : class
        {
            MakeSureArchitecture();
            return mArchitecture.mContainer.Get<T>();
        }
        //提供一个注册模块的API
        public void Register<T>(T instance)
        {
            MakeSureArchitecture();
            mArchitecture.mContainer.Register<T>(instance);
        }
    }
}