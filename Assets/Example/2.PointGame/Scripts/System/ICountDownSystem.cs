using System;
namespace QFramework.Example
{
    public interface ICountDownSystem : ISystem
    {
        int CurrentRemainSeconds { get; }
        void Update();
    }
    public class CountDownSystem : AbstractSystem, ICountDownSystem
    {
        private DateTime mGameStartTime { get; set; }
        private bool mStarted;
        public int CurrentRemainSeconds => 10 - (int)(DateTime.Now - mGameStartTime).TotalSeconds;
        protected override void OnInit()
        {
            this.RegisterEvent<OnGameStartEvent>(e =>
            {
                mStarted = true;
                mGameStartTime = DateTime.Now;
            });
            this.RegisterEvent<OnGamePassEvent>(e => mStarted = false);
        }
        public void Update()
        {
            if (mStarted)
            {
                if (DateTime.Now - mGameStartTime > TimeSpan.FromSeconds(10))
                {
                    this.SendEvent<OnCountDownEndEvent>();
                    mStarted = false;
                }
            }
        }
    }
}