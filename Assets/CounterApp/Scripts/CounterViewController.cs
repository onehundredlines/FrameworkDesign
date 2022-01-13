using FrameworkDesign;
using UnityEngine;
using UnityEngine.UI;

namespace CounterApp
{
    public class CounterViewController : MonoBehaviour, IController
    {
        private ICounterModel mCounterModel;
        //获取
        private void Awake() => mCounterModel = this.GetModel<ICounterModel>();
        //注册
        private void OnEnable() => mCounterModel.Count.OnValueChanged += OnCountChanged;
        private void Start()
        {
            //交互逻辑
            transform.Find("ButtonAdd").GetComponent<Button>().onClick.AddListener(() => this.SendCommand<AddCountCommand>());
            //交互逻辑
            transform.Find("ButtonSub").GetComponent<Button>().onClick.AddListener(() => this.SendCommand<SubCountCommand>());
            OnCountChanged(mCounterModel.Count.Value);
        }
        private void OnDestroy()
        {
            //注销
            mCounterModel.Count.OnValueChanged -= OnCountChanged;
            //回收
            mCounterModel = null;
        }
        //表现逻辑
        private void OnCountChanged(int newValue) => transform.Find("TextCount").GetComponent<Text>().text = newValue.ToString();
        IArchitecture IBelongToArchitecture.GetArchitecture() => CounterApp.Interface;
    }

    public interface ICounterModel : IModel
    {
        BindableProperty<int> Count { get; }
    }
    /// <summary>
    /// 不需要是静态的
    /// 不需要是单例
    /// </summary>
    public class CounterModel : AbstractModel, ICounterModel
    {
        public BindableProperty<int> Count { get; } = new BindableProperty<int> {Value = 0};
        protected override void OnInit()
        {
            var storage = this.GetUtility<IStorage>();
            Count.Value = storage.LoadInt("COUNTER_COUNT");
            Count.OnValueChanged += i => storage.SaveInt("COUNTER_COUNT", i);
        }
    }
}