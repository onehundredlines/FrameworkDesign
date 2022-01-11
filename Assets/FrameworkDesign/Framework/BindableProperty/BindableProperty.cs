using System;

namespace FrameworkDesign
{
    /// <summary>
    /// 数据 + 数据变更事件，节省代码量
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BindableProperty<T> where T : IEquatable<T>
    {
        private T mValue = default(T);
        public T Value {
            get => mValue;
            set {
                if (!value.Equals(mValue))
                {
                    mValue = value;
                    OnValueChanged?.Invoke(value);
                }
            }
        }
        public Action<T> OnValueChanged;
    }
}