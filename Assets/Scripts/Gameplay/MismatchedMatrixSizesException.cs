using System;
using UnityEngine;

namespace FlatVillage.Gameplay
{
    public class MismatchedMatrixSizesException : Exception
    {
        public MismatchedMatrixSizesException(
            Vector2Int majorMatrixSize,
            string majorMatrixName,
            Vector2Int newMatrixSize,
            string newMatrixName) 
            : base($"The size of each matrix passed must be " +
                    $"equal to the size of {majorMatrixName}. The transmitted " +
                    $"matrix size in {newMatrixName} is {newMatrixSize}, but it " +
                    $"should be {majorMatrixSize}!")
        {
        }
    }
}
