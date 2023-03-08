using UnityEngine;
using UnityEngine.UI;

namespace GameUI
{
    public abstract class UIPage : MonoBehaviour
    {
        [SerializeField] protected Canvas _canvas;
        [SerializeField] protected GraphicRaycaster _raycaster;

        private bool _isOpen;
        public bool IsOpen
        {
            get => _isOpen;
            set
            {
                _isOpen = value;
                _canvas.enabled = value;
                _raycaster.enabled = value;
            }
        }

        public abstract void Show(bool animated = true);
        public abstract void Hide(bool animated = true);


    }
}