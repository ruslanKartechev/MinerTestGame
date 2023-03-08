using DG.Tweening;
using Game.Main;
using Game.Resources;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameUI
{
    public class ResourceUIBlock : MonoBehaviour
    {
        [SerializeField] private ResourceIconsRepository _icons;
        [SerializeField] private TextMeshProUGUI _countText;
        [SerializeField] private Image _icon;
        [SerializeField] private float _scaleChange;
        [SerializeField] private GlobalData _data;
        private Resource _res;
        private Sequence _scaling;
        private float _beforeCount;
        private bool _isActive;
        
        public bool IsActive => _isActive;

        public ResourceBlockPositioner Positioner { get; set; }
        
        public void InitFor(Resource res)
        {
            if (_res != null)
                _res.Amount.UnsubOnChange(OnAmountChange);
            _beforeCount = res.Amount.Value;
            _res = res;
            _icon.sprite = _icons.GetIcon(res.ID);
            _countText.text = $"{res.Amount.Value}";
            _res.Amount.SubOnChange(OnAmountChange);
            if (res.Amount.Value == 0)
                HideNow();
            else
                ShowNow();
        }
        
        private void OnAmountChange(float count)
        {
            _scaling?.Kill();
            _scaling = DOTween.Sequence();
            _countText.transform.localScale = Vector3.one;
            _countText.text = $"{_beforeCount}";
            var scaleDur = _data.UIResourceScaleDuration;
            if (count <= _beforeCount)
            {
                _scaling.Append(_countText.transform.DOScale(Vector3.one * (1f - _scaleChange), scaleDur))
                    .OnComplete(() => { _countText.text = $"{count}"; })
                    .Append(_countText.transform.DOScale(Vector3.one, scaleDur));
            }
            else
            {
                _scaling.Append(_countText.transform.DOScale(Vector3.one * (1f + _scaleChange), scaleDur))
                    .OnComplete(() => { _countText.text = $"{count}"; })
                    .Append(_countText.transform.DOScale(Vector3.one, scaleDur));
            }
            _beforeCount = count;
            if (count == 0 && _isActive)
                HideAnim();
            if (!_isActive && count > 0)
                ShowAnim();
        }
        
        private void HideNow()
        {
            _isActive = false;
            gameObject.SetActive(false);      
        }

        private void ShowNow()
        {
            gameObject.SetActive(true);      
            _isActive = true;
            transform.localScale = Vector3.one;
        }

        private void HideAnim()
        {
            _isActive = false;
            transform.DOScale(Vector3.zero, 0.5f).OnComplete(() => { gameObject.SetActive(false);});
            Positioner.RemoveFromGrid(this);
        }

        private void ShowAnim()
        {
            gameObject.SetActive(true);      
            _isActive = true;
            transform.DOScale(Vector3.one, 0.5f);
            Positioner.AddToGrid(this);
        }
        
    }
}