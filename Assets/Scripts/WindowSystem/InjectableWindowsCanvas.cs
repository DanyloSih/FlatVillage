using DanPie.Framework.WindowSystem;
using Zenject;

namespace FlatVillage.WindowSystem
{

    public class InjectableWindowsCanvas : WindowsCanvas
    {
        private DiContainer _container;

        [Inject]
        public void Construct(DiContainer container)
        {
            _container = container;
        }

        protected override T InstantiateWindow<T>(T windowPrefab)
        {
            return (T)_container.InstantiatePrefab(windowPrefab).GetComponent(typeof(T));
        }
    }
}
