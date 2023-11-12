using FlatVillage.Gameplay;
using Zenject;

namespace FlatVillage.Installers
{
    public class LayersBinderInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<LayersTilesProvider>().To<LayersTilesProvider>().AsSingle();
        }
    }
}