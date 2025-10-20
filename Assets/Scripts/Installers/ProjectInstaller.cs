using Assets.Scripts.Factories;
using Assets.Scripts.Signals;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Installers
{
    [CreateAssetMenu(menuName = "Installers/ProjectInstaller")]
    public class ProjectInstaller : ScriptableObjectInstaller<ProjectInstaller>
    {
        public override void InstallBindings()
        {
            Container.DeclareSignal<MiniGameCompletedSignal>();
            Container.BindInterfacesAndSelfTo<MiniGameFactory>().AsSingle();
        }
    }
}
