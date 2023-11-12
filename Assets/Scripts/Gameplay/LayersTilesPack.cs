using System;
using System.Collections.Generic;
using System.Linq;
using FlatVillage.Maps;
using UnityEngine;

namespace FlatVillage.Gameplay
{
    public struct LayersTilesPack
    {
        private readonly Vector2Int _positionInBasicLayerMatrix;
        private List<LayerTileInfo> _eachLayerTiles;

        public Vector2Int PositionInBasicLayerMatrix => _positionInBasicLayerMatrix;
        public IReadOnlyList<LayerTileInfo> EachLayerTiles => _eachLayerTiles;


        public LayersTilesPack(
            Vector2Int positionInMatrix,
            List<ILayerProviderBase> layers)
        {
            _positionInBasicLayerMatrix = positionInMatrix;
            _eachLayerTiles = layers.Select(
                x => x.GetLayerTile(positionInMatrix)).ToList();
        }
    }
}
