using System;
using Game.Animations;
using UnityEngine;
using UnityEngine.AI;

namespace Game.Controls
{
    public class PlayerMoveController : MonoBehaviour, IPlayerMover
    {
        public event Action OnMovementStarted;
        public event Action OnMovementStopped;

        [SerializeField] private NavMeshAgent _navMeshAgent;
        [SerializeField] private InputChannel _inputChannel;
        [SerializeField] private PlayerMoveSettings _settings;
        private IPlayerAnimator _animator;
        
        private bool _isMoving;
        private bool _isWalking;
        private bool _allowed;

        public bool AllowMovement
        {
            get => _allowed;
            set
            {
                _allowed = value;
                if (value)
                {
                    _inputChannel.Move += Move;
                    _inputChannel.Stop += Stop;
                }
                else
                {
                    _inputChannel.Move -= Move;
                    _inputChannel.Stop -= Stop;
                    Stop();
                }
            }
        }

        public bool IsMoving => _isMoving;

        private void Awake()
        {
            _animator = GetComponent<IPlayerAnimator>();
        }

        public void SetupMover(PlayerMoveSettings settings)
        {
            _settings = settings;
            _navMeshAgent.speed = _settings.MoveSpeed;
            _navMeshAgent.acceleration = _settings.Acceleration;
        }
        
        private void OnEnable()
        {
            _inputChannel.Move += Move;
            _inputChannel.Stop += Stop;
        }

        private void OnDisable()
        {
            _inputChannel.Move -= Move;
            _inputChannel.Stop -= Stop;   
        }

        private void Stop()
        {
            _isMoving = false;
            _isWalking = false;
            _navMeshAgent.isStopped = true;
            _navMeshAgent.updateRotation = false;
            _animator.PlayIdle();
            OnMovementStopped?.Invoke();
        }

        private void Move(Vector2 dir, float force)
        {
            _navMeshAgent.updateRotation = true;
            var position = transform.position;
            var forward = transform.forward;
            var horInputMagnitude = Mathf.Abs(dir.x);
            if(horInputMagnitude >= _settings.RotXThreshold)
            {
                float rotAmount;
                var rotDir = dir.x;
                if (dir.y < _settings.BackMoveYThreshold)
                {
                    forward = -transform.forward;
                    rotDir *= -1;
                }
                rotAmount = Mathf.Sign(rotDir)
                                * Mathf.Lerp(_settings.RotSpeedMin, _settings.RotSpeedMax, horInputMagnitude)
                                * Time.deltaTime;
                transform.Rotate(transform.up, rotAmount);   
            }
            else
            {
                if (dir.y < _settings.BackMoveYThreshold)
                    forward = -transform.forward;
            }
            var angularMod = Mathf.Lerp(1f, _settings.TurningSpeedMod, (horInputMagnitude - _settings.RotXThreshold));
            if (dir.y > _settings.MoveYRange|| dir.y < - _settings.MoveYRange)
            {
                _navMeshAgent.nextPosition = position + forward * (angularMod * force * _settings.MoveSpeed * Time.deltaTime);
                if (_isWalking == false)
                {
                    _navMeshAgent.isStopped = false;
                    _isWalking = true;
                    _animator.PlayRun();
                }
            }
            else
            {
                if (_isWalking)
                {
                    _isWalking = false;
                    _animator.PlayIdle();
                }
            }
            if (_isMoving == false)
            {
                _isMoving = true;
                OnMovementStarted?.Invoke();
            }
        }

        
        
        #if UNITY_EDITOR
        private void Update()
        {
            return;
            var input = new Vector2(0, 0);
            if (Input.GetKey(KeyCode.W))
            {
                input.y = 1;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                input.y = -1;
            }
        
            if (Input.GetKey(KeyCode.D))
            {
                input.x = 1;
            }
            else if (Input.GetKey(KeyCode.A))
            {
                input.x = -1;
            }

            if (input == Vector2.zero)
            {
                if(_isMoving)
                    Stop();
            }
            else
                Move(input, 1);
        }
        #endif
    }
}