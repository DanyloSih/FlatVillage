using DanPie.Framework.DMath;
using UnityEngine;

namespace FlatVillage.Generators
{
    public interface IMapToTilemapApplier<TData> : IMapToTilemapApplierReadonly
    {
        void SetNewMatrix(MatrixRepresentation<TData> map);

        void UpdateTile(Vector2Int tilePointInMatrix, int newTileID);
    }
}