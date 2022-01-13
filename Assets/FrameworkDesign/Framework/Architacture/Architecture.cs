using System;
using System.Collections.Generic;
namespace FrameworkDesign
{
    public interface IArchitecture
    {
        /// <summary>
        /// 注册System系统层
        /// </summary>
        void RegisterSystem<S>(S system) where S : ISystem;
        /// <summary>
        /// 注册Model数据层
        /// </summary>
        void RegisterModel<M>(M model) where M : IModel;
        /// <summary>
        /// 注册Utility工具层
        /// </summary>
        void RegisterUtility<U>(U utility);
        M GetModel<M>() where M : class, IModel;
        /// <summary>
        /// 获取Utility工具
        /// 获取API
        /// </summary>
        C GetUtility<C>() where C : class;
    }
    /// <summary>
    /// 架构
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class Architecture<T> : IArchitecture where T : Architecture<T>, new()
    {

        // 是否初始化完成
        private bool mInited;
        // 缓存要初始化的Model
        private List<IModel> mModelList = new List<IModel>();
        // 缓存要初始化的System
        private List<ISystem> mSystemList = new List<ISystem>();
        // 类似单例，仅可在内部访问。与单例没有访问限制不同
        private static T mArchitecture;
        public static IArchitecture Interface {
            get {
                if (mArchitecture == null)
                {
                    MakeSureArchitecture();
                }
                return mArchitecture;
            }
        }
        private readonly IOCContainer mContainer = new IOCContainer();
        // 注册补丁
        public static Action<T> OnRegisterPatch = architecture => { };
        /// <summary>
        /// 确保Container有实例
        /// </summary>
        private static void MakeSureArchitecture()
        {
            if (mArchitecture != null) return;
            mArchitecture = new T();
            mArchitecture.Init();
            //调用
            OnRegisterPatch?.Invoke(mArchitecture);
            //初始化Model，此处先初始化Model，因为Model比System更底层，System是会访问Model的，所以要早于初始化System
            foreach(var model in mArchitecture.mModelList)
            {
                model.Init();
            }
            //清空Model
            mArchitecture.mModelList.Clear();
            //初始化System，初始化Model之后进行System的初始化
            foreach(var system in mArchitecture.mSystemList)
            {
                system.Init();
            }
            //清空System
            mArchitecture.mSystemList.Clear();
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
        /// 注册System层
        /// 注册API
        /// </summary>
        public void RegisterSystem<S>(S system) where S : ISystem
        {
            //给System赋值
            system.SetArchitecture(this);
            mContainer.Register(system);
            if (!mInited)
            {
                mSystemList.Add(system);
            } else
            {
                system.Init();
            }
        }
        /// <summary>
        /// 注册Model层
        /// 注册API
        /// </summary>
        public void RegisterModel<M>(M model) where M : IModel
        {
            //给Model赋值
            model.SetArchitecture(this);
            mContainer.Register(model);
            if (!mInited)
            {
                mModelList.Add(model);
            } else
            {
                model.Init();
            }
        }
        public void RegisterUtility<U>(U utility) { mContainer.Register(utility); }
        /// <summary>
        /// 获取模块
        /// 获取API
        /// </summary>
        public static G Get<G>() where G : class
        {
            MakeSureArchitecture();
            return mArchitecture.mContainer.Get<G>();
        }
        public M GetModel<M>() where M : class, IModel { return mContainer.Get<M>(); }
        public U GetUtility<U>() where U : class { return mContainer.Get<U>(); }
    }
}