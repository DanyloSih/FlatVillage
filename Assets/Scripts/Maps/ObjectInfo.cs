using System;
using DanPie.Framework.Common;
using UnityEngine;

namespace FlatVillage.Maps
{
    [Serializable]
    public class ObjectInfo
    {
        [SerializeField] private string _name;

        private int _id;
        private bool _isInitialized = false;

        public string Name { get => _name; }
        public int ID { get => _id; }

        public void InitializeID(int id)
        {
            if (_isInitialized)
            {
                throw new AlreadyInitializedException();
            }
            _id = id;
        }

        public ObjectInfo(string name, int id)
        {
            _name = name;
            _id = id;
        }
    }
}
