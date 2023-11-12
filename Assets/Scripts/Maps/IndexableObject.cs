using System;
using DanPie.Framework.Common;
using UnityEngine;

namespace FlatVillage.Maps
{
    [Serializable]
    public class IndexableObject : IIndexableObject
    {
        private int _id;
        private bool _isInitialized = false;

        public int ID { get => _id; }

        public void InitializeID(int id)
        {
            if (_isInitialized)
            {
                throw new AlreadyInitializedException();
            }
            _id = id;
        }

        public IndexableObject(int id)
        {
            _id = id;
        }
    }
}
