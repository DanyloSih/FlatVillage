using UnityEngine;

namespace FlatVillage.Generators
{
    public class Stage
    {
        private CustomYieldInstruction _customOperation;
        private string _name;

        public string Name { get => _name; }
        public bool KeepWaiting { get => _customOperation.keepWaiting; }

        public Stage(CustomYieldInstruction customOperation, string name)
        {
            _customOperation = customOperation;
            _name = name;
        }
    }
}
