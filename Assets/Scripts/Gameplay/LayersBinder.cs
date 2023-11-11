using System;
using System.Collections.Generic;
using System.Linq;
using FlatVillage.Maps;
using UnityEngine;

namespace FlatVillage.Gameplay
{
    public class LayersBinder
    {
        private bool _isLayersInitialized = false;
        private ILayerProvider<int, TileInfo> _basicLayerProvider;
        private List<ILayerProviderBase> _additionalLayerProviders = new List<ILayerProviderBase>();

        public ILayerProvider<int, TileInfo> BasicLayerProvider { get => _basicLayerProvider; }
        public virtual bool IsLayersInitialized { get => _isLayersInitialized; }
        private Map<int, TileInfo> _basicLayer { get => _basicLayerProvider.Map; }

        public void InitializeSession(
            ILayerProvider<int, TileInfo> basicLayerProvider,
            List<ILayerProviderBase> additionalLayerProviders)
        {
            _basicLayerProvider = basicLayerProvider;
            SetNewAdditionalLayers(additionalLayerProviders);
            _isLayersInitialized = true;
        }

        public ILayerProviderBase GetAdditionalLayerProviderByName(string name)
        {
            try
            {
                return _additionalLayerProviders.First(x => x.Name == name);
            }
            catch (InvalidOperationException)
            {
                throw new ArgumentException($"Layer with name {name} not exist!");
            }
        }

        public FullTileInfo GetFullTileInfo(Vector2Int positionInMatrix)
        {
            return new FullTileInfo(
                positionInMatrix,
                _basicLayerProvider,
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

        private void SetNewAdditionalLayers(List<ILayerProviderBase> additionalLayerProviders)
        {
            _additionalLayerProviders.Clear();
            foreach (var item in additionalLayerProviders)
            {
                CheckNewMapSize(
                    item.GetMatrixSize(),
                    $"\"additional layer provider: {item.Name}\"");

                _additionalLayerProviders.Add(item);
            }
        }
    }
}
