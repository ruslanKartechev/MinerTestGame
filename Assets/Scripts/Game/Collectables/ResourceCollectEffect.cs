using DG.Tweening;
using UnityEngine;

namespace Game.Collectables
{
    public class ResourceCollectEffect : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private ParticleSystem _poofParticles;
        [SerializeField] private float _downScaleTime;
        [SerializeField] private float _punchTime;
        [SerializeField] private float _punchScale;
        private Sequence _scaling;

        private void OnEnable()
        {
            transform.localScale = Vector3.one;
        }

        public void PlayOnCollected()
        {
            _rigidbody.isKinematic = true;
            _poofParticles.transform.parent = transform.parent;
            _poofParticles.Play();
            _poofParticles.transform.position = transform.position;
            _scaling = DOTween.Sequence();
            _scaling.Append(transform.DOScale(Vector3.one * _punchScale, _punchTime))
                .Append(transform.DOScale(Vector3.one * 0, _downScaleTime))
                .OnComplete(() =>
                {
                    gameObject.SetActive(false);
                    _poofParticles.gameObject.SetActive(false);
                } );
            
        }

    }
}