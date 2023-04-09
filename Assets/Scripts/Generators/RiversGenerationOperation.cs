using System;
using System.Collections.Generic;
using System.Linq;
using DanPie.Framework.DMath;
using DanPie.Framework.Extensions;
using DanPie.Framework.Randomnicity;
using UnityEngine;

namespace FlatVillage.Generators
{
    public class RiversGenerationOperation : GenerationOperation
    {
        [Serializable]
        public class RiverGenerationSettings : RandUser
        {
            [Min(0)]
            [SerializeField] private Vector2Int _minMaxSourcesCount;
            [Min(0)]
            [SerializeField] private Vector2 _minMaxMutationForce;
            [Min(0)]
            [SerializeField] private int _mutationStep;

            public int SourcesCount { get => (int)(Rand.NextInt(_minMaxSourcesCount) * Scale); }
            public Vector2 MinMaxMutationForce { get => _minMaxMutationForce * Scale; }
            public int MutationStep { get => (int)(_mutationStep * Scale); }
            public float Scale { get; set; } = 1.0f;

            public RiverGenerationSettings(
                Vector2Int minMaxSourcesCount,
                Vector2 minMaxMutationForce,
                int mutationStep)
            {
                _minMaxSourcesCount = minMaxSourcesCount;
                _minMaxMutationForce = minMaxMutationForce;
                _mutationStep = mutationStep;
            }
        }

        private MatrixRepresentationInt _map;
        private int _sourcesCount;
        private int[] _availableToSourceSpawnTilesID;
        private int _waterTileID;
        private Vector2 _minMaxMutationForce;
        private int _mutationStep;
        private List<Vector2Int> _allAvailablePoints;

        public RiversGenerationOperation(
            MatrixRepresentationInt matrixRepresentation,
            int[] availableToSourceSpawnTilesID,
            int waterTileID,
            RiverGenerationSettings settings)
        {
            _map = matrixRepresentation;
            _sourcesCount = settings.SourcesCount;
            _availableToSourceSpawnTilesID = availableToSourceSpawnTilesID;
            _waterTileID = waterTileID;
            _minMaxMutationForce = settings.MinMaxMutationForce;
            _mutationStep = settings.MutationStep;
            Seed = settings.Seed;
        }

        protected override void OnOperationStarted()
        {
            _allAvailablePoints 
                = GetAllPointsByCondition(_map, (m, id) => _availableToSourceSpawnTilesID.Contains(m[id]));
        }

        protected override bool OnOperationTick(long ticks)
        {
            GenerateRiver();
            return ticks >= _sourcesCount - 1;
        }

        private void GenerateRiver()
        {
            Vector2Int currentPosition;
            Vector2 direction;
            int steps = 1;
            try
            {
                currentPosition = PopRandomPoint(_allAvailablePoints);
            }
            catch (ArgumentException)
            {
                return;
            }

            direction = GetRandomDirection();
            currentPosition += ToVector2Int(direction);
            while (!StopRiverCondition(currentPosition))
            {
                _map.SetData(currentPosition, _waterTileID);
                if(steps % _mutationStep == 0)
                {
                    direction = MutateDirection(direction);
                }
                currentPosition += ToVector2Int(direction);
                steps++;
            }
        }

        private Vector2 MutateDirection(Vector2 previousDirection)
        {
            float minForce = _minMaxMutationForce.x;
            float halphForce = _minMaxMutationForce.y / 2f;
            return (
                previousDirection +
                new Vector2(Random.NextFloat(-halphForce, halphForce),
                            Random.NextFloat(-halphForce, halphForce)) +
                new Vector2(Random.NextSign() * minForce, Random.NextSign() * minForce)).normalized;
        }

        private bool StopRiverCondition(Vector2Int currentPosition)
        {
            int id;
            try
            {
                id = _map.GetData(currentPosition);
            }
            catch (IndexOutOfRangeException)
            {
                return true;
            }

            return id == _waterTileID;
        }
    }
}
