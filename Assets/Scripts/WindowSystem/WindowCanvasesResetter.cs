using System;
using UnityEngine.SceneManagement;
using Zenject;

namespace FlatVillage.WindowSystem
{
    public class WindowCanvasesResetter : IInitializable, IDisposable
    {
        private WindowsCanvasesManager _windowsCanvasesManager;

        [Inject]
        public WindowCanvasesResetter(WindowsCanvasesManager windowsCanvasesManager)
        {
            _windowsCanvasesManager = windowsCanvasesManager;
        }

        public void Initialize()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        public void Dispose()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            _windowsCanvasesManager.Main.RemoveAllWindows();
            _windowsCanvasesManager.Popup.RemoveAllWindows();
        }
    }
}
