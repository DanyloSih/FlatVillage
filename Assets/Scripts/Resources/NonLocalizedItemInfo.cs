using UnityEngine;

namespace FlatVillage.Resources
{
    [CreateAssetMenu(
        fileName = nameof(NonLocalizedItemInfo), 
        menuName = "FlatVillage/Resources/" + nameof(NonLocalizedItemInfo))]
    public class NonLocalizedItemInfo : ItemInfo
    {
        [SerializeField] private string _tag;
        [SerializeField] private string _displayName;
        [SerializeField] private Sprite _sprite;

        public override string GetDisplayName()
        {
            return _displayName;
        }

        public override Sprite GetSprite()
        {
            return _sprite;
        }

        protected override string OnGetTag()
        {
            return _tag;
        }
    }
}