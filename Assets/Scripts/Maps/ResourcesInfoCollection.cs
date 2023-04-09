using System.Collections.Generic;
using UnityEngine;

namespace FlatVillage.Maps
{
    [CreateAssetMenu(
        fileName = "ResourcesInfoCollection",
        menuName = "ObjectsInfo/ResourcesInfoCollection",
        order = 0)]
    public class ResourcesInfoCollection : ObjectsInfoCollection<ResourceInfo>
    {
        [SerializeField] private List<ResourceInfo> _resourcesInfo = new List<ResourceInfo>(); 

        protected override List<ResourceInfo> ObjectsInfo { get => _resourcesInfo; }
    }
}
