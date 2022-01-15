using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Task = System.Threading.Tasks.Task;
namespace FrameworkDesign.Example
{
    public interface IAchievementSystem : ISystem
    {
    }
    public class AchievementItem
    {
        public string Name { get; set; }
        public Func<bool> CheckComplete { get; set; }
        public bool Unlocked { get; set; }
    }
    public class AchievementSystem : AbstractSystem, IAchievementSystem
    {
        private bool mMissed;
        private List<AchievementItem> mAchievementItems = new List<AchievementItem>();
        protected override void OnInit()
        {
            this.RegisterEvent<OnMissEvent>(OnMissEvent =>
            {
                mMissed = true;
            });
            this.RegisterEvent<OnGameStartEvent>(GameStartEvent =>
            {
                mMissed = false;
            });
            mAchievementItems.Add(new AchievementItem()
            {
                Name = "高分",
                CheckComplete = () => this.GetModel<IGameModel>().BestScore.Value >= 90
            });
            mAchievementItems.Add(new AchievementItem()
            {
                Name = "不到0",
                CheckComplete = () => this.GetModel<IGameModel>().Score.Value <= 0
            });
            mAchievementItems.Add(new AchievementItem()
            {
                Name = "无失误",
                CheckComplete = () => !mMissed
            });
            mAchievementItems.Add(new AchievementItem()
            {
                Name = "全满成就",
                CheckComplete = () => mAchievementItems.Count(item => item.Unlocked) >= 3
            });
            //成就系统一般是持久化的，所以如果需要持久化，也是在这个时机进行，可以让Unlocked变成BindableProperty
            this.RegisterEvent<OnGamePassEvent>(async e =>
            {
                await Task.Delay(TimeSpan.FromSeconds(0.1f));
                foreach(var achievementItem in mAchievementItems)
                {
                    if (!achievementItem.Unlocked && achievementItem.CheckComplete())
                    {
                        achievementItem.Unlocked = true;
                        Debug.Log($"解锁成就：{achievementItem.Name}");
                    }
                }
            });
        }
    }
}