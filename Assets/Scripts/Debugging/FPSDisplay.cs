using UnityEngine;
using UnityEngine.UI;

namespace FlatVillage.Debugging
{
    public class FPSDisplay : MonoBehaviour
    {
        [SerializeField] private Text _fpsText;

        private float _deltaTime = 0.0f;

        protected void Update()
        {
            _deltaTime += (Time.unscaledDeltaTime - _deltaTime) * 0.1f;

            float fps = 1.0f / _deltaTime;
            _fpsText.text = string.Format("FPS: {0:0.}", fps);
        }
    }

}
