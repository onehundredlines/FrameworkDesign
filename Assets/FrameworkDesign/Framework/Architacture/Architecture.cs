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
        S GetSystem<S>() where S : class, ISystem;
        /// <summary>
        /// 注册Model数据层
        /// </summary>
        void RegisterModel<M>(M model) where M : IModel;
        M GetModel<M>() where M : class, IModel;
        /// <summary>
        /// 注册Utility工具层
        /// </summary>
        void RegisterUtility<U>(U utility) where U : IUtility;
        /// <summary>
        /// 获取Utility工具
        /// 获取API
        /// </summary>
        C GetUtility<C>() where C : class, IUtility;
        /// <summary>
        /// 创建Command，并将Command发送给Architecture
        /// </summary>
        void SendCommand<N>() where N : ICommand, new();
        /// <summary>
        /// 将Command发送给Architecture
        /// </summary>
        void SendCommand<N>(N command) where N : ICommand;
        void SendEvent<E>() where E : new();
        void SendEvent<E>(E e);
        ICancel RegisterEvent<E>(Action<E> onEvent);
        void CancelEvent<E>(Action<E> onEvent);
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
        // 注册补丁
        public static Action<T> OnRegisterPatch = architecture => { };
        // 类似单例，仅可在内部访问。与单例没有访问限制不同
        private static T mArchitecture;
        public static IArchitecture Interface {
            get {
                if (mArchitecture == null) MakeSureArchitecture();
                return mArchitecture;
            }
        }
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
            foreach(var model in mArchitecture.mModelList) model.Init();
            //清空Model
            mArchitecture.mModelList.Clear();
            //初始化System，初始化Model之后进行System的初始化
            foreach(var system in mArchitecture.mSystemList) system.Init();
            //清空System
            mArchitecture.mSystemList.Clear();
            mArchitecture.mInited = true;
        }
        /// <summary>
        /// 子类注册模块
        /// </summary>
        protected abstract void Init();
        private IOCContainer mContainer = new IOCContainer();
        /// <summary>
        /// 注册模块API
        /// </summary>
        private static void Register<K>(K instance)
        {
            MakeSureArchitecture();
            mArchitecture.mContainer.Register(instance);
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
            if (!mInited) mSystemList.Add(system);
            else system.Init();
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
            if (!mInited) mModelList.Add(model);
            else model.Init();
        }
        public void RegisterUtility<U>(U utility) where U : IUtility => mContainer.Register(utility);
        /// <summary>
        /// 获取System模块
        /// </summary>
        public S GetSystem<S>() where S : class, ISystem => mContainer.Get<S>();
        /// <summary>
        /// 获取Model模块
        /// </summary>
        public M GetModel<M>() where M : class, IModel => mContainer.Get<M>();
        /// <summary>
        /// 获取Utility模块
        /// </summary>
        public U GetUtility<U>() where U : class, IUtility => mContainer.Get<U>();
        public void SendCommand<N>() where N : ICommand, new()
        {
            var command = new N();
            command.SetArchitecture(this);
            command.Execute();
            // command.SetArchitecture(null);
        }
        public void SendCommand<N>(N command) where N : ICommand
        {
            command.SetArchitecture(this);
            command.Execute();
            // command.SetArchitecture(null);
        }
        private readonly ITypeEventSystem mTypeEventSystem = new TypeEventSystem();
        public void SendEvent<E>() where E : new() => mTypeEventSystem.Send<E>();
        public void SendEvent<E>(E e) => mTypeEventSystem.Send<E>(e);
        public ICancel RegisterEvent<E>(Action<E> onEvent) => mTypeEventSystem.Register(onEvent);
        public void CancelEvent<E>(Action<E> onEvent) => mTypeEventSystem.Cancel(onEvent);
    }
}