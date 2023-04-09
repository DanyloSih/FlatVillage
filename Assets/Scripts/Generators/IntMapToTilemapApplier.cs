using UnityEngine.Tilemaps;
using DanPie.Framework.DMath;
using UnityEngine;
using FlatVillage.Maps;
using System;
using DanPie.Framework.UnityExtensions;

namespace FlatVillage.Generators
{
    public class IntMapToTilemapApplier : IMapToTilemapApplier<int>
    {
        private Tilemap _tilemap;
        private TilesInfoCollection _baseTilesInfoCollection;
        private MatrixRepresentation<int> _map;
        private Vector2Int _offset;
        private Vector3Int _offset3D;

        public IntMapToTilemapApplier(
            Tilemap tilemap,
            TilesInfoCollection baseTilesInfoCollection,
            MatrixRepresentationInt map = null)
        {
            _tilemap = tilemap;
            _baseTilesInfoCollection = baseTilesInfoCollection;

            if (map != null)
            {
                SetNewMatrix(map);
            }
        }

        public void SetNewMatrix(MatrixRepresentation<int> map)
        {
            _map = map;
            _offset = _map.Size / 2;
            _offset3D = new Vector3Int(_offset.x, _offset.y);
            UpdateTilemap();
        }

        public Vector2Int TilemapPointToMatrixPoint(Vector2 tilemapPoint)
        {
            CheckMap();

            return tilemapPoint.To2Int() + _offset;
        }

        public Vector2 MatrixPointToTilemapPoint(Vector2Int matrixPoint)
        {
            CheckMap();

            return matrixPoint - _offset + Vector2.one / 2;
        }

        public void UpdateTile(Vector2Int tilePointInMatrix, int newTileID)
        {
            CheckMap();

            _map.SetData(tilePointInMatrix, newTileID);

            _tilemap.SetTile(
                new Vector3Int(tilePointInMatrix.x, tilePointInMatrix.y, 0) - _offset3D,
                _baseTilesInfoCollection.GetTileInfoByID(newTileID).Tile);
        }

        private void UpdateTilemap()
        {
            CheckMap();

            _tilemap.ClearAllTiles();
            for (int y = 0; y < _map.Rows; y++)
            {
                for (int x = 0; x < _map.Columns; x++)
                {
                    _tilemap.SetTile(
                        new Vector3Int(x, y, 0) - _offset3D,
                        _baseTilesInfoCollection.GetTileInfoByID(_map[x, y]).Tile);
                }
            }
        }

        private void CheckMap()
        {
            if (_map == null)
            {
                throw new Exception($"Map matrix is null! First install the map matrix " +
                    $"with method {nameof(SetNewMatrix)}.");
            }
        }
    }
}
