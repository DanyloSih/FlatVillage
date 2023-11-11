using System.Linq;
using FlatVillage.Maps;
using UnityEngine;
using Zenject;

namespace FlatVillage.Tests.Controls
{
    public class MapTouchProvider : MonoBehaviour
    {
        private Camera _mainCamera;
        private Plane _plane;
        private BasicMap _baseMap;

        [Inject]
        public void Construct(BasicMap baseMap)
        {
            _baseMap = baseMap;
        }

        protected void Start()
        {
            _mainCamera = Camera.main;
            Transform cameraTransform = _mainCamera.transform;
            _plane = new Plane(-cameraTransform.forward, cameraTransform.position + cameraTransform.forward * 10);
        }

        protected void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

                if (_plane.Raycast(ray, out float distance))
                {
                    Vector3 hitPoint = ray.GetPoint(distance);
                    Vector2Int tileCenter = new Vector2Int(
                        Mathf.RoundToInt(hitPoint.x - 0.5f),
                        Mathf.RoundToInt(hitPoint.y - 0.5f)
                    );
                    tileCenter += _baseMap.Matrix.Size / 2;

                    _baseMap.SetTile(tileCenter, 0);
                    var neighbors = _baseMap.Matrix.GetNeighboursOfMatrixMember(tileCenter)
                        .GetAvailableNeighbours().ToList();

                    for (int i = 0; i < neighbors.Count; i++)
                    {
                        _baseMap.SetTile(
                            _baseMap.Matrix.FromIDToVector(neighbors[i].Value.ID), i % 5);
                    }
                }
            }
        }
    }
}
