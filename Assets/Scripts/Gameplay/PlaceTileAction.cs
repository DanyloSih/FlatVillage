using FlatVillage.Maps;
using UnityEngine;
using Zenject;

namespace FlatVillage.Gameplay
{
    public class PlaceTileAction : ITileAction
    {
        private BasicMap _baseMap;
        private int _placeTileID;
        private Vector2Int _tilePositionInMatrix;

        [Inject]
        public PlaceTileAction(BasicMap baseMap, int placeTileID)
        {
            _baseMap = baseMap;
            _placeTileID = placeTileID;
        }

        public string GetText()
        {
            return $"Place tile: {_placeTileID}";
        }

        public bool IsShown()
        {
            return _baseMap.Matrix.IsPointInside(_tilePositionInMatrix);
        }

        public bool TryInvoke()
        {
            _baseMap.SetTile(_tilePositionInMatrix, _placeTileID);
            return true;
        }

        public void UpdateTileContext(Vector2Int tilePositionInMatrix)
        {
            _tilePositionInMatrix = tilePositionInMatrix;
        }
    }
}
