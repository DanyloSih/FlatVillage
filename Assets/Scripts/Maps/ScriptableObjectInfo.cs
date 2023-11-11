using DanPie.Framework.Common;
using UnityEngine;

namespace FlatVillage.Maps
{
    public abstract class ScriptableObjectInfo : ScriptableObject, IObjectInfo
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

        public void OnEnable()
        {
            _id = 0;
            _isInitialized = false;
            OnEnabled();
        }

        protected virtual void OnEnabled() { }
    }
}
