using System;
using FlatVillage.Maps;
using FlatVillage.Resources;
using UnityEngine;
using Zenject;

namespace FlatVillage.Gameplay
{
    /// <summary>
    /// The class is created to define a context.
    /// </summary>
    [Serializable]
    public class BaseMapPlaceTileAction : PlaceTileOnIntMapAction<TileInfo>
    {

    }

    [Serializable]
    public class PlaceTileOnIntMapAction<TObjectInfo> : ITileAction<int, TObjectInfo>
        where TObjectInfo : class, IIndexableObject
    {
        [SerializeField] private ItemInfo _itemInfo;
        [SerializeField] private ItemInfo _setTileItemInfo;

        private Map<int, TObjectInfo> _map;
        private Vector2Int _tilePositionInMatrix;
        private TilesInfoCollection _tilesInfoCollection;

        [Inject]
        public void Construct(TilesInfoCollection tilesInfoCollection)
        {
            _tilesInfoCollection = tilesInfoCollection;
        }

        public bool IsCanBeShown()
        {
            return _map.Matrix.IsPointInside(_tilePositionInMatrix);
        }

        public bool TryInvoke()
        {
            _map.SetTile(
                _tilePositionInMatrix, 
                _tilesInfoCollection.GetByItemInfo(_setTileItemInfo).ID);
            return true;
        }

        public void UpdateTileContext(
            Map<int, TObjectInfo> map, Vector2Int tilePositionInMatrix)
        {
            _map = map;
            _tilePositionInMatrix = tilePositionInMatrix;
        }

        public ItemInfo GetItemInfo()
        {
            return _itemInfo;
        }
    }
}
