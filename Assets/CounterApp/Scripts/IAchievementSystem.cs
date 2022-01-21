using QFramework;
using UnityEngine;
namespace CounterApp
{
    public interface IAchievementSystem : ISystem
    {
    }
    public class AchievementSystem : AbstractSystem, IAchievementSystem
    {
        protected override void OnInit()
        {
            var counterModel = this.GetModel<ICounterModel>();
            var previousCount = counterModel.Count.Value;
            bool count9Unlock = false;
            // 将bool值存储
            // var storage = Architecture.GetUtility<IStorage>();
            // storage.SaveInt();
            bool count18Unlock = false;
            counterModel.Count.Register(newCount =>
            {
                if (previousCount < 9 && newCount >= 9 && !count9Unlock)
                {
                    count9Unlock = true;
                    Debug.Log("解锁：点击10次成就");
                } else if (previousCount < 18 && newCount >= 18 && count9Unlock && !count18Unlock)
                {
                    count18Unlock = true;
                    Debug.Log("解锁：点击18次成就");
                }
                previousCount = newCount;
            });
        }
    }
}