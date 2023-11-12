using System.Collections.Generic;
using UnityEngine;

namespace FlatVillage.Maps
{
    [CreateAssetMenu(
        fileName = "TilesInfoCollection",
        menuName = "ObjectsInfo/TilesInfoCollection",
        order = 0)]
    public class TilesInfoCollection : IndexableWithItemInfoObjectsCollection<TileInfo>
    {
        [SerializeField] private List<TileInfo> _tilesInfo = new List<TileInfo>();

        protected override List<TileInfo> CollectionObjects { get => _tilesInfo; }
    }
}
