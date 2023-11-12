using DanPie.Framework.DMath;

namespace FlatVillage.Generators
{
    public interface IMatrixUser<TMatrixData>
    {
        void SetNewMatrix(MatrixRepresentation<TMatrixData> map);
    }
}
