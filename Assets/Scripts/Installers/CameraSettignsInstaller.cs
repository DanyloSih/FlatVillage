using FlatVillage.Settings;
using UnityEngine;
using Zenject;

namespace FlatVillage.Installers
{
    public class CameraSettignsInstaller : MonoInstaller
    {
        [SerializeField] private CameraSettings _cameraSettings;

        public override void InstallBindings()
        {
            Container.Bind<CameraSettings>().FromInstance(_cameraSettings).AsTransient();
        }
    }
}