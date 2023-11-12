using FlatVillage.Maps;
using UnityEngine;

namespace FlatVillage.Gameplay
{
    public interface ITileAction<TMapData, TObjectInfo> : ITileActionInfo
         where TObjectInfo : class, IIndexableObject
    {
        public void UpdateTileContext(Map<TMapData, TObjectInfo> map, Vector2Int tilePositionInMatrix);
    }
}
