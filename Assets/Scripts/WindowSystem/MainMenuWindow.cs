using DanPie.Framework.WindowSystem;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

namespace FlatVillage.WindowSystem
{
    public class MainMenuWindow : WindowObject
    {
        [SerializeField] private Button _playButton;
        [SerializeField] private string _gameSceneName;
        [SerializeField] private LoadingWindow _loadingWindowPrefab;

        private WindowsCanvasesManager _windowsCanvasesManager;
        private LoadingWindow _loadingWindow;

        [Inject]
        public void Construct(WindowsCanvasesManager windowsCanvasesManager)
        {
            _windowsCanvasesManager = windowsCanvasesManager;
        }

        protected override void OnAwake()
        {
            _playButton.onClick.AddListener(Play);
            _loadingWindow = _windowsCanvasesManager.Main.InstanceOf(_loadingWindowPrefab);
        }

        private void Play()
        {
            _windowsCanvasesManager.Main.ShowOnly(_loadingWindow.GetType());
            SceneManager.LoadSceneAsync(_gameSceneName);
        }
    }
}
