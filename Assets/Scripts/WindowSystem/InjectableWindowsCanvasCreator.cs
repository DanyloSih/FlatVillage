using System.Collections.Generic;
using DanPie.Framework.WindowSystem;
using UnityEngine;
using Zenject;

namespace FlatVillage.WindowSystem
{
    public class InjectableWindowsCanvasCreator<T> : WindowsCanvasesCreator<T>
        where T : WindowsCanvas
    {
        private DiContainer _container;
        private Transform _parent;
        private string[] _canvasesNames;

        public InjectableWindowsCanvasCreator(
            int ordersInLayer,
            string[] canvasesNames,
            DiContainer container,
            Transform parent) : base(ordersInLayer)
        {
            _container = container;
            _parent = parent;
            _canvasesNames = canvasesNames;
        }

        protected override T CreateWindowsCanvasObject(int windowsCanvasId)
        {
            var obj = _container.InstantiateComponentOnNewGameObject<T>(
                $"WindowsCanvas({_canvasesNames[windowsCanvasId]})");

            obj.transform.parent = _parent;
            return obj;
        }
    }
}
