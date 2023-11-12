using FlatVillage.Resources;
using UnityEngine;

namespace FlatVillage.Maps
{
    public interface ILayerProviderBase : IIndexableObject
    {
        public bool IsInitialized { get; }
        public ItemInfo ItemInfo { get; }

        public Vector2Int GetMatrixSize();

        public LayerTileInfo GetLayerTile(Vector2Int position);

    }
}
