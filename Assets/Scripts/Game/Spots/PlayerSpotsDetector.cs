using Data.DTypes;
using Game.Controls;
using Game.Resources;
using UnityEngine;

namespace Game.Spots
{
    public class PlayerSpotsDetector : MonoBehaviour
    {
        [HideInInspector] public Spot TargetSpot;
        public ResourcesInventory Inventory;
        public Transform FlyFrom;
        private IPlayerMover _playerMover;
        private Spot _closeSpot;

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
            if (TargetSpot != null)
            {
                TargetSpot.StopInteraction();
                TargetSpot = null;
            }
        }

        private void OnStoppedMoving()
        {
            if (_closeSpot != null)
            {
                InteractWith(_closeSpot);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.CompareTag(Tags.Spot)) 
                return;
            var spot = other.gameObject.GetComponent<Spot>();
            if (spot == null) 
                return;
            if (_playerMover.IsMoving)
            {
                _closeSpot = spot;
                return;
            }
            InteractWith(spot);
        }

        private void InteractWith(Spot spot)
        {
            if (spot == TargetSpot) 
                return;
            if(TargetSpot != null)
                TargetSpot.StopInteraction();
            spot.Interact(new Spot.SpotWorkingArgs(){Inventory = Inventory, GiveTo = FlyFrom, TakeFrom = FlyFrom});
            TargetSpot = spot;
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (!other.gameObject.CompareTag(Tags.Spot)) return;
            var spot = other.gameObject.GetComponent<Spot>();
            if (spot == null) 
                return;
            if (spot == TargetSpot)
            {
                spot.StopInteraction();
                TargetSpot = null;
            }

            if (spot == _closeSpot)
            {
                _closeSpot = null;
            }
        }
        
        
    }
}