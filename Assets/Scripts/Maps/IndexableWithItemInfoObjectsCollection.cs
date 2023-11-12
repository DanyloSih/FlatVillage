using System;
using System.Linq;
using FlatVillage.Resources;

namespace FlatVillage.Maps
{
    public abstract class IndexableWithItemInfoObjectsCollection<T> : IndexableScriptableObjectsCollection<T>
        where T : class, IIndexableObject, IObjectWithItemInfo
    {
        public T GetByItemInfo(ItemInfo itemInfo)
        {
            try
            {
                return CollectionObjects.First(x => x.ItemInfo.Equals(itemInfo));
            }
            catch
            {
                throw new InvalidOperationException($"The \"{name}\" collection does not contain " +
                    $"an object with \'{typeof(ItemInfo)}\" whose tag is equal to: {itemInfo.GetTag()}!");
            }
        }
    }
}
