using Assets.Scripts.Configs;
using Assets.Scripts.Features.MiniGames;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Factories
{
    public class MiniGameFactory
    {
        private readonly DiContainer _container;

        public MiniGameFactory(DiContainer container)
        {
            _container = container;
        }

        public IMiniGame Create(MiniGameConfig config, Transform parent = null)
        {
            if (config == null || config.prefab == null)
            {
                Debug.LogError("MiniGameConfig или Prefab не заданы");
                return null;
            }

            return _container.InstantiatePrefabForComponent<IMiniGame>(config.prefab, parent);
        }
    }
}
