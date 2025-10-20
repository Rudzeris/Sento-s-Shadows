using UnityEngine;

namespace Assets.Scripts.Features.MiniGames
{
    public class ButtonClickMiniGame : IMiniGame
    {
        public void OnFail()
        {
            throw new System.NotImplementedException();
        }

        public void OnSuccess()
        {
            throw new System.NotImplementedException();
        }

        public void StartGame()
        {
            Debug.Log("StartGame");
        }
    }
}
