namespace FrameworkDesign.Example
{
    public struct StartGameCommand : ICommand
    {
        public void Execute() { GameStartEvent.Trigger(); }
    }
}