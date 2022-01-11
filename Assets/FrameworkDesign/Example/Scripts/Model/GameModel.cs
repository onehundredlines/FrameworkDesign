using UnityEngine;
namespace FrameworkDesign.Example
{
    public class GameModel : MonoBehaviour
    {
        public static BindableProperty<int> KillCount = new BindableProperty<int>();
        public static BindableProperty<int> Gold = new BindableProperty<int>();
        public static BindableProperty<int> Score = new BindableProperty<int>();
        public static BindableProperty<int> HighScore = new BindableProperty<int>();
    }
}