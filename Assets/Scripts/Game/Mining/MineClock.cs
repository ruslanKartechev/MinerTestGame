using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Game.Mining
{
    public class MineClock : MonoBehaviour
    {

        [SerializeField] private TextMeshPro _text;
        [Header("Icon")]
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Sprite _miningSprite;
        [SerializeField] private Sprite _recoverSprite;
        [SerializeField] private Sprite _readySprite;
        [Header("Scaling")] 
        [SerializeField] private float _bouncingYPos;
        [SerializeField] private float _bouncingYMagn;
        [SerializeField] private float _bounceDur;
        
        [Header("Scaling")]
        [SerializeField] private float _bigScale = 1;
        [SerializeField] private float _smallScale = 0;
        [SerializeField] private float _scaleDur = 0.5f;

        private Coroutine _scaling;
        private Coroutine _lookRotating;
        private Tween _scaleTween;
        private Sequence _bouncing;
        
        private void SetupIconIdleAnim()
        {
            _bouncing?.Kill();
            _bouncing = DOTween.Sequence();
            _bouncing.Append(_spriteRenderer.transform.DOLocalMoveY(_bouncingYPos + _bouncingYMagn, _bounceDur)).SetEase(Ease.Linear)
                .Append(_spriteRenderer.transform.DOLocalMoveY(_bouncingYPos - _bouncingYMagn, _bounceDur * 2)).SetEase(Ease.Linear)
                .Append(_spriteRenderer.transform.DOLocalMoveY(_bouncingYPos, _bounceDur));
            _bouncing.SetLoops(-1);
        }

        public void Show()
        {
            _scaleTween.Kill();
            _text.transform.localScale = Vector3.one * _smallScale;
            _scaleTween = _text.transform.DOScale(Vector3.one * _bigScale, _scaleDur);
            SetupIconIdleAnim();
        }

        public void Hide()
        {
            _scaleTween.Kill();
            _text.transform.localScale = Vector3.one * _smallScale;
            _scaleTween = _text.transform.DOScale(Vector3.one * _smallScale, _scaleDur);
        }

        public void UpdateTime(float time)
        {
            _text.text = $"{time:N1}";
        }

        public void SetMiningMode()
        {
            _spriteRenderer.sprite = _miningSprite;
        }

        public void SetRecoveryMode()
        {
            _spriteRenderer.sprite = _recoverSprite;
        }

        public void SetAvailableMode()
        {
            _spriteRenderer.sprite = _readySprite;
            _text.text = "";
        }
        
        
    }
}