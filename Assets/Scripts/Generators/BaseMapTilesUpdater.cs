using UnityEngine.Tilemaps;
using DanPie.Framework.DMath;
using UnityEngine;
using FlatVillage.Maps;

namespace FlatVillage.Generators
{
    public class BaseMapTilesUpdater : MatrixUser<int>, IMapTilesUpdater<int>
    {
        private Tilemap _tilemap;
        private TilesInfoCollection _baseTilesInfoCollection;

        public BaseMapTilesUpdater(
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

        public void UpdateTile(Vector2Int tilePointInMatrix, int newTileID)
        {
            CheckMatrix();

            Matrix.SetData(tilePointInMatrix, newTileID);

            _tilemap.SetTile(
                new Vector3Int(tilePointInMatrix.x, tilePointInMatrix.y, 0) - HalfSizeOfMatrix3D,
                _baseTilesInfoCollection.GetByID(newTileID).Tile);
        }

        protected override void OnMatrixUpdated(MatrixRepresentation<int> matrix)
        {
            UpdateTilemap();
        }

        private void UpdateTilemap()
        {
            CheckMatrix();

            _tilemap.ClearAllTiles();
            for (int y = 0; y < Matrix.Rows; y++)
            {
                for (int x = 0; x < Matrix.Columns; x++)
                {
                    _tilemap.SetTile(
                        new Vector3Int(x, y, 0) - HalfSizeOfMatrix3D,
                        _baseTilesInfoCollection.GetByID(Matrix[x, y]).Tile);
                }
            }
        }
    }
}
