using System.Collections.Generic;
using UnityEngine;

namespace FlatVillage.Generators
{
    public class GenerationOperations : CustomYieldInstruction
    {
        private List<Stage> _stages;
        private int _stageIndex = 0;

        public float Progress { get => Mathf.Clamp01((float)_stageIndex / _stages.Count); }

        public bool IsGenerated { get; private set; } = false;

        public GenerationOperations(Stage initialStage, params Stage[] stages)
        {
            _stages = new List<Stage> { initialStage };
            _stages.AddRange(stages);
        }

        public override bool keepWaiting
        {
            get
            {
                if (_stageIndex < _stages.Count)
                {
                    if (!_stages[_stageIndex].KeepWaiting)
                    {
                        _stageIndex++;
                    }
                }
                return _stageIndex < _stages.Count;
            }
        }
    }
}
