using FlatVillage.Generators;
using FlatVillage.Maps;
using UnityEngine;
using UnityEngine.Tilemaps;
using Zenject;

namespace FlatVillage.Installers
{
    public class BaseMapInstaller : MonoInstaller
    {
        [SerializeField] private TilesInfoCollection _tilesInfo;
        [SerializeField] private Tilemap _tilemap;

        public override void InstallBindings()
        {
            Container.Bind(typeof(IInitializable)).FromInstance(_tilesInfo).AsSingle();
            var mapApplier = new IntMapToTilemapApplier(_tilemap, _tilesInfo);
            Container.Bind(typeof(BasicMap)).FromInstance(new BasicMap(_tilesInfo, _tilemap, mapApplier)).AsSingle();
        }
    }
}