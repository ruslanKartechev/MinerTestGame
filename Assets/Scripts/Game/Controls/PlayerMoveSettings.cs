using UnityEngine;

namespace Game.Controls
{
    [CreateAssetMenu(fileName = nameof(PlayerMoveSettings), menuName = "SO/Settings/" + nameof(PlayerMoveSettings))]
    public class PlayerMoveSettings : ScriptableObject
    {
        [SerializeField] private float _moveSpeed = 1;
        [SerializeField] private float _acceleration = 1f;
        [Space(10)]
        [SerializeField] private float _rotSpeedMin = 1f;
        [SerializeField] private float _rotSpeedMax = 1f;
        [SerializeField] private float _turningSpeedMod = 0.2f;
        [Space(10)]
        [SerializeField] private float _rotXThreshold = 0.2f;
        [SerializeField] private float _backMoveYThreshold = 0f;
        [SerializeField] private float moveYRange = 0f;

        
        public float MoveSpeed => _moveSpeed;
        public float Acceleration => _acceleration;
        public float RotSpeedMin => _rotSpeedMin;
        public float RotSpeedMax => _rotSpeedMax;
        public float TurningSpeedMod => _turningSpeedMod;
        public float RotXThreshold => _rotXThreshold;
        public float BackMoveYThreshold => _backMoveYThreshold;
        public float MoveYRange => moveYRange;


    }
}