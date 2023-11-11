using UnityEngine;

namespace FlatVillage.Maps
{
    public interface ILayerProviderBase : IObjectInfo
    {
        public Vector2Int GetMatrixSize();
        public ILayerTile GetLayerTile(Vector2Int position);
        public bool IsInitialized { get; }
    }
}
