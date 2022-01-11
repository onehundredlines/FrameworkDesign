using UnityEngine;
using UnityEngine.UI;

namespace CounterApp
{
    public class CounterViewController : MonoBehaviour
    {
        private void Start()
        {
            UpdateView();
            transform.Find("ButtonAdd").GetComponent<Button>().onClick.AddListener(() =>
            {
                CounterModel.Count++;
                UpdateView();
            }); 
            transform.Find("ButtonSub").GetComponent<Button>().onClick.AddListener(() =>
            {
                CounterModel.Count--;
                UpdateView();
            });
        }
        private void UpdateView()
        {
            transform.Find("TextCount").GetComponent<Text>().text = CounterModel.Count.ToString();
        }
    }
    public static class CounterModel
    {
        public static int Count;
    }
}