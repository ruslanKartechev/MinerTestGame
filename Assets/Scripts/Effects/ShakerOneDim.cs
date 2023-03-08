using System.Collections;
using Data.DTypes;
using UnityEngine;

namespace Effects
{
    public class ShakerOneDim : MonoBehaviour
    {
        [SerializeField] protected Transform _target;
        public RFloat Duration;
        public RFloat Frequency;
        public RFloat Distance;
        public Vector3 LocalAxis;
        
        protected Coroutine _shaking;
        protected Vector3 _startPos;
        
        
        public void StartShake1D()
        {
            StopShake();
            _startPos = _target.position;
            _shaking = StartCoroutine(Shaking1D(Duration.Value));
        }

        public void StopShake()
        {
            if (_shaking != null)
            {
                StopCoroutine(_shaking);
                _target.position = _startPos;
            }
            
        }

        protected IEnumerator Shaking1D(float duration)
        {
            var elased = 0f;
            var T = 1f / Frequency.Value;
            var quarterT = T / 2;
            var phase = UnityEngine.Random.Range(0f,1f) > 0.5f ? 1 : 3;
            var phaseElapsed = 0f;
            var startPos = _startPos;
            var pos = startPos;
            var worldAxis =transform.TransformVector(LocalAxis);
            var p1 = pos + worldAxis * Distance.Value;
            var p2 = pos - worldAxis * Distance.Value;
            
            while (elased <= duration)
            {
                var t = 0f;
                switch (phase)
                {
                    case 1:
                        t = phaseElapsed / quarterT;
                        pos = Vector3.Lerp(startPos, p1, t);
                        if (t >= 1)
                        {
                            phase++;
                            phaseElapsed = 0;
                        }
                        break;
                    case 2:
                        t = phaseElapsed / quarterT;
                        pos = Vector3.Lerp(p1, startPos, t);
                        if (t >= 1)
                        {
                            phase++;
                            phaseElapsed = 0;
                        }
                        break;
                    case 3:
                        t = phaseElapsed / quarterT;
                        pos = Vector3.Lerp(startPos, p2, t);
                        if (t >= 1)
                        {
                            phase++;
                            phaseElapsed = 0;
                        }
                        break;
                        case 4:
                            t = phaseElapsed / quarterT;
                            pos = Vector3.Lerp(p2, startPos, t);
                            if (t >= 1)
                            {
                                phase = 1;
                                phaseElapsed = 0;
                            }
                        break;
                        
                }

                _target.position = pos;
                phaseElapsed += Time.deltaTime;
                elased += Time.deltaTime;
                yield return null;
            }

            _target.position = startPos;
        }
        
        
    }
}