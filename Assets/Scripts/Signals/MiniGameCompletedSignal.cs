namespace Assets.Scripts.Signals
{
    public class MiniGameCompletedSignal
    {
        public bool Success { get; }
        
        public MiniGameCompletedSignal(bool success)
        {
            Success = success;
        }
    }
}
