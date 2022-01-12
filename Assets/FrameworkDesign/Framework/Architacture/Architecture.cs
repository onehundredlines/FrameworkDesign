using System;
using System.Collections.Generic;
namespace FrameworkDesign
{
    public interface IArchitecture
    {
        /// <summary>
        /// 注册Model
        /// </summary>
        void RegisterModel<M>(M model) where M : IModel;
        /// <summary>
        /// 注册Utility
        /// </summary>
        void RegisterUtility<U>(U utility);
        /// <summary>
        /// 获取工具
        /// 获取Utility API
        /// </summary>
        T GetUtility<T>() where T : class;
    }
    /// <summary>
    /// 架构
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class Architecture<T> : IArchitecture where T : Architecture<T>, new()
    {
        //是否初始化完成
        private bool mInited;
        //用来缓存要初始化的Model
        private List<IModel> mModelList = new List<IModel>();
        //类似单例，仅可在内部访问。与单例没有访问限制不同，
        private static T mArchitecture;
        private readonly IOCContainer mContainer = new IOCContainer();
        public static Action<T> OnRegisterPatch = architecture => { };
        /// <summary>
        /// 确保Container有实例
        /// </summary>
        private static void MakeSureArchitecture()
        {
            if (mArchitecture != null) return;
            mArchitecture = new T();
            mArchitecture.Init();
            OnRegisterPatch?.Invoke(mArchitecture);
            foreach(var architectureModel in mArchitecture.mModelList)
            {
                architectureModel.Init();
            }
            mArchitecture.mModelList.Clear();
            mArchitecture.mInited = true;
        }
        /// <summary>
        /// 子类注册模块
        /// </summary>
        protected abstract void Init();
        /// <summary>
        /// 注册模块API
        /// </summary>
        public static void Register<K>(K instance)
        {
            MakeSureArchitecture();
            mArchitecture.mContainer.Register<K>(instance);
        }
        /// <summary>
        /// 注册Model API
        /// </summary>
        public void RegisterModel<M>(M model) where M : IModel
        {
            //给Model赋值
            model.Architecture = this;
            mContainer.Register(model);
            if (!mInited)
            {
                mModelList.Add(model);
            } else
            {
                model.Init();
            }
        }
        public void RegisterUtility<U>(U utility)
        {
            mContainer.Register(utility);
        }
        /// <summary>
        /// 获取模块API
        /// </summary>
        public static K Get<K>() where K : class
        {
            MakeSureArchitecture();
            return mArchitecture.mContainer.Get<K>();
        }
        public L GetUtility<L>() where L : class { return mContainer.Get<L>(); }
    }
}