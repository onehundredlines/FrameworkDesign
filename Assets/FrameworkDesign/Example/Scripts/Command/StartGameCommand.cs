namespace FrameworkDesign.Example
{
    public class StartGameCommand : AbstractCommand
    {
        /// <summary>
        /// 在OnExecute中写真正的逻辑
        /// </summary>
        protected override void OnExecute()
        {
            //游戏有再来一次的功能，需要重置数据
            var gameModel = this.GetModel<IGameModel>();
            gameModel.KillCount.Value = 0;
            gameModel.Score.Value = 0;
            this.SendEvent<OnGameStartEvent>();
        }
    }
}