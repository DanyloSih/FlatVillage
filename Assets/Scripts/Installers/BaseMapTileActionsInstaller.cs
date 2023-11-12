using System.Collections.Generic;
using FlatVillage.Gameplay;
using FlatVillage.Maps;
using SimpleHeirs;
using UnityEngine;
using Zenject;

namespace FlatVillage.Installers
{
    public class BaseMapTileActionsInstaller : MonoInstaller
    {
        [SerializeField] private List<HeirsProvider<ITileAction<int, TileInfo>>> _tileActions;

        public override void InstallBindings()
        {
            foreach (var tileAction in _tileActions)
            {
                var value = tileAction.GetValue();
                Container.Inject(value);

                Container
                    .Bind<ITileAction<int, TileInfo>>()
                    .FromInstance(value)
                    .AsTransient();
            }
        }
    }
}