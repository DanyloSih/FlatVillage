using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace FlatVillage.Maps
{
    [CreateAssetMenu(
        fileName = "TilesInfoCollection",
        menuName = "ObjectsInfo/TilesInfoCollection",
        order = 0)]
    public class TilesInfoCollection : ObjectsInfoCollection<TileInfo>
    {
        [SerializeField] private List<TileInfo> _tilesInfo = new List<TileInfo>();

        protected override List<TileInfo> ObjectsInfo { get => _tilesInfo; }
    }
}
