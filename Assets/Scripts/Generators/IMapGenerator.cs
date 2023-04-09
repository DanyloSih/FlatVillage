using UnityEngine.Tilemaps;
using DanPie.Framework.DMath;

namespace FlatVillage.Generators
{
    public interface IMapGenerator
	{
        public MatrixRepresentationInt CurrentMap { get; }

        public GenerationOperations Generate(out MatrixRepresentationInt map);
	}
}
