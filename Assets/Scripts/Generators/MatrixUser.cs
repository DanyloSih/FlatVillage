using DanPie.Framework.DMath;
using UnityEngine;
using System;

namespace FlatVillage.Generators
{
    public abstract class MatrixUser<TMatrixData> : IMatrixUser<TMatrixData>
    {
        private MatrixRepresentation<TMatrixData> _matrix;
        private Vector2Int _halfSizeOfMatrix;
        private Vector3Int _halfSizeOfMatrix3D;

        protected MatrixRepresentation<TMatrixData> Matrix { get => _matrix; }
        protected Vector2Int HalfSizeOfMatrix { get => _halfSizeOfMatrix; }
        protected Vector3Int HalfSizeOfMatrix3D { get => _halfSizeOfMatrix3D; }

        public void SetNewMatrix(MatrixRepresentation<TMatrixData> matrix)
        {
            _matrix = matrix;
            _halfSizeOfMatrix = Matrix.Size / 2;
            _halfSizeOfMatrix3D = new Vector3Int(HalfSizeOfMatrix.x, HalfSizeOfMatrix.y);
            OnMatrixUpdated(matrix);
        }

        public void CheckMatrix()
        {
            if (Matrix == null)
            {
                throw new Exception($"Map matrix is null! First install the map matrix " +
                    $"with method {nameof(SetNewMatrix)}.");
            }
        }

        protected virtual void OnMatrixUpdated(MatrixRepresentation<TMatrixData> matrix)
        {

        }
    }
}
