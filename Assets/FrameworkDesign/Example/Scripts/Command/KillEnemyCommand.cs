namespace FrameworkDesign.Example
{
    public class KillEnemyCommand : AbstractCommand
    {
        /// <summary>
        /// 在OnExecute中写真正的逻辑
        /// </summary>
        protected override void OnExecute()
        {
            var gameModel = this.GetModel<IGameModel>();
            gameModel.KillCount.Value++;
            if (gameModel.KillCount.Value >= 9) GamePassEvent.Trigger();
        }
    }
}