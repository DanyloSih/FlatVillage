using System;
using System.Collections.Generic;
using System.Linq;
using DanPie.Framework.DMath;
using DanPie.Framework.Extensions;
using DanPie.Framework.Randomnicity;
using UnityEngine;

namespace FlatVillage.Generators
{
    public class BiomeGenerationOperation : GenerationOperation
    {
        [Serializable]
        public class BiomeGenerationSettings : RandUser
        {
            [SerializeField] private Vector2Int _minMaxNumberOfRidges;
            [SerializeField] private Vector2Int _minMaxBranchLength;
            [SerializeField] private Vector2Int _minMaxQuantityInRidge;
            [SerializeField] private Vector2 _minMaxNoiseScale;

            public int BranchLength { get => (int)(Rand.NextInt(_minMaxBranchLength) * Scale); }
            public int QuantityInRidge { get => (int)(Rand.NextInt(_minMaxQuantityInRidge) * Scale); }
            public int NumberOfRidges { get => (int)(Rand.NextInt(_minMaxNumberOfRidges) * Scale); }
            public float NoiseScale { get => Rand.NextFloat(_minMaxNoiseScale) * Scale; }
            public float Scale { get; set; } = 1.0f;

            public BiomeGenerationSettings(
                Vector2Int minMaxBranchLength,
                Vector2Int minMaxQuantityInRidge,
                Vector2Int minMaxNumberOfRidges,
                Vector2 minMaxNoiseScale)
            {
                _minMaxBranchLength = minMaxBranchLength;
                _minMaxQuantityInRidge = minMaxQuantityInRidge;
                _minMaxNumberOfRidges = minMaxNumberOfRidges;
                _minMaxNoiseScale = minMaxNoiseScale;
            }
        }

        private MatrixRepresentationInt _map;
        private int _biomeTileID;
        private int[] _ignoredTilesID;
        private int _branchLength;
        private int _quantityInRidge;
        private int _numberOfRidges;
        private BiomeGenerationSettings _settings;
        private PerlinNoiseMatrixGenerator _noiseGenerator;
        private List<Vector2Int> _availablePointsToSpawn;

        public BiomeGenerationOperation(
            MatrixRepresentationInt map,
            int biomeTileID,
            int[] ignoredTilesID,
            BiomeGenerationSettings settings)
        {
            _settings = settings;
            _map = map;
            _biomeTileID = biomeTileID;
            _ignoredTilesID = ignoredTilesID;
            _branchLength = settings.BranchLength;
            _quantityInRidge = settings.QuantityInRidge;
            _numberOfRidges = settings.NumberOfRidges;
            Seed = settings.Seed;
        }

        protected override void OnSeedChanged(int seed)
        {
            _noiseGenerator = new PerlinNoiseMatrixGenerator(_map.Size, _settings.NoiseScale, seed: seed);
        }

        protected override void OnOperationStarted()
        {
            _availablePointsToSpawn
                = GetAllPointsByCondition(_map, (m, i) => !_ignoredTilesID.Contains(m[i]));
        }

        protected override bool OnOperationTick(long ticks)
        {
            GenerateMountainsRidge();
            return ticks >= _numberOfRidges - 1;
        }

        private void GenerateMountainsRidge()
        {
            Vector2Int spawnPoint;
            try
            {
                spawnPoint = PopRandomPoint(_availablePointsToSpawn);
            }
            catch (ArgumentException)
            {
                return;
            }
            MatrixRepresentationFloat noiseMap = _noiseGenerator.GenerateNoise();

            List<Vector2Int> mountainsPosition = new List<Vector2Int> { spawnPoint };
            Vector2Int currentPoint = spawnPoint;
            int branchCounter = 0;

            for (int i = 0; i < _quantityInRidge; i++)
            {
                var baseMapNeighbors = _map.GetNeighboursOfMatrixMember(currentPoint)
                    .GetAvailableNeighboursOnMainAxes();


                if (baseMapNeighbors.Count() == 0
                    || branchCounter >= _branchLength
                    || !_map.IsPointInside(currentPoint)
                    || _ignoredTilesID.Contains(_map.GetData(currentPoint)))
                {
                    branchCounter = 0;
                    currentPoint = GetAnotherPoint(mountainsPosition);
                    continue;
                }

                if (_availablePointsToSpawn.Contains(currentPoint))
                {
                    _availablePointsToSpawn.Remove(currentPoint);
                }
                _map.SetData(currentPoint, _biomeTileID);
                branchCounter++;

                MatrixMember<float>? highestNeighbor = noiseMap.GetNeighboursOfMatrixMember(currentPoint)
                    .GetAvailableNeighboursOnMainAxes().OrderByDescending(x => x.Value.Data).First();

                currentPoint = noiseMap.FromIDToVector(highestNeighbor.Value.ID);
            }
        }

        private Vector2Int GetAnotherPoint(List<Vector2Int> mountainsPosition)
        {
            Vector2Int result;
            var point = mountainsPosition[Random.Next(0, mountainsPosition.Count)];
            var direction = ToVector2Int(GetRandomDirection());
            result = point + direction;

            return result;
        }
    }
}
