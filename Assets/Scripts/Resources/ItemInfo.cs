using System;
using UnityEditor;
using UnityEngine;

namespace FlatVillage.Resources
{
    public abstract class ItemInfo : ScriptableObject
    {
        public string GetTag()
        {
            string tag = OnGetTag();
            if (string.IsNullOrEmpty(tag))
            {
                throw new InvalidOperationException($"You should not return " +
                    $"empty value from {nameof(GetTag)} method! \n" +
                    $"Be sure that you have configured the asset with " +
                    $"type: \"{GetType().Name}\" and named: \"{name}\" correctly!");
            }

            return tag;
        }

        public override bool Equals(object other)
        {
            if(GetType().Equals(other.GetType()))
            {
                return ((ItemInfo)other).GetTag().Equals(GetTag());
            }

            return false;
        }

        public override int GetHashCode()
        {
            return GetTag().GetHashCode();
        }

        /// <summary>
        /// Can return empty value!
        /// </summary>
        public abstract string GetDisplayName();

        /// <summary>
        /// Can return empty value!
        /// </summary>
        public abstract Sprite GetSprite();

        protected abstract string OnGetTag();
    }
}