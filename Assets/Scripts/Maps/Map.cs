using DanPie.Framework.DMath;
using FlatVillage.Generators;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace FlatVillage.Maps
{
    public class Map<TData, TObjectInfo>
        where TObjectInfo : class, IObjectInfo
    {
        private MatrixRepresentation<TData> _map;
        private IObjectsInfoCollection<TObjectInfo> _objectsInfo;
        private Tilemap _tilemap;
        private IMapToTilemapApplier<TData> _mapApplier;

        public IReadonlyMatrixRepresentation<TData> Matrix { get => _map; }
        public IObjectsInfoCollection<TObjectInfo> ObjectsInfo { get => _objectsInfo; }
        public Tilemap Tilemap { get => _tilemap; }
        public IMapToTilemapApplierReadonly MapApplier { get => _mapApplier; }

        public Map(
            IObjectsInfoCollection<TObjectInfo> objectsInfo,
            Tilemap tilemap,
            IMapToTilemapApplier<TData> mapToTilemapApplier)
        {
            _mapApplier = mapToTilemapApplier;
            _objectsInfo = objectsInfo;
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
