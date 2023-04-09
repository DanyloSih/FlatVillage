using UnityEngine;

namespace FlatVillage.Generators
{
    public interface IMapToTilemapApplierReadonly
    {
        Vector2Int TilemapPointToMatrixPoint(Vector2 tilemapPoint);

        Vector2 MatrixPointToTilemapPoint(Vector2Int matrixPoint);
    }
}