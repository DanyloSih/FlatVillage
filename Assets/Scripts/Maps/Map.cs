using DanPie.Framework.DMath;
using FlatVillage.Generators;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace FlatVillage.Maps
{
    public class Map<TData, TObjectInfo>
        where TObjectInfo : class, IIndexableObject
    {
        private MatrixRepresentation<TData> _map;
        private IIndexableObjectsCollection<TObjectInfo> _objectsInfo;
        private Tilemap _tilemap;
        private IMapTilesUpdater<TData> _mapApplier;
        private ITilemapMatrixPointsConverter<TData> _tilemapMatrixPointsConverter;

        public IReadonlyMatrixRepresentation<TData> Matrix { get => _map; }
        public IIndexableObjectsCollection<TObjectInfo> ObjectsInfo { get => _objectsInfo; }
        public Tilemap Tilemap { get => _tilemap; }
        public ITilemapMatrixPointsConverter<TData> TilemapMatrixPointsConverter { get => _tilemapMatrixPointsConverter; }

        public Map(
            IIndexableObjectsCollection<TObjectInfo> objectsInfo,
            Tilemap tilemap,
            IMapTilesUpdater<TData> mapToTilemapApplier,
            ITilemapMatrixPointsConverter<TData> tilemapMatrixPointsConverter)
        {
            _mapApplier = mapToTilemapApplier;
            _objectsInfo = objectsInfo;
            _tilemap = tilemap;
            _map = new MatrixRepresentation<TData>(1, 1);
            _tilemapMatrixPointsConverter = tilemapMatrixPointsConverter;
        }

        public Vector2Int WorldPositionToMatrixPosition(Vector2 worldPoint)
        {
            Vector2Int tilePoint = new Vector2Int(
                Mathf.RoundToInt(worldPoint.x - 0.5f),
                Mathf.RoundToInt(worldPoint.y - 0.5f));

            return _tilemapMatrixPointsConverter.TilemapPointToMatrixPoint(tilePoint);
        }

        public void SetTile(Vector2Int tilePositionInMatrix, int tileID)
        {
            _mapApplier.UpdateTile(tilePositionInMatrix, tileID);
        }

        public void UpdateMap(MatrixRepresentation<TData> map)
        {
            _map = map;
            _tilemapMatrixPointsConverter.SetNewMatrix(map);
            _mapApplier.SetNewMatrix(map);
        }
    }
}
