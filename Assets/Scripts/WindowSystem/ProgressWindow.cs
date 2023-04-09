using DanPie.Framework.WindowSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FlatVillage.WindowSystem
{
    public class ProgressWindow : WindowObject
    {
        [SerializeField] private TMP_Text _progressText;
        [SerializeField] private Slider _progressBar;

        public void SetProgressText(string text)
        {
            _progressText.text = text;
        }

        /// <summary>
        /// Progress should be interpolated from 0 to 1.
        /// </summary>
        public void SetProgressValue(float progress)
        {
            _progressBar.value = Mathf.Clamp01(progress);
        }
    }
}
