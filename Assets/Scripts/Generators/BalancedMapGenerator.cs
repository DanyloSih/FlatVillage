using System;
using DanPie.Framework.DMath;
using UnityEngine;
using Zenject;
using RiverGenerationSettings = FlatVillage.Generators.RiversGenerationOperation.RiverGenerationSettings;
using Random = System.Random;
using DanPie.Framework.Extensions;
using static FlatVillage.Generators.BiomeGenerationOperation;
using FlatVillage.Maps;

namespace FlatVillage.Generators
{
    [Serializable]
    public class BalancedMapGenerator : MonoBehaviour, IMapGenerator
    {
        [SerializeField] private bool _randomiseSeed = false;
        [SerializeField] private int _seed = 2288;
        [Tooltip("Defines the multiplication factor for each generator value, including field size.")]
        [SerializeField] private float _scale = 1f;
        [SerializeField] private Vector2Int _mapSize = new Vector2Int(20, 15);
        [SerializeField] private RiverGenerationSettings _riverGenerationSettings;
        [SerializeField] private BiomeGenerationSettings _mountainsGenerationSettings;
        [SerializeField] private BiomeGenerationSettings _forestGenerationSettings;

        private Random _rand = new Random();
        private MatrixRepresentationInt _currentMap;
        private ObjectsInfoCollection<TileInfo> _baseMapTilesInfo;
        private ObjectInfo _water;
        private ObjectInfo _sand;
        private ObjectInfo _meadow;
        private ObjectInfo _forest;
        private ObjectInfo _mountain;

        public MatrixRepresentationInt CurrentMap { get => _currentMap; }

        [Inject]
        public void Construct(BasicMap baseMap)
        {
            _baseMapTilesInfo = baseMap.ObjectsInfo;
            _sand = _baseMapTilesInfo.GetTileInfoByName("sand");
            _water = _baseMapTilesInfo.GetTileInfoByName("water");
            _meadow = _baseMapTilesInfo.GetTileInfoByName("meadow");
            _forest = _baseMapTilesInfo.GetTileInfoByName("forest");
            _mountain = _baseMapTilesInfo.GetTileInfoByName("mountain");
        }

        public GenerationOperations Generate(out MatrixRepresentationInt map)
        {
            InitializeGenerationSettings();

            var generationOperations = new GenerationOperations(
                new Stage(
                    new FillOperation(_currentMap, _meadow.ID),
                    "Filling the map with meadow tiles."),
                new Stage(
                    new RiversGenerationOperation(_currentMap, new int[] { _meadow.ID }, _water.ID, _riverGenerationSettings),
                    "Generating rivers."),
                new Stage(
                    new BiomeGenerationOperation(_currentMap, _mountain.ID, new int[] { _water.ID }, _mountainsGenerationSettings),
                    "Generating mountains."),
                new Stage(
                    new BiomeGenerationOperation(_currentMap, _forest.ID, new int[] { _water.ID, _mountain.ID }, _forestGenerationSettings),
                    "Generating forests.")
                );

            map = _currentMap;
            return generationOperations;
        }

        private void InitializeGenerationSettings()
        {
            if (_randomiseSeed)
            {
                _seed = new Random().NextInt();
            }
            _rand = new Random(_seed);
            
            _currentMap = new MatrixRepresentationInt(
                (int)(_mapSize.x * Math.Sqrt(_scale)), (int)(_mapSize.y * Mathf.Sqrt(_scale)));

            _riverGenerationSettings.Seed = _rand.NextInt();
            _riverGenerationSettings.Scale = _scale;

            _mountainsGenerationSettings.Seed = _rand.NextInt();
            _mountainsGenerationSettings.Scale = _scale;

            _forestGenerationSettings.Seed = _rand.NextInt();
            _forestGenerationSettings.Scale = _scale;
        }
    }
}
