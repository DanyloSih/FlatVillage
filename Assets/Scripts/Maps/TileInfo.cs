using System;
using System.Collections.Generic;
using FlatVillage.Resources;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace FlatVillage.Maps
{
    [Serializable]
    public class TileInfo : IndexableObject, IObjectWithItemInfo
    {
        [SerializeField] private ItemInfo _itemInfo;
        [SerializeField] private TileBase _tile;

        /// <summary>
        /// DO NOT USE THIS! Created for serialization.
        /// </summary>
        public TileInfo() : base(0)
        {
        }

        public TileInfo(
            int id,
            TileBase tile,
            ItemInfo itemInfo) : base(id)
        {
            _tile = tile;
            _itemInfo = itemInfo;
        }

        public TileBase Tile { get => _tile; }
        public ItemInfo ItemInfo { get => _itemInfo; }
    }
}
