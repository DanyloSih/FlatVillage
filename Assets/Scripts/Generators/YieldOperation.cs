using UnityEngine;

namespace FlatVillage.Generators
{
    public abstract class YieldOperation : CustomYieldInstruction
    {
        private bool _isStarted = false;
        private long _ticks = 0;

        public sealed override bool keepWaiting
        {
            get
            {
                TryStartOperation();
                _ticks++;
                if (_ticks >= long.MaxValue - 2)
                {
                    _ticks = 0;
                    OnTicksReset();
                }
                return !OnOperationTick(_ticks);
            }
        }

        private void TryStartOperation()
        {
            if (_isStarted)
            {
                return;
            }
            _isStarted = true;
            OnOperationStarted();
        }

        protected abstract void OnOperationStarted();

        protected abstract bool OnOperationTick(long tickId);

        protected virtual void OnTicksReset() { }
    }
}
