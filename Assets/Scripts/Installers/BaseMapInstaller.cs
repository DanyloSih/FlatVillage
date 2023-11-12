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
            var mapApplier = new BaseMapTilesUpdater(_tilemap, _tilesInfo);
            var pointsConverter = new TilemapMatrixPointsViaOffsetConverter();

            Container.Bind<TilesInfoCollection>().FromInstance(_tilesInfo);
            Container.Bind(typeof(BasicMap)).FromInstance(
                new BasicMap(_tilesInfo, _tilemap, mapApplier, pointsConverter)).AsSingle();
        }
    }
}