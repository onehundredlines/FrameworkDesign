using CounterApp;
namespace FrameworkDesign.Example
{
    public class PointGame : Architecture<PointGame>
    {
        protected override void Init()
        {
            RegisterSystem<IAchievementSystem>(new AchievementSystem());
            RegisterSystem<ICountDownSystem>(new CountDownSystem());
            RegisterSystem<IScoreSystem>(new ScoreSystem());
            RegisterModel<IGameModel>(new GameModel());
            RegisterUtility<IStorage>(new PlayerPrefsStorage());
        }
    }
}