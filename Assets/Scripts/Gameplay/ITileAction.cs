using UnityEngine;

namespace FlatVillage.Gameplay
{
    public interface ITileAction
    {
        public void UpdateTileContext(Vector2Int tilePositionInMatrix);

        public string GetText();

        public bool IsShown();

        public bool TryInvoke();
    }
}
