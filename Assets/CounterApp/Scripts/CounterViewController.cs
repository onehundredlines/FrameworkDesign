using FrameworkDesign;
using UnityEngine;
using UnityEngine.UI;

namespace CounterApp
{
    public class CounterViewController : MonoBehaviour, IController
    {
        private ICounterModel mCounterModel;
        private void Awake()
        {
            //获取
            mCounterModel = GetArchitecture().GetModel<ICounterModel>();
        }
        private void OnEnable()
        {
            //注册
            mCounterModel.Count.OnValueChanged += OnCountChanged;
        }
        private void Start()
        {
            transform.Find("ButtonAdd").GetComponent<Button>().onClick.AddListener(() =>
            {
                //交互逻辑
                new AddCountCommand().Execute();
            });
            transform.Find("ButtonSub").GetComponent<Button>().onClick.AddListener(() =>
            {
                //交互逻辑
                new SubCountCommand().Execute();
            });
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
        private void OnCountChanged(int newValue) { transform.Find("TextCount").GetComponent<Text>().text = newValue.ToString(); }
        public IArchitecture GetArchitecture()
        {
            return CounterApp.Interface;
        }
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
            var storage = GetArchitecture().GetUtility<IStorage>();
            Count.Value = storage.LoadInt("COUNTER_COUNT");
            Count.OnValueChanged += i => storage.SaveInt("COUNTER_COUNT", i);
        }
    }
}