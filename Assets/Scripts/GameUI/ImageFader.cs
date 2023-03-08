using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace GameUI
{
    public class ImageFader : MonoBehaviour
    {
        public float FadeDuration = 0.3f;
        [SerializeField] private List<Image> _images;
        private List<ImageData> _data;
        private void Awake()
        {
            _data = new List<ImageData>();
            foreach (var im in _images)
            {
                _data.Add(new ImageData(im));
            }
        }
        
        public void Show(bool animated)
        {
            if (animated)
            {
                foreach (var d in _data)
                {
                    d.Im.DOKill();
                    d.Im.DOFade(d.StartAlpha, FadeDuration);
                }   
            }
            else
            {
                foreach (var d in _data)
                {
                    d.Im.DOKill();
                    d.Im.color = new Color(d.Im.color.r, d.Im.color.g, d.Im.color.b, d.StartAlpha);
                }   
            }
        }

        public void Hide(bool animated)
        {
            if (animated)
            {
                foreach (var d in _data)
                {
                    d.Im.DOKill();
                    d.Im.DOFade(0f, FadeDuration);
                }   
            }
            else
            {
                foreach (var d in _data)
                {
                    d.Im.DOKill();
                    d.Im.color = new Color(d.Im.color.r, d.Im.color.g, d.Im.color.b, 0);
                }   
            }
        }


        private class ImageData
        {
            public Image Im;
            public float StartAlpha;

            public ImageData(Image im)
            {
                Im = im;
                StartAlpha = im.color.a;
            }
        }
    }
}