using FlatVillage.WindowSystem;
using UnityEngine;
using Zenject;

namespace FlatVillage.Installers
{
    public class WindowsCanvasesManagerInstaller : MonoInstaller
    {
        [SerializeField] private int _numberOfOrdersAvailableInOneWindowCanvas = 5000;

        public override void InstallBindings()
        {
            InjectableWindowsCanvasCreator<InjectableWindowsCanvas> creator 
                = new InjectableWindowsCanvasCreator<InjectableWindowsCanvas>(
                    _numberOfOrdersAvailableInOneWindowCanvas, 
                    new string[] { "Main", "Popup", "Debug" }, 
                    Container, 
                    transform);

            var canvases = creator.CreateWindowsCanvases(3);

            Container.Bind(typeof(WindowsCanvasesManager))
                .FromInstance(new WindowsCanvasesManager(canvases[0], canvases[1], canvases[2])).AsSingle();
        }
    }
}