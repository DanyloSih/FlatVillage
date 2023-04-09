using FlatVillage.Generators;
using UnityEngine.Tilemaps;

namespace FlatVillage.Maps
{
    /// <summary>
    /// Created to indicate context.
    /// </summary>
    public class BasicMap : Map<int, TileInfo>
    {
        public BasicMap(ObjectsInfoCollection<TileInfo> tilesInfo, Tilemap tilemap, IMapToTilemapApplier<int> mapToTilemapApplier) : base(tilesInfo, tilemap, mapToTilemapApplier)
        {
        }
    }
}
