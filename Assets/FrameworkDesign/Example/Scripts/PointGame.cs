namespace FrameworkDesign.Example
{
    public class PointGame : Architecture<PointGame>
    {
        protected override void Init() { Register<IGameModel>(new GameModel()); }
    }
}