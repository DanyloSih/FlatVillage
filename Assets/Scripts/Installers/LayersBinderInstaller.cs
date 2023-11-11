using FlatVillage.Gameplay;
using Zenject;

namespace FlatVillage.Installers
{
    public class LayersBinderInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<LayersBinder>().To<LayersBinder>().AsSingle();
        }
    }
}