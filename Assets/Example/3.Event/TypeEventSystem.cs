using System;
using UnityEngine;
namespace QFramework.Example
{
    public class TypeEventSystem : MonoBehaviour
    {
        public struct EventA
        {
        }
        public struct EventB
        {
            public int ParamB;
        }
        public interface IEventGroup
        {
        }
        public struct EventC : IEventGroup
        {
        }

        public struct EventD : IEventGroup
        {
        }
        private QFramework.TypeEventSystem typeEventSystem = new QFramework.TypeEventSystem();
        private void Start()
        {
            typeEventSystem.Register<EventA>(new Action<EventA>(OnEventA));
            typeEventSystem.Register<EventA>(onEventA => { }).CancelOnDestroy(gameObject);
            typeEventSystem.Register<EventB>(onEventB => { Debug.Log($"onEventB {onEventB.ParamB}"); }).CancelOnDestroy(gameObject);
            typeEventSystem.Register<EventB>(OnEventB).CancelOnDestroy(gameObject);
            typeEventSystem.Register<IEventGroup>(e => { Debug.Log($"{e.GetType()}"); }).CancelOnDestroy(gameObject);

        }
        private void Update()
        {
            if (Input.GetMouseButtonDown(0)) typeEventSystem.Send<EventA>();
            if (Input.GetMouseButtonDown(1)) typeEventSystem.Send<EventB>(new EventB {ParamB = 123});
            if (Input.GetKeyDown(KeyCode.Space))
            {
                typeEventSystem.Send<IEventGroup>(new EventC());
                typeEventSystem.Send<IEventGroup>(new EventD());
            }
        }
        private void OnEventA(EventA obj) => Debug.Log("OnEventA");
        private void OnEventB(EventB obj) => Debug.Log($"OnEventB {obj.ParamB}");
        private void OnDestroy() => typeEventSystem.Cancel<EventA>(OnEventA);
    }
}