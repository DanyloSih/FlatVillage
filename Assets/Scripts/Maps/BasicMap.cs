using FlatVillage.Generators;
using UnityEngine.Tilemaps;

namespace FlatVillage.Maps
{
    /// <summary>
    /// Created to indicate context.
    /// </summary>
    public class BasicMap : Map<int, TileInfo>
    {
        public BasicMap(
            IndexableScriptableObjectsCollection<TileInfo> tilesInfo, 
            Tilemap tilemap, 
            IMapTilesUpdater<int> mapToTilemapApplier,
            ITilemapMatrixPointsConverter<int> tilemapMatrixPointsConverter) 
            : base(tilesInfo, tilemap, mapToTilemapApplier, tilemapMatrixPointsConverter)
        {
        }
    }
}
