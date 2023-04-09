using System;
using System.Collections.Generic;
using DanPie.Framework.DMath;
using DanPie.Framework.Extensions;
using DanPie.Framework.Randomnicity;
using UnityEngine;
using Random = System.Random;

namespace FlatVillage.Generators
{
    public abstract class GenerationOperation : YieldOperation
    {
        private RandUser _randUser = new RandUser();

        public Random Random { get => _randUser.Rand; }
        public int Seed
        {
            get => _randUser.Seed;

            set
            {
                _randUser.Seed = value;
                OnSeedChanged(Seed);
            }
        }

        protected virtual void OnSeedChanged(int newSeed) { }

        protected List<Vector2Int> GetAllPointsByCondition(
            MatrixRepresentationInt map, 
            Func<MatrixRepresentationInt, int, bool> isAvailableTileCondition)
        {
            List<Vector2Int> result = new List<Vector2Int>();

            int id = 0;
            foreach (var val in map.GetEnumerable())
            {
                if (isAvailableTileCondition(map, id))
                {
                    result.Add(map.FromIDToVector(id));
                }
                id++;
            }
            return result;
        }

        protected Vector2Int PopRandomPoint(List<Vector2Int> availablePoints)
        {
            if (availablePoints.Count == 0)
            {
                throw new ArgumentException("List must contain at least one element!");
            }
            int id = Random.Next(0, availablePoints.Count);
            Vector2Int result = availablePoints[id];
            availablePoints.RemoveAt(id);
            return result;
        }

        protected Vector2 GetRandomDirection()
            => new Vector2(Random.NextFloat(-1f, 1f), Random.NextFloat(-1f, 1f)).normalized;

        protected Vector2Int ToVector2Int(Vector2 vector)
        {
            int x = Mathf.RoundToInt(vector.x);
            int y = Mathf.RoundToInt(vector.y);

            if (x != 0 && y != 0)
            {
                if (Random.NextFloat(0f, 1f) < 0.5f)
                {
                    x = 0;
                }
                else
                {
                    y = 0;
                }
            }

            return new Vector2Int(x, y);
        }
    }
}
