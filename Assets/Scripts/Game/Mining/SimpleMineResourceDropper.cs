using Game.Resources;
using UnityEngine;

namespace Game.Mining
{
    public class SimpleMineResourceDropper : MonoBehaviour
    {
        [SerializeField] private string _resourceID;
        [SerializeField] private DropResourcePool _pool;
        [SerializeField] private Transform _dropFromPosition;
        [SerializeField] private Transform _dropToPosition;
        [SerializeField] private float _dropRadiusMin;
        [SerializeField] private float _dropRadiusMax;

        public string ResourceID
        {
            get => _resourceID;
            set => _resourceID = value;
        }
        
        // ReSharper disable Unity.PerformanceAnalysis
        public void Drop(int count)
        {
            var vector = _dropToPosition.position - _dropFromPosition.position;
            vector.y = 0;
            vector.Normalize();
            var angleStep = 360 / (count);
            var randomAngleOffset = UnityEngine.Random.Range(0f, 360f);
            for (var i = 0; i < count; i++)
            {
                var instance = _pool.GetItem().Object;
                instance.transform.SetPositionAndRotation( _dropFromPosition.position, 
                    _dropFromPosition.rotation);
                var posVec = Quaternion.Euler(0, angleStep * (i) + randomAngleOffset, 0)
                             * vector
                             * UnityEngine.Random.Range(_dropRadiusMin, _dropRadiusMax);
                var toPos = _dropToPosition.position + posVec;
                instance.Drop(toPos);
            }
        }
        
        
    }
}