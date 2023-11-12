using System;
using DanPie.Framework.DMath;
using UnityEngine;
using Zenject;
using RiverGenerationSettings = FlatVillage.Generators.RiversGenerationOperation.RiverGenerationSettings;
using Random = System.Random;
using DanPie.Framework.Extensions;
using static FlatVillage.Generators.BiomeGenerationOperation;
using FlatVillage.Maps;
using FlatVillage.Resources;

namespace FlatVillage.Generators
{
    [Serializable]
    public class BalancedMapGenerator : MonoBehaviour, IMapGenerator
    {
        [Header("GenerationSettings: ")]
        [SerializeField] private bool _randomiseSeed = false;
        [SerializeField] private int _seed = 2288;
        [Tooltip("Defines the multiplication factor for each generator value, including field size.")]
        [SerializeField] private float _scale = 1f;
        [SerializeField] private Vector2Int _mapSize = new Vector2Int(20, 15);
        [SerializeField] private RiverGenerationSettings _riverGenerationSettings;
        [SerializeField] private BiomeGenerationSettings _mountainsGenerationSettings;
        [SerializeField] private BiomeGenerationSettings _forestGenerationSettings;

        [Header("References to resources: ")]
        [SerializeField] private ItemInfo _waterItemInfo;
        [SerializeField] private ItemInfo _sandItemInfo;
        [SerializeField] private ItemInfo _meadowItemInfo;
        [SerializeField] private ItemInfo _forestItemInfo;
        [SerializeField] private ItemInfo _mountainItemInfo;

        private int _waterID;
        private int _sandID;
        private int _meadowID;
        private int _forestID;
        private int _mountainID;
        private Random _rand = new Random();
        private MatrixRepresentationInt _currentMap;
        private IIndexableObjectsCollection<TileInfo> _baseMapTilesInfo;
        private TilesInfoCollection _tilesInfoCollection;

        public MatrixRepresentationInt CurrentMap { get => _currentMap; }

        [Inject]
        public void Construct(BasicMap baseMap, TilesInfoCollection tilesInfoCollection)
        {
            _baseMapTilesInfo = baseMap.ObjectsInfo;
            _tilesInfoCollection = tilesInfoCollection;
            _sandID = _tilesInfoCollection.GetByItemInfo(_sandItemInfo).ID;
            _waterID = _tilesInfoCollection.GetByItemInfo(_waterItemInfo).ID;
            _meadowID = _tilesInfoCollection.GetByItemInfo(_meadowItemInfo).ID;
            _forestID = _tilesInfoCollection.GetByItemInfo(_forestItemInfo).ID;
            _mountainID = _tilesInfoCollection.GetByItemInfo(_mountainItemInfo).ID;
        }

        public GenerationOperations Generate(out MatrixRepresentationInt map)
        {
            InitializeGenerationSettings();

            var generationOperations = new GenerationOperations(
                new Stage(
                    new FillOperation(_currentMap, _meadowID),
                    "Filling the map with meadow tiles."),
                new Stage(
                    new RiversGenerationOperation(_currentMap, new int[] { _meadowID }, _waterID, _riverGenerationSettings),
                    "Generating rivers."),
                new Stage(
                    new BiomeGenerationOperation(_currentMap, _mountainID, new int[] { _waterID }, _mountainsGenerationSettings),
                    "Generating mountains."),
                new Stage(
                    new BiomeGenerationOperation(_currentMap, _forestID, new int[] { _waterID, _mountainID }, _forestGenerationSettings),
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
