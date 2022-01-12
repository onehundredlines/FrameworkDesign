namespace FrameworkDesign.Example
{
    public struct KillEnemyCommand : ICommand
    {
        public void Execute()
        {
            GameModel.Instance.KillCount.Value++;
            if (GameModel.Instance.KillCount.Value >= 10)
            {
                GamePassEvent.Trigger();
            }
        }
    }
}