using FlatVillage.Controls;
using UnityEngine;
using Zenject;
using SimpleHeirs;

namespace FlatVillage.Installers
{
    public class MainControlActionsProviderInstaller : MonoInstaller
    {
        [SerializeField] private HeirsProvider<IMainControlActionsProvider> _mainControlActionsProvider;

        public override void InstallBindings()
        {
            Container.Bind(typeof(IMainControlActionsProvider))
                .FromInstance(_mainControlActionsProvider.GetValue());
        }
    }
}