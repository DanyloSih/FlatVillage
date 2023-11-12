using System;
using System.Collections;
using FlatVillage.Gameplay;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FlatVillage.Views
{
    public class TileActionView : MonoBehaviour
    {
        [SerializeField] private Image _background;
        [SerializeField] private TMP_Text _actionText;
        [SerializeField] private Button _actionButton;
        [SerializeField] private Color _errorColor = Color.red;
        [SerializeField] private float _errorAnimationTime = 1.5f;

        private Color _startColor;
        private ITileActionInfo _tileAction;
        private Coroutine _errorAnimationCoroutine;

        public void Initialize(ITileActionInfo tileAction)
        {
            _startColor = _background.color;
            _tileAction = tileAction;
            _actionText.text = tileAction.GetItemInfo().GetDisplayName();
        }

        protected void OnEnable()
        {
            _actionButton.onClick.AddListener(OnClick);
        }
        
        protected void OnDisable()
        {
            _actionButton.onClick.RemoveListener(OnClick);
        }

        private void OnClick()
        {
            if (!_tileAction.TryInvoke())
            {
                PlayErrorAnimation();
            }
        }

        private void PlayErrorAnimation()
        {
            if (_errorAnimationCoroutine != null)
            {
                StopCoroutine(_errorAnimationCoroutine);
            }

            _errorAnimationCoroutine = StartCoroutine(ErrorAnimation());
        }

        private IEnumerator ErrorAnimation()
        {
            _background.color = _errorColor;
            yield return new WaitForSeconds(_errorAnimationTime);
            _background.color = _startColor;
            _errorAnimationCoroutine = null;
        }
    }
}
