using System;
using UnityEngine;

namespace QFramework.Example
{
    [Serializable]
    public class IntProperty : BindableProperty<int>
    {
        public IntProperty(int value = default) : base(value)
        {
        }
    }
    public class BindableProperty : MonoBehaviour
    {
        public  IntProperty Age = new IntProperty(10);
        public  IntProperty Counter = new IntProperty();
        private void Start()
        {
            Age.Register(age =>
            {
                Debug.Log($"{nameof(age)}: {age.ToString()}");
                Counter.Value = 10 * age;
            }).CancelOnDestroy(gameObject);
            Counter.RegisterWithInitValue(counter => Debug.Log($"{nameof(counter)}: {counter.ToString()}")).CancelOnDestroy(gameObject);
        }
        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Age.Value++;
            }
            if (Input.GetMouseButtonDown(1))
            {
                Age.Value--;
            }
        }
    }
}