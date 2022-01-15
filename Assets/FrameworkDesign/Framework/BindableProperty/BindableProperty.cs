using System;
namespace FrameworkDesign
{
    /// <summary>
    /// 数据 + 数据变更事件，节省代码量
    /// </summary>
    public class BindableProperty<T> where T : IEquatable<T>
    {
        private T mValue = default(T);
        public T Value {
            get => mValue;
            set {
                if (!value.Equals(mValue))
                {
                    mValue = value;
                    mOnValueChanged?.Invoke(value);
                }
            }
        }
        private Action<T> mOnValueChanged = v => { };
        public ICancel RegisterOnValueChanged(Action<T> onValueChanged)
        {
            mOnValueChanged += onValueChanged;
            return new BindablePropertyCancel<T>()
            {
                BindableProperty = this,
                OnValueChanged = onValueChanged
            };
        }
        public void CancelOnValueChanged(Action<T> onValueChanged) => mOnValueChanged -= onValueChanged;
    }
    public class BindablePropertyCancel<T> : ICancel where T : IEquatable<T>
    {
        public BindableProperty<T> BindableProperty { get; set; }
        public Action<T> OnValueChanged { get; set; }
        public void Cancel()
        {
            BindableProperty.CancelOnValueChanged(OnValueChanged);
            BindableProperty = null;
            OnValueChanged = null;
        }
    }
}