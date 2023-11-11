using FlatVillage.Gameplay;

namespace FlatVillage.Maps
{
    public class PlayerInfo : ObjectInfo
    {
        private IPlayer _player;

        public PlayerInfo(IPlayer player) : base(player.Name, player.ID)
        {
            _player = player;
        }

        public IPlayer Player { get => _player; }
    }
}
