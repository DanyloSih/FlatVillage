using UnityEngine;

namespace FlatVillage.Settings
{

    public class FPSLocker : MonoBehaviour
    {
        [SerializeField] private bool _vSync = false;
        [SerializeField] private bool _useDisplayMaxFPS;
        [SerializeField] private int _targetFPS = 60;

        protected void Start()
        {
            QualitySettings.vSyncCount = _vSync ? 1 : 0;

            Application.targetFrameRate = _useDisplayMaxFPS
                ? Mathf.RoundToInt(Screen.currentResolution.refreshRate + 2)
                : _targetFPS;
        }
    }
}
