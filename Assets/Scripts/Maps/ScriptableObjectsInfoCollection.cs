using System;
using System.Collections.Generic;
using System.Linq;
using DanPie.Framework.Exceptions;
using UnityEngine;
using Zenject;

namespace FlatVillage.Maps
{
    public abstract class ScriptableObjectsInfoCollection<T> 
        : ScriptableObject, IObjectsInfoCollection<T>
        where T : class, IObjectInfo
    {
        protected abstract List<T> ObjectsInfo { get; }

        private bool _idsUpdated = false;

        public int GetObjectsCount()
        {
            return ObjectsInfo.Count;
        }

        public T GetByID(int id)
        {
            if (id != Mathf.Clamp(id, 0, ObjectsInfo.Count - 1))
            {
                throw new ArgumentException($"Unable to get information about tile with id {id}! The " +
                    $"collection has information about tiles only in the range from 0 to {ObjectsInfo.Count - 1}.");
            }

            return ObjectsInfo[id];
        }

        public T GetByName(string tileName)
        {
            UpdateTilesInfoIDs();
            T result = null;
            try
            {
                result = ObjectsInfo.First(x => x.Name == tileName);
            }
            catch (InvalidOperationException)
            {
                throw new ArgumentException($"The collection does not contain information about a tile named {tileName}! " +
                    $"Carefully check the spelling of the name or the presence of this tile in the collection.");
            }

            return result;
        }

        public void OnEnable()
        {
            _idsUpdated = false;
            if (ObjectsInfo.Count == 0)
            {
                throw new FieldDataException(GetType().Name, nameof(ObjectsInfo),
                    "It makes no sense to leave this field empty, the collection must contain at least one element!");
            }

            UpdateTilesInfoIDs();
        }

        private void UpdateTilesInfoIDs()
        {
            if (_idsUpdated)
            {
                return;
            }
            _idsUpdated = true;

            int counter = 0;
            foreach (T tileInfo in ObjectsInfo)
            {
                tileInfo.InitializeID(counter);
                counter++;
            }
        }
    }
}
