using System;
using System.Collections.Generic;
using DanPie.Framework.Exceptions;
using UnityEngine;

namespace FlatVillage.Maps
{
    public abstract class IndexableScriptableObjectsCollection<T> 
        : ScriptableObject, IIndexableObjectsCollection<T>
        where T : class, IIndexableObject
    {
        protected abstract List<T> CollectionObjects { get; }

        private bool _idsUpdated = false;

        public int GetObjectsCount()
        {
            return CollectionObjects.Count;
        }

        public T GetByID(int id)
        {
            if (id != Mathf.Clamp(id, 0, CollectionObjects.Count - 1))
            {
                throw new ArgumentException($"Unable to get information about \"{typeof(T).Name}\" element with id {id}! \nThe " +
                    $"collection has information about \"{typeof(T).Name}\" elements only in the range from 0 to {CollectionObjects.Count - 1}.");
            }

            return CollectionObjects[id];
        }

        public void OnEnable()
        {
            _idsUpdated = false;
            if (CollectionObjects.Count == 0)
            {
                throw new FieldDataException(GetType().Name, nameof(CollectionObjects),
                    "It makes no sense to leave this field empty, the collection must contain at least one element!");
            }

            UpdateIndexableObjectIDs();
        }

        private void UpdateIndexableObjectIDs()
        {
            if (_idsUpdated)
            {
                return;
            }
            _idsUpdated = true;

            int counter = 0;
            foreach (T indexableObject in CollectionObjects)
            {
                indexableObject.InitializeID(counter);
                counter++;
            }
        }
    }
}
