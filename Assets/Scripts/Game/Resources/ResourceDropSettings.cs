using UnityEngine;

namespace Game.Resources
{
    [CreateAssetMenu(fileName = nameof(ResourceDropSettings), menuName = "SO/Resources/" + nameof(ResourceDropSettings))]
    public class ResourceDropSettings : ScriptableObject
    {
        [SerializeField] private float _dropUpOffset = 2f;
        [SerializeField] private float _dropTime = 0.6f;
        [SerializeField] private float _rotSpeed = 1f;
        [SerializeField] private float _isCollectableDelay;
     
        
        
        public float DropUpOffset => _dropUpOffset;

        public float DropTime => _dropTime;

        public float RotSpeed => _rotSpeed;

        public float IsCollectableDelay => _isCollectableDelay;
    }
}