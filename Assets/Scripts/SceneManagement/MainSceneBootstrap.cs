using FlatVillage.WindowSystem;
using UnityEngine;
using Zenject;

namespace FlatVillage.SceneManagement
{
    public class MainSceneBootstrap : MonoBehaviour
    {
        [SerializeField] private MainMenuWindow _mainMenuWindowPrefab;

        private WindowsCanvasesManager _windowsCanvasesManager;
        private MainMenuWindow _mainMenuWindow;

        [Inject]
        public void Construct(WindowsCanvasesManager windowsCanvasesManager)
        {
            _windowsCanvasesManager = windowsCanvasesManager;
        }

        public void Start()
        {
            _mainMenuWindow = _windowsCanvasesManager.Main.InstanceOf(_mainMenuWindowPrefab);
            _windowsCanvasesManager.Main.ShowOnly(_mainMenuWindow.GetType());
        }
    }
}
