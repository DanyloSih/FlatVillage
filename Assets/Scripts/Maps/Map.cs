using DanPie.Framework.DMath;
using FlatVillage.Generators;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace FlatVillage.Maps
{
    public class Map<TData, TObjectInfo>
        where TObjectInfo : ObjectInfo
    {
        private MatrixRepresentation<TData> _map;
        private ObjectsInfoCollection<TObjectInfo> _objectsInfo;
        private Tilemap _tilemap;
        private IMapToTilemapApplier<TData> _mapApplier;

        public IReadonlyMatrixRepresentation<TData> CurrentMap { get => _map; }
        public ObjectsInfoCollection<TObjectInfo> ObjectsInfo { get => _objectsInfo; }
        public Tilemap Tilemap { get => _tilemap; }
        public IMapToTilemapApplierReadonly MapApplier { get => _mapApplier; }

        public Map(
            ObjectsInfoCollection<TObjectInfo> tilesInfo,
            Tilemap tilemap,
            IMapToTilemapApplier<TData> mapToTilemapApplier)
        {
            _mapApplier = mapToTilemapApplier;
            _objectsInfo = tilesInfo;
            _tilemap = tilemap;
            _map = new MatrixRepresentation<TData>(1, 1);
        }

        public Vector2Int WorldPositionToMatrixPosition(Vector2 worldPoint)
        {
            Vector2Int tilePoint = new Vector2Int(
                Mathf.RoundToInt(worldPoint.x - 0.5f),
                Mathf.RoundToInt(worldPoint.y - 0.5f));

            return _mapApplier.TilemapPointToMatrixPoint(tilePoint);
        }

        public void SetTile(Vector2Int tilePositionInMatrix, int tileID)
        {
            _mapApplier.UpdateTile(tilePositionInMatrix, tileID);
        }

        public void UpdateMap(MatrixRepresentation<TData> map)
        {
            _map = map;
            _mapApplier.SetNewMatrix(map);
        }
    }
}
