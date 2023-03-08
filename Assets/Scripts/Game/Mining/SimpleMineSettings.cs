using UnityEngine;

namespace Game.Mining
{
    [CreateAssetMenu(fileName = nameof(SimpleMineSettings), menuName = "SO/Repository/" + nameof(SimpleMineSettings))]
    public class SimpleMineSettings : ScriptableObject
    {
        [SerializeField] private int _dropCount;
        [SerializeField] private float _miningDuration;
        [SerializeField] private float _recoveringTime;
        [SerializeField] private int _hitsToStop;

        public float MiningDuration => _miningDuration;
        public float RecoveringTime => _recoveringTime;
        public int HitsToStop => _hitsToStop;
        public int DropCount => _dropCount;

    }
}