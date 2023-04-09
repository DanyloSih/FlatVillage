using FlatVillage.Generators;
using UnityEngine;
using Zenject;

namespace FlatVillage.Installers
{
    public class BalancedMapGeneratorInstaller : MonoInstaller
    {
        [SerializeField] private BalancedMapGenerator _mapGenerator;

        public override void InstallBindings()
        {
            Container.Bind(typeof(BalancedMapGenerator), typeof(IMapGenerator))
                .FromInstance(_mapGenerator).AsSingle().Lazy();
        }
    }
}