using DanPie.Framework.DMath;
using UnityEngine;

namespace FlatVillage.Gameplay
{
    public interface IPlayer
    {
        public int ID { get; }
        public string Name { get; }
        public Color Color { get; }

        public void OnTurnStart();

        public void OnTurnEnd();
    }
}
