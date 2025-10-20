using Assets.Scripts.Configs;
using Assets.Scripts.Factories;
using Assets.Scripts.Features.MiniGames;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Features.Sctructures
{
    public class Structure : MonoBehaviour
    {

        [SerializeField] private StructureConfig _config;

        [Inject] private MiniGameFactory _gameFactory;
        private IMiniGame _miniGame;

        private void Start()
        {
            _miniGame = _gameFactory.Create(_config.miniGameConfig);
        }

        public void Interact()
        {
            _miniGame?.StartGame();
        }
    }
}
