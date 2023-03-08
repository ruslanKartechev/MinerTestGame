using Data.DTypes;
using Game.Controls;
using UnityEngine;

namespace Game.Mining
{
    public class PlayerMiningDetector: MonoBehaviour
    {
        [HideInInspector] public BaseMine TargetMine;
        [SerializeField] private float _rotToFaceTime;
        private IPlayerMover _playerMover;
        private BaseMine _closeMine;

        private void Awake()
        {
            _playerMover = GetComponent<IPlayerMover>();
        }

        private void OnEnable()
        {
            _playerMover.OnMovementStarted += OnMoveStarted;
            _playerMover.OnMovementStopped += OnStoppedMoving;
        }

        private void OnDisable()
        {
            _playerMover.OnMovementStopped -= OnStoppedMoving;
            _playerMover.OnMovementStarted -= OnMoveStarted;
        }

        private void OnMoveStarted()
        {
            if (TargetMine != null)
            {
                TargetMine.StopMining();
                TargetMine = null;
            }
        }

        private void OnStoppedMoving()
        {
            if (_closeMine != null)
            {
                InteractWith(_closeMine);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.CompareTag(Tags.Mine))
                return;
            var mine = other.gameObject.GetComponent<BaseMine>();
            if (mine == null)
                return;
            if (_playerMover.IsMoving)
            {
                _closeMine = mine;
                return;
            }
            InteractWith(mine);
        }

        private void InteractWith(BaseMine mine)
        {
            if (mine == TargetMine)
                return;
            if (TargetMine != null)
                TargetMine.StopMining();
            mine.StartMining();
            TargetMine = mine;
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.gameObject.CompareTag(Tags.Mine)) return;
            var mine = other.gameObject.GetComponent<BaseMine>();
            if (mine == null)
                return;
            
            if (mine == TargetMine)
            {
                mine.StopMining();
                TargetMine = null;
            }

            if (mine == _closeMine)
            {
                _closeMine = null;
            }
        }

    }
}