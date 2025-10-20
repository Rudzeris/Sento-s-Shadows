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

        private IMiniGame _miniGame;
        private MiniGameFactory _miniGameFactory;

        [Inject]
        public void Construct(MiniGameFactory miniGameFactory)
        {
            _miniGameFactory = miniGameFactory;
        }

        private void Start()
        {
            Canvas canvas = FindAnyObjectByType<Canvas>();
            _miniGame = _miniGameFactory.Create(_config.miniGameConfig, canvas.transform);
        }

        public void Interact()
        {
            _miniGame?.StartGame();
        }
    }
}
