namespace FrameworkDesign.Example
{
    public struct KillEnemyCommand : ICommand
    {
        public void Execute()
        {
            GameModel.KillCount.Value++;
            if (GameModel.KillCount.Value >= 10)
            {
                GamePassEvent.Trigger();
            }
        }
    }
}