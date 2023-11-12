using System;
using System.Collections.Generic;

namespace FlatVillage.Maps
{
    public static class IObjectsInfoCollectionExtensions
    {
        public static void CheckAssociation<T>(
            this IIndexableObjectsCollection<T> objectsInfoCollection,
            List<T> associatedList)
             where T : class, IIndexableObject
        {
            List<T> majorList = objectsInfoCollection.ToList();
            CheckListIDs(majorList, nameof(majorList));
            CheckListIDs(associatedList, nameof(associatedList));

            if (majorList.Count != associatedList.Count)
            {
                throw new ArgumentException($"The number of elements in " +
                    $"{nameof(majorList)} and {associatedList} should be equal!");
            }

            for (int i = 0; i < majorList.Count; i++)
            {
                if (majorList[i] != associatedList[i])
                {
                    throw new ArgumentException($"{typeof(T).Name} references in {nameof(associatedList)} objects within " +
                        $"must be in the same order as in {nameof(majorList)}!");
                }
            }
        }

        public static List<T> ToList<T>(this IIndexableObjectsCollection<T> objectsInfoCollection)
            where T : class, IIndexableObject
        {
            List<T> result = new List<T>();
            for (int i = 0; i < objectsInfoCollection.GetObjectsCount(); i++)
            {
                result.Add(objectsInfoCollection.GetByID(i));
            }

            return result;
        }

        private static void CheckListIDs<T>(List<T> list, string listName)
            where T : class, IIndexableObject
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].ID != i)
                {
                    throw new ArgumentException($"{typeof(T).Name} IDs must strictly correspond to " +
                        $"the order in the list in which they were transferred inside {listName}!");
                }
            }
        }   
    }
}