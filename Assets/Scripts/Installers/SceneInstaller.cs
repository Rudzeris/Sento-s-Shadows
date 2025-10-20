using Assets.Scripts.Features.Input;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Installers
{
    public class SceneInstaller : MonoInstaller
    {
        [SerializeField] private PlayerInputHandler _inputHandler;
        public override void InstallBindings()
        {
            Container.Bind<IInputHandler>().To<PlayerInputHandler>().FromInstance(_inputHandler).AsSingle();
        }
    }
}
