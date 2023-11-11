using UnityEngine;

namespace FlatVillage.Gameplay
{
    public class PlayersFactory
    {
    }

    public class Player : IPlayer
    {
        private int _id;
        private string _name;
        private Color _color;

        public int ID { get => _id; }
        public string Name { get => _name; }
        public Color Color { get => _color; }
        public bool MoveCameraEveryTurn { get; set; } = true;

        public void OnTurnEnd()
        {
            
        }

        public void OnTurnStart()
        {
            
        }
    }
}
