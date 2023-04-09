using FlatVillage.Gameplay;
using UnityEngine;
using Zenject;

namespace FlatVillage.Installers
{
    public class TileActionsInstaller : MonoInstaller
    {
        [SerializeField] private int _setTileID = 3;

        public override void InstallBindings()
        {
            Container.Bind<ITileAction>().To<PlaceTileAction>().AsTransient().WithArguments(_setTileID);
        }
    }
}