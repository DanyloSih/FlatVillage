using DanPie.Framework.DMath;

namespace FlatVillage.Generators
{
    public class FillOperation : YieldOperation
    {
        private MatrixRepresentationInt _matrixRepresentation;
        private readonly int _fillItemId;
        private bool _isDone = false;

        public FillOperation(MatrixRepresentationInt matrixRepresentation, int fillItemId)
        {
            _matrixRepresentation = matrixRepresentation;
            _fillItemId = fillItemId;
        }

        protected override void OnOperationStarted()
        {
            for (int i = 0; i < _matrixRepresentation.FullLength; i++)
            {
                _matrixRepresentation[i] = _fillItemId;
            }
            _isDone = true;
        }

        protected override bool OnOperationTick(long ticks)
        {
            return _isDone; 
        }
    }
}
