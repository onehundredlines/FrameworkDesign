using FrameworkDesign;
using UnityEngine;
using UnityEngine.UI;

namespace CounterApp
{
    public class CounterViewController : MonoBehaviour
    {
        private void OnEnable()
        {
            // 与其他方法交互之一：方法调用
            // UpdateView();
            //与其他方法交互之三：事件
            // OnCountChangedEvent.RegisterEvent(OnCountChanged);
            //与其他方法交互之二：委托回调
            // CounterModel.OnCountChanged += OnCountChanged;
            //与其他方法交互之二：委托回调
            // OnCountChanged(CounterModel.Count);
            CounterModel.Count.OnValueChanged += OnCountChanged;
            OnCountChanged(CounterModel.Count.Value);
        }
        private void Start()
        {
            transform.Find("ButtonAdd").GetComponent<Button>().onClick.AddListener(() =>
            {
                // CounterModel.Count++;
                // 与其他方法交互之一：方法调用
                // UpdateView();
                CounterModel.Count.Value++;
            });
            transform.Find("ButtonSub").GetComponent<Button>().onClick.AddListener(() =>
            {
                CounterModel.Count.Value--;
                // 与其他方法交互之一：方法调用
                // UpdateView();
            });
        }
        private void OnDestroy()
        {
            //与其他方法交互之二：委托回调
            // CounterModel.OnCountChanged -= OnCountChanged;
            //与其他方法交互之三：事件
            // OnCountChangedEvent.UnRegisterEvent(OnCountChanged);
            CounterModel.Count.OnValueChanged -= OnCountChanged;
        }
        private void OnCountChanged(int newValue)
        {
            // transform.Find("TextCount").GetComponent<Text>().text =  CounterModel.Count.ToString();
            transform.Find("TextCount").GetComponent<Text>().text = newValue.ToString();
        }
        /// <summary>
        /// 与其他方法交互之一：方法调用
        /// </summary>
        // private void UpdateView()
        // {
        //     transform.Find("TextCount").GetComponent<Text>().text = CounterModel.Count.ToString();
        // }
    }

    public static class CounterModel
    {
        // private static int count;
        //与其他方法交互之二：委托回调
        // public static Action<int> OnCountChanged;
        // public static int Count {
        //     get => count;
        //     set {
        //         if (count != value)
        //         {
        //             count = value;
        //             OnCountChangedEvent.Trigger();
        //             //与其他方法交互之二：委托回调
        //             // OnCountChanged?.Invoke(value);
        //         }
        //     }
        // }
        public static BindableProperty<int> Count = new BindableProperty<int>();
    }

    //与其他方法交互之三：事件
    // public class OnCountChangedEvent : Event<OnCountChangedEvent>
    // {
    //
    // }
}