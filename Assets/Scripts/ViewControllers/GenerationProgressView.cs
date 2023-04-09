using System;
using FlatVillage.Generators;
using FlatVillage.WindowSystem;
using UnityEngine;
using Zenject;

namespace FlatVillage.ViewControllers
{
    public class GenerationProgressView : MonoBehaviour
    {
        [SerializeField] private ProgressWindow _progressWindowPrefab;

        private GenerationOperations _generationOperations;
        private WindowsCanvasesManager _windowsCanvases;
        private ProgressWindow _progressWindow;

        [Inject]
        public void Construct(WindowsCanvasesManager windowsCanvases)
        {
            _windowsCanvases = windowsCanvases;
        }

        public void Start()
        {
            _progressWindow = _windowsCanvases.Main.InstanceOf(_progressWindowPrefab);
        }

        public void ShowNewGenerationProcess(GenerationOperations generationOperations)
        {
            if (generationOperations == null)
            {
                throw new ArgumentNullException(nameof(generationOperations));
            }

            TryUnsubscribe();

            _generationOperations = generationOperations;
            UpdateView();

            _windowsCanvases.Main.ShowAlso(_progressWindow.GetType());

            _generationOperations.Generated += OnGenerated;
            _generationOperations.StageCompleted += UpdateView;

        }

        private void TryUnsubscribe()
        {
            if (_generationOperations != null)
            {
                _generationOperations.StageCompleted -= UpdateView;
                _generationOperations.Generated -= OnGenerated;
            }
            _generationOperations = null;
        }

        private void OnGenerated()
        {
            TryUnsubscribe();
            _progressWindow.Hide();
        }

        private void UpdateView()
        {
            _progressWindow.SetProgressText(_generationOperations.CurrentStage.Name);
            _progressWindow.SetProgressValue(_generationOperations.Progress);
        }
    }
}
