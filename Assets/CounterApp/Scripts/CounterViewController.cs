using FrameworkDesign;
using UnityEngine;
using UnityEngine.UI;

namespace CounterApp
{
    public class CounterViewController : MonoBehaviour
    {
        private void OnEnable()
        {
            CounterModel.Count.OnValueChanged += OnCountChanged;
            OnCountChanged(CounterModel.Count.Value);
        }
        private void Start()
        {
            transform.Find("ButtonAdd").GetComponent<Button>().onClick.AddListener(() =>
            {
                CounterModel.Count.Value++;
            });
            transform.Find("ButtonSub").GetComponent<Button>().onClick.AddListener(() =>
            {
                CounterModel.Count.Value--;
            });
        }
        private void OnDestroy()
        {
            CounterModel.Count.OnValueChanged -= OnCountChanged;
        }
        private void OnCountChanged(int newValue)
        {
            transform.Find("TextCount").GetComponent<Text>().text = newValue.ToString();
        }
    }

    public static class CounterModel
    {
        public static BindableProperty<int> Count = new BindableProperty<int>();
    }
}