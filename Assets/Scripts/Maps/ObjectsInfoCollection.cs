using System;
using System.Collections.Generic;
using System.Linq;

namespace FlatVillage.Maps
{
    public class ObjectsInfoCollection<T> : IObjectsInfoCollection<T>
        where T : ObjectInfo
    {
        private List<T> _collection = new List<T>();

        public ObjectsInfoCollection(List<T> collection)
        {
            _collection = collection;
            int counter = 0;
            foreach (var item in _collection)
            {
                item.InitializeID(counter);
                counter++;
            }
        }

        public T GetByID(int id)
        {
            return _collection[id];
        }

        public T GetByName(string tileName)
        {
            T result = null;
            try
            {
                result = _collection.First(x => x.Name == tileName);
            }
            catch (InvalidOperationException)
            {
                throw new ArgumentException($"The collection does not contain information about a tile named {tileName}! " +
                    $"Carefully check the spelling of the name or the presence of this tile in the collection.");
            }

            return result;
        }

        public int GetObjectsCount()
        {
            return _collection.Count;
        }
    }
}