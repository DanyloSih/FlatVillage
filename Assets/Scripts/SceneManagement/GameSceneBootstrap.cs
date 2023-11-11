using System.Collections;
using DanPie.Framework.DMath;
using FlatVillage.Generators;
using FlatVillage.Maps;
using FlatVillage.ViewControllers;
using FlatVillage.WindowSystem;
using UnityEngine;
using Zenject;

namespace FlatVillage.SceneManagement
{

    public class GameSceneBootstrap : MonoBehaviour
    {
        [SerializeField] private DebugInfoWindow _debugInfoWindowPrefab;
        [SerializeField] private LoadingWindow _loadingWindowPrefab;
        [SerializeField] private GenerationProgressView _generationProgressView;

        private WindowsCanvasesManager _windowsCanvasesManager;
        private BasicMap _baseMap;
        private IMapGenerator _mapGenerator;
        private LoadingWindow _loadingWindow;
        private DebugInfoWindow _debugInfoWindow;

        [Inject]
        public void Construct(
            IMapGenerator mapGenerator,
            BasicMap baseMap,
            WindowsCanvasesManager windowsCanvasesManager)
        {
            _windowsCanvasesManager = windowsCanvasesManager;
            _baseMap = baseMap;
            _mapGenerator = mapGenerator;
        }

        public void Start() 
        {
            _loadingWindow = _windowsCanvasesManager.Main.InstanceOf(_loadingWindowPrefab);
            _debugInfoWindow = _windowsCanvasesManager.Debug.InstanceOf(_debugInfoWindowPrefab);
            _windowsCanvasesManager.Debug.ShowAlso(_debugInfoWindow.GetType());
            StartCoroutine(MapGenerationProcess());
        }

        public IEnumerator MapGenerationProcess()
        {
            _windowsCanvasesManager.Main.ShowOnly(_loadingWindow.GetType());
            var operations = _mapGenerator.Generate(out MatrixRepresentationInt map);
            _generationProgressView.ShowNewGenerationProcess(operations);
            yield return operations;
            _baseMap.UpdateMap(map);
            yield return null;
            _loadingWindow.Hide();
        }
    }
}
