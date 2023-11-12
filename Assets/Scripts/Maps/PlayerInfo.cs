using FlatVillage.Gameplay;

namespace FlatVillage.Maps
{
    public class PlayerInfo : IndexableObject
    {
        private IPlayer _player;

        public PlayerInfo(IPlayer player) : base(player.ID)
        {
            _player = player;
        }

        public IPlayer Player { get => _player; }
    }
}
