using System.Numerics;
using UnityEngine.Tilemaps;

namespace FlatVillage.Generators
{
    public interface IMapGenerator
	{
		public GenerationOperations Generate(Tilemap tilemap);
	}

    public class BalancedMapGenerator : IMapGenerator
    {
        public GenerationOperations Generate(Tilemap tilemap)
        {
            return new GenerationOperations(
                new Stage(null, ""),
                new Stage(null, ""),
                new Stage(null, ""),
                new Stage(null, "")
                );
        }
    }
}
