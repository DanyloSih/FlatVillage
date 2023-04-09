using System;
using UnityEngine;

namespace FlatVillage.Maps
{
    [Serializable]
    public class ResourceExtractionInfo
    {
        [SerializeField] private string _resourceName;
        [Tooltip("Can be negative!")]
        [SerializeField] private float _extractionValue;
    }
}
