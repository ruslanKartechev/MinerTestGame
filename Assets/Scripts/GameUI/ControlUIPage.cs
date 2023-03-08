using System.Collections;
using Game.Controls;
using Game.Main;
using UnityEngine;
using UnityEngine.UI;

namespace GameUI
{
    public class ControlUIPage : UIPage
    {
        [SerializeField] private Image _joystick;
        [SerializeField] private Transform _mainBlock;
        [SerializeField] private JoystickSettings _joystickSettings;
        [SerializeField] private InputChannel _inputChannel;
        [SerializeField] private ImageFader _imageFader;
        [SerializeField] private GlobalData _data;
        private float _range;
        private float _sensitivity;
        private Vector2 _stickPos;
        private RectTransform _stickRect;
        private Coroutine _inputChecking;

        private void Awake()
        {
            _imageFader.FadeDuration = _data.UIJoystickFadeDuration;
            _mainBlock.gameObject.SetActive(false);
            _stickRect = _joystick.GetComponent<RectTransform>();
            _stickRect.anchoredPosition = _stickPos = Vector2.zero;
            _range = _joystickSettings.Range;
            _sensitivity = _joystickSettings.Sensitivity;
        }
      

        public override void Show(bool animated = true)
        {
            IsOpen = true;
        }

        public override void Hide(bool animated = true)
        {
            StopInputCheckAndReset();
            IsOpen = false;
        }

        private IEnumerator InputCheck()
        {
            var oldPos = (Vector2)Input.mousePosition;
            Vector2 newPos;
            while (true)
            {
                newPos = Input.mousePosition;
                var diff = newPos - oldPos;
                _stickPos += diff.normalized * (Time.deltaTime * _sensitivity * 100);
                var dist = _stickPos.magnitude;
                if (dist > _range)
                {
                    _stickPos = _stickPos / dist * _range;
                    dist = _range;
                }
                _stickRect.anchoredPosition = _stickPos;
                _inputChannel.RaiseMove(_stickPos.normalized, dist / _range);
                oldPos = newPos;
                yield return null;
            }
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                OnClick();   
            }
            else if(Input.GetMouseButtonUp(0))
            {
                OnRelease();
            }
        }

        private void OnClick()
        {
            _mainBlock.gameObject.SetActive(true);
            _imageFader.Show(false);
            _mainBlock.position = Input.mousePosition;
            if(_inputChecking != null)
                StopCoroutine(_inputChecking);
            _inputChecking = StartCoroutine(InputCheck());
        }

        private void OnRelease()
        {
            _imageFader.Hide(true);
            StopInputCheckAndReset();
            _inputChannel.RaiseStop();
        }

        private void StopInputCheckAndReset()
        {
            if(_inputChecking != null)
                StopCoroutine(_inputChecking);
            _stickRect.anchoredPosition = _stickPos = Vector2.zero;
        }
   
        
    }
}