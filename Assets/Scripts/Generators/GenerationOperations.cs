using System;
using System.Collections.Generic;
using UnityEngine;

namespace FlatVillage.Generators
{
    public class GenerationOperations : CustomYieldInstruction
    {
        private List<Stage> _stages;
        private int _stageIndex = 0;

        public event Action Generated;
        public event Action StageCompleted;

        public float Progress { get => Mathf.Clamp01((float)_stageIndex / _stages.Count); }
        public Stage CurrentStage { get => _stages[Mathf.Clamp(_stageIndex, 0, _stages.Count - 1)]; }
        public bool IsGenerated { get => _stageIndex >= _stages.Count; }

        public override bool keepWaiting
        {
            get
            {

                if (!IsGenerated)
                {
                    if (!_stages[_stageIndex].KeepWaiting)
                    {
                        _stageIndex++;
                        StageCompleted?.Invoke();
                    }
                }

                if (IsGenerated)
                {
                    Generated?.Invoke();
                }

                return !IsGenerated;
            }
        }

        public GenerationOperations(Stage initialStage, params Stage[] stages)
        {
            _stages = new List<Stage> { initialStage };
            _stages.AddRange(stages);
        }
    }
}
