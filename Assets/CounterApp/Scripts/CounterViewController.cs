using FrameworkDesign;
using UnityEngine;
using UnityEngine.UI;

namespace CounterApp
{
    public class CounterViewController : MonoBehaviour
    {
        private CounterModel mCounterModel;
        private void Awake()
        {
            mCounterModel = CounterApp.Get<CounterModel>();
        }
        private void OnEnable()
        {
            mCounterModel.Count.OnValueChanged += OnCountChanged;
            OnCountChanged(mCounterModel.Count.Value);
        }
        private void Start()
        {
            transform.Find("ButtonAdd").GetComponent<Button>().onClick.AddListener(() =>
            {
                new AddCountCommand().Execute();
            });
            transform.Find("ButtonSub").GetComponent<Button>().onClick.AddListener(() =>
            {
                new SubCountCommand().Execute();
            });
        }
        private void OnDestroy()
        {
            mCounterModel.Count.OnValueChanged -= OnCountChanged;
            mCounterModel = null;
        }
        private void OnCountChanged(int newValue) { transform.Find("TextCount").GetComponent<Text>().text = newValue.ToString(); }
    }

    public class CounterModel
    {
        public readonly BindableProperty<int> Count = new BindableProperty<int>() {Value = 0};
    }
}