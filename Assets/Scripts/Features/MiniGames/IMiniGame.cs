namespace Assets.Scripts.Features.MiniGames
{
    public interface IMiniGame
    {
        void StartGame();
        void OnSuccess();
        void OnFail();
    }
}
