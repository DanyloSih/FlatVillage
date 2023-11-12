using DanPie.Framework.DMath;
using UnityEngine;

namespace FlatVillage.Generators
{
    public interface IMapTilesUpdater<TMapData> : IMatrixUser<TMapData>
    {
        void UpdateTile(Vector2Int tilePointInMatrix, int newTileID);
    }
}