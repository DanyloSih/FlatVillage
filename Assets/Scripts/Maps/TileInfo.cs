using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace FlatVillage.Maps
{
    [Serializable]
    public class TileInfo : ObjectInfo
    {
        [SerializeField] private TileBase _tile;
        [SerializeField] private List<ResourceExtractionInfo> _resourceExtractionInfo;

        public TileInfo(
            string tileName,
            int id,
            List<ResourceExtractionInfo> resourceExtractionInfo,
            TileBase tile) : base(tileName, id)
        {
            _resourceExtractionInfo = resourceExtractionInfo;
            _tile = tile;
        }

        public TileBase Tile { get => _tile; }
        public List<ResourceExtractionInfo> ResourceExtractionInfo { get => _resourceExtractionInfo; }
    }
}
