using System;
using System.Collections;
using Bezier;
using DG.Tweening;
using Pool;
using UnityEngine;

namespace Game.Resources
{
    public class FlyingRes : MonoBehaviour, IPooledObject<FlyingRes>
    {
        [SerializeField] private FlyingResSettings _settings;
        private IPool<FlyingRes> _pool;
        public Transform InterPart;
        private int _sideDir = 1;

        public void Show()
        {
            gameObject.SetActive(true);
        }
        
        public void Hide()
        {
            _pool.ReturnItem(this);
            gameObject.SetActive(false);
        }
        
        public void Init(IPool<FlyingRes> pool)
        {
            _pool = pool;
            gameObject.SetActive(false);
        }

        public void CollectBack()
        {
            gameObject.SetActive(false);
        }

        public void FlyFromTo(Transform from, Transform to, Action onEnd)
        {
            var sideOffset = _settings.SideOffset;
            transform.position = from.position;
            var side = Vector3.Cross((to.position - from.position), Vector3.up).normalized 
                       * (_sideDir * sideOffset);
            var inf = Vector3.Lerp(from.position, to.position, _settings.CurveInflectionPos);
            inf += side + Vector3.up * _settings.CurveInflectionHeight;
            transform.position = from.position;
            // if (_settings.OscillationsCount > 0)
            // {
            //     InterPart.DOLocalMoveX(_settings.OscillationsMagnitude, _settings.FlyingTime / _settings.OscillationsCount)
            //         .SetLoops(_settings.OscillationsCount);
            // }
            
            _sideDir *= -1;
            gameObject.SetActive(true);
            StartCoroutine(MoveOnCurve(transform.position, inf,to.position, _settings.FlyingTime, onEnd));
        }

        private IEnumerator MoveOnCurve(Vector3 from, Vector3 inf, Vector3 to, float time, Action onEnd)
        {
               
            var elapsed = 0f;
            while (elapsed <= time)
            {
                var pos = BezierCalculator.GetPointQuadratic(from, inf, to, elapsed / time);
                transform.position = pos;
                var ol = Mathf.Sin(elapsed * _settings.OscillationsSpeed) * _settings.OscillationsMagnitude;
                InterPart.localPosition = new Vector3(ol, ol, 0);
                elapsed += Time.deltaTime;
                yield return null;
            }
            transform.position = to;
            onEnd.Invoke();
        }
        
        public FlyingRes Object => this;
    }
}