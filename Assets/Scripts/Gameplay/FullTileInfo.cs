using System;
using System.Collections.Generic;
using System.Linq;
using FlatVillage.Maps;
using UnityEngine;

namespace FlatVillage.Gameplay
{
    public struct FullTileInfo
    {
        private ILayerTile _basicTileInfo;
        private List<ILayerTile> _eachLayerTiles;
        private readonly Vector2Int _positionInBasicLayerMatrix;

        public Vector2Int PositionInBasicLayerMatrix => _positionInBasicLayerMatrix;

        public FullTileInfo(
            Vector2Int positionInBasicLayerMatrix,
            ILayerProvider<int, TileInfo> basicLayer,
            List<ILayerProviderBase> additionalLayers)
        {
            _basicTileInfo = basicLayer.GetLayerTile(positionInBasicLayerMatrix);
            _positionInBasicLayerMatrix = positionInBasicLayerMatrix;
            _eachLayerTiles = additionalLayers.Select(
                x => x.GetLayerTile(positionInBasicLayerMatrix)).ToList();
        }

        public IObjectInfo GetObjectInfoByLayerName(string layerName)
        {
            if (_basicTileInfo.LayerName == layerName)
            {
                return _basicTileInfo.ObjectInfo;
            }
            try
            {
                return _eachLayerTiles.First(x => x.LayerName == layerName).ObjectInfo;
            }
            catch (InvalidOperationException)
            {
                throw new ArgumentException($"Missing information about layer {layerName}!");
            }
        }
    }
}
