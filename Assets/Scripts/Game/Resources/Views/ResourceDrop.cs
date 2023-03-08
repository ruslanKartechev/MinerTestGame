using System.Collections;
using Bezier;
using DG.Tweening;
using Game.Collectables;
using Pool;
using UnityEngine;

namespace Game.Resources
{
    public class ResourceDrop : ResourceView, IPooledObject<ResourceView>
    {
        [SerializeField] private Rigidbody _rb;
        [SerializeField] private Collider _collider;
        [SerializeField] private ResourceDropSettings _settings;
        [SerializeField] private CollectableResource _collectable;
        
        private IPool<ResourceView> _pool;

        public void Init(IPool<ResourceView> pool)
        {
            _pool = pool;
            _collectable.OnCollected += OnCollected;
            gameObject.SetActive(false);
        }
        
        public void CollectBack()
        {
            gameObject.SetActive(false);   
        }

        public ResourceView Object => this;
        
        
        public override void Drop(Vector3 endPos)
        {
            gameObject.SetActive(true);
            _collectable.SetCollectable(false);
            _rb.isKinematic = true;
            _collider.enabled = true;
            transform.DOKill();
            StopAllCoroutines();
            StartCoroutine(Dropping(transform.position, endPos, _settings.DropTime));
            StartCoroutine(SetCollectable(_settings.IsCollectableDelay));
        }

        private IEnumerator Dropping(Vector3 start, Vector3 endPos, float time)
        {
            var inflection = Vector3.Lerp(start, endPos, 0.5f) + Vector3.up * _settings.DropUpOffset;
            var elapsed = 0f;
            var rotSpeed = _settings.RotSpeed;
            while (elapsed < time)
            {
                transform.Rotate(Vector3.up, rotSpeed * Time.deltaTime);
                transform.position = BezierCalculator.GetPointQuadratic(start, inflection, endPos, elapsed / time);
                elapsed += Time.deltaTime;
                yield return null;
            }
            OnDropped();
        }
        
        private void OnDropped()
        {
            _rb.isKinematic = false;
            _rb.AddForce(Vector3.down, ForceMode.Impulse);
        }

        private IEnumerator SetCollectable(float delay)
        {
            yield return new WaitForSeconds(delay);
            _collectable.SetCollectable(true);
        }
        
        private void OnCollected()
        {
            gameObject.SetActive(false);
            _pool.ReturnItem(this);
        }
    }
}