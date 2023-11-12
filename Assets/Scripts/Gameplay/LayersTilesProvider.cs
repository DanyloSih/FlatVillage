using System;
using System.Collections.Generic;
using System.Linq;
using FlatVillage.Maps;
using FlatVillage.Resources;
using UnityEngine;

namespace FlatVillage.Gameplay
{
    public class LayersTilesProvider
    {
        private bool _isLayersInitialized = false;
        private ILayerProvider<int, TileInfo> _basicLayerProvider;
        private List<ILayerProviderBase> _additionalLayerProviders = new List<ILayerProviderBase>();

        public ILayerProvider<int, TileInfo> BasicLayerProvider { get => _basicLayerProvider; }
        public virtual bool IsLayersInitialized { get => _isLayersInitialized; }
        private Map<int, TileInfo> _basicLayer { get => _basicLayerProvider.Map; }

        public void Initialize(
            List<ILayerProviderBase> layerProviders)
        {
            SetNewLayers(layerProviders);
            _isLayersInitialized = true;
        }

        public ILayerProviderBase GetAdditionalLayerProviderByItemInfo(ItemInfo layerItemInfo)
        {
            try
            {
                return _additionalLayerProviders.First(x => x.ItemInfo.Equals(layerItemInfo));
            }
            catch (InvalidOperationException)
            {
                throw new ArgumentException($"Layer with {nameof(ItemInfo)} " +
                    $"name \"{layerItemInfo.name}\" not exist!");
            }
        }

        public LayersTilesPack GetFullTileInfo(Vector2Int positionInMatrix)
        {
            return new LayersTilesPack(
                positionInMatrix,
                _additionalLayerProviders);
        }

        protected void CheckNewMapSize(Vector2Int newMatrixSize, string paramName)
        {
            if (_basicLayer.Matrix.Size != newMatrixSize)
            {
                throw new MismatchedMatrixSizesException(
                    _basicLayer.Matrix.Size, nameof(_basicLayer), newMatrixSize, paramName);
            }
        }

        private void SetNewLayers(List<ILayerProviderBase> additionalLayerProviders)
        {
            _additionalLayerProviders.Clear();
            foreach (var item in additionalLayerProviders)
            {
                CheckNewMapSize(
                    item.GetMatrixSize(),
                    $"\"additional layer provider: {item.ItemInfo.GetDisplayName()}\"");

                _additionalLayerProviders.Add(item);
            }
        }
    }
}
