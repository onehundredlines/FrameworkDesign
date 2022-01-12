namespace FrameworkDesign.Example
{
    public struct KillEnemyCommand : ICommand
    {
        public void Execute()
        {
            var gameModel = PointGame.Get<GameModel>();
            gameModel.KillCount.Value++;
            if (gameModel.KillCount.Value >= 10)
            {
                GamePassEvent.Trigger();
            }
        }
    }
}