using DanPie.Framework.WindowSystem;

namespace FlatVillage.WindowSystem
{

    public class WindowsCanvasesManager
    {
        private WindowsCanvas _main;
        private WindowsCanvas _popup;
        private WindowsCanvas _debug;

        public WindowsCanvas Main { get => _main; }
        public WindowsCanvas Popup { get => _popup; }
        public WindowsCanvas Debug { get => _debug; }

        public WindowsCanvasesManager(WindowsCanvas main, WindowsCanvas popup, WindowsCanvas debug)
        {
            _main = main;
            _popup = popup;
            _debug = debug;
        }
    }
}
