using FlatVillage.Controls;
using UnityEngine;
using Zenject;

namespace FlatVillage.Installers
{
    public class MainControlActionsProviderInstaller : MonoInstaller
    {
        [SerializeField] private MapMoveActionsProviderAdapter _mainControlActionsProvider;

        public override void InstallBindings()
        {
            Container.Bind(typeof(IMainControlActionsProvider))
                .FromInstance(_mainControlActionsProvider);
        }
    }
}