namespace FrameworkDesign.Example
{
    public class BuyLifeCommand : AbstractCommand
    {
        protected override void OnExecute()
        {
            var gameMode = this.GetModel<IGameModel>();
            gameMode.Gold.Value--;
            gameMode.Life.Value++;
        }
    }
}