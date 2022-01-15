using System;
using System.Collections.Generic;
using UnityEngine;

namespace FrameworkDesign
{
    public interface ITypeEventSystem
    {
        void Send<T>() where T : new();
        void Send<T>(T e);
        ICancel Register<T>(Action<T> onEvent);
        void Cancel<T>(Action<T> onEvent);
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
            typeEventSystem.Cancel<T>(OnEvent);
            typeEventSystem = null;
            OnEvent = null;
        }
    }
    public class CancelOnDestroyTrigger : MonoBehaviour
    {
        private HashSet<ICancel> mCancel = new HashSet<ICancel>();
        public void AddCancel(ICancel cancel) => mCancel.Add(cancel);
        private void OnDestroy()
        {
            foreach(var cancel in mCancel) cancel.Cancel();
            mCancel.Clear();
        }
    }
    public static class CancelExtension
    {
        public static void CancelWhenGameObjectDestroy(this ICancel cancel, GameObject go)
        {
            var trigger = go.GetComponent<CancelOnDestroyTrigger>();
            if (!trigger) go.AddComponent<CancelOnDestroyTrigger>();
            else trigger.AddCancel(cancel);
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
        public ICancel Register<T>(Action<T> onEvent)
        {
            var type = typeof(T);
            if (!RegistrationsMap.TryGetValue(type, out var registrations))
            {
                registrations = new Registrations<T>();
                RegistrationsMap.Add(type, registrations);
            }
            ((Registrations<T>)registrations).OnEvent += onEvent;
            return new TypeEventSystemCancel<T>()
            {
                OnEvent = onEvent,
                typeEventSystem = this
            };
        }
        public void Cancel<T>(Action<T> onEvent)
        {
            var type = typeof(T);
            if (RegistrationsMap.TryGetValue(type, out var registrations)) ((Registrations<T>)registrations).OnEvent -= onEvent;
        }
    }
}