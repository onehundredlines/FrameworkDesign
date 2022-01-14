namespace FrameworkDesign.Example
{
    public class StartGameCommand : AbstractCommand
    {
        /// <summary>
        /// 在OnExecute中写真正的逻辑
        /// </summary>
        protected override void OnExecute() => this.SendEvent<GameStartEvent>();
    }
}