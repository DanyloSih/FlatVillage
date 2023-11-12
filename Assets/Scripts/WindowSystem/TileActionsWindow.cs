using System.Collections.Generic;
using DanPie.Framework.WindowSystem;
using FlatVillage.Gameplay;
using FlatVillage.Settings;
using FlatVillage.Views;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace FlatVillage.WindowSystem
{
    public class TileActionsWindow : WindowObject
    {
        [SerializeField] private RectTransform _popupWindow;
        [SerializeField] private float _popupScale = 0.7f;
        [SerializeField] private RectTransform _tileActionViewsContainer;
        [SerializeField] private Button _closeButton;
        [SerializeField] private TileActionView _tileActionViewPrefab;

        private List<TileActionView> _tileActionsViews = new List<TileActionView>();
        private Camera _camera;
        private Vector2 _tileWorldPoint;
        private CameraSettings _cameraSettings;

        [Inject]
        public void Construct(CameraSettings cameraSettings)
        {
            _cameraSettings = cameraSettings;
        }

        public void Initialize(
            IEnumerable<ITileActionInfo> tileActions,
            Vector2 tileWorldPoint)
        {
            OnHide();
            _tileWorldPoint = tileWorldPoint;

            foreach (var tileAction in tileActions)
            {
                if (!tileAction.IsCanBeShown())
                {
                    continue;
                }
                var tileActionViewInstance = Instantiate(_tileActionViewPrefab, _tileActionViewsContainer);
                tileActionViewInstance.Initialize(tileAction);
                _tileActionsViews.Add(tileActionViewInstance);
            }
            UpdatePopupTransform();
        }

        protected override void OnAwake()
        {
            _closeButton.onClick.AddListener(Hide);
            _camera = Camera.main;
        }

        protected override void OnHide()
        {
            base.OnHide();
            for (int i = 0; i < _tileActionViewsContainer.childCount; i++)
            {
                Destroy(_tileActionViewsContainer.GetChild(i).gameObject);
            }
            _tileActionsViews.Clear();
        }

        protected void Update()
        {
            UpdatePopupTransform();
        }

        private void UpdatePopupTransform()
        {
            var targetScreenPoint = _camera.WorldToScreenPoint(_tileWorldPoint);
            _popupWindow.position = targetScreenPoint;

            float size = _cameraSettings.MinMaxZoomBorder.y / _camera.orthographicSize;
            _popupWindow.localScale = Vector3.one * size * _popupScale;
        }
    }
}
