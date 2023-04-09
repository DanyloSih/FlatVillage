using UnityEngine;

namespace FlatVillage.Settings
{
    [CreateAssetMenu(
    fileName = "CameraSettings",
    menuName = "GameSettings/CameraSettings",
    order = 0)]
    public class CameraSettings : ScriptableObject
    {
        [SerializeField] private Vector2 _minMaxZoom;

        public Vector2 MinMaxZoomBorder { get => _minMaxZoom; set => _minMaxZoom = value; }
    }
}
