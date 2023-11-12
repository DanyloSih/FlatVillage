using UnityEngine;

namespace FlatVillage.Generators
{
    public interface ITilemapMatrixPointsConverter<TMatrixData> : IMatrixUser<TMatrixData>
    {
        public Vector2Int TilemapPointToMatrixPoint(Vector2 tilemapPoint);

        public Vector2 MatrixPointToTilemapPoint(Vector2Int matrixPoint);
    }
}
