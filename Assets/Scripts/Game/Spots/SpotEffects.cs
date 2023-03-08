using DG.Tweening;
using UnityEngine;

namespace Game.Spots
{
    public class SpotEffects : MonoBehaviour
    {
        [SerializeField] private Transform _rotTarget;
        [SerializeField] private float _jumpTime;
        [SerializeField] private float _jumpY;
        [SerializeField] private int _spinsCount;
        [SerializeField] private float _rotBackOnStopTime = 0.25f;
        [Space(10)]
        [SerializeField] private Renderer _floor;
        [SerializeField] private Material _aciveMat;
        [SerializeField] private Material _passiveMat;
        
        
        public void ShowStartInteraction()
        {
            _floor.material = _aciveMat;
        }
        
        public void ShowStopInteraction()
        {
            _floor.material = _passiveMat;
            _rotTarget.DOKill();
            var angle = _rotTarget.localEulerAngles.y;
            if(angle % 90 != 0)
                _rotTarget.DOLocalRotateQuaternion(Quaternion.Euler(new Vector3(0, 0, 0)), _rotBackOnStopTime);
        }

        public void ShowProduction(float time)
        {
            _rotTarget.DOKill();
            var t = 0.5f * time / _spinsCount;
            _rotTarget.DOLocalRotateQuaternion(Quaternion.Euler(new Vector3(0f, 180f, 0f)), t).SetLoops(_spinsCount * 2);
        }

        public void ShowOutput()
        {
            var startY = _rotTarget.localPosition.y;
            var sequence = DOTween.Sequence();
            sequence.Append(_rotTarget.DOLocalMoveY(_jumpY, _jumpTime/2))
                .Append(_rotTarget.DOLocalMoveY(startY, _jumpTime/2)).SetEase(Ease.Linear);

            // var eulers = _rotTarget.localEulerAngles;
            // eulers.y += 90;
            // _rotTarget.DOLocalRotate(eulers, _rotTime);
        }
        
        
        
        
    }
}