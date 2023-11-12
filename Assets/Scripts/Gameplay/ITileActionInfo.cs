using FlatVillage.Resources;
using UnityEngine;

namespace FlatVillage.Gameplay
{
    public interface ITileActionInfo : ITileActionBase
    {
        ItemInfo GetItemInfo(); 

        bool IsCanBeShown();
    }
}