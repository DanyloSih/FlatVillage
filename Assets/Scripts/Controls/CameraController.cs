using DanPie.Framework.UnityExtensions;
using UnityEngine;
using Zenject;
using FlatVillage.Settings;

namespace FlatVillage.Controls
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private float _zoomOffsetForce = 1.5f;

        private IMainControlActionsProvider _mainControlActionsProvider;
        private CameraSettings _cameraSettings;
        private Camera _camera;
        private Vector2 _screenSize;

        [Inject]
        public void Construct(
            IMainControlActionsProvider mainControlActionsProvider,
            CameraSettings cameraSettings)
        {
            _mainControlActionsProvider = mainControlActionsProvider;
            _cameraSettings = cameraSettings;
        }

        protected void Start()
        {
            _camera = Camera.main;
            _screenSize = new Vector3(Screen.width, Screen.height);
            
        }

        protected void OnEnable()
        {
            _mainControlActionsProvider.Moved += OnMove;
            _mainControlActionsProvider.Zoom += OnZoom;
        }

        protected void OnDisable()
        {
            _mainControlActionsProvider.Moved -= OnMove;
            _mainControlActionsProvider.Zoom -= OnZoom;
        }

        private void OnZoom(float delta)
        {
            float sizeDelta = _camera.orthographicSize;
            _camera.orthographicSize
                = Mathf.Clamp(
                    _camera.orthographicSize + delta,
                    _cameraSettings.MinMaxZoomBorder.x,
                    _cameraSettings.MinMaxZoomBorder.y);
            sizeDelta = _camera.orthographicSize - sizeDelta;

            if (sizeDelta < 0)
            {
                var relativeMousePos 
                    = _mainControlActionsProvider.LastActionScreenPoint - _screenSize / 2;

                relativeMousePos 
                    = relativeMousePos.Merge(_screenSize / 2, (a, b) => a / b);

                OnMove(-relativeMousePos * _zoomOffsetForce * sizeDelta);
            }
        }

        private void OnMove(Vector2 delta)
        {
            Vector3 newPos = _camera.transform.position 
                + new Vector3(delta.x, delta.y, 0);

            _camera.transform.position = newPos;
        }
    }
}
