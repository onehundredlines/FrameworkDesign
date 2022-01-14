namespace FrameworkDesign.Example
{
    public class PointGame : Architecture<PointGame>
    {
        protected override void Init()
        {
            RegisterSystem<IScoreSystem>(new ScoreSystem());
            RegisterModel<IGameModel>(new GameModel());
            RegisterUtility<IStorage>(new PlayerPrefsStorage());
        }
    }
}