using UnityEngine;
using DanPie.Framework.UnityExtensions;

namespace FlatVillage.Generators
{
    public class TilemapMatrixPointsViaOffsetConverter 
        : MatrixUser<int>, ITilemapMatrixPointsConverter<int>
    {
        public Vector2Int TilemapPointToMatrixPoint(Vector2 tilemapPoint)
        {
            CheckMatrix();

            return tilemapPoint.To2Int() + HalfSizeOfMatrix;
        }

        public Vector2 MatrixPointToTilemapPoint(Vector2Int matrixPoint)
        {
            CheckMatrix();

            return matrixPoint - HalfSizeOfMatrix + Vector2.one / 2;
        }
    }
}
