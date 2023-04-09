using UnityEngine;

namespace FlatVillage.Maps
{
    public class ResourceInfo : ObjectInfo
    {
        private Texture2D _icon;

        public Texture2D Icon { get => _icon; }

        public ResourceInfo(string name, int id, Texture2D icon) : base(name, id)
        {
            _icon = icon;
        }
    }
}
