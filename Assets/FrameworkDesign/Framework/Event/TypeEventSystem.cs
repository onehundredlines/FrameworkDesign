using System;
using System.Collections.Generic;
using UnityEngine;

namespace FrameworkDesign
{
    public interface ITypeEventSystem
    {
        void Send<T>() where T : new();
        void Send<T>(T e);
        IUnregister Register<T>(Action<T> onEvent);
        void Unregister<T>(Action<T> onEvent);
    }
    public interface IUnregister
    {
        void Unregister();
    }
    public struct TypeEventSystemUnregister<T> : IUnregister
    {
        public ITypeEventSystem typeEventSystem;
        public Action<T> OnEvent;
        public void Unregister()
        {
            typeEventSystem.Unregister<T>(OnEvent);
            typeEventSystem = null;
            OnEvent = null;
        }
    }
    public class UnregisterOnDestroyTrigger : MonoBehaviour
    {
        private HashSet<IUnregister> mUnregisters = new HashSet<IUnregister>();
        public void AddUnregister(IUnregister unregister) => mUnregisters.Add(unregister);
        private void OnDestroy()
        {
            foreach(var unregister in mUnregisters) unregister.Unregister();
            mUnregisters.Clear();
        }
    }
    public static class UnregisterExtension
    {
        public static void UnregisterWhenGameObjectDestroy(this IUnregister unregister, GameObject go)
        {
            var trigger = go.GetComponent<UnregisterOnDestroyTrigger>();
            if (!trigger) go.AddComponent<UnregisterOnDestroyTrigger>();
            else trigger.AddUnregister(unregister);
        }
    }
    public class TypeEventSystem : ITypeEventSystem
    {
        public interface IRegistrations
        {

        }
        public class Registrations<T> : IRegistrations
        {
            public Action<T> OnEvent = e => { };
        }
        public readonly Dictionary<Type, IRegistrations> RegistrationsMap = new Dictionary<Type, IRegistrations>();
        public void Send<T>() where T : new()
        {
            var e = new T();
            Send<T>(e);
        }
        public void Send<T>(T e)
        {
            var type = typeof(T);
            if (RegistrationsMap.TryGetValue(type, out var registrations)) ((Registrations<T>)registrations).OnEvent(e);
        }
        public IUnregister Register<T>(Action<T> onEvent)
        {
            var type = typeof(T);
            if (!RegistrationsMap.TryGetValue(type, out var registrations))
            {
                registrations = new Registrations<T>();
                RegistrationsMap.Add(type, registrations);
            }
            ((Registrations<T>)registrations).OnEvent += onEvent;
            return new TypeEventSystemUnregister<T>()
            {
                OnEvent = onEvent,
                typeEventSystem = this
            };
        }
        public void Unregister<T>(Action<T> onEvent)
        {
            var type = typeof(T);
            if (RegistrationsMap.TryGetValue(type, out var registrations)) ((Registrations<T>)registrations).OnEvent -= onEvent;
        }
    }
}