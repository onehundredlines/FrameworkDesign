using System;
using System.Collections.Generic;
using UnityEngine;
namespace QFramework
{
    #region Architecture
    public interface IArchitecture
    {
        void RegisterSystem<TSystem>(TSystem system) where TSystem : ISystem;
        void RegisterModel<TModel>(TModel model) where TModel : IModel;
        void RegisterUtility<TUtility>(TUtility utility) where TUtility : IUtility;
        TSystem GetSystem<TSystem>() where TSystem : class, ISystem;
        TModel GetModel<TModel>() where TModel : class, IModel;
        TUtility GetUtility<TUtility>() where TUtility : class, IUtility;
        void SendCommand<TCommand>() where TCommand : ICommand, new();
        void SendCommand<TCommand>(TCommand command) where TCommand : ICommand;
        TResult SendQuery<TResult>(IQuery<TResult> query);
        void SendEvent<TEvent>() where TEvent : new();
        void SendEvent<TEvent>(TEvent e);
        ICancel RegisterEvent<TEvent>(Action<TEvent> onEvent);
        void CancelEvent<TEvent>(Action<TEvent> onEvent);
    }
    public abstract class Architecture<T> : IArchitecture where T : Architecture<T>, new()
    {
        /// <summary>
        /// 是否初始化完成
        /// </summary>
        private bool isInitialized;
        // 缓存要初始化的Model
        private readonly List<IModel> mModelList = new List<IModel>();
        // 缓存要初始化的System
        private readonly List<ISystem> mSystemList = new List<ISystem>();
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
            mArchitecture.isInitialized = true;
        }
        /// <summary>
        /// 子类注册模块
        /// </summary>
        protected abstract void Init();
        private readonly IOCContainer mContainer = new IOCContainer();
        public void RegisterSystem<TSystem>(TSystem system) where TSystem : ISystem
        {
            //给System赋值
            system.SetArchitecture(this);
            mContainer.Register(system);
            if (!isInitialized) mSystemList.Add(system);
            else system.Init();
        }
        public void RegisterModel<TModel>(TModel model) where TModel : IModel
        {
            //给Model赋值
            model.SetArchitecture(this);
            mContainer.Register(model);
            if (!isInitialized) mModelList.Add(model);
            else model.Init();
        }
        public void RegisterUtility<TUtility>(TUtility utility) where TUtility : IUtility => mContainer.Register(utility);
        public TSystem GetSystem<TSystem>() where TSystem : class, ISystem => mContainer.Get<TSystem>();
        public TModel GetModel<TModel>() where TModel : class, IModel => mContainer.Get<TModel>();
        public TUtility GetUtility<TUtility>() where TUtility : class, IUtility => mContainer.Get<TUtility>();
        public void SendCommand<TCommand>() where TCommand : ICommand, new()
        {
            var command = new TCommand();
            command.SetArchitecture(this);
            command.Execute();
            // command.SetArchitecture(null);
        }
        public void SendCommand<TCommand>(TCommand command) where TCommand : ICommand
        {
            command.SetArchitecture(this);
            command.Execute();
            // command.SetArchitecture(null);
        }
        public TResult SendQuery<TResult>(IQuery<TResult> query)
        {
            query.SetArchitecture(this);
            return query.Do();
        }
        private readonly ITypeEventSystem mTypeEventSystem = new TypeEventSystem();
        public void SendEvent<TEvent>() where TEvent : new() => mTypeEventSystem.Send<TEvent>();
        public void SendEvent<TEvent>(TEvent e) => mTypeEventSystem.Send<TEvent>(e);
        public ICancel RegisterEvent<TEvent>(Action<TEvent> onEvent) => mTypeEventSystem.Register(onEvent);
        public void CancelEvent<TEvent>(Action<TEvent> onEvent) => mTypeEventSystem.Cancel(onEvent);
    }
    #endregion
    #region Controller
    public interface IController : ICanGetModel, ICanGetSystem, ICanSendCommand, ICanRegisterEvent, ICanSentQuery
    {
    }
    #endregion
    #region System
    public interface ISystem : ICanSetArchitecture, ICanGetModel, ICanGetUtility, ICanRegisterEvent, ICanSendEvent, ICanGetSystem
    {
        void Init();
    }
    public abstract class AbstractSystem : ISystem
    {
        private IArchitecture mArchitecture;
        IArchitecture IBelongToArchitecture.GetArchitecture() => mArchitecture;
        void ICanSetArchitecture.SetArchitecture(IArchitecture architecture) => mArchitecture = architecture;
        void ISystem.Init() => OnInit();
        protected abstract void OnInit();
    }
    #endregion
    #region Model
    public interface IModel : ICanSetArchitecture, ICanGetUtility, ICanSendEvent
    {
        void Init();
    }
    public abstract class AbstractModel : IModel
    {
        private IArchitecture mArchitecture;
        IArchitecture IBelongToArchitecture.GetArchitecture() => mArchitecture;
        void ICanSetArchitecture.SetArchitecture(IArchitecture architecture) => mArchitecture = architecture;
        void IModel.Init() { OnInit(); }
        protected abstract void OnInit();
    }
    #endregion
    #region Utility
    public interface IUtility
    {
    }
    #endregion
    #region Command
    public interface ICommand : ICanSetArchitecture, ICanGetModel, ICanGetSystem, ICanSendCommand, ICanGetUtility, ICanSendEvent, ICanSentQuery
    {
        void Execute();
    }
    public abstract class AbstractCommand : ICommand
    {
        private IArchitecture mArchitecture;
        IArchitecture IBelongToArchitecture.GetArchitecture() => mArchitecture;
        void ICanSetArchitecture.SetArchitecture(IArchitecture architecture) => mArchitecture = architecture;
        void ICommand.Execute() => OnExecute();
        protected abstract void OnExecute();
    }
    #endregion
    #region Query
    public interface IQuery<out TResult> : ICanSetArchitecture, ICanGetModel, ICanGetSystem, ICanSentQuery
    {
        TResult Do();
    }
    public abstract class AbstractQuery<T> : IQuery<T>
    {
        public T Do() => OnDo();
        protected abstract T OnDo();
        private IArchitecture mArchitecture;
        public void SetArchitecture(IArchitecture architecture) => mArchitecture = architecture;
        public IArchitecture GetArchitecture() => mArchitecture;
    }
    #endregion
    #region Rule
    public interface IBelongToArchitecture
    {
        IArchitecture GetArchitecture();
    }
    public interface ICanSetArchitecture
    {
        void SetArchitecture(IArchitecture architecture);
    }
    public interface ICanGetModel : IBelongToArchitecture
    {
    }
    public static class CanGetModelExtension
    {
        public static TModel GetModel<TModel>(this ICanGetModel self) where TModel : class, IModel => self.GetArchitecture().GetModel<TModel>();
    }
    public interface ICanGetSystem : IBelongToArchitecture
    {
    }
    public static class CanGetSystemExtension
    {
        public static TSystem GetSystem<TSystem>(this ICanGetModel self) where TSystem : class, ISystem => self.GetArchitecture().GetSystem<TSystem>();
    }
    public interface ICanGetUtility : IBelongToArchitecture
    {
    }
    public static class CanGetUtilityExtension
    {
        public static TUtility GetUtility<TUtility>(this IBelongToArchitecture self) where TUtility : class, IUtility => self.GetArchitecture().GetUtility<TUtility>();
    }
    public interface ICanRegisterEvent : IBelongToArchitecture
    {
    }
    public static class CanRegisterEventExtension
    {
        public static ICancel RegisterEvent<TEvent>(this ICanRegisterEvent self, Action<TEvent> onEvent) => self.GetArchitecture().RegisterEvent<TEvent>(onEvent);
        public static void CancelEvent<TEvent>(this ICanRegisterEvent self, Action<TEvent> onEvent) => self.GetArchitecture().CancelEvent(onEvent);
    }
    public interface ICanSendCommand : IBelongToArchitecture
    {
    }
    public static class CanSendCommandExtension
    {
        public static void SendCommand<TCommand>(this ICanSendCommand self) where TCommand : ICommand, new() => self.GetArchitecture().SendCommand<TCommand>();
        public static void SendCommand<TCommand>(this ICanSendCommand self, TCommand command) where TCommand : ICommand => self.GetArchitecture().SendCommand<TCommand>(command);
    }
    public interface ICanSendEvent : IBelongToArchitecture
    {
    }
    public static class CanSendEventExtension
    {
        public static void SendEvent<TEvent>(this ICanSendEvent self) where TEvent : new() => self.GetArchitecture().SendEvent<TEvent>();
        public static void SendEvent<TEvent>(this ICanSendEvent self, TEvent onEvent) => self.GetArchitecture().SendEvent<TEvent>(onEvent);
    }
    public interface ICanSentQuery : IBelongToArchitecture
    {
    }
    public static class CanSendQueryExtension
    {
        public static TResult SendQuery<TResult>(this ICanSentQuery self, IQuery<TResult> query) => self.GetArchitecture().SendQuery(query);
    }
    #endregion
    #region TypeEventSystem
    public interface ITypeEventSystem
    {
        void Send<TEvent>() where TEvent : new();
        void Send<TEvent>(TEvent e);
        ICancel Register<TEvent>(Action<TEvent> onEvent);
        void Cancel<TEvent>(Action<TEvent> onEvent);
    }
    public interface ICancel
    {
        void Cancel();
    }
    public struct TypeEventSystemCancel<T> : ICancel
    {
        public ITypeEventSystem typeEventSystem;
        public Action<T> OnEvent;
        public void Cancel()
        {
            typeEventSystem.Cancel(OnEvent);
            typeEventSystem = null;
            OnEvent = null;
        }
    }
    public class CancelOnDestroyTrigger : MonoBehaviour
    {
        private readonly HashSet<ICancel> mCancel = new HashSet<ICancel>();
        public void AddCancel(ICancel cancel) => mCancel.Add(cancel);
        private void OnDestroy()
        {
            foreach(var cancel in mCancel) cancel.Cancel();
            mCancel.Clear();
        }
    }
    public static class CancelExtension
    {
        public static void CancelOnDestroy(this ICancel cancel, GameObject go)
        {
            var trigger = go.GetComponent<CancelOnDestroyTrigger>();
            if (!trigger) go.AddComponent<CancelOnDestroyTrigger>();
            else trigger.AddCancel(cancel);
        }
    }
    public class TypeEventSystem : ITypeEventSystem
    {
        private interface IRegistrations
        {
        }
        private class Registrations<T> : IRegistrations
        {
            public Action<T> OnEvent = e => { };
        }
        public static readonly TypeEventSystem Global = new TypeEventSystem();
        private readonly Dictionary<Type, IRegistrations> RegistrationsMap = new Dictionary<Type, IRegistrations>();
        public void Send<TEvent>() where TEvent : new()
        {
            var e = new TEvent();
            Send(e);
        }
        public void Send<TEvent>(TEvent e)
        {
            var type = typeof(TEvent);
            if (RegistrationsMap.TryGetValue(type, out var registrations)) ((Registrations<TEvent>)registrations).OnEvent(e);
        }
        public ICancel Register<TEvent>(Action<TEvent> onEvent)
        {
            var type = typeof(TEvent);
            if (!RegistrationsMap.TryGetValue(type, out var registrations))
            {
                registrations = new Registrations<TEvent>();
                RegistrationsMap.Add(type, registrations);
            }
            ((Registrations<TEvent>)registrations).OnEvent += onEvent;
            return new TypeEventSystemCancel<TEvent>
            {
                OnEvent = onEvent,
                typeEventSystem = this
            };
        }
        public void Cancel<TEvent>(Action<TEvent> onEvent)
        {
            var type = typeof(TEvent);
            if (RegistrationsMap.TryGetValue(type, out var registrations)) ((Registrations<TEvent>)registrations).OnEvent -= onEvent;
        }
    }
    public interface IOnEvent<in TEvent>
    {
        void OnEvent(TEvent e);
    }
    public static class OnGlobalEventExtension
    {
        public static ICancel Register<T>(this IOnEvent<T> self) where T : struct => TypeEventSystem.Global.Register<T>(self.OnEvent);
        public static void Cancel<T>(this IOnEvent<T> self) where T : struct => TypeEventSystem.Global.Cancel<T>(self.OnEvent);
    }
    #endregion
    #region IOC
    public class IOCContainer
    {
        private readonly Dictionary<Type, object> mInstance = new Dictionary<Type, object>();
        public void Register<T>(T instance)
        {
            var key = typeof(T);
            if (!mInstance.ContainsKey(key)) mInstance.Add(key, instance);
            else mInstance[key] = instance;
        }
        public T Get<T>() where T : class
        {
            var key = typeof(T);
            if (mInstance.TryGetValue(key, out var retInstance)) return retInstance as T;
            return null;
        }
    }
    #endregion
    #region BindableProperty
    public class BindableProperty<T>
    {
        private T mValue;
        public T Value {
            get => mValue;
            set {
                if (value == null && mValue == null || value != null && value.Equals(mValue)) return;
                mValue = value;
                mOnValueChanged?.Invoke(value);
            }
        }
        private Action<T> mOnValueChanged = v => { };
        public BindableProperty(T value = default) { mValue = value; }
        public ICancel Register(Action<T> onValueChanged)
        {
            mOnValueChanged += onValueChanged;
            return new BindablePropertyCancel<T>
            {
                BindableProperty = this,
                OnValueChanged = onValueChanged
            };
        }
        public ICancel RegisterWithInitValue(Action<T> onValueChanged)
        {
            onValueChanged(mValue);
            return Register(onValueChanged);
        }
        public static implicit operator T(BindableProperty<T> property) => property.Value;
        public override string ToString() { return Value.ToString(); }
        public void Cancel(Action<T> onValueChanged) => mOnValueChanged -= onValueChanged;
    }
    public class BindablePropertyCancel<T> : ICancel
    {
        public BindableProperty<T> BindableProperty { get; set; }
        public Action<T> OnValueChanged { get; set; }
        public void Cancel()
        {
            BindableProperty.Cancel(OnValueChanged);
            BindableProperty = null;
            OnValueChanged = null;
        }
    }
    #endregion
}