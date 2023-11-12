using System.Collections.Generic;
using System.Linq;
using DanPie.Framework.DMath;
using DanPie.Framework.UnityExtensions;
using FlatVillage.Controls;
using FlatVillage.Maps;
using FlatVillage.WindowSystem;
using UnityEngine;
using Zenject;

namespace FlatVillage.Gameplay
{
    public class TileActionsInvoker : MonoBehaviour
    {
        [SerializeField] private TileActionsWindow _tileActionsWindowPrefab;

        private BasicMap _baseMap;
        private WindowsCanvasesManager _windowsCanvasesManager;
        private List<ITileAction<int, TileInfo>> _tileActions;
        private IMainControlActionsProvider _mainControlActionsProvider;
        private Camera _camera;
        private TileActionsWindow _tileActionsWindow;
        private Vector2Int _previousMatrixPos;

        [Inject]
        public void Construct(
            WindowsCanvasesManager windowsCanvasesManager,
            List<ITileAction<int, TileInfo>> tileActions,
            IMainControlActionsProvider mainControlActionsProvider,
            BasicMap baseMap)
        {
            _baseMap = baseMap;
            _windowsCanvasesManager = windowsCanvasesManager;
            _tileActions = tileActions;
            _mainControlActionsProvider = mainControlActionsProvider;
        }

        protected void Start()
        {
            _camera = Camera.main;
            _tileActionsWindow = _windowsCanvasesManager.Popup
                .InstanceOf(_tileActionsWindowPrefab);
        }

        protected void OnEnable()
        {
            _mainControlActionsProvider.Clicked += OnTileClick;
        }

        protected void OnDisable()
        {
            _mainControlActionsProvider.Clicked -= OnTileClick;
        }

        private void OnTileClick(Vector2 worldPoint)
        {
            int layer = LayerMask.NameToLayer("Ignore Raycast");
            Vector2Int matrixPos = _baseMap.WorldPositionToMatrixPosition(worldPoint);
            if (!_camera.IsPointerHitUI(
                _mainControlActionsProvider.LastActionScreenPoint, new int[] { layer }))
            {
                foreach (var tile in _tileActions)
                {
                    tile.UpdateTileContext(_baseMap, matrixPos);
                }

                if (_tileActionsWindow.IsVisible && _previousMatrixPos == matrixPos)
                {
                    _tileActionsWindow.Hide();     
                }
                else
                {
                    ShowTileActions(_baseMap.TilemapMatrixPointsConverter
                       .MatrixPointToTilemapPoint(matrixPos));
                }
            }

            _previousMatrixPos = matrixPos;
        }

        private void ShowTileActions(Vector2 worldPoint)
        {
            _windowsCanvasesManager.Popup.ShowAlso(_tileActionsWindow.GetType());
            _tileActionsWindow.Initialize(_tileActions.Cast<ITileActionInfo>(), worldPoint);
        }
    }
}
