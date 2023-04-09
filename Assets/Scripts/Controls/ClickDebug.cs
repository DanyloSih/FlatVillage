using UnityEngine;
using Zenject;

namespace FlatVillage.Controls
{
    public class ClickDebug : MonoBehaviour
    {
        private IMainControlActionsProvider _mainControlActionsProvider;

        [Inject]
        public void Construct(IMainControlActionsProvider mainControlActionsProvider)
        {
            _mainControlActionsProvider = mainControlActionsProvider;
            //_mainControlActionsProvider.Clicked += () => Debug.Log("Clicked!!!!");
        }
    }
}
